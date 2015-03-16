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

namespace WeightCenterDesignAndEstimateSoft.Tool
{
    public partial class WeightSortEditForm : Form
    {
        #region 属性

        private string strType = string.Empty;
        //重量分类操作标识
        private string strOperType = string.Empty;
        private TreeNode selNode = null;
        private WeightSortData weightSort = null;

        public WeightSortEditForm()
        {
            InitializeComponent();

        }

        public WeightSortEditForm(WeightSortManageForm _weightSortManage, string str_Type)
        {
            InitializeComponent();
            this.Text = "重量分类新建对话框";

            strType = str_Type;
            IntiSettingButton();
        }

        public WeightSortEditForm(WeightSortManageForm _weightSortManage, WeightSortData _weightSort, string str_Type)
        {
            InitializeComponent();
            weightSort = _weightSort;
            strType = str_Type;


            btnAddNode.Text = "添加子节点(&N)";

            SetPageData();
            IntiSettingButton();
        }

        #endregion

        #region 自定义方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void IntiSettingButton()
        {
            switch (strType)
            {
                case "new":
                case "JYNew":
                    {
                        btnAddNode.Enabled = true;
                        btnModifyNode.Enabled = true;
                        btnDelete.Enabled = true;
                        btnUp.Enabled = true;
                        btnDown.Enabled = true;

                        txtNodeName.Enabled = false;
                        txtRemark.Enabled = false;

                        txtWeightSortName.Enabled = true;

                        btnSaveNode.Enabled = false;
                        btnNoldeCancle.Enabled = false;

                        break;
                    }
                case "edit":
                    {
                        btnAddNode.Enabled = true;
                        btnModifyNode.Enabled = true;
                        btnDelete.Enabled = true;
                        btnUp.Enabled = true;
                        btnDown.Enabled = true;

                        txtNodeName.Enabled = false;
                        txtRemark.Enabled = false;

                        txtWeightSortName.Enabled = false;

                        btnSaveNode.Enabled = false;
                        btnNoldeCancle.Enabled = false;


                        break;
                    }
                case "readOnlyEdit":
                    {
                        btnAddNode.Enabled = false;
                        btnModifyNode.Enabled = false;
                        btnDelete.Enabled = false;
                        btnUp.Enabled = true;
                        btnDown.Enabled = true;

                        txtWeightSortName.Enabled = false;

                        txtNodeName.Enabled = false;
                        txtRemark.Enabled = false;

                        btnSaveNode.Enabled = false;
                        btnNoldeCancle.Enabled = false;

                        break;
                    }

            }
        }

        private void SettingControls()
        {
            switch (strOperType)
            {
                case "new":
                case "edit":
                    {
                        btnAddNode.Enabled = false;
                        btnModifyNode.Enabled = false;
                        btnDelete.Enabled = false;
                        btnUp.Enabled = false;
                        btnDown.Enabled = false;

                        txtNodeName.Enabled = true;
                        txtRemark.Enabled = true;

                        btnSaveNode.Enabled = true;
                        btnNoldeCancle.Enabled = true;
                        break;
                    }
                case "confirm":
                case "cancle":
                    {
                        IntiSettingButton();
                        break;
                    }
            }

        }

        /// <summary>
        /// 绑定重量结构树数据子节点
        /// </summary>
        static private void BindTreeNode(TreeNode ParentNode, int nParentID, WeightSortData wsd)
        {
            IEnumerable<WeightData> selection = from wd in wsd.lstWeightData where wd.nParentID == nParentID select wd;
            foreach (WeightData wd in selection)
            {
                TreeNode node = ParentNode.Nodes.Add(ParentNode.Name + "\\" + wd.weightName, wd.weightName);
                node.ToolTipText = wd.strRemark;

                BindTreeNode(node, wd.nID, wsd);
            }
        }

        /// <summary>
        /// 绑定重量结构树数据
        /// </summary>
        static public void BindTreeList(TreeView tree, WeightSortData wsd)
        {
            tree.Nodes.Clear();

            IEnumerable<WeightData> selection = from wd in wsd.lstWeightData where wd.nParentID == -1 select wd;
            foreach (WeightData wd in selection)
            {
                TreeNode node = tree.Nodes.Add(wd.weightName, wd.weightName);
                node.ToolTipText = wd.strRemark;

                BindTreeNode(node, wd.nID, wsd);
            }
        }

        private void SetPageData()
        {
            if (strType == "JYNew")
            {
                this.Text = "基于" + weightSort.sortName + "新建对话框";
                txtWeightSortName.Text = weightSort.sortName + " 副本";
            }
            else if (strType == "edit" || strType == "readOnlyEdit")
            {
                this.Text = "重量分类编辑对话框";
                txtWeightSortName.Text = weightSort.sortName;
            }
            BindTreeList(treeViewWeightStructure, weightSort);
            treeViewWeightStructure.ExpandAll();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNode_Click(object sender, EventArgs e)
        {
            selNode = treeViewWeightStructure.SelectedNode;
            txtNodeName.Text = string.Empty;
            txtRemark.Text = string.Empty;

            strOperType = "new";
            SettingControls();

            txtNodeName.Focus();
        }

        /// <summary>
        /// 上移事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            if (selNode == null)
            {
                MessageBox.Show("没选择节点！");
                return;
            }
            if (selNode.Level == 0)
            {
                return;
            }

            TreeNode parentNode = selNode.Parent;

            if (selNode == parentNode.FirstNode)
            {
                return;
            }

            treeViewWeightStructure.SelectedNode = null;

            int npos = selNode.Index - 1;
            selNode.Remove();
            parentNode.Nodes.Insert(npos, selNode);

            treeViewWeightStructure.SelectedNode = selNode;
        }

        /// <summary>
        /// 下移事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            if (selNode == null)
            {
                MessageBox.Show("没选择节点！");
                return;
            }
            if (selNode.Level == 0)
            {
                return;
            }

            TreeNode parentNode = selNode.Parent;

            if (selNode == parentNode.LastNode)
            {
                return;
            }

            TreeNode nextNode = selNode.NextNode;

            nextNode.Remove();
            parentNode.Nodes.Insert(selNode.Index, nextNode);
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
                MessageBox.Show("没选择节点");
                return;
            }

            if (selNode.Level == 0)
            {
                treeViewWeightStructure.Nodes.Remove(selNode);
                btnAddNode.Text = "添加根节点(&N)";
                txtWeightSortName.Enabled = false;
            }
            else
            {
                TreeNode node = selNode.Parent;
                node.Nodes.Remove(selNode);
                treeViewWeightStructure.SelectedNode = null;
            }

            selNode = null;
            txtNodeName.Text = string.Empty;
            txtRemark.Text = string.Empty;
        }

        private void WriteTreeDataToList(TreeNode node, ref List<WeightData> wdList, ref int nNodeID)
        {
            int nParentID = nNodeID;
            foreach (TreeNode subnode in node.Nodes)
            {
                WeightData wd = new WeightData();
                wd.nID = ++nNodeID;
                wd.weightName = subnode.Text;
                wd.strRemark = subnode.ToolTipText;
                wd.nParentID = nParentID;
                wdList.Add(wd);

                WriteTreeDataToList(subnode, ref wdList, ref nNodeID);
            }
        }

        /// <summary>
        /// 确定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string strSortName = txtWeightSortName.Text.Trim();

            if (strSortName.Length == 0)
            {
                MessageBox.Show("重量分类名称为空！");
                return;
            }
            else
            {
                if (Verification.IsCheckString(strSortName))
                {
                    MessageBox.Show("重量分类名称含有非法字符！");
                    return;
                }
            }

            if (treeViewWeightStructure.Nodes.Count == 0)
            {
                MessageBox.Show("重量分类必须有根节点！");
                return;
            }

            WeightSortData ws = null;
            if (strType == "edit" || strType == "readOnlyEdit")
            {
                foreach (WeightSortData tempwsd in WeightSortManageForm.GetListWeightSortData())
                {
                    if (tempwsd.sortName == strSortName)
                    {
                        ws = tempwsd;
                        ws.lstWeightData.Clear();
                    }
                }
                if (ws == null)
                {
                    MessageBox.Show("未知错误！");
                    return;
                }
            }
            else
            {
                ws = new WeightSortData();
            }
            ws.sortName = strSortName;

            int nNodeID = -1;
            for (int k = 0; k < treeViewWeightStructure.Nodes.Count; k++)
            {
                TreeNode subnode = treeViewWeightStructure.Nodes[k];
                List<WeightData> lstWeightData = ws.lstWeightData;

                //添加根节点
                WeightData rootWeightData = new WeightData();
                rootWeightData.nID = ++nNodeID;
                rootWeightData.weightName = subnode.Text;
                rootWeightData.strRemark = subnode.ToolTipText;
                rootWeightData.nParentID = -1;

                lstWeightData.Add(rootWeightData);

                WriteTreeDataToList(subnode, ref lstWeightData, ref nNodeID);
            }

            if (WeightSortManageForm.SaveHccFile(ws, strType != "edit" && strType != "readOnlyEdit"))
            {
                if (strType == "edit" || strType == "readOnlyEdit")
                {
                    XLog.Write("编辑重量分类成功");
                }
                else
                {
                    if (strType == "new")
                    {
                        XLog.Write("新建重量分类成功");
                    }
                    if (strType == "JYNew")
                    {
                        XLog.Write("基于新建重量分类成功");
                    }
                    WeightSortManageForm.GetListWeightSortData().Add(ws);
                }
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// 更改节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifyNode_Click(object sender, EventArgs e)
        {

            selNode = treeViewWeightStructure.SelectedNode;
            if (selNode == null)
            {
                MessageBox.Show("没选择节点！");
                return;
            }

            strOperType = "edit";
            SettingControls();

            txtNodeName.Focus();
        }

        private void btnSaveNode_Click(object sender, EventArgs e)
        {
            string strChildName = txtNodeName.Text;
            if (strChildName == string.Empty)
            {
                MessageBox.Show("请输入节点名称！");
                return;
            }
            else
            {
                if (Verification.IsCheckString(strChildName))
                {
                    MessageBox.Show("节点名称含有非法字符！");
                    return;
                }
            }

            if (txtRemark.Text != string.Empty)
            {
                if (Verification.IsCheckRemarkString(txtRemark.Text))
                {
                    MessageBox.Show("节点备注含有非法字符！");
                    return;
                }
            }

            if (strOperType == "new")
            {
                if (selNode != null)
                { 
                    foreach(TreeNode tempnode in selNode.Nodes)
                    {
                        if (tempnode.Text == txtNodeName.Text)
                        {
                            MessageBox.Show("节点重名！请重新输入节点名称！");
                            return;
                        }
                    }
                }

                TreeNode childNode = new TreeNode();
                childNode.Name = strChildName;
                childNode.Text = strChildName;
                childNode.ToolTipText = txtRemark.Text;

                if (selNode == null)
                {
                    if (treeViewWeightStructure.Nodes.Count == 0)
                    {
                        treeViewWeightStructure.Nodes.Add(childNode);
                        btnAddNode.Text = "添加子节点(&N)";
                        treeViewWeightStructure.SelectedNode = childNode;
                        selNode = childNode;
                    }
                    else
                    {
                        MessageBox.Show("请选择添加节点的位置！");
                        return;
                    }
                }
                else
                {
                    selNode.Nodes.Add(childNode);
                    selNode.Expand();

                    treeViewWeightStructure.SelectedNode = childNode.Parent;
                    selNode = childNode.Parent;

                    txtNodeName.Text = selNode.Text;
                    txtRemark.Text = selNode.ToolTipText;
                }
            }
            //编辑
            else if (strOperType == "edit")
            {
                if (selNode != null)
                {
                    TreeNode tempnode = selNode;
                    while (tempnode.PrevNode != null)
                    {
                        tempnode = tempnode.PrevNode;
                        if (tempnode.Text == txtNodeName.Text)
                        {
                            MessageBox.Show("节点重名！请重新输入节点名称！");
                            return;
                        }
                    }
                    tempnode = selNode;
                    while (tempnode.NextNode != null)
                    {
                        tempnode = tempnode.NextNode;
                        if (tempnode.Text == txtNodeName.Text)
                        {
                            MessageBox.Show("节点重名！请重新输入节点名称！");
                            return;
                        }
                    }
                }

                selNode.Name = txtNodeName.Text;
                selNode.Text = txtNodeName.Text;
                selNode.ToolTipText = txtRemark.Text;
            }

            strOperType = "confirm";
            SettingControls();
        }

        private void btnNoldeCancle_Click(object sender, EventArgs e)
        {
            strOperType = "cancle";
            SettingControls();
            // 若取消新建,则页面显示当前选择节点信息
            if (selNode != null)
            {
                txtNodeName.Text = selNode.Text;
                txtRemark.Text = selNode.ToolTipText;
            }

            // 若当前树中无节点,清空页面信息
            if (treeViewWeightStructure.Nodes.Count == 0)
            {
                txtNodeName.Text = string.Empty;
                txtRemark.Text = string.Empty;
            }
        }

        private void treeViewWeightStructure_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            strOperType = "cancle";
            IntiSettingButton();

            selNode = e.Node;

            txtNodeName.Text = selNode.Text;
            txtRemark.Text = selNode.ToolTipText;

        }

        private void treeViewWeightStructure_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void treeViewWeightStructure_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selNode = e.Node;

            if (e.Node != null)
            {
                txtNodeName.Text = e.Node.Text;
                txtRemark.Text = e.Node.ToolTipText;
            }
        }
    }
}
