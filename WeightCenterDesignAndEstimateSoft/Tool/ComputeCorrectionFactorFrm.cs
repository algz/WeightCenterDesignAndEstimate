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
using BLL;
using System.Xml;
using ZaeroModelSystem;
using ZedGraph;
using System.Collections;

namespace WeightCenterDesignAndEstimateSoft.Tool
{
    public partial class ComputeCorrectionFactorFrm : Form
    {
        #region 属性

        private MainForm mainForm = null;

        /// <summary>
        /// 重量数据类型
        /// </summary>
        public string strWeightDataType = string.Empty;

        private string ratioType = string.Empty;

        /// <summary>
        /// 重量数据1
        /// </summary>
        public WeightSortData importSortData1 = null;
        /// <summary>
        /// 重量数据2
        /// </summary>
        public WeightSortData importSortData2 = null;
        /// <summary>
        /// 修正因子
        /// </summary>
        public List<ParaData> lstCalculateRatio = null;

        private const int digit = 6;
        private const int digitPic = 3;

        public ComputeCorrectionFactorFrm()
        {
            InitializeComponent();
        }

        public ComputeCorrectionFactorFrm(MainForm main_form)
        {
            InitializeComponent();
            gridWeightData.Columns[1].ValueType = System.Type.GetType("System.Decimal");
            gridWeightData.Columns[2].ValueType = System.Type.GetType("System.Decimal");
            gridWeightData.Columns[3].ValueType = System.Type.GetType("System.Decimal");

            mainForm = main_form;
        }

        #endregion

        #region 自定义方法

        public void BindWeightData(WeightSortData sortData)
        {
            treeViewSort.Nodes.Clear();
            MainForm.BindWeightDesignSortNoValue(sortData, treeViewSort);
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
            try
            {

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
                else if (strPath.EndsWith(".xls"))
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return IsRight;
        }

        /// <summary>
        /// 判断是否为最后一个节点的重量数据
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="weightSortData"></param>
        /// <returns></returns>
        private bool IsLastWeightNode(WeightData weight, WeightSortData weightSortData)
        {
            bool IsLast = true;

            if (weightSortData.lstWeightData.Count > 0)
            {
                foreach (WeightData data in weightSortData.lstWeightData)
                {
                    if (weight.nID == data.nParentID)
                    {
                        return false;
                    }
                }
            }

            return IsLast;
        }

        /// <summary>
        /// 获取基层节点的重量数据列表
        /// </summary>
        public List<WeightData> GetListWeightData(WeightSortData sortData)
        {
            List<WeightData> lstWeightData = null;

            if (sortData != null && sortData.lstWeightData != null && sortData.lstWeightData.Count > 0)
            {
                lstWeightData = new List<WeightData>();

                foreach (WeightData data in sortData.lstWeightData)
                {
                    if (IsLastWeightNode(data, sortData))
                    {
                        lstWeightData.Add(data);
                    }
                }
            }

            return lstWeightData;
        }

        /// <summary>
        /// 设置列表的数据
        /// </summary>
        /// <param name="strWeightType">重量数据类型</param>
        /// <param name="lstWeightData">修正因子列表</param>
        /// <param name="IsRemove">是否清空所有数据</param>
        /// <param name="IsRemoveRatio">是否清空因子数据</param>
        public void SetGridViewData(string strWeightType, List<WeightData> lstWeightData, bool IsRemoveAll, bool IsRemoveRatio)
        {
            if (lstWeightData == null || lstWeightData.Count == 0)
            {
                gridWeightData.Rows.Clear();
            }

            if (lstWeightData != null && lstWeightData.Count > 0)
            {
                //清空数据
                if (IsRemoveAll || importSortData2 == null)
                {
                    gridWeightData.Rows.Clear();
                }

                if (gridWeightData.Rows.Count == 0)
                {
                    gridWeightData.Rows.Add(lstWeightData.Count);
                }

                //重量数据1
                if (strWeightType == "data1")
                {
                    for (int i = 0; i < gridWeightData.Rows.Count; i++)
                    {
                        gridWeightData.Rows[i].Cells[0].Value = lstWeightData[i].weightName;
                        gridWeightData.Rows[i].Cells[0].ToolTipText = lstWeightData[i].nID.ToString();
                        gridWeightData.Rows[i].Cells[1].Value = lstWeightData[i].weightValue;
                        gridWeightData.Rows[i].Cells[1].ToolTipText = lstWeightData[i].nID.ToString();
                    }
                }

                //重量数据1
                if (strWeightType == "data2")
                {
                    for (int i = 0; i < gridWeightData.Rows.Count; i++)
                    {
                        foreach (WeightData data in lstWeightData)
                        {
                            if (gridWeightData.Rows[i].Cells[0].Value != null && gridWeightData.Rows[i].Cells[0].ToolTipText == data.nID.ToString())
                            {
                                gridWeightData.Rows[i].Cells[2].Value = lstWeightData[i].weightValue;
                                gridWeightData.Rows[i].Cells[2].ToolTipText = lstWeightData[i].nID.ToString();

                            }
                        }
                    }

                    //清空修正因子
                    if (IsRemoveRatio)
                    {
                        for (int i = 0; i < gridWeightData.Rows.Count; i++)
                        {
                            gridWeightData.Rows[i].Cells[3].Value = null;
                        }
                    }
                }
            }
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
        /// 设置修正因子数据
        /// </summary>
        private void SetCalculateRatioData(string strRatioType)
        {
            ratioType = strRatioType;

            List<ParaData> lstPara = WeightSortData.GetlstCalculateRatio(strRatioType, importSortData1, importSortData2);
            lstCalculateRatio = lstPara;
            if (gridWeightData.Rows.Count > 0 && lstPara != null && lstPara.Count > 0)
            {
                for (int i = 0; i < gridWeightData.Rows.Count; i++)
                {
                    gridWeightData.Rows[i].Cells[3].Value = Math.Round(lstPara[i].paraValue, digit);
                    if (lstPara[i].paraValue < 0)
                    {
                        gridWeightData.Rows[i].Cells[3].Style.ForeColor = Color.Red;
                    }

                }
            }
        }

        ///// <summary>
        ///// 找到重量分类对应的基层重量
        ///// </summary>
        ///// <param name="_weightSortData"></param>
        ///// <returns></returns>
        //private List<WeightData> getBasicWeightData(WeightSortData _weightSortData)
        //{
        //    List<WeightData> lstWeightData = null;
        //    WeightData tempWeightData = null;
        //    int triger = 0;
        //    if (_weightSortData == null || _weightSortData.lstWeightData.Count == 0)
        //    {
        //        return lstWeightData;
        //    }
        //    else
        //    {
        //        for (int i = 0; i < _weightSortData.lstWeightData.Count; i = i + 1)
        //        {
        //            tempWeightData = _weightSortData.lstWeightData[i];
        //            triger = 0;
        //            for (int j = 0; i < _weightSortData.lstWeightData.Count; j = j + 1)
        //            {
        //                if (_weightSortData.lstWeightData[j].nParentID == tempWeightData.nID)
        //                {
        //                    triger = triger + 1;
        //                }
        //            }
        //            if (triger == 0)
        //            {
        //                lstWeightData.Add(tempWeightData);
        //            }
        //        }
        //        return lstWeightData;
        //    }
        //}

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
        /// 画修正因子柱状图
        /// </summary>
        /// <param name="lstTempPara"></param>
        private void DisplayInPicture(List<ParaData> lstTempPara, string strType)
        {
            GraphPane myPane = zedGraphControlPic.GraphPane;

            //清除原来的图形
            myPane.CurveList.Clear();
            myPane.GraphObjList.Clear();

            //设置图例   LegendPos.Right;
            myPane.Legend.Position = LegendPos.Right;
            myPane.Legend.IsHStack = true;
            myPane.Legend.FontSpec.Size = 12.0F;
            myPane.Legend.FontSpec.FontColor = Color.Navy;

            //柱状图标题 
            myPane.Title.FontSpec.IsBold = true;
            myPane.Title.FontSpec.FontColor = Color.Navy;
            myPane.Title.Text = strType;

            string[] xLables = null;
            double[] dValue = null;

            if (lstTempPara != null && lstTempPara.Count > 0)
            {
                xLables = new string[lstTempPara.Count];
                dValue = new double[lstTempPara.Count];
                double y = 0;

                for (int i = 0; i < lstTempPara.Count; i++)
                {
                    y = Math.Round(lstTempPara[i].paraValue, digitPic);

                    dValue[i] = y;
                    xLables[i] = lstTempPara[i].paraName;
                }
            }

            // 设置x轴为文本显示             
            myPane.XAxis.Type = AxisType.Text;
            myPane.XAxis.Scale.TextLabels = xLables;
            //文字竖着放
            myPane.XAxis.Scale.FontSpec.Angle = -90;
            //设置x轴标签字体             
            myPane.XAxis.Scale.FontSpec.Family = "宋体";
            myPane.XAxis.Scale.FontSpec.Size = 8.0F;

            myPane.XAxis.MajorTic.IsBetweenLabels = true;

            BarItem myCurveCur = myPane.AddBar(string.Empty, null, dValue, Color.Blue);

            // 为每个“柱子”上方添加值标签             
            for (int i = 0; i < dValue.Length; i++)
            {
                double Y = dValue[i];
                TextObj text = new TextObj(Y.ToString(), i + 1, Y + 0.5);
                text.FontSpec.Border.IsVisible = false;
                text.FontSpec.Fill.IsVisible = false;
                text.FontSpec.Size = 8.0F;

                myPane.GraphObjList.Add(text);
            }

            myPane.Chart.Fill = new Fill(Color.White, Color.FromArgb(255, 255, 166));
            myPane.Fill = new Fill(Color.White, Color.FromArgb(200, 200, 255));

            myPane.YAxis.Scale.MajorStep = 5;

            myPane.BarSettings.Type = BarType.SortedOverlay;

            zedGraphControlPic.AxisChange();
            zedGraphControlPic.Refresh();
        }

        /// <summary>
        /// 初始化图表
        /// </summary>
        public void InitializezedGraphControl()
        {
            GraphPane myPane = zedGraphControlPic.GraphPane;

            //清除原来的图形
            myPane.CurveList.Clear();
            myPane.GraphObjList.Clear();

            myPane.Title.Text = "标题";
            myPane.XAxis.Scale.TextLabels = null;

            zedGraphControlPic.AxisChange();
            zedGraphControlPic.Refresh();
        }

        /// <summary>
        /// 修改重量数据1
        /// </summary>
        private void UpdataSortData1()
        {
            if (importSortData1 != null && importSortData1.lstWeightData.Count > 0)
            {
                for (int i = 0; i < gridWeightData.RowCount; i++)
                {
                    foreach (WeightData data in importSortData1.lstWeightData)
                    {
                        if (gridWeightData.Rows[i].Cells[1].ToolTipText == data.nID.ToString())
                        {
                            data.weightValue = Convert.ToDouble(gridWeightData.Rows[i].Cells[1].Value.ToString());
                        }
                    }
                }

                //求和
                foreach (WeightData data in importSortData1.lstWeightData)
                {
                    WeightSortData.GetSortDataTotal(data, importSortData1);
                }
            }
        }

        /// <summary>
        /// 修改重量数据2
        /// </summary>
        private void UpdataSortData2()
        {
            if (importSortData2 != null && importSortData2.lstWeightData.Count > 0)
            {
                for (int i = 0; i < gridWeightData.RowCount; i++)
                {
                    foreach (WeightData data in importSortData2.lstWeightData)
                    {
                        if (gridWeightData.Rows[i].Cells[2].ToolTipText == data.nID.ToString())
                        {
                            data.weightValue = Convert.ToDouble(gridWeightData.Rows[i].Cells[2].Value.ToString());
                        }
                    }
                }

                //求和
                foreach (WeightData data in importSortData2.lstWeightData)
                {
                    WeightSortData.GetSortDataTotal(data, importSortData2);
                }
            }
        }

        /// <summary>
        /// 更新修正因子列表
        /// </summary>
        private void UpdatalListCalculateRatio()
        {
            if (lstCalculateRatio != null && lstCalculateRatio.Count > 0)
            {
                for (int i = 0; i < gridWeightData.RowCount; i++)
                {
                    lstCalculateRatio[i].paraValue = Convert.ToDouble(gridWeightData.Rows[i].Cells[3].Value.ToString());
                }
            }
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 从当前重量设计数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void memu1FromCurrentWeighDesignImport_Click(object sender, EventArgs e)
        {
            if (mainForm.designProjectData == null)
            {
                MessageBox.Show("当前重量设计没有数据");
                XLog.Write("当前重量设计没有数据");
                return;
            }
            else
            {
                if (mainForm.designProjectData.lstWeightArithmetic == null || mainForm.designProjectData.lstWeightArithmetic.Count == 0)
                {
                    MessageBox.Show("当前重量设计没有数据");
                    XLog.Write("当前重量设计没有数据");
                    return;
                }
            }

            strWeightDataType = "data1";
            ImportWeightDataListForm form = new ImportWeightDataListForm(this, mainForm.designProjectData, "weightDesign");
            form.ShowDialog();
        }

        /// <summary>
        /// 从型号重量数据库导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void memu1FromTypeDBImport_Click(object sender, EventArgs e)
        {
            BLLTypeWeightData bllTypeWeight = new BLLTypeWeightData();

            List<TypeWeightData> lstTypeWeight = bllTypeWeight.GetListModel();

            if (lstTypeWeight.Count == 0)
            {
                XLog.Write("数据库没有型号重量数据");
                MessageBox.Show("数据库没有型号重量数据");
                return;
            }
            strWeightDataType = "data1";
            ImportWeightDataListForm form = new ImportWeightDataListForm(this, lstTypeWeight, "typeDB");
            form.ShowDialog();
        }

        /// <summary>
        /// 从重量设计数据库导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void memu1FromWeightDesignDBImport_Click(object sender, EventArgs e)
        {
            BLLWeightDesignData bllWeightDesign = new BLLWeightDesignData();
            List<WeightDesignData> lstWeightDesign = bllWeightDesign.GetListModel();

            if (lstWeightDesign.Count == 0)
            {
                XLog.Write("数据库没有重量设计数据");
                MessageBox.Show("数据库没有重量设计数据");
            }

            strWeightDataType = "data1";
            ImportWeightDataListForm form = new ImportWeightDataListForm(this, lstWeightDesign, "weightDB");
            form.ShowDialog();
        }

        /// <summary>
        /// 重量数据1从数据文件导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void memu1FromDataFileImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "xml文件 (*.xml)|*.xml|Excle文件 (*.xls)|*.xls|All files (*.*)|*.*";
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
                //    MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                //    return;
                //}

                if (strFilePath.EndsWith(".xls"))
                {
                    sortData = WeightSortData.GetXlsImportSortData(strFilePath);
                }
                else if (strFilePath.EndsWith(".xml"))
                {
                    sortData = WeightSortData.GetXmlImporSortData(strFilePath);
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
                    MessageBox.Show("导入文件\"" + strFilePath + "\"没有数据");
                    XLog.Write("导入文件\"" + strFilePath + "\"没有数据");
                    return;
                }
                strWeightDataType = "data1";
                List<WeightData> lstWeightData = GetListWeightData(sortData);

                if (importSortData2 != null)
                {
                    DialogResult result = MessageBox.Show("继续导入会清空所有数据?", "继续提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        SetGridViewData(strWeightDataType, lstWeightData, true, false);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    SetGridViewData(strWeightDataType, lstWeightData, true, false);
                }

                InitializezedGraphControl();
                BindWeightData(sortData);
                importSortData1 = sortData;
                importSortData2 = null;
                lstCalculateRatio = null;
            }
        }

        /// <summary>
        /// 重量数据1导出至数据文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void memu1ExportToDataFile_Click(object sender, EventArgs e)
        {
            if (importSortData1 == null)
            {
                XLog.Write("重量数据1没有数据不能导出");
                MessageBox.Show("重量数据1没有数据不能导出");
                return;
            }

            ExportDataToDataFile(importSortData1);
        }

        private void memu2CurrentAdjustmentImport_Click(object sender, EventArgs e)
        {
            if (treeViewSort.Nodes.Count == 0)
            {
                XLog.Write("先导入重量数据1数据");
                MessageBox.Show("先导入重量数据1数据");
                return;
            }

            if (mainForm.designProjectData == null)
            {
                MessageBox.Show("重量调整结果没有数据");
                XLog.Write("重量调整结果没有数据");
                return;
            }
            if (mainForm.designProjectData.lstAdjustmentResultData == null || mainForm.designProjectData.lstAdjustmentResultData.Count == 0)
            {
                MessageBox.Show("重量调整结果没有数据");
                XLog.Write("重量调整结果没有数据");
                return;
            }
            strWeightDataType = "data2";
            ImportWeightDataListForm form = new ImportWeightDataListForm(this, mainForm.designProjectData, "weightAdjust");
            form.ShowDialog();
        }

        private void memu2FromDBTypeDataImport_Click(object sender, EventArgs e)
        {
            if (treeViewSort.Nodes.Count == 0)
            {
                XLog.Write("先导入重量数据1数据");
                MessageBox.Show("先导入重量数据1数据");
                return;
            }

            BLLTypeWeightData bllTypeWeight = new BLLTypeWeightData();
            List<TypeWeightData> lstTypeWeight = bllTypeWeight.GetListModel();

            if (lstTypeWeight.Count == 0)
            {
                XLog.Write("数据库没有型号重量数据");
                MessageBox.Show("数据库没有型号重量数据");
                return;
            }

            strWeightDataType = "data2";
            ImportWeightDataListForm form = new ImportWeightDataListForm(this, lstTypeWeight, "typeDB");
            form.ShowDialog();
        }

        private void memu2FromDBWeightDataImport_Click(object sender, EventArgs e)
        {
            if (treeViewSort.Nodes.Count == 0)
            {
                XLog.Write("先导入重量数据1数据");
                MessageBox.Show("先导入重量数据1数据");
                return;
            }

            BLLWeightDesignData bllWeightDesign = new BLLWeightDesignData();
            List<WeightDesignData> lstWeightDesign = bllWeightDesign.GetListModel();

            if (lstWeightDesign.Count == 0)
            {
                XLog.Write("数据库没有重量设计数据");
                MessageBox.Show("数据库没有重量设计数据");
                return;
            }

            strWeightDataType = "data2";
            ImportWeightDataListForm form = new ImportWeightDataListForm(this, lstWeightDesign, "weightDB");
            form.ShowDialog();
        }

        private void memu2FromDataFileImport_Click(object sender, EventArgs e)
        {
            if (treeViewSort.Nodes.Count == 0)
            {
                XLog.Write("先导入重量数据1数据");
                MessageBox.Show("先导入重量数据1数据");
                return;
            }

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "xml文件 (*.xml)|*.xml|Excle文件 (*.xls)|*.xls|All files (*.*)|*.*";
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
                    sortData = WeightSortData.GetXlsImportSortData(strFilePath);
                }
                else if (strFilePath.EndsWith(".xml"))
                {
                    sortData = WeightSortData.GetXmlImporSortData(strFilePath);
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
                if (sortData != null && sortData.lstWeightData.Count == 0)
                {
                    MessageBox.Show("导入文件\"" + strFilePath + "\"没有数据");
                    XLog.Write("导入文件\"" + strFilePath + "\"没有数据");
                    return;
                }

                //判断导入重量分类是否一致
                bool IsSame = WeightSortData.blIsSame(importSortData1, sortData);

                if (IsSame == false)
                {
                    XLog.Write("导入数据和重量数据1重量分类不一致");
                    MessageBox.Show("导入数据和重量数据1重量分类不一致");
                    return;
                }
                List<WeightData> lstWeightData = GetListWeightData(sortData);

                strWeightDataType = "data2";
                if (lstCalculateRatio != null && lstCalculateRatio.Count > 0)
                {
                    DialogResult result = MessageBox.Show("继续导入会清空因子数据?", "继续提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        lstCalculateRatio = null;
                        importSortData2 = sortData;
                        SetGridViewData(strWeightDataType, lstWeightData, false, true);
                        InitializezedGraphControl();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    importSortData2 = sortData;
                    SetGridViewData(strWeightDataType, lstWeightData, false, false);
                }
            }
        }

        private void memu2ExportToDataFile_Click(object sender, EventArgs e)
        {
            if (importSortData2 == null)
            {
                XLog.Write("重量数据2没有数据不能导出");
                MessageBox.Show("重量数据2没有数据不能导出");
                return;
            }
            ExportDataToDataFile(importSortData2);
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (lstCalculateRatio == null || lstCalculateRatio.Count == 0)
            {
                MessageBox.Show("没有修正因子数据不能导出");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml文件 (*.xml)|*.xml|Excel文件 (*.xls)|*.xls";
            dlg.OverwritePrompt = true;
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = zedGraphControlPic.GraphPane.Title.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dlg.FileName;

                if (strFilePath.EndsWith(".xml"))
                {
                    List<string> lstContent = GetFileContent(lstCalculateRatio);
                    CommonFunction.mWriteListStringToFile(strFilePath, lstContent);
                }

                if (strFilePath.EndsWith(".xls"))
                {
                    DataTable table = GetExcelTable(lstCalculateRatio);
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

        private void toolMemuCalculateTechRatio_Click(object sender, EventArgs e)
        {
            if (importSortData1 == null)
            {
                MessageBox.Show("请导入重量数据1");
                return;
            }
            if (importSortData2 == null)
            {
                MessageBox.Show("请导入重量数据2");
                return;
            }

            if (importSortData1 != null && importSortData2 != null)
            {
                SetCalculateRatioData("技术因子");

                DisplayInPicture(lstCalculateRatio, "技术因子");
            }
        }

        private void toolMemuExportRatio_Click(object sender, EventArgs e)
        {
            if (lstCalculateRatio == null || lstCalculateRatio.Count == 0)
            {
                MessageBox.Show("没有修正因子数据不能导出");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml文件 (*.xml)|*.xml|Excel文件 (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = zedGraphControlPic.GraphPane.Title.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dlg.FileName;

                if (strFilePath.EndsWith(".xml"))
                {
                    List<string> lstContent = GetFileContent(lstCalculateRatio);
                    CommonFunction.mWriteListStringToFile(strFilePath, lstContent);
                }

                if (strFilePath.EndsWith(".xls"))
                {
                    DataTable table = GetExcelTable(lstCalculateRatio);
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

        private void toolMemuCalculateJHRatio_Click(object sender, EventArgs e)
        {
            if (importSortData1 == null)
            {
                MessageBox.Show("请导入重量数据1");
                return;
            }
            if (importSortData2 == null)
            {
                MessageBox.Show("请导入重量数据2");
                return;
            }

            if (importSortData1 != null && importSortData2 != null)
            {
                SetCalculateRatioData("校核因子");

                DisplayInPicture(lstCalculateRatio, "校核因子");
            }
        }

        /// <summary>
        /// 计算校核因子事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculateJHRatio_Click(object sender, EventArgs e)
        {
            if (importSortData1 == null)
            {
                MessageBox.Show("请导入重量数据1");
                return;
            }
            if (importSortData2 == null)
            {
                MessageBox.Show("请导入重量数据2");
                return;
            }

            if (importSortData1 != null && importSortData2 != null)
            {
                SetCalculateRatioData("校核因子");

                DisplayInPicture(lstCalculateRatio, "校核因子");
            }
        }

        /// <summary>
        /// 计算技术因子事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculateTechRatio_Click(object sender, EventArgs e)
        {
            if (importSortData1 == null)
            {
                MessageBox.Show("请导入重量数据1");
                return;
            }
            if (importSortData2 == null)
            {
                MessageBox.Show("请导入重量数据2");
                return;
            }

            if (importSortData1 != null && importSortData2 != null)
            {
                SetCalculateRatioData("技术因子");

                DisplayInPicture(lstCalculateRatio, "技术因子");
            }
        }

        private void _CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (importSortData1 != null)
            {
                UpdataSortData1();
            }
            if (importSortData2 != null)
            {
                UpdataSortData2();
            }

            if (lstCalculateRatio != null && lstCalculateRatio.Count > 0)
            {
                UpdatalListCalculateRatio();
                DisplayInPicture(lstCalculateRatio, ratioType);
            }
        }

        private void gridWeightData_DataError(object sender, System.Windows.Forms.DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("非法数字,请重新输入");
        }

        /// <summary>
        /// 重量数据1从当前重量调整数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolFormAdjustWeightImport_Click(object sender, EventArgs e)
        {
            if (mainForm.designProjectData == null)
            {
                MessageBox.Show("重量调整结果没有数据");
                XLog.Write("重量调整结果没有数据");
                return;
            }
            if (mainForm.designProjectData.lstAdjustmentResultData == null || mainForm.designProjectData.lstAdjustmentResultData.Count == 0)
            {
                MessageBox.Show("重量调整结果没有数据");
                XLog.Write("重量调整结果没有数据");
                return;
            }
            strWeightDataType = "data1";
            ImportWeightDataListForm form = new ImportWeightDataListForm(this, mainForm.designProjectData, "weightAdjust");
            form.ShowDialog();
        }

        /// <summary>
        /// 重量数据2从当前重量设计数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolMemuWeihtData2FormWeightDataImport_Click(object sender, EventArgs e)
        {
            if (treeViewSort.Nodes.Count == 0)
            {
                XLog.Write("先导入重量数据1数据");
                MessageBox.Show("先导入重量数据1数据");
                return;
            }

            if (mainForm.designProjectData == null)
            {
                MessageBox.Show("当前重量设计没有数据");
                XLog.Write("当前重量设计没有数据");
                return;
            }
            else
            {
                if ((mainForm.designProjectData.lstWeightArithmetic == null || mainForm.designProjectData.lstWeightArithmetic.Count == 0)
                   && (mainForm.designProjectData.lstAdjustmentResultData == null || mainForm.designProjectData.lstAdjustmentResultData.Count == 0))
                {
                    MessageBox.Show("当前重量设计没有数据");
                    XLog.Write("当前重量设计没有数据");
                    return;
                }
            }

            strWeightDataType = "data2";
            ImportWeightDataListForm form = new ImportWeightDataListForm(this, mainForm.designProjectData, "weightDesign");
            form.ShowDialog();
        }

        #endregion
    }
}
