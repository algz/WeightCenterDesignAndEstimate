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

namespace WeightCenterDesignAndEstimateSoft.Task
{
    public partial class CoreEnvelopeDesignForm : Form
    {
        private Dictionary<string, string> waItems = null;
        public CoreEnvelopeArithmetic curWa = null;
        private List<CExpression> curExprList = null;

        private List<WeightParameter> curWaParas = new List<WeightParameter>();

        private MainForm mainForm = null;
        private bool bEditProject = false;

        public CoreEnvelopeDesignForm()
        {
            InitializeComponent();
            dataGridViewParaInput.Columns[1].ValueType = System.Type.GetType("System.Decimal");
        }

        public CoreEnvelopeDesignForm(MainForm main_Form, CoreEnvelopeArithmetic _coreEnvelope)
        {
            InitializeComponent();
            dataGridViewParaInput.Columns[1].ValueType = System.Type.GetType("System.Decimal");

            mainForm = main_Form;
            curWa = _coreEnvelope;
        }

        public CoreEnvelopeDesignForm(MainForm main_Form)
        {
            InitializeComponent();
            dataGridViewParaInput.Columns[1].ValueType = System.Type.GetType("System.Decimal");

            mainForm = main_Form;
        }

        private void CoreEnvelopeDesignForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 11; ++i)
            {
                tabControl1.TabPages.Add(WeightParameter.ParaTypeList[i]);
            }

            if (curWa == null)
            {
                waItems = CoreEnvelopeAtithmeticManageForm.GetArithmeticItems();
                foreach (KeyValuePair<string, string> items in waItems)
                {
                    cmbWeightMethod.Items.Add(items.Key);
                }
            }
            else
            {
                bEditProject = true;
                txtWeightEstName.Text = curWa.DataName;

                txtWeightEstName.Enabled = false;

                cmbWeightMethod.Items.Add(curWa.Name);
                cmbWeightMethod.SelectedIndex = 0;
                cmbWeightMethod.Enabled = false;
            }
        }

        private void cmbWeightMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bEditProject)
            {
                string itemkey = (string)cmbWeightMethod.SelectedItem;
                curWa = CoreEnvelopeArithmetic.ReadArithmeticData(waItems[itemkey]);
            }
            OnCurWaChanged();
        }

        private void OnCurWaChanged()
        {
            curExprList = new List<CExpression>();

            treeViewClass.Nodes[0].Nodes.Clear();

            string errmsg;
            foreach (NodeFormula nf in curWa.FormulaList)
            {
                treeViewClass.Nodes[0].Nodes.Add(nf.NodeName);

                for (int i = 0; i < 2; ++i)
                {
                    WeightFormula wf = nf[i];
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
            }

            treeViewClass.Nodes[0].Expand();

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
                CExpression expr = curExprList[2 * i];
                foreach (WeightParameter wp in curWa.FormulaList[i].XFormula.ParaList)
                {
                    expr.SetVariableValue(wp.ParaName, wp.ParaValue);
                }
                double xvalue = expr.Run();
                if (double.IsInfinity(xvalue) || double.IsNaN(xvalue))
                {
                    MessageBox.Show("参数值或公式有误！节点: " + curWa.FormulaList[i].NodeName + ".X");
                    return;
                }

                curWa.FormulaList[i].XFormula.Value = xvalue;

                expr = curExprList[2 * i + 1];
                foreach (WeightParameter wp in curWa.FormulaList[i].YFormula.ParaList)
                {
                    expr.SetVariableValue(wp.ParaName, wp.ParaValue);
                }

                double yvalue = expr.Run();
                if (double.IsInfinity(yvalue) || double.IsNaN(yvalue))
                {
                    MessageBox.Show("参数值或公式有误！节点: " + curWa.FormulaList[i].NodeName + ".Y");
                    return;
                }
                curWa.FormulaList[i].YFormula.Value = yvalue;
            }

            // curWa.WriteArithmeticFile("test.xml", true);

            if (mainForm.designProjectData == null)
            {
                CoreDesignProject coreForm = new CoreDesignProject(mainForm, "new");
                coreForm.ShowDialog();
            }

            if (mainForm.designProjectData.lstCoreEnvelopeDesign == null)
            {
                mainForm.designProjectData.lstCoreEnvelopeDesign = new List<Model.CoreEnvelopeArithmetic>();
            }


            Dictionary<WeightParameter, WeightParameter> wpDict = new Dictionary<WeightParameter, WeightParameter>();

            List<WeightFormula> FormulaList = new List<WeightFormula>();
            foreach (NodeFormula ndformula in curWa.FormulaList)
            {
                for (int i = 0; i < 2; ++i)
                {
                    WeightFormula formula = ndformula[i];
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
            }
            if (!bEditProject)
            {
                mainForm.designProjectData.lstCoreEnvelopeDesign.Add(curWa);
                mainForm.BindProjectTreeData(mainForm.designProjectData);
                mainForm.SetCoreEnvelopeDesignTab(curWa, mainForm.designProjectData.lstCoreEnvelopeDesign.Count - 1);
                XLog.Write("设计数据\"" + txtWeightEstName.Text + "\"计算完成！");

            }
            else
            {
                //设置页面数据
                mainForm.SetCoreEnvelopeDesignResult(curWa);
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

            //从xml文件导入
            if (dialog.FileName.EndsWith(".xml") || dialog.FileName.EndsWith(".XML"))
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

            //从excel文件导入
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

            //导出至xml文件
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

            //导出至xml文件
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
            //设置页面数据
            SetPageData(WeightEstimateForm.GetListParaData());
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

        private void btnWriteParaToTDE_Click(object sender, EventArgs e)
        {
            if (PubSyswareCom.IsRuntimeServerStarted())
            {
                List<ParaData> lstTdePara = WeightEstimateForm.GetListParaData();
                if (lstTdePara != null && lstTdePara.Count > 0)
                {
                    foreach (WeightParameter weight in curWaParas)
                    {
                        IEnumerable<ParaData> selection = from para in lstTdePara where weight.ParaName == para.paraName select para;

                        if (selection.Count() == 0)
                        {
                            PubSyswareCom.CreateDoubleParameter(weight.ParaName, weight.ParaValue, true, true, false);
                            MainForm.SetParameterUnit(weight.ParaName, weight.ParaUnit);
                            WeightEstimateForm.SetParaGroup(weight.ParaType, weight.ParaName);
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
                        WeightEstimateForm.SetParaGroup(weight.ParaType, weight.ParaName);
                    }
                }
                MessageBox.Show("参数写入TDE成功!");
            }
        }

        private void CoreEnvelopeDesignForm_KeyDown(object sender, KeyEventArgs e)
        {
            //从数据文件导入参数值
            if (e.KeyCode == Keys.I && e.Modifiers == Keys.Alt)
            {
                btnImportFromXml_Click(null, null);
            }
            //从TDE\IDE导入参数值
            if (e.KeyCode == Keys.P && e.Modifiers == Keys.Alt)
            {
                btnImportFromIde_Click(null, null);
            }
            //导出参数值至数据文件
            if (e.KeyCode == Keys.E && e.Modifiers == Keys.Alt)
            {
                btnExportToXml_Click(null, null);
            }
            //将参数写入TDE\IDE
            if (e.KeyCode == Keys.W && e.Modifiers == Keys.Alt)
            {
                btnWriteParaToTDE_Click(null, null);
            }
            //计算
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Alt)
            {
                btnCompute_Click(null, null);
            }
        }
    }
}
