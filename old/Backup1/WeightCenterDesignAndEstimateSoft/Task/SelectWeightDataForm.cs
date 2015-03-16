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

namespace WeightCenterDesignAndEstimateSoft.Task.WeightAdjustmentSubforms
{
    public partial class SelectWeightDataForm : Form
    {

        #region 属性

        /// <summary>
        /// 标题
        /// </summary>
        private string grpText = string.Empty;

        private DesignProjectData designProject = null;

        private List<TypeWeightData> lstTypeWeight = null;
        private List<WeightDesignData> lstWeightDesign = null;

        private TreeNode selNode = null;

        private WeightAdjustmentForm form = null;

        public SelectWeightDataForm(WeightAdjustmentForm _form, string _grpText, bool btnRefresh)
        {
            InitializeComponent();

            this.grpBx_WeightDataList.Text = grpText;
            this.btn_Refresh.Visible = btnRefresh;
            grpText = _grpText;
            form = _form;

            if (grpText == "型号重量数据列表")
            {
                BLLTypeWeightData bllTypeWeightData = new BLLTypeWeightData();
                lstTypeWeight = bllTypeWeightData.GetListModel();
            }
            if (grpText == "重量设计数据列表")
            {
                BLLWeightDesignData bllWeightDesign = new BLLWeightDesignData();
                lstWeightDesign = bllWeightDesign.GetListModel();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_form"></param>
        /// <param name="_designProject"></param>
        /// <param name="_grpText"></param>
        /// <param name="btnRefresh">是否显示刷新按钮</param>
        public SelectWeightDataForm(WeightAdjustmentForm _form, DesignProjectData _designProject, string _grpText, bool btnRefresh)
        {
            InitializeComponent();

            this.grpBx_WeightDataList.Text = grpText;
            this.btn_Refresh.Visible = btnRefresh;
            designProject = _designProject;
            grpText = _grpText;
            form = _form;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 自定义方法

        private void BindListData()
        {
            treeViewWeightData.Nodes.Clear();

            //当前重量设计数据
            if (grpText == "重量设计结果列表")
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
            //当前重量调整结果
            if (grpText == "重量调整结果列表")
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
            if (grpText == "型号重量数据列表")
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
            if (grpText == "重量设计数据列表")
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

        #endregion

        #region  事件处理

        private void SelectWeightDataForm_Load(object sender, EventArgs e)
        {
            BindListData();
        }

        private void treeViewWeightData_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;

        }

        private void treeViewWeightData_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            selNode = e.Node;
            treeViewWeightData.SelectedNode = e.Node;
        }

        /// <summary>
        /// 导入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ImptCrtWghtDsgnRst_Click(object sender, EventArgs e)
        {
            if (selNode == null || selNode.Level == 0)
            {
                MessageBox.Show("请选择数据");
                return;
            }

            //绑定列表数据
            WeightSortData sortData = GetWeightSortData();
            if (sortData == null || sortData.lstWeightData.Count == 0)
            {
                MessageBox.Show("没有重量数据,不能导入");
                return;
            }


            if ((form.lstJHRatio != null && form.lstJHRatio.Count > 0)
                || (form.lstTeachRatio != null && form.lstTeachRatio.Count > 0))
            {
                //判断导入基础重量分类一致
                if (WeightSortData.blIsSame(form.basicWeightData, sortData) == false)
                {

                    DialogResult result = MessageBox.Show("当前选择会清空当前数据?", "继续提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        form.InitializeData();

                        form.BindWeightData(sortData);

                        List<WeightData> lstBasicWeightData = WeightSortData.GetListWeightData(form.basicWeightData);
                        form.SetBasicWeightData(lstBasicWeightData);
                        form.SetRatioGridView(lstBasicWeightData);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    form.InitializeData();
                    form.BindWeightData(sortData);
                    List<WeightData> lstWeightData = WeightSortData.GetListWeightData(sortData);
                    form.SetBasicWeightData(lstWeightData);
                    form.SetRatioGridView(lstWeightData);
                }
            }
            else
            {
                //设置基础重量数据
                form.BindWeightData(sortData);
                List<WeightData> lstWeightData = WeightSortData.GetListWeightData(sortData);
                form.SetBasicWeightData(lstWeightData);
                form.SetRatioGridView(lstWeightData);
            }

            this.Close();
        }

        #endregion
    }
}
