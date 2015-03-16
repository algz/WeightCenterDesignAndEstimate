using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using Model;
using System.IO;

namespace WeightCenterDesignAndEstimateSoft.Tool
{
    public partial class WeightArithmeticManageForm : Form
    {
        private TreeNode selNode = null;

        static private string strPath = System.AppDomain.CurrentDomain.BaseDirectory;

        private MainForm mainForm = null;

        public WeightArithmeticManageForm(MainForm form)
        {
            InitializeComponent();
            mainForm = form;
            WeightParameter.ResetWeightParameterList();
        }

        static public List<Dictionary<string, string>> GetArithmeticItems()
        {
            List<Dictionary<string, string>> waItem = new List<Dictionary<string, string>>();

            List<WeightSortData> wsDataList = WeightSortManageForm.GetListWeightSortData(false);

            System.IO.Directory.SetCurrentDirectory(strPath);

            foreach (WeightSortData wsd in wsDataList)
            {
                Dictionary<string, string> waDict = new Dictionary<string, string>();
                waItem.Add(waDict);

                string dirname = "weightCategory\\" + wsd.sortName;
                string[] files = System.IO.Directory.GetFiles(dirname, "*.WEM");
                for (int i = 0; i < files.Length; ++i)
                {
                    string strname = files[i];
                    strname = strname.Substring(strname.LastIndexOf('\\') + 1);
                    strname = strname.Substring(0, strname.Length - 4);
                    waDict.Add(strname, files[i]);
                }
            }

            return waItem;
        }

        private void WeightArithmeticManageForm_Load(object sender, EventArgs e)
        {
            treeViewArithmeticList.ImageList = imageListTreeView;

            //if (!System.IO.Directory.Exists("weightCategory"))
            //{
            //    MessageBox.Show("不存在 weightCategory 目录！");
            //    Close();
            //    return;
            //}

            List<Dictionary<string, string>> waItems = GetArithmeticItems();

            TreeNode rootnode1 = treeViewArithmeticList.Nodes.Add("weightCategory", "重量分类", 0, 1);

            List<WeightSortData> wsDataList = WeightSortManageForm.GetListWeightSortData();

            if (wsDataList.Count > 0)
            {
                for (int i = 0; i < wsDataList.Count; ++i)
                {
                    TreeNode subnode = rootnode1.Nodes.Add("weightCategory\\" + wsDataList[i].sortName, wsDataList[i].sortName, 2, 3);

                    foreach (KeyValuePair<string, string> pair in waItems[i])
                    {
                        subnode.Nodes.Add(pair.Value, pair.Key, 4, 5);
                    }
                }
            }
            else
            {
                btnNew.Enabled = false;
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
            if (dpData.lstWeightArithmetic != null)
            {
                TreeNode rootnode2 = treeViewArithmeticList.Nodes.Add("设计结果列表", "设计结果列表", 0, 1);
                foreach (WeightArithmetic wa in dpData.lstWeightArithmetic)
                {
                    TreeNode node = rootnode2.Nodes.Add(wa.DataName, wa.DataName, 4, 5);
                    node.Tag = wa;
                }
                rootnode2.Expand();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            WeightArithmeticOperForm form = new WeightArithmeticOperForm("new", selNode);
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (TreeNode node in treeViewArithmeticList.Nodes[0].Nodes)
            {
                if (node.Text == form.strWeightSortName)
                {
                    string key = node.Name + "\\" + form.strWeightArithmeticFileName + ".wem";
                    if (!node.Nodes.ContainsKey(key))
                    {
                        node.Nodes.Add(key, form.strWeightArithmeticFileName, 4, 5);
                    }
                    if (node.IsExpanded == false)
                    {
                        node.Expand();
                    }
                    break;
                }
            }

            WeightParameter.GetWeightParameterList()[10].Clear();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            WeightArithmeticOperForm form = new WeightArithmeticOperForm("edit", selNode);
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            WeightParameter.GetWeightParameterList()[10].Clear();
        }

        private void btnJYNew_Click(object sender, EventArgs e)
        {
            if (treeViewArithmeticList.Nodes.Count > 1 && selNode.Parent == treeViewArithmeticList.Nodes[1])
            {
                WeightArithmetic wa = (WeightArithmetic)selNode.Tag;

                if (!QueryWeightSort(wa))
                {
                    return;
                }
            }

            WeightArithmeticOperForm form = new WeightArithmeticOperForm("jynew", selNode);
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            foreach (TreeNode node in treeViewArithmeticList.Nodes[0].Nodes)
            {
                if (node.Text == form.strWeightSortName)
                {
                    node.Nodes.Add(node.Name + "\\" + form.strWeightArithmeticFileName + ".wem", form.strWeightArithmeticFileName, 4, 5);
                    if (node.IsExpanded == false)
                    {
                        node.Expand();
                    }
                    break;
                }
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
                    System.IO.Directory.SetCurrentDirectory(strPath);
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
                dlg.Filter = "Wem files (*.wem)|*.wem";
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                List<WeightSortData> wsDataList = WeightSortManageForm.GetListWeightSortData(false);

                WeightArithmetic wa = WeightArithmetic.ReadArithmeticData(dlg.FileName);
                WeightParameter.GetWeightParameterList()[10].Clear();

                if (!QueryWeightSort(wa))
                {
                    return;
                }

                if (!WeightArithmeticOperForm.WriteArithmeticFile(wa, true))
                {
                    return;
                }
                //加节点
                foreach (TreeNode treenode in treeViewArithmeticList.Nodes[0].Nodes)
                {
                    if (treenode.Text == wa.SortName)
                    {
                        string filename = "weightCategory\\" + wa.SortName + "\\" + wa.Name + ".wem";
                        if (!treenode.Nodes.ContainsKey(filename))
                        {
                            treenode.Nodes.Add(filename, wa.Name, 4, 5);
                            if (treenode.IsExpanded == false)
                            {
                                treenode.Expand();
                            }
                        }
                        break;
                    }
                }
            }
            catch 
            {
                XCommon.XLog.Write("导入重量算法文件错误");
            }
        }

        private bool QueryWeightSort(WeightArithmetic wa)
        {
            List<WeightSortData> wsDataList = WeightSortManageForm.GetListWeightSortData(false);

            int nindex = -1;
            for (nindex = wsDataList.Count - 1; nindex >= 0; --nindex)
            {
                if (wsDataList[nindex].sortName == wa.SortName)
                {
                    break;
                }
            }

            bool bHasSort = false;
            int nfit = 0;
            if (nindex != -1)
            {
                bHasSort = true;

                if (wa.MatchWeightSort(wsDataList[nindex], false))
                {
                    nfit = 1;
                }
            }
            if (nfit == 0)
            {
                for (int i = 0; i < wsDataList.Count; ++i)
                {
                    if (i == nindex)
                    {
                        continue;
                    }

                    if (wa.MatchWeightSort(wsDataList[i], false))
                    {
                        string Message = bHasSort ? ("算法文件未能和分类\"" + wa.SortName + "\"匹配，但和分类\"") : ("分类\"" + wa.SortName + "\"不存在，但算法文件和分类\"");
                        DialogResult dr = MessageBox.Show(Message + wsDataList[i].sortName + "\"匹配。是否导入到分类\"" + wsDataList[i].sortName + "\"？", "分类不匹配", MessageBoxButtons.YesNoCancel);
                        if (dr == DialogResult.Cancel)
                        {
                            return false;
                        }
                        else if (dr == DialogResult.Yes)
                        {
                            wa.SortName = wsDataList[i].sortName;
                            nindex = i;
                            nfit = 2;
                            break;
                        }
                        else
                        {
                            nfit = 0;
                        }
                    }
                }

                // 0 建分类
                // 1 符合
                // 2 在其它分类找到

                if (nfit == 0)
                {
                    DialogResult dr = MessageBox.Show("算法未找到匹配分类，是否新建分类？", "是否建分类", MessageBoxButtons.YesNo);
                    if (dr != DialogResult.Yes)
                    {
                        return false;
                    }
                    // 建分类

                    if (bHasSort)
                    {
                        string strmsg = "请输入分类名称";
                        bool bcontinue = true;
                        while (bcontinue)
                        {
                            string sortname = Microsoft.VisualBasic.Interaction.InputBox(strmsg, "输入名称", wa.SortName, -1, -1);
                            sortname = sortname.Trim();
                            if (sortname == "")
                            {
                                return false;
                            }
                            bcontinue = false;
                            foreach (WeightSortData wsditem in wsDataList)
                            {
                                if (wsditem.sortName == sortname)
                                {
                                    bcontinue = true;
                                    strmsg = "分类\"" + sortname + "\"已存在!\r\n请重新输入分类名称";
                                }
                            }
                            if (!bcontinue)
                            {
                                wa.SortName = sortname;
                            }
                        }
                    }

                    WeightSortData wsd = wa.MakeNewWeightSort();
                    if (!WeightSortManageForm.SaveHccFile(wsd, false))
                    {
                        return false;
                    }
                    wsDataList.Add(wsd);

                    treeViewArithmeticList.Nodes[0].Nodes.Add("weightCategory\\" + wa.SortName, wa.SortName, 2, 3);
                }
            }

            return true;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = selNode.Text;

            WeightArithmetic wa = null;
            if (selNode.Tag != null)
            {
                wa = selNode.Tag as WeightArithmetic;
                dlg.FileName = wa.Name;
            }

            dlg.OverwritePrompt = false;
            dlg.DefaultExt = "wem";
            dlg.Filter = "Wem files (*.wem)|*.wem";
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

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
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
            }
            else
            {
                wa = selNode.Tag as WeightArithmetic;
                if (!wa.WriteArithmeticFile(dlg.FileName, true))
                {
                    return;
                }
            }

            XCommon.XLog.Write("成功导出算法到文件\"" + dlg.FileName + "\"！");
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
