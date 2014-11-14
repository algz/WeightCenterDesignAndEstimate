using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Xml;
using Model.assessData;
using System.Collections;
using Dev.PubLib;

namespace WeightCenterDesignAndEstimateSoft
{
    /// <summary>
    /// Sysware集成平台结构化参数模块
    /// </summary>
    class SyswareDataObjectUtil
    {
        public delegate string DataObjectToXmlDelegate<T>(T o, bool bl);

        #region 公共方法
        
        /// <summary>
        /// 结构参数对象转xml文件公共方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="title"></param>
        /// <param name="dataObjectMethod"></param>
        /// <returns></returns>
        public static string DataObjectToXml<T>(T result, string title, DataObjectToXmlDelegate<T> dataObjectMethod)
        {
            if (PubSyswareCom.IsRuntimeServerStarted())
            {
                if (result != null)
                {
                    object obj = null;
                    string path = null;
                    PubSyswareCom.GetParameterNames(null, ref obj);
                    if (obj == null)
                    {
                        return "TDE/IDE 参数表没打开";
                    }
                    object[] objs = obj as object[];
                    bool bl = false;
                    foreach (object o in objs)
                    {
                        if (o.ToString() == title)
                        {
                            bl = true;
                            break;
                        }
                    }
                    path = dataObjectMethod(result, bl);
                    if (path == null)
                    {
                        return "SyswareDataObject.xml 生成失败";
                    }
                    bl = bl ? (PubSyswareCom.AddChildDataObject(title, path)) : (PubSyswareCom.AddDataObject(path));
                    if (!bl)
                    {
                        return "结构化参数表同步失败";
                    }
                    return "";
                }
                else
                {
                    return "评估软件数据集为空";
                }
            }
            else
            {
                return "TDE/IDE 没有启动成功";
            }
        }

        /// <summary>
        /// 保存SyswareDataObject对象到XML文件,由XX业务对象保存XML时调用
        /// </summary>
        /// <param name="myXmlDoc">可选</param>
        /// <param name="rootElement">可选</param>
        /// <param name="sdo">必选</param>
        private static XmlDocument saveSyswareDataObjectToXML(XmlDocument myXmlDoc, XmlElement rootElement, SyswareDataObject sdo)
        {
            if (sdo == null)
            {
                return null;
            }
            if (myXmlDoc == null || rootElement == null)
            {
                myXmlDoc = new XmlDocument();
                rootElement = myXmlDoc.CreateElement("DataObjectSet");
                myXmlDoc.AppendChild(rootElement);

                //初始化第一层的第一个子节点
                XmlElement firstLevelElement = myXmlDoc.CreateElement("DataStruct");
                rootElement.AppendChild(firstLevelElement);

                XmlElement element = myXmlDoc.CreateElement("id");
                firstLevelElement.AppendChild(element);
                element.InnerText = sdo.id; //名称

                element = myXmlDoc.CreateElement("name");
                firstLevelElement.AppendChild(element);
                element.InnerText = sdo.name; //名称

                element = myXmlDoc.CreateElement("value");
                firstLevelElement.AppendChild(element);
                element.InnerText = sdo.value; //值

                element = myXmlDoc.CreateElement("unit");
                firstLevelElement.AppendChild(element);
                element.InnerText = sdo.unit; //单位

                element = myXmlDoc.CreateElement("remark");
                firstLevelElement.AppendChild(element);
                element.InnerText = sdo.remark; //备注

                if (sdo.children.Count > 0)
                {
                    element = myXmlDoc.CreateElement("Children");
                    firstLevelElement.AppendChild(element);
                    saveSyswareDataObjectToXML(myXmlDoc, element, sdo);
                }
            }
            else
            {
                foreach (SyswareDataObject dataObject in sdo.children)
                {
                    //初始化第一层的第一个子节点
                    XmlElement firstLevelElement = myXmlDoc.CreateElement("DataStruct");
                    rootElement.AppendChild(firstLevelElement);

                    XmlElement element = myXmlDoc.CreateElement("id");
                    firstLevelElement.AppendChild(element);

                    element = myXmlDoc.CreateElement("name");
                    firstLevelElement.AppendChild(element);
                    element.InnerText = dataObject.name; //名称

                    element = myXmlDoc.CreateElement("value");
                    firstLevelElement.AppendChild(element);
                    element.InnerText = dataObject.value; //值

                    element = myXmlDoc.CreateElement("unit");
                    firstLevelElement.AppendChild(element);
                    element.InnerText = dataObject.unit; //单位

                    element = myXmlDoc.CreateElement("remark");
                    firstLevelElement.AppendChild(element);
                    element.InnerText = dataObject.remark; //备注

                    if (dataObject.children != null && dataObject.children.Count > 0)
                    {
                        element = myXmlDoc.CreateElement("Children");
                        firstLevelElement.AppendChild(element);
                        saveSyswareDataObjectToXML(myXmlDoc, element, dataObject);
                    }
                }
            }
            return myXmlDoc;

        }

        #endregion

        #region 保存XX业务对象为XML文件


        /// <summary>
        /// 重心包线评估结果到SyswareDataObject.xml
        /// </summary>
        /// <param name="result"></param>
        /// <param name="isAddChildren">是否增加子结点</param>
        /// <returns></returns>
        public static string saveCoreEnvelopeAssessDataObjectToXml(CoreAssessResult result, bool isAddChildren)
        {
            string path = null;
            try
            {
                SyswareDataObject sdo = new SyswareDataObject();
                SyswareDataObject subSdo = new SyswareDataObject();
                subSdo.id = result.resultID;
                subSdo.name = result.resultName;

                SyswareDataObject cSdo = new SyswareDataObject();
                cSdo.name = "基准重心包线数据";
                cSdo.children = transFormCoreEnvelopeCutToDataObject(result.datumCoreDataList);
                subSdo.children.Add(cSdo);

                cSdo = new SyswareDataObject();
                cSdo.name = "重心包线评估数据";
                cSdo.children = transFormCoreEnvelopeCutToDataObject(result.assessCoreDataList);
                subSdo.children.Add(cSdo);


                cSdo = new SyswareDataObject();
                cSdo.name = "合理性评估数据";
                cSdo.value = Math.Round(result.evaluationResult, 6).ToString();
                foreach (CorePointExt cpdEx in result.assessCoreDataList)
                {

                    SyswareDataObject nSdo = new SyswareDataObject();
                    nSdo.name = cpdEx.pointName;

                    SyswareDataObject aSdo = new SyswareDataObject();
                    aSdo.name = "评估结果";
                    aSdo.value = Math.Round(Convert.ToDouble(cpdEx.assessValue), 6).ToString();
                    //aSdo.unit = "Kilogram";
                    nSdo.children.Add(aSdo);

                    SyswareDataObject wSdo = new SyswareDataObject();
                    wSdo.name = "权重结果";
                    wSdo.value = Math.Round(Convert.ToDouble(cpdEx.weightedValue), 6).ToString();
                    //aSdo.unit = "Kilogram";
                    nSdo.children.Add(wSdo);

                    cSdo.children.Add(nSdo);
                }
                subSdo.children.Add(cSdo);


                if (isAddChildren)
                {
                    sdo = subSdo;
                }
                else
                {
                    sdo.name = "重心包线评估结果";
                    sdo.children.Add(subSdo);
                }


                XmlDocument myXmlDoc = saveSyswareDataObjectToXML(null, null, sdo);
                path = System.IO.Path.GetTempPath() + System.IO.Path.GetRandomFileName() + ".xml";
                //将xml文件保存到临时路径下
                myXmlDoc.Save(path);
            }
            catch 
            {
                //XLog.Write("无法保存重心包线评估XML." + e.Message);
                return null;
            }

            return path;
        }

        /// <summary>
        /// 导出重心包线剪裁结果到SyswareDataObject.xml
        /// </summary>
        /// <param name="coreCut"></param>
        /// <param name="isAddChildren"></param>
        /// <returns></returns>
        public static string saveCoreEnvelopeCutDataObjectToXml(CoreEnvelopeCutResultData coreCut, bool isAddChildren)
        {
            string path = null;
            try
            {
                SyswareDataObject sdo = new SyswareDataObject();
                SyswareDataObject subSdo = new SyswareDataObject();
                subSdo.name = coreCut.cutResultName;

                SyswareDataObject cSdo = new SyswareDataObject();
                cSdo.name = "基础重心包线数据";
                cSdo.children = transFormCoreEnvelopeCutToDataObject(coreCut.lstBasicCoreEnvelope);
                subSdo.children.Add(cSdo);

                cSdo = new SyswareDataObject();
                cSdo.name = "重心包线剪裁数据";
                cSdo.children = transFormCoreEnvelopeCutToDataObject(coreCut.lstCutEnvelopeCore);
                subSdo.children.Add(cSdo);


                if (isAddChildren)
                {
                    sdo = subSdo;
                }
                else
                {
                    sdo.name = "重心包线剪裁结果";
                    sdo.children.Add(subSdo);
                }


                XmlDocument myXmlDoc = saveSyswareDataObjectToXML(null, null, sdo);
                path = System.IO.Path.GetTempPath() + System.IO.Path.GetRandomFileName() + ".xml";
                //将xml文件保存到临时路径下
                myXmlDoc.Save(path);
            }
            catch 
            {
                //XLog.Write("无法保存重量设计XML." + e.Message);
                return null;
            }

            return path;
        }

        

        /// <summary>
        /// 导出重心包线设计结果到SyswareDataObject.xml
        /// </summary>
        /// <param name="coreEnvelope"></param>
        /// <param name="isAddChildren"></param>
        /// <returns></returns>
        public static string saveCoreEnvelopeDesignDataObjectToXml(CoreEnvelopeArithmetic coreEnvelope, bool isAddChildren)
        {
            string path = null;
            try
            {

                SyswareDataObject sdo = new SyswareDataObject();
                SyswareDataObject subSdo = new SyswareDataObject();

                foreach (NodeFormula n in coreEnvelope.FormulaList)
                {
                    SyswareDataObject nSdo = new SyswareDataObject();
                    nSdo.name = n.NodeName;
                    SyswareDataObject xSdo = new SyswareDataObject();
                    xSdo.name = n.XFormula.NodePath;
                    xSdo.value = Math.Round(Convert.ToDouble(n.XFormula.Value), 6).ToString();
                    xSdo.unit = "Millimeter";
                    nSdo.children.Add(xSdo);
                    SyswareDataObject ySdo = new SyswareDataObject();
                    ySdo.name = n.YFormula.NodePath;
                    ySdo.value = Math.Round(Convert.ToDouble(n.YFormula.Value), 6).ToString();
                    ySdo.unit = "Kilogram";

                    nSdo.children.Add(ySdo);

                    subSdo.children.Add(nSdo);
                }

                if (isAddChildren)
                {
                    subSdo.name = coreEnvelope.DataName;
                    sdo = subSdo;
                }
                else
                {
                    sdo.name = "重心包线设计结果";
                    subSdo.name = coreEnvelope.DataName;
                    sdo.children.Add(subSdo);
                }


                XmlDocument myXmlDoc = saveSyswareDataObjectToXML(null, null, sdo);
                path = System.IO.Path.GetTempPath() + System.IO.Path.GetRandomFileName() + ".xml";
                //将xml文件保存到临时路径下
                myXmlDoc.Save(path);
            }
            catch
            {
                //XLog.Write("无法保存重量设计XML." + e.Message);
                return null;
            }

            return path;
        }

        /// <summary>
        /// 导出重量调整结果为对应的SyswareDataObject.xml
        /// </summary>
        /// <param name="result"></param>
        /// <param name="isAddChildren"></param>
        /// <returns></returns>
        public static string saveWeightAssessDataObjectToXml(WeightAssessResult result, bool isAddChildren)
        {
            string path = null;
            try
            {

                SyswareDataObject sdo = new SyswareDataObject();
                SyswareDataObject subSdo = new SyswareDataObject();
                subSdo.id = result.resultID;
                subSdo.name = result.resultName;
                //subSdo.children = new List<SyswareDataObject>();

                //基准重量
                SyswareDataObject basicSdo = new SyswareDataObject();
                basicSdo.name = "基准重量";
                basicSdo.value = Math.Round(result.datumWeightTotal, 6).ToString();
                basicSdo.unit = "Kilogram";
                basicSdo.children = transFormWeightArithmeticToDataObject(-1, result.datumWeightDataList);
                subSdo.children.Add(basicSdo);

                //评估重量
                SyswareDataObject assessSdo = new SyswareDataObject();
                assessSdo.name = "评估重量";
                assessSdo.value = Math.Round(result.assessWeightTotal, 6).ToString();
                assessSdo.unit = "Kilogram";
                assessSdo.children = transFormWeightArithmeticToDataObject(-1, result.assessWeightDataList);
                subSdo.children.Add(assessSdo);

                //先进性评估
                SyswareDataObject advancedSdo = new SyswareDataObject();
                advancedSdo.name = "先进性评估";
                advancedSdo.value = result.advancedInflationTotal.ToString();
                advancedSdo.children = transFormWeightAssessToDataObject(result);
                subSdo.children.Add(advancedSdo);

                //合理性评估
                SyswareDataObject rationalitySdo = new SyswareDataObject();
                rationalitySdo.name = "合理性评估";
                rationalitySdo.value = result.rationalityInflationTotal.ToString();
                rationalitySdo.children = transFormWeightAssessToDataObject(result);
                subSdo.children.Add(rationalitySdo);

                if (isAddChildren)
                {
                    sdo = subSdo;
                }
                else
                {
                    sdo.name = "重量评估结果";
                    sdo.children.Add(subSdo);
                }

                XmlDocument myXmlDoc = saveSyswareDataObjectToXML(null, null, sdo);
                path = System.IO.Path.GetTempPath() + System.IO.Path.GetRandomFileName() + ".xml";
                //将xml文件保存到临时路径下
                myXmlDoc.Save(path);
            }
            catch 
            {
                //XLog.Write("无法保存重量评估XML." + e.Message);
                return null;
            }

            return path;
        }

 


        /// <summary>
        /// 导出重量调整结果为对应的SyswareDataObject.xml
        /// </summary>
        /// <param name="result"></param>
        /// <param name="isAddChildren"></param>
        /// <returns></returns>
        public static string saveWeightAdjustDataObjectToXml(WeightAdjustmentResultData adjustResult, bool isAddChildren)
        {
            string path = null;
            try
            {

                SyswareDataObject sdo = new SyswareDataObject();
                SyswareDataObject subSdo = new SyswareDataObject();
                if (isAddChildren)
                {
                    subSdo.name = adjustResult.WeightAdjustName;
                    sdo = subSdo;
                }
                else
                {
                    sdo.name = "重量调整结果";
                    subSdo.name = adjustResult.WeightAdjustName;
                    sdo.children.Add(subSdo);
                }

                //基础重量
                SyswareDataObject basicSdo = new SyswareDataObject();
                basicSdo.name = "基础重量";
                basicSdo.children = transFormWeightArithmeticToDataObject(-1, adjustResult.basicWeightData.lstWeightData);
                subSdo.children.Add(basicSdo);

                //调整重量
                SyswareDataObject adjustSdo = new SyswareDataObject();
                adjustSdo.name = "调整重量";
                adjustSdo.children = transFormWeightArithmeticToDataObject(-1, adjustResult.weightAdjustData.lstWeightData);
                subSdo.children.Add(adjustSdo);

                XmlDocument myXmlDoc = saveSyswareDataObjectToXML(null, null, sdo);
                path = System.IO.Path.GetTempPath() + System.IO.Path.GetRandomFileName() + ".xml";
                //将xml文件保存到临时路径下
                myXmlDoc.Save(path);
            }
            catch
            {
                //XLog.Write("无法保存重量设计XML." + e.Message);
                return null;
            }

            return path;
        }

        /// <summary>
        /// 导出重量设计结果为对应的SyswareDataObject.xml
        /// </summary>
        /// <param name="result"></param>
        /// <param name="isAddChildren"></param>
        /// <returns></returns>
        public static string saveWeightArithmeticDataObjectToXml(WeightArithmetic result, bool isAddChildren)
        {
            string path = null;
            try
            {

                SyswareDataObject sdo = new SyswareDataObject();
                if (isAddChildren)
                {
                    SyswareDataObject subSdo = new SyswareDataObject();
                    sdo.name = result.DataName;
                    sdo.children.Add(transFormWeightArithmeticToDataObject(-1, result.ExportDataToWeightSort().lstWeightData)[0]);
                }
                else
                {
                    sdo.name = "重量设计结果";
                    SyswareDataObject subSdo = new SyswareDataObject();
                    subSdo.name = result.DataName;
                    subSdo.children = transFormWeightArithmeticToDataObject(-1, result.ExportDataToWeightSort().lstWeightData);
                    sdo.children.Add(subSdo);

                }
                XmlDocument myXmlDoc = saveSyswareDataObjectToXML(null, null, sdo);
                path = System.IO.Path.GetTempPath() + System.IO.Path.GetRandomFileName() + ".xml";
                //将xml文件保存到临时路径下
                myXmlDoc.Save(path);
            }
            catch 
            {
               // XLog.Write("无法保存重量设计XML." + e.Message);
                
                return null;
            }

            return path;
        }

        
        #endregion

        #region 转换XXX业务对象为SyswareDataObejct

        /// <summary>
        /// 转换重心剪裁对象为SyswareDataObejct
        /// </summary>
        /// <param name="cpdList"></param>
        /// <returns></returns>
        private static List<SyswareDataObject> transFormCoreEnvelopeCutToDataObject(IList cpdList)
        {
            List<SyswareDataObject> sdoList = new List<SyswareDataObject>();
            foreach (object o in cpdList)
            {
                CorePointData cpd = o as CorePointData;

                SyswareDataObject nSdo = new SyswareDataObject();
                nSdo.name = cpd.pointName;

                SyswareDataObject xSdo = new SyswareDataObject();
                xSdo.name = "横坐标";
                xSdo.value = Math.Round(Convert.ToDouble(cpd.pointXValue), 6).ToString();
                xSdo.unit = "Millimeter";
                nSdo.children.Add(xSdo);

                SyswareDataObject ySdo = new SyswareDataObject();
                ySdo.name = "纵坐标";
                ySdo.value = Math.Round(Convert.ToDouble(cpd.pointYValue), 6).ToString();
                ySdo.unit = "Kilogram";
                nSdo.children.Add(ySdo);

                sdoList.Add(nSdo);
            }
            return sdoList;
        }

        /// <summary>
        /// 转换重量评估对象为SyswareDataObejct
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static List<SyswareDataObject> transFormWeightAssessToDataObject(WeightAssessResult result)
        {
            List<SyswareDataObject> sdoList = new List<SyswareDataObject>();
            foreach (WeightAssessParameter wap in result.weightAssessParamList)
            {
                SyswareDataObject sdo = new SyswareDataObject();
                sdo.name = wap.weightName;
                sdo.value = wap.advancedInflation.ToString();
                sdo.unit = wap.weightUnit;
                sdoList.Add(sdo);
            }
            return sdoList;
        }
        
        /// <summary>
        /// 转换重量设计/调整对象到SyswareDataObject对象
        /// </summary>
        /// <param name="nParentID">必选</param>
        /// <param name="wdList">必选</param>
        /// <returns></returns>
        private static List<SyswareDataObject> transFormWeightArithmeticToDataObject(int nParentID, List<WeightData> wdList)
        {
            IEnumerable<WeightData> selection = from wd in wdList where wd.nParentID == nParentID select wd;
            if (selection.Count() != 0)
            {

                List<SyswareDataObject> sdoList = new List<SyswareDataObject>();
                foreach (WeightData wd in selection)
                {
                    SyswareDataObject dataObject = new SyswareDataObject();
                    dataObject.id = System.Guid.NewGuid().ToString();
                    dataObject.name = wd.weightName;
                    dataObject.value = Math.Round(Convert.ToDouble(wd.weightValue), 6).ToString();
                    dataObject.unit = "Kilogram";//wd.weightUnit;
                    dataObject.remark = wd.strRemark;

                    IEnumerable<WeightData> wdEnum = from w in wdList where w.nParentID == wd.nID select w;
                    if (wdEnum.Count() > 0)
                    {
                        dataObject.children = transFormWeightArithmeticToDataObject(wd.nID, wdList);
                    }

                    sdoList.Add(dataObject);
                }

                return sdoList;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
