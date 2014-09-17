using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeightCenterDesignAndEstimateSoft.Task.WeightAdjustmentSubforms;
using System.Xml;
using System.IO;
using Model;
using WeightCenterDesignAndEstimateSoft.Tool;
using XCommon;
using ZaeroModelSystem;
using ZedGraph;
using System.Windows.Forms.DataVisualization.Charting;

namespace WeightCenterDesignAndEstimateSoft.Task
{
    /// <summary>
    /// 重量调整对话框
    /// </summary>
    public partial class WeightAdjustmentForm : Form
    {
        #region 属性

        private WeightAdjustmentResultData weightAdjustment = null;
        private MainForm mainForm = null;
        /// <summary>
        /// 重新计算
        /// </summary>
        private bool IsReCal = false;

        private const int digit = 6;
        private const int digitPic = 3;

        /// <summary>
        /// 基础重量数据
        /// </summary>
        public WeightSortData basicWeightData = null;
        /// <summary>
        /// 校核因子列表
        /// </summary>
        public List<ParaData> lstJHRatio = null;
        /// <summary>
        /// 技术因子列表
        /// </summary>
        public List<ParaData> lstTeachRatio = null;

        private List<ParaData> lstRatio = null;

        private WeightSortData modifyWeightSortData = null;

        private static int rightCount = 0;

        private string preSelText = string.Empty;

        public WeightAdjustmentForm()
        {
            InitializeComponent();
        }

        public WeightAdjustmentForm(MainForm _mainForm)
        {
            InitializeComponent();
            mainForm = _mainForm;

            //初始化基础重量数据表
            dgvBaseWeight.Rows.Add();
            dgvBaseWeight.Rows[0].Cells[0].Value = "重量数值";
            dgvBaseWeight.Rows[0].Cells[0].ReadOnly = true;

            BindSelData();
        }
        public WeightAdjustmentForm(MainForm _mainForm, WeightAdjustmentResultData _weightAdjustment)
        {
            InitializeComponent();
            mainForm = _mainForm;
            weightAdjustment = _weightAdjustment;

            BindSelData();

            //初始化基础重量数据表
            dgvBaseWeight.Rows.Add();
            dgvBaseWeight.Rows[0].Cells[0].Value = "重量数值";
            dgvBaseWeight.Rows[0].Cells[0].ReadOnly = true;
        }

        #endregion

        #region 自定义方法

        private void BindSelData()
        {
            cmbCurrentSel.Items.Add("校核因子");
            cmbCurrentSel.Items.Add("技术因子");
            cmbCurrentSel.Items.Add("校核因子和技术因子");

            cmbCurrentSel.SelectedIndex = 0;
        }

        public void BindWeightData(WeightSortData sortData)
        {
            basicWeightData = sortData.Clone();
            treeViewWeightData.Nodes.Clear();
            MainForm.BindWeightDesignSort(sortData, treeViewWeightData);
        }

        /// <summary>
        /// 设置基础重量数据
        /// </summary>
        /// <param name="lstWeightData"></param>
        public void SetBasicWeightData(List<WeightData> lstWeightData)
        {
            dgvBaseWeight.Rows.Clear();
            dgvBaseWeight.Columns.Clear();

            if (lstWeightData != null && lstWeightData.Count > 0)
            {
                //第一列
                DataGridViewTextBoxColumn firstColumn = new DataGridViewTextBoxColumn();
                firstColumn.HeaderText = "重量名称";
                firstColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvBaseWeight.Columns.Add(firstColumn);

                dgvBaseWeight.Rows.Add();

                for (int i = 0; i < lstWeightData.Count; i++)
                {
                    DataGridViewTextBoxColumn txtColumn = new DataGridViewTextBoxColumn();
                    txtColumn.Name = lstWeightData[i].nID.ToString();
                    txtColumn.HeaderText = lstWeightData[i].weightName;
                    txtColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                    //备注
                    txtColumn.ToolTipText = WeightSortData.GetNodePath(basicWeightData, lstWeightData[i]);

                    dgvBaseWeight.Columns.Add(txtColumn);

                    dgvBaseWeight.Columns[i + 1].ValueType = System.Type.GetType("System.Decimal");
                    dgvBaseWeight.Rows[0].Cells[i + 1].Value = lstWeightData[i].weightValue;
                }
            }

            if (dgvBaseWeight.ColumnCount > 0)
            {
                dgvBaseWeight.Rows[0].Cells[0].Value = "重量数值";
                dgvBaseWeight.Rows[0].Cells[0].ReadOnly = true;
            }
            toolMemuRatioData.Enabled = true;
        }

        /// <summary>
        /// 设置修正因子列表
        /// </summary>
        /// <param name="lstWeightData"></param>
        public void SetRatioGridView(List<WeightData> lstWeightData)
        {
            //设置GridView
            dgvCorrectFactor.Rows.Clear();
            dgvCorrectFactor.Columns.Clear();

            if (lstWeightData != null && lstWeightData.Count > 0)
            {
                //第一列
                DataGridViewTextBoxColumn firstColumn = new DataGridViewTextBoxColumn();
                firstColumn.HeaderText = "重量名称";
                firstColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvCorrectFactor.Columns.Add(firstColumn);

                if (cmbCurrentSel.Text == "校核因子" || cmbCurrentSel.Text == "技术因子")
                {
                    dgvCorrectFactor.Rows.Add(2);
                }

                if (cmbCurrentSel.Text == "校核因子和技术因子")
                {
                    dgvCorrectFactor.Rows.Add(3);
                }

                for (int i = 0; i < lstWeightData.Count; i++)
                {
                    DataGridViewTextBoxColumn txtColumn = new DataGridViewTextBoxColumn();

                    txtColumn.Name = lstWeightData[i].nID.ToString();
                    txtColumn.HeaderText = lstWeightData[i].weightName;
                    txtColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                    //节点路径
                    txtColumn.ToolTipText = WeightSortData.GetNodePath(basicWeightData, lstWeightData[i]);

                    dgvCorrectFactor.Columns.Add(txtColumn);

                    dgvCorrectFactor.Columns[i + 1].ValueType = System.Type.GetType("System.Decimal");
                }

                if (dgvCorrectFactor.Rows.Count == 2)
                {
                    if (cmbCurrentSel.Text == "校核因子")
                    {
                        dgvCorrectFactor.Rows[0].Cells[0].Value = "校核因子";
                        dgvCorrectFactor.Rows[0].Cells[0].ReadOnly = true;
                    }
                    if (cmbCurrentSel.Text == "技术因子")
                    {
                        dgvCorrectFactor.Rows[0].Cells[0].Value = "技术因子";
                        dgvCorrectFactor.Rows[0].Cells[0].ReadOnly = true;
                    }
                    dgvCorrectFactor.Rows[1].Cells[0].Value = "修正因子";
                    dgvCorrectFactor.Rows[1].Cells[0].ReadOnly = true;

                    dgvCorrectFactor.Rows[1].ReadOnly = true;
                }

                if (dgvCorrectFactor.Rows.Count == 3)
                {
                    dgvCorrectFactor.Rows[0].Cells[0].Value = "校核因子";
                    dgvCorrectFactor.Rows[0].Cells[0].ReadOnly = true;
                    dgvCorrectFactor.Rows[1].Cells[0].Value = "技术因子";
                    dgvCorrectFactor.Rows[1].Cells[0].ReadOnly = true;
                    dgvCorrectFactor.Rows[2].Cells[0].Value = "修正因子";
                    dgvCorrectFactor.Rows[2].Cells[0].ReadOnly = true;

                    dgvCorrectFactor.Rows[2].ReadOnly = true;
                }
            }

        }

        /// <summary>
        /// 获取当前重量分类
        /// </summary>
        /// <returns></returns>
        private WeightSortData GetCurrentSortData()
        {
            WeightSortData sortData = basicWeightData.Clone();

            if (sortData != null && sortData.lstWeightData != null && sortData.lstWeightData.Count > 0)
            {
                foreach (WeightData data in sortData.lstWeightData)
                {
                    for (int i = 1; i < dgvBaseWeight.ColumnCount; i++)
                    {
                        if (dgvBaseWeight.Columns[i].Name == data.nID.ToString())
                        {
                            data.weightValue = Convert.ToDouble(dgvBaseWeight.Rows[0].Cells[i].Value);
                            break;
                        }
                    }
                }

                //求和
                foreach (WeightData data in sortData.lstWeightData)
                {
                    WeightSortData.GetSortDataTotal(data, sortData);
                }
            }
            return sortData;
        }

        /// <summary>
        /// 判断导入重量数据文件格式是否正确
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="path"></param>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private bool IsImortWeightDataFileFormat(string strPath)
        {
            bool IsRight = false;

            //重量数据

            if (strPath.EndsWith(".xml"))
            {
                XmlNode node = null;
                XmlDocument doc = new XmlDocument();
                doc.Load(strPath);

                node = doc.SelectSingleNode("重量分类/重量分类名称");
                if (node != null)
                {
                    IsRight = true;
                }
            }
            if (strPath.EndsWith(".xls"))
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

                if (table.Columns[0].Caption == "重量分类名称")
                {
                    IsRight = true;
                }
            }

            return IsRight;
        }

        /// <summary>
        /// 导出数据至数据文件
        /// </summary>
        /// <param name="sortData"></param>
        private void ExportDataToDataFile(WeightSortData sortData)
        {
            if (sortData == null)
            {
                XLog.Write("没有数据不能导出");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml文件 (*.xml)|*.xml|Excel文件 (*.xls)|*.xls";
            dlg.OverwritePrompt = true;
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = sortData.sortName;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dlg.FileName;

                if (strFilePath.EndsWith(".xml"))
                {
                    List<string> lstContent = MainForm.GetDesignResultFlieContent(sortData);
                    CommonFunction.mWriteListStringToFile(strFilePath, lstContent);
                }

                if (strFilePath.EndsWith(".xls"))
                {
                    DataTable table = MainForm.GetDesignResultTable(sortData);
                    int result = XCommon.FileStatus.FileIsOpen(strFilePath);

                    if (result == 1)
                    {
                        MessageBox.Show("请关闭文件" + strFilePath + ",再导出");
                    }
                    else
                    {
                        CommonExcel commonExcel = new CommonExcel();
                        commonExcel.lstColumn = MainForm.GetDesignResultExcleCloumn();
                        commonExcel.CreateExcel("example", strFilePath, "sheet1", table);
                    }
                }

                XLog.Write("导出文件\"" + strFilePath + "\"成功");
            }
        }

        /// <summary>
        /// 导出修正因子数据至数据文件
        /// </summary>
        /// <param name="sortData"></param>
        private void ExportRatioDataToDataFile(List<ParaData> lstPara)
        {
            if (lstPara == null || lstPara.Count == 0)
            {
                XLog.Write("没有数据不能导出");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml文件 (*.xml)|*.xml|Excel文件 (*.xls)|*.xls";
            dlg.OverwritePrompt = true;
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = lstPara[0].paraName.Substring(0, 4);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dlg.FileName;

                if (strFilePath.EndsWith(".xml"))
                {
                    List<string> lstContent = GetFileContent(lstPara);
                    CommonFunction.mWriteListStringToFile(strFilePath, lstContent);
                }

                if (strFilePath.EndsWith(".xls"))
                {
                    DataTable table = GetExcelTable(lstPara);
                    int result = XCommon.FileStatus.FileIsOpen(strFilePath);

                    if (result == 1)
                    {
                        MessageBox.Show("请关闭文件" + strFilePath + ",再导出");
                    }
                    else
                    {
                        CommonExcel commonExcel = new CommonExcel();
                        commonExcel.lstColumn = GetExcelCloumn();
                        commonExcel.CreateExcel("example", strFilePath, "sheet1", table);
                    }
                }

                XLog.Write("导出文件\"" + strFilePath + "\"成功");
            }
        }

        /// <summary>
        /// 设置校核因子数据
        /// </summary>
        /// <param name="lstJHPara"></param>
        public void SetJHRatioData(List<ParaData> lstJHPara)
        {
            if (lstJHPara != null && lstJHPara.Count > 0)
            {
                lstJHRatio = lstJHPara;

                //校核因子
                for (int i = 1; i < dgvCorrectFactor.ColumnCount; i++)
                {
                    //备注相同
                    foreach (ParaData data in lstJHPara)
                    {
                        if (dgvCorrectFactor.Columns[i].ToolTipText == data.strRemark)
                        {
                            dgvCorrectFactor.Rows[0].Cells[i].Value = Math.Round(data.paraValue, digit);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置技术因子数据
        /// </summary>
        /// <param name="lstTechPara"></param>
        public void SetTechRatioData(List<ParaData> lstTechPara)
        {
            //技术因子
            if (lstTechPara != null && lstTechPara.Count > 0)
            {
                lstTeachRatio = lstTechPara;

                for (int i = 1; i < dgvCorrectFactor.ColumnCount; i++)
                {
                    foreach (ParaData data in lstTechPara)
                    {
                        //备注相同
                        if (dgvCorrectFactor.Columns[i].ToolTipText == data.strRemark)
                        {
                            if (cmbCurrentSel.Text == "技术因子")
                            {
                                dgvCorrectFactor.Rows[0].Cells[i].Value = Math.Round(data.paraValue, digit);
                            }
                            if (cmbCurrentSel.Text == "校核因子和技术因子")
                            {
                                dgvCorrectFactor.Rows[1].Cells[i].Value = Math.Round(data.paraValue, digit);

                            }
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置修正后的因子数据
        /// </summary>
        /// <param name="lstModifyara"></param>
        public void SetRatioData(List<ParaData> lstModify)
        {
            if (lstModify != null && lstModify.Count > 0)
            {
                lstRatio = lstModify;
                //修正因子
                for (int i = 1; i < dgvCorrectFactor.ColumnCount; i++)
                {
                    foreach (ParaData data in lstModify)
                    {
                        if (dgvCorrectFactor.Columns[i].ToolTipText == data.strRemark)
                        {
                            if (cmbCurrentSel.Text == "校核因子" || cmbCurrentSel.Text == "技术因子")
                            {
                                dgvCorrectFactor.Rows[1].Cells[i].Value = Math.Round(data.paraValue, digit);
                            }
                            if (cmbCurrentSel.Text == "校核因子和技术因子")
                            {
                                dgvCorrectFactor.Rows[2].Cells[i].Value = Math.Round(data.paraValue, digit);
                            }
                            break;
                        }
                    }
                }
            }
        }

        ///// <summary>
        ///// 画修正因子柱状图
        ///// </summary>
        ///// <param name="lstTempPara"></param>
        //public void DisplayInPicture(List<ParaData> lstJHPara, List<ParaData> lstTeachPara, List<ParaData> lstModifyPara)
        //{
        //    GraphPane myPane = zedGraphControlPic.GraphPane;

        //    //清除原来的图形
        //    myPane.CurveList.Clear();
        //    myPane.GraphObjList.Clear();

        //    //设置图例   LegendPos.Right;
        //    myPane.Legend.Position = LegendPos.Right;
        //    myPane.Legend.IsHStack = true;
        //    myPane.Legend.FontSpec.Size = 12.0F;
        //    myPane.Legend.FontSpec.FontColor = Color.Navy;

        //    //柱状图标题 
        //    myPane.Title.FontSpec.IsBold = true;
        //    myPane.Title.FontSpec.FontColor = Color.Navy;
        //    myPane.Title.Text = string.Empty;


        //    string[] xLables = null;
        //    double y = 0;
        //    double[] dJHValue = null;
        //    double[] dTechValue = null;
        //    double[] dModifyValue = null;

        //    BarItem myCurveCur = null;

        //    //校核因子
        //    if (lstJHPara != null && lstJHPara.Count > 0)
        //    {
        //        xLables = new string[lstJHPara.Count];
        //        dJHValue = new double[lstJHPara.Count];

        //        string strType = lstJHPara[0].paraName.Substring(0, 4);

        //        for (int i = 0; i < lstJHPara.Count; i++)
        //        {
        //            y = Math.Round(lstJHPara[i].paraValue, digitPic);
        //            dJHValue[i] = y;
        //            xLables[i] = lstJHPara[i].paraName.Substring(5, lstJHPara[i].paraName.Length - 5);
        //        }
        //        myCurveCur = myPane.AddBar(strType, null, dJHValue, Color.Blue);
        //    }

        //    //技术因子
        //    if (lstTeachPara != null && lstTeachPara.Count > 0)
        //    {
        //        xLables = new string[lstTeachPara.Count];
        //        dTechValue = new double[lstTeachPara.Count];

        //        string strType = lstTeachPara[0].paraName.Substring(0, 4);

        //        for (int i = 0; i < lstTeachPara.Count; i++)
        //        {
        //            y = Math.Round(lstTeachPara[i].paraValue, digitPic);
        //            dTechValue[i] = y;
        //            xLables[i] = lstTeachPara[i].paraName.Substring(5, lstTeachPara[i].paraName.Length - 5);
        //        }
        //        myCurveCur = myPane.AddBar(strType, null, dTechValue, Color.Green);
        //    }

        //    //修正因子
        //    if (lstModifyPara != null && lstModifyPara.Count > 0)
        //    {
        //        dModifyValue = new double[lstModifyPara.Count];

        //        for (int i = 0; i < lstModifyPara.Count; i++)
        //        {
        //            y = Math.Round(lstModifyPara[i].paraValue, digitPic);
        //            dModifyValue[i] = y;
        //        }
        //        myCurveCur = myPane.AddBar("修正因子", null, dModifyValue, Color.Red);
        //    }

        //    // 设置x轴为文本显示             
        //    myPane.XAxis.Type = AxisType.Text;
        //    myPane.XAxis.Scale.TextLabels = xLables;
        //    //文字竖着放
        //    myPane.XAxis.Scale.FontSpec.Angle = -90;
        //    //设置x轴标签字体             
        //    myPane.XAxis.Scale.FontSpec.Family = "宋体";
        //    myPane.XAxis.Scale.FontSpec.Size = 12.0F;
        //    myPane.XAxis.MajorTic.IsBetweenLabels = true;


        //    // 为每个“柱子”上方添加值标签  

        //    //校核因子
        //    if (dJHValue != null && dJHValue.Length > 0)
        //    {
        //        for (int i = 0; i < dJHValue.Length; i++)
        //        {
        //            double Y = dJHValue[i];
        //            TextObj text = new TextObj(Y.ToString(), i + 1, Y + 0.5);
        //            text.FontSpec.Border.IsVisible = false;
        //            text.FontSpec.Fill.IsVisible = false;
        //            text.FontSpec.Size = 8.0F;

        //            myPane.GraphObjList.Add(text);
        //        }
        //    }
        //    //技术因子
        //    if (dTechValue != null && dTechValue.Length > 0)
        //    {
        //        for (int i = 0; i < dTechValue.Length; i++)
        //        {
        //            double Y = dTechValue[i];
        //            TextObj text = new TextObj(Y.ToString(), i + 1, Y + 0.5);
        //            text.FontSpec.Border.IsVisible = false;
        //            text.FontSpec.Fill.IsVisible = false;
        //            text.FontSpec.Size = 8.0F;

        //            myPane.GraphObjList.Add(text);
        //        }
        //    }

        //    //修正因子
        //    if (dModifyValue != null && dModifyValue.Length > 0)
        //    {
        //        for (int i = 0; i < dModifyValue.Length; i++)
        //        {
        //            double Y = dModifyValue[i];
        //            TextObj text = new TextObj(Y.ToString(), i + 1, Y + 0.5);
        //            text.FontSpec.Border.IsVisible = false;
        //            text.FontSpec.Fill.IsVisible = false;
        //            text.FontSpec.Size = 8.0F;

        //            myPane.GraphObjList.Add(text);
        //        }
        //    }

        //    myPane.Chart.Fill = new Fill(Color.White, Color.FromArgb(255, 255, 166));
        //    myPane.Fill = new Fill(Color.White, Color.FromArgb(200, 200, 255));

        //    myPane.YAxis.Scale.MajorStep = 5;
        //    myPane.BarSettings.Type = BarType.Cluster;

        //    zedGraphControlPic.AxisChange();
        //    zedGraphControlPic.Refresh();

        //    zedGraphControlPic.SaveFileDialog.FilterIndex = 4;
        //}

        private void DisplayAdjustmentInPic(List<ParaData> lstJHPara, List<ParaData> lstTeachPara, List<ParaData> lstModifyPara)
        {
            chartAdjustment.Titles.Clear();
            chartAdjustment.Series.Clear();
            chartAdjustment.ChartAreas.Clear();
            chartAdjustment.Legends.Clear();

            rightCount = 0;
            toolStripMenuItemDisplayData.Text = "显示数据";

            //标题
            Title title1 = new Title();
            title1.Text = "重量调整";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            chartAdjustment.Titles.Add(title1);

            AddChartSeries(chartAdjustment, lstJHPara, "校核因子", Color.Blue);
            AddChartSeries(chartAdjustment, lstTeachPara, "技术因子", Color.Green);
            AddChartSeries(chartAdjustment, lstModifyPara, "修正因子", Color.Red);

            if (chartAdjustment.ChartAreas.Count > 0)
            {
                chartAdjustment.ContextMenuStrip = contextMenuStripRatioCompare;

                chartAdjustment.ChartAreas[0].AxisX.IsLabelAutoFit = true;
                chartAdjustment.ChartAreas[0].AxisX.LabelStyle.Font = new Font("微软雅黑", 9);

                //X坐标显示不全
                chartAdjustment.ChartAreas[0].AxisX.Interval = 1;
                chartAdjustment.ChartAreas[0].AxisX.IntervalOffset = 1;

                chartAdjustment.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;

                //放大缩小
                chartAdjustment.ChartAreas[0].CursorX.Interval = 0.001D;
                chartAdjustment.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                chartAdjustment.ChartAreas[0].CursorY.Interval = 0.001D;
                chartAdjustment.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            }
        }

        private void AddChartSeries(System.Windows.Forms.DataVisualization.Charting.Chart chart1, List<ParaData> lstPara, string strLable, Color color)
        {
            if (lstPara != null && lstPara.Count > 0)
            {
                //ChartArea
                if (chart1.ChartAreas.Count == 0)
                {
                    ChartArea chartArea1 = new ChartArea();
                    chartArea1.Name = "ChartArea1";
                    chart1.ChartAreas.Add(chartArea1);
                }

                //Series
                Series series = new Series();
                //legend
                System.Windows.Forms.DataVisualization.Charting.Legend legend = new System.Windows.Forms.DataVisualization.Charting.Legend();

                legend.Position.Auto = true;
                if (chart1.Series.Count == 0)
                {
                    series.Name = "Series1";
                    legend.Name = "legend1";
                }
                if (chart1.Series.Count == 1)
                {
                    series.Name = "Series2";
                    legend.Name = "legend2";
                    series.LegendText = strLable;
                }
                if (chart1.Series.Count == 2)
                {
                    series.Name = "Series3";
                    legend.Name = "legend3";
                }
                series.LegendText = strLable;
                legend.Docking = Docking.Top;
                chart1.Legends.Add(legend);

                int count = lstPara.Count;
                double yValue = 0;
                string strParaName = string.Empty;
                foreach (ParaData data in lstPara)
                {

                    yValue = Math.Round(data.paraValue, digitPic);
                    strParaName = data.paraName.Substring(5, data.paraName.Length - 5);
                    series.Points.AddXY(strParaName, yValue);
                }

                int index = -1;
                string strType = string.Empty;
                for (int i = 0; i < lstPara.Count; i++)
                {
                    yValue = Math.Round(lstPara[i].paraValue, digitPic);
                    index = lstPara[i].paraName.IndexOf('-');
                    strType = strLable + lstPara[i].paraName.Substring(index, lstPara[i].paraName.Length - index);
                    series.Points[i].ToolTip = strType + ":" + yValue.ToString();
                }


                series.ChartArea = "ChartArea1";
                chart1.Series.Add(series);

                series.ChartType = SeriesChartType.Column;                //柱形图
                series.Color = color;                              //线条颜色   
                //series.BorderWidth = 2;                                  //线条宽度   
                //series.ShadowOffset = 1;                                 //阴影宽度   
                //series.IsVisibleInLegend = true;                         //是否显示数据说明   
                //series.MarkerStyle = MarkerStyle.Circle;               //线条上的数据点标志类型 
                //series.MarkerSize = 8;

                //series["DrawingStyle"] = "Cylinder";

                //柱形宽度
                series["PixelPointWidth"] = "30";
                //像素点深度
                series["PixelPointDepth"] = "80";
                //像素点间隙深度
                series["PixelPointGapDepth"] = "10";
                //series["PointWidth"] = "0.8";
            }
        }

        /// <summary>
        /// 求解调整后的重量分类
        /// </summary>
        /// <param name="_weightSortData"></param>
        /// <param name="_lstPara"></param>
        /// <returns></returns>
        private WeightSortData getWeightModified(WeightSortData _weightSortData, List<ParaData> _lstPara)
        {
            WeightSortData tempWeightSortData = null;
            WeightData tempWeightData = null;
            List<WeightData> lstWeightData = null;

            if (_weightSortData == null || _weightSortData.lstWeightData.Count == 0
                || _lstPara == null || _lstPara.Count == 0)
            {
                return tempWeightSortData;
            }
            else
            {
                // 判断当前的重量分类和修正因子是否合适
                if (WeightSortData.blIsFit(_weightSortData, _lstPara))
                {
                    // 复制重量分类
                    tempWeightSortData = _weightSortData.Clone();
                    // 置零非基层的重量数据
                    lstWeightData = WeightSortData.GetListWeightData(tempWeightSortData);
                    foreach (WeightData _weightData in tempWeightSortData.lstWeightData)
                    {
                        int intCounter = 0;
                        foreach (WeightData _weightData2 in lstWeightData)
                        {
                            if (_weightData.nID != _weightData2.nID)
                            {
                                intCounter = intCounter + 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        //if (intCounter == lstWeightData.Count - 1)
                        //{
                        //    _weightData.weightValue = 0.0;
                        //}
                    }
                    // 找到相同的重量名称

                    for (int i = 0; i < lstWeightData.Count; i = i + 1)
                    {
                        StringBuilder tempPath = new StringBuilder();

                        for (int j = 0; j < _lstPara.Count; j = j + 1)
                        {
                            // 错误的修正因子名称
                            if (_lstPara[j].paraName.Length < 4)
                            {
                                break;
                            }
                            if (lstWeightData[i].weightName == _lstPara[j].paraName.Substring(5, _lstPara[j].paraName.Length - 5))
                            {
                                tempPath.Append("\\" + lstWeightData[i].weightName);

                                // 获取根
                                bool triger = true;
                                int intInner = lstWeightData[i].nID;
                                while (triger)
                                {
                                    tempWeightData = WeightSortData.getParentNode(tempWeightSortData, intInner);

                                    if (tempWeightData != null && tempWeightData.nParentID != -1)
                                    {
                                        intInner = tempWeightData.nID;
                                        tempPath.Insert(0, "\\" + tempWeightData.weightName);
                                    }
                                    else if (tempWeightData != null && tempWeightData.nParentID == -1)
                                    {
                                        tempPath.Insert(0, tempWeightData.weightName);
                                        triger = false;
                                    }
                                    else
                                    {
                                        triger = false;
                                    }
                                }
                                // 找到对应修正因子
                                if (tempPath.ToString() == _lstPara[j].strRemark)
                                {
                                    foreach (WeightData _weightData in tempWeightSortData.lstWeightData)
                                    {
                                        if (_weightData.nID == lstWeightData[i].nID)
                                        {
                                            _weightData.weightValue = _weightData.weightValue * _lstPara[j].paraValue;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //求和
                foreach (WeightData data in tempWeightSortData.lstWeightData)
                {
                    WeightSortData.GetSortDataTotal(data, tempWeightSortData);
                }
                return tempWeightSortData;
            }
        }

        private List<ParaData> GetXmlListRatioData(string strFilePath)
        {
            List<ParaData> lstParaData = null;
            try
            {
                XmlDocument xdoc = new XmlDocument();

                xdoc.Load(strFilePath);

                string strPath = "修正因子数据/参数列表";
                XmlNode node = xdoc.SelectSingleNode(strPath);
                XmlNodeList nodeList = node.ChildNodes;

                if (nodeList != null && nodeList.Count > 0)
                {
                    lstParaData = new List<ParaData>();
                    foreach (XmlNode childNode in nodeList)
                    {
                        ParaData data = new ParaData();
                        data.paraName = childNode.ChildNodes[0].InnerText;
                        data.paraUnit = childNode.ChildNodes[1].InnerText;
                        data.paraType = Convert.ToInt32(childNode.ChildNodes[2].InnerText);
                        data.paraValue = Convert.ToDouble(childNode.ChildNodes[3].InnerText);
                        data.strRemark = childNode.ChildNodes[4].InnerText;

                        lstParaData.Add(data);
                    }
                }
            }
            catch
            {
                XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                return null;
            }

            return lstParaData;
        }

        private List<ParaData> GetExcelRatioData(string strFilePath)
        {
            List<ParaData> lstParaData = null;
            try
            {
                if (File.Exists(strFilePath))
                {
                    ExcelLib OpExcel = new ExcelLib();
                    //指定操作的文件
                    OpExcel.OpenFileName = strFilePath;
                    //打开文件
                    if (OpExcel.OpenExcelFile() == false)
                    {
                        return lstParaData;
                    }
                    //取得所有的工作表名称
                    string[] strSheetsName = OpExcel.getWorkSheetsName();

                    //默认操作第一张表
                    OpExcel.SetActiveWorkSheet(1);
                    System.Data.DataTable table;
                    table = OpExcel.getAllCellsValue();
                    OpExcel.CloseExcelApplication();

                    int count = table.Rows.Count;
                    if (count > 0)
                    {
                        lstParaData = new List<ParaData>();
                        for (int i = 0; i < count; i++)
                        {
                            ParaData data = new ParaData();

                            data.paraName = table.Rows[i][0].ToString();
                            data.paraUnit = table.Rows[i][1].ToString();
                            data.paraType = Convert.ToInt32(table.Rows[i][2].ToString());
                            data.paraValue = Convert.ToDouble(table.Rows[i][3].ToString());
                            data.strRemark = table.Rows[i][4].ToString();

                            lstParaData.Add(data);
                        }
                    }
                }
            }
            catch
            {
                XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                return null;
            }

            return lstParaData;
        }

        /// <summary>
        /// 判断修正因子数据文件格式是否正确
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private bool IsRatioDataFileFormat(string strPath)
        {
            bool IsRight = false;

            if (strPath.EndsWith(".xml"))
            {
                XmlNode node = null;
                XmlDocument doc = new XmlDocument();
                doc.Load(strPath);

                node = doc.SelectSingleNode("修正因子数据/参数列表");
                if (node != null)
                {
                    IsRight = true;
                }
            }
            if (strPath.EndsWith(".xls"))
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

                if (table.Columns[0].Caption == "参数名称")
                {
                    IsRight = true;
                }
            }

            return IsRight;
        }

        /// <summary>
        /// 获取修正后的重量数据
        /// </summary>
        /// <param name="jHSortData"></param>
        /// <param name="techSortData"></param>
        private WeightSortData GetModifyWeightData(WeightSortData jHSortData, WeightSortData techSortData)
        {
            WeightSortData sortData = null;
            if (jHSortData != null && jHSortData.lstWeightData.Count > 0
                && techSortData != null && techSortData.lstWeightData.Count > 0)
            {
                sortData = basicWeightData.Clone();

                List<WeightData> lstJHWeightData = WeightSortData.GetListWeightData(jHSortData);
                List<WeightData> lstTeachWeightData = WeightSortData.GetListWeightData(techSortData);

                List<WeightData> lstModifyWeightData = new List<WeightData>();

                foreach (WeightData data in lstJHWeightData)
                {
                    foreach (WeightData weight in lstTeachWeightData)
                    {
                        if (data.nID == weight.nID)
                        {
                            WeightData newWeight = new WeightData();

                            newWeight.nID = data.nID;
                            newWeight.nParentID = data.nParentID;
                            newWeight.weightName = data.weightName;
                            newWeight.weightValue = data.weightValue * weight.weightValue;
                            newWeight.strRemark = data.strRemark;
                            lstModifyWeightData.Add(newWeight);

                            break;
                        }
                    }
                }

                if (sortData != null && lstModifyWeightData.Count > 0)
                {
                    foreach (WeightData data in sortData.lstWeightData)
                    {
                        foreach (WeightData weight in lstModifyWeightData)
                        {
                            if (data.nID == weight.nID)
                            {
                                data.weightValue = weight.weightValue;
                                break;
                            }
                        }
                    }

                    //求和
                    foreach (WeightData data in sortData.lstWeightData)
                    {
                        WeightSortData.GetSortDataTotal(data, sortData);
                    }
                }

            }

            return sortData;

        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public void InitializeData()
        {
            lstJHRatio = null;
            lstTeachRatio = null;
            lstRatio = null;

            //basicWeightData = null;
            modifyWeightSortData = null;


            //修正因子列表清空
            dgvCorrectFactor.Rows.Clear();
            dgvCorrectFactor.Columns.Clear();

            //清空图形
            chartAdjustment.Titles.Clear();
            chartAdjustment.Series.Clear();
            chartAdjustment.ChartAreas.Clear();
            chartAdjustment.Legends.Clear();
            chartAdjustment.ContextMenuStrip = null;
        }

        /// <summary>
        /// 判断导入的因子类型
        /// </summary>
        private string ImportRatioType(List<ParaData> lstPara, string strImportType)
        {
            string str = string.Empty;
            if (lstPara != null && lstPara.Count > 0)
            {
                string strType = lstPara[0].paraName.Substring(0, 4);

                //if (cmbCurrentSel.Text == "校核因子")
                //{
                //    if (strType == "技术因子")
                //    {
                //        str = "只能导入校核因子";
                //    }
                //}

                //if (cmbCurrentSel.Text == "技术因子")
                //{
                //    if (strType == "校核因子")
                //    {
                //        str = "只能导入技术因子";
                //    }
                //}



                if (cmbCurrentSel.Text == "校核因子" || cmbCurrentSel.Text == "技术因子")
                {
                    if (strImportType != cmbCurrentSel.Text)
                    {
                        str = "请导入" + cmbCurrentSel.Text;
                    }
                    else
                    {
                        if (strType != strImportType)
                        {
                            str = "请导入" + strImportType;
                        }
                    }
                }
                else if (cmbCurrentSel.Text == "校核因子和技术因子")
                {
                    if (strImportType != strType)
                    {
                        str = "请导入" + strImportType;
                    }
                }
            }
            return str;
        }

        /// <summary>
        /// 获取Xml文件的WeightSortData
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private WeightSortData GetXmlImporSortData(string strPath)
        {
            WeightSortData sortData = null;

            try
            {
                if (!File.Exists(strPath))
                {
                    return sortData;
                }
                sortData = new WeightSortData();

                string path = string.Empty;
                XmlNode node = null;

                XmlDocument doc = new XmlDocument();
                doc.Load(strPath);


                //重量分类名称
                path = "重量分类/重量分类名称";
                node = doc.SelectSingleNode(path);
                sortData.sortName = (node == null ? string.Empty : node.InnerText);

                //重量分类备注
                path = "重量分类/重量分类备注";
                node = doc.SelectSingleNode(path);
                sortData.strRemark = (node == null ? string.Empty : node.InnerText);

                //重量列表
                path = "重量分类/重量数据列表";
                node = doc.SelectSingleNode(path);

                XmlNodeList nodelist = (node == null ? null : node.ChildNodes);

                if (nodelist != null && nodelist.Count > 0)
                {
                    List<WeightData> lstWeightData = new List<WeightData>();
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        WeightData data = new WeightData();

                        data.nID = Convert.ToInt32(childNode.ChildNodes[0].InnerText);
                        data.weightName = childNode.ChildNodes[1].InnerText;
                        data.weightValue = Convert.ToDouble(childNode.ChildNodes[3].InnerText);
                        data.strRemark = childNode.ChildNodes[4].InnerText;
                        data.nParentID = Convert.ToInt32(childNode.ChildNodes[5].InnerText);

                        lstWeightData.Add(data);
                    }
                    sortData.lstWeightData = lstWeightData;
                }
            }
            catch
            {
                XLog.Write("导入文件\"" + strPath + "\"格式错误");
                MessageBox.Show("导入文件\"" + strPath + "\"格式错误");
                return null;
            }
            return sortData;
        }

        /// <summary>
        ///获取xls文件 WeightSortData
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        private WeightSortData GetXlsImportSortData(string strFilePath)
        {
            WeightSortData sortData = null;
            try
            {
                if (File.Exists(strFilePath))
                {
                    ExcelLib OpExcel = new ExcelLib();
                    //指定操作的文件
                    OpExcel.OpenFileName = strFilePath;
                    //打开文件
                    if (OpExcel.OpenExcelFile() == false)
                    {
                        return sortData;
                    }
                    //取得所有的工作表名称
                    string[] strSheetsName = OpExcel.getWorkSheetsName();

                    //默认操作第一张表
                    OpExcel.SetActiveWorkSheet(1);
                    System.Data.DataTable table;
                    table = OpExcel.getAllCellsValue();
                    OpExcel.CloseExcelApplication();

                    int count = table.Rows.Count;
                    if (count > 0)
                    {
                        sortData = new WeightSortData();
                        sortData.sortName = table.Rows[0][0].ToString();

                        List<WeightData> lstWeightData = new List<WeightData>();
                        for (int i = 0; i < count; i++)
                        {
                            WeightData data = new WeightData();

                            data.nID = Convert.ToInt32(table.Rows[i][1].ToString());
                            data.weightName = table.Rows[i][2].ToString();
                            data.weightValue = Convert.ToDouble(table.Rows[i][4].ToString());
                            data.strRemark = table.Rows[i][5].ToString();
                            data.nParentID = Convert.ToInt32(table.Rows[i][6].ToString());

                            lstWeightData.Add(data);
                        }
                        sortData.lstWeightData = lstWeightData;
                    }
                }
            }
            catch
            {
                XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                return null;
            }
            return sortData;
        }

        /// <summary>
        /// 更新基础重量数据
        /// </summary>
        private void UpDateBasicWeightSortData()
        {
            if (basicWeightData != null && basicWeightData.lstWeightData.Count > 0)
            {
                for (int i = 1; i < dgvBaseWeight.ColumnCount; i++)
                {
                    foreach (WeightData data in basicWeightData.lstWeightData)
                    {
                        if (dgvBaseWeight.Columns[i].Name == data.nID.ToString())
                        {
                            data.weightValue = Convert.ToDouble(dgvBaseWeight.Rows[0].Cells[i].Value.ToString());
                            break;
                        }
                    }
                }

                //求和
                foreach (WeightData data in basicWeightData.lstWeightData)
                {
                    WeightSortData.GetSortDataTotal(data, basicWeightData);
                }
            }
        }

        /// <summary>
        /// 更新校核因子数据
        /// </summary>
        private void UpDateJHRatioData()
        {
            double dValue = 0;
            if (lstJHRatio != null && lstJHRatio.Count > 0)
            {
                for (int i = 1; i < dgvCorrectFactor.ColumnCount; i++)
                {
                    dValue = dgvCorrectFactor.Rows[0].Cells[i].Value == null ? 0 : Convert.ToDouble(dgvCorrectFactor.Rows[0].Cells[i].Value.ToString());

                    foreach (ParaData data in lstJHRatio)
                    {
                        if (dgvCorrectFactor.Columns[i].ToolTipText == data.strRemark)
                        {
                            lstJHRatio[i - 1].paraValue = dValue;
                            break;
                        }
                    }
                }
            }
            else
            {
                if (lstJHRatio == null)
                {
                    lstJHRatio = new List<ParaData>();
                }
                for (int i = 1; i < dgvCorrectFactor.ColumnCount; i++)
                {
                    dValue = dgvCorrectFactor.Rows[0].Cells[i].Value == null ? 0 : Convert.ToDouble(dgvCorrectFactor.Rows[0].Cells[i].Value.ToString());
                    ParaData data = new ParaData();
                    data.paraName = "校核因子-" + dgvCorrectFactor.Columns[i].HeaderText;
                    data.paraUnit = "无量纲";
                    data.paraValue = dValue;
                    data.paraType = 8;

                    //获取路径
                    data.strRemark = dgvCorrectFactor.Columns[i].ToolTipText;

                    lstJHRatio.Add(data);
                }
            }
        }

        /// <summary>
        /// 更新技术因子
        /// </summary>
        private void UpDateTechRatioData()
        {
            double dValue = 0;
            if (lstTeachRatio != null && lstTeachRatio.Count > 0)
            {
                for (int i = 1; i < dgvCorrectFactor.ColumnCount; i++)
                {
                    if (cmbCurrentSel.Text == "技术因子")
                    {
                        dValue = dgvCorrectFactor.Rows[0].Cells[i].Value == null ? 0 : Convert.ToDouble(dgvCorrectFactor.Rows[0].Cells[i].Value.ToString());
                        lstTeachRatio[i - 1].paraValue = dValue;
                    }

                    if (cmbCurrentSel.Text == "校核因子和技术因子")
                    {
                        dValue = dgvCorrectFactor.Rows[1].Cells[i].Value == null ? 0 : Convert.ToDouble(dgvCorrectFactor.Rows[1].Cells[i].Value.ToString());
                        lstTeachRatio[i - 1].paraValue = dValue;
                    }
                }
            }
            else
            {
                if (lstTeachRatio == null)
                {
                    lstTeachRatio = new List<ParaData>();
                }

                for (int i = 1; i < dgvCorrectFactor.ColumnCount; i++)
                {
                    if (cmbCurrentSel.Text == "技术因子")
                    {
                        dValue = dgvCorrectFactor.Rows[0].Cells[i].Value == null ? 0 : Convert.ToDouble(dgvCorrectFactor.Rows[0].Cells[i].Value.ToString());
                    }

                    if (cmbCurrentSel.Text == "校核因子和技术因子")
                    {
                        dValue = dgvCorrectFactor.Rows[1].Cells[i].Value == null ? 0 : Convert.ToDouble(dgvCorrectFactor.Rows[1].Cells[i].Value.ToString());
                    }

                    ParaData data = new ParaData();
                    data.paraName = "技术因子-" + dgvCorrectFactor.Columns[i].HeaderText;
                    data.paraUnit = "无量纲";
                    data.paraValue = dValue;
                    data.paraType = 8;

                    //获取路径
                    data.strRemark = dgvCorrectFactor.Columns[i].ToolTipText;

                    lstTeachRatio.Add(data);
                }
            }
        }

        /// <summary>
        /// 更新修正因子数据
        /// </summary>
        private void UpDateModifyRatioData()
        {
            lstRatio = new List<ParaData>();

            if (cmbCurrentSel.Text == "校核因子和技术因子")
            {
                string strParaName1 = string.Empty;
                string strParaName2 = string.Empty;

                if (lstJHRatio != null && lstTeachRatio != null && lstJHRatio.Count > 0 && lstTeachRatio.Count > 0)
                {
                    foreach (ParaData data1 in lstJHRatio)
                    {
                        strParaName1 = data1.paraName.Substring(5, data1.paraName.Length - 5);
                        foreach (ParaData data2 in lstTeachRatio)
                        {
                            strParaName2 = data2.paraName.Substring(5, data2.paraName.Length - 5);

                            if (strParaName1 == strParaName2 && data1.strRemark == data2.strRemark)
                            {
                                ParaData data = new ParaData();
                                data.paraName = "修正因子-" + strParaName2;
                                data.paraType = 8;
                                data.strRemark = data1.strRemark;
                                data.paraValue = data1.paraValue * data2.paraValue;

                                lstRatio.Add(data);
                                break;
                            }
                        }
                    }
                }
            }
            else if (cmbCurrentSel.Text == "校核因子")
            {
                if (lstJHRatio != null && lstJHRatio.Count > 0)
                {
                    string strParaName = string.Empty;
                    foreach (ParaData data in lstJHRatio)
                    {
                        strParaName = data.paraName.Substring(5, data.paraName.Length - 5);
                        data.paraName = "修正因子-" + strParaName;
                        lstRatio.Add(data);
                    }
                }
            }
            else if (cmbCurrentSel.Text == "技术因子")
            {
                if (lstTeachRatio != null && lstTeachRatio.Count > 0)
                {
                    string strParaName = string.Empty;
                    foreach (ParaData data in lstTeachRatio)
                    {
                        strParaName = data.paraName.Substring(5, data.paraName.Length - 5);
                        data.paraName = "修正因子-" + strParaName;
                        lstRatio.Add(data);
                    }
                }
            }
        }

        /// <summary>
        /// 设置页面数据
        /// </summary>
        private void SetPageData()
        {
            if (weightAdjustment != null)
            {
                txtAdjustmentDataName.Text = weightAdjustment.WeightAdjustName;

                //绑定重量分类数据
                BindWeightData(weightAdjustment.basicWeightData);

                //当前选择
                if (weightAdjustment.checkFactor != null)
                {
                    cmbCurrentSel.Text = "校核因子";
                }
                if (weightAdjustment.technologyFactor != null)
                {
                    cmbCurrentSel.Text = "技术因子";
                }
                if (weightAdjustment.checkFactor != null && weightAdjustment.technologyFactor != null)
                {
                    cmbCurrentSel.Text = "校核因子和技术因子";
                }

                //基础重量数据
                SetBasicWeightData(WeightSortData.GetListWeightData(weightAdjustment.basicWeightData));

                //修正因子
                SetRatioGridView(WeightSortData.GetListWeightData(weightAdjustment.basicWeightData));

                //设置修正因子
                SetJHRatioData(weightAdjustment.checkFactor);
                SetTechRatioData(weightAdjustment.technologyFactor);

                UpDateModifyRatioData();
                //设置修正因子
                SetRatioData(lstRatio);

                //柱状图
                DisplayAdjustmentInPic(weightAdjustment.checkFactor, weightAdjustment.technologyFactor, lstRatio);
            }
        }

        private List<string> GetFileContent(List<ParaData> lstTempPara)
        {
            List<string> lstContent = new List<string>();

            if (lstTempPara != null && lstTempPara.Count > 0)
            {
                lstContent.Insert(0, "<?xml version=" + "\"1.0\"" + " encoding=" + "\"gb2312\"" + " ?> ");
                lstContent.Add("<修正因子数据>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<参数列表>");

                string str = string.Empty;
                foreach (ParaData data in lstTempPara)
                {
                    lstContent.Add(CommonFunction.mStrModifyToString8(2) + "<参数>");

                    str = CommonFunction.mStrModifyToString8(3) + "<参数名称>" + data.paraName + "</参数名称>";
                    lstContent.Add(str);
                    str = CommonFunction.mStrModifyToString8(3) + "<参数单位>" + data.paraUnit + "</参数单位>";
                    lstContent.Add(str);
                    str = CommonFunction.mStrModifyToString8(3) + "<参数类型>" + data.paraType.ToString() + "</参数类型>";
                    lstContent.Add(str);
                    str = CommonFunction.mStrModifyToString8(3) + "<参数数值>" + data.paraValue.ToString() + "</参数数值>";
                    lstContent.Add(str);
                    str = CommonFunction.mStrModifyToString8(3) + "<参数备注>" + data.strRemark + "</参数备注>";
                    lstContent.Add(str);


                    lstContent.Add(CommonFunction.mStrModifyToString8(2) + "</参数>");
                }

                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "</参数列表>");
                lstContent.Add("</修正因子数据>");
            }

            return lstContent;
        }

        private DataTable GetExcelTable(List<ParaData> lstTempPara)
        {
            DataTable table = GetExcelTableStructure();

            if (lstTempPara != null && lstTempPara.Count > 0)
            {
                foreach (ParaData data in lstTempPara)
                {
                    DataRow dr = table.NewRow();

                    dr["para_Name"] = data.paraName;
                    dr["para_Unit"] = data.paraUnit;
                    dr["para_Type"] = data.paraType;
                    dr["para_Value"] = data.paraValue;
                    dr["para_Remark"] = data.strRemark;

                    table.Rows.Add(dr);
                }
            }

            return table;
        }

        private DataTable GetExcelTableStructure()
        {
            DataTable table = new DataTable();

            table.Columns.Add("para_Name");
            table.Columns.Add("para_Unit");
            table.Columns.Add("para_Type");
            table.Columns.Add("para_Value");
            table.Columns.Add("para_Remark");

            return table;
        }

        private List<string> GetExcelCloumn()
        {
            List<string> lstContent = new List<string>();

            lstContent.Add("参数名称");
            lstContent.Add("参数单位");
            lstContent.Add("参数类型");
            lstContent.Add("参数数值");
            lstContent.Add("参数备注");

            return lstContent;
        }

        /// <summary>
        /// 从数据文件导入修正因子
        /// </summary>
        private void FormDataFileImportRatio(string strImportType)
        {
            if (basicWeightData == null || basicWeightData.lstWeightData.Count == 0)
            {
                MessageBox.Show("请先导入基础重量数据");
                return;
            }

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "xml文件 (*.xml)|*.xml|Excle文件 (*.xls)|*.xls|All File (*.*)|*.*";
            fileDialog.RestoreDirectory = true;
            fileDialog.FilterIndex = 1;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = fileDialog.FileName;

                //if (IsRatioDataFileFormat(strFilePath) == false)
                //{
                //    XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                //    return;
                //}

                //获取重量设计数据
                List<ParaData> lstPara = null;
                if (strFilePath.EndsWith(".xls"))
                {
                    lstPara = GetExcelRatioData(strFilePath);
                }
                else if (strFilePath.EndsWith(".xml"))
                {
                    lstPara = GetXmlListRatioData(strFilePath);
                }
                else
                {
                    XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                    MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                    return;
                }

                if (lstPara == null)
                {
                    return;
                }
                if (lstPara != null && lstPara.Count == 0)
                {
                    MessageBox.Show("导入的文件没有数据");
                    return;
                }

                string str = ImportRatioType(lstPara, strImportType);
                if (str != string.Empty)
                {
                    MessageBox.Show(str);
                    return;
                }

                //判断导入修正因子是否与重量基础数据一致
                if (WeightSortData.blIsFit(basicWeightData, lstPara) == false)
                {
                    MessageBox.Show("导入的修正因子与基础数据不一致");
                    return;
                }

                if (lstPara != null && lstPara.Count > 0)
                {
                    string strTrype = lstPara[0].paraName.Substring(0, 4);
                    if (strTrype == "校核因子")
                    {
                        SetJHRatioData(lstPara);
                    }
                    if (strTrype == "技术因子")
                    {
                        SetTechRatioData(lstPara);
                    }
                    XLog.Write("导入文件\"" + strFilePath + "\"成功");
                }

                if (cmbCurrentSel.Text == "校核因子")
                {
                    lstRatio = new List<ParaData>();

                    if (lstJHRatio != null && lstJHRatio.Count > 0)
                    {
                        foreach (ParaData data in lstJHRatio)
                        {
                            lstRatio.Add(data);
                        }
                    }
                }
                else if (cmbCurrentSel.Text == "技术因子")
                {
                    lstRatio = new List<ParaData>();

                    if (lstTeachRatio != null && lstTeachRatio.Count > 0)
                    {
                        foreach (ParaData data in lstTeachRatio)
                        {
                            lstRatio.Add(data);
                        }
                    }
                }
                else
                {
                    lstRatio = new List<ParaData>();
                    string strParaName1 = string.Empty;
                    string strParaName2 = string.Empty;

                    if (lstJHRatio != null && lstTeachRatio != null && lstJHRatio.Count > 0 && lstTeachRatio.Count > 0)
                    {
                        foreach (ParaData data1 in lstJHRatio)
                        {
                            strParaName1 = data1.paraName.Substring(5, data1.paraName.Length - 5);
                            foreach (ParaData data2 in lstTeachRatio)
                            {
                                strParaName2 = data2.paraName.Substring(5, data2.paraName.Length - 5);

                                if (strParaName1 == strParaName2 && data1.strRemark == data2.strRemark)
                                {
                                    ParaData data = new ParaData();
                                    data.paraName = data1.paraName;
                                    data.paraType = 8;
                                    data.strRemark = data1.strRemark;
                                    data.paraValue = data1.paraValue * data2.paraValue;

                                    lstRatio.Add(data);
                                    break;
                                }
                            }
                        }
                    }
                }

                //设置修正因子
                SetRatioData(lstRatio);

                //显示柱状图
                //DisplayInPicture(lstJHRatio, lstTeachRatio, lstRatio);
                DisplayAdjustmentInPic(lstJHRatio, lstTeachRatio, lstRatio);
            }
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 载入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeightAdjustmentForm_Load(object sender, EventArgs e)
        {
            SetPageData();

            if (weightAdjustment != null)
            {
                IsReCal = true;
                txtAdjustmentDataName.Enabled = false;
                treeViewWeightData.Focus();
            }
            else
            {
                IsReCal = false;
                txtAdjustmentDataName.Enabled = true;
            }
        }

        /// <summary>
        /// “取消”关闭重量调整对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 导入当前重量设计数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tSMItm_ImportWeightDesignData_Click(object sender, EventArgs e)
        {
            if (mainForm.designProjectData == null)
            {
                MessageBox.Show("当前重量设计结果没有数据");
                return;
            }

            if (mainForm.designProjectData.lstWeightArithmetic == null || mainForm.designProjectData.lstWeightArithmetic.Count == 0)
            {
                MessageBox.Show("当前重量设计结果没有数据");
                return;
            }

            Form selectWghtDatFrm = new SelectWeightDataForm(this, mainForm.designProjectData, "重量设计结果列表", false);
            selectWghtDatFrm.Text = "选择当前重量设计结果";
            selectWghtDatFrm.ShowDialog();
        }

        /// <summary>
        /// 导入当前重量调整数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tSMItm_ImportWeightAdjustData_Click(object sender, EventArgs e)
        {
            if (mainForm.designProjectData == null)
            {
                MessageBox.Show("重量调整结果没有数据");
                return;
            }
            if (mainForm.designProjectData.lstAdjustmentResultData == null || mainForm.designProjectData.lstAdjustmentResultData.Count == 0)
            {
                MessageBox.Show("重量调整结果没有数据");
                return;
            }

            Form selectWghtDatFrm = new SelectWeightDataForm(this, mainForm.designProjectData, "重量调整结果列表", false);
            selectWghtDatFrm.Text = "选择重量调整结果";
            selectWghtDatFrm.ShowDialog();
        }

        /// <summary>
        /// 从数据库导入型号重量数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tSMItm_ImportModelWeightDataFromDB_Click(object sender, EventArgs e)
        {
            Form selectWghtDatFrm = new SelectWeightDataForm(this, "型号重量数据列表", false);
            selectWghtDatFrm.Text = "选择数据库型号重量数据";
            selectWghtDatFrm.ShowDialog();
        }

        /// <summary>
        /// 从数据库导入重量设计数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tSMItm_ImportWeightDesignDataFromDB_Click(object sender, EventArgs e)
        {
            Form selectWghtDatFrm = new SelectWeightDataForm(this, "重量设计数据列表", true);
            selectWghtDatFrm.Text = "选择数据库重量设计数据";
            selectWghtDatFrm.Show();
        }

        /// <summary>
        /// 从数据文件导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tSMItm_ImportFromDataFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "xml文件 (*.xml)|*.xml|Excle文件 (*.xls)|*.xls|All File (*.*)|*.*";
            fileDialog.RestoreDirectory = true;
            fileDialog.FilterIndex = 1;

            //获取重量分类
            WeightSortData sortData = null;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = fileDialog.FileName;

                //bool IsRight = IsImortWeightDataFileFormat(strFilePath);
                //if (IsRight == false)
                //{
                //    XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                //    return;
                //}

                if (strFilePath.EndsWith(".xls"))
                {
                    sortData = GetXlsImportSortData(strFilePath);
                }
                else if (strFilePath.EndsWith(".xml"))
                {
                    sortData = GetXmlImporSortData(strFilePath);
                }
                else
                {
                    XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                    MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                    return;
                }

                if (sortData == null)
                {
                    return;
                }
                else if (sortData.lstWeightData.Count == 0)
                {
                    XLog.Write("导入文件无重量分类数据");
                    return;
                }

                BindWeightData(sortData);
                SetBasicWeightData(WeightSortData.GetListWeightData(sortData));
                SetRatioGridView(WeightSortData.GetListWeightData(sortData));
            }
        }

        /// <summary>
        /// 导出至数据文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tSMItm_ExportToDataFile_Click(object sender, EventArgs e)
        {
            if (basicWeightData == null)
            {
                XLog.Write("没有基础重量数据不能导出");
                MessageBox.Show("没有基础重量数据不能导出");
                return;
            }

            ExportDataToDataFile(basicWeightData);
        }

        private void dgvBaseWeight_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            WeightSortData sortData = GetCurrentSortData();
            BindWeightData(sortData);
        }

        private TreeNode TraverseTree(TreeNode node, string ntext, bool isTraverseChild)
        {
            if (node.Text != ntext)
            {
                if ((!isTraverseChild) && node.Nodes.Count > 0)
                {
                    node = node.Nodes[0];
                    return TraverseTree(node, ntext, false);
                }
                else if (node.NextNode != null)
                {
                    node = node.NextNode;
                    return TraverseTree(node, ntext, false);
                }
                else if (node.Parent != null)
                {
                    node = node.Parent;
                    return TraverseTree(node, ntext, true);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return node;
            }
        }

        private void dgvBaseWeight_DataError(object sender, System.Windows.Forms.DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("非法数字,请重新输入");
        }

        /// <summary>
        /// 计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculation_Click(object sender, EventArgs e)
        {
            if (txtAdjustmentDataName.Text == string.Empty)
            {
                MessageBox.Show("请输入调整数据名称");
                return;
            }
            else
            {
                if (Verification.IsCheckString(txtAdjustmentDataName.Text))
                {
                    MessageBox.Show("调整数据名称不能输入非法字符");
                    return;
                }
            }

            if (basicWeightData == null)
            {
                MessageBox.Show("请导入基础重量数据");
                return;
            }
            if (cmbCurrentSel.Text == "校核因子" || cmbCurrentSel.Text == "校核因子和技术因子")
            {
                if (lstJHRatio == null || lstJHRatio.Count == 0)
                {
                    MessageBox.Show("请导入校核因子");
                    return;
                }
            }
            if (cmbCurrentSel.Text == "技术因子" || cmbCurrentSel.Text == "校核因子和技术因子")
            {
                if (lstTeachRatio == null || lstTeachRatio.Count == 0)
                {
                    MessageBox.Show("请导入技术因子");
                    return;
                }
            }

            //调整后的重量数据
            modifyWeightSortData = getWeightModified(basicWeightData, lstRatio);

            //重量调整数据
            if (weightAdjustment == null)
            {
                weightAdjustment = new WeightAdjustmentResultData();
            }
            weightAdjustment.WeightAdjustName = txtAdjustmentDataName.Text;
            weightAdjustment.SortName = basicWeightData.sortName;
            weightAdjustment.basicWeightData = basicWeightData;
            weightAdjustment.weightAdjustData = modifyWeightSortData;
            weightAdjustment.technologyFactor = lstTeachRatio;
            weightAdjustment.checkFactor = lstJHRatio;

            //关联主页面
            if (mainForm.designProjectData == null)
            {
                mainForm.designProjectData = new DesignProjectData();
            }

            if (mainForm.designProjectData.lstAdjustmentResultData == null)
            {
                mainForm.designProjectData.lstAdjustmentResultData = new List<WeightAdjustmentResultData>();
            }

            //重新计算
            if (IsReCal)
            {
                mainForm.SetWeightAdjustmentResult(weightAdjustment);
                XLog.Write("重量调整数据\"" + txtAdjustmentDataName.Text + "\"重新计算完成！");
            }
            else
            {
                mainForm.designProjectData.lstAdjustmentResultData.Add(weightAdjustment);
                mainForm.BindProjectTreeData(mainForm.designProjectData);
                mainForm.SetWeightAdjustTab(weightAdjustment, mainForm.designProjectData.lstAdjustmentResultData.Count - 1);

                XLog.Write("重量调整数据\"" + txtAdjustmentDataName.Text + "\"计算完成！");
            }

            this.Close();
        }

        #region 菜单操作

        /// <summary>
        /// 导入当前重量设计数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolMemuImportWeightDesignData_Click(object sender, EventArgs e)
        {
            if (mainForm.designProjectData == null)
            {
                MessageBox.Show("当前重量设计结果没有数据");
                return;
            }

            if (mainForm.designProjectData.lstWeightArithmetic == null || mainForm.designProjectData.lstWeightArithmetic.Count == 0)
            {
                MessageBox.Show("当前重量设计结果没有数据");
                return;
            }

            Form selectWghtDatFrm = new SelectWeightDataForm(this, mainForm.designProjectData, "重量设计结果列表", false);
            selectWghtDatFrm.Text = "选择当前重量设计结果";
            selectWghtDatFrm.ShowDialog();
        }

        /// <summary>
        /// 导入当前重量调整数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolMemuImportAdjustmentData_Click(object sender, EventArgs e)
        {
            if (mainForm.designProjectData == null)
            {
                MessageBox.Show("重量调整结果没有数据");
                return;
            }
            if (mainForm.designProjectData.lstAdjustmentResultData == null || mainForm.designProjectData.lstAdjustmentResultData.Count == 0)
            {
                MessageBox.Show("重量调整结果没有数据");
                return;
            }

            Form selectWghtDatFrm = new SelectWeightDataForm(this, mainForm.designProjectData, "重量调整结果列表", false);
            selectWghtDatFrm.Text = "选择重量调整结果";
            selectWghtDatFrm.ShowDialog();
        }

        /// <summary>
        /// 从数据库导入型号重量数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolMemuImportTypeWeightData_Click(object sender, EventArgs e)
        {
            Form selectWghtDatFrm = new SelectWeightDataForm(this, "型号重量数据列表", false);
            selectWghtDatFrm.Text = "选择数据库型号重量数据";
            selectWghtDatFrm.ShowDialog();
        }

        /// <summary>
        /// 从数据库导入重量设计数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolMemuFromDBImportWeightDesignData_Click(object sender, EventArgs e)
        {
            Form selectWghtDatFrm = new SelectWeightDataForm(this, "重量设计数据列表", true);
            selectWghtDatFrm.Text = "选择数据库重量设计数据";
            selectWghtDatFrm.Show();
        }

        /// <summary>
        /// 从数据文件导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolMemuFormDataFileImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "xml文件 (*.xml)|*.xml|Excle文件 (*.xls)|*.xls|All File (*.*)|*.*";
            fileDialog.RestoreDirectory = true;
            fileDialog.FilterIndex = 1;

            //获取重量分类
            WeightSortData sortData = null;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = fileDialog.FileName;

                //bool IsRight = IsImortWeightDataFileFormat(strFilePath);
                //if (IsRight == false)
                //{
                //    XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                //    return;
                //}

                if (strFilePath.EndsWith(".xls"))
                {
                    sortData = GetXlsImportSortData(strFilePath);
                }
                else if (strFilePath.EndsWith(".xml"))
                {
                    sortData = GetXmlImporSortData(strFilePath);
                }
                else
                {
                    XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                    MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                    return;
                }
                if (sortData == null)
                {
                    return;
                }
                else if (sortData != null && sortData.lstWeightData.Count == 0)
                {
                    XLog.Write("导入文件无重量分类数据");
                    return;
                }

                BindWeightData(sortData);
                SetBasicWeightData(WeightSortData.GetListWeightData(sortData));
                SetRatioGridView(WeightSortData.GetListWeightData(sortData));
            }
        }

        /// <summary>
        /// 导出至数据文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolMemuExportToDataFile_Click(object sender, EventArgs e)
        {
            if (basicWeightData == null)
            {
                XLog.Write("没有基础重量数据不能导出");
                MessageBox.Show("没有基础重量数据不能导出");
                return;
            }

            ExportDataToDataFile(basicWeightData);
        }

        #endregion

        /// <summary>
        /// 从数据文件导入修正因子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolFromDataFileImportRatio_Click(object sender, EventArgs e)
        {
            FormDataFileImportRatio("校核因子");
        }

        private void dgvCorrectFactor_DataError(object sender, System.Windows.Forms.DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("非法数字,请重新输入");
        }

        private void cmbCurrentSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((lstJHRatio != null && lstJHRatio.Count > 0) || (lstTeachRatio != null && lstTeachRatio.Count > 0))
            {
                if (preSelText != cmbCurrentSel.Text)
                {
                    DialogResult result = MessageBox.Show("当前选择会清空当前数据?", "继续提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        InitializeData();
                        List<WeightData> lstBasicWeightData = WeightSortData.GetListWeightData(basicWeightData);
                        SetRatioGridView(lstBasicWeightData);

                        preSelText = cmbCurrentSel.Text;
                    }
                    else
                    {
                        cmbCurrentSel.Text = preSelText;
                        return;
                    }
                }
            }
            else
            {
                //有基础重量数据
                if (basicWeightData != null && basicWeightData.lstWeightData != null && basicWeightData.lstWeightData.Count > 0)
                {
                    List<WeightData> lstBasicWeightData = WeightSortData.GetListWeightData(basicWeightData);
                    SetRatioGridView(lstBasicWeightData);
                }
                preSelText = cmbCurrentSel.Text;
            }

        }

        /// <summary>
        /// 导出修正因子数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolMemuExportRatioToDataFile_Click(object sender, EventArgs e)
        {
            //校核因子
            if (lstJHRatio != null && lstJHRatio.Count > 0)
            {
                ExportRatioDataToDataFile(lstJHRatio);
            }
            else
            {
                MessageBox.Show("没有校核因子数据，不能导出");
            }
        }

        private void dgvCorrectFactor_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string strJHRatio = string.Empty;
            string strTechRatio = string.Empty;
            if (dgvCorrectFactor.Rows.Count > 0)
            {
                for (int i = 0; i < dgvCorrectFactor.Rows.Count; i++)
                {
                    if (dgvCorrectFactor.Rows[i].Cells[0].Value.ToString() == "校核因子")
                    {
                        strJHRatio = "校核因子";
                    }
                    if (dgvCorrectFactor.Rows[i].Cells[0].Value.ToString() == "技术因子")
                    {
                        strTechRatio = "技术因子";
                    }
                }
            }

            if (strJHRatio == "校核因子")
            {
                //更新校核因子
                UpDateJHRatioData();
            }

            if (strTechRatio == "技术因子")
            {
                //更新技术因子
                UpDateTechRatioData();
            }

            //更新修正因子
            UpDateModifyRatioData();

            //图形显示
            //DisplayInPicture(lstJHRatio, lstTeachRatio, lstRatio);
            DisplayAdjustmentInPic(lstJHRatio, lstTeachRatio, lstRatio);

            SetRatioData(lstRatio);
        }

        private void tSMItm_ImptFactorFromDataFile_Click(object sender, EventArgs e)
        {
            FormDataFileImportRatio("校核因子");
        }

        private void toolMemuImportTechRatio_Click(object sender, EventArgs e)
        {
            FormDataFileImportRatio("技术因子");
        }

        /// <summary>
        /// 从数据文件导入技术因子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolMemuFromDataFileImportTechRatio_Click(object sender, EventArgs e)
        {
            FormDataFileImportRatio("技术因子");
        }

        private void tSMItm_ExptFactorToDataFile_Click(object sender, EventArgs e)
        {
            //校核因子
            if (lstJHRatio != null && lstJHRatio.Count > 0)
            {
                ExportRatioDataToDataFile(lstJHRatio);
            }
            else
            {
                MessageBox.Show("没有校核因子数据，不能导出");
            }
        }

        private void toolStripMenuItemExportTechRatioToDataFile_Click(object sender, EventArgs e)
        {
            //技术因子
            if (lstTeachRatio != null && lstTeachRatio.Count > 0)
            {
                ExportRatioDataToDataFile(lstTeachRatio);
            }
            else
            {
                MessageBox.Show("没有技术因子数据，不能导出");
            }
        }

        private void toolStripMenuItemExportTechRatio_Click(object sender, EventArgs e)
        {
            //技术因子
            if (lstTeachRatio != null && lstTeachRatio.Count > 0)
            {
                ExportRatioDataToDataFile(lstTeachRatio);
            }
            else
            {
                MessageBox.Show("没有技术因子数据，不能导出");
            }
        }

        #endregion

        /// <summary>
        /// 图片保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemRatioCompare_Click(object sender, EventArgs e)
        {
            MainForm.SavePictureToFile(chartAdjustment);
        }

        private void chartAdjustment_MouseDown(object sender, MouseEventArgs e)
        {
            treeViewWeightData.Focus();
        }

        private void toolStripMenuItemDisplayData_Click(object sender, EventArgs e)
        {
            if (rightCount % 2 == 0)
            {
                for (int i = 0; i < chartAdjustment.Series.Count; i++)
                {
                    chartAdjustment.Series[i].IsValueShownAsLabel = true;
                }
                toolStripMenuItemDisplayData.Text = "隐藏数据";
            }
            else
            {
                for (int i = 0; i < chartAdjustment.Series.Count; i++)
                {
                    chartAdjustment.Series[i].IsValueShownAsLabel = false;
                }
                toolStripMenuItemDisplayData.Text = "显示数据";
            }
            rightCount++;
        }
    }
}
