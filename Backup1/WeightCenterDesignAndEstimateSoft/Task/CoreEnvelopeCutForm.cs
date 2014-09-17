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
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml;
using System.IO;
using ZaeroModelSystem;

namespace WeightCenterDesignAndEstimateSoft.Task
{
    public partial class CoreEnvelopeCutForm : Form
    {
        private string DesignName = string.Empty;
        private int nSelIndex = 0;

        private DiscreteSet discreteset = new DiscreteSet();
        public CoreEnvelopeCutResultData olddata = null;
        public CoreEnvelopeCutResultData data = null;

        private ZedGraph.LineItem zedBasicCoreEnvelopeLine = null;
        private ZedGraph.LineItem zedCutCoreEnvelopeLine = null;
        private ZedGraph.CurveList zedDiscreteCurve = new ZedGraph.CurveList();

        //曲线设置
        private string[] zedTitle = new string[] { "原始重心包线", "不可用离散重心点", "可用离散重心点", "剪裁重心包线" };
        private Color[] zedLineColor = new Color[] { Color.Black, Color.Blue, Color.Blue, Color.Red };
        private float[] zedLineWidth = new float[] { 1.0f, 1.0f, 1.0f, 2.0f };
        private ZedGraph.SymbolType[] zedLineSymType = new ZedGraph.SymbolType[] { ZedGraph.SymbolType.Star, ZedGraph.SymbolType.Circle, ZedGraph.SymbolType.Circle, ZedGraph.SymbolType.Diamond };
        private ZedGraph.FillType[] zedLineFillType = new ZedGraph.FillType[] { ZedGraph.FillType.None, ZedGraph.FillType.None, ZedGraph.FillType.GradientByColorValue, ZedGraph.FillType.Solid };

        //燃油剪裁所需
        private double fMaxWeight = 0;
        private double fBaseWeight = 0;
        private List<CorePointData> fuellist = null;
        private List<CorePointData> lstLeftEnvelope = null;
        private List<CorePointData> lstRightEnvelope = null;

        /// <summary>
        /// 离散重心数据
        /// </summary>
        private List<CorePointData> lstCoreEvaluationData = null;

        private MainForm mainForm = null;
        private bool bEditProject = false;

        public Model.CoreEnvelopeDesign coreEnvelope = null;
        public Model.CoreEnvelopeArithmetic coreEnvelopeDesign = null;
        public Model.CoreEnvelopeCutResultData coreEnvelopeCut = null;

        //是否正在拾取点
        private bool bPickup = false;

        public CoreEnvelopeCutForm(MainForm parent)
        {
            InitializeComponent();

            zedGraphControlCoreEnvelope.GraphPane.Title.IsVisible = false;
            zedGraphControlCoreEnvelope.GraphPane.Legend.Border.IsVisible = false;
            zedGraphControlCoreEnvelope.GraphPane.XAxis.Title.Text = "重心位置(mm)";
            zedGraphControlCoreEnvelope.GraphPane.YAxis.Title.Text = "重量(kg)";

            data = new CoreEnvelopeCutResultData(0);

            mainForm = parent;
        }

        public CoreEnvelopeCutForm(MainForm parent, CoreEnvelopeCutResultData _coreCut)
        {
            InitializeComponent();

            zedGraphControlCoreEnvelope.GraphPane.Title.IsVisible = false;
            zedGraphControlCoreEnvelope.GraphPane.Legend.Border.IsVisible = false;
            zedGraphControlCoreEnvelope.GraphPane.XAxis.Title.Text = "重心位置(mm)";
            zedGraphControlCoreEnvelope.GraphPane.YAxis.Title.Text = "重量(kg)";

            mainForm = parent;
            bEditProject = true;

            olddata = _coreCut;

            nSelIndex = olddata.nCutType;
            // 输入修改名称ToolStripMenuItem.Enabled = false;
            //输入修改名称ToolStripMenuItem.Text = _coreCut.cutResultName;
            txtCutName.Enabled = false;
            txtCutName.Text = _coreCut.cutResultName;

            data = new CoreEnvelopeCutResultData(nSelIndex);
            //////
            foreach (CorePointData cpd in olddata.lstBasicCoreEnvelope)
            {
                data.lstBasicCoreEnvelope.Add(new CorePointData(cpd));
            }
            foreach (CorePointData cpd in olddata.lstFuelCore)
            {
                data.lstFuelCore.Add(new CorePointData(cpd));
            }

            foreach (CorePointData cpd in olddata.lstCutEnvelopeCore)
            {
                data.lstCutEnvelopeCore.Add(new CorePointData(cpd));
            }

            if (nSelIndex != 0)
            {
                data.lstCoreEvaluation.AddRange(olddata.lstCoreEvaluation);
            }

            discreteset.nCircularPtCount = _coreCut.nDiscreteCircularPtCount;
            discreteset.nRadialPtCount = _coreCut.nDiscreteRadialPtCount;
            discreteset.fRadialFirstLen = _coreCut.fDiscreteRadialFirstLen;
            discreteset.fRadialRatio = _coreCut.fDiscreteRadialRatio;
            discreteset.fRatioWidthVsHeight = _coreCut.fRatioWidthVsHeight == 0 ? 1 : _coreCut.fRatioWidthVsHeight;
        }

        private void CoreEnvelopeCutForm_Load(object sender, EventArgs e)
        {
            剪裁方式ToolStripMenuItem.SelectedIndex = nSelIndex;

            if (zedCutCoreEnvelopeLine == null)
            {
                for (int i = 0; i < 4; ++i)
                {
                    zedCutCoreEnvelopeLine = new ZedGraph.LineItem(zedTitle[i]);
                    zedCutCoreEnvelopeLine.Color = zedLineColor[i];
                    zedCutCoreEnvelopeLine.Line.Width = zedLineWidth[i];
                    zedCutCoreEnvelopeLine.Symbol.Type = zedLineSymType[i];
                    zedCutCoreEnvelopeLine.Symbol.Fill.Type = zedLineFillType[i];
                    zedGraphControlCoreEnvelope.GraphPane.CurveList.Add(zedCutCoreEnvelopeLine);
                }
            }

            if (bEditProject)
            {
                DesignName = data.cutResultName;
                剪裁方式ToolStripMenuItem.Enabled = false;
                剪裁ToolStripMenuItem.Enabled = true;

                UpdateCoreEnvelopeData();

                foreach (CorePointData cpd in data.lstCutEnvelopeCore)
                {
                    zedCutCoreEnvelopeLine.AddPoint(cpd.pointXValue, cpd.pointYValue);
                }
                UpdateEvaluation();
            }
        }

        private void buttonImportFromDesignData_Click(object sender, EventArgs e)
        {
            //此处加入导入数据代码，把包线存入 data.lstBasicCoreEnvelope
            if (mainForm.designProjectData.lstCoreEnvelopeDesign == null || mainForm.designProjectData.lstCoreEnvelopeDesign.Count == 0)
            {
                MessageBox.Show("当前重心包线没有数据");
                return;
            }
            //Todo
            SelectCoreCutForm cutForm = new SelectCoreCutForm(this, mainForm, "CoreEnvelope");
            cutForm.ShowDialog();

            if (coreEnvelopeDesign != null)
            {
                if (coreEnvelopeDesign.FormulaList != null && coreEnvelopeDesign.FormulaList.Count > 0)
                {
                    if (data.lstBasicCoreEnvelope!=null&&data.lstBasicCoreEnvelope.Count>0)
                    {
                        data.lstBasicCoreEnvelope.Clear();
                    }
                    foreach (Model.NodeFormula formula in coreEnvelopeDesign.FormulaList)
                    {
                        if (data.lstBasicCoreEnvelope == null)
                        {
                            data.lstBasicCoreEnvelope = new List<CorePointData>();
                        }

                        CorePointData pt = new CorePointData();
                        pt.pointName = formula.NodeName;
                        pt.pointXValue = Math.Round(formula.XFormula.Value, 6);
                        pt.pointYValue = Math.Round(formula.YFormula.Value, 6);

                        data.lstBasicCoreEnvelope.Add(pt);
                    }

                    XCommon.XLog.Write("成功从当前设计数据导入\"" + coreEnvelopeDesign.DataName + "\"数据！");
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }

            //成功后，执行以下代码
            UpdateCoreEnvelopeData();
            剪裁ToolStripMenuItem.Enabled = true;
        }

        private void buttonImportFromCutData_Click(object sender, EventArgs e)
        {
            //此处加入导入数据代码，把包线存入 data.lstBasicCoreEnvelope
            //Todo

            if (mainForm.designProjectData.lstCutResultData == null || mainForm.designProjectData.lstCutResultData.Count == 0)
            {
                MessageBox.Show("当前重心包线剪裁没有数据");
                return;
            }

            SelectCoreCutForm cutForm = new SelectCoreCutForm(this, mainForm, "CoreCut");
            cutForm.ShowDialog();

            if (coreEnvelopeCut != null && coreEnvelopeCut != null && coreEnvelopeCut.lstBasicCoreEnvelope.Count > 0 && coreEnvelopeCut.lstCutEnvelopeCore.Count == 0)
            {
                if (data.lstBasicCoreEnvelope == null)
                {
                    data.lstBasicCoreEnvelope = new List<CorePointData>();
                }
                data.lstBasicCoreEnvelope.Clear();

                foreach (CorePointData pt in coreEnvelopeCut.lstBasicCoreEnvelope)
                {
                    CorePointData core = new CorePointData();
                    core.pointName = pt.pointName;
                    core.pointXValue = pt.pointXValue;
                    core.pointYValue = pt.pointYValue;

                    data.lstBasicCoreEnvelope.Add(core);
                }

                XCommon.XLog.Write("当前剪裁结果无剪裁数据,导入\"" + coreEnvelopeCut.cutResultName + "\"基准数据！");
            }

            if (coreEnvelopeCut != null && coreEnvelopeCut != null && coreEnvelopeCut.lstBasicCoreEnvelope.Count > 0 && coreEnvelopeCut.lstCutEnvelopeCore.Count > 0)
            {
                if (data.lstBasicCoreEnvelope == null)
                {
                    data.lstBasicCoreEnvelope = new List<CorePointData>();
                }
                data.lstBasicCoreEnvelope.Clear();

                foreach (CorePointData pt in coreEnvelopeCut.lstCutEnvelopeCore)
                {
                    CorePointData core = new CorePointData();
                    core.pointName = pt.pointName;
                    core.pointXValue = pt.pointXValue;
                    core.pointYValue = pt.pointYValue;

                    data.lstBasicCoreEnvelope.Add(core);
                }

                XCommon.XLog.Write("成功从当前剪裁数据导入\"" + coreEnvelopeCut.cutResultName + "\"数据！");
            }

            //成功后，执行以下代码
            UpdateCoreEnvelopeData();
            剪裁ToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// 从数据库导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonImportFromDatabase_Click(object sender, EventArgs e)
        {
            //此处加入导入数据代码，把包线存入 data.lstBasicCoreEnvelope
            //Todo

            BLL.BLLCoreEnvelopeDesign coreBll = new BLL.BLLCoreEnvelopeDesign();
            List<Model.CoreEnvelopeDesign> lstCoreEnvelope = coreBll.GetListModel();
            if (lstCoreEnvelope == null || lstCoreEnvelope.Count == 0)
            {
                MessageBox.Show("重心包线数据库没有数据");
                return;
            }

            SelectCoreCutForm cutForm = new SelectCoreCutForm(this, "DB");
            cutForm.ShowDialog();

            if (coreEnvelope != null)
            {
                data.lstBasicCoreEnvelope = CoreEnvelopeDesign.GetStringToListCorePointData(coreEnvelope.CoreEnvelope);

                XCommon.XLog.Write("成功从数据库导入\"" + coreEnvelope.DesignData_Name + "\"数据！");
            }

            //成功后，执行以下代码
            UpdateCoreEnvelopeData();
            剪裁ToolStripMenuItem.Enabled = true;
        }

        private void buttonImportFromFile(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel文件 (*.xls,*.xlsx)|*.xls;*.xlsx|xml文件 (*.xml)|*.xml|所有文件 (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //bool IsFormat = IsImorCoreDataFileFormat(dialog.FileName);
                //if (IsFormat == false)
                //{
                //    MessageBox.Show("导入原始重心包线数据格式错误!");
                //    return;
                //}

                if (dialog.FileName.EndsWith(".xls") || dialog.FileName.EndsWith(".xlsx"))
                {
                    if (ImportExcelToPointList(data.lstBasicCoreEnvelope, dialog.FileName))
                    {
                        UpdateCoreEnvelopeData();
                        剪裁ToolStripMenuItem.Enabled = true;
                    }
                }
                else if (dialog.FileName.EndsWith(".xml"))
                {
                    if (ImportXmlToPointList(data.lstBasicCoreEnvelope, dialog.FileName))
                    {
                        UpdateCoreEnvelopeData();
                        剪裁ToolStripMenuItem.Enabled = true;
                        XCommon.XLog.Write("成功从文件导入\"" + dialog.FileName + "\"数据！");
                    }
                }
            }
        }

        private void UpdateCoreEnvelopeData()
        {
            if (data.lstBasicCoreEnvelope != null && data.lstBasicCoreEnvelope.Count > 0)
            {
                if (discreteset.nCircularPtCount < data.lstBasicCoreEnvelope.Count)
                {
                    discreteset.nCircularPtCount = data.lstBasicCoreEnvelope.Count;
                }

                if (!BeClockWiseRotate(data.lstBasicCoreEnvelope))
                {
                    data.lstBasicCoreEnvelope.Reverse();
                }

                fMaxWeight = data.lstBasicCoreEnvelope.Max(bce => bce.pointYValue);
                if (zedBasicCoreEnvelopeLine != null)
                {
                    zedGraphControlCoreEnvelope.GraphPane.CurveList.Remove(zedBasicCoreEnvelopeLine);
                }

                zedBasicCoreEnvelopeLine = AddCurveToZedGraph1(zedGraphControlCoreEnvelope, "", data.lstBasicCoreEnvelope);

                DiscreteCore();

                if (nSelIndex == 0)
                {
                    ResetFuelList();
                }

                zedGraphControlCoreEnvelope.AxisChange();
                zedGraphControlCoreEnvelope.Refresh();
            }
        }

        /// <summary>
        /// 判断导入重心数据文件格式是否正确
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="path"></param>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private bool IsImorCoreDataFileFormat(string strPath)
        {
            bool IsRight = false;

            //重心数据
            if (strPath.EndsWith(".xml"))
            {
                XmlNode node = null;
                XmlDocument doc = new XmlDocument();
                doc.Load(strPath);

                node = doc.SelectSingleNode("重心数据/重心坐标列表");
                if (node != null)
                {
                    IsRight = true;
                }
            }
            if (strPath.EndsWith(".xls") || strPath.EndsWith(".xlsx"))
            {
                ExcelLib OpExcel = new ExcelLib();
                //指定操作的文件
                OpExcel.OpenFileName = strPath;
                //打开文件
                if (OpExcel.OpenExcelFile() == false)
                {
                    return false;
                }
                //取得所有的工作表名称
                string[] strSheetsName = OpExcel.getWorkSheetsName();

                //默认操作第一张表
                OpExcel.SetActiveWorkSheet(1);
                System.Data.DataTable table;
                table = OpExcel.getAllCellsValue();
                OpExcel.CloseExcelApplication();

                if (table.Columns[0].Caption == "重心坐标点名称")
                {
                    IsRight = true;
                }
            }

            return IsRight;
        }

        private void ResetFuelList()
        {
            lstLeftEnvelope = null;
            lstRightEnvelope = null;

            if (data.nCutType == 0 && data.lstFuelCore.Count != 0)
            {
                fuellist = new List<CorePointData>();
                double fMaxFuelWeight = data.lstFuelCore.Max(bce => bce.pointYValue);
                fBaseWeight = fMaxWeight - fMaxFuelWeight;

                foreach (CorePointData cpd in data.lstFuelCore)
                {
                    fuellist.Add(new CorePointData(cpd.pointXValue, cpd.pointYValue + fBaseWeight, cpd.pointName));
                }

                if (ComputeFuelEvaluation())
                {
                    UpdateEvaluation();
                }
            }
        }

        private ZedGraph.LineItem AddCurveToZedGraph(ZedGraph.ZedGraphControl zed, string title, List<CorePointData> ptlst, Color clr, ZedGraph.SymbolType symType, bool bclosed)
        {
            if (ptlst.Count == 0)
            {
                return null;
            }

            int ndet = bclosed ? 1 : 0;
            double[] fx = new double[ptlst.Count + ndet];
            double[] fy = new double[ptlst.Count + ndet];

            for (int i = 0; i < ptlst.Count; ++i)
            {
                fx[i] = ptlst[i].pointXValue;
                fy[i] = ptlst[i].pointYValue;
            }

            if (bclosed)
            {
                fx[ptlst.Count] = ptlst[0].pointXValue;
                fy[ptlst.Count] = ptlst[0].pointYValue;
            }

            return zed.GraphPane.AddCurve(title, fx, fy, clr, symType);
        }

        private ZedGraph.LineItem AddCurveToZedGraph1(ZedGraph.ZedGraphControl zed, string title, List<CorePointData> ptlst)
        {
            return AddCurveToZedGraph(zed, "", ptlst, zedLineColor[0], zedLineSymType[0], true);
        }

        private ZedGraph.LineItem AddCurveToZedGraph2(ZedGraph.ZedGraphControl zed, string title, List<CorePointData> ptlst)
        {
            ZedGraph.LineItem line = AddCurveToZedGraph(zed, "", ptlst, zedLineColor[2], zedLineSymType[2], true);
            line.Line.Fill.Type = ZedGraph.FillType.None;
            line.Symbol.Fill.Type = zedLineFillType[2];
            return line;
        }

        private bool ImportExcelToPointList(List<CorePointData> ptlst, string strfilename)
        {
            ptlst.Clear();

            Excel.Application app = new Excel.ApplicationClass();
            try
            {
                Object missing = System.Reflection.Missing.Value;
                app.Visible = false;
                Excel.Workbook wBook = app.Workbooks.Open(strfilename, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                Excel.Worksheet wSheet = wBook.Worksheets[1] as Excel.Worksheet;

                if (wSheet.Rows.Count > 1)
                {
                    for (int i = 2; i <= wSheet.Rows.Count; ++i)
                    {
                        string cellid = "A" + i.ToString();
                        Excel.Range DataCell = wSheet.get_Range(cellid, cellid);

                        string ParaName = (string)DataCell.Text;

                        DataCell = DataCell.Next.Next.Next;
                        if ((string)DataCell.Text == "")
                        {
                            break;
                        }

                        ParaName = ParaName.Trim();
                        CorePointData node = new CorePointData(ParaName);

                        node.pointXValue = (double)DataCell.Value2;
                        node.pointYValue = (double)DataCell.Next.Value2;

                        ptlst.Add(node);
                    }
                }

                app.Quit();
                app = null;

                XCommon.XLog.Write("成功从文件\"" + strfilename + "\"导入数据！");
            }
            catch (Exception err)
            {
                MessageBox.Show("导入Excel出错！错误原因：" + err.Message, "提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return true;
        }


        private List<string> GetCoreEnvelopeFileContent(List<CorePointData> ptlst)
        {
            List<string> lstContent = new List<string>();

            if (ptlst.Count > 0)
            {
                lstContent.Insert(0, "<?xml version=" + "\"1.0\"" + " encoding=" + "\"gb2312\"" + " ?> ");

                lstContent.Add("<重心数据>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<重心坐标列表>");

                foreach (CorePointData pt in ptlst)
                {
                    lstContent.Add(CommonFunction.mStrModifyToString8(2) + "<重心坐标>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<重心坐标点名称>" + pt.pointName + "</重心坐标点名称>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<横轴单位>毫米</横轴单位>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<纵轴单位>千克</纵轴单位>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<横轴数值>" + pt.pointXValue.ToString() + "</横轴数值>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<纵轴数值>" + pt.pointYValue.ToString() + "</纵轴数值>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(2) + "</重心坐标>");
                }

                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "</重心坐标列表>");
                lstContent.Add("</重心数据>");
            }

            return lstContent;
        }

        private List<CorePointData> GetXmlListCorePointData(string strFilePath)
        {
            List<CorePointData> lstCorePointData = null;
            try
            {

                if (!File.Exists(strFilePath))
                {
                    return lstCorePointData;
                }

                string path = string.Empty;
                XmlNode node = null;

                XmlDocument doc = new XmlDocument();
                doc.Load(strFilePath);

                //重量列表
                path = "重心数据/重心坐标列表";
                node = doc.SelectSingleNode(path);

                XmlNodeList nodeList = node.ChildNodes;

                if (nodeList.Count > 0)
                {
                    lstCorePointData = new List<CorePointData>();

                    foreach (XmlNode childNode in nodeList)
                    {
                        CorePointData pt = new CorePointData();

                        pt.pointName = childNode.ChildNodes[0].InnerText;
                        pt.pointXValue = Convert.ToDouble(childNode.ChildNodes[3].InnerText);
                        pt.pointYValue = Convert.ToDouble(childNode.ChildNodes[4].InnerText);

                        lstCorePointData.Add(pt);
                    }
                }
            }
            catch
            {
                XCommon.XLog.Write("导入原始重心包线数据格式错误");
                MessageBox.Show("导入原始重心包线数据格式错误!");
                return null;
            }
            return lstCorePointData;
        }
        private bool ImportXmlToPointList(List<CorePointData> ptlst, string strfilename)
        {
            //把Xml文件strfilename导入到列表ptlst
            //Todo
            List<CorePointData> lstCorePt = GetXmlListCorePointData(strfilename);

            if (lstCorePt != null && lstCorePt.Count > 0)
            {
                foreach (CorePointData data in lstCorePt)
                {
                    ptlst.Add(data);
                }
                return true;
            }

            return false;
        }

        private CorePointData GetCentroid(List<CorePointData> ptlst)
        {
            if (ptlst == null || ptlst.Count < 3)
            {
                return null;
            }

            double x0 = ptlst[ptlst.Count - 1].pointXValue;
            double y0 = ptlst[ptlst.Count - 1].pointYValue;
            double x1 = ptlst[0].pointXValue;
            double y1 = ptlst[0].pointYValue;

            double fx = (y1 - y0) * (x1 * x1 + x1 * x0 + x0 * x0);
            double fy = (x1 - x0) * (y1 * y1 + y1 * y0 + y0 * y0);
            double fArea = (x1 - x0) * (y1 + y0);

            for (int i = 1; i < ptlst.Count; ++i)
            {
                x0 = ptlst[i - 1].pointXValue;
                y0 = ptlst[i - 1].pointYValue;
                x1 = ptlst[i].pointXValue;
                y1 = ptlst[i].pointYValue;

                fx += (y1 - y0) * (x1 * x1 + x1 * x0 + x0 * x0);
                fy += (x1 - x0) * (y1 * y1 + y1 * y0 + y0 * y0);
                fArea += (x1 - x0) * (y1 + y0);
            }

            fArea *= 3;

            return new CorePointData(-fx / fArea, fy / fArea);
        }

        private bool BeClockWiseRotate(List<CorePointData> ptlst)
        {
            CorePointData centroid = GetCentroid(ptlst);

            if (centroid == null)
            {
                return true;
            }

            for (int i = 1; i < ptlst.Count; ++i)
            {
                double x0 = ptlst[ptlst.Count - 1].pointXValue - centroid.pointXValue;
                double y0 = ptlst[ptlst.Count - 1].pointYValue - centroid.pointYValue;
                double x1 = ptlst[0].pointXValue - centroid.pointXValue;
                double y1 = ptlst[0].pointYValue - centroid.pointYValue;

                double fangle1 = Math.Acos(x0 / (x0 * x0 + y0 * y0));
                if (y0 < 0)
                {
                    fangle1 = 2 * Math.PI - fangle1;
                }
                double fangle2 = Math.Acos(x1 / (x1 * x1 + y1 * y1));
                if (y1 < 0)
                {
                    fangle2 = 2 * Math.PI - fangle2;
                }

                double dangle = fangle2 - fangle1;

                if (Math.Abs(dangle) < 1e-5)
                {
                    continue;
                }
                if (Math.Abs(dangle) > Math.PI)
                {
                    dangle = -dangle;
                }

                return dangle < 0;

            }
            return true;
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            List<string> headers = new List<string> { "重心点名称", "轴向坐标(mm)", "重量(kg)" };
            BindingList<CorePointData> lstPt = new BindingList<CorePointData>();
            foreach (CorePointData cpd in data.lstBasicCoreEnvelope)
            {
                lstPt.Add(new CorePointData(cpd));
            }

            GridViewModifyForm mdform = new GridViewModifyForm(lstPt, headers, 0);
            mdform.Text = "原始重心包线数据修改";

            if (mdform.ShowDialog() == DialogResult.OK)
            {
                data.lstBasicCoreEnvelope.Clear();
                data.lstBasicCoreEnvelope.AddRange(lstPt);
                UpdateCoreEnvelopeData();
            }
        }

        private bool ExportExcelFromPointList(List<CorePointData> ptlst, string strfilename)
        {
            //把列表ptlst导出到Excel文件strfilename
            Excel.Application app = new Excel.ApplicationClass();
            try
            {
                Object missing = System.Reflection.Missing.Value;
                app.Visible = false;
                Excel.Workbook wBook = app.Workbooks.Add(missing);
                //Excel.Workbook wBook = app.Workbooks.Open(strfilename, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                Excel.Worksheet wSheet = wBook.Worksheets[1] as Excel.Worksheet;

                Excel.Range DataCell = wSheet.get_Range("A1", "A1");
                DataCell.Value2 = "重心坐标点名称";
                DataCell = DataCell.Next;
                DataCell.Value2 = "X轴单位";
                DataCell = DataCell.Next;
                DataCell.Value2 = "Y轴单位";
                DataCell = DataCell.Next;
                DataCell.Value2 = "X轴数值";
                DataCell = DataCell.Next;
                DataCell.Value2 = "Y轴数值";

                if (wSheet.Rows.Count > 1)
                {
                    for (int i = 0; i < ptlst.Count; ++i)
                    {
                        string cellid = "A" + (i + 2).ToString();
                        DataCell = wSheet.get_Range(cellid, cellid);

                        DataCell.Value2 = ptlst[i].pointName;
                        DataCell = DataCell.Next;
                        DataCell.Value2 = "毫米";
                        DataCell = DataCell.Next;
                        DataCell.Value2 = "千克";
                        DataCell = DataCell.Next;

                        DataCell.Value2 = ptlst[i].pointXValue;
                        DataCell.Next.Value2 = ptlst[i].pointYValue;
                    }
                }

                app.DisplayAlerts = false;
                app.AlertBeforeOverwriting = false;

                wBook.SaveAs(strfilename, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                app.Quit();
                app = null;

                XCommon.XLog.Write("成功导出数据到文件\"" + strfilename + "\"！");
            }
            catch (Exception err)
            {
                MessageBox.Show("导出数据到Excel文件出错！错误原因：" + err.Message, "提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return true;
        }

        private bool ExportXmlFromPointList(List<CorePointData> ptlst, string strfilename)
        {
            //把列表ptlst导出到Xml文件strfilename
            //Todo
            List<string> lstContent = GetCoreEnvelopeFileContent(ptlst);
            XCommon.CommonFunction.mWriteListStringToFile(strfilename, lstContent);
            return false;
        }

        private void buttonExportToFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel文件 (*.xls,*.xlsx)|*.xls;*.xlsx|xml文件 (*.xml)|*.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileName.EndsWith(".xls") || dialog.FileName.EndsWith(".xlsx"))
                {
                    if (ExportExcelFromPointList(data.lstBasicCoreEnvelope, dialog.FileName))
                    {

                    }
                }
                else if (dialog.FileName.EndsWith(".xml"))
                {
                    if (ExportXmlFromPointList(data.lstBasicCoreEnvelope, dialog.FileName))
                    {

                    }
                }
            }

        }

        private void 导出离散点至数据文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel文件 (*.xls,*.xlsx)|*.xls;*.xlsx|xml文件 (*.xml)|*.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileName.EndsWith(".xls") || dialog.FileName.EndsWith(".xlsx"))
                {
                    if (ExportExcelFromPointList(data.lstDiscreteCore, dialog.FileName))
                    {

                    }
                }
                else if (dialog.FileName.EndsWith(".xml"))
                {
                    if (ExportXmlFromPointList(data.lstDiscreteCore, dialog.FileName))
                    {

                    }
                }
            }
        }

        private void buttonDiscreteSet_Click(object sender, EventArgs e)
        {
            DiscreteSetForm form = new DiscreteSetForm(discreteset);
            if (form.ShowDialog() == DialogResult.OK)
            {
                DiscreteCore();
                if (nSelIndex == 0)
                {
                    if (lstLeftEnvelope != null && lstRightEnvelope != null)
                    {
                        if (ComputeFuelEvaluation())
                        {
                            UpdateEvaluation();
                            return;
                        }
                    }
                }

                zedGraphControlCoreEnvelope.Refresh();
            }
        }

        private void DiscreteCore()
        {
            data.lstDiscreteCore.Clear();
            if (zedDiscreteCurve.Count > 0)
            {
                foreach (ZedGraph.LineItem li in zedDiscreteCurve)
                {
                    zedGraphControlCoreEnvelope.GraphPane.CurveList.Remove(li);
                }
                zedDiscreteCurve.Clear();
            }

            if (discreteset.nRadialPtCount == 0)
            {
                return;
            }

            if (data.lstBasicCoreEnvelope.Count == 0)
            {
                MessageBox.Show("没有输入基础包线数据！");
                return;
            }

            List<CorePointData> CircularPt0 = null;
            if (!CreateCircularPtNums(discreteset.nCircularPtCount, data.lstBasicCoreEnvelope, out CircularPt0, discreteset.fRatioWidthVsHeight))
            {
                MessageBox.Show("基础包线数据错误！");
                return;
            }
            data.lstDiscreteCore.AddRange(CircularPt0);

            ZedGraph.LineItem line = AddCurveToZedGraph2(zedGraphControlCoreEnvelope, "", CircularPt0);

            zedDiscreteCurve.Add(line);
            if (discreteset.nRadialPtCount > 1)
            {
                CorePointData centd = GetCentroid(data.lstBasicCoreEnvelope);
                List<double> ratios = CreateRatioKnots(discreteset.nRadialPtCount, discreteset.fRadialFirstLen, discreteset.fRadialRatio);

                for (int i = 1; i < discreteset.nRadialPtCount; ++i)
                {
                    List<CorePointData> CircularPti = new List<CorePointData>();

                    foreach (CorePointData pt in CircularPt0)
                    {
                        CircularPti.Add(new CorePointData(centd.pointXValue * ratios[i] + pt.pointXValue * (1 - ratios[i]), centd.pointYValue * ratios[i] + pt.pointYValue * (1 - ratios[i])));
                    }
                    line = AddCurveToZedGraph2(zedGraphControlCoreEnvelope, "", CircularPti);

                    zedDiscreteCurve.Add(line);
                    data.lstDiscreteCore.AddRange(CircularPti);
                }
            }
            zedGraphControlCoreEnvelope.AxisChange();
        }

        private List<double> CreateRatioKnots(int num, double first, double ratio)
        {
            List<double> knots = new List<double>();
            if (num == 0)
            {
                return knots;
            }
            knots.Add(0);

            double curLen = first;
            double totallen = 0;
            for (int i = 1; i < num; ++i)
            {
                totallen += curLen;
                knots.Add(totallen);
                curLen *= ratio;

                double frest = (1 - totallen) / (num - i);
                bool bfit = (ratio >= 1) ? curLen < frest : curLen > frest;
                if (!bfit)
                {
                    for (int j = i + 1; j < num; ++j)
                    {
                        totallen += frest;
                        knots.Add(totallen);
                    }
                    break;
                }
            }

            return knots;
        }

        private bool CreateCircularPtNums(int totalnum, List<CorePointData> ptlst, out List<CorePointData> outptlst, double fRatioWidthVsHeight)
        {
            outptlst = null;
            if (ptlst.Count < 3)
            {
                return false;
            }
            outptlst = new List<CorePointData>();

            if (totalnum <= ptlst.Count)
            {
                for (int i = 0; i < ptlst.Count; ++i)
                {
                    outptlst.Add(new CorePointData(ptlst[i]));
                }
            }
            else
            {
                List<double> lens = new List<double>();

                double fx0 = ptlst[ptlst.Count - 1].pointXValue;
                double fy0 = ptlst[ptlst.Count - 1].pointYValue;

                double totallen = 0;
                for (int i = 0; i < ptlst.Count; ++i)
                {
                    double dx = ptlst[i].pointXValue - fx0;
                    fx0 = ptlst[i].pointXValue;
                    double dy = ptlst[i].pointYValue - fy0;
                    fy0 = ptlst[i].pointYValue;

                    double flenth = Math.Sqrt(dx * dx * fRatioWidthVsHeight * fRatioWidthVsHeight + dy * dy);
                    lens.Add(flenth);
                    totallen += flenth;
                }

                lens.Add(lens[0]);
                lens.RemoveAt(0);

                double unitlen = totallen / totalnum;

                fx0 = ptlst[0].pointXValue;
                fy0 = ptlst[0].pointYValue;

                for (int i = 0; i < lens.Count; ++i)
                {
                    double fnum = lens[i] / unitlen;
                    int nref = (int)fnum;
                    if (fnum > (nref * nref + nref) / (nref + 0.5))
                    {
                        ++nref;
                    }


                    if (nref == 0)
                    {
                        continue;
                    }

                    int next = i + 1;

                    if (i == lens.Count - 1)
                    {
                        next = 0;
                    }

                    double dx = (ptlst[next].pointXValue - fx0) / nref;
                    double dy = (ptlst[next].pointYValue - fy0) / nref;
                    for (int j = 0; j < nref; ++j)
                    {
                        outptlst.Add(new CorePointData(fx0 + dx * j, fy0 + dy * j));
                    }

                    fx0 = ptlst[next].pointXValue;
                    fy0 = ptlst[next].pointYValue;
                }

            }
            return true;
        }

        private void buttonImportFuelFromFile(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel文件 (*.xls,*.xlsx)|*.xls;*.xlsx|xml文件 (*.xml)|*.xml|所有文件 (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //导入Excel文件
                if (dialog.FileName.EndsWith(".xls") || dialog.FileName.EndsWith(".xlsx"))
                {
                    if (ImportExcelToPointList(data.lstFuelCore, dialog.FileName))
                    {
                        ResetFuelList();
                    }
                }
                else if (dialog.FileName.EndsWith(".xml"))
                {
                    if (ImportXmlToPointList(data.lstFuelCore, dialog.FileName))
                    {
                        ResetFuelList();
                    }
                }
            }
        }

        private List<int> GetXmlListCoreEvaluationData(string strFilePath)
        {
            lstCoreEvaluationData = null;
            List<int> lstCoreEvaluation = null;

            if (!File.Exists(strFilePath))
            {
                return lstCoreEvaluation;
            }

            string path = string.Empty;
            XmlNode node = null;

            XmlDocument doc = new XmlDocument();
            doc.Load(strFilePath);

            path = "重心离散点评估数据/离散点评估数据列表";
            node = doc.SelectSingleNode(path);

            if (node == null)
            {
                return lstCoreEvaluation;
            }

            XmlNodeList nodeList = node.ChildNodes;

            if (nodeList.Count > 0)
            {
                lstCoreEvaluationData = new List<CorePointData>();
                lstCoreEvaluation = new List<int>();

                int t = -1;
                foreach (XmlNode childNode in nodeList)
                {
                    CorePointData pt = new CorePointData();

                    pt.pointName = childNode.ChildNodes[0].InnerText;
                    pt.pointXValue = Convert.ToDouble(childNode.ChildNodes[3].InnerText);
                    pt.pointYValue = Convert.ToDouble(childNode.ChildNodes[4].InnerText);

                    t = Convert.ToInt32(childNode.ChildNodes[5].InnerText);
                    lstCoreEvaluation.Add(t);

                    lstCoreEvaluationData.Add(pt);
                }
            }
            return lstCoreEvaluation;
        }

        /// <summary>
        /// 获取离散评估数据
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        private List<int> GetExcelCoreEvaluationData(string strFilePath)
        {
            lstCoreEvaluationData = null;
            //评估数据
            List<int> lstCoreEvaluation = null;

            if (File.Exists(strFilePath))
            {
                ExcelLib OpExcel = new ExcelLib();
                //指定操作的文件
                OpExcel.OpenFileName = strFilePath;
                //打开文件
                if (OpExcel.OpenExcelFile() == false)
                {
                    return lstCoreEvaluation;
                }
                //取得所有的工作表名称
                string[] strSheetsName = OpExcel.getWorkSheetsName();

                //默认操作第一张表
                OpExcel.SetActiveWorkSheet(1);
                System.Data.DataTable table;
                //列标题重复
                table = OpExcel.getAllCellsValue();
                OpExcel.CloseExcelApplication();

                int count = table.Rows.Count;

                if (count > 0)
                {
                    lstCoreEvaluationData = new List<CorePointData>();
                    lstCoreEvaluation = new List<int>();

                    int t = -1;
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        CorePointData pt = new CorePointData();
                        pt.pointName = table.Rows[i][0].ToString();
                        pt.pointXValue = Convert.ToDouble(table.Rows[i][3].ToString());
                        pt.pointYValue = Convert.ToDouble(table.Rows[i][4].ToString());

                        t = Convert.ToInt32(table.Rows[i][5].ToString());
                        lstCoreEvaluation.Add(t);
                        lstCoreEvaluationData.Add(pt);
                    }
                }
            }

            return lstCoreEvaluation;
        }

        private void 从文件导入评估数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 导入数据到 data.lstCoreEvaluation
            //Todo
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel文件 (*.xls,*.xlsx)|*.xls;*.xlsx|xml文件 (*.xml)|*.xml";
            dialog.RestoreDirectory = true;
            dialog.FilterIndex = 1;

            List<int> lstCoreEvaluation = null;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                bool IsFormat = IsImorCoreEvaluationDataFormat(dialog.FileName);
                if (IsFormat == false)
                {
                    MessageBox.Show("导入评估数据格式错误");
                    return;
                }

                if (lstCoreEvaluation == null)
                {
                    lstCoreEvaluation = new List<int>();
                }
                //导入Excel文件
                if (dialog.FileName.EndsWith(".xls") || dialog.FileName.EndsWith(".xlsx"))
                {
                    lstCoreEvaluation = GetExcelCoreEvaluationData(dialog.FileName);
                }
                else if (dialog.FileName.EndsWith(".xml"))
                {
                    lstCoreEvaluation = GetXmlListCoreEvaluationData(dialog.FileName);
                }

                //检测导入评估数据和离散点数据  是否匹配:dx*dx+dy*dy<0.00001(1e-5)
                double dx = -1, dy = -1;
                double dis = -1;
                if (lstCoreEvaluationData != null && data.lstDiscreteCore != null)
                {
                    for (int i = 0; i < lstCoreEvaluationData.Count; i++)
                    {
                        dx = Math.Abs(lstCoreEvaluationData[i].pointXValue - data.lstDiscreteCore[i].pointXValue);
                        dy = Math.Abs(lstCoreEvaluationData[i].pointYValue - data.lstDiscreteCore[i].pointYValue);
                        dis = dx * dx + dy * dy;

                        if (dis >= 0.00001)
                        {
                            MessageBox.Show("离散点不匹配");
                            return;
                        }
                    }
                }

                XCommon.XLog.Write("成功从文件导入\"" + dialog.FileName + "\"数据！");
            }

            //导入数据存入 data.lstCoreEvaluation
            if (lstCoreEvaluation != null && lstCoreEvaluation.Count > 0)
            {
                foreach (int d in lstCoreEvaluation)
                {
                    data.lstCoreEvaluation.Add(d);
                }
            }
            // 更新显示
            UpdateEvaluation();
        }

        /// <summary>
        /// 判断导入评估数据文件格式是否正确
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="path"></param>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private bool IsImorCoreEvaluationDataFormat(string strPath)
        {
            bool IsRight = false;

            //重心数据
            if (strPath.EndsWith(".xml"))
            {
                XmlNode node = null;
                XmlDocument doc = new XmlDocument();
                doc.Load(strPath);

                node = doc.SelectSingleNode("重心离散点评估数据/离散点评估数据列表");
                if (node != null)
                {
                    IsRight = true;
                }
            }
            if (strPath.EndsWith(".xls") || strPath.EndsWith(".xlsx"))
            {
                ExcelLib OpExcel = new ExcelLib();
                //指定操作的文件
                OpExcel.OpenFileName = strPath;
                //打开文件
                if (OpExcel.OpenExcelFile() == false)
                {
                    return false;
                }
                //取得所有的工作表名称
                string[] strSheetsName = OpExcel.getWorkSheetsName();

                //默认操作第一张表
                OpExcel.SetActiveWorkSheet(1);
                System.Data.DataTable table;
                table = OpExcel.getAllCellsValue();
                OpExcel.CloseExcelApplication();

                if (table.Columns != null && table.Columns.Count > 0)
                {
                    if (table.Columns[0].Caption == "离散点重心坐标名称")
                    {
                        IsRight = true;
                    }
                }
            }

            return IsRight;
        }

        private bool GetLeftRightEnvelope()
        {
            lstLeftEnvelope = null;
            lstRightEnvelope = null;
            if (data.lstBasicCoreEnvelope.Count == 0)
            {
                MessageBox.Show("没有输入基础包线数据！");
                return false;
            }
            if (data.lstFuelCore.Count == 0)
            {
                MessageBox.Show("没有输入燃油数据！");
                return false;
            }
            List<CorePointData> originleft = new List<CorePointData>(data.lstBasicCoreEnvelope);
            int ishiftbase = 0;
            double fminweight = originleft[0].pointYValue;
            for (int i = 1; i < originleft.Count; ++i)
            {
                if ((fminweight > originleft[i].pointYValue) || (Math.Abs(fminweight - originleft[i].pointYValue) < 1e-5) && (originleft[ishiftbase].pointXValue > originleft[i].pointXValue))
                {
                    fminweight = originleft[i].pointYValue;
                    ishiftbase = i;
                }
            }
            //
            if (ishiftbase != 0)
            {
                for (int i = 0; i < ishiftbase; ++i)
                {
                    originleft.Add(originleft[i]);
                }
                originleft.RemoveRange(0, ishiftbase);
            }

            if (Math.Abs(fminweight - originleft[originleft.Count - 1].pointYValue) < 1e-5)
            {
                for (int i = 2; i < originleft.Count - 1; ++i)
                {
                    if (Math.Abs(fminweight - originleft[i].pointYValue) < 1e-5)
                    {
                        originleft.RemoveRange(i + 1, originleft.Count - i - 1);
                        break;
                    }
                }
            }
            else
            {
                originleft.Add(originleft[0]);
            }

            int nsmax = 1;
            int nemax = 1;
            double fmaxweight = originleft[1].pointYValue;
            for (int i = 2; i < originleft.Count; ++i)
            {
                if (fmaxweight < originleft[i].pointYValue)
                {
                    fmaxweight = originleft[i].pointYValue;
                    nsmax = i;
                    nemax = i;
                }
                else if ((Math.Abs(fmaxweight - originleft[i].pointYValue) < 1e-5) && (originleft[nemax].pointXValue < originleft[i].pointXValue))
                {
                    nemax = i;
                }
            }
            List<CorePointData> originright = new List<CorePointData>(originleft);
            originright.RemoveRange(0, nemax);
            originleft.RemoveRange(nsmax + 1, originleft.Count - nsmax - 1);

            //

            lstLeftEnvelope = new List<CorePointData>();
            lstRightEnvelope = new List<CorePointData>();

            double fMinWeight = originleft[0].pointYValue;

            CorePointData addionalfuel = new CorePointData(fuellist[fuellist.Count - 1].pointXValue, fuellist[fuellist.Count - 1].pointYValue + 0.1);
            fuellist.Add(addionalfuel);

            foreach (CorePointData pt in originleft)
            {
                lstLeftEnvelope.Add(new CorePointData(pt));
            }
            foreach (CorePointData pt in originright)
            {
                lstRightEnvelope.Add(new CorePointData(pt));
            }

            foreach (CorePointData pt in lstLeftEnvelope)
            {
                if (pt.pointYValue > fBaseWeight)
                {
                    for (int i = 1; i < fuellist.Count; ++i)
                    {
                        if (fuellist[i].pointYValue > pt.pointYValue)
                        {
                            double dy1 = pt.pointYValue - fuellist[i - 1].pointYValue;
                            double dy2 = fuellist[i].pointYValue - pt.pointYValue;

                            double fx = (fuellist[i - 1].pointXValue * dy2 + fuellist[i].pointXValue * dy1) / (dy1 + dy2);

                            double newfx = (pt.pointXValue * fBaseWeight + fx * (pt.pointYValue - fBaseWeight)) / pt.pointYValue;

                            if (pt.pointXValue < newfx)
                            {
                                pt.pointXValue = newfx;
                            }

                            break;
                        }
                    }
                }
            }

            foreach (CorePointData pt in lstRightEnvelope)
            {
                if (pt.pointYValue > fBaseWeight)
                {
                    for (int i = 1; i < fuellist.Count; ++i)
                    {
                        if (fuellist[i].pointYValue > pt.pointYValue)
                        {
                            double dy1 = pt.pointYValue - fuellist[i - 1].pointYValue;
                            double dy2 = fuellist[i].pointYValue - pt.pointYValue;

                            double fx = (fuellist[i - 1].pointXValue * dy2 + fuellist[i].pointXValue * dy1) / (dy1 + dy2);

                            double newfx = (pt.pointXValue * fBaseWeight + fx * (pt.pointYValue - fBaseWeight)) / pt.pointYValue;
                            if (pt.pointXValue > newfx)
                            {
                                pt.pointXValue = newfx;
                            }

                            break;
                        }
                    }
                }
            }

            fuellist.RemoveAt(fuellist.Count - 1);
            //
            int nsFuelPt = 0;
            for (; nsFuelPt < fuellist.Count; ++nsFuelPt)
            {
                if (fuellist[nsFuelPt].pointYValue > fMinWeight)
                {
                    break;
                }
            }
            //
            for (int i = nsFuelPt; i < fuellist.Count - 1; ++i)
            {
                double fxs = 0;
                double fxe = 0;

                CorePointData pt1 = new CorePointData(fuellist[i]);
                CorePointData pt2 = new CorePointData(fuellist[i]);

                for (int j = 1; j < originleft.Count; ++j)
                {
                    if (originleft[j].pointYValue > pt1.pointYValue)
                    {
                        double dy1 = pt1.pointYValue - originleft[j - 1].pointYValue;
                        double dy2 = originleft[j].pointYValue - pt1.pointYValue;

                        fxs = (originleft[j - 1].pointXValue * dy2 + originleft[j].pointXValue * dy1) / (dy1 + dy2);

                        if (fxs < pt1.pointXValue)
                        {
                            pt1.pointXValue = (pt1.pointXValue * (pt1.pointYValue - fBaseWeight) + fxs * fBaseWeight) / pt1.pointYValue;
                        }
                        else
                        {
                            pt1.pointXValue = fxs;
                        }

                        break;
                    }
                }

                for (int j = originright.Count - 2; j >= 0; --j)
                {
                    if (originright[j].pointYValue > pt2.pointYValue)
                    {
                        double dy1 = pt2.pointYValue - originright[j + 1].pointYValue;
                        double dy2 = originright[j].pointYValue - pt2.pointYValue;

                        fxe = (originright[j + 1].pointXValue * dy2 + originright[j].pointXValue * dy1) / (dy1 + dy2);

                        if (fxe > pt2.pointXValue)
                        {
                            pt2.pointXValue = (pt2.pointXValue * (pt2.pointYValue - fBaseWeight) + fxe * fBaseWeight) / pt2.pointYValue;
                        }
                        else
                        {
                            pt2.pointXValue = fxe;
                        }

                        break;
                    }
                }

                //
                for (int k = 0; k < lstLeftEnvelope.Count; ++k)
                {
                    if (pt1.pointYValue < lstLeftEnvelope[k].pointYValue)
                    {
                        lstLeftEnvelope.Insert(k, pt1);
                        break;
                    }
                }
                for (int k = lstRightEnvelope.Count - 1; k >= 0; --k)
                {
                    if (pt2.pointYValue < lstRightEnvelope[k].pointYValue)
                    {
                        lstRightEnvelope.Insert(k + 1, pt2);
                        break;
                    }
                }
            }

            lstRightEnvelope.Reverse();

            return true;
        }

        private bool ComputeFuelEvaluation()
        {
            if (lstLeftEnvelope == null || lstRightEnvelope == null)
            {
                if (!GetLeftRightEnvelope())
                {
                    return false;
                }
            }
            data.lstCoreEvaluation.Clear();

            if (data.lstDiscreteCore.Count == 0)
            {
                return false;
            }

            foreach (CorePointData cpd in data.lstDiscreteCore)
            {
                data.lstCoreEvaluation.Add((cpd.pointXValue >= getXValueFromList(cpd.pointYValue, lstLeftEnvelope) && cpd.pointXValue <= getXValueFromList(cpd.pointYValue, lstRightEnvelope)) ? 1 : 0);
            }

            return true;
        }

        private void UpdateEvaluation()
        {
            if (zedDiscreteCurve.Count == 0 || zedDiscreteCurve.Count * (zedDiscreteCurve[0].NPts - 1) != data.lstCoreEvaluation.Count)
            {
                return;
            }

            int ncur = -1;

            for (int i = 0; i < zedDiscreteCurve.Count; ++i)
            {

                for (int j = 0; j < zedDiscreteCurve[0].NPts - 1; ++j)
                {
                    zedDiscreteCurve[i][j].ColorValue = data.lstCoreEvaluation[++ncur];
                    if (j == 0)
                    {
                        zedDiscreteCurve[i][zedDiscreteCurve[0].NPts - 1].ColorValue = zedDiscreteCurve[i][0].ColorValue;
                    }
                }
            }
            zedGraphControlCoreEnvelope.Refresh();
        }

        // lstData 的Y值需从小到大排序
        private double getXValueFromList(double fy, List<CorePointData> lstData)
        {
            int ncount = lstData.Count;
            if (ncount == 0)
            {
                return 0;
            }
            if (ncount == 1)
            {
                return lstData[0].pointXValue;
            }

            if (fy <= lstData[0].pointYValue)
            {
                return lstData[0].pointXValue;
            }
            if (fy >= lstData[ncount - 1].pointYValue)
            {
                return lstData[ncount - 1].pointXValue;
            }

            double fRet = 0;

            for (int i = 1; i < ncount; ++i)
            {
                if (fy < lstData[i].pointYValue)
                {
                    double dy1 = fy - lstData[i - 1].pointYValue;
                    double dy2 = lstData[i].pointYValue - fy;

                    fRet = (lstData[i - 1].pointXValue * dy2 + lstData[i].pointXValue * dy1) / (dy1 + dy2);
                    break;
                }
                if (fy == lstData[i].pointYValue)
                {
                    return lstData[i].pointXValue;
                }
            }

            return fRet;
        }

        private void buttonCutDataModify_Click(object sender, EventArgs e)
        {
            if (nSelIndex == 0)
            {
                List<string> headers = new List<string> { "燃油点名称", "轴向坐标(mm)", "重量(kg)" };
                BindingList<CorePointData> lstPt = new BindingList<CorePointData>();
                foreach (CorePointData cpd in data.lstFuelCore)
                {
                    lstPt.Add(new CorePointData(cpd));
                }
                GridViewModifyForm mdform = new GridViewModifyForm(lstPt, headers, 0);
                mdform.Text = "燃油数据修改";

                if (mdform.ShowDialog() == DialogResult.OK)
                {
                    data.lstFuelCore.Clear();
                    data.lstFuelCore.AddRange(lstPt);
                    ResetFuelList();
                }
            }
            else
            {
                //读入评估数据到 data.lstCoreEvaluation
                List<string> headers = new List<string> { "燃油点轴向坐标(mm)", "燃油点重量(kg)", "评估结果" };
                BindingList<EvaluationData> lstPt = new BindingList<EvaluationData>();
                foreach (CorePointData cpd in data.lstDiscreteCore)
                {
                    lstPt.Add(new EvaluationData(cpd));
                }
                if (data.lstCoreEvaluation.Count == lstPt.Count)
                {
                    for (int i = 0; i < lstPt.Count; ++i)
                    {
                        lstPt[i].bEvalValue = data.lstCoreEvaluation[i];
                    }
                }

                GridViewModifyForm mdform = new GridViewModifyForm(lstPt, headers, 2);
                mdform.Text = "评估数据修改";

                if (mdform.ShowDialog() == DialogResult.OK)
                {
                    data.lstCoreEvaluation.Clear();
                    for (int i = 0; i < lstPt.Count; ++i)
                    {
                        data.lstCoreEvaluation[i] = lstPt[i].bEvalValue;
                    }
                }

                //完成后，刷新显示
                UpdateEvaluation();
            }
        }

        private void 剪裁方式ToolStripMenuItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            nSelIndex = 剪裁方式ToolStripMenuItem.SelectedIndex;

            从文件导入燃油数据ToolStripMenuItem.Enabled = (nSelIndex == 0);
            从文件导入评估数据ToolStripMenuItem.Enabled = (nSelIndex != 0);
        }

        private void 手动点选包线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bPickup = true;
            zedGraphControlCoreEnvelope.IsShowContextMenu = false;

            zedCutCoreEnvelopeLine.AddPoint(new ZedGraph.PointPair(0, 0));
        }

        private bool zedGraphControlCoreEnvelope_MouseDownEvent(ZedGraph.ZedGraphControl sender, MouseEventArgs e)
        {
            if (bPickup)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        {
                            ZedGraph.CurveItem nearcurve;
                            int nIndex;
                            if (zedGraphControlCoreEnvelope.GraphPane.FindNearestPoint(new PointF(e.X, e.Y), out nearcurve, out nIndex))
                            {
                                zedCutCoreEnvelopeLine.AddPoint(new ZedGraph.PointPair(nearcurve[nIndex]));
                                if (zedCutCoreEnvelopeLine.NPts == 1)
                                {
                                    zedCutCoreEnvelopeLine.AddPoint(new ZedGraph.PointPair(nearcurve[nIndex]));
                                }
                                zedGraphControlCoreEnvelope.Refresh();
                            }

                            break;
                        }
                    case MouseButtons.Middle:
                        {
                            bPickup = false;
                            zedGraphControlCoreEnvelope.IsShowContextMenu = true;
                            if (zedCutCoreEnvelopeLine.NPts != 0)
                            {
                                zedCutCoreEnvelopeLine.RemovePoint(zedCutCoreEnvelopeLine.NPts - 1);
                            }
                            if (zedCutCoreEnvelopeLine.NPts > 2)
                            {
                                double fdx = zedCutCoreEnvelopeLine[0].X - zedCutCoreEnvelopeLine[zedCutCoreEnvelopeLine.NPts - 1].X;
                                double fdy = zedCutCoreEnvelopeLine[0].Y - zedCutCoreEnvelopeLine[zedCutCoreEnvelopeLine.NPts - 1].Y;
                                double fdz = zedCutCoreEnvelopeLine[0].Z - zedCutCoreEnvelopeLine[zedCutCoreEnvelopeLine.NPts - 1].Z;

                                if (Math.Abs(fdx) + Math.Abs(fdy) + Math.Abs(fdz) > 1e-5)
                                {
                                    zedCutCoreEnvelopeLine.AddPoint(zedCutCoreEnvelopeLine[0]);
                                }
                                //if (zedCutCoreEnvelopeLine[0] != zedCutCoreEnvelopeLine[zedCutCoreEnvelopeLine.NPts - 1])
                                //{
                                //    zedCutCoreEnvelopeLine.AddPoint(zedCutCoreEnvelopeLine[0]);
                                //}

                            }
                            zedGraphControlCoreEnvelope.Refresh();
                            break;
                        }
                    case MouseButtons.Right:
                        {
                            zedCutCoreEnvelopeLine.RemovePoint(zedCutCoreEnvelopeLine.NPts - 1);
                            zedGraphControlCoreEnvelope.Refresh();
                            break;
                        }
                }

            }
            return default(bool);
        }

        private bool zedGraphControlCoreEnvelope_MouseMoveEvent(ZedGraph.ZedGraphControl sender, MouseEventArgs e)
        {
            if (bPickup)
            {
                if (zedCutCoreEnvelopeLine.NPts != 0)
                {
                    ZedGraph.CurveItem nearcurve;
                    int nIndex;
                    if (zedGraphControlCoreEnvelope.GraphPane.FindNearestPoint(new PointF(e.X, e.Y), out nearcurve, out nIndex))
                    {
                        zedCutCoreEnvelopeLine[zedCutCoreEnvelopeLine.NPts - 1].X = nearcurve[nIndex].X;
                        zedCutCoreEnvelopeLine[zedCutCoreEnvelopeLine.NPts - 1].Y = nearcurve[nIndex].Y;
                    }
                    else if (zedCutCoreEnvelopeLine.NPts > 1)
                    {
                        zedCutCoreEnvelopeLine[zedCutCoreEnvelopeLine.NPts - 1].X = zedCutCoreEnvelopeLine[zedCutCoreEnvelopeLine.NPts - 2].X;
                        zedCutCoreEnvelopeLine[zedCutCoreEnvelopeLine.NPts - 1].Y = zedCutCoreEnvelopeLine[zedCutCoreEnvelopeLine.NPts - 2].Y;
                    }
                    zedGraphControlCoreEnvelope.Refresh();
                }
            }
            return default(bool);
        }

        //private bool SetDesignName()
        //{
        //    string title = (DesignName.Length == 0) ? "输入设计数据名称" : "修改设计数据名称";
        //    string prompt = (DesignName.Length == 0) ? "请输入设计数据名称：\n\n（输入空字符串视为取消）" : "请输入新的设计数据名称：\n\n（输入空字符串视为取消）";

        //    string newname = string.Empty;
        //    while (true)
        //    {
        //        newname = Microsoft.VisualBasic.Interaction.InputBox(prompt, title, DesignName, -1, -1);
        //        if (newname.Length == 0)
        //        {
        //            break;
        //        }
        //        string realname = newname.Trim();
        //        if (realname.Length == 0)
        //        {
        //            MessageBox.Show("设计数据名称没有可视字符！");
        //        }
        //        else
        //        {
        //            if (!XCommon.Verification.IsCheckString(realname))
        //            {
        //                DesignName = realname;
        //                break;
        //            }
        //            else
        //            {
        //                MessageBox.Show("设计数据名称含有非法字符！");
        //            }
        //        }

        //    }

        //    return (newname.Length != 0);
        //}

        private bool SetDesignName()
        {
            string realname = DesignName.Trim();

            if (DesignName.Trim().Length == 0)
            {
                MessageBox.Show("请输入设计数据名称");
                return false;
            }
            else
            {
                if (!XCommon.Verification.IsCheckString(realname))
                {
                    DesignName = realname;
                }
                else
                {
                    MessageBox.Show("设计数据名称含有非法字符！");
                    return false;
                }
            }

            return true;
        }

        private void 输入修改名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetDesignName();
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!bEditProject)
            {
                if (DesignName.Length == 0 || SetDesignName() == false)
                {
                    return;
                }

                data.cutResultName = DesignName;
            }


            data.lstCutEnvelopeCore.Clear();
            for (int i = 0; i < zedCutCoreEnvelopeLine.NPts; ++i)
            {
                data.lstCutEnvelopeCore.Add(new CorePointData(zedCutCoreEnvelopeLine[i].X, zedCutCoreEnvelopeLine[i].Y));
            }

            if (mainForm.designProjectData == null)
            {
                CoreDesignProject coreForm = new CoreDesignProject(mainForm, "new");
                coreForm.ShowDialog();
            }
            if (!bEditProject)
            {
                if (mainForm.designProjectData.lstCutResultData == null)
                {
                    mainForm.designProjectData.lstCutResultData = new List<CoreEnvelopeCutResultData>();
                }

                data.nCutType = nSelIndex;
                data.nDiscreteCircularPtCount = discreteset.nCircularPtCount;
                data.nDiscreteRadialPtCount = discreteset.nRadialPtCount;
                data.fDiscreteRadialFirstLen = discreteset.fRadialFirstLen;
                data.fDiscreteRadialRatio = discreteset.fRadialRatio;
                data.fRatioWidthVsHeight = discreteset.fRatioWidthVsHeight;

                mainForm.designProjectData.lstCutResultData.Add(data);
                mainForm.BindProjectTreeData(mainForm.designProjectData);
                mainForm.SetCoreEnvelopeCutTab(data, mainForm.designProjectData.lstCutResultData.Count - 1);

                XLog.Write("设计数据\"" + DesignName + "\"剪裁完成！");
            }
            else
            {
                //设置页面的数据
                olddata.nDiscreteCircularPtCount = discreteset.nCircularPtCount;
                olddata.nDiscreteRadialPtCount = discreteset.nRadialPtCount;
                olddata.fDiscreteRadialFirstLen = discreteset.fRadialFirstLen;
                olddata.fDiscreteRadialRatio = discreteset.fRadialRatio;
                olddata.fRatioWidthVsHeight = discreteset.fRatioWidthVsHeight;
                olddata.lstBasicCoreEnvelope = data.lstBasicCoreEnvelope;
                olddata.lstCoreEvaluation = data.lstCoreEvaluation;
                olddata.lstDiscreteCore = data.lstDiscreteCore;
                olddata.lstFuelCore = data.lstFuelCore;
                olddata.lstCutEnvelopeCore = data.lstCutEnvelopeCore;

                mainForm.SetCoreEnvelopeCutResult(olddata);
                XLog.Write("设计数据\"" + DesignName + "\"重新剪裁完成！");
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 确认事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!bEditProject)
            {
                DesignName = txtCutName.Text;
                if (SetDesignName() == false)
                {
                    return;
                }

                data.cutResultName = DesignName;
            }


            data.lstCutEnvelopeCore.Clear();
            for (int i = 0; i < zedCutCoreEnvelopeLine.NPts; ++i)
            {
                data.lstCutEnvelopeCore.Add(new CorePointData(zedCutCoreEnvelopeLine[i].X, zedCutCoreEnvelopeLine[i].Y, (i + 1).ToString()));
            }

            if (mainForm.designProjectData == null)
            {
                CoreDesignProject coreForm = new CoreDesignProject(mainForm, "new");
                coreForm.ShowDialog();
            }
            if (!bEditProject)
            {
                if (mainForm.designProjectData.lstCutResultData == null)
                {
                    mainForm.designProjectData.lstCutResultData = new List<CoreEnvelopeCutResultData>();
                }

                data.nCutType = nSelIndex;
                data.nDiscreteCircularPtCount = discreteset.nCircularPtCount;
                data.nDiscreteRadialPtCount = discreteset.nRadialPtCount;
                data.fDiscreteRadialFirstLen = discreteset.fRadialFirstLen;
                data.fDiscreteRadialRatio = discreteset.fRadialRatio;
                data.fRatioWidthVsHeight = discreteset.fRatioWidthVsHeight;

                mainForm.designProjectData.lstCutResultData.Add(data);
                mainForm.BindProjectTreeData(mainForm.designProjectData);
                mainForm.SetCoreEnvelopeCutTab(data, mainForm.designProjectData.lstCutResultData.Count - 1);

                XLog.Write("设计数据\"" + DesignName + "\"剪裁完成！");
            }
            else
            {
                //设置页面的数据
                olddata.nDiscreteCircularPtCount = discreteset.nCircularPtCount;
                olddata.nDiscreteRadialPtCount = discreteset.nRadialPtCount;
                olddata.fDiscreteRadialFirstLen = discreteset.fRadialFirstLen;
                olddata.fDiscreteRadialRatio = discreteset.fRadialRatio;
                olddata.fRatioWidthVsHeight = discreteset.fRatioWidthVsHeight;
                olddata.lstBasicCoreEnvelope = data.lstBasicCoreEnvelope;
                olddata.lstCoreEvaluation = data.lstCoreEvaluation;
                olddata.lstDiscreteCore = data.lstDiscreteCore;
                olddata.lstFuelCore = data.lstFuelCore;
                olddata.lstCutEnvelopeCore = data.lstCutEnvelopeCore;

                mainForm.SetCoreEnvelopeCutResult(olddata);
                XLog.Write("设计数据\"" + DesignName + "\"重新剪裁完成！");
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}