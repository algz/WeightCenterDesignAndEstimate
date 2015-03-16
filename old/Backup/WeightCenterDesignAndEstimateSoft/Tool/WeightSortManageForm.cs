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
using System.IO;
using System.Xml;

namespace WeightCenterDesignAndEstimateSoft.Tool
{
    public partial class WeightSortManageForm : Form
    {
        #region 属性

        private MainForm mainForm = null;

        static private List<WeightSortData> lstWeightSort = null;
        private WeightSortData weightSort = null;
        private TreeNode selNode = null;

        static private string strPath = System.AppDomain.CurrentDomain.BaseDirectory;

        public string strType = string.Empty;

        private List<WeightSortData> lstWeightSortDesignResult = new List<WeightSortData>();
        private List<WeightSortData> lstWeightSortAdjustResult = new List<WeightSortData>();

        public WeightSortManageForm(MainForm form)
        {
            InitializeComponent();
            lstWeightSort = null;
            mainForm = form;
        }


        private void WeightSortManageForm_Load(object sender, EventArgs e)
        {
            BindWeightSortData();
        }

        #endregion

        #region  自定义方法

        /// <summary>
        /// 绑定重量分类数据
        /// </summary>
        public void BindWeightSortData()
        {
            treeViewWeightSort.Nodes.Clear();
            lstWeightSort = GetListWeightSortData();

            TreeNode rootnode1 = treeViewWeightSort.Nodes.Add("重量分类列表", "重量分类列表");
            if (lstWeightSort != null && lstWeightSort.Count > 0)
            {
                foreach (WeightSortData ws in lstWeightSort)
                {
                    TreeNode node = rootnode1.Nodes.Add(ws.sortName, ws.sortName);
                    node.Tag = ws;
                }
                rootnode1.Expand();
            }

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
                TreeNode rootnode2 = treeViewWeightSort.Nodes.Add("设计结果列表", "设计结果列表");
                foreach (WeightArithmetic wa in dpData.lstWeightArithmetic)
                {
                    WeightSortData ws = wa.MakeNewWeightSort(false);
                    lstWeightSortDesignResult.Add(ws);

                    TreeNode node = rootnode2.Nodes.Add(wa.DataName, wa.DataName);
                    node.Tag = ws;
                }
                rootnode2.Expand();
            }
            if (dpData.lstAdjustmentResultData != null)
            {
                TreeNode rootnode3 = treeViewWeightSort.Nodes.Add("调整结果列表", "调整结果列表");
                foreach (WeightAdjustmentResultData wa in dpData.lstAdjustmentResultData)
                {
                    lstWeightSortDesignResult.Add(wa.basicWeightData);

                    TreeNode node = rootnode3.Nodes.Add(wa.WeightAdjustName, wa.WeightAdjustName);
                    node.Tag = wa.basicWeightData;
                }
                rootnode3.Expand();
            }
        }

        static public bool SaveHccFile(WeightSortData ws, bool bOverWritePrompt)
        {
            string filepath = strPath + "weightCategory\\" + ws.sortName + "\\" + ws.sortName + ".hcc";

            if (bOverWritePrompt)
            {
                if (System.IO.File.Exists(filepath))
                {
                    string msg = "文件\"" + ws.sortName + ".hcc\"" + "已存在，是否继续并覆盖该文件？\r\n如果覆盖可能会导致该分类下的算法文件不可用！";
                    if (MessageBox.Show(msg, "分类文件已存在", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return false;
                    }
                }
            }

            string strWeightSortFolder = strPath + "weightCategory\\" + ws.sortName;
            if (!System.IO.Directory.Exists(strWeightSortFolder))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(strWeightSortFolder);
                }
                catch
                {
                    MessageBox.Show("创建分类目录失败！");
                    return false;
                }
            }

            return SaveHccFile(ws, filepath, false);
        }

        static public bool SaveHccFile(WeightSortData ws, string filepath, bool bOverWritePrompt)
        {
            if (bOverWritePrompt)
            {
                if (System.IO.File.Exists(filepath))
                {
                    string msg = "文件\"" + ws.sortName + ".hcc\"" + "已存在，是否继续并覆盖该文件？";
                    if (MessageBox.Show(msg, "分类文件已存在", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return false;
                    }
                }
            }

            XmlTextWriter writeXml = null;
            try
            {
                writeXml = new XmlTextWriter(filepath, Encoding.GetEncoding("gb2312"));
            }
            catch
            {
                MessageBox.Show("创建或写入文件失败！");
                return false;
            }

            writeXml.Formatting = Formatting.Indented;
            writeXml.Indentation = 5;
            writeXml.WriteStartDocument();

            writeXml.WriteStartElement("重量分类");
            {
                writeXml.WriteStartElement("重量分类名称");
                writeXml.WriteString(ws.sortName);
                writeXml.WriteEndElement();
                writeXml.WriteStartElement("重量分类备注");
                writeXml.WriteString("");
                writeXml.WriteEndElement();

                writeXml.WriteStartElement("重量数据列表");
                {
                    foreach (WeightData wd in ws.lstWeightData)
                    {
                        writeXml.WriteStartElement("重量数据");
                        {
                            writeXml.WriteStartElement("ID");
                            writeXml.WriteValue(wd.nID);
                            writeXml.WriteEndElement();
                            writeXml.WriteStartElement("重量名称");
                            writeXml.WriteString(wd.weightName);
                            writeXml.WriteEndElement();
                            writeXml.WriteStartElement("重量单位");
                            writeXml.WriteString(wd.weightUnit);
                            writeXml.WriteEndElement();
                            writeXml.WriteStartElement("重量数值");
                            writeXml.WriteValue(wd.weightValue);
                            writeXml.WriteEndElement();
                            writeXml.WriteStartElement("备注");
                            writeXml.WriteString(wd.strRemark);
                            writeXml.WriteEndElement();
                            writeXml.WriteStartElement("PARENTID");
                            writeXml.WriteValue(wd.nParentID);
                            writeXml.WriteEndElement();
                        }
                        writeXml.WriteEndElement();
                    }
                }
                writeXml.WriteEndElement();
            }
            writeXml.WriteEndElement();
            writeXml.Close();

            return true;
        }

        static public List<WeightSortData> GetListWeightSortData(bool bForceReread)
        {
            if (lstWeightSort != null && !bForceReread)
            {
                return lstWeightSort;
            }
            else
            {
                lstWeightSort = new List<WeightSortData>();
            }

            string strFileFolder = strPath + "weightCategory";
            if (Directory.Exists(strFileFolder))
            {
                string[] strArrayFolder = Directory.GetDirectories(strFileFolder);

                if (strArrayFolder != null && strArrayFolder.Length > 0)
                {
                    for (int i = 0; i < strArrayFolder.Length; i++)
                    {
                        int index = strArrayFolder[i].LastIndexOf("\\");
                        string strSortName = strArrayFolder[i].Substring(index + 1, strArrayFolder[i].Length - index - 1);
                        string[] strArrayFile = Directory.GetFiles(strArrayFolder[i], strSortName + ".hcc");

                        if (strArrayFile.Length > 0)
                        {
                            lstWeightSort.Add(WeightSortData.GetSortData(strArrayFile[0]).Clone());
                        }

                    }
                }
            }

            return lstWeightSort;
        }

        static public List<WeightSortData> GetListWeightSortData()
        {
            return GetListWeightSortData(false);
        }

        #endregion

        #region  事件

        private void RefreshTreeViewData()
        {
            TreeNode rootnode = treeViewWeightSort.Nodes[0];
            if (rootnode == null)
            {
                return;
            }

            foreach (WeightSortData wsd in lstWeightSort)
            {
                if (!rootnode.Nodes.ContainsKey(wsd.sortName))
                {
                    TreeNode node = rootnode.Nodes.Add(wsd.sortName, wsd.sortName);
                    node.Tag = wsd;
                }
            }

        }

        /// <summary>
        /// 新建事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            WeightSortEditForm form = new WeightSortEditForm(this, "new");
            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshTreeViewData();
            }
        }

        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            weightSort = selNode.Tag as WeightSortData;
            if (weightSort == null)
            {
                return;
            }

            string[] files = System.IO.Directory.GetFiles(strPath + "weightCategory" + "\\" + selNode.Name, "*.WEM");

            string strtype = (files.Length > 0) ? "readOnlyEdit" : "edit";

            WeightSortEditForm form = new WeightSortEditForm(this, weightSort, strtype);
            form.ShowDialog();
        }

        /// <summary>
        /// 基于新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJYNew_Click(object sender, EventArgs e)
        {
            if (selNode == null || selNode.Level == 0)
            {
                XCommon.XLog.Write("请选择重量分类");
                return;
            }

            weightSort = selNode.Tag as WeightSortData;
            if (weightSort == null)
            {
                return;
            }

            WeightSortEditForm form = new WeightSortEditForm(this, weightSort, "JYNew");
            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshTreeViewData();
            }
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strFloder = strPath + "weightCategory" + "\\" + selNode.Name;

            if (!System.IO.Directory.Exists(strFloder))
            {
                MessageBox.Show("目录\"" + strFloder + "\"" + "不存在！");
                return;
            }

            string[] files = System.IO.Directory.GetFiles(strFloder, "*.WEM");

            if (files.Length > 0)
            {
                DialogResult result = MessageBox.Show("该分类下有算法文件！如果继续将把分类下的所有算法文件一起删除！是否继续？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            Directory.Delete(strFloder, true);

            //WeightSortData tempSortData = new WeightSortData();
            //foreach (WeightSortData sortData in lstWeightSort)
            //{
            //    if (sortData.sortName == selNode.Name)
            //    {
            //        tempSortData = sortData;
            //    }
            //}

            WeightSortData tempSortData = selNode.Tag as WeightSortData;
            if (tempSortData == null)
            {
                return;
            }

            lstWeightSort.Remove(tempSortData);

            treeViewWeightSort.Nodes[0].Nodes.Remove(selNode);

            XLog.Write("删除" + selNode.Name + "成功");
        }

        private void treeViewWeightSort_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selNode = e.Node;

            int nFlag = 0;
            if (selNode.Parent != null)
            {
                ++nFlag;
                if (selNode.Parent.Index != 0)
                {
                    ++nFlag;
                }
            }

            //bool benable = (treeViewWeightSort.Nodes[0] == selNode) ? false : true;

            btnEdit.Enabled = (nFlag == 1);
            btnJYNew.Enabled = (nFlag != 0);
            btnDelete.Enabled = (nFlag == 1);
            btnExport.Enabled = (nFlag != 0);
        }

        /// <summary>
        /// 导入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.RestoreDirectory = true;
            fileDialog.Filter = "hcc文件 (*.hcc)|*.hcc";
            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            WeightSortData sortData = WeightSortData.GetSortData(fileDialog.FileName);

            if (sortData == null)
            {
                MessageBox.Show("错误的分类文件!");
                return;
            }

            bool boverwrite = treeViewWeightSort.Nodes[0].Nodes.ContainsKey(sortData.sortName);

            if (!SaveHccFile(sortData, boverwrite))
            {
                XLog.Write("导入失败!");
                return;
            }

            lstWeightSort.Add(sortData);
            if (!boverwrite)
            {
                RefreshTreeViewData();
            }

            XLog.Write("导入成功");

            foreach (TreeNode node in treeViewWeightSort.Nodes[0].Nodes)
            {
                if (node.Text == sortData.sortName)
                {
                    treeViewWeightSort.SelectedNode = node;
                }
            }
        }

        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (selNode == null || selNode.Level == 0)
            {
                XLog.Write("请选择重量分类");
                return;
            }

            //WeightSortData weightSortData = new WeightSortData();

            //foreach (WeightSortData sortData in lstWeightSort)
            //{
            //    if (selNode.Text == sortData.sortName)
            //    {
            //        weightSortData = sortData;
            //    }
            //}

            WeightSortData weightSortData = selNode.Tag as WeightSortData;
            if (weightSortData == null)
            {
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "hcc文件 (*.hcc)|*.hcc";
            dlg.OverwritePrompt = false;
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = weightSortData.sortName;// selNode.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dlg.FileName;
                if (SaveHccFile(weightSortData, strFilePath, true))
                {
                    XLog.Write("导出成功");
                }
            }
        }

        #endregion

        /// <summary>
        /// 双击节点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewWeightSort_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            weightSort = selNode.Tag as WeightSortData;
            if (weightSort == null)
            {
                return;
            }
            string strFolderPath = strPath + "weightCategory" + "\\" + selNode.Name;

            if (Directory.Exists(strFolderPath))
            {
                string[] files = System.IO.Directory.GetFiles(strFolderPath, "*.WEM");
                string strtype = (files.Length > 0) ? "readOnlyEdit" : "edit";

                WeightSortEditForm form = new WeightSortEditForm(this, weightSort, strtype);
                form.ShowDialog();
            }
            else
            {
                XLog.Write("文件\"" + strFolderPath + "\"不存在");
            }
        }
    }
}
