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
    public partial class WeightArithmeticOperForm : Form
    {
        public string strWeightSortName = "";
        public string strWeightArithmeticFileName = "";

        private string strType = string.Empty;

        private WeightArithmetic waData = new WeightArithmetic();
        private Dictionary<string, string> dictTempPara = new Dictionary<string, string>();

        private Dictionary<WeightFormula, string> mapCandidateFormula = null;

        public WeightArithmeticOperForm()
        {
            InitializeComponent();
        }

        public WeightArithmeticOperForm(string str_Type, TreeNode selNode)
        {
            InitializeComponent();
            strType = str_Type;

            List<WeightSortData> wsDataList = WeightSortManageForm.GetListWeightSortData();

            for (int i = 0; i < wsDataList.Count; ++i)
            {
                comboBoxWeightSort.Items.Add(wsDataList[i].sortName);
            }
            string wsortname = null;

            switch (strType)
            {
                case "new":
                    {
                        wsortname = selNode.Text;
                        txtName.ReadOnly = false;
                        txtName.Text = "新建算法";
                        break;
                    }
                case "edit":
                    {
                        waData = WeightArithmetic.ReadArithmeticData(selNode.Name);
                        dateTimePickerCreateTime.Text = waData.CreateTime;
                        dateTimePickerLastModifyTime.Text = waData.LastModifyTime;
                        txtRemark.Text = waData.Remark;

                        comboBoxWeightSort.Enabled = false;
                        wsortname = selNode.Parent.Text;
                        txtName.Text = waData.Name;
                        txtName.ReadOnly = true;
                        break;
                    }
                case "jynew":
                    {
                        if (selNode.Tag == null)
                        {
                            waData = WeightArithmetic.ReadArithmeticData(selNode.Name);
                            wsortname = selNode.Parent.Text;
                        }
                        else
                        {
                            WeightArithmetic tempwa = selNode.Tag as WeightArithmetic;
                            waData = (tempwa != null) ? tempwa.Clone() : (new WeightArithmetic());
                            wsortname = waData.SortName;
                        }
                        dateTimePickerCreateTime.Text = waData.CreateTime;
                        dateTimePickerLastModifyTime.Text = waData.LastModifyTime;
                        txtRemark.Text = waData.Remark;

                        mapCandidateFormula = new Dictionary<WeightFormula, string>();
                        foreach (WeightFormula wf in waData.FormulaList)
                        {
                            mapCandidateFormula.Add(wf, wf.NodePath);
                        }
                      
                        txtName.Text = waData.Name + " 副本";
                        txtName.ReadOnly = false;
                        break;
                    }
            }
            int nselindex = 0;
            for (int i = 0; i < wsDataList.Count; ++i)
            {
                if (wsDataList[i].sortName == wsortname)
                {
                    nselindex = i;
                    break;
                }
            }
            comboBoxWeightSort.SelectedIndex = nselindex;
        }

        private void treeViewWeightSortNode_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (textBoxFormula.Modified)
            {
                if (MessageBox.Show("公式已更改，是否放弃？", "公式已更改", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    textBoxFormula.Modified = false;
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

            TreeNode node = e.Node;
            if (node.Nodes.Count == 0)
            {
                WeightFormula wf = (WeightFormula)node.Tag;

                if (wf == null)
                {
                    MessageBox.Show("异常错误！节点无对应公式！");
                    return;
                }
                textBoxFormula.Text = wf.Formula;

                SetParaList(wf.ParaList);

                buttonEdit.Enabled = true;

                buttonSelect.Enabled = (listBoxCandidateFormula.Items.Count > 0);
            }
            else
            {
                listViewPara.Items.Clear();

                List<TreeNode> ret = new List<TreeNode>();
                GetFinalNodeList(node, ref ret);

                HashSet<WeightParameter> wpSet = new HashSet<WeightParameter>();

                foreach (TreeNode finalnode in ret)
                {
                    WeightFormula wf = (WeightFormula)finalnode.Tag;
                    if (wf == null)
                    {
                        MessageBox.Show("异常错误！节点无对应公式！");
                        return;
                    }
                    foreach (WeightParameter wp in wf.ParaList)
                    {
                        wpSet.Add(wp);
                    }
                }
                SetParaList(wpSet);

                textBoxFormula.Text = "组合节点无公式";
                buttonEdit.Enabled = false;
                buttonSelect.Enabled = false;
            }
        }

        private void buttonFunction_Click(object sender, EventArgs e)
        {
            Button btnpress = (Button)sender;

            textBoxFormula.Paste(btnpress.Text);
            textBoxFormula.SelectionStart--;

            textBoxFormula.Focus();
        }

        private void buttonOperator_Click(object sender, EventArgs e)
        {
            Button btnpress = (Button)sender;

            textBoxFormula.Paste(btnpress.Text);

            textBoxFormula.Focus();
        }

        private void SetEditMode(int nstate)
        {
            groupBoxParaList.Visible = (nstate == 1);

            groupBoxFunction.Visible = (nstate == 2);
            groupBoxParaInput.Visible = (nstate == 2);

            groupBoxSelectFormula.Visible = (nstate == 3);

            groupBoxFormula.Visible = (nstate != 3);
            textBoxFormula.ReadOnly = (nstate == 1);

            panelApply.Visible = (nstate != 1);
            panelEdit.Visible = (nstate == 1);

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewWeightSortNode.SelectedNode;
            WeightFormula wf = (WeightFormula)node.Tag;

            dictTempPara.Clear();

            foreach (WeightParameter wp in wf.ParaList)
            {
                //if (WeightArithmetic.FindGlobleParameters(wp.ParaName, null).Count > 1)
                //{
                dictTempPara.Add(wp.ParaName, wp.ParaUnit);
                //}
            }

            SetEditMode(2);
            textBoxFormula.Focus();
            textBoxFormula.SelectionStart = textBoxFormula.TextLength;
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            listBoxCandidateFormula.SelectedIndex = 0;
            SetEditMode(3);
        }

        private void listBoxCandidateFormula_DoubleClick(object sender, EventArgs e)
        {
            buttonApply_Click(null, null);
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            WeightFormula wf = null;
            if (groupBoxSelectFormula.Visible)
            {
                TreeNode node = treeViewWeightSortNode.SelectedNode;

                WeightFormula oldwf = (WeightFormula)node.Tag;
                oldwf.nAttach = 0;
                wf = mapCandidateFormula.First(temppair => temppair.Value == (string)listBoxCandidateFormula.SelectedItem).Key;
                node.Tag = wf;
                wf.NodePath = node.Name;
                wf.nAttach = 1;
                textBoxFormula.Text = wf.Formula;
                listBoxCandidateFormula.Items.Remove(listBoxCandidateFormula.SelectedItem);

                if (mapCandidateFormula.ContainsKey(oldwf))
                {
                    listBoxCandidateFormula.Items.Add(mapCandidateFormula[oldwf]);
                }
            }
            else
            {
                string errmsg;
                ZstExpression.CExpression expr = ZstExpression.CExpression.Parse(textBoxFormula.Text, out errmsg);

                if (expr == null)
                {
                    MessageBox.Show(errmsg);
                    return;
                }
                textBoxFormula.Text = expr.GetExpression();

                TreeNode node = treeViewWeightSortNode.SelectedNode;
                wf = (WeightFormula)node.Tag;

                wf.Formula = textBoxFormula.Text;
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
            }
            SetParaList(wf.ParaList);
            textBoxFormula.Modified = false;
            SetEditMode(1);

            dateTimePickerLastModifyTime.Value = DateTime.Today;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewWeightSortNode.SelectedNode;
            WeightFormula wf = (WeightFormula)node.Tag;
            textBoxFormula.Text = wf.Formula;
            textBoxFormula.Modified = false;
            SetEditMode(1);
        }

        private void comboBoxWeightSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nwsort = comboBoxWeightSort.SelectedIndex;
            if (nwsort == -1)
            {
                return;
            }
            List<WeightSortData> wsDataList = WeightSortManageForm.GetListWeightSortData();

            WeightSortEditForm.BindTreeList(treeViewWeightSortNode, wsDataList[nwsort]);

            List<TreeNode> FinalNodes = new List<TreeNode>();
            foreach (TreeNode node in treeViewWeightSortNode.Nodes)
            {
                GetFinalNodeList(node, ref FinalNodes);
            }

            if (strType == "jynew")
            {
                waData.FormulaList.RemoveRange(mapCandidateFormula.Count, waData.FormulaList.Count - mapCandidateFormula.Count);

                foreach (KeyValuePair<WeightFormula, string> wfpair in mapCandidateFormula)
                {
                    wfpair.Key.nAttach = 0;
                    foreach (TreeNode node in FinalNodes)
                    {
                        if (node.Name == wfpair.Value)
                        {
                            wfpair.Key.nAttach = 1;
                            node.Tag = wfpair.Key;
                            break;
                        }
                    }
                }

                listBoxCandidateFormula.Items.Clear();
                foreach (KeyValuePair<WeightFormula, string> wfpair in mapCandidateFormula)
                {
                    if (wfpair.Key.nAttach == 0)
                    {
                        listBoxCandidateFormula.Items.Add(wfpair.Value);
                    }
                }
            }
            else if (waData.FormulaList.Count > 0)
            {
                foreach (WeightFormula wf in waData.FormulaList)
                {
                    wf.nAttach = 0;
                    foreach (TreeNode node in FinalNodes)
                    {
                        if (node.Name == wf.NodePath)
                        {
                            wf.nAttach = 1;
                            node.Tag = wf;
                            break;
                        }
                    }
                }
            }

            foreach (TreeNode node in FinalNodes)
            {
                if (node.Tag == null)
                {
                    WeightFormula wf = new WeightFormula();
                    wf.NodePath = node.Name;
                    wf.nAttach = 1;
                    node.Tag = wf;
                    waData.FormulaList.Add(wf);
                }
            }

            treeViewWeightSortNode.ExpandAll();
            SetEditMode(1);
            textBoxFormula.Text = "";
            textBoxFormula.Modified = false;
            buttonEdit.Enabled = false;
            buttonSelect.Enabled = false;
            listViewPara.Items.Clear();
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

            textBoxFormula.Focus();
        }

        private void listViewParaAll_DoubleClick(object sender, EventArgs e)
        {
            if (listViewParaAll.SelectedItems.Count == 1)
            {
                string paraName = listViewParaAll.SelectedItems[0].Text;

                if (dictTempPara.ContainsKey(paraName))
                {
                    string formula = textBoxFormula.Text.Substring(0, textBoxFormula.SelectionStart) + " " + textBoxFormula.Text.Substring(textBoxFormula.SelectionStart + textBoxFormula.SelectionLength);
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

                textBoxFormula.Paste(paraName);

                textBoxFormula.Focus();
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

        static public bool WriteArithmeticFile(WeightArithmetic wa, bool bOverWritePrompt)
        {
            string filepath = "weightCategory\\" + wa.SortName + "\\" + wa.Name + ".wem";

            return wa.WriteArithmeticFile(filepath, bOverWritePrompt);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("算法名称不能为空!");
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
                        //if (wp.ParaType == 10 && wp.ParaUnit.Length == 0 && wp.ParaRemark.Length == 0)
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

            waData.SortName = comboBoxWeightSort.Text;
            waData.Name = txtName.Text;
            waData.CreateTime = dateTimePickerCreateTime.Text;
            waData.LastModifyTime = dateTimePickerLastModifyTime.Text;
            waData.Remark = txtRemark.Text;

            if (WriteArithmeticFile(waData, bprompt) == false)
            {
                return;
            }

            strWeightSortName = waData.SortName;
            strWeightArithmeticFileName = waData.Name;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtRemark_ModifiedChanged(object sender, EventArgs e)
        {
            dateTimePickerLastModifyTime.Value = DateTime.Today;
        }


    }

}
