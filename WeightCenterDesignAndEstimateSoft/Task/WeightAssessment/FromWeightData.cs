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

namespace WeightCenterDesignAndEstimateSoft.Task.WeightAssessment
{
    public partial class FromWeightData : Form
    {
        public MainForm mainForm;
        private int flag;
        private List<TypeWeightData> lstTypeWeight;
        private List<WeightDesignData> lstWeightDesign;

        /// <summary>
        /// 依据flag的值生成不同的UI界面.
        /// </summary>
        /// <param name="flag">0:当前重量设计数据导入;1当前调整设计数据导入;</param>
        public FromWeightData(int flag)
        {
            InitializeComponent();
            this.flag = flag;
        }

        private void FromCurrentWeightData_Load(object sender, EventArgs e)
        {
            mainForm = ((WeightAssessmentForm)this.Owner).mainForm;
            if (flag == 0)
            {
                this.Text = "基准重量设计数据  当前重量设计数据";
                refreshTreeView(mainForm.designProjectData.lstWeightArithmetic);
            }
            else if (flag == 1)
            {
                this.Text = "基准重量数据  当前重量调整数据";
                this.refreshTreeView(mainForm.designProjectData.lstAdjustmentResultData);
            }
            else if (flag == 2)
            {
                this.Text = "基准重量数据  型号重量数据库数据";
                this.refreshDB.Visible = true;
                BLLTypeWeightData bllTypeWeightData = new BLLTypeWeightData();
                lstTypeWeight = bllTypeWeightData.GetListModel();
                this.refreshTreeView(lstTypeWeight);
            }
            else if (flag == 3)
            {
                this.Text = "基准重量数据  设计重量数据库数据";
                this.refreshDB.Visible = true;
                BLLWeightDesignData bllWeightDesign = new BLLWeightDesignData();
                lstWeightDesign = bllWeightDesign.GetListModel();
                this.refreshTreeView(lstWeightDesign);
            }
        }

        /// <summary>
        /// 确认按钮单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.weightResultTree.SelectedNode == null)
            {
                return;
            }
            int i  = Convert.ToInt16(this.weightResultTree.SelectedNode.ToolTipText);
            if (flag == 0)
            {
                WeightArithmetic wa = mainForm.designProjectData.lstWeightArithmetic[i];
                ((WeightAssessmentForm)this.Owner).saveWeightDataGridView(wa.ExportDataToWeightSort(),1);
            }
            else if (flag == 1)
            {
                WeightAdjustmentResultData ward = mainForm.designProjectData.lstAdjustmentResultData[i];
                ((WeightAssessmentForm)this.Owner).saveWeightDataGridView(ward.weightAdjustData,1);
            }
            else if (flag == 2)
            {
                WeightSortData wsd=WeightSortData.clsStringToWeightSortData(lstTypeWeight[i].MainSystem_Name);
                ((WeightAssessmentForm)this.Owner).saveWeightDataGridView(wsd,1);
            }
            else if (flag == 3)
            {
                WeightSortData wsd = WeightSortData.clsStringToWeightSortData(lstWeightDesign[i].MainSystem_Name);
                ((WeightAssessmentForm)this.Owner).saveWeightDataGridView(wsd,1);
            }
            this.Close();
        }

        /// <summary>
        /// 取消按钮单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 自定义方法

        /// <summary>
        /// 使用当前重量数据刷新树结点
        /// </summary>
        /// <param name="walist"></param>
        private void refreshTreeView(List<WeightArithmetic> walist)
        {
            //重量设计结果列表
            if (walist != null && walist.Count > 0)
            {
                TreeNode parentNode = new TreeNode("重量设计结果列表");
                this.weightResultTree.Nodes.Add(parentNode);
                for (int i = 0; i < walist.Count; i++)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Name = walist[i].Name;
                    childNode.Text = walist[i].DataName;
                    childNode.ToolTipText = i.ToString();
                    parentNode.Nodes.Add(childNode);
                }
                this.weightResultTree.ExpandAll();
            }
        }

        /// <summary>
        /// 使用当前调整数据刷新树结点
        /// </summary>
        /// <param name="walist"></param>
        private void refreshTreeView(List<WeightAdjustmentResultData> wardlist)
        {
            //重量设计结果列表
            if (wardlist != null && wardlist.Count > 0)
            {
                TreeNode parentNode = new TreeNode("重量调整结果列表");
                this.weightResultTree.Nodes.Add(parentNode);
                for (int i = 0; i < wardlist.Count; i++)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Name = wardlist[i].WeightAdjustName;
                    childNode.Text = wardlist[i].WeightAdjustName;
                    childNode.ToolTipText = i.ToString();
                    parentNode.Nodes.Add(childNode);
                }
                this.weightResultTree.ExpandAll();
            }
        }

        /// <summary>
        /// 使用型号数据库刷新树结点
        /// </summary>
        /// <param name="lstTypeWeight"></param>
        private void refreshTreeView(List<TypeWeightData> lstTypeWeight)
        {
            if (lstTypeWeight != null && lstTypeWeight.Count > 0)
            {
                TreeNode nodeRoot = new TreeNode("型号重量数据列表");
                weightResultTree.Nodes.Add(nodeRoot);
                for (int i = 0; i < lstTypeWeight.Count; i++)
                {
                    TreeNode node = new TreeNode();
                    node.Name = lstTypeWeight[i].Id.ToString();
                    node.Text = lstTypeWeight[i].Helicopter_Name;
                    node.ToolTipText = i.ToString();
                    nodeRoot.Nodes.Add(node);
                }
                nodeRoot.ExpandAll();
            }
        }

        private void refreshTreeView(List<WeightDesignData> lstWeightDesign)
        {
            if (lstWeightDesign != null && lstWeightDesign.Count > 0)
            {
                TreeNode nodeRoot = new TreeNode("设计重量数据列表");
                weightResultTree.Nodes.Add(nodeRoot);
                for (int i = 0; i < lstWeightDesign.Count; i++)
                {
                    TreeNode node = new TreeNode();
                    node.Name = lstWeightDesign[i].Id.ToString();
                    node.Text = lstWeightDesign[i].Helicopter_Name;
                    node.ToolTipText = i.ToString();
                    nodeRoot.Nodes.Add(node);
                }
                nodeRoot.ExpandAll();
            }
        }
        
        #endregion

    }
}
