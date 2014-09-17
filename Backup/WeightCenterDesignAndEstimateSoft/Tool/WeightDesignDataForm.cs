using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL;
using XCommon;
using Model;
using ZedGraph;
using ZaeroModelSystem;
using System.IO;
using System.Xml;
using System.Windows.Forms.DataVisualization.Charting;

namespace WeightCenterDesignAndEstimateSoft.Tool
{
    public partial class WeightDesignDataForm : Form
    {
        #region 属性

        private string strOperType = string.Empty;
        private TreeNode selNode = null;

        private BLLWeightDesignData bllDesignData = new BLLWeightDesignData();
        private List<WeightDesignData> lstDesignData = null;
        private List<WeightSortData> lstWeightSortData = null;

        private WeightDesignData weightDesinData = null;

        public WeightDesignDataForm()
        {
            InitializeComponent();

            SetPageButton();
            BindWeightSortData(WeightSortManageForm.GetListWeightSortData());
            BindWeightData();
        }

        #endregion

        #region 自定义方法

        /// <summary>
        /// 绑定重量数据，从数据文件中读取
        /// </summary>
        private void BindWeightData()
        {
            treeViewList.Nodes.Clear();

            //重量设计数据
            lstDesignData = bllDesignData.GetListModel();

            if (lstDesignData.Count > 0)
            {
                TreeNode node = new TreeNode("重量设计数据");
                treeViewList.Nodes.Add(node);

                foreach (WeightDesignData data in lstDesignData)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Name = data.Id.ToString();
                    childNode.Text = data.DesignData_Name;

                    node.Nodes.Add(childNode);
                }
                node.Expand();
            }
        }

        /// <summary>
        /// 获取设计重量数据实体
        /// </summary>
        /// <returns></returns>
        private void GetPageWeightDeisignData()
        {
            if (strOperType == CommonMessage.operEdit)
            {
                weightDesinData.Id = Convert.ToInt32(selNode.Name);
            }
            else
            {
                weightDesinData.Id = bllDesignData.GetMaxId();
            }

            weightDesinData.DesignData_Name = txtDesignDataName.Text;
            weightDesinData.DesignData_Submitter = txtSubmitter.Text;
            weightDesinData.Helicopter_Name = txtHelicopterName.Text;
            weightDesinData.DataRemark = txtRemark.Text;
            weightDesinData.LastModify_Time = DateTime.Now.ToString();
            weightDesinData.DesignTaking_Weight = txtDesignTakingWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtDesignTakingWeight.Text);
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
        /// 设置页面控件
        /// </summary>
        private void SetPageButton()
        {
            if (strOperType == CommonMessage.operNone || strOperType == CommonMessage.operConfirm || strOperType == CommonMessage.operCancle)
            {
                txtDesignDataName.Enabled = false;
                txtHelicopterName.Enabled = false;
                txtSubmitter.Enabled = false;
                txtRemark.Enabled = false;
                cmbWeightSort.Enabled = false;
                txtDesignTakingWeight.Enabled = false;

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

                treeViewList.Enabled = true;
            }
            if (strOperType == CommonMessage.operNew || strOperType == CommonMessage.operJYNew || strOperType == CommonMessage.operEdit)
            {
                txtDesignDataName.Enabled = true;
                txtHelicopterName.Enabled = true;
                txtSubmitter.Enabled = true;
                txtRemark.Enabled = true;
                cmbWeightSort.Enabled = true;

                txtDesignTakingWeight.Enabled = true;

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

                treeViewList.Enabled = false;
            }
            txtLastModifyTime.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// 初始化空件
        /// </summary>
        private void IntiControl()
        {
            txtDesignDataName.Text = string.Empty;
            txtHelicopterName.Text = string.Empty;
            txtSubmitter.Text = string.Empty;
            txtRemark.Text = string.Empty;
            cmbWeightSort.Text = string.Empty;
            txtDesignTakingWeight.Text = string.Empty;
            txtLastModifyTime.Text = string.Empty;


            treeViewWeightSort.Nodes.Clear();

            gridWeightData.Rows.Clear();
            gridWeightData.Columns.Clear();

            //清除原来的图形

            chartWeightDesign.Titles.Clear();
            chartWeightDesign.Series.Clear();
            chartWeightDesign.ChartAreas.Clear();
            chartWeightDesign.Legends.Clear();
            chartWeightDesign.ContextMenuStrip = null;
        }

        /// <summary>
        /// 页面验证
        /// </summary>
        /// <returns></returns>
        private string PageVerificationInfo()
        {
            string strErroInfo = string.Empty;

            if (txtDesignDataName.Text == string.Empty)
            {
                strErroInfo = "请输入设计数据名称";
                return strErroInfo;
            }
            else
            {
                if (Verification.IsCheckString(txtDesignDataName.Text))
                {
                    strErroInfo = "设计数据名称不能输入非法字符";
                    return strErroInfo;
                }
            }
            if (txtHelicopterName.Text == string.Empty)
            {
                strErroInfo = "请输入直升机名称";
                return strErroInfo;
            }
            else
            {
                if (Verification.IsCheckSignleString(txtHelicopterName.Text))
                {
                    strErroInfo = "直升机名称不能输入非法字符";
                    return strErroInfo;
                }
            }

            if (txtSubmitter.Text == string.Empty)
            {
                strErroInfo = "请输入设计数据提交者";
                return strErroInfo;
            }
            else
            {
                if (Verification.IsCheckString(txtSubmitter.Text))
                {
                    strErroInfo = "设计数据提交者不能输入非法字符";
                    return strErroInfo;
                }
            }

            if (txtDesignTakingWeight.Text != string.Empty)
            {
                if (Verification.IsDoubleNumer(txtDesignTakingWeight.Text) == false)
                {
                    strErroInfo = "设计起飞重量应为数字";
                    return strErroInfo;
                }
            }

            if (txtRemark.Text != string.Empty)
            {
                if (Verification.IsCheckRemarkString(txtRemark.Text))
                {
                    strErroInfo = "备注不能输入非法字符";
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
            // 判断选择节点
            if (selNode == null || selNode.Level == 0)
            {
                IntiControl();
                return;
            }

            if (lstDesignData.Count > 0)
            {
                // 获取重量设计数据
                WeightDesignData desinData = GetWeightDesignData(Convert.ToInt32(selNode.Name));

                WeightSortData sortData = null;

                if (desinData != null)
                {
                    txtDesignDataName.Text = desinData.DesignData_Name;
                    txtHelicopterName.Text = desinData.Helicopter_Name;
                    txtSubmitter.Text = desinData.DesignData_Submitter;
                    txtRemark.Text = desinData.DataRemark;
                    txtDesignTakingWeight.Text = desinData.DesignTaking_Weight.ToString();
                    txtLastModifyTime.Text = desinData.LastModify_Time;

                    sortData = clsStringToWeightSortData(desinData.MainSystem_Name);
                    if (sortData != null)
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
                        MainForm.DisplayPiePic(chartWeightDesign, lstWeightData, "重量分布");
                        if (lstWeightData != null && lstWeightData.Count > 0)
                        {
                            chartWeightDesign.ContextMenuStrip = contextMenuStripWeighImage;
                        }
                        else
                        {
                            chartWeightDesign.ContextMenuStrip = null;
                        }
                    }
                    else
                    {
                        // 清空当前的重量数据树、下拉框与图形菜单
                        // 清空下拉菜单
                        cmbWeightSort.Text = string.Empty;
                        // 清空重量分类树
                        treeViewWeightSort.Nodes.Clear();
                        // 清空重量数据表格
                        gridWeightData.Rows.Clear();
                        gridWeightData.Columns.Clear();
                        //清空图形
                        chartWeightDesign.Titles.Clear();
                        chartWeightDesign.Series.Clear();
                        chartWeightDesign.ChartAreas.Clear();
                        chartWeightDesign.Legends.Clear();
                        chartWeightDesign.ContextMenuStrip = null;

                        return;
                    }
                }
                desinData = null;
            }
        }

        /// <summary>
        /// 获取重量结果内容
        /// </summary>
        /// <returns></returns>
        private List<string> GetListContent()
        {
            List<string> lstContent = new List<string>();
            WeightDesignData weightData = GetWeightDesignData(Convert.ToInt32(selNode.Name));

            if (weightData != null)
            {
                WeightSortData sortData = clsStringToWeightSortData(weightData.MainSystem_Name);
                string strSortName = sortData == null ? string.Empty : sortData.sortName;

                lstContent.Insert(0, "<?xml version=" + "\"1.0\"" + " encoding=" + "\"gb2312\"" + " ?> ");

                lstContent.Add("<重量设计数据>");

                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<设计数据名称>" + weightData.DesignData_Name + "</设计数据名称>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<设计数据提交者>" + weightData.DesignData_Submitter + "</设计数据提交者>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<直升机名称>" + weightData.Helicopter_Name + "</直升机名称>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<设计起飞重量>" + weightData.DesignTaking_Weight.ToString() + "</设计起飞重量>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<数据备注>" + weightData.DataRemark + "</数据备注>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<最后修改日期>" + weightData.LastModify_Time + "</最后修改日期>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<重量分类名称>" + strSortName + "</重量分类名称>");

                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<重量列表>");

                //重量数据
                List<string> lstWeightContent = MainForm.GetDesignWeightDataContent(3, sortData);
                foreach (string strContent in lstWeightContent)
                {
                    lstContent.Add(strContent);
                }
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "</重量列表>");

                lstContent.Add("</重量设计数据>");
            }
            return lstContent;
        }

        private List<string> GetExcleCloumn()
        {
            List<string> lstTitle = new List<string>();

            lstTitle.Add("设计数据名称");
            lstTitle.Add("设计数据提交者");
            lstTitle.Add("直升机名称");
            lstTitle.Add("数据备注");
            lstTitle.Add("最后修改时间");
            lstTitle.Add("直升机设计起飞重量");
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
        /// 获取表结构
        /// </summary>
        /// <returns></returns>
        private DataTable GetTableExcleStruct()
        {
            DataTable table = new DataTable();

            //设计数据名称
            table.Columns.Add("DesignData_Name");
            //设计数据提交者
            table.Columns.Add("DesignData_Submitter");
            //直升机名称
            table.Columns.Add("Helicopter_Name");

            //数据备注
            table.Columns.Add("DataRemark");

            //最后修改时间
            table.Columns.Add("LastModify_Time");

            table.Columns.Add("DesignTaking_Weight");

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
        private DataTable GetTableExcleData(WeightDesignData designData, WeightSortData sortData)
        {
            DataTable table = GetTableExcleStruct();

            DataRow drFirst = table.NewRow();

            drFirst["DesignData_Name"] = designData.DesignData_Name;
            drFirst["DesignData_Submitter"] = designData.DesignData_Submitter;
            drFirst["Helicopter_Name"] = designData.Helicopter_Name;
            drFirst["DataRemark"] = designData.DataRemark;
            drFirst["LastModify_Time"] = designData.LastModify_Time;
            drFirst["DesignTaking_Weight"] = designData.DesignTaking_Weight;
            table.Rows.Add(drFirst);

            DataRow dr = null;
            if (sortData != null && sortData.lstWeightData.Count > 0)
            {
                foreach (WeightData data in sortData.lstWeightData)
                {
                    if (data == sortData.lstWeightData.First())
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

                        dr["DesignData_Name"] = string.Empty;
                        dr["DesignData_Submitter"] = string.Empty;
                        dr["Helicopter_Name"] = string.Empty;
                        dr["DataRemark"] = string.Empty;
                        dr["LastModify_Time"] = string.Empty;
                        dr["DesignTaking_Weight"] = string.Empty;
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
        /// 获取Excel文件的重量设计对象
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        private WeightDesignData GetExcelWeightDesignData(string strFilePath)
        {
            WeightDesignData weightData = null;
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
                        string strSortName = string.Empty;

                        weightData = new WeightDesignData();
                        weightData.Id = bllDesignData.GetMaxId() + 1;
                        weightData.DesignData_Name = table.Rows[0][0].ToString();
                        weightData.DesignData_Submitter = table.Rows[0][1].ToString();
                        weightData.Helicopter_Name = table.Rows[0][2].ToString();
                        weightData.DataRemark = table.Rows[0][3].ToString();
                        weightData.LastModify_Time = table.Rows[0][4].ToString();
                        weightData.DesignTaking_Weight = Convert.ToDouble(table.Rows[0][5]);

                        if (table.Rows[0][6] is DBNull || table.Rows[0][6].ToString() == string.Empty)
                        {
                            weightData.MainSystem_Name = string.Empty;
                        }
                        else
                        {
                            strSortName = table.Rows[0][6].ToString();
                            string strMainSystemWeight = strSortName + "|";
                            string strFH = "、";

                            for (int i = 0; i < count; i++)
                            {
                                strMainSystemWeight += table.Rows[i][8].ToString() + strFH
                                    + table.Rows[i][7].ToString() + strFH + table.Rows[i][9].ToString() + strFH
                                      + table.Rows[i][10].ToString() + strFH + table.Rows[i][12].ToString() + "|";
                            }
                            weightData.MainSystem_Name = strMainSystemWeight;
                        }
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

        /// <summary>
        /// 获取重量设计数据
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private WeightDesignData GetXmlWeightDesignData(string strPath)
        {
            WeightDesignData weightData = null;
            try
            {

                if (!File.Exists(strPath))
                {
                    return weightData;
                }

                weightData = new WeightDesignData();
                weightData.Id = bllDesignData.GetMaxId();

                string path = string.Empty;
                XmlNode node = null;

                XmlDocument doc = new XmlDocument();
                doc.Load(strPath);

                //设计数据名称
                path = "重量设计数据/设计数据名称";
                node = doc.SelectSingleNode(path);
                weightData.DesignData_Name = node.InnerText;

                //设计数据提交者
                path = "重量设计数据/设计数据提交者";
                node = doc.SelectSingleNode(path);
                weightData.DesignData_Submitter = node.InnerText;

                //直升机名称
                path = "重量设计数据/直升机名称";
                node = doc.SelectSingleNode(path);
                weightData.Helicopter_Name = node.InnerText;


                //数据备注
                path = "重量设计数据/数据备注";
                node = doc.SelectSingleNode(path);
                weightData.DataRemark = node.InnerText;

                //最后修改日期
                path = "重量设计数据/最后修改日期";
                node = doc.SelectSingleNode(path);
                weightData.LastModify_Time = node.InnerText;


                //设计起飞重量
                path = "重量设计数据/设计起飞重量";
                node = doc.SelectSingleNode(path);
                weightData.DesignTaking_Weight = Convert.ToDouble(node.InnerText);

                //重量分类名称
                path = "重量设计数据/重量分类名称";
                string strSortName = doc.SelectSingleNode(path).InnerText;

                //重量列表
                path = "重量设计数据/重量列表";
                node = doc.SelectSingleNode(path);

                string strMainSystemWeight = string.Empty;
                string strFH = "、";

                XmlNodeList nodelist = node.ChildNodes;
                if (nodelist.Count > 0)
                {
                    strMainSystemWeight = strSortName + "|";
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        strMainSystemWeight += childNode.ChildNodes[1].InnerText + strFH
                           + childNode.ChildNodes[0].InnerText + strFH + childNode.ChildNodes[2].InnerText + strFH
                            + childNode.ChildNodes[3].InnerText + strFH + childNode.ChildNodes[5].InnerText + strFH + "|";
                    }
                }

                weightData.MainSystem_Name = strMainSystemWeight;
            }
            catch
            {
                XLog.Write("导入文件\"" + strPath + "\"格式错误");
                MessageBox.Show("导入文件\"" + strPath + "\"格式错误");
            }

            return weightData;
        }

        /// <summary>
        /// 获取重量设计对象
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private WeightDesignData GetWeightDesignData(int ID)
        {
            WeightDesignData designData = null;
            foreach (WeightDesignData data in lstDesignData)
            {
                if (ID == data.Id)
                {
                    designData = data.Clone();
                }
            }
            return designData;
        }

        public static List<WeightData> GetAllListWeightData(WeightSortData sortData, DataGridView gridWeightData)
        {
            List<WeightData> lstWeightData = new List<WeightData>();
            double dValue = MainForm.GetDesignResultCount(sortData, gridWeightData);
            dValue = Math.Round(dValue, 6);

            for (int i = 0; i < sortData.lstWeightData.Count; i++)
            {
                if (sortData.lstWeightData[i].nParentID == -1)
                {
                    sortData.lstWeightData[i].weightValue = dValue;
                    lstWeightData.Add(sortData.lstWeightData[i]);
                }

                for (int j = 0; j < gridWeightData.ColumnCount; j++)
                {
                    if (sortData.lstWeightData[i].weightName == gridWeightData.Columns[j].Name)
                    {
                        dValue = gridWeightData.Rows[0].Cells[j].Value is System.DBNull ? 0 : Convert.ToDouble(gridWeightData.Rows[0].Cells[j].Value);
                        sortData.lstWeightData[i].weightValue = dValue;
                        lstWeightData.Add(sortData.lstWeightData[i]);
                    }
                }
            }
            return lstWeightData;
        }

        /// <summary>
        /// 设置操作标题
        /// </summary>
        /// <param name="strType"></param>
        private void SetOperTitle(string strType)
        {
            if (strType == CommonMessage.operNew)
            {
                gruWeightData.Text = "新建重量设计数据";
            }
            if (strType == CommonMessage.operJYNew)
            {
                gruWeightData.Text = "基于新建重量设计数据";
            }
            if (strType == CommonMessage.operEdit)
            {
                gruWeightData.Text = "编辑重量设计数据";
            }
            if (strType == string.Empty)
            {
                gruWeightData.Text = "重量设计数据";
            }
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
                if (strType == "design")
                {
                    if (strPath.EndsWith(".xml"))
                    {
                        XmlNode node = null;
                        XmlDocument doc = new XmlDocument();
                        doc.Load(strPath);

                        node = doc.SelectSingleNode("重量设计数据/设计数据名称");
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

                        if (table.Columns[0].Caption == "设计数据名称")
                        {
                            IsRight = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return IsRight;
        }

        /// <summary>
        /// 获取Xml文件的WeightSortData
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static WeightSortData GetXmlImporSortData(string strPath)
        {
            WeightSortData sortData = null;
            try
            {
                if (!File.Exists(strPath))
                {
                    return sortData;
                }
                sortData = new WeightSortData();

                string path = string.Empty;
                XmlNode node = null;

                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(strPath);
                }
                catch (Exception ex)
                {
                    XLog.Write("打开文件\"" + strPath + "\"错误");
                    return sortData;
                }

                node = doc.SelectSingleNode("重量分类/重量分类名称");
                if (node == null)
                {
                    XLog.Write("导入文件\"" + strPath + "\"格式错误");
                    return sortData;
                }


                //重量分类名称
                path = "重量分类/重量分类名称";
                node = doc.SelectSingleNode(path);
                sortData.sortName = (node == null ? string.Empty : node.InnerText);

                //重量分类备注
                path = "重量分类/重量分类备注";
                node = doc.SelectSingleNode(path);
                sortData.strRemark = (node == null ? string.Empty : node.InnerText);

                //重量列表
                path = "重量分类/重量数据列表";
                node = doc.SelectSingleNode(path);

                XmlNodeList nodelist = (node == null ? null : node.ChildNodes);

                if (nodelist != null && nodelist.Count > 0)
                {
                    List<WeightData> lstWeightData = new List<WeightData>();
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        WeightData data = new WeightData();

                        data.nID = Convert.ToInt32(childNode.ChildNodes[0].InnerText);
                        data.weightName = childNode.ChildNodes[1].InnerText;
                        data.weightValue = Convert.ToDouble(childNode.ChildNodes[3].InnerText);
                        data.strRemark = childNode.ChildNodes[4].InnerText;
                        data.nParentID = Convert.ToInt32(childNode.ChildNodes[5].InnerText);

                        lstWeightData.Add(data);
                    }
                    sortData.lstWeightData = lstWeightData;
                }
            }
            catch
            {
                XLog.Write("导入文件\"" + strPath + "\"格式错误");
                MessageBox.Show("导入文件\"" + strPath + "\"格式错误");
            }
            return sortData;
        }

        /// <summary>
        ///获取xls文件 WeightSortData
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        public static WeightSortData GetXlsImportSortData(string strFilePath)
        {
            WeightSortData sortData = null;
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
                        return sortData;
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
                        sortData = new WeightSortData();
                        sortData.sortName = table.Rows[0][0].ToString();

                        List<WeightData> lstWeightData = new List<WeightData>();
                        for (int i = 0; i < count; i++)
                        {
                            WeightData data = new WeightData();

                            data.nID = Convert.ToInt32(table.Rows[i][1].ToString());
                            data.weightName = table.Rows[i][2].ToString();
                            data.weightValue = Convert.ToDouble(table.Rows[i][4].ToString());
                            data.strRemark = table.Rows[i][5].ToString();
                            data.nParentID = Convert.ToInt32(table.Rows[i][6].ToString());

                            lstWeightData.Add(data);
                        }
                        sortData.lstWeightData = lstWeightData;
                    }
                }
            }
            catch
            {
                XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");

                return null;
            }
            return sortData;
        }

        /// <summary>
        /// WeightSortData
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static WeightSortData clsStringToWeightSortData(string str)
        {
            // 创建临时重量分类数据对象
            WeightSortData tempWeightSortData = null;

            // 创建重量数据列表对象
            List<WeightData> lstWeightData = null;
            //判断是否为空字符串
            if (str == null || str == string.Empty)
            {
                return null;
            }
            // 新重量分类数据
            tempWeightSortData = new WeightSortData();
            lstWeightData = new List<WeightData>();
            string[] tempStrings = str.Split('|');
            foreach (string tempString in tempStrings)
            {
                if (tempString == tempStrings[0])
                {
                    tempWeightSortData.sortName = tempString;
                }
                else
                {
                    // 获取重量数据
                    string[] tempInnerStrings = tempString.Split('、');
                    if (tempInnerStrings.Length > 1)
                    {
                        // 创建重量数据对象
                        WeightData tempWeightData = new WeightData();

                        tempWeightData.weightName = tempInnerStrings[0];
                        tempWeightData.nID = Convert.ToInt32(tempInnerStrings[1]);
                        tempWeightData.weightValue = Convert.ToDouble(tempInnerStrings[3]);
                        tempWeightData.nParentID = Convert.ToInt32(tempInnerStrings[4]);

                        // 添加重量数据至重量数据列表
                        lstWeightData.Add(tempWeightData);
                    }
                }
            }
            // 添加重量数据列表至重量分类
            tempWeightSortData.lstWeightData = lstWeightData;
            return tempWeightSortData;
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
        /// 获取当前重量分类
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static WeightSortData GetCurrentWeightSortData(string str, DataGridView gridWeightData)
        {
            WeightSortData sortData = clsStringToWeightSortData(str);

            if (sortData != null)
            {
                foreach (WeightData data in sortData.lstWeightData)
                {
                    for (int i = 0; i < gridWeightData.ColumnCount; i++)
                    {
                        if (data.nID.ToString() == gridWeightData.Columns[i].Name)
                        {
                            data.weightValue = Convert.ToDouble(gridWeightData.Rows[0].Cells[i].Value);
                        }
                    }
                }

                foreach (WeightData data in sortData.lstWeightData)
                {
                    IEnumerable<WeightData> selection = from wd in sortData.lstWeightData where wd.nParentID == data.nID select wd;

                    GetSortDataTotal(data, sortData);
                }
            }

            return sortData;
        }

        /// <summary>
        /// 求重量分类的和
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sortData"></param>
        /// <returns></returns>
        public static void GetSortDataTotal(WeightData data, WeightSortData sortData)
        {
            IEnumerable<WeightData> selection = from wd in sortData.lstWeightData where wd.nParentID == data.nID select wd;

            if (selection.Count() > 0)
            {
                foreach (WeightData weight in selection)
                {
                    GetSortDataTotal(weight, sortData);
                }
            }
            else
            {
                IEnumerable<WeightData> parentSelection = from wd in sortData.lstWeightData where wd.nID == data.nParentID select wd;
                IEnumerable<WeightData> childSelection = from wd in sortData.lstWeightData where wd.nParentID == data.nParentID select wd;

                double childValue = 0;
                foreach (WeightData weight in childSelection)
                {
                    childValue += weight.weightValue;
                }

                if (parentSelection.Count() > 0)
                {
                    foreach (WeightData weight in sortData.lstWeightData)
                    {
                        if (weight.nID == parentSelection.ToList()[0].nID)
                        {
                            weight.weightValue = childValue;
                        }
                    }
                }
            }
        }

        private bool IsSameWeightSort(WeightSortData sortData)
        {
            bool IsSame = false;

            foreach (WeightSortData data in lstWeightSortData)
            {
                if (WeightSortData.blIsSame(sortData, data))
                {
                    IsSame = true;
                    return IsSame;
                }
            }
            return IsSame;
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

            GetPageWeightDeisignData();

            if (strOperType == CommonMessage.operNew || strOperType == CommonMessage.operJYNew)
            {
                bool IsAdd = bllDesignData.Add(weightDesinData);

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
                bool IsEdit = bllDesignData.Update(weightDesinData);

                if (IsEdit)
                {
                    XLog.Write("修改成功");
                }
            }

            strOperType = CommonMessage.operConfirm;
            SetPageButton();
            BindWeightData();
            SetOperTitle(string.Empty);

            foreach (TreeNode node in treeViewList.Nodes[0].Nodes)
            {
                if (node.Name == weightDesinData.Id.ToString())
                {
                    treeViewList.SelectedNode = node;
                    selNode = node;
                }
            }

            weightDesinData = null;
        }

        /// <summary>
        /// 重量分类选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWeightSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = cmbWeightSort.Text;
            if (strOperType == CommonMessage.operNew || strOperType == CommonMessage.operJYNew || strOperType == CommonMessage.operEdit)
            {
                if (CommonMessage.IsWeightDataImport == false)
                {
                    WeightSortData sortData = null;

                    if (clsStringToWeightSortData(weightDesinData.MainSystem_Name) != null)
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

                    weightDesinData.MainSystem_Name = WeightDataMangeForm.GetMainSystemWeight(sortData);
                    MainForm.BindWeightDesignSort(sortData, treeViewWeightSort);

                    //设置GridView
                    MainForm.SetWeightDesignGridView(sortData, gridWeightData);

                    //清空图形
                    chartWeightDesign.Titles.Clear();
                    chartWeightDesign.Series.Clear();
                    chartWeightDesign.ChartAreas.Clear();
                    chartWeightDesign.Legends.Clear();
                }
            }
        }

        /// <summary>
        /// 新建事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            weightDesinData = new WeightDesignData();

            strOperType = CommonMessage.operNew;
            SetPageButton();
            IntiControl();
            SetOperTitle(strOperType);
        }

        private void treeViewList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            selNode = e.Node;
            treeViewList.SelectedNode = e.Node;

            weightDesinData = new WeightDesignData();
            //SettingPageData();
        }

        private void treeViewList_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
        }

        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selNode == null || selNode.Level == 0)
            {
                XLog.Write("请选择重量设计数据");
                return;
            }
            strOperType = CommonMessage.operEdit;
            SetPageButton();
            SetOperTitle(strOperType);

            weightDesinData = GetWeightDesignData(Convert.ToInt32(selNode.Name));
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
                XLog.Write("请选择重量设计数据");
                return;
            }
            strOperType = CommonMessage.operJYNew;
            SetPageButton();
            SetOperTitle(strOperType);

            weightDesinData = GetWeightDesignData(Convert.ToInt32(selNode.Name));
        }

        /// <summary>
        /// 确认事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selNode == null || selNode.Level == 0)
            {
                XLog.Write("请选择重量设计数据");
                return;
            }

            DialogResult result = MessageBox.Show("是否删除" + selNode.Text + "?", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                bool IsDelete = bllDesignData.Delete(Convert.ToInt32(selNode.Name));

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

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindWeightData();
            selNode = null;
        }

        /// <summary>
        /// 导入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "xml文件 (*.xml)|*.xml|Excle文件 (*.xls)|*.xls|All File (*.*)|*.*";
            fileDialog.RestoreDirectory = true;
            fileDialog.FilterIndex = 1;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = fileDialog.FileName;

                //if (IsImortWeightDataFileFormat("design", strFilePath) == false)
                //{
                //    XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                //    MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                //    return;
                //}

                //获取重量设计数据
                WeightDesignData weightData = null;

                if (strFilePath.EndsWith(".xls"))
                {
                    weightData = GetExcelWeightDesignData(strFilePath);
                }
                else if (strFilePath.EndsWith(".xml"))
                {
                    weightData = GetXmlWeightDesignData(strFilePath);
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


                if (bllDesignData.Add(weightData))
                {
                    //刷新列表
                    BindWeightData();

                    foreach (TreeNode node in treeViewList.Nodes[0].Nodes)
                    {
                        if (node.Name == weightData.Id.ToString())
                        {
                            treeViewList.SelectedNode = node;
                            selNode = node;
                        }
                    }

                    SettingPageData();

                    XLog.Write("导入文件\"" + strFilePath + "\"成功");
                }
                else
                {
                    XLog.Write("导入文件\"" + strFilePath + "\"失败");
                }
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
                XLog.Write("请选择重量设计数据");
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

                if (strFilePath.EndsWith(".xml"))
                {
                    List<string> lstContent = GetListContent();
                    CommonFunction.mWriteListStringToFile(strFilePath, lstContent);
                    XLog.Write("导出文件\"" + strFilePath + "\"成功");
                }
                if (strFilePath.EndsWith(".xls"))
                {
                    if (lstDesignData.Count > 0)
                    {
                        WeightDesignData weightData = GetWeightDesignData(Convert.ToInt32(selNode.Name));

                        WeightSortData sortData = clsStringToWeightSortData(weightData.MainSystem_Name);
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

        private void gridWeightData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //绑定重量分类,获取页面编辑后的重量分类数据（包含重量数据）
            WeightSortData sortData = GetCurrentWeightSortData(weightDesinData.MainSystem_Name, gridWeightData);
            //写入当前的weightDesinData中
            weightDesinData.MainSystem_Name = WeightDataMangeForm.GetMainSystemWeight(sortData);
            MainForm.BindWeightDesignSort(sortData, treeViewWeightSort);

            //绑定饼图
            List<WeightData> lstTempWeightData = MainForm.GetPicListWeightData(sortData, gridWeightData);
            MainForm.DisplayPiePic(chartWeightDesign, lstTempWeightData, "重量分布");
            if (lstTempWeightData != null && lstTempWeightData.Count > 0)
            {
                chartWeightDesign.ContextMenuStrip = contextMenuStripWeighImage;
            }
            else
            {
                chartWeightDesign.ContextMenuStrip = null;
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

        private void btnConfirmCancle_Click(object sender, EventArgs e)
        {
            strOperType = CommonMessage.operCancle;
            SetPageButton();
            SetOperTitle(string.Empty);

            SettingPageData();
            weightDesinData = null;
        }

        /// <summary>
        /// 导入重量数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportWeightData_Click(object sender, EventArgs e)
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

                //bool IsRight = IsImortWeightDataFileFormat("weight", strFilePath);
                //if (IsRight == false)
                //{
                //    XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                //    MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                //    return;
                //}

                if (strFilePath.EndsWith(".xls"))
                {
                    sortData = GetXlsImportSortData(strFilePath);
                }
                else if (strFilePath.EndsWith(".xml"))
                {
                    sortData = GetXmlImporSortData(strFilePath);
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
                WeightSortData weightSortData = GetCurrentWeightSortData(WeightDataMangeForm.GetMainSystemWeight(clsStringToWeightSortData(weightDesinData.MainSystem_Name)), gridWeightData);

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
                        cmbWeightSort.Text = weightSortData.sortName;
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
                MainForm.DisplayPiePic(chartWeightDesign, lstTempWeightData, "重量分布");
                if (lstTempWeightData != null && lstTempWeightData.Count > 0)
                {
                    chartWeightDesign.ContextMenuStrip = contextMenuStripWeighImage;
                }
                else
                {
                    chartWeightDesign.ContextMenuStrip = null;
                }
            }
            weightDesinData.MainSystem_Name = WeightDataMangeForm.GetMainSystemWeight(sortData);

            XLog.Write("导入重量数据成功");
            CommonMessage.IsWeightDataImport = false;
        }

        /// <summary>
        /// 导出重量数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportWeightData_Click(object sender, EventArgs e)
        {
            WeightSortData tempWeightSort = new WeightSortData();
            if (strOperType == CommonMessage.operNew || strOperType == CommonMessage.operJYNew || strOperType == CommonMessage.operEdit)
            {
                tempWeightSort = clsStringToWeightSortData(weightDesinData.MainSystem_Name);
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
                WeightDesignData tempWeightDesinData = GetWeightDesignData(Convert.ToInt32(selNode.Name));
                tempWeightSort = clsStringToWeightSortData(tempWeightDesinData.MainSystem_Name);
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
            dlg.FileName = txtDesignDataName.Text;

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

        #endregion

        private void chartWeightDesign_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult result = chartWeightDesign.HitTest(e.X, e.Y);
            if (result.PointIndex < 0)
            {
                return;
            }

            bool exploded = false;

            if (chartWeightDesign.Series.Count > 0)
            {
                if (chartWeightDesign.Series[0].Points[result.PointIndex].CustomProperties == "Exploded=true")
                {
                    exploded = true;
                }
                else
                {
                    exploded = false;
                }

                foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint in chartWeightDesign.Series[0].Points)
                {
                    tpoint.CustomProperties = "";
                    if (exploded)
                    {
                        return;
                    }
                }

                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint = chartWeightDesign.Series[0].Points[result.PointIndex];
                    tpoint.CustomProperties = "Exploded = true";

                }
                if (result.ChartElementType == ChartElementType.LegendItem)
                {
                    System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint = chartWeightDesign.Series[0].Points[result.PointIndex];
                    tpoint.CustomProperties = "Exploded = true";
                }
            }
        }

        private void chartWeightDesign_MouseMove(object sender, MouseEventArgs e)
        {
            HitTestResult result = chartWeightDesign.HitTest(e.X, e.Y);

            if (chartWeightDesign.Series.Count > 0)
            {
                foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint in chartWeightDesign.Series[0].Points)
                {
                    tpoint.BackSecondaryColor = Color.Black;
                    tpoint.BackHatchStyle = ChartHatchStyle.None;
                    tpoint.BorderWidth = 1;
                }

                if (result.ChartElementType == ChartElementType.DataPoint || result.ChartElementType == ChartElementType.LegendItem)
                {
                    chartWeightDesign.Cursor = Cursors.Hand;
                    System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint = chartWeightDesign.Series[0].Points[result.PointIndex];

                    tpoint.BackSecondaryColor = Color.White;

                    tpoint.BackHatchStyle = ChartHatchStyle.Percent25;

                    tpoint.BorderWidth = 2;
                }
                else
                {
                    chartWeightDesign.Cursor = Cursors.Default;
                }
            }
        }

        private void toolStripMenuItemWeighImage_Click(object sender, EventArgs e)
        {
            MainForm.SavePictureToFile(chartWeightDesign);
        }

        private void treeViewList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selNode = e.Node;

            SettingPageData();
        }
    }
}
