using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XCommon;
using WeightCenterDesignAndEstimateSoft.Tool;
using Model;
using ZstExpression;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml;
using Dev.PubLib;

namespace WeightCenterDesignAndEstimateSoft
{
    public partial class WeightEstimateForm : Form
    {
        private List<Dictionary<string, string>> waItems = null;
        private WeightArithmetic curWa = null;
        private List<CExpression> curExprList = null;
        List<WeightSortData> lstWeightSort = null;
        private List<WeightParameter> curWaParas = new List<WeightParameter>();

        private MainForm mainForm = null;
        private bool bEditProject = false;

        public WeightEstimateForm()
        {
            InitializeComponent();
            dataGridViewParaInput.Columns[1].ValueType = System.Type.GetType("System.Decimal");
        }

        public WeightEstimateForm(MainForm main_Form, WeightArithmetic weight_Arithmetic)
        {
            InitializeComponent();
            dataGridViewParaInput.Columns[1].ValueType = System.Type.GetType("System.Decimal");


            mainForm = main_Form;
            curWa = weight_Arithmetic;
        }

        private void WeightEstimateForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 11; ++i)
            {
                tabControl1.TabPages.Add(WeightParameter.ParaTypeList[i]);
            }


            if (curWa == null)
            {
                lstWeightSort = WeightSortManageForm.GetListWeightSortData();

                if (lstWeightSort.Count > 0)
                {
                    foreach (WeightSortData wsd in lstWeightSort)
                    {
                        cmbWeightClass.Items.Add(wsd);
                    }
                    cmbWeightClass.DisplayMember = "sortName";

                    waItems = WeightArithmeticManageForm.GetArithmeticItems();
                }
            }
            else
            {
                bEditProject = true;
                txtWeightEstName.Text = curWa.DataName;
                lstWeightSort = new List<WeightSortData>();
                lstWeightSort.Add(curWa.MakeNewWeightSort());
                cmbWeightClass.Items.Add(curWa.SortName);
                cmbWeightClass.SelectedIndex = 0;
                txtWeightEstName.Enabled = false;
                cmbWeightClass.Enabled = false;
                cmbWeightMethod.Enabled = false;
            }
        }

        private void cmbWeightClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewParaInput.Rows.Clear();
            cmbWeightMethod.Items.Clear();

            btnCompute.Enabled = false;
            flowLayoutPanelParaImport.Enabled = false;
            flowLayoutPanelParaExport.Enabled = false;

            if (!bEditProject)
            {
                foreach (KeyValuePair<string, string> items in waItems[cmbWeightClass.SelectedIndex])
                {
                    cmbWeightMethod.Items.Add(items);
                }
                cmbWeightMethod.DisplayMember = "Key";
            }
            else
            {
                cmbWeightMethod.Items.Add(curWa.Name);
                cmbWeightMethod.SelectedIndex = 0;
            }

            WeightSortEditForm.BindTreeList(treeViewClass, lstWeightSort[cmbWeightClass.SelectedIndex]);
            treeViewClass.ExpandAll();
        }

        private void cmbWeightMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bEditProject)
            {
                string itemvalue = ((KeyValuePair<string, string>)cmbWeightMethod.SelectedItem).Value;
                curWa = WeightArithmetic.ReadArithmeticData(itemvalue);
            }

            OnCurWaChanged();
        }

        private void OnCurWaChanged()
        {
            curExprList = new List<CExpression>();

            string errmsg;
            foreach (WeightFormula wf in curWa.FormulaList)
            {
                CExpression expr = CExpression.Parse(wf.Formula, out errmsg);
                if (expr == null)
                {
                    string outmsg = "公式\"" + wf.Formula + "\"错误:" + errmsg;
                    XLog.Write(outmsg);
                    MessageBox.Show(outmsg);
                    return;
                }
                curExprList.Add(expr);
            }

            curWaParas = curWa.GetParaList();

            dataGridViewParaInput.Rows.Clear();

            foreach (WeightParameter wp in curWaParas)
            {
                dataGridViewParaInput.Rows.Add(new object[] { wp.ParaName, wp.ParaValue, wp.ParaUnit, WeightParameter.ParaTypeList[wp.ParaType], wp.ParaRemark });
            }

            btnCompute.Enabled = true;
            flowLayoutPanelParaImport.Enabled = true;
            flowLayoutPanelParaExport.Enabled = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl1.SelectedTab.Controls.Add(dataGridViewParaInput);

            if (curWaParas == null)
            {
                return;
            }

            int nSel = tabControl1.SelectedIndex - 1;

            if (nSel != -1)
            {
                for (int i = 0; i < dataGridViewParaInput.Rows.Count; ++i)
                {
                    dataGridViewParaInput.Rows[i].Visible = (curWaParas[i].ParaType == nSel);
                }
            }
            else
            {
                foreach (DataGridViewRow dgvr in dataGridViewParaInput.Rows)
                {
                    dgvr.Visible = true;
                }
            }
        }

        private void dataGridViewParaInput_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("非法数字!");
            e.Cancel = true;
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            if (!bEditProject)
            {
                txtWeightEstName.Text = txtWeightEstName.Text.Trim();
                if (txtWeightEstName.Text.Length == 0)
                {
                    MessageBox.Show("设计数据名称不能为空！");
                    return;
                }

                if (Verification.IsCheckString(txtWeightEstName.Text))
                {
                    MessageBox.Show("设计数据名称含有非法字符");
                    return;
                }
                curWa.DataName = txtWeightEstName.Text;
            }

            for (int i = 0; i < curWa.FormulaList.Count; ++i)
            {
                CExpression expr = curExprList[i];
                foreach (WeightParameter wp in curWa.FormulaList[i].ParaList)
                {
                    expr.SetVariableValue(wp.ParaName, wp.ParaValue);
                }

                double value = expr.Run();
                if (double.IsInfinity(value) || double.IsNaN(value))
                {
                    MessageBox.Show("参数值或公式有误！节点: " + curWa.FormulaList[i].NodePath);
                    return;
                }
                curWa.FormulaList[i].Value = value;
            }


            //curWa.WriteArithmeticFile("test.xml", true);

            if (mainForm.designProjectData == null)
            {
                CoreDesignProject coreForm = new CoreDesignProject(mainForm, "new");
                coreForm.ShowDialog();
            }

            if (mainForm.designProjectData.lstWeightArithmetic == null)
            {
                mainForm.designProjectData.lstWeightArithmetic = new List<WeightArithmetic>();
            }


            //计算
            Dictionary<WeightParameter, WeightParameter> wpDict = new Dictionary<WeightParameter, WeightParameter>();

            List<WeightFormula> FormulaList = new List<WeightFormula>();
            foreach (WeightFormula formula in curWa.FormulaList)
            {
                List<WeightParameter> ParaList = new List<WeightParameter>();
                foreach (WeightParameter para in formula.ParaList)
                {
                    WeightParameter tempWp = null;
                    if (!wpDict.ContainsKey(para))
                    {
                        tempWp = new WeightParameter(para);
                        wpDict.Add(para, tempWp);
                    }
                    else
                    {
                        tempWp = wpDict[para];
                    }
                    ParaList.Add(tempWp);
                }
                formula.ParaList = ParaList;
            }

            if (!bEditProject)
            {
                mainForm.designProjectData.lstWeightArithmetic.Add(curWa);
                mainForm.BindProjectTreeData(mainForm.designProjectData);
                mainForm.SetWeightDesignTab(curWa, mainForm.designProjectData.lstWeightArithmetic.Count - 1);

                XLog.Write("设计数据\"" + txtWeightEstName.Text + "\"计算完成！");
            }
            else
            {
                //刷新页面
                mainForm.SetWeightDesignReuslt(curWa);
                XLog.Write("设计数据\"" + txtWeightEstName.Text + "\"重新计算完成！");
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnImportFromXml_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Xml files (*.xml)|*.xml|Excel files (*.xls)|*.xls|All files (*.*)|*.*";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (dialog.FileName.EndsWith(".xml"))
            {

                XmlDocument xmldoc = new XmlDocument();

                try
                {
                    xmldoc.Load(dialog.FileName);
                }
                catch
                {
                    MessageBox.Show("打开文件错误!");
                    return;
                }


                XmlNode rootnode = xmldoc.SelectSingleNode("参数列表");

                if (rootnode == null)
                {
                    MessageBox.Show("错误的参数文件!");
                    return;
                }

                foreach (XmlNode node in rootnode.ChildNodes)
                {
                    string ParaName = node.ChildNodes[0].InnerText;

                    ParaName = ParaName.Trim();

                    WeightParameter wp = curWaParas.Find(p => p.ParaName == ParaName);
                    if (wp == null)
                    {
                        continue;
                    }
                    if (wp.ParaUnit != node.ChildNodes[1].InnerText.Trim())
                    {
                        continue;
                    }

                    double value = 0;
                    double.TryParse(node.ChildNodes[3].InnerText, out value);
                    wp.ParaValue = value;
                }
                for (int i = 0; i < dataGridViewParaInput.Rows.Count; ++i)
                {
                    dataGridViewParaInput.Rows[i].Cells[1].Value = curWaParas[i].ParaValue.ToString();
                }
            }
            if (dialog.FileName.EndsWith(".xls"))
            {
                Excel.Application app = new Excel.ApplicationClass();
                try
                {
                    Object missing = System.Reflection.Missing.Value;
                    app.Visible = false;
                    Excel.Workbook wBook = app.Workbooks.Open(dialog.FileName, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                    Excel.Worksheet wSheet = wBook.Worksheets[1] as Excel.Worksheet;

                    if (wSheet.Rows.Count > 1)
                    {
                        for (int i = 2; i <= wSheet.Rows.Count; ++i)
                        {
                            string cellid = "A" + i.ToString();
                            Excel.Range DataCell = wSheet.get_Range(cellid, cellid);

                            string ParaName = (string)DataCell.Text;

                            ParaName = ParaName.Trim();
                            if (ParaName == "")
                            {
                                break;
                            }

                            WeightParameter wp = curWaParas.Find(p => p.ParaName == ParaName);
                            if (wp == null)
                            {
                                continue;
                            }
                            if (wp.ParaUnit != (string)DataCell.Next.Text)
                            {
                                continue;
                            }

                            wp.ParaValue = (double)DataCell.Next.Next.Value2;
                        }
                    }

                    for (int i = 0; i < dataGridViewParaInput.Rows.Count; ++i)
                    {
                        dataGridViewParaInput.Rows[i].Cells[1].Value = curWaParas[i].ParaValue.ToString();
                    }

                    app.Quit();
                    app = null;
                }
                catch (Exception err)
                {
                    MessageBox.Show("导入Excel出错！错误原因：" + err.Message, "提示信息",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            XLog.Write("从文件\"" + dialog.FileName + "\"导入参数值成功！");
        }

        private void btnExportToXml_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Xml files (*.xml)|*.xml|Excel files (*.xls)|*.xls";
            dialog.OverwritePrompt = true;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (dialog.FileName.EndsWith(".xml"))
            {

                XmlDocument xmldoc = new XmlDocument();
                XmlTextWriter writeXml = null;
                try
                {
                    writeXml = new XmlTextWriter(dialog.FileName, Encoding.GetEncoding("gb2312"));
                }
                catch
                {
                    MessageBox.Show("创建或写入文件失败！");
                    return;
                }

                writeXml.Formatting = Formatting.Indented;
                writeXml.Indentation = 5;
                writeXml.WriteStartDocument();

                writeXml.WriteStartElement("参数列表");
                {
                    foreach (WeightParameter wp in curWaParas)
                    {
                        writeXml.WriteStartElement("参数");
                        {
                            writeXml.WriteStartElement("参数名称");
                            writeXml.WriteString(wp.ParaName);
                            writeXml.WriteEndElement();
                        }
                        {
                            writeXml.WriteStartElement("参数单位");
                            writeXml.WriteString(wp.ParaUnit);
                            writeXml.WriteEndElement();
                        }
                        {
                            writeXml.WriteStartElement("参数类型");
                            writeXml.WriteValue(wp.ParaType);
                            writeXml.WriteEndElement();
                        }
                        {
                            writeXml.WriteStartElement("参数数值");
                            writeXml.WriteValue(wp.ParaValue);
                            writeXml.WriteEndElement();
                        }
                        {
                            writeXml.WriteStartElement("参数备注");
                            writeXml.WriteString(wp.ParaRemark);
                            writeXml.WriteEndElement();
                        }
                        writeXml.WriteEndElement();
                    }
                }
                writeXml.WriteEndElement();
                writeXml.Close();
            }

            if (dialog.FileName.EndsWith(".xls"))
            {
                Excel.Application app = new Excel.ApplicationClass();
                try
                {
                    Object missing = System.Reflection.Missing.Value;
                    app.Visible = false;

                    Excel.Workbook wBook = app.Workbooks.Add(missing);

                    Excel.Worksheet wSheet = wBook.Worksheets[1] as Excel.Worksheet;

                    Excel.Range DataCell = wSheet.get_Range("A1", "A1");
                    DataCell.Value2 = "参数名称";
                    DataCell.Next.Value2 = "参数单位";
                    DataCell.Next.Next.Value2 = "参数数值";

                    for (int i = 0; i < curWaParas.Count; ++i)
                    {
                        WeightParameter wp = curWaParas[i];

                        string cellid = "A" + (i + 2).ToString();
                        DataCell = wSheet.get_Range(cellid, cellid);
                        DataCell.Value2 = wp.ParaName;
                        DataCell.Next.Value2 = wp.ParaUnit;
                        DataCell.Next.Next.Value2 = wp.ParaValue;
                    }

                    //设置禁止弹出保存和覆盖的询问提示框    
                    app.DisplayAlerts = false;
                    app.AlertBeforeOverwriting = false;
                    //保存工作簿    
                    wBook.SaveAs(dialog.FileName, Excel.XlFileFormat.xlWorkbookNormal, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlNoChange, missing, missing, missing, missing, missing);
                    wBook.Close(false, missing, missing);
                    app.Quit();
                    app = null;

                }
                catch (Exception err)
                {
                    MessageBox.Show("导出Excel出错！错误原因：" + err.Message, "提示信息",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }


            XLog.Write("成功导出参数值到文件\"" + dialog.FileName + "\"！");
        }

        private void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Excel.Application app = new Excel.ApplicationClass();
            try
            {
                Object missing = System.Reflection.Missing.Value;
                app.Visible = false;
                Excel.Workbook wBook = app.Workbooks.Open(dialog.FileName, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                Excel.Worksheet wSheet = wBook.Worksheets[1] as Excel.Worksheet;

                if (wSheet.Rows.Count > 1)
                {
                    for (int i = 2; i <= wSheet.Rows.Count; ++i)
                    {
                        string cellid = "A" + i.ToString();
                        Excel.Range DataCell = wSheet.get_Range(cellid, cellid);

                        string ParaName = (string)DataCell.Text;

                        ParaName = ParaName.Trim();
                        if (ParaName == "")
                        {
                            break;
                        }

                        WeightParameter wp = curWaParas.Find(p => p.ParaName == ParaName);
                        if (wp == null)
                        {
                            continue;
                        }
                        if (wp.ParaUnit != (string)DataCell.Next.Text)
                        {
                            continue;
                        }

                        wp.ParaValue = (double)DataCell.Next.Next.Value2;
                    }
                }

                for (int i = 0; i < dataGridViewParaInput.Rows.Count; ++i)
                {
                    dataGridViewParaInput.Rows[i].Cells[1].Value = curWaParas[i].ParaValue.ToString();
                }

                app.Quit();
                app = null;

                XLog.Write("从文件\"" + dialog.FileName + "\"导入参数值成功！");
            }
            catch (Exception err)
            {
                MessageBox.Show("导入Excel出错！错误原因：" + err.Message, "提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
            dialog.OverwritePrompt = true;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Excel.Application app = new Excel.ApplicationClass();
            try
            {
                Object missing = System.Reflection.Missing.Value;
                app.Visible = false;

                Excel.Workbook wBook = app.Workbooks.Add(missing);

                Excel.Worksheet wSheet = wBook.Worksheets[1] as Excel.Worksheet;

                Excel.Range DataCell = wSheet.get_Range("A1", "A1");
                DataCell.Value2 = "参数名称";
                DataCell.Next.Value2 = "参数单位";
                DataCell.Next.Next.Value2 = "参数数值";

                for (int i = 0; i < curWaParas.Count; ++i)
                {
                    WeightParameter wp = curWaParas[i];

                    string cellid = "A" + (i + 2).ToString();
                    DataCell = wSheet.get_Range(cellid, cellid);
                    DataCell.Value2 = wp.ParaName;
                    DataCell.Next.Value2 = wp.ParaUnit;
                    DataCell.Next.Next.Value2 = wp.ParaValue;
                }

                //设置禁止弹出保存和覆盖的询问提示框    
                app.DisplayAlerts = false;
                app.AlertBeforeOverwriting = false;
                //保存工作簿    
                wBook.SaveAs(dialog.FileName, Excel.XlFileFormat.xlWorkbookNormal, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlNoChange, missing, missing, missing, missing, missing);
                wBook.Close(false, missing, missing);
                app.Quit();
                app = null;
                XLog.Write("成功导出参数值到文件\"" + dialog.FileName + "\"！");
            }
            catch (Exception err)
            {
                MessageBox.Show("导出Excel出错！错误原因：" + err.Message, "提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImportFromIde_Click(object sender, EventArgs e)
        {
            SetPageData(GetListParaData());
        }

        private void dataGridViewParaInput_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            string text = dataGridViewParaInput.Rows[i].Cells[1].Value.ToString();
            if (text == "")
            {
                dataGridViewParaInput.Rows[i].Cells[1].Value = "0";
                curWaParas[i].ParaValue = 0;
            }
            else
            {
                curWaParas[i].ParaValue = double.Parse(text);
            }
        }

        /// <summary>
        /// 设置页面数据
        /// </summary>
        /// <param name="lstPara"></param>
        private void SetPageData(List<ParaData> lstPara)
        {
            if (lstPara != null && lstPara.Count > 0)
            {
                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    DataGridView gridResult = new DataGridView();
                    if (tabControl1.TabPages[i].Controls.Count > 0)
                    {
                        gridResult = (DataGridView)tabControl1.TabPages[i].Controls[0];
                    }

                    if (gridResult.Rows.Count > 0)
                    {
                        for (int j = 0; j < gridResult.Rows.Count; j++)
                        {
                            foreach (ParaData data in lstPara)
                            {
                                if (data.paraName == gridResult.Rows[j].Cells[0].Value.ToString())
                                {
                                    gridResult.Rows[j].Cells[1].Value = data.paraValue;
                                    curWaParas[j].ParaValue = data.paraValue;
                                    break;
                                }
                            }
                        }
                    }
                }
                XLog.Write("从IDE导入参数值成功");
            }
        }

        /// <summary>
        /// 获取参数表数据转换成ListParaData
        /// </summary>
        /// <returns></returns>
        public static List<ParaData> GetListParaData()
        {
            List<ParaData> lstPara = null;
            if (PubSyswareCom.IsRuntimeServerStarted())
            {
                //获取参数的值
                object obj = null;
                PubSyswareCom.GetParameterNames(string.Empty, ref obj);

                //转换为字符串数组
                if (obj is Object[])
                {
                    lstPara = new List<ParaData>();

                    object[] objName = obj as Object[];
                    for (int i = 0; i < objName.Count(); i++)
                    {
                        Object objValue = null;
                        PubSyswareCom.mGetParameter(string.Empty, objName[i].ToString(), ref objValue);

                        if (objValue != null)
                        {
                            //参数
                            if (objName[i].ToString() != "weightData" && objName[i].ToString() != "coreEnvelopeData")
                            {
                                ParaData data = new ParaData();
                                data.paraName = objName[i].ToString();
                                data.paraValue = Convert.ToDouble(objValue.ToString());

                                lstPara.Add(data);
                            }
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("TDE/IDE 没有启动成功");
            }

            return lstPara;
        }

        public static void SetParaGroup(int paraType, string paraName)
        {
            if (paraType == 0)
            {
                PubSyswareCom.SetParameterGroup(paraName, "指标参数");
            }
            if (paraType == 1)
            {
                PubSyswareCom.SetParameterGroup(paraName, "构型和总体参数");
            }
            if (paraType == 2)
            {
                PubSyswareCom.SetParameterGroup(paraName, "旋翼参数");
            }
            if (paraType == 3)
            {
                PubSyswareCom.SetParameterGroup(paraName, "机身翼面参数");
            }
            if (paraType == 4)
            {
                PubSyswareCom.SetParameterGroup(paraName, "着陆装置参数");
            }
            if (paraType == 5)
            {
                PubSyswareCom.SetParameterGroup(paraName, "动力系统参数");
            }
            if (paraType == 6)
            {
                PubSyswareCom.SetParameterGroup(paraName, "传动系统参数");
            }
            if (paraType == 7)
            {
                PubSyswareCom.SetParameterGroup(paraName, "操纵系统参数");
            }
            if (paraType == 8)
            {
                PubSyswareCom.SetParameterGroup(paraName, "人工参数");
            }
            if (paraType == 9)
            {
                PubSyswareCom.SetParameterGroup(paraName, "其他类型参数");
            }
            if (paraType == 10)
            {
                PubSyswareCom.SetParameterGroup(paraName, "临时参数");
            }
        }

        /// <summary>
        /// 设置参数单位
        /// </summary>
        /// <param name="paraName"></param>
        /// <param name="strUnit"></param>
        public static void SetParaUnit(string paraName, string strUnit)
        {
        }

        private void btnWriteParaToTde_Click(object sender, EventArgs e)
        {
            if (PubSyswareCom.IsRuntimeServerStarted())
            {
                List<ParaData> lstTdePara = GetListParaData();
                if (lstTdePara != null && lstTdePara.Count > 0)
                {
                    foreach (WeightParameter weight in curWaParas)
                    {
                        IEnumerable<ParaData> selection = from para in lstTdePara where (weight.ParaName == para.paraName) select para;

                        if (selection.Count() == 0)
                        {
                            PubSyswareCom.CreateDoubleParameter(weight.ParaName, weight.ParaValue, true, true, false);
                            MainForm.SetParameterUnit(weight.ParaName, weight.ParaUnit);
                            SetParaGroup(weight.ParaType, weight.ParaName);
                        }
                        else
                        {
                            PubSyswareCom.mSetParameter(string.Empty, weight.ParaName, weight.ParaValue);
                        }
                    }
                }
                else
                {
                    foreach (WeightParameter weight in curWaParas)
                    {
                        PubSyswareCom.CreateDoubleParameter(weight.ParaName, weight.ParaValue, true, true, false);
                        MainForm.SetParameterUnit(weight.ParaName, weight.ParaUnit);
                        SetParaGroup(weight.ParaType, weight.ParaName);
                    }
                }
                MessageBox.Show("参数写入TDE成功!");
            }
            else
            {
                MessageBox.Show("TDE/IDE 没有启动成功");
            }
        }

    }
}