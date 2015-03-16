using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Model;
using System.IO;

namespace WeightCenterDesignAndEstimateSoft.Tool
{
    public partial class CoreEnvelopeAtithmeticManageForm : Form
    {
        private TreeNode selNode = null;

        private MainForm mainForm = null;

        public CoreEnvelopeAtithmeticManageForm(MainForm form)
        {
            InitializeComponent();
            mainForm = form;
            WeightParameter.ResetWeightParameterList();

            treeViewArithmeticList.ImageList = imageListTreeView;

            //if (!System.IO.Directory.Exists("CenterofGravityEnvelopeMethod"))
            //{
            //    MessageBox.Show("不存在 CenterofGravityEnvelopeMethod 目录！");
            //    Close();
            //    return;
            //}

            Dictionary<string, string> waItems = GetArithmeticItems();

            TreeNode rootnode1 = treeViewArithmeticList.Nodes.Add("CenterofGravityEnvelopeMethod", "重心包线算法", 0, 1);

            foreach (KeyValuePair<string, string> pair in waItems)
            {
                rootnode1.Nodes.Add(pair.Value, pair.Key, 4, 5);
            }

            rootnode1.ExpandAll();

            if (mainForm == null)
            {
                return;
            }
            DesignProjectData dpData = mainForm.designProjectData;

            if (dpData == null)
            {
                return;
            }
            if (dpData.lstCoreEnvelopeDesign != null)
            {
                TreeNode rootnode2 = treeViewArithmeticList.Nodes.Add("设计结果列表", "设计结果列表", 0, 1);
                foreach (CoreEnvelopeArithmetic wa in dpData.lstCoreEnvelopeDesign)
                {
                    TreeNode node = rootnode2.Nodes.Add(wa.DataName, wa.DataName, 4, 5);
                    node.Tag = wa;
                }
                rootnode2.Expand();
            }
        }

        static public Dictionary<string, string> GetArithmeticItems()
        {
            Dictionary<string, string> waDict = new Dictionary<string, string>();

            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);

            string dirname = "CenterofGravityEnvelopeMethod";

            if (System.IO.Directory.Exists(dirname))
            {
                string[] files = System.IO.Directory.GetFiles(dirname, "*.MCEM");

                for (int i = 0; i < files.Length; ++i)
                {
                    string strname = files[i];
                    strname = strname.Substring(strname.LastIndexOf('\\') + 1);
                    strname = strname.Substring(0, strname.Length - 5);
                    waDict.Add(strname, files[i]);
                }
            }
            else
            {
                System.IO.Directory.CreateDirectory(dirname);
            }

            return waDict;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            CoreEnvelopeArithmeticOperForm form = new CoreEnvelopeArithmeticOperForm("new", selNode);
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            TreeNode node = treeViewArithmeticList.Nodes[0];

            string key = "CenterofGravityEnvelopeMethod\\" + form.strCoreEnvelopeArithmeticFileName + ".mcem";
            if (!node.Nodes.ContainsKey(key))
            {
                node.Nodes.Add(key, form.strCoreEnvelopeArithmeticFileName, 4, 5);
            }
            if (node.IsExpanded == false)
            {
                node.Expand();
            }

            WeightParameter.GetWeightParameterList()[10].Clear();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            CoreEnvelopeArithmeticOperForm form = new CoreEnvelopeArithmeticOperForm("edit", selNode);
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            WeightParameter.GetWeightParameterList()[10].Clear();
        }

        private void btnJYNew_Click(object sender, EventArgs e)
        {
            CoreEnvelopeArithmeticOperForm form = new CoreEnvelopeArithmeticOperForm("jynew", selNode);
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            TreeNode node = treeViewArithmeticList.Nodes[0];

            string key = "CenterofGravityEnvelopeMethod\\" + form.strCoreEnvelopeArithmeticFileName + ".mcem";
            if (!node.Nodes.ContainsKey(key))
            {
                node.Nodes.Add(key, form.strCoreEnvelopeArithmeticFileName, 4, 5);
            }
            if (node.IsExpanded == false)
            {
                node.Expand();
            }

            WeightParameter.GetWeightParameterList()[10].Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult result = MessageBox.Show("是否删除" + selNode.Name + "?", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
                    System.IO.File.Delete(selNode.Name);

                    selNode.Parent.Nodes.Remove(selNode);
                }
            }
            catch
            {
                MessageBox.Show("删除文件出现错误！");
                return;
            }

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog dlg = new OpenFileDialog();
                dlg.RestoreDirectory = true;
                dlg.Filter = "Mcem files (*.mcem)|*.mcem";
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                CoreEnvelopeArithmetic cea = CoreEnvelopeArithmetic.ReadArithmeticData(dlg.FileName); ;
                if (cea == null)
                {
                    return;
                }

                bool boverwrite = false;

                TreeNode node = treeViewArithmeticList.Nodes[0];

                while (true)
                {
                    bool binclude = false;
                    foreach (TreeNode subnode in node.Nodes)
                    {
                        if (subnode.Text == cea.Name)
                        {
                            binclude = true;
                            break;
                        }
                    }
                    if (binclude)
                    {
                        DialogResult dr = MessageBox.Show("已有该算法，是否重命名？\r\n选\"否\"的话将覆盖现有算法。\r\n选\"取消\"则停止导入。", "该算法已存在", MessageBoxButtons.YesNoCancel);
                        if (dr == DialogResult.Yes)
                        {
                            string sortname = Microsoft.VisualBasic.Interaction.InputBox("请输入新的算法名称：", "输入新的算法名称", cea.Name, -1, -1);
                            sortname = sortname.Trim();
                            if (sortname == "")
                            {
                                return;
                            }

                            cea.Name = sortname;
                        }
                        else if (dr == DialogResult.Cancel)
                        {
                            return;
                        }
                        else
                        {
                            boverwrite = true;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }


                if (CoreEnvelopeArithmeticOperForm.WriteArithmeticFile(cea, false))
                {
                    if (!boverwrite)
                    {
                        node.Nodes.Add("CenterofGravityEnvelopeMethod\\" + cea.Name + ".mcem", cea.Name, 4, 5);
                    }
                }

                XCommon.XLog.Write("成功导入算法\"" + cea.Name + "\"！");
            }
            catch (Exception ex)
            {
                XCommon.XLog.Write("导入重心包线算法文件错误." + ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = selNode.Text;

            CoreEnvelopeArithmetic wa = null;

            if (selNode.Tag != null)
            {
                wa = selNode.Tag as CoreEnvelopeArithmetic;
                dlg.FileName = wa.Name;
            }

            dlg.OverwritePrompt = false;
            dlg.DefaultExt = "mcem";
            dlg.Filter = "Mcem files (*.mcem)|*.mcem";
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }


            //从本地导出
            if (selNode.Tag == null)
            {
                if (selNode.Name != dlg.FileName)
                {
                    string strSourcefile = System.AppDomain.CurrentDomain.BaseDirectory + selNode.Name;
                    try
                    {
                        if (File.Exists(dlg.FileName))
                        {
                            if (MessageBox.Show("文件\"" + dlg.FileName + "\"已存在，是否覆盖？", "文件已存在", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                System.IO.File.Copy(strSourcefile, dlg.FileName, true);
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            System.IO.File.Copy(strSourcefile, dlg.FileName, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
            }
            else
            {
                if (!wa.WriteArithmeticFile(dlg.FileName, true))
                {
                    return;
                }
            }

            if (selNode != null)
            {
                //int index = selNode.Name.LastIndexOf("\\");
                //string strSFName = index == -1 ? string.Empty : selNode.Name.Substring(index + 1, selNode.Name.Length - index - 1);

                //int index1 = strSFName.LastIndexOf(".");
                //string strName = index1 == -1 ? string.Empty : strSFName.Substring(0, index1);


                XCommon.XLog.Write("成功导出算法到文件\"" + dlg.FileName + "\"！");
            }
        }

        private void treeViewArithmeticList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selNode = e.Node;

            btnEdit.Enabled = (selNode.ImageIndex == 4) && (selNode.Tag == null);
            btnJYNew.Enabled = (selNode.ImageIndex == 4);
            btnDelete.Enabled = (selNode.ImageIndex == 4) && (selNode.Tag == null);
            btnExport.Enabled = (selNode.ImageIndex == 4);
        }
    }
}
