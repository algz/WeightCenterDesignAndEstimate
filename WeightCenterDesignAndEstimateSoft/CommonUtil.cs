using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XCommon;
using System.Xml;
using Model;
using System.IO;
using ZaeroModelSystem;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
using Model.assessData;
using System.Reflection;
using System.Collections;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;
using Microsoft.Win32;
using System.Configuration;
using System.Diagnostics;

namespace WeightCenterDesignAndEstimateSoft
{
    class CommonUtil
    {
        /// <summary>
        /// Xml文件转换成WeightSortData类型
        /// </summary>
        /// <param name="strPath">文件路径</param>
        /// <returns>返回WeightSortData类型</returns>
        public static WeightSortData GetXmlImporSortData(string strPath)
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
        ///xls文件转换成WeightSortData类型
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <returns>返回WeightSortData类型</returns>
        public static WeightSortData GetXlsImportSortData(string strFilePath)
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
        /// 导出WeightSortData类型实例至XML/XLS文件
        /// </summary>
        /// <param name="sortData">WeightSortData类型</param>
        public static void ExportDataToDataFile(WeightSortData sortData)
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
        /// 绘制饼图
        /// </summary>
        /// <param name="chart1"></param>
        /// <param name="lstWeightData"></param>
        public static void DisplayPiePic(System.Windows.Forms.DataVisualization.Charting.Chart chart1, List<WeightAssessParameter> waList, string strTitle)
        {
            if (waList != null && waList.Count > 0)
            {
                chart1.Titles.Clear();
                chart1.Series.Clear();
                chart1.ChartAreas.Clear();
                chart1.Legends.Clear();

                //lstWeightData = GetSortListWeightData(lstWeightData);
                string[] arrayXValue = new string[waList.Count];
                double[] arrayYValue = new double[waList.Count];
                for (int i = 0; i < waList.Count; i++)
                {
                    arrayYValue[i] = Math.Round(waList[i].weightedValue, 6);
                    arrayXValue[i] = waList[i].weightName;
                }

                //标题
                Title title1 = new Title();
                title1.Text = strTitle;
                title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
                chart1.Titles.Add(title1);

                //Legend
                System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
                legend1.IsTextAutoFit = true;
                legend1.Name = "Legend1";
                chart1.Legends.Add(legend1);


                //ChartArea
                ChartArea chartArea1 = new ChartArea();
                chartArea1.Name = "ChartArea1";
                chart1.ChartAreas.Add(chartArea1);

                //Series
                Series series = new Series("Series1");
                series.Points.DataBindXY(arrayXValue, arrayYValue);

                for (int i = 0; i < series.Points.Count; i++)
                {
                    series.Points[i].ToolTip = arrayXValue[i] + ":" + arrayYValue[i].ToString();
                    //series.Points[i].LabelToolTip = arrayXValue[i] + ":" + arrayYValue[i].ToString();
                    series.Points[i].IsValueShownAsLabel = true;
                }

                series.Legend = "Legend1";
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.Pie;
                series.IsValueShownAsLabel = true;
                chart1.Series.Add(series);

                chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                chart1.Series[0].CustomProperties = "PieLabelStyle=outside";
            }
        }

        /// <summary>
        /// 绘制折线图
        /// </summary>
        /// <param name="chart1"></param>
        /// <param name="lstWeightData"></param>
        public static void DisplayLinePic(System.Windows.Forms.DataVisualization.Charting.Chart chart1, List<WeightAssessParameter> waList, string strTitle)
        {
            if (waList != null && waList.Count > 0)
            {
                chart1.Titles.Clear();
                chart1.Series.Clear();
                chart1.ChartAreas.Clear();
                chart1.Legends.Clear();


                string[] xValues = new string[waList.Count];
                double[] minYValues = new double[waList.Count];
                double[] maxYValues = new double[waList.Count];
                for (int i = 0; i < waList.Count; i++)
                {
                    xValues[i] = waList[i].weightName;
                    minYValues[i] = waList[i].minValue;
                    maxYValues[i] = waList[i].maxValue;
                }

                //标题
                Title title1 = new Title();
                title1.Text = strTitle;
                title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
                chart1.Titles.Add(title1);

                //Legend
                System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
                legend1.IsTextAutoFit = true;
                legend1.Name = "Legend1";
                chart1.Legends.Add(legend1);


                //ChartArea
                ChartArea chartArea1 = new ChartArea();
                chartArea1.Name = "ChartArea1";
                chart1.ChartAreas.Add(chartArea1);
                //X坐标显示不全
                chartArea1.AxisX.Interval = 1;
                chartArea1.AxisX.IntervalOffset = 1;
                chartArea1.AxisX.LabelStyle.IsStaggered = false;


                //Series
                Series minSeries = new Series("minSeries");
                Series maxSeries = new Series("maxSeries");
                minSeries.Name = "最小值";
                maxSeries.Name = "最大值";
                minSeries.Points.DataBindXY(xValues, minYValues);
                maxSeries.Points.DataBindXY(xValues, maxYValues);

                //Series series = new Series("Series1");
                //series.Points.DataBindXY(arrayXValue, arrayYValue);

                //for (int i = 0; i < series.Points.Count; i++)
                //{
                //    //series.Points[i].ToolTip = arrayXValue[i] + ":" + arrayYValue[i].ToString();
                //    //series.Points[i].LabelToolTip = arrayXValue[i] + ":" + arrayYValue[i].ToString();
                //    series.Points[i].IsValueShownAsLabel = true;
                //}

                minSeries.Legend = maxSeries.Legend = "Legend1";
                minSeries.ChartArea = maxSeries.ChartArea = "ChartArea1";
                minSeries.ChartType = maxSeries.ChartType = SeriesChartType.Line;//SeriesChartType.Pie;
                minSeries.IsValueShownAsLabel = maxSeries.IsValueShownAsLabel = true;
                chart1.Series.Add(minSeries);
                chart1.Series.Add(maxSeries);

                //chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                //chart1.Series[0].CustomProperties = "PieLabelStyle=outside";
            }
        }

        /// <summary>
        /// 读取XML文件转换成List<CorePointExt>类型
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <returns>CorePointData集合类型</returns>
        public static List<CorePointExt> LoadXmlToPointList(string strFilePath)
        {
            List<CorePointExt> lstCorePointExt = null;
            try
            {
                if (!File.Exists(strFilePath))
                {
                    return lstCorePointExt;
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
                    lstCorePointExt = new List<CorePointExt>();

                    foreach (XmlNode childNode in nodeList)
                    {
                        CorePointExt pt = new CorePointExt();
                        pt.pointName = childNode.ChildNodes[0].InnerText;
                        pt.pointXValue = Convert.ToDouble(childNode.ChildNodes[3].InnerText);
                        pt.pointYValue = Convert.ToDouble(childNode.ChildNodes[4].InnerText);
                        if (childNode.ChildNodes.Count > 5)
                        {
                            pt.isAssess = Convert.ToBoolean(childNode.ChildNodes[5].InnerText);
                        }


                        lstCorePointExt.Add(pt);
                    }
                }
            }
            catch
            {
                XCommon.XLog.Write("导入原始重心包线数据格式错误");
                MessageBox.Show("导入原始重心包线数据格式错误!");
                return null;
            }
            return lstCorePointExt;
        }

        public static bool ExportXmlFromPointList(List<CorePointExt> ptlst, string strfilename)
        {
            List<string> lstContent = new List<string>();

            if (ptlst.Count > 0)
            {
                lstContent.Insert(0, "<?xml version=" + "\"1.0\"" + " encoding=" + "\"gb2312\"" + " ?> ");

                lstContent.Add("<重心数据>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<重心坐标列表>");

                foreach (CorePointExt pt in ptlst)
                {
                    lstContent.Add(CommonFunction.mStrModifyToString8(2) + "<重心坐标>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<重心坐标点名称>" + pt.pointName + "</重心坐标点名称>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<横轴单位>毫米</横轴单位>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<纵轴单位>千克</纵轴单位>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<横轴数值>" + pt.pointXValue.ToString() + "</横轴数值>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<纵轴数值>" + pt.pointYValue.ToString() + "</纵轴数值>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<是否评估>" + pt.isAssess + "</是否评估>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(2) + "</重心坐标>");
                }

                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "</重心坐标列表>");
                lstContent.Add("</重心数据>");
            }

            XCommon.CommonFunction.mWriteListStringToFile(strfilename, lstContent);
            return true;
        }

        public static CorePointExt corePointDataToCorePoinExt(CorePointData cpd)
        {
            CorePointExt cpe = new CorePointExt();
            var cpdType = typeof(CorePointData);
            foreach (var propertie in cpdType.GetProperties())
            {
                propertie.SetValue(cpe, propertie.GetValue(cpd, null), null);
            }

            return cpe;
        }

        /// <summary>
        /// 导出重量评估数据列表到工程中心
        /// </summary>
        /// <param name="carList"></param>
        /// <returns></returns>
        public string exportWeightAssessXmlString(List<WeightAssessResult> carList)
        {
            string s = "<重量评估结果列表>";
            foreach (WeightAssessResult car in carList)
            {
                s += exportReflectionObject(car);
            }
            return s + "</重量评估结果列表>";
        }

        /// <summary>
        /// 导出重心包线评估数据列表到工程中心
        /// </summary>
        /// <param name="carList"></param>
        /// <returns></returns>
        public string exportCoreAssessXmlString(List<CoreAssessResult> carList)
        {
            string s = "<重心包线评估结果列表>";
            foreach (CoreAssessResult car in carList)
            {
                s += exportReflectionObject(car);
            }
            return s + "</重心包线评估结果列表>";
        }

        public string exportReflectionObject(Object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] peroperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            string s = "";
            foreach (PropertyInfo property in peroperties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    if (typeof(ICollection).IsAssignableFrom(property.PropertyType))
                    {
                        ICollection list = (ICollection)property.GetValue(obj, null);
                        s += "<" + property.Name + ">";
                        foreach (object o in list)
                        {
                            s += exportReflectionObject(o);
                        }
                        s += "</" + property.Name + ">";
                    }
                    else
                    {
                        s += "<" + property.Name + ">" + property.GetValue(obj, null) + "</" + property.Name + ">";
                    }
                }
            }
            return "<" + type.Name + ">" + s + "</" + type.Name + ">";
        }

        /// <summary>
        /// 导入工程中重量评估列表
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public List<WeightAssessResult> importWeightAssessXMLString(XmlDocument doc)
        {
            XmlNode rootNode = doc.SelectSingleNode("PRJ/重量重心设计工程/重量评估结果列表");
            if (rootNode == null)
            {
                return null;
            }
            List<WeightAssessResult> list = new List<WeightAssessResult>();
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                list.Add((WeightAssessResult)importReflectionObject(node, typeof(WeightAssessResult)));
            }
            return list;
        }

        /// <summary>
        /// 导入工程中重心包线评估列表
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public List<CoreAssessResult> importCoreAssessXMLString(XmlDocument doc)
        {
            XmlNode rootNode = doc.SelectSingleNode("PRJ/重量重心设计工程/重心包线评估结果列表");
            if (rootNode == null)
            {
                return null;
            }
            List<CoreAssessResult> list = new List<CoreAssessResult>();
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                list.Add((CoreAssessResult)importReflectionObject(node, typeof(CoreAssessResult)));
            }
            return list;
        }

        private object importReflectionObject(XmlNode node, Type type)
        {
            object obj = Activator.CreateInstance(type);//通过反射创建对象实例
            foreach (XmlNode n in node.ChildNodes)
            {
                PropertyInfo[] peroperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo property in peroperties)
                {

                    if (property.CanWrite && property.Name == n.Name)
                    {
                        var v = new object();
                        if (typeof(ICollection).IsAssignableFrom(property.PropertyType))
                        {
                            Type[] t1 = property.PropertyType.GetGenericArguments();
                            Type subType = t1[0];
                            var listType = typeof(List<>).MakeGenericType(subType);
                            var list = Activator.CreateInstance(listType);
                            var addMethod = listType.GetMethod("Add");
                            foreach (XmlNode n1 in n.ChildNodes)
                            {
                                addMethod.Invoke(list, new object[] { importReflectionObject(n1, subType) });
                            }
                            v = list;
                        }
                        else
                        {

                            if (property.PropertyType == typeof(Boolean))
                            {
                                v = Convert.ToBoolean(String.Compare(n.InnerText, "true", true) == 0 ? true : false);
                            }
                            else if (property.PropertyType == typeof(Double))
                            {
                                v = Convert.ToDouble(n.InnerText);
                            }
                            else if (property.PropertyType == typeof(Int32))
                            {
                                v = Convert.ToInt32(n.InnerText);
                            }
                            else
                            {
                                v = n.InnerText;
                            }
                        }
                        type.GetProperty(n.Name).SetValue(obj, v, null);
                        break;
                    }
                }
            }
            return obj;
        }

        public static bool ExportExcelFromPointList(List<CorePointExt> ptlst, string strfilename)
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
                DataCell = DataCell.Next;
                DataCell.Value2 = "是否评估";

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
                        DataCell.Next.Next.Value2 = ptlst[i].isAssess;
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

        public static bool ImportExcelToPointList(List<CorePointExt> ptlst, string strfilename)
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
                        CorePointExt node = new CorePointExt();
                        node.pointName = ParaName;
                        node.pointXValue = (double)DataCell.Value2;
                        node.pointYValue = (double)DataCell.Next.Value2;
                        node.isAssess = Convert.ToBoolean(DataCell.Next.Next.Value2);

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

        /// <summary>
        /// 显示柱状图
        /// </summary>
        /// <param name="chart1"></param>
        /// <param name="result"></param>
        public static void DisplayColumnInPic(System.Windows.Forms.DataVisualization.Charting.Chart chart1, WeightAssessResult result)
        {
            chart1.Titles.Clear();
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Legends.Clear();

            if (result.datumWeightDataList.Count > 0 && result.assessWeightDataList.Count > 0)
            {
                //标题
                Title title1 = new Title();
                title1.Text = "重量对比";
                title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
                chart1.Titles.Add(title1);

                AddChartSeries(chart1, result.datumWeightDataList, "基准重量", Color.Blue);
                AddChartSeries(chart1, result.assessWeightDataList, "评估重量", Color.Red);

                if (chart1.ChartAreas.Count > 0)
                {
                    chart1.ChartAreas[0].AxisX.IsLabelAutoFit = true;
                    chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("微软雅黑", 9);

                    //X坐标显示不全
                    chart1.ChartAreas[0].AxisX.Interval = 1;
                    chart1.ChartAreas[0].AxisX.IntervalOffset = 1;
                    chart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;

                    //放大缩小
                    chart1.ChartAreas[0].CursorX.Interval = 0.001D;
                    chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                    chart1.ChartAreas[0].CursorY.Interval = 0.001D;
                    chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
                }
            }
        }

        private static void AddChartSeries(System.Windows.Forms.DataVisualization.Charting.Chart chart1, List<WeightData> lstWeightData, string strLable, Color color)
        {
            if (lstWeightData != null && lstWeightData.Count > 0)
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
                else
                {
                    series.Name = "Series2";
                    legend.Name = "legend2";
                }
                series.LegendText = strLable;
                legend.Docking = Docking.Top;
                chart1.Legends.Add(legend);

                int count = lstWeightData.Count;

                double yValue = 0;
                foreach (WeightData data in lstWeightData)
                {
                    yValue = Math.Round(data.weightValue, 3);
                    series.Points.AddXY(data.weightName, yValue);
                }

                string strSign = string.Empty;
                if (series.Name == "Series1")
                {
                    strSign = "基准重量-";
                }
                if (series.Name == "Series2")
                {
                    strSign = "评估重量-";
                }

                for (int i = 0; i < lstWeightData.Count; i++)
                {
                    yValue = Math.Round(lstWeightData[i].weightValue, 6);
                    series.Points[i].ToolTip = strSign + lstWeightData[i].weightName + ":" + yValue.ToString();
                }

                series.ChartArea = "ChartArea1";
                chart1.Series.Add(series);

                series.ChartType = SeriesChartType.Column;          //柱形图
                series.Color = color;                              //线条颜色   

                //柱形宽度
                series["PixelPointWidth"] = "30";
                //像素点深度
                series["PixelPointDepth"] = "80";
                //像素点间隙深度
                series["PixelPointGapDepth"] = "10";

            }
        }

        /// <summary>
        /// 绘制折线图
        /// </summary>
        /// <param name="chart1"></param>
        /// <param name="lstWeightData"></param>
        public static void DisplayAssessLinePic(System.Windows.Forms.DataVisualization.Charting.Chart chart1, List<WeightAssessParameter> waList, string strTitle)
        {
            if (waList != null && waList.Count > 0)
            {
                chart1.Titles.Clear();
                chart1.Series.Clear();
                chart1.ChartAreas.Clear();
                chart1.Legends.Clear();

                string[] xValues = new string[waList.Count];
                double[] yValues = new double[waList.Count];

                if (strTitle == "合理性评估")
                {
                    for (int i = 0; i < waList.Count; i++)
                    {
                        xValues[i] = waList[i].weightName;
                        yValues[i] = Math.Round(waList[i].rationalityInflation, 3);
                    }
                }
                else if (strTitle == "先进性评估")
                {
                    for (int i = 0; i < waList.Count; i++)
                    {
                        xValues[i] = waList[i].weightName;
                        yValues[i] = Math.Round(waList[i].advancedInflation, 3);
                    }
                }

                //标题
                Title title1 = new Title();
                title1.Text = strTitle;
                title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
                chart1.Titles.Add(title1);

                //Legend
                System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
                legend1.IsTextAutoFit = true;
                legend1.Name = "Legend1";
                chart1.Legends.Add(legend1);

                //ChartArea
                ChartArea chartArea1 = new ChartArea();
                chartArea1.Name = "ChartArea1";
                chart1.ChartAreas.Add(chartArea1);
                //X坐标显示不全
                chartArea1.AxisX.Interval = 1;
                chartArea1.AxisX.IntervalOffset = 1;
                chartArea1.AxisX.LabelStyle.IsStaggered = false;

                //Series
                Series rationalitySeries = new Series("Series1");
                rationalitySeries.Name = strTitle;
                rationalitySeries.MarkerStyle = MarkerStyle.Circle;

                rationalitySeries.Points.DataBindXY(xValues, yValues);

                for (int i = 0; i < rationalitySeries.Points.Count; i++)
                {
                    rationalitySeries.Points[i].ToolTip = yValues[i].ToString();
                }

                rationalitySeries.Legend = "Legend1";
                rationalitySeries.ChartArea = "ChartArea1";
                rationalitySeries.ChartType = SeriesChartType.Line;
                //rationalitySeries.IsValueShownAsLabel = true;
                chart1.Series.Add(rationalitySeries);
            }
        }

        /// <summary>
        /// 导出重量评估数据到xml
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="lstPara"></param> 
        public void ExportWeightEstimatedToXml(string strFileName, WeightAssessResult result)
        {
            if (result.weightAssessParamList != null && result.weightAssessParamList.Count > 0)
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlTextWriter writeXml = null;
                try
                {
                    writeXml = new XmlTextWriter(strFileName, Encoding.GetEncoding("gb2312"));
                }
                catch
                {
                    MessageBox.Show("创建或写入文件失败！");
                    return;
                }

                writeXml.Formatting = Formatting.Indented;
                writeXml.Indentation = 5;
                writeXml.WriteStartDocument();

                writeXml.WriteStartElement("重量评估");
                writeXml.WriteStartElement("重量评估参数列表");
                foreach (WeightAssessParameter para in result.weightAssessParamList)
                {
                    writeXml.WriteStartElement("重量评估参数");
                    {
                        writeXml.WriteStartElement("重量名称");
                        writeXml.WriteString(para.weightName);
                        writeXml.WriteEndElement();

                        writeXml.WriteStartElement("基准重量");
                        writeXml.WriteString(para.datumWeight.ToString());
                        writeXml.WriteEndElement();

                        writeXml.WriteStartElement("评估重量");
                        writeXml.WriteString(para.assessWeight.ToString());
                        writeXml.WriteEndElement();

                        writeXml.WriteStartElement("最小值");
                        writeXml.WriteString(para.minValue.ToString());
                        writeXml.WriteEndElement();

                        writeXml.WriteStartElement("最大值");
                        writeXml.WriteString(para.maxValue.ToString());
                        writeXml.WriteEndElement();

                        writeXml.WriteStartElement("权重值");
                        writeXml.WriteString(para.weightedValue.ToString());
                        writeXml.WriteEndElement();

                        writeXml.WriteStartElement("合理指标");
                        writeXml.WriteString(para.rationalityInflation.ToString());
                        writeXml.WriteEndElement();

                        writeXml.WriteStartElement("先进指标");
                        writeXml.WriteString(para.advancedInflation.ToString());
                        writeXml.WriteEndElement();
                    }

                    writeXml.WriteEndElement();
                }
                writeXml.WriteEndElement();
                writeXml.WriteEndElement();
                writeXml.WriteEndDocument();
                writeXml.Close();
            }
        }

        /// <summary>
        /// 导出重量评估数据到excle
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="lstPara"></param>
        public void ExportWeightEstimatedToExcle(string strFileName, WeightAssessResult result)
        {
            Excel.Application app = new Excel.ApplicationClass();
            try
            {
                Object missing = System.Reflection.Missing.Value;
                app.Visible = false;

                Excel.Workbook wBook = app.Workbooks.Add(missing);

                Excel.Worksheet wSheet = wBook.Worksheets[1] as Excel.Worksheet;

                Excel.Range DataCell = wSheet.get_Range("A1", "A1");
                DataCell.Value2 = "重量名称";
                DataCell.Next.Value2 = "基准重量";
                DataCell.Next.Next.Value2 = "评估重量";
                DataCell.Next.Next.Next.Value2 = "最小值";
                DataCell.Next.Next.Next.Next.Value2 = "最大值";
                DataCell.Next.Next.Next.Next.Next.Value2 = "权重值";
                DataCell.Next.Next.Next.Next.Next.Next.Value2 = "合理指标";
                DataCell.Next.Next.Next.Next.Next.Next.Next.Value2 = "先进指标";

                List<WeightAssessParameter> lstPara = result.weightAssessParamList;
                for (int i = 0; i < lstPara.Count; ++i)
                {
                    WeightAssessParameter wp = lstPara[i];

                    string cellid = "A" + (i + 2).ToString();
                    DataCell = wSheet.get_Range(cellid, cellid);
                    DataCell.Value2 = wp.weightName;
                    DataCell.Next.Value2 = wp.datumWeight;
                    DataCell.Next.Next.Value2 = wp.assessWeight;
                    DataCell.Next.Next.Next.Value2 = wp.minValue;
                    DataCell.Next.Next.Next.Next.Value2 = wp.maxValue;
                    DataCell.Next.Next.Next.Next.Next.Value2 = wp.weightedValue;
                    DataCell.Next.Next.Next.Next.Next.Next.Value2 = wp.rationalityInflation;
                    DataCell.Next.Next.Next.Next.Next.Next.Next.Value2 = wp.advancedInflation;
                }

                //设置禁止弹出保存和覆盖的询问提示框    
                app.DisplayAlerts = false;
                app.AlertBeforeOverwriting = false;
                //保存工作簿    
                wBook.SaveAs(strFileName, Excel.XlFileFormat.xlWorkbookNormal, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlNoChange, missing, missing, missing, missing, missing);
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

        public static string GetWeightEstimatedDataToString(WeightAssessResult result)
        {
            StringBuilder strBuilder = new StringBuilder();
            if (result.weightAssessParamList != null && result.weightAssessParamList.Count > 0)
            {
                string str = "、";
                strBuilder.Append(result.resultName);
                foreach (WeightAssessParameter para in result.weightAssessParamList)
                {
                    strBuilder.Append("|");
                    strBuilder.Append(para.weightName + str);
                    strBuilder.Append(para.datumWeight.ToString() + str);
                    strBuilder.Append(para.assessWeight.ToString() + str);
                    strBuilder.Append(para.minValue.ToString() + str);
                    strBuilder.Append(para.maxValue.ToString() + str);
                    strBuilder.Append(para.weightedValue.ToString() + str);
                    strBuilder.Append(para.rationalityInflation.ToString() + str);
                    strBuilder.Append(para.advancedInflation.ToString());
                }
            }

            return strBuilder.ToString();
        }

        public void ExportCoreEstimatedToXml(string strFileName, CoreAssessResult result)
        {
            if (result.assessCoreDataList != null && result.assessCoreDataList.Count > 0)
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlTextWriter writeXml = null;
                try
                {
                    writeXml = new XmlTextWriter(strFileName, Encoding.GetEncoding("gb2312"));
                }
                catch
                {
                    MessageBox.Show("创建或写入文件失败！");
                    return;
                }

                writeXml.Formatting = Formatting.Indented;
                writeXml.Indentation = 5;
                writeXml.WriteStartDocument();

                writeXml.WriteStartElement("重心评估");
                writeXml.WriteStartElement("重心评估参数列表");

                foreach (CorePointExt core in result.assessCoreDataList)
                {
                    writeXml.WriteStartElement("重心评估数据");
                    {
                        writeXml.WriteStartElement("重心坐标点名称");
                        writeXml.WriteString(core.pointName);
                        writeXml.WriteEndElement();

                        writeXml.WriteStartElement("横轴单位");
                        writeXml.WriteString("毫米");
                        writeXml.WriteEndElement();

                        writeXml.WriteStartElement("纵轴单位");
                        writeXml.WriteString("千克");
                        writeXml.WriteEndElement();

                        writeXml.WriteStartElement("横轴数值");
                        writeXml.WriteString(core.pointXValue.ToString());
                        writeXml.WriteEndElement();

                        writeXml.WriteStartElement("纵轴数值");
                        writeXml.WriteString(core.pointYValue.ToString());
                        writeXml.WriteEndElement();

                        writeXml.WriteStartElement("是否评估");
                        writeXml.WriteString(core.isAssess.ToString());
                        writeXml.WriteEndElement();

                        writeXml.WriteStartElement("权重");
                        writeXml.WriteString(core.weightedValue.ToString());
                        writeXml.WriteEndElement();

                        writeXml.WriteStartElement("评估结果");
                        writeXml.WriteString(core.assessValue.ToString());
                        writeXml.WriteEndElement();
                    }
                    writeXml.WriteEndElement();
                }

                writeXml.WriteEndElement();
                writeXml.WriteEndElement();
                writeXml.WriteEndDocument();
                writeXml.Close();
            }
        }

        public void ExportCoreEstimatedToExcle(string strFileName, CoreAssessResult result)
        {
            Excel.Application app = new Excel.ApplicationClass();
            try
            {
                Object missing = System.Reflection.Missing.Value;
                app.Visible = false;

                Excel.Workbook wBook = app.Workbooks.Add(missing);

                Excel.Worksheet wSheet = wBook.Worksheets[1] as Excel.Worksheet;

                Excel.Range DataCell = wSheet.get_Range("A1", "A1");
                DataCell.Value2 = "重心坐标点名称";
                DataCell.Next.Value2 = "横轴单位";
                DataCell.Next.Next.Value2 = "纵轴单位";
                DataCell.Next.Next.Next.Value2 = "横轴数值";
                DataCell.Next.Next.Next.Next.Value2 = "纵轴数值";
                DataCell.Next.Next.Next.Next.Next.Value2 = "是否评估";
                DataCell.Next.Next.Next.Next.Next.Next.Value2 = "权重";
                DataCell.Next.Next.Next.Next.Next.Next.Next.Value2 = "评估结果";

                List<CorePointExt> lstCorePoint = result.assessCoreDataList;
                for (int i = 0; i < lstCorePoint.Count; ++i)
                {
                    CorePointExt core = lstCorePoint[i];

                    string cellid = "A" + (i + 2).ToString();
                    DataCell = wSheet.get_Range(cellid, cellid);
                    DataCell.Value2 = core.pointName;
                    DataCell.Next.Value2 = "毫米";
                    DataCell.Next.Next.Value2 = "千克";
                    DataCell.Next.Next.Next.Value2 = core.pointXValue;
                    DataCell.Next.Next.Next.Next.Value2 = core.pointYValue;
                    DataCell.Next.Next.Next.Next.Next.Value2 = core.isAssess;
                    DataCell.Next.Next.Next.Next.Next.Next.Value2 = core.weightedValue;
                    DataCell.Next.Next.Next.Next.Next.Next.Next.Value2 = core.assessValue;
                }

                //设置禁止弹出保存和覆盖的询问提示框    
                app.DisplayAlerts = false;
                app.AlertBeforeOverwriting = false;
                //保存工作簿    
                wBook.SaveAs(strFileName, Excel.XlFileFormat.xlWorkbookNormal, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlNoChange, missing, missing, missing, missing, missing);
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

        public static string GetCoreEstimatedDataToString(CoreAssessResult result)
        {
            StringBuilder strBulider = new StringBuilder();

            if (result.assessCoreDataList != null && result.assessCoreDataList.Count > 0)
            {
                string str = "、";
                foreach (CorePointExt core in result.assessCoreDataList)
                {
                    if (core != result.assessCoreDataList.First())
                    {
                        strBulider.Append("|");
                    }
                    strBulider.Append(core.pointName + ":");
                    strBulider.Append("横坐标(毫米)" + str);
                    strBulider.Append("纵坐标(千克)" + str);
                    strBulider.Append(core.pointXValue.ToString() + str);
                    strBulider.Append(core.pointYValue.ToString() + str);
                    strBulider.Append(core.isAssess.ToString() + str);
                    strBulider.Append(core.weightedValue.ToString() + str);
                    strBulider.Append(core.assessValue.ToString());
                }
            }

            return strBulider.ToString();
        }

        public static void setWDMDBFilePath(string path){
            string appDomainConfigFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            //string strFileName = "WES.exe";
            string name=Process.GetCurrentProcess().MainModule.FileName;
            //AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);//.OpenExeConfiguration(name);//
            config.AppSettings.Settings["WDMFileName"].Value = path;
            config.Save(ConfigurationSaveMode.Modified);// 重新载入配置文件的配置节
            //System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }

        public static string getWDMDBFilePath()
        {
            //System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            //string strFileName = System.AppDomain.CurrentDomain.BaseDirectory + "WES.exe";
            //AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
            string dirPath = Environment.CurrentDirectory;// Application.ExecutablePath;
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);//OpenExeConfiguration(strFileName);
            string filePath=config.AppSettings.Settings["WDMFileName"].Value;
            if (filePath==null||filePath.Equals(""))
            {
                string[] files = Directory.GetFiles(dirPath, "*.wdm");
                filePath = files.Count()!=0?files[0]:"wdm.wdm";
            }
            if (!File.Exists(filePath))
            {
                MessageBox.Show("WDM文件不存在!");
                return "";
            }
            return filePath;
        }
    }
}
