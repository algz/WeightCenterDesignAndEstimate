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
using BLL;

namespace WeightCenterDesignAndEstimateSoft.Tool
{
    public partial class ImportWeightDataListForm : Form
    {
        #region 属性

        private ComputeCorrectionFactorFrm form = null;
        private DesignProjectData designProject = null;
        private string strImportType = string.Empty;

        private TreeNode selNode = null;

        private List<TypeWeightData> lstTypeWeight = null;
        List<WeightDesignData> lstWeightDesign = null;

        public ImportWeightDataListForm()
        {
            InitializeComponent();
        }

        public ImportWeightDataListForm(ComputeCorrectionFactorFrm _form, DesignProjectData _designProject, string str_ImportType)
        {
            InitializeComponent();

            form = _form;
            designProject = _designProject;
            strImportType = str_ImportType;
        }

        public ImportWeightDataListForm(ComputeCorrectionFactorFrm _form, List<TypeWeightData> lst_TypeWeight, string str_ImportType)
        {
            InitializeComponent();

            form = _form;
            lstTypeWeight = lst_TypeWeight;
            strImportType = str_ImportType;
        }

        public ImportWeightDataListForm(ComputeCorrectionFactorFrm _form, List<WeightDesignData> lst_WeightDesign, string str_ImportType)
        {
            InitializeComponent();

            form = _form;
            lstWeightDesign = lst_WeightDesign;
            strImportType = str_ImportType;
        }

        #endregion

        #region 自定义方法

        private void BindListData()
        {
            treeViewWeightData.Nodes.Clear();

            //当前重量设计数据
            if (strImportType == "weightDesign")
            {
                if (designProject.lstWeightArithmetic != null && designProject.lstWeightArithmetic.Count > 0)
                {
                    TreeNode nodeRoot = new TreeNode("重量设计结果列表");
                    treeViewWeightData.Nodes.Add(nodeRoot);
                    for (int i = 0; i < designProject.lstWeightArithmetic.Count; i++)
                    {
                        TreeNode node = new TreeNode();
                        node.Name = designProject.lstWeightArithmetic[i].Name;
                        node.Text = designProject.lstWeightArithmetic[i].DataName;
                        node.ToolTipText = i.ToString();
                        nodeRoot.Nodes.Add(node);
                    }
                    nodeRoot.ExpandAll();
                }
            }

            //重量调整结果列表
            if (strImportType == "weightAdjust")
            {
                if (designProject.lstAdjustmentResultData != null && designProject.lstAdjustmentResultData.Count > 0)
                {
                    TreeNode nodeRoot = new TreeNode("重量调整结果列表");
                    treeViewWeightData.Nodes.Add(nodeRoot);
                    for (int i = 0; i < designProject.lstAdjustmentResultData.Count; i++)
                    {
                        TreeNode node = new TreeNode();
                        node.Name = designProject.lstAdjustmentResultData[i].WeightAdjustName;
                        node.Text = designProject.lstAdjustmentResultData[i].WeightAdjustName;
                        node.ToolTipText = i.ToString();
                        nodeRoot.Nodes.Add(node);
                    }
                    nodeRoot.ExpandAll();
                }
            }

            //型号重量数据库
            if (strImportType == "typeDB")
            {
                if (lstTypeWeight != null && lstTypeWeight.Count > 0)
                {
                    TreeNode nodeRoot = new TreeNode("型号重量数据列表");
                    treeViewWeightData.Nodes.Add(nodeRoot);
                    for (int i = 0; i < lstTypeWeight.Count; i++)
                    {
                        TreeNode node = new TreeNode();
                        node.Name = lstTypeWeight[i].Id.ToString();
                        node.Text = lstTypeWeight[i].Helicopter_Name;
                        nodeRoot.Nodes.Add(node);
                    }
                    nodeRoot.ExpandAll();
                }
            }

            //重量设计数据库
            if (strImportType == "weightDB")
            {
                if (lstWeightDesign != null && lstWeightDesign.Count > 0)
                {
                    TreeNode nodeRoot = new TreeNode("重量设计数据列表");
                    treeViewWeightData.Nodes.Add(nodeRoot);
                    for (int i = 0; i < lstWeightDesign.Count; i++)
                    {
                        TreeNode node = new TreeNode();
                        node.Name = lstWeightDesign[i].Id.ToString();
                        node.Text = lstWeightDesign[i].DesignData_Name;
                        nodeRoot.Nodes.Add(node);
                    }
                    nodeRoot.ExpandAll();
                }
            }

        }

        /// <summary>
        /// 获取重量分类
        /// </summary>
        /// <returns></returns>
        private WeightSortData GetWeightSortData()
        {
            WeightSortData sortData = null;

            //当前重量设计数据
            if (selNode.Parent.Text == "重量设计结果列表")
            {
                for (int i = 0; i < designProject.lstWeightArithmetic.Count; i++)
                {
                    if (selNode.ToolTipText == i.ToString())
                    {
                        sortData = designProject.lstWeightArithmetic[i].ExportDataToWeightSort();
                    }
                }
            }
            //重量调整结果数据
            if (selNode.Parent.Text == "重量调整结果列表")
            {
                for (int i = 0; i < designProject.lstAdjustmentResultData.Count; i++)
                {
                    if (selNode.ToolTipText == i.ToString())
                    {
                        sortData = designProject.lstAdjustmentResultData[i].weightAdjustData;
                    }
                }
            }

            //型号重量数据库
            if (selNode.Parent.Text == "型号重量数据列表")
            {

                for (int i = 0; i < lstTypeWeight.Count; i++)
                {
                    if (selNode.Name == lstTypeWeight[i].Id.ToString())
                    {
                        sortData = WeightSortData.clsStringToWeightSortData(lstTypeWeight[i].MainSystem_Name);
                    }
                }
            }

            //重量设计数据库
            if (selNode.Parent.Text == "重量设计数据列表")
            {

                for (int i = 0; i < lstWeightDesign.Count; i++)
                {
                    if (selNode.Name == lstWeightDesign[i].Id.ToString())
                    {
                        sortData = WeightSortData.clsStringToWeightSortData(lstWeightDesign[i].MainSystem_Name);
                    }
                }
            }

            return sortData;
        }

        /// <summary>
        /// 设置窗体标题
        /// </summary>
        private void SetFormText()
        {
            string strTitle = string.Empty;

            if (form.strWeightDataType == "data1")
            {
                strTitle = "重量数据1";
            }

            if (form.strWeightDataType == "data2")
            {
                strTitle = "重量数据2";
            }
            if (strImportType == "weightDesign")
            {
                this.Text = strTitle + " 当前重量设计数据";
                btnRefush.Visible = false;
            }
            if (strImportType == "typeDB")
            {
                this.Text = strTitle + " 型号重量数据库数据";
                btnRefush.Visible = true;
            }
            if (strImportType == "weighteDB")
            {
                this.Text = strTitle + " 型重量设计数据库数据";
                btnRefush.Visible = true;
            }
            if (strImportType == "weightAdjust")
            {
                this.Text = strTitle + " 当前重量调整数据";
                btnRefush.Visible = false;
            }
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 载入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportWeightDataListForm_Load(object sender, EventArgs e)
        {
            SetFormText();
            BindListData();
        }

        /// <summary>
        /// 确定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (selNode == null || selNode.Level == 0)
            {
                XCommon.XLog.Write("请选择数据");
                MessageBox.Show("请选择数据");
                return;
            }

            WeightSortData sortData = GetWeightSortData();
            List<WeightData> lstWeightData = form.GetListWeightData(sortData);

            if (sortData == null || sortData.lstWeightData.Count == 0)
            {
                MessageBox.Show("导入该条数据为空");
                return;
            }

            if (form.strWeightDataType == "data1")
            {
                if (form.importSortData2 != null)
                {
                    DialogResult result = MessageBox.Show("继续导入会清空所有数据?", "继续提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        form.BindWeightData(sortData);
                        form.SetGridViewData(form.strWeightDataType, lstWeightData, true, false);
                        form.importSortData1 = sortData;
                        form.importSortData2 = null;
                        form.lstCalculateRatio = null;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    form.BindWeightData(sortData);
                    form.SetGridViewData(form.strWeightDataType, lstWeightData, false, false);
                    form.importSortData1 = sortData;
                }
                form.InitializezedGraphControl();
            }

            //重量分类是否一致
            if (form.strWeightDataType == "data2")
            {
                bool IsSame = WeightSortData.blIsSame(form.importSortData1, sortData);

                if (IsSame == false)
                {
                    XLog.Write("导入数据和重量数据1重量分类不一致");
                    MessageBox.Show("导入数据和重量数据1重量分类不一致");
                    return;
                }

                if (form.lstCalculateRatio != null && form.lstCalculateRatio.Count > 0)
                {
                    DialogResult result = MessageBox.Show("继续导入会清空因子数据?", "继续提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        form.lstCalculateRatio = null;
                        form.importSortData2 = sortData;
                        form.SetGridViewData(form.strWeightDataType, lstWeightData, false, true);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    form.importSortData2 = sortData;
                    form.SetGridViewData(form.strWeightDataType, lstWeightData, false, false);
                }
                form.InitializezedGraphControl();
            }
            XLog.Write("导入数据成功");

            this.Close();
        }

        private void treeViewWeightData_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeViewWeightData.SelectedNode = e.Node;
            selNode = e.Node;
        }

        private void treeViewWeightData_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 刷新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefush_Click(object sender, EventArgs e)
        {
            if (strImportType == "typeDB")
            {
                BLLTypeWeightData bllTypeData = new BLLTypeWeightData();
                lstTypeWeight = bllTypeData.GetListModel();
            }
            if (strImportType == "weighteDB")
            {
                BLLWeightDesignData bllWeightDesign = new BLLWeightDesignData();
                lstWeightDesign = bllWeightDesign.GetListModel();
            }

            BindListData();
            selNode = null;
            treeViewWeightData.SelectedNode = null;
        }

        #endregion

        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewWeightData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (selNode == null || selNode.Level == 0)
            {
                XCommon.XLog.Write("请选择数据");
                MessageBox.Show("请选择数据");
                return;
            }

            WeightSortData sortData = GetWeightSortData();
            List<WeightData> lstWeightData = form.GetListWeightData(sortData);

            if (sortData == null || sortData.lstWeightData.Count == 0)
            {
                MessageBox.Show("导入该条数据为空");
                return;
            }

            if (form.strWeightDataType == "data1")
            {
                if (form.importSortData2 != null)
                {
                    DialogResult result = MessageBox.Show("继续导入会清空所有数据?", "继续提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        form.BindWeightData(sortData);
                        form.SetGridViewData(form.strWeightDataType, lstWeightData, true, false);
                        form.importSortData1 = sortData;
                        form.importSortData2 = null;
                        form.lstCalculateRatio = null;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    form.BindWeightData(sortData);
                    form.SetGridViewData(form.strWeightDataType, lstWeightData, false, false);
                    form.importSortData1 = sortData;
                }
                form.InitializezedGraphControl();
            }

            //重量分类是否一致
            if (form.strWeightDataType == "data2")
            {
                bool IsSame = WeightSortData.blIsSame(form.importSortData1, sortData);

                if (IsSame == false)
                {
                    XLog.Write("导入数据和重量数据1重量分类不一致");
                    MessageBox.Show("导入数据和重量数据1重量分类不一致");
                    return;
                }

                if (form.lstCalculateRatio != null && form.lstCalculateRatio.Count > 0)
                {
                    DialogResult result = MessageBox.Show("继续导入会清空因子数据?", "继续提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        form.lstCalculateRatio = null;
                        form.importSortData2 = sortData;
                        form.SetGridViewData(form.strWeightDataType, lstWeightData, false, true);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    form.importSortData2 = sortData;
                    form.SetGridViewData(form.strWeightDataType, lstWeightData, false, false);
                }
                form.InitializezedGraphControl();
            }
            XLog.Write("导入数据成功");

            this.Close();
        }

        private void treeViewWeightData_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selNode = e.Node;
        }


    }
}
