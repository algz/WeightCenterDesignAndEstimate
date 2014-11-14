using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using BLL;
using System.IO;
using System.Xml;
using XCommon;
using ZedGraph;
using System.Collections;
using ZaeroModelSystem;
using System.Windows.Forms.DataVisualization.Charting;
using System.Net;
using Dev.PubLib;

namespace WeightCenterDesignAndEstimateSoft.Tool
{
    public partial class WeightDataMangeForm : Form
    {
        #region 属性

        private string strOperType = string.Empty;

        private BLLTypeWeightData bllTypeWeight = new BLLTypeWeightData();
        private string strPath = System.AppDomain.CurrentDomain.BaseDirectory;

        private List<WeightSortData> lstWeightSortData = null;
        private List<TypeWeightData> lstTypeWeight = null;
        private TreeNode selNode = null;

        private TypeWeightData typeWeightData = null;

        //private XmlNodeList lstNodeInstance = null;
        private XmlNodeList lstNode1 = null;
        private XmlNodeList lstNode2 = null;
        private XmlNodeList lstNode3 = null;
        private XmlNodeList lstModel = null;

        //private XmlNodeList lstClassNode = null;

        public WeightDataMangeForm()
        {
            InitializeComponent();

            SetPageButton();
            BindTypeData();
            BindWeightSortData(WeightSortManageForm.GetListWeightSortData());

            BindWeightData();
        }
        #endregion

        #region 自定义方法

        /// <summary>
        /// 绑定重量数据
        /// </summary>
        private void BindWeightData()
        {
            treeViewTypeList.Nodes.Clear();

            //型号重量数据
            lstTypeWeight = bllTypeWeight.GetListModel();

            if (lstTypeWeight.Count > 0)
            {
                TreeNode node = new TreeNode("型号重量数据");
                treeViewTypeList.Nodes.Add(node);

                foreach (TypeWeightData data in lstTypeWeight)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Name = data.Id.ToString();
                    childNode.Text = data.Helicopter_Name;

                    node.Nodes.Add(childNode);
                }
                node.Expand();
            }
        }

        /// <summary>
        /// 绑定机型类型
        /// </summary>
        private void BindTypeData()
        {
            cmbType.Items.Add("民用直升机");
            cmbType.Items.Add("军用运输直升机");
            cmbType.Items.Add("军用武装直升机");
            //cmbType.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定重量分类数据
        /// </summary>
        private void BindWeightSortData(List<WeightSortData> lstTempWeightSortData)
        {
            cmbWeightSort.Items.Clear();

            lstWeightSortData = new List<WeightSortData>();
            foreach (WeightSortData data in lstTempWeightSortData)
            {
                lstWeightSortData.Add(data.Clone());
            }

            cmbWeightSort.Items.Insert(0, string.Empty);

            if (lstWeightSortData != null && lstWeightSortData.Count > 0)
            {
                foreach (WeightSortData sortData in lstWeightSortData)
                {
                    cmbWeightSort.Items.Add(sortData.sortName);
                }
            }
        }

        /// <summary>
        /// 设置页面按钮
        /// </summary>
        private void SetPageButton()
        {
            if (strOperType == CommonMessage.operNone || strOperType == CommonMessage.operConfirm || strOperType == CommonMessage.operCancle)
            {
                txtName.Enabled = false;
                txtLastModifyTime.Enabled = false;
                cmbType.Enabled = false;
                txtCountry.Enabled = false;
                txtDesignTakingWeight.Enabled = false;
                txtMaxTakingWeight.Enabled = false;
                cmbWeightSort.Enabled = false;
                txtEmptyWeight.Enabled = false;


                btnConfirmModify.Enabled = false;
                btnConfirmCancle.Enabled = false;
                gridWeightData.ReadOnly = true;

                btnImportWeightData.Enabled = false;

                btnNew.Enabled = true;
                btnJYNew.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnRefresh.Enabled = true;
                btnImport.Enabled = true;
                btnExport.Enabled = true;

                treeViewTypeList.Enabled = true;
            }
            if (strOperType == CommonMessage.operNew || strOperType == CommonMessage.operJYNew || strOperType == CommonMessage.operEdit)
            {
                txtName.Enabled = true;
                txtLastModifyTime.Enabled = true;
                cmbType.Enabled = true;
                txtCountry.Enabled = true;
                txtDesignTakingWeight.Enabled = true;
                txtMaxTakingWeight.Enabled = true;
                cmbWeightSort.Enabled = true;
                txtEmptyWeight.Enabled = true;

                btnConfirmModify.Enabled = true;
                btnConfirmCancle.Enabled = true;
                gridWeightData.ReadOnly = false;

                btnImportWeightData.Enabled = true;

                btnNew.Enabled = false;
                btnJYNew.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnRefresh.Enabled = false;
                btnImport.Enabled = false;
                btnExport.Enabled = false;

                treeViewTypeList.Enabled = false;
            }
            txtLastModifyTime.Text = DateTime.Now.ToString();
        }

        private void IntiControl()
        {
            txtName.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtDesignTakingWeight.Text = string.Empty;
            txtEmptyWeight.Text = string.Empty;
            txtMaxTakingWeight.Text = string.Empty;
            cmbType.Text = string.Empty;
            txtLastModifyTime.Text = string.Empty;

            cmbWeightSort.Text = string.Empty;
            cmbType.SelectedIndex = -1;


            treeViewWeightSort.Nodes.Clear();

            gridWeightData.Rows.Clear();
            gridWeightData.Columns.Clear();

            //清除原来的图形
            chartTypeWeightData.Titles.Clear();
            chartTypeWeightData.Series.Clear();
            chartTypeWeightData.ChartAreas.Clear();
            chartTypeWeightData.Legends.Clear();
            chartTypeWeightData.ContextMenuStrip = null;
        }

        /// <summary>
        /// 设置操作标题
        /// </summary>
        /// <param name="strType"></param>
        private void SetOperTitle(string strType)
        {
            if (strType == CommonMessage.operNew)
            {
                gruWeightData.Text = "新建型号重量数据";
            }
            if (strType == CommonMessage.operJYNew)
            {
                gruWeightData.Text = "基于新建型号重量数据";
            }
            if (strType == CommonMessage.operEdit)
            {
                gruWeightData.Text = "编辑型号重量数据";
            }
            if (strType == CommonMessage.operCancle || strType == CommonMessage.operConfirm)
            {
                gruWeightData.Text = "型号重量数据";
            }
        }

        /// <summary>
        /// 获取直升机主要系统重量
        /// </summary>
        /// <returns></returns>
        public static string GetMainSystemWeight(WeightSortData sortData)
        {
            string strMainSystemWeight = string.Empty;
            if (sortData != null && sortData.lstWeightData.Count > 0)
            {
                strMainSystemWeight = sortData.sortName + "|";
                string strFH = "、";
                foreach (WeightData data in sortData.lstWeightData)
                {
                    //重量数据:名称,ID,单位,数值,nParentID
                    strMainSystemWeight += data.weightName + strFH + data.nID + strFH
                        + data.weightUnit + strFH + data.weightValue.ToString() + strFH + data.nParentID + strFH + "|";
                }

                if (strMainSystemWeight != string.Empty)
                {
                    strMainSystemWeight = strMainSystemWeight.Substring(0, strMainSystemWeight.Length - 2);
                }
            }

            return strMainSystemWeight;
        }

        /// <summary>
        /// 获取型号重量数据实体
        /// </summary>
        /// <returns></returns>
        private TypeWeightData GetPageTypeWeightData()
        {
            if (strOperType == CommonMessage.operEdit)
            {
                typeWeightData.Id = Convert.ToInt32(selNode.Name);
            }
            else
            {
                typeWeightData.Id = bllTypeWeight.GetMaxId();
            }

            typeWeightData.Helicopter_Name = txtName.Text;
            typeWeightData.Last_ModifyTime = DateTime.Now.ToString();
            typeWeightData.Helicopter_Type = cmbType.Text;
            typeWeightData.Helicoter_Country = txtCountry.Text;
            typeWeightData.DesignTaking_Weight = txtDesignTakingWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtDesignTakingWeight.Text);
            typeWeightData.MaxTaking_Weight = txtMaxTakingWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtMaxTakingWeight.Text);
            typeWeightData.EmptyWeight = txtEmptyWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtEmptyWeight.Text);

            return typeWeightData;
        }

        private WeightSortData GetSortData(string strFilePath)
        {
            string path = string.Empty;
            XmlNode node = null;

            WeightSortData sortData = new WeightSortData();

            if (!File.Exists(strFilePath))
            {
                return sortData;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(strFilePath);

            //重量分类名称
            path = "重量分类/重量分类名称";
            node = doc.SelectSingleNode(path);
            sortData.sortName = node.InnerText;

            //重量分类名称备注
            path = "重量分类/重量分类备注";
            node = doc.SelectSingleNode(path);
            sortData.strRemark = node.InnerText;

            //重量数据列表
            path = "重量分类/重量数据列表";
            node = doc.SelectSingleNode(path);

            List<WeightData> lstWeightData = new List<WeightData>();
            for (int m = 0; m < node.ChildNodes.Count; m++)
            {
                WeightData weightData = new WeightData();
                weightData.weightName = node.ChildNodes[m].ChildNodes[0].InnerText;
                weightData.strRemark = node.ChildNodes[m].ChildNodes[3].InnerText;

                lstWeightData.Add(weightData);
            }
            sortData.lstWeightData = lstWeightData;

            return sortData;

        }

        /// <summary>
        /// 获取与列表相同的重量分类
        /// </summary>
        /// <param name="sortData"></param>
        /// <returns></returns>
        private WeightSortData GetSameWeightSort(WeightSortData sortData)
        {
            WeightSortData sort = null;
            if (lstWeightSortData != null && lstWeightSortData.Count > 0 && sortData != null)
            {
                foreach (WeightSortData data in lstWeightSortData)
                {
                    if (WeightSortData.blIsSame(sortData, data))
                    {
                        sort = new WeightSortData();
                        sort = data;
                        return sort;
                    }
                }
            }
            return sort;
        }

        private WeightSortData GetZeroWeight(WeightSortData sortData)
        {
            WeightSortData tempWeightSortData = null;
            if (sortData != null)
            {
                tempWeightSortData = new WeightSortData();
                tempWeightSortData = sortData.Clone();
                for (int i = 0; i < tempWeightSortData.lstWeightData.Count; i++)
                {
                    tempWeightSortData.lstWeightData[i].weightValue = 0;
                }
            }
            return tempWeightSortData;
        }

        /// <summary>
        /// 页面验证
        /// </summary>
        /// <returns></returns>
        private string PageVerificationInfo()
        {
            string strErroInfo = string.Empty;

            //机型名称
            if (txtName.Text == string.Empty)
            {
                strErroInfo = "请输入机型名称";
                return strErroInfo;
            }
            else
            {
                if (Verification.IsCheckSignleString(txtName.Text))
                {
                    strErroInfo = "机型名称不能输入非法字符";
                    return strErroInfo;
                }
            }

            //机型国籍
            if (txtCountry.Text == string.Empty)
            {
                strErroInfo = "请输入机型国籍";
                return strErroInfo;
            }
            else
            {
                if (Verification.IsCheckString(txtCountry.Text))
                {
                    strErroInfo = "机型国籍不能输入非法字符";
                    return strErroInfo;
                }
            }

            //机型类型
            if (cmbType.SelectedIndex == -1)
            {
                strErroInfo = "请选择机型类型";
                return strErroInfo;
            }

            //设计起飞重量
            if (txtDesignTakingWeight.Text != string.Empty)
            {
                if (Verification.IsDoubleNumer(txtDesignTakingWeight.Text) == false)
                {
                    strErroInfo = "设计起飞重量应为数字";
                    return strErroInfo;
                }
            }

            //最大起飞重量
            if (txtMaxTakingWeight.Text != string.Empty)
            {
                if (Verification.IsDoubleNumer(txtMaxTakingWeight.Text) == false)
                {
                    strErroInfo = "最大起飞重量应为数字";
                    return strErroInfo;
                }
            }

            //空机重量

            if (txtEmptyWeight.Text == string.Empty)
            {
                strErroInfo = "请输入空机重量";
                return strErroInfo;
            }
            else
            {
                if (Verification.IsDoubleNumer(txtEmptyWeight.Text) == false)
                {
                    strErroInfo = "空机重量应为数字";
                    return strErroInfo;
                }
            }

            return strErroInfo;
        }

        /// <summary>
        /// 设置页面数据
        /// </summary>
        private void SettingPageData()
        {
            if (selNode == null || selNode.Level == 0)
            {
                IntiControl();
                return;
            }

            if (lstTypeWeight.Count > 0)
            {
                TypeWeightData weightData = GetTypeWeightData(selNode.Name);
                WeightSortData sortData = null;

                if (weightData != null)
                {
                    txtName.Text = weightData.Helicopter_Name;
                    txtLastModifyTime.Text = weightData.Last_ModifyTime;
                    cmbType.Text = weightData.Helicopter_Type;
                    txtCountry.Text = weightData.Helicoter_Country;
                    txtDesignTakingWeight.Text = weightData.DesignTaking_Weight.ToString();
                    txtMaxTakingWeight.Text = weightData.MaxTaking_Weight.ToString();
                    txtEmptyWeight.Text = weightData.EmptyWeight.ToString();
                    txtLastModifyTime.Text = weightData.Last_ModifyTime;

                    sortData = WeightDesignDataForm.clsStringToWeightSortData(weightData.MainSystem_Name);
                    if (sortData != null && sortData.lstWeightData.Count > 0)
                    {
                        WeightSortData tempSortData = GetSameWeightSort(sortData);
                        if (tempSortData == null)
                        {
                            lstWeightSortData.Add(GetZeroWeight(sortData));
                            BindWeightSortData(lstWeightSortData);
                            cmbWeightSort.Text = sortData.sortName;
                        }
                        else
                        {
                            cmbWeightSort.Text = tempSortData.sortName;
                        }

                        //绑定gridview
                        MainForm.BindWeightDesignGridView(sortData, gridWeightData);

                        //绑定重量分类数据
                        MainForm.BindWeightDesignSort(sortData, treeViewWeightSort);

                        //绑定图形
                        List<WeightData> lstWeightData = MainForm.GetPicListWeightData(sortData, gridWeightData);
                        MainForm.DisplayPiePic(chartTypeWeightData, lstWeightData, "重量分布");
                        if (lstWeightData != null && lstWeightData.Count > 0)
                        {
                            chartTypeWeightData.ContextMenuStrip = contextMenuStripWeightImage;
                        }
                        else
                        {
                            chartTypeWeightData.ContextMenuStrip = null;
                        }
                    }
                    else
                    {
                        cmbWeightSort.Text = string.Empty;

                        treeViewWeightSort.Nodes.Clear();

                        gridWeightData.Rows.Clear();
                        gridWeightData.Columns.Clear();

                        //清除原来的图形
                        chartTypeWeightData.Titles.Clear();
                        chartTypeWeightData.Series.Clear();
                        chartTypeWeightData.ChartAreas.Clear();
                        chartTypeWeightData.Legends.Clear();
                        chartTypeWeightData.ContextMenuStrip = null;
                    }
                }
            }
        }

        private WeightSortData GetWeightSortData(string strSortName)
        {
            WeightSortData sortData = null;
            if (lstWeightSortData != null && lstWeightSortData.Count > 0)
            {
                foreach (WeightSortData data in lstWeightSortData)
                {
                    if (strSortName == data.sortName)
                    {
                        sortData = data;
                    }
                }
            }
            return sortData;
        }

        /// <summary>
        /// 获取表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTableStruct()
        {
            DataTable table = new DataTable();

            //主旋翼桨叶重量
            table.Columns.Add("MainPaddle");

            //主旋翼桨毂重量
            table.Columns.Add("MainPropellerHub");

            //尾桨组件重量
            table.Columns.Add("ScullModule");

            //机身重量
            table.Columns.Add("Fuselage");

            //起落架重量
            table.Columns.Add("Undercarriage");

            //传动系统重量
            table.Columns.Add("TransmissionSystem");

            //燃油系统重量
            table.Columns.Add("FuelSystem");

            //动力子系统重量
            table.Columns.Add("PowerSubsystem");

            //飞行控制组件重量
            table.Columns.Add("RIFCA");

            return table;
        }

        /// <summary>
        /// 获取表结构
        /// </summary>
        /// <returns></returns>
        private DataTable GetTableExcleStruct()
        {
            DataTable table = new DataTable();

            //直升机名称
            table.Columns.Add("Helicopter_Name");

            //直升机类型
            table.Columns.Add("Helicopter_Type");

            //直升机国籍
            table.Columns.Add("Helicopter_Country");

            //直升机最大起飞重量
            table.Columns.Add("MaxTaking_Weight");

            //直升机设计起飞重量
            table.Columns.Add("DesignTaking_Weight");

            //直升机空机重量
            table.Columns.Add("Empty_Weight");

            //直升机空机重量
            table.Columns.Add("LastModify_Time");

            table.Columns.Add("Sort_Name");
            table.Columns.Add("ID");
            table.Columns.Add("Weight_Name");
            table.Columns.Add("Weight_Unit");
            table.Columns.Add("Weight_Value");
            table.Columns.Add("Weight_Remark");
            table.Columns.Add("Prarent_ID");


            return table;
        }

        /// <summary>
        /// 获取tables数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DataTable GetTableData(List<string> lstSort)
        {
            DataTable table = GetTableStruct();

            if (lstSort.Count > 0)
            {
                DataRow dr = table.NewRow();

                for (int i = 0; i < lstSort.Count; i++)
                {
                    string[] strArray = lstSort[i].Split('、');

                    if (i == 1)
                    {
                        dr["MainPaddle"] = CommonFunction.GetStringContent(strArray);
                    }
                    if (i == 2)
                    {
                        dr["MainPropellerHub"] = CommonFunction.GetStringContent(strArray);
                    }
                    if (i == 3)
                    {
                        dr["ScullModule"] = CommonFunction.GetStringContent(strArray);
                    }
                    if (i == 4)
                    {
                        dr["Fuselage"] = CommonFunction.GetStringContent(strArray);
                    }
                    if (i == 5)
                    {
                        dr["Undercarriage"] = CommonFunction.GetStringContent(strArray);
                    }
                    if (i == 6)
                    {
                        dr["TransmissionSystem"] = CommonFunction.GetStringContent(strArray);
                    }
                    if (i == 7)
                    {
                        dr["FuelSystem"] = CommonFunction.GetStringContent(strArray);
                    }
                    if (i == 8)
                    {
                        dr["PowerSubsystem"] = CommonFunction.GetStringContent(strArray);
                    }
                    if (i == 9)
                    {
                        dr["RIFCA"] = CommonFunction.GetStringContent(strArray);
                    }

                }
                table.Rows.Add(dr);
            }

            return table;
        }

        /// <summary>
        /// 获取tables数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DataTable GetTableExcleData(TypeWeightData weightData, WeightSortData sortData)
        {
            DataTable table = GetTableExcleStruct();

            DataRow drFirst = table.NewRow();

            drFirst["Helicopter_Name"] = weightData.Helicopter_Name;
            drFirst["Helicopter_Type"] = weightData.Helicopter_Type;
            drFirst["Helicopter_Country"] = weightData.Helicoter_Country;
            drFirst["MaxTaking_Weight"] = weightData.MaxTaking_Weight;
            drFirst["DesignTaking_Weight"] = weightData.DesignTaking_Weight;
            drFirst["Empty_Weight"] = weightData.EmptyWeight;
            drFirst["LastModify_Time"] = weightData.Last_ModifyTime;

            table.Rows.Add(drFirst);

            DataRow dr = null;
            if (sortData != null && sortData.lstWeightData.Count > 0)
            {
                foreach (WeightData data in sortData.lstWeightData)
                {
                    if (data == sortData.lstWeightData[0])
                    {
                        if (table.Rows.Count > 0)
                        {
                            table.Rows[0]["Sort_Name"] = sortData.sortName;

                            table.Rows[0]["ID"] = data.nID;
                            table.Rows[0]["Weight_Name"] = data.weightName;
                            table.Rows[0]["Weight_Unit"] = "千克";
                            table.Rows[0]["Weight_Remark"] = data.strRemark;
                            table.Rows[0]["Prarent_ID"] = data.nParentID;
                            table.Rows[0]["Weight_Value"] = data.weightValue;
                        }
                    }
                    else
                    {
                        dr = table.NewRow();

                        dr["Helicopter_Name"] = string.Empty;
                        dr["Helicopter_Type"] = string.Empty;
                        dr["Helicopter_Country"] = string.Empty;
                        dr["MaxTaking_Weight"] = string.Empty;
                        dr["DesignTaking_Weight"] = string.Empty;
                        dr["Empty_Weight"] = string.Empty;
                        dr["Sort_Name"] = string.Empty;

                        dr["ID"] = data.nID;
                        dr["Weight_Name"] = data.weightName;
                        dr["Weight_Unit"] = "千克";
                        dr["Weight_Remark"] = data.strRemark;
                        dr["Prarent_ID"] = data.nParentID;
                        dr["Weight_Value"] = data.weightValue;

                        table.Rows.Add(dr);
                    }
                }
            }

            return table;
        }

        /// <summary>
        /// 绑定gridview数据
        /// </summary>
        public void BindGridViewData(List<string> lstSort, DataGridView gridView)
        {
            DataTable table = GetTableData(lstSort);
            gridView.DataSource = table;
        }

        public static List<double> GetListWeightData(DataGridView gridWeightData)
        {
            List<double> lstValue = new List<double>();
            double dbValue = 0;

            for (int i = 0; i < gridWeightData.Rows.Count; i++)
            {
                for (int j = 0; j < gridWeightData.Columns.Count; j++)
                {
                    if (gridWeightData.Rows[i].Cells[j].Value != null)
                    {
                        if (gridWeightData.Rows[i].Cells[j].Value.ToString() != string.Empty)
                        {
                            dbValue = Convert.ToDouble(gridWeightData.Rows[i].Cells[j].Value);
                        }
                        lstValue.Add(dbValue);
                    }
                }
            }

            return lstValue;
        }

        public static List<string> GetListWeightTitle()
        {
            List<string> lstTitle = new List<string>();

            lstTitle.Add("主旋翼桨叶重量");
            lstTitle.Add("主旋翼桨毂重量");
            lstTitle.Add("尾桨部件重量");
            lstTitle.Add("机身重量");
            lstTitle.Add("起落架重量");
            lstTitle.Add("传动系统重量");
            lstTitle.Add("燃油系统重量");
            lstTitle.Add("动力子系统重量");
            lstTitle.Add("飞行控制组件重量");

            return lstTitle;
        }

        private List<string> GetExcleCloumn()
        {
            List<string> lstTitle = new List<string>();

            lstTitle.Add("直升机名称");
            lstTitle.Add("直升机类型");
            lstTitle.Add("直升机国籍");
            lstTitle.Add("直升机最大起飞重量");
            lstTitle.Add("直升机设计起飞重量");
            lstTitle.Add("直升机空机重量");
            lstTitle.Add("最后修改时间");
            lstTitle.Add("重量分类名称");

            lstTitle.Add("ID");
            lstTitle.Add("重量名称");
            lstTitle.Add("重量单位");
            lstTitle.Add("重量数值");
            lstTitle.Add("备注");
            lstTitle.Add("PARENTID");

            return lstTitle;
        }

        /// <summary>
        /// 获取重量结果内容
        /// </summary>
        /// <returns></returns>
        private List<string> GetListContent(TypeWeightData weightData)
        {
            List<string> lstContent = null;

            if (weightData != null)
            {
                lstContent = new List<string>();
                WeightSortData sortData = WeightDesignDataForm.clsStringToWeightSortData(weightData.MainSystem_Name);
                string strSortName = sortData == null ? string.Empty : sortData.sortName;

                lstContent.Insert(0, "<?xml version=" + "\"1.0\"" + " encoding=" + "\"gb2312\"" + " ?> ");

                List<double> lstValue = GetListWeightData(gridWeightData);
                List<string> lstTitle = GetListWeightTitle();

                lstContent.Add("<型号重量数据>");

                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<直升机名称>" + weightData.Helicopter_Name + "</直升机名称>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<直升机类型>" + weightData.Helicopter_Type + "</直升机类型>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<直升机国籍>" + weightData.Helicoter_Country + "</直升机国籍>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<设计起飞重量>" + weightData.DesignTaking_Weight.ToString() + "</设计起飞重量>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<最大起飞重量>" + weightData.MaxTaking_Weight.ToString() + "</最大起飞重量>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<空机重量>" + weightData.EmptyWeight.ToString() + "</空机重量>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<最后修改时间>" + weightData.Last_ModifyTime + "</最后修改时间>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<重量分类名称>" + strSortName + "</重量分类名称>");


                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<重量列表>");

                //重量数据

                List<string> lstWeightContent = MainForm.GetDesignWeightDataContent(3, sortData);
                foreach (string strContent in lstWeightContent)
                {
                    lstContent.Add(strContent);
                }

                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "</重量列表>");

                lstContent.Add("</型号重量数据>");
            }
            return lstContent;
        }

        /// <summary>
        /// 获取型号重量数据
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private TypeWeightData GetXmlWeightTypeData(string strPath)
        {
            TypeWeightData weightData = null;
            try
            {

                if (!File.Exists(strPath))
                {
                    return weightData;
                }

                weightData = new TypeWeightData();
                weightData.Id = bllTypeWeight.GetMaxId() + 1;

                string path = string.Empty;
                XmlNode node = null;

                XmlDocument doc = new XmlDocument();
                doc.Load(strPath);

                //直升机名称
                path = "型号重量数据/直升机名称";
                node = doc.SelectSingleNode(path);
                weightData.Helicopter_Name = node.InnerText;

                //直升机类型
                path = "型号重量数据/直升机类型";
                node = doc.SelectSingleNode(path);
                weightData.Helicopter_Type = node.InnerText;

                //直升机国籍
                path = "型号重量数据/直升机国籍";
                node = doc.SelectSingleNode(path);
                weightData.Helicoter_Country = node.InnerText;

                //最大起飞重量
                path = "型号重量数据/最大起飞重量";
                node = doc.SelectSingleNode(path);
                weightData.MaxTaking_Weight = Convert.ToDouble(node.InnerText);

                //设计起飞重量
                path = "型号重量数据/设计起飞重量";
                node = doc.SelectSingleNode(path);
                weightData.DesignTaking_Weight = Convert.ToDouble(node.InnerText);

                //空机重量
                path = "型号重量数据/空机重量";
                node = doc.SelectSingleNode(path);
                weightData.EmptyWeight = Convert.ToDouble(node.InnerText);

                //空机重量
                path = "型号重量数据/空机重量";
                node = doc.SelectSingleNode(path);
                weightData.EmptyWeight = Convert.ToDouble(node.InnerText);

                //最后修改时间
                path = "型号重量数据/最后修改时间";
                weightData.Last_ModifyTime = doc.SelectSingleNode(path).InnerText;

                //重量分类名称
                path = "型号重量数据/重量分类名称";
                string strSortName = doc.SelectSingleNode(path).InnerText;

                //重量列表
                path = "型号重量数据/重量列表";
                node = doc.SelectSingleNode(path);

                XmlNodeList nodelist = node.ChildNodes;
                if (nodelist.Count > 0)
                {
                    string strMainSystemWeight = strSortName + "|";
                    string strFH = "、";

                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        strMainSystemWeight += childNode.ChildNodes[1].InnerText + strFH
                                        + childNode.ChildNodes[0].InnerText + strFH + childNode.ChildNodes[2].InnerText + strFH
                                         + childNode.ChildNodes[3].InnerText + strFH + childNode.ChildNodes[5].InnerText + strFH + "|";
                    }

                    weightData.MainSystem_Name = strMainSystemWeight;
                }
            }
            catch
            {
                XLog.Write("导入文件\"" + strPath + "\"格式错误");
                MessageBox.Show("导入文件\"" + strPath + "\"格式错误");
                return null;
            }

            return weightData;
        }

        private TypeWeightData GetExcelTypeWeightData(string strFilePath)
        {
            TypeWeightData weightData = null;
            try
            {
                if (File.Exists(strFilePath))
                {
                    ExcelLib OpExcel = new ExcelLib();
                    //指定操作的文件
                    OpExcel.OpenFileName = strFilePath;
                    //打开文件
                    if (OpExcel.OpenExcelFile() == false)
                    {
                        return weightData;
                    }
                    //取得所有的工作表名称
                    string[] strSheetsName = OpExcel.getWorkSheetsName();

                    //默认操作第一张表
                    OpExcel.SetActiveWorkSheet(1);
                    System.Data.DataTable table;
                    table = OpExcel.getAllCellsValue();
                    OpExcel.CloseExcelApplication();

                    int count = table.Rows.Count;
                    if (count > 0)
                    {
                        weightData = new TypeWeightData();

                        weightData.Id = bllTypeWeight.GetMaxId() + 1;
                        weightData.Helicopter_Name = table.Rows[0][0].ToString();
                        weightData.Helicopter_Type = table.Rows[0][1].ToString();
                        weightData.Helicoter_Country = table.Rows[0][2].ToString();
                        weightData.MaxTaking_Weight = Convert.ToDouble(table.Rows[0][3]);
                        weightData.DesignTaking_Weight = Convert.ToDouble(table.Rows[0][4]);
                        weightData.EmptyWeight = Convert.ToDouble(table.Rows[0][5]);
                        weightData.Last_ModifyTime = table.Rows[0][6].ToString();

                        string strMainSystemWeight = string.Empty;
                        string strSortName = table.Rows[0][7].ToString();

                        if (strSortName != string.Empty)
                        {
                            strMainSystemWeight = strSortName + "|";
                            string strFH = "、";
                            for (int i = 0; i < count; i++)
                            {
                                strMainSystemWeight += table.Rows[i][9].ToString() + strFH
                                    + table.Rows[i][8].ToString() + strFH + table.Rows[i][10].ToString() + strFH
                                    + table.Rows[i][11].ToString() + strFH + table.Rows[i][13].ToString() + "|";
                            }
                        }

                        weightData.MainSystem_Name = strMainSystemWeight;
                    }
                }
            }
            catch
            {
                XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                return null;
            }
            return weightData;
        }

        private TypeWeightData GetTypeWeightData(string ID)
        {
            TypeWeightData weightData = null;
            if (lstTypeWeight != null && lstTypeWeight.Count > 0)
            {
                foreach (TypeWeightData data in lstTypeWeight)
                {
                    if (data.Id.ToString() == ID)
                    {
                        weightData = data.Clone();
                        return weightData;
                    }
                }
            }
            return weightData;
        }

        /// <summary>
        /// 通过名称获取重量分类
        /// </summary>
        /// <param name="strSortName"></param>
        /// <returns></returns>
        private WeightSortData GetFileWeightSortData(string strSortName)
        {
            WeightSortData sortData = null;
            if (lstWeightSortData != null && lstWeightSortData.Count > 0)
            {
                foreach (WeightSortData data in lstWeightSortData)
                {
                    if (strSortName == data.sortName)
                    {
                        sortData = data;
                        return sortData;
                    }
                }
            }
            return sortData;
        }

        /// <summary>
        /// 判断导入重量数据文件格式是否正确
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="path"></param>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private bool IsImortWeightDataFileFormat(string strType, string strPath)
        {
            bool IsRight = false;

            try
            {
                //重量数据
                if (strType == "weight")
                {
                    if (strPath.EndsWith(".xml"))
                    {
                        XmlNode node = null;
                        XmlDocument doc = new XmlDocument();
                        doc.Load(strPath);

                        node = doc.SelectSingleNode("重量分类/重量分类名称");
                        if (node != null)
                        {
                            IsRight = true;
                        }
                    }
                    if (strPath.EndsWith(".xls"))
                    {
                        ExcelLib OpExcel = new ExcelLib();
                        //指定操作的文件
                        OpExcel.OpenFileName = strPath;
                        //打开文件
                        if (OpExcel.OpenExcelFile() == false)
                        {
                            return false;
                        }
                        //取得所有的工作表名称
                        string[] strSheetsName = OpExcel.getWorkSheetsName();

                        //默认操作第一张表
                        OpExcel.SetActiveWorkSheet(1);
                        System.Data.DataTable table;
                        table = OpExcel.getAllCellsValue();
                        OpExcel.CloseExcelApplication();

                        if (table.Columns[0].Caption == "重量分类名称")
                        {
                            IsRight = true;
                        }
                    }
                }

                ///重量设计
                if (strType == "type")
                {
                    if (strPath.EndsWith(".xml"))
                    {
                        XmlNode node = null;
                        XmlDocument doc = new XmlDocument();
                        doc.Load(strPath);

                        node = doc.SelectSingleNode("型号重量数据/直升机名称");
                        if (node != null)
                        {
                            IsRight = true;
                        }
                    }
                    if (strPath.EndsWith(".xls"))
                    {
                        ExcelLib OpExcel = new ExcelLib();
                        //指定操作的文件
                        OpExcel.OpenFileName = strPath;
                        //打开文件
                        if (OpExcel.OpenExcelFile() == false)
                        {
                            return false;
                        }
                        //取得所有的工作表名称
                        string[] strSheetsName = OpExcel.getWorkSheetsName();

                        //默认操作第一张表
                        OpExcel.SetActiveWorkSheet(1);
                        System.Data.DataTable table;
                        table = OpExcel.getAllCellsValue();
                        OpExcel.CloseExcelApplication();

                        if (table.Columns[0].Caption == "直升机名称")
                        {
                            IsRight = true;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return IsRight;
        }

        #region 从基础库获取

        private List<TypeWeightData> GetListTypeWeightData(string strFileName)
        {
            List<TypeWeightData> lstWeightData = new List<TypeWeightData>();

            if (!File.Exists(strFileName))
            {
                return lstWeightData;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(strFileName);

            XmlNodeList lstNodeDB = doc.GetElementsByTagName("inclass");

            string dbId = string.Empty;
            if (lstNodeDB != null && lstNodeDB.Count > 0)
            {
                dbId = lstNodeDB[0].Attributes["id"].Value;
                lstNode1 = doc.GetElementsByTagName("node1");
                lstNode2 = doc.GetElementsByTagName("node2");
                lstNode3 = doc.GetElementsByTagName("node3");
                lstModel = doc.GetElementsByTagName("model");
            }

            foreach (XmlNode typeNode in lstNode1)
            {
                TypeWeightData data = new TypeWeightData();

                string strID = string.Empty;
                int count = 0;
                //型号ID
                if (typeNode.Attributes["pid"].Value == dbId)
                {
                    strID = typeNode.Attributes["id"].Value;
                    data.Id = bllTypeWeight.GetMaxId() + count;
                    count++;

                    string strSortName = string.Empty;
                    foreach (XmlNode node in lstNode2)
                    {
                        //基本信息
                        if (node.Attributes["pid"].Value == strID && node.Attributes["name"].Value == "基本信息")
                        {
                            string strBasicId = node.Attributes["id"].Value;
                            foreach (XmlNode childNode in lstModel)
                            {
                                if (childNode.Attributes["pid"].Value == strBasicId && childNode.Attributes["name"].Value == "直升机类型")
                                {
                                    data.Helicopter_Type = childNode.Attributes["value"].Value;
                                }
                                if (childNode.Attributes["pid"].Value == strBasicId && childNode.Attributes["name"].Value == "直升机国籍")
                                {
                                    data.Helicoter_Country = childNode.Attributes["value"].Value;
                                }
                                if (childNode.Attributes["pid"].Value == strBasicId && childNode.Attributes["name"].Value == "直升机空机重量")
                                {
                                    data.EmptyWeight = childNode.Attributes["value"].Value == null ? 0 : Convert.ToDouble(childNode.Attributes["value"].Value);
                                }
                                if (childNode.Attributes["pid"].Value == strBasicId && childNode.Attributes["name"].Value == "直升机名称")
                                {
                                    data.Helicopter_Name = childNode.Attributes["value"].Value;
                                }
                                if (childNode.Attributes["pid"].Value == strBasicId && childNode.Attributes["name"].Value == "直升机最大起飞重量")
                                {
                                    data.MaxTaking_Weight = childNode.Attributes["value"].Value == null ? 0 : Convert.ToDouble(childNode.Attributes["value"].Value);
                                }
                                if (childNode.Attributes["pid"].Value == strBasicId && childNode.Attributes["name"].Value == "直升机设计起飞重量")
                                {
                                    data.DesignTaking_Weight = childNode.Attributes["value"].Value == null ? 0 : Convert.ToDouble(childNode.Attributes["value"].Value);
                                }
                                if (childNode.Attributes["pid"].Value == strBasicId && childNode.Attributes["name"].Value == "最后修改时间")
                                {
                                    data.Last_ModifyTime = childNode.Attributes["value"].Value;
                                }
                                if (childNode.Attributes["pid"].Value == strBasicId && childNode.Attributes["name"].Value == "重量分类名称")
                                {
                                   strSortName = childNode.Attributes["value"].Value;
                                }
                            }
                        }

                        //重量数据
                        if (node.Attributes["pid"].Value == strID && node.Attributes["name"].Value == "空机重量")
                        {
                            WeightSortData sortData = new WeightSortData();
                            sortData.lstWeightData = new List<WeightData>();
                            //重量分类名称
                            sortData.sortName = strSortName;

                            foreach (XmlNode node3 in lstNode3)
                            {
                                if (node3.Attributes["pid"].Value == node.Attributes["id"].Value)
                                {
                                    foreach (XmlNode childNode in lstModel)
                                    {
                                        if (childNode.Attributes["pid"].Value == node3.Attributes["id"].Value)
                                        {
                                            WeightData weightData = new WeightData();
                                            weightData.nID = int.Parse(childNode.Attributes["xlID"].Value);
                                            weightData.nParentID = int.Parse(childNode.Attributes["zlparentId"].Value);
                                            weightData.weightName = childNode.Attributes["zlName"].Value;
                                            weightData.weightValue = childNode.Attributes["zlsz"].Value == null ? 0 : Convert.ToDouble(childNode.Attributes["zlsz"].Value);
                                            weightData.strRemark = childNode.Attributes["zldesc"].Value;

                                            sortData.lstWeightData.Add(weightData);
                                            break;
                                        }
                                    }
                                }
                            }
                            foreach (XmlNode childNode in lstModel)
                            {
                                //空机重量
                                if (childNode.Attributes["pid"].Value == node.Attributes["id"].Value && childNode.Attributes["xlID"].Value == "0")
                                {
                                    WeightData weightData = new WeightData();
                                    weightData.nID = int.Parse(childNode.Attributes["xlID"].Value);
                                    weightData.nParentID = int.Parse(childNode.Attributes["zlparentId"].Value);
                                    weightData.weightName = childNode.Attributes["zlName"].Value;
                                    weightData.weightValue = childNode.Attributes["zlsz"].Value == null ? 0 : Convert.ToDouble(childNode.Attributes["zlsz"].Value);
                                    weightData.strRemark = childNode.Attributes["zldesc"].Value;

                                    sortData.lstWeightData.Add(weightData);
                                    break;
                                }
                            }
                            sortData.lstWeightData = sortData.lstWeightData.OrderBy(s => s.nID).ToList();
                            data.MainSystem_Name = GetMainSystemWeight(sortData);
                        }
                    }
                }
                lstWeightData.Add(data);
            }

            return lstWeightData;
        }

        /// <summary>
        /// 从基础库获取数据
        /// </summary>
        public void BasicDBGetData(string strTemplatePath)
        {
            try
            {
                bool IsOpen = PubSyswareCom.OpenTemplate(strTemplatePath);

                object obj = null;
                if (IsOpen==false)
                {
                    MessageBox.Show("打开模板失败");
                    return;
                }
                PubSyswareCom.AutoExecuteTemplate(strTemplatePath);
                PubSyswareCom.mGetParameter(strTemplatePath, "weightdata", ref obj);
                PubSyswareCom.CloseTemplate(strTemplatePath);

                //读取参数
                List<string> lstContent = new List<string>();
                if (obj != null)
                {
                    lstContent.Add(obj.ToString());
                }

                XLog.Write("从基础数据库同步数据进行中");
               
                string strFolder = System.IO.Directory.GetCurrentDirectory() + "\\BasicData";
                if (!Directory.Exists(strFolder))
                {
                    Directory.CreateDirectory(strFolder);
                }
                string strFileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                string strFilePath = strFolder + "\\" + strFileName + ".xml";
                CommonFunction.mWriteListStringToFileBasic(strFilePath, lstContent);

                #region 同步数据

                try
                {
                    this.Enabled = false;

                    List<TypeWeightData> lstWeightData = GetListTypeWeightData(strFilePath);
                    //DB-Data
                    List<TypeWeightData> lstDBWeightData = bllTypeWeight.GetListModel();

                    List<TypeWeightData> lstSameWeightData = new List<TypeWeightData>();

                    //找到名称相同的型号重量数据
                    if (lstDBWeightData != null && lstDBWeightData.Count > 0 && lstWeightData != null && lstWeightData.Count > 0)
                    {
                        foreach (TypeWeightData weight in lstWeightData)
                        {
                            foreach (TypeWeightData data in lstDBWeightData)
                            {
                                if (weight.Helicopter_Name == data.Helicopter_Name)
                                {
                                    weight.Id = data.Id;
                                    lstSameWeightData.Add(weight);
                                    break;
                                }
                            }
                        }
                    }
                    //找到不同名称的型号重量数据
                    if (lstSameWeightData != null && lstSameWeightData.Count > 0 && lstWeightData != null && lstWeightData.Count > 0)
                    {
                        foreach (TypeWeightData data in lstSameWeightData)
                        {
                            lstWeightData.Remove(data);
                        }
                    }

                    if (lstDBWeightData != null && lstDBWeightData.Count > 0)
                    {
                        //有同名的型号重量数据，更新，不同的添加
                        if (lstSameWeightData != null && lstSameWeightData.Count > 0)
                        {
                            DialogResult result = MessageBox.Show("是否替换同名的型号重量数据?", "替换提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            if (result == DialogResult.Yes)
                            {
                                //更新添加
                                foreach (TypeWeightData data in lstSameWeightData)
                                {
                                    bllTypeWeight.Update(data);
                                }
                            }
                            //添加
                            if (lstWeightData != null && lstWeightData.Count > 0)
                            {
                                foreach (TypeWeightData weightData in lstWeightData)
                                {
                                    bllTypeWeight.Add(weightData);
                                }

                            }
                            BindWeightData();
                            MessageBox.Show("从基础数据库同步数据成功");
                        }
                        else
                        {
                            //添加
                            if (lstWeightData != null && lstWeightData.Count > 0)
                            {
                                foreach (TypeWeightData weightData in lstWeightData)
                                {
                                    bllTypeWeight.Add(weightData);
                                }
                            }
                            BindWeightData();
                            MessageBox.Show("从基础数据库同步数据成功");
                        }

                    }
                    else
                    {
                        if (lstWeightData != null && lstWeightData.Count > 0)
                        {
                            foreach (TypeWeightData weightData in lstWeightData)
                            {
                                bllTypeWeight.Add(weightData);
                            }
                            BindWeightData();
                            MessageBox.Show("从基础数据库同步数据成功");
                        }
                    }

                    this.Enabled = true;
                    XLog.Write("从基础数据库同步数据成功");

                }
                catch (Exception ex)
                {
                    this.Enabled = true;
                    XLog.Write(ex.Message);
                }
                #endregion
            }
            catch
            {
                MessageBox.Show("从基础库读取数据失败");
                return;
            }
        }

        #endregion

        #endregion

        #region 事件处理

        /// <summary>
        /// 确认修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirmModify_Click(object sender, EventArgs e)
        {
            //页面验证信息
            string strErroInfo = PageVerificationInfo();
            if (strErroInfo != string.Empty)
            {
                XLog.Write(strErroInfo);
                return;
            }

            GetPageTypeWeightData();

            if (strOperType == CommonMessage.operNew || strOperType == CommonMessage.operJYNew)
            {
                bool IsAdd = bllTypeWeight.Add(typeWeightData);

                if (IsAdd)
                {
                    if (strOperType == CommonMessage.operJYNew)
                    {
                        XLog.Write("基于新建成功");
                    }
                    else
                    {
                        XLog.Write("新建成功");
                    }
                }
            }
            if (strOperType == CommonMessage.operEdit)
            {
                bool IsEdit = bllTypeWeight.Update(typeWeightData);

                if (IsEdit)
                {
                    XLog.Write("修改成功");
                }
            }

            strOperType = CommonMessage.operConfirm;
            SetPageButton();
            BindWeightData();
            SetOperTitle(strOperType);

            foreach (TreeNode node in treeViewTypeList.Nodes[0].Nodes)
            {
                if (node.Name == typeWeightData.Id.ToString())
                {
                    treeViewTypeList.SelectedNode = node;
                    selNode = node;
                }
            }

            typeWeightData = null;
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            typeWeightData = new TypeWeightData();

            strOperType = CommonMessage.operNew;
            SetPageButton();
            IntiControl();
            SetOperTitle(strOperType);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selNode == null || selNode.Level == 0)
            {
                XLog.Write("请选择型号重量数据");
                return;
            }

            strOperType = CommonMessage.operEdit;
            SetPageButton();
            SetOperTitle(strOperType);

            typeWeightData = GetTypeWeightData(selNode.Name);
        }

        /// <summary>
        /// 基于新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJYNew_Click(object sender, EventArgs e)
        {
            if (selNode == null || selNode.Level == 0)
            {
                XLog.Write("请选择型号重量数据");
                return;
            }
            strOperType = CommonMessage.operJYNew;
            SetPageButton();
            SetOperTitle(strOperType);
            typeWeightData = typeWeightData = GetTypeWeightData(selNode.Name);
        }

        /// <summary>
        /// 单击节点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewTypeList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            selNode = e.Node;
            treeViewTypeList.SelectedNode = e.Node;

            typeWeightData = new TypeWeightData();
            SettingPageData();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selNode == null || selNode.Level == 0)
            {
                XLog.Write("请选择型号重量数据");
                return;
            }

            DialogResult result = MessageBox.Show("是否删除" + selNode.Text + "?", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                bool IsDelete = bllTypeWeight.Delete(Convert.ToInt32(selNode.Name));

                if (IsDelete)
                {
                    XLog.Write("删除成功");

                    selNode = null;
                    strOperType = CommonMessage.operDelete;

                    SetPageButton();
                    BindWeightData();
                    IntiControl();
                }
            }

        }

        private void treeViewTypeList_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
        }

        /// <summary>
        /// 取消修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirmCancle_Click(object sender, EventArgs e)
        {
            strOperType = CommonMessage.operCancle;
            SetPageButton();
            SetOperTitle(strOperType);

            SettingPageData();
            typeWeightData = null;
        }

        /// <summary>
        /// 刷新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindWeightData();
            selNode = null;
        }

        /// <summary>
        /// 选择重量分类事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWeightSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (strOperType == CommonMessage.operNew || strOperType == CommonMessage.operJYNew || strOperType == CommonMessage.operEdit)
            {
                if (CommonMessage.IsWeightDataImport == false)
                {
                    WeightSortData sortData = null;

                    if (WeightDesignDataForm.clsStringToWeightSortData(typeWeightData.MainSystem_Name) != null)
                    {
                        DialogResult result = MessageBox.Show("是否清空当前重量数据?", "清空提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (result == DialogResult.Yes)
                        {
                            sortData = GetFileWeightSortData(cmbWeightSort.Text);
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        sortData = GetFileWeightSortData(cmbWeightSort.Text);
                    }

                    //绑定重量分类
                    typeWeightData.MainSystem_Name = GetMainSystemWeight(sortData);
                    MainForm.BindWeightDesignSort(sortData, treeViewWeightSort);

                    //绑定gridview
                    MainForm.SetWeightDesignGridView(sortData, gridWeightData);
                    //清空图形
                    chartTypeWeightData.Titles.Clear();
                    chartTypeWeightData.Series.Clear();
                    chartTypeWeightData.ChartAreas.Clear();
                    chartTypeWeightData.Legends.Clear();
                }
            }
        }

        /// <summary>
        /// 确定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 导入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "xml文件 (*.xml)|*.xml|Excle文件 (*.xls)|*.xls|All File (*.*)|*.*";
                fileDialog.RestoreDirectory = true;
                fileDialog.FilterIndex = 1;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string strFilePath = fileDialog.FileName;

                    //if (IsImortWeightDataFileFormat("type", strFilePath) == false)
                    //{
                    //    XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                    //    MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                    //    return;
                    //}

                    //获取型号重量数据
                    TypeWeightData weightData = null;

                    if (strFilePath.EndsWith(".xls"))
                    {
                        weightData = GetExcelTypeWeightData(strFilePath);
                    }
                    else if (strFilePath.EndsWith(".xml"))
                    {
                        weightData = GetXmlWeightTypeData(strFilePath);
                    }
                    else
                    {
                        XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                        MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                        return;
                    }

                    if (weightData == null)
                    {
                        return;
                    }

                    if (bllTypeWeight.Add(weightData))
                    {
                        //刷新列表
                        BindWeightData();

                        foreach (TreeNode node in treeViewTypeList.Nodes[0].Nodes)
                        {
                            if (node.Name == weightData.Id.ToString())
                            {
                                treeViewTypeList.SelectedNode = node;
                                selNode = node;
                            }
                        }

                        SettingPageData();

                        XLog.Write("导入文件\"" + strFilePath + "\"成功");
                    }
                }
            }
            catch
            {
                XLog.Write("导入型号重量数据失败");
            }
        }

        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (selNode == null || selNode.Level == 0)
            {
                XLog.Write("请选择型号重量数据");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml文件 (*.xml)|*.xml|Excle文件 (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = selNode.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dlg.FileName;
                TypeWeightData weightData = GetTypeWeightData(selNode.Name);

                if (strFilePath.EndsWith(".xml"))
                {
                    List<string> lstContent = GetListContent(weightData);
                    CommonFunction.mWriteListStringToFile(strFilePath, lstContent);
                    XLog.Write("导出文件\"" + strFilePath + "\"成功");
                }
                if (strFilePath.EndsWith(".xls"))
                {
                    if (lstTypeWeight.Count > 0)
                    {
                        WeightSortData sortData = WeightDesignDataForm.clsStringToWeightSortData(weightData.MainSystem_Name);
                        DataTable table = GetTableExcleData(weightData, sortData);
                        int result = XCommon.FileStatus.FileIsOpen(strFilePath);

                        if (result == 1)
                        {
                            MessageBox.Show("请关闭文件\"" + strFilePath + "\"再导出");

                        }
                        else
                        {
                            CommonExcel commonExcel = new CommonExcel();
                            commonExcel.lstColumn = GetExcleCloumn();
                            commonExcel.CreateExcel("example", strFilePath, "sheet1", table);
                            XLog.Write("导出文件\"" + strFilePath + "\"成功");
                        }
                    }
                }

            }
        }

        private void gridWeightData_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (Verification.IsDoubleNumer(e.FormattedValue.ToString()) == false)
            {
                MessageBox.Show("非法数字");
                e.Cancel = true;
            }
        }

        private void gridWeightData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //绑定重量分类
            WeightSortData sortData = WeightDesignDataForm.GetCurrentWeightSortData(typeWeightData.MainSystem_Name, gridWeightData);
            typeWeightData.MainSystem_Name = GetMainSystemWeight(sortData);
            MainForm.BindWeightDesignSort(sortData, treeViewWeightSort);

            //绑定图形
            List<WeightData> lstTempWeightData = MainForm.GetPicListWeightData(sortData, gridWeightData);
            MainForm.DisplayPiePic(chartTypeWeightData, lstTempWeightData, "重量分布");
            if (lstTempWeightData != null && lstTempWeightData.Count > 0)
            {
                chartTypeWeightData.ContextMenuStrip = contextMenuStripWeightImage;
            }
            else
            {
                chartTypeWeightData.ContextMenuStrip = null;
            }
        }

        /// <summary>
        /// 重量数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportWeightData_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "xml文件 (*.xml)|*.xml|Excle文件 (*.xls)|*.xls|All File (*.*)|*.*";
                fileDialog.RestoreDirectory = true;
                fileDialog.FilterIndex = 1;

                //获取重量分类
                WeightSortData sortData = null;
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string strFilePath = fileDialog.FileName;
                    //if (IsImortWeightDataFileFormat("weight", strFilePath) == false)
                    //{
                    //    XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                    //    MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                    //    return;
                    //}

                    if (strFilePath.EndsWith(".xls"))
                    {
                        sortData = WeightDesignDataForm.GetXlsImportSortData(strFilePath);
                    }
                    else if (strFilePath.EndsWith(".xml"))
                    {
                        sortData = WeightDesignDataForm.GetXmlImporSortData(strFilePath);
                    }
                    else
                    {
                        XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                        MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                        return;
                    }

                    if (sortData == null)
                    {
                        return;
                    }
                    if (sortData != null && sortData.lstWeightData.Count == 0)
                    {
                        XLog.Write("导入文件无重量分类数据");
                        MessageBox.Show("导入文件无重量分类数据");
                        return;
                    }

                    CommonMessage.IsWeightDataImport = true;

                    //判断导入的重量分布是否一致
                    WeightSortData weightSortData = WeightDesignDataForm.GetCurrentWeightSortData(GetMainSystemWeight(WeightDesignDataForm.clsStringToWeightSortData(typeWeightData.MainSystem_Name)), gridWeightData);

                    if (weightSortData != null)
                    {
                        bool IsSame = WeightSortData.blIsSame(sortData, weightSortData);

                        if (IsSame == false)
                        {
                            DialogResult result = MessageBox.Show("导入的重量分类与现有的重量分类不同?", "继续提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            if (result == DialogResult.Yes)
                            {
                                //绑定重量分类
                                WeightSortData tempSortData = GetSameWeightSort(sortData);
                                if (tempSortData == null)
                                {
                                    lstWeightSortData.Add(GetZeroWeight(sortData));
                                    BindWeightSortData(lstWeightSortData);
                                    cmbWeightSort.Text = sortData.sortName;
                                }
                                else
                                {
                                    cmbWeightSort.Text = tempSortData.sortName;
                                }
                            }
                            else
                            {
                                CommonMessage.IsWeightDataImport = false;
                                return;
                            }
                        }
                        else
                        {
                            cmbWeightSort.Text = sortData.sortName;
                        }
                    }
                    else
                    {
                        WeightSortData tempSortData = GetSameWeightSort(sortData);
                        if (tempSortData == null)
                        {
                            lstWeightSortData.Add(GetZeroWeight(sortData));
                            BindWeightSortData(lstWeightSortData);

                            cmbWeightSort.Text = Convert.ToString(sortData.sortName);
                        }
                        else
                        {
                            cmbWeightSort.Text = tempSortData.sortName;
                        }
                    }

                    MainForm.BindWeightDesignGridView(sortData, gridWeightData);
                    MainForm.BindWeightDesignSort(sortData, treeViewWeightSort);
                    List<WeightData> lstTempWeightData = MainForm.GetPicListWeightData(sortData, gridWeightData);
                    MainForm.DisplayPiePic(chartTypeWeightData, lstTempWeightData, "重量分布");
                    if (lstTempWeightData != null && lstTempWeightData.Count > 0)
                    {
                        chartTypeWeightData.ContextMenuStrip = contextMenuStripWeightImage;
                    }
                    else
                    {
                        chartTypeWeightData.ContextMenuStrip = null;
                    }
                }
                typeWeightData.MainSystem_Name = GetMainSystemWeight(sortData);

                XLog.Write("导入重量数据成功");
                CommonMessage.IsWeightDataImport = false;
            }
            catch (Exception ex)
            {
                XLog.Write("导入重量数据失败."+ex.Message);
            }
        }

        /// <summary>
        ///  重量数据导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportWeightData_Click(object sender, EventArgs e)
        {
            WeightSortData tempWeightSort = new WeightSortData();
            if (strOperType == CommonMessage.operNew || strOperType == CommonMessage.operJYNew || strOperType == CommonMessage.operEdit)
            {
                tempWeightSort = WeightDesignDataForm.clsStringToWeightSortData(typeWeightData.MainSystem_Name);
                if (tempWeightSort == null)
                {
                    XLog.Write("没有数据不能导出");
                    return;
                }
            }
            else
            {
                if (selNode == null || selNode.Level == 0)
                {
                    XLog.Write("请选择重量设计数据");
                    return;
                }
                TypeWeightData tempTypeWeightData = GetTypeWeightData(selNode.Name);
                tempWeightSort = WeightDesignDataForm.clsStringToWeightSortData(tempTypeWeightData.MainSystem_Name);
            }

            if (tempWeightSort == null)
            {
                XLog.Write("没有数据不能导出");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml文件 (*.xml)|*.xml|Excel文件 (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = txtName.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dlg.FileName;

                if (strFilePath.EndsWith(".xml"))
                {
                    List<string> lstContent = MainForm.GetDesignResultFlieContent(tempWeightSort);
                    CommonFunction.mWriteListStringToFile(strFilePath, lstContent);
                }

                if (strFilePath.EndsWith(".xls"))
                {
                    DataTable table = MainForm.GetDesignResultTable(tempWeightSort);
                    int result = XCommon.FileStatus.FileIsOpen(strFilePath);

                    if (result == 1)
                    {
                        MessageBox.Show("请关闭文件" + strFilePath + ",再导出");
                    }
                    else
                    {
                        CommonExcel commonExcel = new CommonExcel();
                        commonExcel.lstColumn = MainForm.GetDesignResultExcleCloumn();
                        commonExcel.CreateExcel("example", strFilePath, "sheet1", table);
                    }
                }

                XLog.Write("导出文件\"" + strFilePath + "\"成功");
            }
        }

        /// <summary>
        /// 从基础库获取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFromBasicToDB_Click(object sender, EventArgs e)
        {
            try
            {
                TemplateSetForm form = new TemplateSetForm(this);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                XLog.Write(ex.Message);
            }
        }

        #endregion

        private void chartTypeWeightData_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult result = chartTypeWeightData.HitTest(e.X, e.Y);
            if (result.PointIndex < 0)
            {
                return;
            }

            bool exploded = false;

            if (chartTypeWeightData.Series.Count > 0)
            {
                if (chartTypeWeightData.Series[0].Points[result.PointIndex].CustomProperties == "Exploded=true")
                {
                    exploded = true;
                }
                else
                {
                    exploded = false;
                }

                foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint in chartTypeWeightData.Series[0].Points)
                {
                    tpoint.CustomProperties = "";
                    if (exploded)
                    {
                        return;
                    }
                }

                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint = chartTypeWeightData.Series[0].Points[result.PointIndex];
                    tpoint.CustomProperties = "Exploded = true";

                }
                if (result.ChartElementType == ChartElementType.LegendItem)
                {
                    System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint = chartTypeWeightData.Series[0].Points[result.PointIndex];
                    tpoint.CustomProperties = "Exploded = true";
                }
            }
        }

        private void chartTypeWeightData_MouseMove(object sender, MouseEventArgs e)
        {
            HitTestResult result = chartTypeWeightData.HitTest(e.X, e.Y);

            if (chartTypeWeightData.Series.Count > 0)
            {
                foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint in chartTypeWeightData.Series[0].Points)
                {
                    tpoint.BackSecondaryColor = Color.Black;
                    tpoint.BackHatchStyle = ChartHatchStyle.None;
                    tpoint.BorderWidth = 1;
                }

                if (result.ChartElementType == ChartElementType.DataPoint || result.ChartElementType == ChartElementType.LegendItem)
                {
                    chartTypeWeightData.Cursor = Cursors.Hand;
                    System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint = chartTypeWeightData.Series[0].Points[result.PointIndex];

                    tpoint.BackSecondaryColor = Color.White;

                    tpoint.BackHatchStyle = ChartHatchStyle.Percent25;

                    tpoint.BorderWidth = 2;
                }
                else
                {
                    chartTypeWeightData.Cursor = Cursors.Default;
                }
            }
        }

        private void toolStripMenuItemWeightImage_Click(object sender, EventArgs e)
        {
            MainForm.SavePictureToFile(chartTypeWeightData);
        }

        private void treeViewTypeList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selNode = e.Node;

            SettingPageData();
        }
    }
}
