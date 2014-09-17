using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml;
using Model;
using XCommon;

namespace WeightCenterDesignAndEstimateSoft.Tool
{
    public partial class CoreEnvelopeArithmeticOperForm : Form
    {
        public string strCoreEnvelopeArithmeticFileName = "";

        private string strType = string.Empty;

        private int FormulaBase = 0;
        private CoreEnvelopeArithmetic waData = new CoreEnvelopeArithmetic();
        private Dictionary<string, string> dictTempPara = new Dictionary<string, string>();

        // 0 不处于编辑模式；1：插入节点模式；2：编辑节点模式
        private int nNodeEditMode = 0;
        // X 0   Y 1
        private int nEditFormulaIndex = 0;

        public CoreEnvelopeArithmeticOperForm()
        {
            InitializeComponent();
        }

        public CoreEnvelopeArithmeticOperForm(string str_Type, TreeNode selNode)
        {
            InitializeComponent();
            strType = str_Type;

            switch (strType)
            {
                case "new":
                    {
                        txtName.ReadOnly = false;
                        txtName.Text = "新建算法";
                        break;
                    }
                case "edit":
                    {
                        waData = CoreEnvelopeArithmetic.ReadArithmeticData(selNode.Name);
                        FormulaBase = waData.FormulaList.Count;
                        dateTimePickerCreateTime.Text = waData.CreateTime;
                        dateTimePickerLastModifyTime.Text = waData.LastModifyTime;
                        txtRemark.Text = waData.Remark;

                        txtName.Text = waData.Name;
                        txtName.ReadOnly = true;
                        break;
                    }
                case "jynew":
                    {
                        if (selNode.Tag == null)
                        {
                            waData = CoreEnvelopeArithmetic.ReadArithmeticData(selNode.Name);
                        }
                        else
                        {
                            CoreEnvelopeArithmetic tempwa = selNode.Tag as CoreEnvelopeArithmetic;
                            waData = (tempwa != null) ? tempwa.Clone() : (new CoreEnvelopeArithmetic());
                        }

                        FormulaBase = waData.FormulaList.Count;
                        dateTimePickerCreateTime.Text = waData.CreateTime;
                        dateTimePickerLastModifyTime.Text = waData.LastModifyTime;
                        txtRemark.Text = waData.Remark;

                        txtName.Text = waData.Name + " 副本";
                        txtName.ReadOnly = false;
                        break;
                    }
            }

            foreach (NodeFormula nf in waData.FormulaList)
            {
                TreeNode node = treeViewWeightSortNode.Nodes[0].Nodes.Add(nf.NodeName, nf.NodeName);
                node.Tag = nf;
            }
            treeViewWeightSortNode.ExpandAll();
        }

        private void treeViewWeightSortNode_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (textBoxFormulaEdit.Modified)
            {
                if (MessageBox.Show("公式已更改，是否放弃？", "公式已更改", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    textBoxFormulaEdit.Modified = false;
                }
            }
        }

        private void SetParaList(IEnumerable<WeightParameter> paras)
        {
            listViewPara.Items.Clear();

            foreach (WeightParameter wp in paras)
            {
                ListViewItem lvi = listViewPara.Items.Add(wp.ParaName);
                lvi.SubItems.Add(wp.ParaUnit);
                lvi.SubItems.Add(WeightParameter.ParaTypeList[wp.ParaType]);
                lvi.SubItems.Add(wp.ParaRemark);
            }
        }

        private void GetFinalNodeList(TreeNode node, ref List<TreeNode> ret)
        {
            if (node.Nodes.Count == 0)
            {
                ret.Add(node);
            }
            else
            {
                foreach (TreeNode subnode in node.Nodes)
                {
                    GetFinalNodeList(subnode, ref ret);
                }
            }
        }

        private void treeViewWeightSortNode_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SetEditMode(1);

            HashSet<WeightParameter> wpSet = new HashSet<WeightParameter>();

            TreeNode node = e.Node;
            if (node.Nodes.Count == 0)
            {
                TreeNode pnode = treeViewWeightSortNode.Nodes[0];

                NodeFormula nf = (NodeFormula)node.Tag;

                if (nf == null)
                {
                    if (pnode != node)
                    {
                        MessageBox.Show("异常错误！节点无对应公式！");
                    }
                    buttonEditX.Enabled = false;
                    buttonEditY.Enabled = false;
                }
                else
                {
                    textBoxFormulaX.Text = nf.XFormula.Formula;
                    textBoxFormulaY.Text = nf.YFormula.Formula;

                    foreach (WeightParameter wp in nf.XFormula.ParaList)
                    {
                        wpSet.Add(wp);
                    }
                    foreach (WeightParameter wp in nf.YFormula.ParaList)
                    {
                        wpSet.Add(wp);
                    }

                    buttonEditX.Enabled = true;
                    buttonEditY.Enabled = true;
                }
                buttonNodeEdit.Enabled = (pnode != node);
                buttonNodeDelete.Enabled = (pnode != node);

                buttonNodeMoveUp.Enabled = (pnode != node) && (pnode.FirstNode != node);
                buttonNodeMoveDown.Enabled = (pnode != node) && (pnode.LastNode != node);

                if (nNodeEditMode == 2)
                {
                    textBoxNodeName.Text = node.Text;
                }
            }
            else
            {
                buttonEditX.Enabled = false;
                buttonEditY.Enabled = false;
                buttonNodeEdit.Enabled = false;
                buttonNodeDelete.Enabled = false;
                buttonNodeMoveUp.Enabled = false;
                buttonNodeMoveDown.Enabled = false;

                listViewPara.Items.Clear();

                List<TreeNode> ret = new List<TreeNode>();
                GetFinalNodeList(node, ref ret);

                foreach (TreeNode finalnode in ret)
                {
                    NodeFormula nf = (NodeFormula)finalnode.Tag;
                    if (nf == null)
                    {
                        MessageBox.Show("异常错误！节点无对应公式！");
                        return;
                    }
                    foreach (WeightParameter wp in nf.XFormula.ParaList)
                    {
                        wpSet.Add(wp);
                    }
                    foreach (WeightParameter wp in nf.YFormula.ParaList)
                    {
                        wpSet.Add(wp);
                    }
                }

                textBoxFormulaX.Text = "-";
                textBoxFormulaY.Text = "-";
            }
            SetParaList(wpSet);
        }

        private void buttonFunction_Click(object sender, EventArgs e)
        {
            Button btnpress = (Button)sender;

            textBoxFormulaEdit.Paste(btnpress.Text);
            textBoxFormulaEdit.SelectionStart--;

            textBoxFormulaEdit.Focus();
        }

        private void buttonOperator_Click(object sender, EventArgs e)
        {
            Button btnpress = (Button)sender;

            textBoxFormulaEdit.Paste(btnpress.Text);

            textBoxFormulaEdit.Focus();
        }

        private void SetEditMode(int nstate)
        {
            groupBoxParaList.Visible = (nstate == 1);
            groupBoxFormula.Visible = (nstate == 1);

            groupBoxFunction.Visible = (nstate == 2);
            groupBoxParaInput.Visible = (nstate == 2);
            groupBoxFormulaEdit.Visible = (nstate == 2);
            panelApply.Visible = (nstate == 2);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            nEditFormulaIndex = (sender == buttonEditX) ? 0 : 1;

            TreeNode node = treeViewWeightSortNode.SelectedNode;
            NodeFormula nf = (NodeFormula)node.Tag;
            WeightFormula wf = nf[nEditFormulaIndex];

            dictTempPara.Clear();

            foreach (WeightParameter wp in wf.ParaList)
            {
                //if (WeightArithmetic.FindGlobleParameters(wp.ParaName, null).Count > 1)
                //{
                dictTempPara.Add(wp.ParaName, wp.ParaUnit);
                //}
            }

            textBoxFormulaEdit.Text = wf.Formula;
            SetEditMode(2);
            textBoxFormulaEdit.Focus();
            textBoxFormulaEdit.SelectionStart = textBoxFormulaEdit.TextLength;
        }

        private void listBoxCandidateFormula_DoubleClick(object sender, EventArgs e)
        {
            buttonApply_Click(null, null);
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            string errmsg;
            ZstExpression.CExpression expr = ZstExpression.CExpression.Parse(textBoxFormulaEdit.Text, out errmsg);

            if (expr == null)
            {
                MessageBox.Show(errmsg);
                return;
            }
            textBoxFormulaEdit.Text = expr.GetExpression();

            TreeNode node = treeViewWeightSortNode.SelectedNode;
            NodeFormula nf = (NodeFormula)node.Tag;
            WeightFormula wf = nf[nEditFormulaIndex];

            wf.Formula = textBoxFormulaEdit.Text;
            List<string> paras = new List<string>();
            expr.GetVariables(ref paras);

            wf.ParaList.Clear();

            foreach (string paraname in paras)
            {
                WeightParameter wp = null;
                if (dictTempPara.ContainsKey(paraname))
                {
                    wp = FindParameter(paraname, dictTempPara[paraname]);
                }
                else
                {
                    wp = FindParameter(paraname, null);
                }
                if (wp == null)
                {
                    wp = new WeightParameter();
                    wp.ParaName = paraname;
                    wp.ParaType = 10;
                }
                wf.ParaList.Add(wp);
            }
            HashSet<WeightParameter> wpSet = new HashSet<WeightParameter>();
            foreach (WeightParameter wp in nf.XFormula.ParaList)
            {
                wpSet.Add(wp);
            }
            foreach (WeightParameter wp in nf.YFormula.ParaList)
            {
                wpSet.Add(wp);
            }
            SetParaList(wpSet);

            if (nEditFormulaIndex == 0)
            {
                textBoxFormulaX.Text = wf.Formula;
                textBoxFormulaX.Modified = false;
            }
            else
            {
                textBoxFormulaY.Text = wf.Formula;
                textBoxFormulaY.Modified = false;
            }

            SetEditMode(1);

            dateTimePickerLastModifyTime.Value = DateTime.Today;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewWeightSortNode.SelectedNode;
            NodeFormula nf = (NodeFormula)node.Tag;
            WeightFormula wf = nf[nEditFormulaIndex];

            if (nEditFormulaIndex == 0)
            {
                textBoxFormulaX.Text = wf.Formula;
                textBoxFormulaX.Modified = false;
            }
            else
            {
                textBoxFormulaY.Text = wf.Formula;
                textBoxFormulaY.Modified = false;
            }

            SetEditMode(1);
        }

        private void listBoxParaType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxParaType.SelectedIndex == -1)
            {
                return;
            }
            listViewParaAll.Items.Clear();

            List<List<WeightParameter>> WeightParaList = WeightParameter.GetWeightParameterList();

            List<WeightParameter> wplist = WeightParaList[listBoxParaType.SelectedIndex];
            for (int i = 0; i < wplist.Count; ++i)
            {
                ListViewItem lvi = listViewParaAll.Items.Add(wplist[i].ParaName);
                lvi.SubItems.Add(wplist[i].ParaUnit);
                lvi.SubItems.Add(wplist[i].ParaRemark);
            }

            textBoxFormulaEdit.Focus();
        }

        private void listViewParaAll_DoubleClick(object sender, EventArgs e)
        {
            if (listViewParaAll.SelectedItems.Count == 1)
            {
                string paraName = listViewParaAll.SelectedItems[0].Text;

                if (dictTempPara.ContainsKey(paraName))
                {
                    string formula = textBoxFormulaEdit.Text.Substring(0, textBoxFormulaEdit.SelectionStart) + " " + textBoxFormulaEdit.Text.Substring(textBoxFormulaEdit.SelectionStart + textBoxFormulaEdit.SelectionLength);
                    string[] separator = new string[] { " ", "+", "-", "*", "/", "^", "(", ")" };
                    string[] ArrayValue = formula.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    bool bfind = false;
                    foreach (string strvalue in ArrayValue)
                    {
                        if (string.Compare(paraName, strvalue, true) == 0)
                        {
                            bfind = true;
                            break;
                        }
                    }

                    string unit = listViewParaAll.SelectedItems[0].SubItems[1].Text;
                    if (bfind)
                    {
                        if (unit != dictTempPara[paraName])
                        {
                            MessageBox.Show("与公式中的现有参数重名，但单位不一致，不能添加该参数！");
                            return;
                        }
                    }
                    else
                    {
                        dictTempPara[paraName] = unit;
                    }
                }
                else
                {
                    //if (WeightArithmetic.FindGlobleParameters(paraName, null).Count > 1)
                    //{
                    string unit = listViewParaAll.SelectedItems[0].SubItems[1].Text;
                    dictTempPara.Add(paraName, unit);
                    //}
                }

                textBoxFormulaEdit.Paste(paraName);

                textBoxFormulaEdit.Focus();
            }
        }

        public WeightParameter FindParameter(string name, string unit)
        {
            WeightParameter localwp = waData.FindParameter(name, unit);
            if (localwp != null)
            {
                return localwp;
            }

            List<List<WeightParameter>> WeightParaList = WeightParameter.GetWeightParameterList();

            for (int i = 0; i < WeightParaList.Count; ++i)
            {
                foreach (WeightParameter temp in WeightParaList[i])
                {
                    if (temp.ParaName == name)
                    {
                        if (unit == null || (temp.ParaUnit == unit))
                        {
                            return new WeightParameter(temp);
                        }
                    }
                }
            }

            return null;
        }

        static public bool WriteArithmeticFile(CoreEnvelopeArithmetic wa, bool bOverWritePrompt)
        {
            string filepath = "CenterofGravityEnvelopeMethod\\" + wa.Name + ".mcem";

            return wa.WriteArithmeticFile(filepath, bOverWritePrompt);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[\\*\\\\/:?<>|\"]");

            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("算法名称不能为空！");
                return;
            }
            else
            {
                if (Verification.IsCheckSignleString(txtName.Text))
                {
                    MessageBox.Show("算法名称包含非法字符！");
                    txtName.Focus();
                    return;
                }
            }

            if (txtRemark.Text != string.Empty)
            {
                if (Verification.IsCheckRemarkString(txtRemark.Text))
                {
                    MessageBox.Show("算法备注包含非法字符！");
                    return;
                }
            }

            //更新节点
            waData.FormulaList.Clear();
            foreach (TreeNode node in treeViewWeightSortNode.Nodes[0].Nodes)
            {
                waData.FormulaList.Add((NodeFormula)node.Tag);
            }

            List<WeightParameter> templistpara = waData.GetParaList();
            bool bParaPrompt = false;
            foreach (WeightParameter wp in templistpara)
            {
                //if (wp.ParaType == 10 && wp.ParaUnit.Length == 0 && wp.ParaRemark.Length == 0)
                if (wp.ParaType == 10)
                {
                    bParaPrompt = true;
                    break;
                }
            }

            if (bParaPrompt)
            {
                if (MessageBox.Show("算法中含有未定义参数(临时参数)！\r\n保存算法前是否对这些参数进行设定？", "参数定义", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    List<WeightParameter> listparaforset = new List<WeightParameter>();
                    foreach (WeightParameter wp in templistpara)
                    {
                        // if (wp.ParaType == 10 && wp.ParaUnit.Length == 0 && wp.ParaRemark.Length == 0)
                        if (wp.ParaType == 10)
                        {
                            listparaforset.Add(wp);
                        }
                    }
                    TempWeightParaSet form = new TempWeightParaSet(listparaforset);
                    form.ShowDialog();
                }
            }

            bool bprompt = (strType == "edit") ? false : true;

            waData.Name = txtName.Text;
            waData.CreateTime = dateTimePickerCreateTime.Text;
            waData.LastModifyTime = dateTimePickerLastModifyTime.Text;
            waData.Remark = txtRemark.Text;

            if (WriteArithmeticFile(waData, bprompt) == false)
            {
                return;
            }

            strCoreEnvelopeArithmeticFileName = waData.Name;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtRemark_ModifiedChanged(object sender, EventArgs e)
        {
            dateTimePickerLastModifyTime.Value = DateTime.Today;
        }

        private void buttonNodeMoveUp_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewWeightSortNode.SelectedNode;
            int npos = node.Index;
            node.Remove();
            treeViewWeightSortNode.Nodes[0].Nodes.Insert(npos - 1, node);
            treeViewWeightSortNode.SelectedNode = node;
            treeViewWeightSortNode.Focus();
        }

        private void buttonNodeMoveDown_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewWeightSortNode.SelectedNode.NextNode;
            treeViewWeightSortNode.SelectedNode = null;
            int npos = node.Index;
            node.Remove();
            treeViewWeightSortNode.Nodes[0].Nodes.Insert(npos - 1, node);
            treeViewWeightSortNode.SelectedNode = node.NextNode;

            treeViewWeightSortNode.Focus();
        }

        private void buttonNodeInsert_Click(object sender, EventArgs e)
        {
            nNodeEditMode = 1;
            panelNodeManage.Visible = false;
            panelEditNodeName.Visible = true;
            labelNodeName.Text = "节点插入：";
        }

        private void buttonNodeEdit_Click(object sender, EventArgs e)
        {
            nNodeEditMode = 2;
            panelNodeManage.Visible = false;
            panelEditNodeName.Visible = true;
            labelNodeName.Text = "节点编辑：";

            TreeNode node = treeViewWeightSortNode.SelectedNode;
            textBoxNodeName.Text = node.Text;
        }

        private void buttonNodeDelete_Click(object sender, EventArgs e)
        {
            NodeFormula node = waData.FormulaList.Find(nf => nf.NodeName == treeViewWeightSortNode.SelectedNode.Text);

            waData.FormulaList.Remove(node);

            treeViewWeightSortNode.Nodes.Remove(treeViewWeightSortNode.SelectedNode);

            if (treeViewWeightSortNode.Nodes.Count == 0)
            {
                treeViewWeightSortNode.SelectedNode = treeViewWeightSortNode.Nodes[0];
            }
        }

        private void buttonApplyNodeName_Click(object sender, EventArgs e)
        {
            string nodename = textBoxNodeName.Text;
            nodename = nodename.Trim();
            if (nodename.Length == 0)
            {
                MessageBox.Show("节点名称为空！");
                return;
            }
            if (XCommon.Verification.IsCheckString(nodename))
            {
                MessageBox.Show("节点名称含有非法字符！");
                return;
            }

            TreeNode pnode = treeViewWeightSortNode.Nodes[0];
            TreeNode node = treeViewWeightSortNode.SelectedNode;

            if (node == null)
            {
                node = pnode;
            }

            if (nNodeEditMode == 2)
            {
                if (node == pnode)
                {
                    MessageBox.Show("不能修改根节点！");
                    return;
                }
                if (nodename == node.Text)
                {
                    return;
                }
            }

            if (pnode.Nodes.ContainsKey(nodename))
            {
                MessageBox.Show("算法中已含有该节点！");
                return;
            }

            if (nNodeEditMode == 2)
            {
                if (node != pnode)
                {
                    var tempnf = waData.FormulaList.Find(nf => nf.NodeName == node.Name);

                    XCommon.XLog.Write("修改节点名称成功，\"" + node.Name + "\"修改为\"" + nodename + "\"！");
                    node.Name = nodename;
                    node.Text = nodename;
                    tempnf.NodeName = nodename;
                }
            }
            else
            {
                TreeNode newNode = null;
                if (node != pnode)
                {
                    newNode = pnode.Nodes.Insert(node.Index, nodename, nodename);
                }
                else
                {
                    newNode = pnode.Nodes.Add(nodename, nodename);
                }
                XCommon.XLog.Write("成功插入节点\"" + nodename + "\"！");
                if (!pnode.IsExpanded)
                {
                    pnode.Expand();
                }

                NodeFormula nf = new NodeFormula(newNode.Text);
                newNode.Tag = nf;

                waData.FormulaList.Add(nf);
            }
        }

        private void buttonCancelNodeName_Click(object sender, EventArgs e)
        {
            nNodeEditMode = 0;
            panelEditNodeName.Visible = false;
            panelNodeManage.Visible = true;
        }
    }

}
