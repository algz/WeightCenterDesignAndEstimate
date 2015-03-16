using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using ZaeroModelSystem;
using System.Windows.Forms;

namespace Model
{
    /// <summary>
    /// 重量分类数据对象
    /// </summary>
    public class WeightSortData
    {
        public WeightSortData Clone()
        {
            WeightSortData sortData = new WeightSortData();
            sortData.sortName = this.sortName;

            sortData.lstWeightData = new List<WeightData>();

            foreach (WeightData data in this.lstWeightData)
            {
                sortData.lstWeightData.Add(data.Clone());
            }

            sortData.strRemark = this.strRemark;

            return sortData;
        }

        /// <summary>
        /// 重量分类名称
        /// </summary>
        public string sortName
        {
            get;
            set;
        }

        private List<WeightData> lstWeight_Data = new List<WeightData>();

        /// <summary>
        /// 重量数据对象
        /// </summary>
        public List<WeightData> lstWeightData
        {
            get
            {
                return lstWeight_Data;
            }
            set
            {
                lstWeight_Data = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string strRemark
        {
            get;
            set;
        }

        static public WeightSortData GetSortData(string strFilePath)
        {
            WeightSortData sortData = null;
            try
            {
                string path = string.Empty;
                XmlNode node = null;

                if (!System.IO.File.Exists(strFilePath))
                {
                    return sortData;
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(strFilePath);

                sortData = new WeightSortData();

                //重量分类名称
                path = "重量分类/重量分类名称";
                node = doc.SelectSingleNode(path);
                sortData.sortName = node.InnerText;

                //重量分类名称备注
                path = "重量分类/重量分类备注";
                node = doc.SelectSingleNode(path);
                sortData.strRemark = node.InnerText;

                //重量数据列表
                path = "重量分类/重量数据列表";
                node = doc.SelectSingleNode(path);

                List<WeightData> lstWeightData = sortData.lstWeightData;
                for (int m = 0; m < node.ChildNodes.Count; m++)
                {
                    WeightData weightData = new WeightData();
                    weightData.nID = int.Parse(node.ChildNodes[m].ChildNodes[0].InnerText);
                    weightData.weightName = node.ChildNodes[m].ChildNodes[1].InnerText;
                    weightData.strRemark = node.ChildNodes[m].ChildNodes[4].InnerText;
                    weightData.nParentID = int.Parse(node.ChildNodes[m].ChildNodes[5].InnerText);

                    lstWeightData.Add(weightData);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return sortData;
        }

        public void GetFinalNodeNameList(int nRootID, ref List<string> namelist, string AddPath)
        {
            IEnumerable<WeightData> selection =
                from wd in lstWeightData
                where wd.nParentID == nRootID
                select wd;
            if (selection.Count() == 0)
            {
                namelist.Add(AddPath);
            }
            else
            {
                string addtemp = AddPath;
                if (AddPath.Length != 0)
                {
                    addtemp += "\\";
                }
                foreach (WeightData wd in selection)
                {
                    string path = addtemp + wd.weightName;
                    GetFinalNodeNameList(wd.nID, ref namelist, path);
                }
            }
        }

        public void GetFinalNodeValueMap(int nRootID, ref Dictionary<string, double> namelist, string AddPath)
        {
            IEnumerable<WeightData> selection =
                from wd in lstWeightData
                where wd.nParentID == nRootID
                select wd;
            if (selection.Count() != 0)
            {
                string addtemp = AddPath;
                if (AddPath.Length != 0)
                {
                    addtemp += "\\";
                }
                foreach (WeightData wd in selection)
                {
                    string path = addtemp + wd.weightName;
                    GetFinalNodeValueMapA(wd, ref namelist, path);
                }
            }
        }

        private void GetFinalNodeValueMapA(WeightData wdRoot, ref Dictionary<string, double> namelist, string AddPath)
        {
            IEnumerable<WeightData> selection =
                from wd in lstWeightData
                where wd.nParentID == wdRoot.nID
                select wd;
            if (selection.Count() == 0)
            {
                namelist.Add(AddPath, wdRoot.weightValue);
            }
            else
            {
                string addtemp = AddPath;
                if (AddPath.Length != 0)
                {
                    addtemp += "\\";
                }
                foreach (WeightData wd in selection)
                {
                    string path = addtemp + wd.weightName;
                    GetFinalNodeValueMapA(wd, ref namelist, path);
                }
            }
        }

        public static WeightSortData clsStringToWeightSortData(string str)
        {
            // 创建临时重量分类数据对象
            WeightSortData tempWeightSortData = null;

            // 创建重量数据列表对象
            List<WeightData> lstWeightData = null;
            //判断是否为空字符串
            if (str == null || str == string.Empty)
            {
                return null;
            }
            // 新重量分类数据
            tempWeightSortData = new WeightSortData();
            lstWeightData = new List<WeightData>();
            string[] tempStrings = str.Split('|');
            foreach (string tempString in tempStrings)
            {
                if (tempString == tempStrings[0])
                {
                    tempWeightSortData.sortName = tempString;
                }
                else
                {
                    // 获取重量数据
                    string[] tempInnerStrings = tempString.Split('、');
                    if (tempInnerStrings.Length > 1)
                    {
                        // 创建重量数据对象
                        WeightData tempWeightData = new WeightData();

                        tempWeightData.weightName = tempInnerStrings[0];
                        tempWeightData.nID = Convert.ToInt32(tempInnerStrings[1]);
                        tempWeightData.weightValue = Convert.ToDouble(tempInnerStrings[3]);
                        tempWeightData.nParentID = Convert.ToInt32(tempInnerStrings[4]);

                        // 添加重量数据至重量数据列表
                        lstWeightData.Add(tempWeightData);
                    }
                }
            }
            // 添加重量数据列表至重量分类
            tempWeightSortData.lstWeightData = lstWeightData;
            return tempWeightSortData;
        }

        /// <summary>
        /// 获取ID节点的父节点
        /// </summary>
        /// <param name="_weightSortData"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static WeightData getParentNode(WeightSortData _weightSortData, int ID)
        {
            // 设置空的重量数据
            WeightData tempWeightData = null;
            // 判断当前的列表中是否存在重量数据，且重量数据个数满足要求
            if (_weightSortData.lstWeightData != null && ID >= _weightSortData.lstWeightData.Count)
            {
                return tempWeightData;
            }
            else
            {
                if (_weightSortData == null || _weightSortData.lstWeightData.Count == 0)
                {
                    // 分类空，列表空，返回空
                    return tempWeightData;
                }
                else
                {
                    // 查找到ID重量数据
                    foreach (WeightData _weightData in _weightSortData.lstWeightData)
                    {
                        if (_weightData.nID == ID && _weightData.nParentID == -1)
                        {
                            // 查找ID重量数据即为重量分类的根，返回空
                            // do nothing
                            //return tempWeightData;
                        }
                        else if (_weightData.nID == ID && _weightData.nParentID != -1)
                        {
                            // 找父节点
                            foreach (WeightData _weightData1 in _weightSortData.lstWeightData)
                            {
                                if (_weightData1.nID == _weightData.nParentID)
                                {
                                    tempWeightData = _weightData1;
                                    break;
                                }
                            }
                        }
                    }
                    return tempWeightData;
                }
            }
        }

        /// <summary>
        /// 判断重量分类是否一致
        /// </summary>
        /// <param name="_tempWeightSortData1st"></param>
        /// <param name="_tempWeightSortData2nd"></param>
        /// <returns></returns>
        public static bool blIsSame(WeightSortData _tempWeightSortData1st, WeightSortData _tempWeightSortData2nd)
        {
            if (_tempWeightSortData1st == null || _tempWeightSortData1st.lstWeightData.Count == 0 || _tempWeightSortData2nd == null || _tempWeightSortData2nd.lstWeightData.Count == 0)
            {
                return false;
            }
            else if (_tempWeightSortData1st.lstWeightData.Count == _tempWeightSortData2nd.lstWeightData.Count)
            {
                int intCounter = 0;
                WeightData tempWeightData1 = new WeightData();

                WeightData tempWeightData2 = new WeightData();

                WeightData tempWeightData = null;

                for (int i = 0; i < _tempWeightSortData1st.lstWeightData.Count; i++)
                {
                    tempWeightData1 = _tempWeightSortData1st.lstWeightData[i];

                    StringBuilder tempPath1 = new StringBuilder();
                    StringBuilder tempPath2 = new StringBuilder();

                    for (int j = 0; j < _tempWeightSortData2nd.lstWeightData.Count; j++)
                    {
                        tempWeightData2 = _tempWeightSortData2nd.lstWeightData[j];

                        if (tempWeightData1.weightName == tempWeightData2.weightName && tempWeightData1.weightUnit == tempWeightData2.weightUnit)
                        {
                            tempPath1.Append("\\" + tempWeightData1.weightName);
                            // 获取根
                            bool triger = true;
                            int intInner = tempWeightData1.nID;
                            while (triger)
                            {
                                tempWeightData = getParentNode(_tempWeightSortData1st, intInner);
                                if (tempWeightData != null && tempWeightData.nParentID != -1)
                                {
                                    intInner = tempWeightData.nID;
                                    tempPath1.Insert(0, "\\" + tempWeightData.weightName);
                                }
                                else if (tempWeightData != null && tempWeightData.nParentID == -1)
                                {
                                    tempPath1.Insert(0, tempWeightData.weightName);
                                    triger = false;
                                }
                                else
                                {
                                    triger = false;
                                }
                            }
                            // 获取根
                            tempPath2.Append("\\" + tempWeightData2.weightName);

                            triger = true;
                            intInner = tempWeightData2.nID;
                            while (triger)
                            {
                                tempWeightData = getParentNode(_tempWeightSortData2nd, intInner);
                                if (tempWeightData != null && tempWeightData.nParentID != -1)
                                {
                                    intInner = tempWeightData.nID;
                                    tempPath2.Insert(0, "\\" + tempWeightData.weightName);
                                }
                                else if (tempWeightData != null && tempWeightData.nParentID == -1)
                                {
                                    tempPath2.Insert(0, tempWeightData.weightName);
                                    triger = false;
                                }
                                else
                                {
                                    triger = false;
                                }
                            }

                            if (tempPath1.ToString() == tempPath2.ToString())
                            {
                                intCounter = intCounter + 1;
                            }
                        }
                    }
                }

                if (intCounter == _tempWeightSortData1st.lstWeightData.Count)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断重量分类与修正因子是否合适
        /// </summary>
        /// <param name="_weightSortData"></param>
        /// <param name="_lstPara"></param>
        /// <returns></returns>
        public static bool blIsFit(WeightSortData _weightSortData, List<ParaData> _lstPara)
        {
            int intCounter = 0;
            WeightData tempWeightData = null;

            List<WeightData> lstWeightData = null;
            // 判断重量分类是否为空或者无重量数据
            if (_weightSortData == null || _weightSortData.lstWeightData.Count == 0)
            {
                return false;
            }
            // 判断参数列表是否为空或者无数据
            if (_lstPara == null || _lstPara.Count == 0)
            {
                return false;
            }

            // 获取最基层指标
            lstWeightData = GetListWeightData(_weightSortData);
            if (lstWeightData.Count == 0)
            {
                return false;
            }

            // 判断重量分类同修正因子是否合适
            if (lstWeightData.Count == _lstPara.Count)
            {
                for (int i = 0; i < lstWeightData.Count; i = i + 1)
                {
                    for (int j = 0; j < _lstPara.Count; j = j + 1)
                    {
                        // 错误的修正因子名称
                        if (_lstPara[j].paraName.Length < 4)
                        {
                            break;
                        }
                        if (lstWeightData[i].weightName == _lstPara[j].paraName.Substring(5, _lstPara[j].paraName.Length - 5))
                        {
                            StringBuilder tempPath = new StringBuilder();
                            tempPath.Append("\\" + lstWeightData[i].weightName);

                            // 获取根
                            bool triger = true;
                            int intInner = lstWeightData[i].nID;
                            while (triger)
                            {
                                tempWeightData = getParentNode(_weightSortData, intInner);
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
                            // 根路径和参数备注对比
                            if (tempPath.ToString() == _lstPara[j].strRemark)
                            {
                                intCounter = intCounter + 1;
                            }
                        }
                    }

                }
                if (intCounter == _lstPara.Count)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 求解调整后的重量分类
        /// </summary>
        /// <param name="_weightSortData"></param>
        /// <param name="_lstPara"></param>
        /// <returns></returns>
        public static WeightSortData getWeightModified(WeightSortData _weightSortData, List<ParaData> _lstPara)
        {
            WeightSortData tempWeightSortData = null;
            WeightData tempWeightData = null;
            string tempPath = string.Empty;

            // 判断当前的重量分类和修正因子是否合适
            if (blIsFit(_weightSortData, _lstPara))
            {
                for (int i = 0; i < _weightSortData.lstWeightData.Count; i = i + 1)
                {
                    for (int j = 0; j < _lstPara.Count; j = j + 1)
                    {
                        // 错误的修正因子名称
                        if (_lstPara[j].paraName.Length < 4)
                        {
                            break;
                        }
                        if (_weightSortData.lstWeightData[i].weightName == _lstPara[j].paraName.Substring(4, _lstPara[j].paraName.Length - 4))
                        {
                            tempPath = string.Empty;
                            tempPath = _weightSortData.lstWeightData[i].weightName + "\\";
                            // 获取根
                            bool triger = true;
                            int intInner = i;
                            while (triger)
                            {
                                tempWeightData = getParentNode(_weightSortData, intInner);
                                if (tempWeightData != null && tempWeightData.nParentID != -1)
                                {
                                    tempPath = tempPath + tempWeightData.weightName + "\\";
                                    intInner = tempWeightData.nParentID;
                                }
                                else
                                {
                                    triger = false;
                                }
                            }
                            // 根路径和参数备注对比
                            if (tempPath == _lstPara[j].strRemark)
                            {
                                _weightSortData.lstWeightData[i].weightValue = _weightSortData.lstWeightData[i].weightValue * _lstPara[j].paraValue;
                            }
                        }
                    }
                }
                return tempWeightSortData;
            }
            else
            {
                return tempWeightSortData;
            }
        }

        /// <summary>
        /// 获取Xml文件的WeightSortData
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
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
                MessageBox.Show("导入文件\"" + strPath + "\"格式错误");
                XCommon.XLog.Write("导入文件\"" + strPath + "\"格式错误");
                return null;
            }
            return sortData;
        }

        /// <summary>
        ///获取xls文件 WeightSortData
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
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
                MessageBox.Show("导入文件\"" + strFilePath + "\"格式错误");
                XCommon.XLog.Write("导入文件\"" + strFilePath + "\"格式错误");
                return null;
            }
            return sortData;
        }

        /// <summary>
        /// 判断是否为最后一个节点的重量数据
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="weightSortData"></param>
        /// <returns></returns>
        private static bool IsLastWeightNode(WeightData weight, WeightSortData weightSortData)
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
        public static List<WeightData> GetListWeightData(WeightSortData sortData)
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
        /// 求重量分类的和
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sortData"></param>
        /// <returns></returns>
        public static void GetSortDataTotal(WeightData data, WeightSortData sortData)
        {
            IEnumerable<WeightData> selection = from wd in sortData.lstWeightData where wd.nParentID == data.nID select wd;

            if (selection.Count() > 0)
            {
                foreach (WeightData weight in selection)
                {
                    GetSortDataTotal(weight, sortData);
                }
            }
            else
            {
                IEnumerable<WeightData> parentSelection = from wd in sortData.lstWeightData where wd.nID == data.nParentID select wd;
                IEnumerable<WeightData> childSelection = from wd in sortData.lstWeightData where wd.nParentID == data.nParentID select wd;

                double childValue = 0;
                foreach (WeightData weight in childSelection)
                {
                    childValue += weight.weightValue;
                }

                if (parentSelection.Count() > 0)
                {
                    foreach (WeightData weight in sortData.lstWeightData)
                    {
                        if (weight.nID == parentSelection.ToList()[0].nID)
                        {
                            weight.weightValue = childValue;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 求解修正因子
        /// </summary>
        /// <param name="strOprType"></param>
        /// <returns></returns>
        public static List<ParaData> GetlstCalculateRatio(string strOprType, WeightSortData tempWeightSortData1st, WeightSortData tempWeightSortData2nd)
        {
            List<ParaData> lstTempPara = null;

            WeightData tempWeightData1 = null;
            WeightData tempWeightData2 = null;
            List<WeightData> lstWeightData1 = null;
            List<WeightData> lstWeightData2 = null;


            WeightData tempWeightData = null;

            // 检查操作类型
            if (strOprType != "技术因子" && strOprType != "校核因子")
            {
                return lstTempPara;
            }

            // 检查重量分类
            if (tempWeightSortData1st == null || tempWeightSortData1st.lstWeightData.Count == 0)
            {
                return lstTempPara;
            }
            if (tempWeightSortData2nd == null || tempWeightSortData2nd.lstWeightData.Count == 0)
            {
                return lstTempPara;
            }
            // 检查重量分类是否一致
            if (WeightSortData.blIsSame(tempWeightSortData1st, tempWeightSortData2nd))
            {
                lstTempPara = new List<ParaData>();
                // 计算修正因子
                lstWeightData1 = GetListWeightData(tempWeightSortData1st);
                lstWeightData2 = GetListWeightData(tempWeightSortData2nd);

                for (int i = 0; i < lstWeightData1.Count; i = i + 1)
                {
                    StringBuilder tempPath1 = new StringBuilder();
                    StringBuilder tempPath2 = new StringBuilder();

                    tempWeightData1 = lstWeightData1[i];
                    for (int j = 0; j < lstWeightData2.Count; j = j + 1)
                    {
                        tempWeightData2 = lstWeightData2[j];
                        if (tempWeightData1.weightName == tempWeightData2.weightName && tempWeightData1.weightUnit == tempWeightData2.weightUnit)
                        {
                            tempPath1.Append("\\" + tempWeightData1.weightName);

                            // 获取根
                            bool triger = true;
                            int intInner = tempWeightData1.nID;
                            while (triger)
                            {
                                tempWeightData = WeightSortData.getParentNode(tempWeightSortData1st, intInner);
                                if (tempWeightData != null && tempWeightData.nParentID != -1)
                                {
                                    intInner = tempWeightData.nID;
                                    tempPath1.Insert(0, "\\" + tempWeightData.weightName);
                                }
                                else if (tempWeightData != null && tempWeightData.nParentID == -1)
                                {
                                    tempPath1.Insert(0, tempWeightData.weightName);
                                    triger = false;
                                }
                                else
                                {
                                    triger = false;
                                }
                            }
                            // 获取根
                            tempPath2.Append("\\" + tempWeightData2.weightName);

                            triger = true;
                            intInner = tempWeightData2.nID;
                            while (triger)
                            {
                                tempWeightData = WeightSortData.getParentNode(tempWeightSortData2nd, intInner);
                                if (tempWeightData != null && tempWeightData.nParentID != -1)
                                {
                                    intInner = tempWeightData.nID;
                                    tempPath2.Insert(0, "\\" + tempWeightData.weightName);
                                }
                                else if (tempWeightData != null && tempWeightData.nParentID == -1)
                                {
                                    tempPath2.Insert(0, tempWeightData.weightName);
                                    triger = false;
                                }
                                else
                                {
                                    triger = false;
                                }
                            }
                            if (tempPath1.ToString() == tempPath2.ToString())
                            {
                                ParaData tempParaData = new ParaData();
                                if (strOprType == "技术因子")
                                {
                                    tempParaData.paraName = "技术因子-" + tempWeightData1.weightName;
                                }
                                else if (strOprType == "校核因子")
                                {
                                    tempParaData.paraName = "校核因子-" + tempWeightData1.weightName;
                                }
                                tempParaData.paraUnit = "无量纲";
                                tempParaData.strRemark = tempPath1.ToString();
                                if (tempWeightData1.weightValue == 0)
                                {
                                    tempParaData.paraValue = 0.0;
                                }
                                else if (tempWeightData2.weightValue == 0)
                                {
                                    tempParaData.paraValue = -1.0;
                                }
                                else
                                {
                                    tempParaData.paraValue = tempWeightData1.weightValue / tempWeightData2.weightValue;
                                }
                                lstTempPara.Add(tempParaData);
                            }
                        }
                    }
                }
                return lstTempPara;
            }
            else
            {
                return lstTempPara;
            }

        }

        /// <summary>
        /// 获取重量数据节点路径
        /// </summary>
        /// <param name="sortData"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetNodePath(WeightSortData sortData, WeightData data)
        {
            StringBuilder strNodePath = new StringBuilder();
            WeightData tempWeightData = null;

            if (sortData != null && sortData.lstWeight_Data.Count > 0)
            {
                foreach (WeightData _WeightData in sortData.lstWeight_Data)
                {
                    if (data.nID == _WeightData.nID && data.weightName == _WeightData.weightName && data.nParentID == _WeightData.nParentID)
                    {
                        strNodePath.Append("\\" + data.weightName);

                        // 获取根
                        bool triger = true;
                        int intInner = data.nID;
                        while (triger)
                        {
                            tempWeightData = getParentNode(sortData, intInner);
                            if (tempWeightData != null && tempWeightData.nParentID != -1)
                            {
                                intInner = tempWeightData.nID;
                                strNodePath.Insert(0, "\\" + tempWeightData.weightName);
                            }
                            else if (tempWeightData != null && tempWeightData.nParentID == -1)
                            {
                                strNodePath.Insert(0, tempWeightData.weightName);
                                triger = false;
                            }
                            else
                            {
                                triger = false;
                            }
                        }
                        break;
                    }
                }
            }

            return strNodePath.ToString();
        }
    }
}
