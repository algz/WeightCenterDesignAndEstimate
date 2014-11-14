using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using XCommon;
using System.IO;
using System.Xml;
using ZaeroModelSystem;
using Dev.PubLib;

namespace WeightCenterDesignAndEstimateSoft.Tool
{
    public partial class ParameterManageForm : Form
    {
        #region 属性

        private ExcelLib m_OpExcel = new ExcelLib();

        private List<ParaData> lstParaData = null;
        private List<ParaData> lstAllParaData = null;

        private string strType = string.Empty;
        private TreeNode selNode = null;

        private string strParaPath = System.AppDomain.CurrentDomain.BaseDirectory + "ParameterCollection";

        public ParameterManageForm()
        {
            InitializeComponent();

            CommonFunction.BindParaTypeData(cmbParameterType);
            CommonFunction.BindParaTypeData(cmbFilterSel);
            cmbFilterSel.Items.Add("无");

            SetingPageButton("inti");

            string strFilePath = strParaPath + "\\" + "ParameterCollection.PMC";
            lstAllParaData = GetParaData(strFilePath);
            BindTreeParaData(lstAllParaData);
        }

        #endregion

        #region 自定义方法

        private void BindTreeParaData(List<ParaData> lstPara)
        {
            lstParaData = new List<ParaData>();

            if (lstPara != null && lstPara.Count > 0)
            {
                foreach (ParaData data in lstPara)
                {
                    lstParaData.Add(data.Clone());
                }
            }

            lstParaData = lstParaData.OrderBy(l => l.paraType).ToList();

            treeViewParameterList.Nodes.Clear();

            if (lstParaData != null && lstParaData.Count > 0)
            {
                TreeNode preNode = new TreeNode();

                TreeNode node = new TreeNode("参数列表");
                treeViewParameterList.Nodes.Add(node);

                foreach (ParaData para in lstParaData)
                {
                    TreeNode parentNode = new TreeNode();
                    parentNode.Name = para.paraType.ToString();
                    parentNode.Text = CommonFunction.GetParaType(para.paraType);

                    TreeNode childNode = new TreeNode();
                    childNode.Name = para.paraName;
                    childNode.Text = para.paraName;
                    childNode.ToolTipText = para.paraName + "|" + para.paraUnit + "|" + para.paraType + "|" + para.strRemark;

                    if (parentNode.Name != preNode.Name)
                    {
                        node.Nodes.Add(parentNode);
                        parentNode.Nodes.Add(childNode);

                        preNode = parentNode;
                    }
                    else
                    {
                        preNode.Nodes.Add(childNode);
                    }
                    parentNode.Expand();

                }
                node.Expand();
            }
        }

        private void SetingPageButton(string strType)
        {
            if (strType == "inti")
            {
                txtParameterName.Enabled = false;
                txtUnit.Enabled = false;
                cmbParameterType.Enabled = false;
                txtParameterRemark.Enabled = false;

                btnParameterConfirm.Enabled = false;
                btnCancle.Enabled = false;
            }
            if (strType == "new")
            {
                txtParameterName.Enabled = true;
                txtUnit.Enabled = true;
                cmbParameterType.Enabled = true;
                txtParameterRemark.Enabled = true;

                btnParameterConfirm.Enabled = true;
                btnCancle.Enabled = true;

                btnNew.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnExport.Enabled = false;
                btnImport.Enabled = false;
                btnJYNew.Enabled = false;
                btnAllExport.Enabled = false;
                btnAllImort.Enabled = false;

                txtSelect.Enabled = false;
                btnSelect.Enabled = false;
                cmbFilterSel.Enabled = false;

                txtParameterName.Text = string.Empty;
                txtUnit.Text = string.Empty;
                cmbParameterType.SelectedIndex = -1;
                txtParameterRemark.Text = string.Empty;
            }
            if (strType == "edit" || strType == "JYNew")
            {
                txtParameterName.Enabled = true;

                txtUnit.Enabled = true;
                cmbParameterType.Enabled = true;
                txtParameterRemark.Enabled = true;

                btnParameterConfirm.Enabled = true;
                btnCancle.Enabled = true;

                btnNew.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnExport.Enabled = false;
                btnImport.Enabled = false;
                btnJYNew.Enabled = false;
                btnAllExport.Enabled = false;
                btnAllImort.Enabled = false;

                txtSelect.Enabled = false;
                btnSelect.Enabled = false;
                cmbFilterSel.Enabled = false;
            }
            if (strType == "confirm" || strType == "cancle" || strType == "delete" || strType == "select")
            {
                txtParameterName.Enabled = false;
                txtUnit.Enabled = false;
                cmbParameterType.Enabled = false;
                txtParameterRemark.Enabled = false;

                btnParameterConfirm.Enabled = false;
                btnCancle.Enabled = false;

                btnNew.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnExport.Enabled = true;
                btnImport.Enabled = true;
                btnJYNew.Enabled = true;
                btnAllExport.Enabled = true;
                btnAllImort.Enabled = true;

                txtSelect.Enabled = true;
                btnSelect.Enabled = true;
                cmbFilterSel.Enabled = true;
                if (strType == "delete" || strType == "select")
                {
                    txtParameterName.Text = string.Empty;
                    txtUnit.Text = string.Empty;
                    cmbParameterType.SelectedIndex = -1;
                    txtParameterRemark.Text = string.Empty;
                }
            }
        }

        private void SettingTitle(string strType)
        {
            if (strType == "inti" || strType == "confirm")
            {
                this.Text = "参数管理对话框";
            }
            if (strType == "new")
            {
                this.Text = "参数新建对话框";
            }
            if (strType == "JYNew")
            {
                this.Text = "参数基于新建对话框";
            }
            if (strType == "edit")
            {
                this.Text = "参数编辑对话框";
            }

        }

        /// <summary>
        /// 获取页面数据
        /// </summary>
        /// <returns></returns>
        private ParaData GetPageData()
        {
            ParaData paraData = new ParaData();

            paraData.paraName = txtParameterName.Text.Trim();
            paraData.paraUnit = txtUnit.Text;
            paraData.paraType = int.Parse(cmbParameterType.SelectedIndex.ToString());
            paraData.strRemark = txtParameterRemark.Text;

            return paraData;
        }

        /// <summary>
        /// 获取页面验证信息
        /// </summary>
        /// <returns></returns>
        private string GetPageVerification()
        {
            string strErroInfo = string.Empty;

            //参数名称
            if (txtParameterName.Text == string.Empty)
            {
                strErroInfo = "请输入参数名称";
                return strErroInfo;
            }
            else
            {
                if (txtParameterName.Text.Contains(" "))
                {
                    strErroInfo = "参数名称不能包含空格";
                    return strErroInfo;
                }
                if (Verification.IsCheckString(txtParameterName.Text))
                {
                    strErroInfo = "请检查参数名称输入非法字符";
                    return strErroInfo;
                }

            }

            //单位
            if (txtUnit.Text == string.Empty)
            {
                strErroInfo = "请输入参数单位";
                return strErroInfo;
            }
            else
            {
                if (Verification.IsCheckString(txtUnit.Text))
                {
                    strErroInfo = "请检查参数单位输入非法字符";
                    return strErroInfo;
                }
            }

            if (cmbParameterType.Text == string.Empty)
            {
                strErroInfo = "请选择参数类型";
                return strErroInfo;
            }

            if (txtParameterRemark.Text != string.Empty)
            {
                if (Verification.IsCheckRemarkString(txtParameterRemark.Text))
                {
                    strErroInfo = "参数备注含有非法字符";
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
            if (selNode != null)
            {
                string[] strArray = selNode.ToolTipText.Split('|');
                if (strArray != null && strArray.Length == 4)
                {
                    txtParameterName.Text = strArray[0];
                    txtUnit.Text = strArray[1];
                    cmbParameterType.SelectedIndex = int.Parse(strArray[2]);
                    txtParameterRemark.Text = strArray[3];
                }

            }
        }

        private List<string> GetFileContent(List<ParaData> lstParaData)
        {
            List<string> lstContent = new List<string>();

            if (lstParaData != null && lstParaData.Count > 0)
            {
                lstContent.Insert(0, "<?xml version=" + "\"1.0\"" + " encoding=" + "\"gb2312\"" + " ?> ");

                lstContent.Add("<PMC>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<参数列表>");

                for (int i = 0; i < lstParaData.Count; i++)
                {
                    lstContent.Add(CommonFunction.mStrModifyToString8(2) + "<参数>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<参数名称>" + lstParaData[i].paraName + "</参数名称>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<参数单位>" + lstParaData[i].paraUnit + "</参数单位>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<参数类型>" + lstParaData[i].paraType.ToString() + "</参数类型>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<参数数值>" + lstParaData[i].paraValue.ToString() + "</参数数值>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<参数备注>" + lstParaData[i].strRemark + "</参数备注>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(2) + "</参数>");
                }

                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "</参数列表>");
                lstContent.Add("</PMC>");
            }

            return lstContent;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <param name="sortData"></param>
        private void SaveFile(string strFilePath, List<ParaData> lstParaData)
        {
            List<string> lstContent = GetFileContent(lstParaData);
            if (lstContent.Count == 0)
            {
                if (File.Exists(strFilePath))
                {
                    File.Delete(strFilePath);
                }
            }
            else
            {
                XCommon.CommonFunction.mWriteListStringToFile(strFilePath, false, lstContent);
            }
        }

        private List<ParaData> GetParaData(string strPath)
        {
            List<ParaData> lstTempData = new List<ParaData>();

            if (!File.Exists(strPath))
            {
                return lstTempData;
            }

            string path = string.Empty;
            XmlNode node = null;

            XmlDocument doc = new XmlDocument();
            doc.Load(strPath);

            //工程名称
            path = "PMC/参数列表";
            node = doc.SelectSingleNode(path);


            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                ParaData para = new ParaData();

                para.paraName = node.ChildNodes[i].ChildNodes[0].InnerText;
                para.paraUnit = node.ChildNodes[i].ChildNodes[1].InnerText;
                para.paraType = int.Parse(node.ChildNodes[i].ChildNodes[2].InnerText);
                para.paraValue = Convert.ToDouble(node.ChildNodes[i].ChildNodes[3].InnerText);
                para.strRemark = node.ChildNodes[i].ChildNodes[4].InnerText;

                lstTempData.Add(para);
            }
            return lstTempData;
        }

        private DataTable GetTableStuct()
        {
            DataTable table = new DataTable();

            table.Columns.Add("ParaName");
            table.Columns.Add("ParaUnit");
            table.Columns.Add("ParaType");
            table.Columns.Add("ParaValue");
            table.Columns.Add("ParaRemark");

            return table;
        }

        /// <summary>
        /// 获取表数据
        /// </summary>
        /// <param name="lstTempParaDagta"></param>
        /// <returns></returns>
        private DataTable GetTableData(List<ParaData> lstTempParaDagta)
        {
            DataTable table = GetTableStuct();

            if (lstTempParaDagta != null && lstTempParaDagta.Count > 0)
            {
                foreach (ParaData para in lstTempParaDagta)
                {
                    DataRow dr = table.NewRow();
                    dr["ParaName"] = para.paraName;
                    dr["ParaUnit"] = para.paraUnit;
                    dr["ParaType"] = para.paraType;
                    dr["ParaValue"] = para.paraValue;
                    dr["ParaRemark"] = para.strRemark;

                    table.Rows.Add(dr);
                }
            }

            return table;
        }

        private List<string> GetColumnName()
        {
            List<string> lstColumnName = new List<string>();

            lstColumnName.Add("参数名称");
            lstColumnName.Add("参数单位");
            lstColumnName.Add("参数类型");
            lstColumnName.Add("参数数值");
            lstColumnName.Add("参数备注");

            return lstColumnName;
        }

        /// <summary>
        /// 获取excle文件数据
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        private List<ParaData> GetFileParaData(string strFilePath)
        {
            List<ParaData> lstParaData = new List<ParaData>();

            if (File.Exists(strFilePath))
            {
                ExcelLib OpExcel = new ExcelLib();
                //指定操作的文件
                OpExcel.OpenFileName = strFilePath;
                //打开文件
                if (OpExcel.OpenExcelFile() == false)
                {
                    return lstParaData;
                }
                //取得所有的工作表名称
                string[] strSheetsName = OpExcel.getWorkSheetsName();

                //默认操作第一张表
                OpExcel.SetActiveWorkSheet(1);
                System.Data.DataTable table;
                table = OpExcel.getAllCellsValue();
                OpExcel.CloseExcelApplication();

                int count = table.Rows.Count;

                for (int i = 0; i < count; i++)
                {
                    ParaData para = new ParaData();

                    para.paraName = table.Rows[i][0].ToString();
                    para.paraUnit = table.Rows[i][1].ToString();
                    para.paraType = Convert.ToInt32(table.Rows[i][2].ToString());
                    para.paraValue = Convert.ToDouble(table.Rows[i][3].ToString());
                    para.strRemark = table.Rows[i][4].ToString();

                    lstParaData.Add(para);
                }
            }

            return lstParaData;
        }

        /// <summary>
        /// 获取查询参数List
        /// </summary>
        /// <returns></returns>
        private List<ParaData> GetSelParaData()
        {
            List<ParaData> lstTempData = new List<ParaData>();

            if ((cmbFilterSel.Text == string.Empty || cmbFilterSel.Text == "无") && txtSelect.Text == string.Empty)
            {
                lstTempData = lstAllParaData;
            }
            if ((cmbFilterSel.Text != string.Empty && txtSelect.Text == string.Empty)
                || (cmbFilterSel.Text != "无" && txtSelect.Text == string.Empty))
            {
                foreach (ParaData para in lstAllParaData)
                {
                    if (para.paraType == cmbFilterSel.SelectedIndex)
                    {
                        lstTempData.Add(para);
                    }
                }
            }
            if ((cmbFilterSel.Text == string.Empty && txtSelect.Text != string.Empty)
                || (cmbFilterSel.Text == "无" && txtSelect.Text != string.Empty))
            {
                foreach (ParaData para in lstAllParaData)
                {
                    if (para.paraName.Contains(txtSelect.Text))
                    {
                        lstTempData.Add(para);
                    }
                }
            }
            if ((cmbFilterSel.Text != string.Empty && txtSelect.Text != string.Empty)
                || (cmbFilterSel.Text != "无" && txtSelect.Text != string.Empty))
            {
                foreach (ParaData para in lstAllParaData)
                {
                    if (para.paraType == cmbFilterSel.SelectedIndex && para.paraName.Contains(txtSelect.Text))
                    {
                        lstTempData.Add(para);
                    }
                }
            }

            return lstTempData;
        }

        /// <summary>
        /// 同步参数到参数列表
        /// </summary>
        private void SynchronizationWeightPara()
        {
            List<ParaData> lstPara = new List<ParaData>();
            if (lstAllParaData != null && lstAllParaData.Count > 0)
            {
                foreach (ParaData para in lstAllParaData)
                {
                    ParaData data = new ParaData();
                    data.paraName = para.paraName;
                    data.paraUnit = para.paraUnit;
                    data.paraType = para.paraType;
                    data.paraValue = para.paraValue;
                    data.strRemark = para.strRemark;

                    lstPara.Add(data);
                }
            }

            //参数表中的参数
            List<ParaData> lstTdePara = WeightEstimateForm.GetListParaData();

            if (lstPara != null && lstPara.Count > 0)
            {
                foreach (ParaData para in lstPara)
                {
                    if (MainForm.IsExitPara(para.paraName, lstTdePara) == false)
                    {
                        PubSyswareCom.CreateDoubleParameter(para.paraName, para.paraValue, true, true, false);

                        MainForm.SetParameterUnit(para.paraName, para.paraUnit);
                       
                        //设置分组
                        if (para.paraType == 0)
                        {
                            PubSyswareCom.SetParameterGroup(para.paraName, "指标参数");
                        }
                        if (para.paraType == 1)
                        {
                            PubSyswareCom.SetParameterGroup(para.paraName, "构型和总体参数");
                        }
                        if (para.paraType == 2)
                        {
                            PubSyswareCom.SetParameterGroup(para.paraName, "旋翼参数");
                        }
                        if (para.paraType == 3)
                        {
                            PubSyswareCom.SetParameterGroup(para.paraName, "机身翼面参数");
                        }
                        if (para.paraType == 4)
                        {
                            PubSyswareCom.SetParameterGroup(para.paraName, "着陆装置参数");
                        }
                        if (para.paraType == 5)
                        {
                            PubSyswareCom.SetParameterGroup(para.paraName, "动力系统参数");
                        }
                        if (para.paraType == 6)
                        {
                            PubSyswareCom.SetParameterGroup(para.paraName, "传动系统参数");
                        }
                        if (para.paraType == 7)
                        {
                            PubSyswareCom.SetParameterGroup(para.paraName, "操纵系统参数");
                        }
                        if (para.paraType == 8)
                        {
                            PubSyswareCom.SetParameterGroup(para.paraName, "人工参数");
                        }
                        if (para.paraType == 9)
                        {
                            PubSyswareCom.SetParameterGroup(para.paraName, "其他类型参数");
                        }
                        if (para.paraType == 10)
                        {
                            PubSyswareCom.SetParameterGroup(para.paraName, "临时参数");
                        }
                    }
                }

            }

            /*------------------------------------------删除文件中没有的参数-----------------------------*/

            List<string> lstName = new List<string>();

            for (int i = 0; i < lstTdePara.Count; i++)
            {
                bool IsExit = false;
                foreach (ParaData data in lstPara)
                {
                    if (data.paraName == lstTdePara[i].paraName)
                    {
                        IsExit = true;
                        break;
                    }
                }
                if (IsExit == false)
                {
                    lstName.Add(lstTdePara[i].paraName);
                }
            }

            //删除参数
            foreach (string str in lstName)
            {
                PubSyswareCom.DeleteParameter(string.Empty, str);
            }

            //----------------------------------------------------------------------------------------------//
            XLog.Write("同步参数表成功");
        }

        private void TreeSelNode(ParaData para)
        {
            TreeNode rootNode = null;
            if (treeViewParameterList.Nodes.Count > 0)
            {
                rootNode = treeViewParameterList.Nodes[0];

                if (rootNode != null && rootNode.Nodes.Count > 0)
                {
                    foreach (TreeNode parentNode in rootNode.Nodes)
                    {
                        if (parentNode.Nodes.Count > 0 && para.paraType.ToString() == parentNode.Name)
                        {
                            foreach (TreeNode node in parentNode.Nodes)
                            {
                                if (para.paraName == node.Name)
                                {
                                    treeViewParameterList.SelectedNode = node;
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 新建事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            SetingPageButton("new");
            SettingTitle("new");

            strType = "new";
        }

        /// <summary>
        /// 确定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnParameterConfirm_Click(object sender, EventArgs e)
        {
            string strErroInfo = GetPageVerification();
            if (strErroInfo != string.Empty)
            {
                XLog.Write(strErroInfo);
                return;
            }

            ParaData paraData = GetPageData();
            bool IsRecover = false;

            if (strType == "new" || strType == "JYNew")
            {
                for (int i = 0; i < lstAllParaData.Count; i++)
                {
                    //新建参数名重复提示覆盖
                    if (paraData.paraType == lstAllParaData[i].paraType && paraData.paraName == lstAllParaData[i].paraName)
                    {
                        DialogResult result = MessageBox.Show("参数名称" + paraData.paraName + "同名是否覆盖？", "覆盖提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (result == DialogResult.Yes)
                        {
                            lstAllParaData[i] = paraData;
                            IsRecover = true;
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                if (IsRecover == false)
                {
                    lstAllParaData.Add(paraData);
                }
            }
            if (strType == "edit")
            {
                // 先删除
                ParaData tempPara = new ParaData();

                string[] strArray = selNode.ToolTipText.Split('|');
                int paraType = -1;
                if (strArray != null && strArray.Length > 2)
                {
                    paraType = int.Parse(strArray[2]);
                }
                for (int j = 0; j < lstAllParaData.Count; j++)
                {
                    if (lstAllParaData[j].paraType == paraType && selNode.Name == lstAllParaData[j].paraName)
                    {
                        tempPara = lstAllParaData[j];
                        lstAllParaData.Remove(lstAllParaData[j]);
                    }
                }


                bool IsSame = false;
                // 在已有的参数列表中查找当前同名同类型参数
                foreach (ParaData para in lstAllParaData)
                {
                    if (para.paraName == paraData.paraName && para.paraType == paraData.paraType)
                    {
                        MessageBox.Show("当前存在同类型同名称参数,请修改!");
                        IsSame = true;
                        lstAllParaData.Add(tempPara);
                        return;
                    }
                }
                // 没有便进行修改
                if (IsSame == false)
                {
                    lstAllParaData.Add(paraData);
                }
            }

            //lst排序
            List<ParaData> lstTemp = GetSelParaData();
            lstTemp = lstTemp.OrderBy(s => s.paraType).ToList();

            BindTreeParaData(lstTemp);

            SetingPageButton("confirm");
            SettingTitle("confirm");

            TreeSelNode(paraData);
        }

        /// <summary>
        /// 单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewParameterList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            selNode = e.Node;
            treeViewParameterList.SelectedNode = e.Node;

            if (selNode.Level == 2)
            {
                //设置页面数据
                SettingPageData();
            }

        }

        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selNode == null || selNode.Level != 2)
            {
                XLog.Write("请选择参数");
                return;
            }
            if (selNode.Level == 2)
            {
                SettingPageData();
                SetingPageButton("edit");
                SettingTitle("edit");

                strType = "edit";
            }
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selNode == null)
            {
                XLog.Write("请选择参数");
                return;
            }

            DialogResult result = MessageBox.Show("是否删除参数" + selNode.Text + "？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                if (selNode.Level == 0)
                {
                    if (lstParaData.Count > 0 && lstAllParaData.Count > 0)
                    {
                        for (int i = 0; i < lstParaData.Count; i++)
                        {
                            for (int j = 0; j < lstAllParaData.Count; j++)
                            {
                                if (lstParaData[i].paraName == lstAllParaData[j].paraName && lstParaData[i].paraType == lstAllParaData[j].paraType)
                                {
                                    lstAllParaData.Remove(lstAllParaData[j]);
                                }
                            }
                        }
                    }
                }
                if (selNode.Level == 1)
                {
                    List<ParaData> lstTempData = new List<ParaData>();
                    for (int i = 0; i < lstParaData.Count; i++)
                    {
                        if (selNode.Name == lstParaData[i].paraType.ToString())
                        {
                            lstTempData.Add(lstParaData[i]);
                        }
                    }
                    for (int i = 0; i < lstTempData.Count; i++)
                    {
                        for (int j = 0; j < lstAllParaData.Count; j++)
                        {
                            if (lstTempData[i].paraName == lstAllParaData[j].paraName && lstTempData[i].paraType == lstAllParaData[j].paraType)
                            {
                                lstAllParaData.Remove(lstAllParaData[j]);
                            }
                        }
                    }
                }
                if (selNode.Level == 2)
                {
                    //获取参数类型
                    string[] strArray = selNode.ToolTipText.Split('|');
                    int paraType = -1;
                    if (strArray != null && strArray.Length > 2)
                    {
                        paraType = int.Parse(strArray[2]);
                    }

                    for (int j = 0; j < lstAllParaData.Count; j++)
                    {
                        if (paraType == lstAllParaData[j].paraType && selNode.Name == lstAllParaData[j].paraName)
                        {
                            lstAllParaData.Remove(lstAllParaData[j]);
                        }
                    }
                }
                strType = "delete";

                selNode = null;
                SetingPageButton(strType);
                //刷新列表
                BindTreeParaData(GetSelParaData());

                XLog.Write("删除参数成功");
            }
        }

        private void treeViewParameterList_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
        }

        /// <summary>
        /// 确定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnComfirm_Click(object sender, EventArgs e)
        {
            //创建参数数据集文件夹
            if (!Directory.Exists(strParaPath))
            {
                Directory.CreateDirectory(strParaPath);
            }

            if ((lstParaData != null && lstParaData.Count > 0) || strType != string.Empty)
            {
                string strFilePath = strParaPath + "\\" + "ParameterCollection.PMC";
                SaveFile(strFilePath, lstAllParaData);
            }

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
                List<ParaData> lstTempParaData = new List<ParaData>();

                if (lstAllParaData.Count > 0)
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();

                    fileDialog.Filter = "PMC文件 (*.PMC)|*.PMC|xls文件 (*.xls)|*.xls|All File (*.*)|*.*";
                    fileDialog.RestoreDirectory = true;
                    fileDialog.FilterIndex = 1;

                    bool IsCover = false;
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        List<ParaData> lstContainParaData = new List<ParaData>();

                        string strFilePath = fileDialog.FileName;
                        if (strFilePath.EndsWith(".PMC"))
                        {
                            lstTempParaData = GetParaData(strFilePath);
                        }
                        else if (strFilePath.EndsWith(".xls"))
                        {
                            lstTempParaData = GetFileParaData(strFilePath);
                        }

                        for (int i = 0; i < lstTempParaData.Count; i++)
                        {
                            for (int j = 0; j < lstAllParaData.Count; j++)
                            {
                                if (lstTempParaData[i].paraType == lstAllParaData[j].paraType && lstTempParaData[i].paraName == lstAllParaData[j].paraName)
                                {
                                    lstAllParaData[j] = lstTempParaData[i];
                                    lstContainParaData.Add(lstTempParaData[i]);
                                    IsCover = true;
                                }
                            }
                        }

                        if (IsCover)
                        {
                            DialogResult result = MessageBox.Show("导入有同名的参数,是否覆盖？", "覆盖提示", MessageBoxButtons.OKCancel);

                            if (result == DialogResult.OK)
                            {

                                //相同的覆盖
                                for (int i = 0; i < lstTempParaData.Count; i++)
                                {
                                    for (int j = 0; j < lstTempParaData.Count; j++)
                                    {
                                        if (lstTempParaData[i].paraType == lstAllParaData[j].paraType && lstTempParaData[i].paraName == lstParaData[j].paraName)
                                        {
                                            lstAllParaData[j] = lstTempParaData[i];
                                        }
                                    }
                                }

                                //移除相同参数
                                for (int k = 0; k < lstContainParaData.Count; k++)
                                {
                                    lstTempParaData.Remove(lstContainParaData[k]);
                                }
                            }
                            else
                            {
                                return;
                            }

                        }
                        for (int m = 0; m < lstTempParaData.Count; m++)
                        {
                            lstAllParaData.Add(lstTempParaData[m]);
                        }


                        BindTreeParaData(GetSelParaData());
                    }

                    XLog.Write("导入成功");
                }
                else
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();

                    fileDialog.Filter = "PMC文件 (*.PMC)|*.PMC|xls文件 (*.xls)|*.xls|All File (*.*)|*.*";
                    fileDialog.RestoreDirectory = true;
                    fileDialog.FilterIndex = 1;

                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string strFilePath = fileDialog.FileName;

                        if (strFilePath.EndsWith(".PMC"))
                        {
                            lstTempParaData = GetParaData(strFilePath);
                        }
                        if (strFilePath.EndsWith(".xls"))
                        {
                            lstTempParaData = GetFileParaData(strFilePath);
                        }
                        lstAllParaData = lstTempParaData;
                        BindTreeParaData(GetSelParaData());
                    }
                    XLog.Write("导入成功");
                }
            }
            catch (Exception ex)
            {
                XLog.Write("导入参数文件错误."+ex.Message);
                MessageBox.Show("导入参数文件错误");
            }
        }

        /// <summary>
        ///  导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (treeViewParameterList.Nodes.Count == 0)
            {
                XLog.Write("没有数据不能导出");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "PMC文件 (*.PMC)|*.PMC|xls文件 (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = "ParameterCollection";

            List<ParaData> lstTempParaDagta = lstParaData;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dlg.FileName;

                if (strFilePath.EndsWith(".PMC"))
                {
                    SaveFile(strFilePath, lstTempParaDagta);
                }
                if (strFilePath.EndsWith(".xls"))
                {
                    DataTable table = GetTableData(lstTempParaDagta);
                    int result = XCommon.FileStatus.FileIsOpen(strFilePath);

                    if (result == 1)
                    {
                        MessageBox.Show("请关闭文件" + strFilePath + ",再导出");
                    }
                    else
                    {
                        CommonExcel commonExcel = new CommonExcel();
                        commonExcel.lstColumn = GetColumnName();
                        commonExcel.CreateExcel("example", strFilePath, "sheet1", table);
                    }
                }

                XLog.Write("导出成功");
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            List<ParaData> lstTempData = GetSelParaData();
            BindTreeParaData(lstTempData);

            strType = "select";
            selNode = null;
            SetingPageButton(strType);
        }

        /// <summary>
        /// 全局导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllImort_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstParaData.Count > 0)
                {
                    DialogResult result = MessageBox.Show("导入会清空当前列表数据,是否继续？", "清空提示", MessageBoxButtons.OKCancel);

                    if (result == DialogResult.OK)
                    {
                        OpenFileDialog fileDialog = new OpenFileDialog();

                        fileDialog.Filter = "PMC文件 (*.PMC)|*.PMC"; ;
                        fileDialog.RestoreDirectory = true;
                        fileDialog.FilterIndex = 1;

                        if (fileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string strFilePath = fileDialog.FileName;
                            List<ParaData> lstTempParaData = GetParaData(strFilePath);
                            lstAllParaData = lstTempParaData;
                            BindTreeParaData(GetSelParaData());
                        }

                        XLog.Write("导入成功");
                    }
                }
                else
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();

                    fileDialog.Filter = "PMC文件 (*.PMC)|*.PMC";
                    fileDialog.RestoreDirectory = true;
                    fileDialog.FilterIndex = 1;

                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string strFilePath = fileDialog.FileName;
                        List<ParaData> lstTempParaData = GetParaData(strFilePath);
                        lstAllParaData = lstTempParaData;
                        BindTreeParaData(GetSelParaData());
                    }
                    XLog.Write("导入成功");
                }
            }
            catch
            {
                XLog.Write("导入参数文件错误");
                MessageBox.Show("导入参数文件错误");
            }
        }

        /// <summary>
        /// 全局导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllExport_Click(object sender, EventArgs e)
        {
            if (lstParaData.Count == 0)
            {
                XLog.Write("没有参数数据不能导出");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "PMC文件 (*.PMC)|*.PMC";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = "ParameterCollection";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dlg.FileName;
                SaveFile(strFilePath, lstAllParaData);

                XLog.Write("导出成功");
            }

        }

        /// <summary>
        /// 基于新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJYNew_Click(object sender, EventArgs e)
        {
            if (selNode == null || selNode.Level != 2)
            {
                XLog.Write("请选择参数");
                return;
            }

            strType = "JYNew";
            SettingPageData();
            SettingTitle(strType);
            SetingPageButton(strType);
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            SetingPageButton("cancle");

            if (selNode.Level == 2)
            {
                txtParameterName.Text = selNode.Name;
                string[] strArray = selNode.ToolTipText.Split('|');
                if (strArray.Length > 3)
                {
                    txtUnit.Text = strArray[1];
                    cmbParameterType.SelectedIndex = int.Parse(strArray[2]);
                    txtParameterRemark.Text = strArray[3];

                }
            }
        }

        /// <summary>
        /// 同步参数表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPuslishToTde_Click(object sender, EventArgs e)
        {
            if (PubSyswareCom.IsRuntimeServerStarted())
            {
                DialogResult result = MessageBox.Show("是否参数同步?", "同步提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    SynchronizationWeightPara();

                    MessageBox.Show("同步参数完成");
                }
            }
            else
            {
                MessageBox.Show("TDE/IDE 没有启动成功");
            }
        }

        #endregion

        private void treeViewParameterList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selNode = e.Node;
            if (selNode.Level == 2)
            {
                txtParameterName.Text = e.Node.Name;
                string[] strArray = e.Node.ToolTipText.Split('|');
                if (strArray.Length > 3)
                {
                    txtUnit.Text = strArray[1];
                    cmbParameterType.SelectedIndex = int.Parse(strArray[2]);
                    txtParameterRemark.Text = strArray[3];

                }
            }
            else
            {
                txtParameterName.Text = string.Empty;
                txtUnit.Text = string.Empty;
                cmbParameterType.SelectedIndex = -1;
                txtParameterRemark.Text = string.Empty;
            }
        }
    }
}
