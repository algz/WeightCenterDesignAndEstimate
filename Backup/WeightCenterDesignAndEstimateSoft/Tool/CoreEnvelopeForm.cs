using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XCommon;
using Model;
using System.Windows.Forms.DataVisualization.Charting;
using ZedGraph;

namespace WeightCenterDesignAndEstimateSoft.Tool
{
    public partial class CoreEnvelopeForm : Form
    {
        #region 属性

        private TreeNode selNode = null;
        CoreEnvelopeDesignManageForm form = null;

        private List<CorePointData> lstCorePointData = null;

        private ZedGraphControl zedGraphControlCore = null;
        // private Chart chartEnvelope;

        public CoreEnvelopeForm()
        {
            InitializeComponent();
        }

        //public CoreEnvelopeForm(CoreEnvelopeDesignManageForm _form, List<CorePointData> lstCorePt, Chart _chartEnvelope)                                                                                                                            
        //{
        //    InitializeComponent();
        //    form = _form;
        //    chartEnvelope = _chartEnvelope;
        //    BindTreeList(lstCorePt);
        //}

        public CoreEnvelopeForm(CoreEnvelopeDesignManageForm _form, List<CorePointData> lstCorePt, ZedGraphControl zedGraphControl_Core)
        {
            InitializeComponent();
            form = _form;
            zedGraphControlCore = zedGraphControl_Core;

            BindTreeList(lstCorePt);
        }

        #endregion

        #region 自定义方法

        private void BindTreeList(List<CorePointData> lstTempCorePointData)
        {
            treeViewList.Nodes.Clear();

            if (lstTempCorePointData != null && lstTempCorePointData.Count > 0)
            {
                lstCorePointData = new List<CorePointData>();
                foreach (CorePointData data in lstTempCorePointData)
                {
                    lstCorePointData.Add(data);
                }
            }

            if (lstCorePointData != null && lstCorePointData.Count > 0)
            {
                TreeNode node = new TreeNode("重心包线");
                treeViewList.Nodes.Add(node);

                foreach (CorePointData data in lstCorePointData)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Name = data.pointName;
                    childNode.Text = data.pointName;
                    childNode.ToolTipText = data.pointXValue.ToString() + "," + data.pointYValue.ToString();
                    node.Nodes.Add(childNode);
                }
                node.ExpandAll();
            }
        }

        private List<CorePointData> GetCurrentCorePointData()
        {
            List<CorePointData> lstCorePtData = new List<CorePointData>();

            if (treeViewList.Nodes.Count > 0)
            {
                TreeNode node = treeViewList.Nodes[0];
                foreach (TreeNode nodeChild in node.Nodes)
                {
                    foreach (CorePointData data in lstCorePointData)
                    {
                        if (nodeChild.Text == data.pointName)
                        {
                            lstCorePtData.Add(data);
                            break;
                        }
                    }
                }
            }

            return lstCorePtData;
        }

        private List<string> GetListNodeName()
        {
            List<string> lstNodeName = new List<string>();

            if (treeViewList.Nodes.Count > 0)
            {
                TreeNode node = treeViewList.Nodes[0];
                foreach (TreeNode childNode in node.Nodes)
                {
                    lstNodeName.Add(childNode.Text);
                }
            }
            return lstNodeName;
        }

        private string PageVerificationInfo()
        {
            string strErroInfo = string.Empty;

            if (txtPtName.Text == string.Empty)
            {
                strErroInfo = "请输入坐标点名称";
                return strErroInfo;
            }
            else
            {
                if (Verification.IsCheckString(txtPtName.Text))
                {
                    strErroInfo = "坐标点名称不能输入非法字符";
                    return strErroInfo;
                }
            }

            return strErroInfo;
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNode_Click(object sender, EventArgs e)
        {
            string strPtName = txtPtName.Text;
            string strErroInfo = PageVerificationInfo();
            if (strErroInfo != string.Empty)
            {
                XLog.Write(strErroInfo);
                return;
            }
            else
            {
                bool IsExit = false;
                if (lstCorePointData != null && lstCorePointData.Count > 0)
                {
                    foreach (CorePointData data in lstCorePointData)
                    {
                        if (data.pointName == strPtName)
                        {
                            XLog.Write("坐标点名称:" + strPtName + "已存在");
                            IsExit = true;
                            return;
                        }
                    }
                }
                if (IsExit == false)
                {
                    if (lstCorePointData == null)
                    {
                        lstCorePointData = new List<CorePointData>();
                    }
                    CorePointData pt = new CorePointData();
                    pt.pointName = strPtName;
                    lstCorePointData.Add(pt);
                }
                BindTreeList(lstCorePointData);
            }
        }

        /// <summary>
        /// 上移事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpNode_Click(object sender, EventArgs e)
        {
            if (selNode == null || selNode.Level == 0)
            {
                XLog.Write("请选择节点");
                return;
            }

            TreeNode node = selNode.Parent;

            //判断第一个子节点
            if (selNode == node.FirstNode)
            {
                return;
            }

            for (int j = 0; j < node.Nodes.Count; j++)
            {
                if (selNode == node.Nodes[j])
                {
                    TreeNode preNode = node.Nodes[j - 1];
                    node.Nodes.Remove(preNode);
                    node.Nodes.Remove(selNode);

                    node.Nodes.Insert(j - 1, selNode);
                    node.Nodes.Insert(j, preNode);

                    treeViewList.SelectedNode = node.Nodes[j - 1];

                    return;
                }
            }
        }

        /// <summary>
        /// 下移事件
        /// </summary>
        private void btnDownNode_Click(object sender, EventArgs e)
        {
            if (selNode == null || selNode.Level == 0)
            {
                XCommon.XLog.Write("请选择节点");
                return;
                /// <param name="sender"></param>
                /// <param name="e"></param>
            }

            TreeNode node = treeViewList.Nodes[0];

            //判断最后一个子节点
            if (selNode == selNode.Parent.LastNode)
            {
                return;
            }

            for (int j = 0; j < node.Nodes.Count; j++)
            {
                if (selNode == node.Nodes[j])
                {
                    TreeNode nextNode = node.Nodes[j + 1];
                    node.Nodes.Remove(selNode);
                    node.Nodes.Remove(nextNode);

                    node.Nodes.Insert(j, nextNode);
                    node.Nodes.Insert(j + 1, selNode);

                    treeViewList.SelectedNode = node.Nodes[j + 1];

                    return;
                }
            }

        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteNode_Click(object sender, EventArgs e)
        {
            if (selNode == null)
            {
                XLog.Write("请选择节点");
                return;
            }

            if (selNode.Level == 0)
            {
                lstCorePointData = null;
            }
            else
            {
                if (lstCorePointData != null && lstCorePointData.Count > 0)
                {
                    for (int i = 0; i < lstCorePointData.Count; i++)
                    {
                        if (lstCorePointData[i].pointName == selNode.Name)
                        {
                            lstCorePointData.Remove(lstCorePointData[i]);
                        }
                    }
                }
            }

            selNode = null;
            txtPtName.Text = string.Empty;
            BindTreeList(lstCorePointData);
        }

        private void treeViewList_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void treeViewList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            selNode = e.Node;
            treeViewList.SelectedNode = e.Node;

            if (selNode.Level == 1)
            {
                txtPtName.Text = selNode.Text;
            }
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
                XLog.Write("请选择节点");
                return;
            }

            string strPtName = txtPtName.Text;

            string strErroInfo = PageVerificationInfo();
            if (strErroInfo != string.Empty)
            {
                XLog.Write(strErroInfo);
                return;
            }
            else
            {

                bool IsExit = false;
                for (int i = 0; i < lstCorePointData.Count; i++)
                {
                    if (lstCorePointData[i].pointName == strPtName)
                    {
                        XLog.Write("坐标点名称:" + strPtName + "已存在");
                        IsExit = true;
                        return;
                    }
                }

                if (IsExit == false)
                {
                    for (int i = 0; i < lstCorePointData.Count; i++)
                    {
                        if (lstCorePointData[i].pointName == selNode.Name)
                        {
                            double XValue = lstCorePointData[i].pointXValue;
                            double YValue = lstCorePointData[i].pointYValue;
                            lstCorePointData[i].pointName = strPtName;

                            break;
                        }
                    }
                }

                BindTreeList(lstCorePointData);

                TreeNode rootNode = treeViewList.Nodes[0];
                foreach (TreeNode childNode in rootNode.Nodes)
                {
                    if (strPtName == childNode.Name)
                    {
                        treeViewList.SelectedNode = childNode;
                        selNode = childNode;
                        break;
                    }
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
            form.lstCoreEnvelope = new List<string>();
            form.lstCoreEnvelope = GetListNodeName();

            //重心包线列表
            List<CorePointData> lstCurrentPtData = GetCurrentCorePointData();
            form.BindCoreEnvelopeList(lstCurrentPtData);

            //GridView重心包线
            form.SetGridView(lstCurrentPtData);
            form.BindGridViewData(lstCurrentPtData);

            //重心包线图
            CoreEnvelopeDesignManageForm.DisplayInPicture(lstCurrentPtData, zedGraphControlCore);
            this.Close();
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
