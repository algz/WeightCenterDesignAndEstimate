using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using Model.assessData;

namespace WeightCenterDesignAndEstimateSoft.Task.CoreEnvelopeAssessment
{
    public partial class FromCoreData : Form
    {
        private int flag;
        private MainForm mainForm;
        private List<CoreEnvelopeArithmetic> lstCoreEnvelopeDesign;//重心包心设计对象
        private List<CoreEnvelopeCutResultData> lstCutResultData;//重心包线剪裁对象
        private List<Model.CoreEnvelopeDesign> lstCoreEnvelope;//重心包线数据库对象

        /// <summary>
        /// 依据flag的值生成不同的UI界面.
        /// </summary>
        /// <param name="flag">0:当前重心设计数据导入;1当前重心调整数据导入;2从重心数据库导入</param>
        public FromCoreData(int flag)
        {
            InitializeComponent();
            this.flag = flag;
        }

        private void FromCoreData_Load(object sender, EventArgs e)
        {
            this.mainForm = ((CoreEnvelopeAssessForm)this.Owner).mainForm;


            this.loadTreeView(this.flag);
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.treeCoreEnvelope.SelectedNode == null)
            {
                MessageBox.Show("请选择结点");
                return;
            }
            CoreEnvelopeAssessForm parentForm=((CoreEnvelopeAssessForm)this.Owner);
            TreeNode selNode = this.treeCoreEnvelope.SelectedNode;
            
            if (flag == 0)
            {
                //从重心设计中加载数据
                for (int i = 0; i < lstCoreEnvelopeDesign.Count; i++)
                {
                    if (selNode.ToolTipText == i.ToString())
                    {
                        List<CorePointExt> cpeList = new List<CorePointExt>();
                        foreach (NodeFormula nf in lstCoreEnvelopeDesign[i].FormulaList)
                        {
                            CorePointExt cpe = new CorePointExt();
                            cpe.pointName = nf.NodeName;
                            cpe.pointXValue = nf.XFormula.Value;
                            cpe.pointYValue = nf.YFormula.Value;
                            cpe.isAssess = false;
                            cpeList.Add(cpe);
                        }
                        parentForm.saveCoreGridView(cpeList, "0");
                        break;
                    }
                }
            }
            else if (flag == 1)
            {
                //从重心裁剪中加载数据
                for (int i = 0; i < lstCutResultData.Count; i++)
                {
                    if (selNode.ToolTipText == i.ToString())
                    {
                        List<CorePointExt> cpeList = new List<CorePointExt>();
                        foreach (CorePointData cpd in lstCutResultData[i].lstCutEnvelopeCore)
                        {
                            cpeList.Add(CommonUtil.corePointDataToCorePoinExt(cpd));
                        }
                        parentForm.saveCoreGridView(cpeList, "0");
                        break;
                    }
                }
            }
            else if (flag == 2)
            {
                //从重心数据库中加载数据
                foreach (Model.CoreEnvelopeDesign core in this.lstCoreEnvelope)
                {
                    if (core.Id.ToString() == selNode.Name)
                    {
                        List<CorePointExt> cpeList=new List<CorePointExt>();
                        List<CorePointData> list=CoreEnvelopeDesign.GetStringToListCorePointData(core.CoreEnvelope);
                        foreach (CorePointData cpd in list)
                        {
                            cpeList.Add(CommonUtil.corePointDataToCorePoinExt(cpd));
                        }
                        parentForm.saveCoreGridView(cpeList, "0");
                        break;
                    }
                }
            }

            this.Close();
        }

        #region 自定义方法

        private void loadTreeView(int flag)
        {
            //当前重心包线设计
            if (flag == 0)
            {
                if (mainForm.designProjectData.lstCoreEnvelopeDesign != null && mainForm.designProjectData.lstCoreEnvelopeDesign.Count > 0)
                {
                    this.lstCoreEnvelopeDesign = mainForm.designProjectData.lstCoreEnvelopeDesign;
                    if (lstCoreEnvelopeDesign != null && lstCoreEnvelopeDesign.Count > 0)
                    {
                        TreeNode node = new TreeNode("重心包线设计结果列表");
                        this.treeCoreEnvelope.Nodes.Add(node);

                        for (int i = 0; i < lstCoreEnvelopeDesign.Count; i++)
                        {
                            TreeNode childNode = new TreeNode();
                            childNode.Name = lstCoreEnvelopeDesign[i].Name;
                            childNode.Text = lstCoreEnvelopeDesign[i].DataName;
                            childNode.ToolTipText = i.ToString();
                            node.Nodes.Add(childNode);
                        }
                        node.Expand();
                    }
                }
            }else if (flag == 1)
            {
                if (mainForm.designProjectData.lstCutResultData != null && mainForm.designProjectData.lstCutResultData.Count > 0)
                {

                    this.lstCutResultData = mainForm.designProjectData.lstCutResultData;
                    if (lstCutResultData != null && lstCutResultData.Count > 0)
                    {
                        TreeNode node = new TreeNode("重心包线剪裁结果列表");
                        this.treeCoreEnvelope.Nodes.Add(node);

                        for (int i = 0; i < lstCutResultData.Count; i++)
                        {
                            TreeNode childNode = new TreeNode();
                            childNode.Name = lstCutResultData[i].cutResultName;
                            childNode.Text = lstCutResultData[i].cutResultName;
                            childNode.ToolTipText = i.ToString();
                            node.Nodes.Add(childNode);
                        }
                        node.Expand();
                    }
                }
            }else if (flag == 2)//数据库
            {
                //开启刷新数据库按钮功能
                this.refreshDB.Visible = true;

                this.refreshDB.PerformClick();
            }
        }

        #endregion

        private void refreshDB_Click(object sender, EventArgs e)
        {
            BLL.BLLCoreEnvelopeDesign coreBll = new BLL.BLLCoreEnvelopeDesign();
            this.lstCoreEnvelope = coreBll.GetListModel();

            if (lstCoreEnvelope != null && lstCoreEnvelope.Count > 0)
            {
                TreeNode node = new TreeNode("重心包线数据列表");
                this.treeCoreEnvelope.Nodes.Add(node);

                foreach (Model.CoreEnvelopeDesign core in lstCoreEnvelope)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Name = core.Id.ToString();
                    childNode.Text = core.DesignData_Name;
                    node.Nodes.Add(childNode);
                }
                node.Expand();
            }
        }

        private void treeCoreEnvelope_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            btnConfirm_Click(sender, e);
        }

    }
}
