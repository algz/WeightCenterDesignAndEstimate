using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WeightCenterDesignAndEstimateSoft.Task
{
    public partial class SelectCoreCutForm : Form
    {
        private List<Model.CoreEnvelopeDesign> lstCoreEnvelope = null;
        private List<Model.CoreEnvelopeArithmetic> lstCoreEnvelopeDesign = null;
        private List<Model.CoreEnvelopeCutResultData> lstCutResultData = null;

        private CoreEnvelopeCutForm coreForm = null;
        private TreeNode selNode = null;
        private MainForm mainForm = null;

        private string strType = string.Empty;

        public SelectCoreCutForm()
        {
            InitializeComponent();
        }

        public SelectCoreCutForm(CoreEnvelopeCutForm core_Form, string str_Type)
        {
            InitializeComponent();

            strType = str_Type;
            coreForm = core_Form;
            BindCoreData();
            SetTitle();
        }

        public SelectCoreCutForm(CoreEnvelopeCutForm core_Form, MainForm main_Form, string str_Type)
        {
            InitializeComponent();

            strType = str_Type;
            coreForm = core_Form;
            mainForm = main_Form;

            BindCoreData();
        }


        /// <summary>
        /// 绑定重心包线数据
        /// </summary>
        private void BindCoreData()
        {
            //数据库
            if (strType == "DB")
            {
                BLL.BLLCoreEnvelopeDesign coreBll = new BLL.BLLCoreEnvelopeDesign();
                lstCoreEnvelope = coreBll.GetListModel();

                if (lstCoreEnvelope != null && lstCoreEnvelope.Count > 0)
                {
                    TreeNode node = new TreeNode("重心包线数据列表");
                    treeViewCoreData.Nodes.Add(node);

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
            //当前重心包线设计
            if (strType == "CoreEnvelope")
            {
                if (mainForm.designProjectData.lstCoreEnvelopeDesign != null && mainForm.designProjectData.lstCoreEnvelopeDesign.Count > 0)
                {
                    lstCoreEnvelopeDesign = mainForm.designProjectData.lstCoreEnvelopeDesign;
                    if (lstCoreEnvelopeDesign != null && lstCoreEnvelopeDesign.Count > 0)
                    {
                        TreeNode node = new TreeNode("重心包线设计结果列表");
                        treeViewCoreData.Nodes.Add(node);

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
            }
            if (strType == "CoreCut")
            {
                if (mainForm.designProjectData.lstCutResultData != null && mainForm.designProjectData.lstCutResultData.Count > 0)
                {

                    lstCutResultData = mainForm.designProjectData.lstCutResultData;
                    if (lstCutResultData != null && lstCutResultData.Count > 0)
                    {
                        TreeNode node = new TreeNode("重心包线剪裁结果列表");
                        treeViewCoreData.Nodes.Add(node);

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
            }

        }

        private void SetTitle()
        {
            if (strType == "CoreEnvelope")
            {
                this.Text = "选择当前重心包线数据";
            }
            if (strType == "DB")
            {
                btn_Refresh.Visible = true;
                this.Text = "选择重心包线数据库数据";
            }
            if (strType == "CoreCut")
            {
                this.Text = "选择当前重心包线剪裁数据";
            }

        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            BindCoreData();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
                MessageBox.Show("请选择重心包线数据");
                return;
            }

            if (strType == "DB")
            {
                foreach (Model.CoreEnvelopeDesign core in lstCoreEnvelope)
                {
                    if (core.Id.ToString() == selNode.Name)
                    {
                        coreForm.coreEnvelope = core;
                    }
                }
            }

            //当前重心包线设计
            if (strType == "CoreEnvelope")
            {
                for (int i = 0; i < lstCoreEnvelopeDesign.Count; i++)
                {
                    if (selNode.ToolTipText == i.ToString())
                    {
                        coreForm.coreEnvelopeDesign = lstCoreEnvelopeDesign[i];
                    }
                }
            }
            //当前重心包线剪裁
            if (strType == "CoreCut")
            {
                for (int i = 0; i < lstCutResultData.Count; i++)
                {
                    if (selNode.ToolTipText == i.ToString())
                    {
                        coreForm.coreEnvelopeCut = lstCutResultData[i];
                    }
                }
            }

            this.Close();
        }

        private void treeViewCoreData_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selNode = e.Node;
        }
    }
}
