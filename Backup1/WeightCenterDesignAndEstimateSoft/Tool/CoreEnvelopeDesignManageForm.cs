using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL;
using Model;
using XCommon;
using ZedGraph;
using System.IO;
using ZaeroModelSystem;
using System.Xml;
using System.Windows.Forms.DataVisualization.Charting;

namespace WeightCenterDesignAndEstimateSoft.Tool
{
    public partial class CoreEnvelopeDesignManageForm : Form
    {
        #region 属性

        private BLLCoreEnvelopeDesign bllCoreEnvelopeDesign = new BLLCoreEnvelopeDesign();

        private List<CoreEnvelopeDesign> lstCoreEnvelopeData = null;
        private CoreEnvelopeDesign coreEnvelopeDesign = null;

        private string strOperType = string.Empty;
        private TreeNode selNode = null;

        private List<CorePointData> lstCorePtData = null;

        private const int digit = 6;
        private const int picDigit = 3;

        //重量包线
        public List<string> lstCoreEnvelope = null;

        public CoreEnvelopeDesignManageForm()
        {
            InitializeComponent();

            SetPageButton();
            BindCoreEnvelopeData();
        }

        #endregion

        #region 自定义方法

        /// <summary>
        /// 绑定重心包线数据
        /// </summary>
        private void BindCoreEnvelopeData()
        {
            treeViewList.Nodes.Clear();

            lstCoreEnvelopeData = bllCoreEnvelopeDesign.GetListModel();

            if (lstCoreEnvelopeData.Count > 0)
            {
                TreeNode node = new TreeNode("重心包线数据列表");
                treeViewList.Nodes.Add(node);

                foreach (CoreEnvelopeDesign core in lstCoreEnvelopeData)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Name = core.Id.ToString();
                    childNode.Text = core.DesignData_Name;
                    node.Nodes.Add(childNode);
                }
                node.Expand();
            }
        }

        private void SetPageButton()
        {
            if (strOperType == CommonMessage.operNone || strOperType == CommonMessage.operConfirm || strOperType == CommonMessage.operCancle)
            {
                txtDesignDataName.Enabled = false;
                txtHelicopterName.Enabled = false;
                txtSubmitter.Enabled = false;
                txtRemark.Enabled = false;
                txtDesignTakingWeight.Enabled = false;

                btnConfimModify.Enabled = false;
                btnCancleModify.Enabled = false;
                btnEditCoreEnvelope.Enabled = false;
                gridCoreEnvelope.ReadOnly = true;

                btnImportCoreData.Enabled = false;

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
                txtDesignTakingWeight.Enabled = true;

                btnConfimModify.Enabled = true;
                btnCancleModify.Enabled = true;
                btnEditCoreEnvelope.Enabled = true;
                gridCoreEnvelope.ReadOnly = false;

                btnImportCoreData.Enabled = true;

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
        /// 获取重心包线数据实体
        /// </summary>
        /// <returns></returns>
        private void GetPageCoreEnvelopeDesignData()
        {
            if (strOperType == CommonMessage.operEdit)
            {
                coreEnvelopeDesign.Id = Convert.ToInt32(selNode.Name);
            }
            else
            {
                coreEnvelopeDesign.Id = bllCoreEnvelopeDesign.GetMaxId();
            }

            coreEnvelopeDesign.DesignData_Name = txtDesignDataName.Text;
            coreEnvelopeDesign.DesignData_Submitter = txtSubmitter.Text;
            coreEnvelopeDesign.Helicopter_Name = txtHelicopterName.Text;
            coreEnvelopeDesign.DataRemark = txtRemark.Text;
            coreEnvelopeDesign.LastModify_Time = DateTime.Now.ToString();
            coreEnvelopeDesign.DesignTaking_Weight = txtDesignTakingWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtDesignTakingWeight.Text);
            //重心包线
            coreEnvelopeDesign.CoreEnvelope = GetCoreEnvelope(gridCoreEnvelope);
        }

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
        /// 设置操作标题
        /// </summary>
        /// <param name="strType"></param>
        private void SetOperTitle(string strType)
        {
            if (strType == CommonMessage.operNew)
            {
                gruEnvelopeData.Text = "新建重心包线设计数据";
            }
            if (strType == CommonMessage.operJYNew)
            {
                gruEnvelopeData.Text = "基于重心包线设计数据";
            }
            if (strType == CommonMessage.operEdit)
            {
                gruEnvelopeData.Text = "编辑重心包线设计数据";
            }
            if (strType == CommonMessage.operCancle || strType == CommonMessage.operConfirm)
            {
                gruEnvelopeData.Text = "重心包线设计数据";
            }
        }

        /// <summary>
        /// 获取从字符串转换成重心数据List
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private List<CorePointData> GetStringToListCorePointData(string strCoreEnvelope)
        {
            List<CorePointData> lstCorePt = null;

            if (strCoreEnvelope != null && strCoreEnvelope != string.Empty)
            {
                lstCorePt = new List<CorePointData>();

                string[] strArray = strCoreEnvelope.Split('|');

                string strNodeName = string.Empty;
                foreach (string strCore in strArray)
                {
                    if (strCore != string.Empty)
                    {
                        int index = strCore.IndexOf(":");
                        strNodeName = strCore.Substring(0, index);

                        string[] strCoreArray = strCore.Split('、');
                        CorePointData data = new CorePointData();
                        data.pointName = strNodeName;
                        data.pointXValue = Convert.ToDouble(strCoreArray[2]);
                        data.pointYValue = Convert.ToDouble(strCoreArray[3]);
                        lstCorePt.Add(data);
                    }
                }
            }
            return lstCorePt;
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

            CoreEnvelopeDesign data = GetCoreDesginData(Convert.ToInt32(selNode.Name));

            if (data != null)
            {
                txtDesignDataName.Text = data.DesignData_Name;
                txtHelicopterName.Text = data.Helicopter_Name;
                txtSubmitter.Text = data.DesignData_Submitter;
                txtRemark.Text = data.DataRemark;
                txtDesignTakingWeight.Text = data.DesignTaking_Weight.ToString();
                txtLastModifyTime.Text = data.LastModify_Time;

                //重心包线
                List<CorePointData> lstCorePt = GetStringToListCorePointData(data.CoreEnvelope);

                //绑定gridview
                SetGridView(lstCorePt);
                BindGridViewData(lstCorePt);

                //重心包线列表
                BindCoreEnvelopeList(lstCorePt);

                //重心包线图
                DisplayInPicture(lstCorePt, zedGraphControlCore,true);
            }

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
            txtDesignTakingWeight.Text = string.Empty;
            txtLastModifyTime.Text = string.Empty;

            //DataGridView
            gridCoreEnvelope.Rows.Clear();
            gridCoreEnvelope.Columns.Clear();

            //清除原来的图形
            GraphPane myPane = zedGraphControlCore.GraphPane;
            myPane.CurveList.Clear();
            myPane.GraphObjList.Clear();
            myPane.Title.Text = "重心包线";
            zedGraphControlCore.Refresh();
            zedGraphControlCore.ContextMenuStrip = null;

            treeCoreEnvelope.Nodes.Clear();
        }

        /// <summary>
        /// 绑定重心包线列表
        /// </summary>
        public void BindCoreEnvelopeList(List<CorePointData> lstTempCorePtData)
        {
            treeCoreEnvelope.Nodes.Clear();

            lstCorePtData = lstTempCorePtData;


            if (lstCorePtData != null && lstCorePtData.Count > 0)
            {
                TreeNode node = new TreeNode("重心包线");
                treeCoreEnvelope.Nodes.Add(node);

                foreach (CorePointData data in lstCorePtData)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Name = data.pointName;
                    childNode.Text = data.pointName;
                    node.Nodes.Add(childNode);

                    TreeNode xNode = new TreeNode();
                    xNode.Name = "横坐标:" + "[" + Math.Round(data.pointXValue, digit).ToString() + "毫米]";
                    xNode.Text = "横坐标:" + "[" + Math.Round(data.pointXValue, digit).ToString() + "毫米]";
                    childNode.Nodes.Add(xNode);

                    TreeNode yNode = new TreeNode();
                    yNode.Name = "横坐标:" + "[" + Math.Round(data.pointYValue, digit).ToString() + "千克]";
                    yNode.Text = "纵坐标:" + "[" + Math.Round(data.pointYValue, digit).ToString() + "千克]";
                    childNode.Nodes.Add(yNode);
                }
                node.ExpandAll();
            }
        }

        public void SetGridView(List<CorePointData> lstCorePt)
        {
            gridCoreEnvelope.Columns.Clear();
            gridCoreEnvelope.Rows.Clear();

            if (lstCorePt != null && lstCorePt.Count > 0)
            {
                //第一列
                DataGridViewTextBoxColumn firstColumn = new DataGridViewTextBoxColumn();
                firstColumn.HeaderText = string.Empty;
                firstColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                gridCoreEnvelope.Columns.Add(firstColumn);

                for (int i = 0; i < lstCorePt.Count; i++)
                {
                    DataGridViewTextBoxColumn txtColumn = new DataGridViewTextBoxColumn();
                    txtColumn.DataPropertyName = lstCorePt[i].pointName;
                    txtColumn.HeaderText = lstCorePt[i].pointName;
                    txtColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                    gridCoreEnvelope.Columns.Add(txtColumn);

                    gridCoreEnvelope.Columns[i + 1].ValueType = System.Type.GetType("System.Decimal");
                }

                if (gridCoreEnvelope.ColumnCount > 0)
                {
                    //添加行
                    gridCoreEnvelope.Rows.Add(2);
                }

                gridCoreEnvelope.Rows[0].Cells[0].Value = "横坐标(毫米)";
                gridCoreEnvelope.Rows[1].Cells[0].Value = "纵坐标(千克)";
                gridCoreEnvelope.Columns[0].ReadOnly = true;
            }
        }

        /// <summary>
        /// 获取重心包线数据
        /// </summary>
        /// <returns></returns>
        public static string GetCoreEnvelope(DataGridView gridCoreEnvelope)
        {
            string strValue = string.Empty;
            object ptX = 0;
            object ptY = 0;

            for (int j = 1; j < gridCoreEnvelope.Columns.Count; j++)
            {
                ptX = gridCoreEnvelope.Rows[0].Cells[j].Value;
                ptY = gridCoreEnvelope.Rows[1].Cells[j].Value;
                if (ptX == null)
                {
                    ptX = 0;
                }
                if (ptY == null)
                {
                    ptY = 0;
                }
                strValue += gridCoreEnvelope.Columns[j].HeaderText + ":" + "横坐标(毫米)、纵坐标(千克)、"
                    + ptX.ToString() + "、" + ptY.ToString() + "|";
            }

            if (strValue != string.Empty)
            {
                strValue = strValue.Substring(0, strValue.Length - 1);
            }

            return strValue;
        }

        private DataTable GetTableStuctre(List<CorePointData> lstCorePtData)
        {
            DataTable table = new DataTable();

            if (lstCorePtData != null && lstCorePtData.Count > 0)
            {
                foreach (CorePointData data in lstCorePtData)
                {
                    table.Columns.Add(data.pointName);
                }
            }

            return table;
        }

        private DataTable GetTableStuctre(string strCoreEnvelope)
        {
            DataTable table = new DataTable();

            foreach (CorePointData data in lstCorePtData)
            {
                table.Columns.Add(data.pointName);
            }

            return table;
        }

        private void GetTableColumn(string strCoreEnvelope)
        {
            if (lstCoreEnvelope == null)
            {
                lstCoreEnvelope = new List<string>();
            }
            string[] strArray = strCoreEnvelope.Split('|');

            string strNodeName = string.Empty;
            lstCoreEnvelope.Clear();
            foreach (string strCore in strArray)
            {
                int index = strCore.IndexOf(":");
                strNodeName = strCore.Substring(0, index);

                lstCoreEnvelope.Add(strNodeName);
            }
        }

        public void BindGridViewData(List<CorePointData> lstCorePoint)
        {
            for (int i = 1; i < gridCoreEnvelope.Columns.Count; i++)
            {
                foreach (CorePointData data in lstCorePoint)
                {
                    if (data.pointName == gridCoreEnvelope.Columns[i].HeaderText)
                    {
                        gridCoreEnvelope.Rows[0].Cells[i].Value = Math.Round(data.pointXValue, digit);
                        gridCoreEnvelope.Rows[1].Cells[i].Value = Math.Round(data.pointYValue, digit);
                    }
                }
            }
        }

        private List<string> GetListContent()
        {
            List<string> lstContent = new List<string>();
            CoreEnvelopeDesign coreData = GetCoreDesginData(Convert.ToInt32(selNode.Name));

            if (coreData != null)
            {
                lstContent.Insert(0, "<?xml version=" + "\"1.0\"" + " encoding=" + "\"gb2312\"" + " ?> ");
                lstContent.Add("<重心包线设计数据>");

                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<设计数据名称>" + coreData.DesignData_Name + "</设计数据名称>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<设计数据提交者>" + coreData.DesignData_Submitter + "</设计数据提交者>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<直升机名称>" + coreData.Helicopter_Name + "</直升机名称>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<数据备注>" + coreData.DataRemark + "</数据备注>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<最后修改日期>" + coreData.LastModify_Time + "</最后修改日期>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<直升机设计起飞重量>" + coreData.DesignTaking_Weight.ToString() + "</直升机设计起飞重量>");

                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<包线节点列表>");

                List<CorePointData> lstCorePt = GetStringToListCorePointData(coreData.CoreEnvelope);

                if (lstCorePt != null && lstCorePt.Count > 0)
                {
                    foreach (CorePointData data in lstCorePt)
                    {
                        lstContent.Add(CommonFunction.mStrModifyToString8(2) + "<包线节点>");

                        lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<节点名称>" + data.pointName + "</节点名称>");
                        lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<横坐标>" + data.pointXValue.ToString() + "</横坐标>");
                        lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<纵坐标>" + data.pointYValue.ToString() + "</纵坐标>");

                        lstContent.Add(CommonFunction.mStrModifyToString8(2) + "</包线节点>");
                    }
                }

                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "</包线节点列表>");

                lstContent.Add("</重心包线设计数据>");

            }
            return lstContent;
        }

        private DataTable GetExcleTableStuctre()
        {
            DataTable table = new DataTable();

            //设计数据名称
            table.Columns.Add("设计数据名称");
            //设计数据提交者
            table.Columns.Add("设计数据提交者");
            //直升机名称
            table.Columns.Add("直升机名称");

            //数据备注
            table.Columns.Add("数据备注");

            //最后修改日期
            table.Columns.Add("最后修改日期");

            //直升机设计起飞重量
            table.Columns.Add("直升机设计起飞重量");

            table.Columns.Add("节点名称");
            table.Columns.Add("横坐标");
            table.Columns.Add("纵坐标");

            return table;
        }

        private List<string> GetExcleColumn()
        {
            List<string> lstColumn = new List<string>();

            lstColumn.Add("设计数据名称");
            lstColumn.Add("设计数据提交者");
            lstColumn.Add("直升机名称");
            lstColumn.Add("数据备注");
            lstColumn.Add("最后修改日期");
            lstColumn.Add("直升机设计起飞重量");

            lstColumn.Add("节点名称");
            lstColumn.Add("横坐标");
            lstColumn.Add("纵坐标");

            return lstColumn;
        }

        private DataTable GetTableExcleData()
        {
            DataTable table = GetExcleTableStuctre();

            CoreEnvelopeDesign coreData = GetCoreDesginData(Convert.ToInt32(selNode.Name));

            if (coreData != null)
            {
                DataRow drFirst = table.NewRow();
                drFirst[0] = coreData.DesignData_Name;
                drFirst[1] = coreData.DesignData_Submitter;
                drFirst[2] = coreData.Helicopter_Name;
                drFirst[3] = coreData.DataRemark;
                drFirst[4] = coreData.LastModify_Time;
                drFirst[5] = coreData.DesignTaking_Weight;
                table.Rows.Add(drFirst);

                string strCoreEnvelope = coreData.CoreEnvelope;

                if (strCoreEnvelope != string.Empty)
                {


                    string strNodeName = string.Empty;
                    int index = -1;
                    string[] strArray = strCoreEnvelope.Split('|');
                    string[] strValue = null;

                    foreach (string strCore in strArray)
                    {
                        index = strCore.IndexOf(":");
                        strNodeName = strCore.Substring(0, index);
                        strValue = strCore.Split('、');

                        if (strCore == strArray[0])
                        {
                            if (table.Rows.Count > 0)
                            {
                                table.Rows[0][6] = strNodeName;
                                table.Rows[0][7] = strValue[2];
                                table.Rows[0][8] = strValue[3];
                            }
                        }
                        else
                        {
                            DataRow dr = table.NewRow();

                            dr[0] = string.Empty;
                            dr[1] = string.Empty;
                            dr[2] = string.Empty;
                            dr[3] = string.Empty;
                            dr[4] = string.Empty;
                            dr[5] = string.Empty;

                            dr[6] = strNodeName;
                            dr[7] = strValue[2];
                            dr[8] = strValue[3];

                            table.Rows.Add(dr);
                        }
                    }
                }
            }

            return table;
        }

        private CoreEnvelopeDesign GetExcelCoreDesignData(string strFilePath)
        {
            CoreEnvelopeDesign CoreData = null;
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
                        return CoreData;
                    }
                    //取得所有的工作表名称
                    string[] strSheetsName = OpExcel.getWorkSheetsName();

                    //默认操作第一张表
                    OpExcel.SetActiveWorkSheet(1);
                    System.Data.DataTable table;
                    //列标题重复
                    table = OpExcel.getAllCellsValue();
                    OpExcel.CloseExcelApplication();

                    int count = table.Rows.Count;
                    string strCoreEnvelope = string.Empty;
                    if (count > 0)
                    {
                        CoreData = new CoreEnvelopeDesign();
                        CoreData.Id = bllCoreEnvelopeDesign.GetMaxId() + 1;

                        CoreData.DesignData_Name = table.Rows[0][0].ToString();
                        CoreData.DesignData_Submitter = table.Rows[0][1].ToString();
                        CoreData.Helicopter_Name = table.Rows[0][2].ToString();
                        CoreData.DataRemark = table.Rows[0][3].ToString();
                        CoreData.LastModify_Time = table.Rows[0][4].ToString();
                        CoreData.DesignTaking_Weight = Convert.ToDouble(table.Rows[0][5]);

                        if (table.Rows[0][6].ToString() != string.Empty)
                        {
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                strCoreEnvelope += table.Rows[i][6].ToString() + ":" + "横坐标(毫米)、纵坐标(千米)、"
                                    + table.Rows[i][7].ToString() + "、" + table.Rows[i][8].ToString() + "|";
                            }
                            strCoreEnvelope = strCoreEnvelope.Substring(0, strCoreEnvelope.Length - 1);
                        }
                        CoreData.CoreEnvelope = strCoreEnvelope;
                    }
                }
            }
            catch
            {
                XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                return null;
            }

            return CoreData;
        }

        private CoreEnvelopeDesign GetXmlCoreDesignData(string strPath)
        {
            CoreEnvelopeDesign CoreData = null;
            try
            {

                if (!File.Exists(strPath))
                {
                    return CoreData;
                }

                CoreData = new CoreEnvelopeDesign();
                CoreData.Id = bllCoreEnvelopeDesign.GetMaxId() + 1;

                string path = string.Empty;
                XmlNode node = null;

                XmlDocument doc = new XmlDocument();
                doc.Load(strPath);

                //设计数据名称
                path = "重心包线设计数据/设计数据名称";
                node = doc.SelectSingleNode(path);
                CoreData.DesignData_Name = node.InnerText;

                //设计数据提交者
                path = "重心包线设计数据/设计数据提交者";
                node = doc.SelectSingleNode(path);
                CoreData.DesignData_Submitter = node.InnerText;

                //直升机名称
                path = "重心包线设计数据/直升机名称";
                node = doc.SelectSingleNode(path);
                CoreData.Helicopter_Name = node.InnerText;


                //数据备注
                path = "重心包线设计数据/数据备注";
                node = doc.SelectSingleNode(path);
                CoreData.DataRemark = node.InnerText;

                //最后修改日期
                path = "重心包线设计数据/最后修改日期";
                node = doc.SelectSingleNode(path);
                CoreData.LastModify_Time = node.InnerText;


                //设计起飞重量
                path = "重心包线设计数据/直升机设计起飞重量";
                node = doc.SelectSingleNode(path);
                CoreData.DesignTaking_Weight = Convert.ToDouble(node.InnerText);

                //重量列表
                path = "重心包线设计数据/包线节点列表";
                node = doc.SelectSingleNode(path);

                string strCoreEnvelope = string.Empty;
                if (node.ChildNodes.Count > 0)
                {
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        strCoreEnvelope += node.ChildNodes[i].ChildNodes[0].InnerText + ":" + "横坐标(毫米)、纵坐标(千米)、"
                            + node.ChildNodes[i].ChildNodes[1].InnerText + "、" + node.ChildNodes[i].ChildNodes[2].InnerText + "|";
                    }
                    strCoreEnvelope = strCoreEnvelope.Substring(0, strCoreEnvelope.Length - 1);
                }

                CoreData.CoreEnvelope = strCoreEnvelope;
            }
            catch
            {
                XLog.Write("导入文件\"" + strPath + "\"格式错误");
                MessageBox.Show("导入文件\"" + strPath + "\"格式错误");
                return null;
            }

            return CoreData;
        }

        private CoreEnvelopeDesign GetCoreDesginData(int Id)
        {
            CoreEnvelopeDesign core = null;
            foreach (CoreEnvelopeDesign data in lstCoreEnvelopeData)
            {
                if (data.Id.ToString() == selNode.Name)
                {
                    core = data;
                }
            }

            return core;
        }

        private List<CorePointData> GetCurrentCorePoint()
        {
            List<CorePointData> lstCorePt = null;

            if (gridCoreEnvelope.Rows.Count > 0)
            {
                lstCorePt = new List<CorePointData>();
                for (int i = 1; i < gridCoreEnvelope.ColumnCount; i++)
                {
                    CorePointData pt = new CorePointData();
                    pt.pointName = gridCoreEnvelope.Columns[i].HeaderText;
                    pt.pointXValue = Convert.ToDouble(gridCoreEnvelope.Rows[0].Cells[i].Value);
                    pt.pointYValue = Convert.ToDouble(gridCoreEnvelope.Rows[1].Cells[i].Value);
                    lstCorePt.Add(pt);
                }

            }
            return lstCorePt;
        }

        private List<CorePointData> GetExcelListCorePointData(string strFilePath)
        {
            List<CorePointData> lstCorePtData = null;
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
                        return lstCorePtData;
                    }
                    //取得所有的工作表名称
                    string[] strSheetsName = OpExcel.getWorkSheetsName();

                    //默认操作第一张表
                    OpExcel.SetActiveWorkSheet(1);
                    System.Data.DataTable table;
                    //列标题重复
                    table = OpExcel.getAllCellsValue();
                    OpExcel.CloseExcelApplication();

                    int count = table.Rows.Count;

                    if (count > 0)
                    {
                        lstCorePtData = new List<CorePointData>();

                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            CorePointData pt = new CorePointData();

                            pt.pointName = table.Rows[i][0].ToString();
                            pt.pointXValue = Convert.ToDouble(table.Rows[i][3].ToString());
                            pt.pointYValue = Convert.ToDouble(table.Rows[i][4].ToString());

                            lstCorePtData.Add(pt);
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

            return lstCorePtData;
        }

        private List<CorePointData> GetXmlListCorePointData(string strFilePath)
        {
            List<CorePointData> lstCorePointData = null;
            try
            {

                if (!File.Exists(strFilePath))
                {
                    return lstCorePointData;
                }

                string path = string.Empty;
                XmlNode node = null;

                XmlDocument doc = new XmlDocument();
                doc.Load(strFilePath);

                //重量列表
                path = "重心数据/重心坐标列表";
                node = doc.SelectSingleNode(path);

                XmlNodeList nodeList = node.ChildNodes;

                if (nodeList.Count > 0)
                {
                    lstCorePointData = new List<CorePointData>();

                    foreach (XmlNode childNode in nodeList)
                    {
                        CorePointData pt = new CorePointData();

                        pt.pointName = childNode.ChildNodes[0].InnerText;
                        pt.pointXValue = Convert.ToDouble(childNode.ChildNodes[3].InnerText);
                        pt.pointYValue = Convert.ToDouble(childNode.ChildNodes[4].InnerText);

                        lstCorePointData.Add(pt);
                    }
                }
            }
            catch
            {
                XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                return null;
            }
            return lstCorePointData;
        }

        /// <summary>
        /// 判断导入重心数据文件格式是否正确
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="path"></param>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private bool IsImorCoreDataFileFormat(string strType, string strPath)
        {
            bool IsRight = false;

            try
            {
                //重心数据
                if (strType == "core")
                {
                    if (strPath.EndsWith(".xml"))
                    {
                        XmlNode node = null;
                        XmlDocument doc = new XmlDocument();
                        doc.Load(strPath);

                        node = doc.SelectSingleNode("重心数据/重心坐标列表");
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

                        if (table.Columns[0].Caption == "重心坐标点名称")
                        {
                            IsRight = true;
                        }
                    }
                }

                ///重量设计
                if (strType == "coreEnvelope")
                {
                    if (strPath.EndsWith(".xml"))
                    {
                        XmlNode node = null;
                        XmlDocument doc = new XmlDocument();
                        doc.Load(strPath);

                        node = doc.SelectSingleNode("重心包线设计数据/设计数据名称");
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
            catch
            {
                return false;
            }
            return IsRight;
        }

        public static void DisplayInPicture(List<CorePointData> lstCorePt, ZedGraphControl zedGraphControlCore, bool IsClosed)
        {
            GraphPane myPane = zedGraphControlCore.GraphPane;

            //清除原来的图形
            myPane.CurveList.Clear();
            myPane.GraphObjList.Clear();

            //设置网格线可见
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            //设置网格线颜色
            myPane.XAxis.MajorGrid.Color = Color.Chocolate;
            myPane.YAxis.MajorGrid.Color = Color.Chocolate;

            //设置网格线形式
            myPane.XAxis.MajorGrid.DashOff = 1;
            myPane.YAxis.MajorGrid.DashOff = 1;
            myPane.XAxis.MajorGrid.DashOn = 4;
            myPane.YAxis.MajorGrid.DashOn = 4;

            //设置显示坐标
            myPane.XAxis.Scale.IsUseTenPower = false;
            myPane.YAxis.Scale.IsUseTenPower = false;
            myPane.XAxis.Scale.MagAuto = true;
            myPane.YAxis.Scale.MagAuto = true;

            myPane.Title.Text = "重心包线";
            myPane.XAxis.Title.Text = "长度(毫米)";
            myPane.YAxis.Title.Text = "重量(千克)";

            PointPairList listCur = new PointPairList();

            double x = 0, y = 0;
            string strTitle = string.Empty;
            string strValue = string.Empty;
            if (lstCorePt != null && lstCorePt.Count > 0)
            {
                for (int j = 0; j < lstCorePt.Count; j++)
                {
                    x = Math.Round(lstCorePt[j].pointXValue, picDigit);
                    y = Math.Round(lstCorePt[j].pointYValue, picDigit);
                    listCur.Add(x, y);

                    //显示名称
                    strTitle = lstCorePt[j].pointName;
                    // 创建一个阴影区域，看起来有渐变
                    TextObj text = new TextObj(strTitle, x, y,
                        CoordType.AxisXYScale, AlignH.Right, AlignV.Center);
                    //是否有背景
                    text.FontSpec.Fill.IsVisible = false;
                    //是否有边框
                    text.FontSpec.Border.IsVisible = false;
                    //文字是否粗体
                    text.FontSpec.IsBold = true;
                    //文字是否斜体
                    text.FontSpec.IsItalic = false;
                    //填充
                    myPane.GraphObjList.Add(text);
                }

                //是否成环形图形
                if (IsClosed)
                {
                    listCur.Add(Math.Round(lstCorePt[0].pointXValue, picDigit), Math.Round(lstCorePt[0].pointYValue, picDigit));
                }
                LineItem myCurveCur = myPane.AddCurve(string.Empty, listCur, Color.Blue, SymbolType.Default);
                myCurveCur.Symbol.Size = 6;
                myCurveCur.Symbol.Fill = new Fill(Color.Blue, Color.Blue);
                myCurveCur.Symbol.Border.IsVisible = true;
                myCurveCur.Line.IsVisible = true;
            }
            zedGraphControlCore.AxisChange();
            zedGraphControlCore.Refresh();
        }

        #endregion

        #region  事件处理

        /// <summary>
        /// 确认修改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfimModify_Click(object sender, EventArgs e)
        {
            string strErroInfo = PageVerificationInfo();
            if (strErroInfo != string.Empty)
            {
                XLog.Write(strErroInfo);
                return;
            }

            GetPageCoreEnvelopeDesignData();

            if (strOperType == CommonMessage.operNew || strOperType == CommonMessage.operJYNew)
            {
                bool IsAdd = bllCoreEnvelopeDesign.Add(coreEnvelopeDesign);

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
                bool IsEdit = bllCoreEnvelopeDesign.Update(coreEnvelopeDesign);

                if (IsEdit)
                {
                    XLog.Write("修改成功");
                }
            }

            strOperType = CommonMessage.operConfirm;
            SetPageButton();
            BindCoreEnvelopeData();

            SetOperTitle(strOperType);

            foreach (TreeNode node in treeViewList.Nodes[0].Nodes)
            {
                if (node.Name == coreEnvelopeDesign.Id.ToString())
                {
                    treeViewList.SelectedNode = node;
                    selNode = node;
                }
            }
            coreEnvelopeDesign = null;
        }

        private void treeViewList_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
        }

        /// <summary>
        /// 重心包线节点单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            selNode = e.Node;
            treeViewList.SelectedNode = e.Node;
            coreEnvelopeDesign = new CoreEnvelopeDesign();
            //SettingPageData();
        }

        /// <summary>
        /// 新建事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            strOperType = CommonMessage.operNew;
            SetPageButton();
            IntiControl();
            SetOperTitle(strOperType);

            coreEnvelopeDesign = new CoreEnvelopeDesign();
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
                XLog.Write("请选择重心包线数据");
                return;
            }
            strOperType = CommonMessage.operEdit;
            SetPageButton();
            SetOperTitle(strOperType);

            coreEnvelopeDesign = GetCoreDesginData(Convert.ToInt32(selNode.Name));
        }

        /// <summary>
        /// 基于新建事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJYNew_Click(object sender, EventArgs e)
        {
            if (selNode == null || selNode.Level == 0)
            {
                XLog.Write("请选择重心包线数据");
                return;
            }

            strOperType = CommonMessage.operJYNew;
            coreEnvelopeDesign = GetCoreDesginData(Convert.ToInt32(selNode.Name));
            SetPageButton();
            SetOperTitle(strOperType);
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
                XLog.Write("请选择重重心包线设计数据");
                return;
            }

            DialogResult result = MessageBox.Show("是否删除" + selNode.Text + "?", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {

                CoreEnvelopeDesign data = GetCoreDesginData(Convert.ToInt32(selNode.Name));
                bool IsDelete = bllCoreEnvelopeDesign.Delete(data.Id);

                if (IsDelete)
                {
                    XLog.Write("删除重心包线设计数据:" + data.DesignData_Name + "成功");
                    selNode = null;
                    IntiControl();
                    strOperType = CommonMessage.operDelete;
                    SetPageButton();
                    BindCoreEnvelopeData();
                }
            }

        }

        /// <summary>
        /// 刷新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindCoreEnvelopeData();
            selNode = null;
        }

        /// <summary>
        /// 编辑重心包线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditCoreEnvelope_Click(object sender, EventArgs e)
        {
            List<CorePointData> lstCorePoint = GetCurrentCorePoint();
            CoreEnvelopeForm form = new CoreEnvelopeForm(this, lstCorePoint, zedGraphControlCore);
            form.ShowDialog();
        }

        /// <summary>
        /// 取消修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancleModify_Click(object sender, EventArgs e)
        {
            strOperType = CommonMessage.operCancle;
            SetPageButton();
            SetOperTitle(string.Empty);

            SettingPageData();
            coreEnvelopeDesign = null;
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
                //if (IsImorCoreDataFileFormat("coreEnvelope", strFilePath) == false)
                //{
                //    XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                //    MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                //    return;
                //}

                //获取重心包线数据
                CoreEnvelopeDesign coreDesign = null;
                if (strFilePath.EndsWith(".xls"))
                {
                    coreDesign = GetExcelCoreDesignData(strFilePath);
                }
                else if (strFilePath.EndsWith(".xml"))
                {
                    coreDesign = GetXmlCoreDesignData(strFilePath);
                }
                else
                {
                    XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                    MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                    return;
                }
                if (coreDesign == null)
                {
                    return;
                }

                if (bllCoreEnvelopeDesign.Add(coreDesign))
                {
                    //刷新列表
                    BindCoreEnvelopeData();

                    foreach (TreeNode node in treeViewList.Nodes[0].Nodes)
                    {
                        if (node.Name == coreDesign.Id.ToString())
                        {
                            treeViewList.SelectedNode = node;
                            selNode = node;
                        }
                    }

                    SettingPageData();

                    XLog.Write("导入文件\"" + strFilePath + "\"成功");
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
                XLog.Write("请选择重重心包线设计数据");
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
                }
                if (strFilePath.EndsWith(".xls"))
                {
                    DataTable table = GetTableExcleData();
                    int result = XCommon.FileStatus.FileIsOpen(strFilePath);

                    if (result == 1)
                    {
                        MessageBox.Show("请关闭文件" + strFilePath + ",再导出");
                    }
                    else
                    {
                        CommonExcel commonExcel = new CommonExcel();
                        commonExcel.lstColumn = GetExcleColumn(); ;

                        commonExcel.CreateExcel("example", strFilePath, "sheet1", table);
                    }
                }

                XLog.Write("导入文件\"" + strFilePath + "\"成功");
            }
        }

        private void gridCoreEnvelope_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            List<CorePointData> lstCurrentData = GetCurrentCorePoint();

            //绑定重心包线树
            BindCoreEnvelopeList(lstCurrentData);

            //绑定图形
            DisplayInPicture(lstCurrentData, zedGraphControlCore,true);
        }

        private void gridCoreEnvelope_DataError(object sender, System.Windows.Forms.DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("非法数字");
        }

        /// <summary>
        /// 导入重心数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportCoreData_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "xml文件 (*.xml)|*.xml|Excle文件 (*.xls)|*.xls|All File (*.*)|*.*";
            fileDialog.RestoreDirectory = true;
            fileDialog.FilterIndex = 1;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = fileDialog.FileName;
                //if (IsImorCoreDataFileFormat("core", strFilePath) == false)
                //{
                //    XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                //    MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");

                //    return;
                //}

                //获取重心包线数据
                List<CorePointData> lstCorePointData = null;
                if (strFilePath.EndsWith(".xls"))
                {
                    lstCorePointData = GetExcelListCorePointData(strFilePath);
                }
                else if (strFilePath.EndsWith(".xml"))
                {
                    lstCorePointData = GetXmlListCorePointData(strFilePath);
                }
                else
                {
                    XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                    MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                    return;
                }

                if (lstCorePointData == null)
                {
                    return;
                }
                if (lstCorePointData != null && lstCorePointData.Count == 0)
                {
                    XLog.Write("导入文件\"" + strFilePath + "\"没有重心数据");
                    MessageBox.Show("导入文件\"" + strFilePath + "\"没有重心数据");
                    return;
                }

                if (treeCoreEnvelope.Nodes.Count > 0)
                {
                    DialogResult result = MessageBox.Show("导入重心数据会清空当前重心数据,是否覆盖？", "覆盖提示", MessageBoxButtons.OKCancel);

                    if (result == DialogResult.OK)
                    {
                        //重心包线列表
                        BindCoreEnvelopeList(lstCorePointData);

                        //绑定gridview
                        SetGridView(lstCorePointData);
                        BindGridViewData(lstCorePointData);

                        //重心包线图
                        DisplayInPicture(lstCorePointData, zedGraphControlCore,true);

                        XLog.Write("导入文件\"" + strFilePath + "\"成功");
                    }
                }
                else
                {
                    //重心包线列表
                    BindCoreEnvelopeList(lstCorePointData);

                    //绑定gridview
                    SetGridView(lstCorePointData);
                    BindGridViewData(lstCorePointData);

                    //重心包线图
                    DisplayInPicture(lstCorePointData, zedGraphControlCore,true);

                    XLog.Write("导入文件\"" + strFilePath + "\"成功");
                }
            }
        }

        /// <summary>
        /// 导出重心数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportCoreData_Click(object sender, EventArgs e)
        {
            if (treeCoreEnvelope.Nodes.Count == 0)
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
                    List<string> lstContent = MainForm.GetCoreEnvelopeDesignFileContent(gridCoreEnvelope);
                    CommonFunction.mWriteListStringToFile(strFilePath, lstContent);
                }
                if (strFilePath.EndsWith(".xls"))
                {
                    DataTable table = MainForm.GetCoreEnvelopeDesignResultTable(gridCoreEnvelope);
                    int result = XCommon.FileStatus.FileIsOpen(strFilePath);

                    if (result == 1)
                    {
                        MessageBox.Show("请关闭文件" + strFilePath + ",再导出");
                    }
                    else
                    {
                        CommonExcel commonExcel = new CommonExcel();
                        commonExcel.lstColumn = MainForm.GetCoreEnvelopeDesignResultExcleCloumn();
                        commonExcel.CreateExcel("example", strFilePath, "sheet1", table);
                    }
                }
                XLog.Write("导出文件\"" + strFilePath + "\"成功");
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeViewList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selNode = e.Node;

            SettingPageData();
        }

        #endregion
    }
}
