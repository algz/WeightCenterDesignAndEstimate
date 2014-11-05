using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XCommon;
using System.Xml;
using WeightCenterDesignAndEstimateSoft.Task;
using WeightCenterDesignAndEstimateSoft.Tool;
using WeightCenterDesignAndEstimateSoft.Data;
using WeightCenterDesignAndEstimateSoft.Setting;
using Model;
using ZedGraph;
using System.Collections;
using System.IO;
using Dev.PubLib;
using SyswareComLibrary;
using System.Windows.Forms.DataVisualization.Charting;
using Model.assessData;
using WeightCenterDesignAndEstimateSoft.Task.CoreEnvelopeAssessment;
using System.Text.RegularExpressions;


namespace WeightCenterDesignAndEstimateSoft
{
    public partial class MainForm : Form
    {
        #region 属性

        private string strProjectFile = string.Empty;

        public DesignProjectData designProjectData = null;

        //重量设计结果集合
        private List<Model.WeightArithmetic> lstWeightArithmetic = null;
        //重量调整结果集合
        private List<WeightAdjustmentResultData> lstAdjustmentResultData = null;
        //重心包线剪裁结果集合
        private List<CoreEnvelopeCutResultData> lstCutResultData = null;
        //重心包线设计结果集合
        private List<Model.CoreEnvelopeArithmetic> lstCoreEnvelopeDesign = null;
        //重量评估结果对象集合
        public List<WeightAssessResult> weightAssessResultList = new List<WeightAssessResult>();
        //重心包线评估结果对象集合
        public List<CoreAssessResult> coreAssessResultList = new List<CoreAssessResult>();

        public static readonly string strDataBasePath = (System.IO.Directory.GetCurrentDirectory() + @"\DataBase\CoreDesignSoft.db");

        private TreeNode selNode = null;
        private const int digit = 6;
        private const int picDigit = 3;

        public string strWeightDesignIndex = string.Empty;
        public string strCoreEnvelopeDesignIndex = string.Empty;
        public string strCoreEnvelopeCutIndex = string.Empty;

        private static int rightCount = 0;

        public MainForm()
        {
            InitializeComponent();
            XLog.TextBoxLog = txtLog;
            BindProjectTreeData(null);
            fToolStripMenuItemTool.Enabled = false;

            tabControlWork.TabPages.Clear();
        }

        #endregion

        #region 自定义方法

        /// <summary>
        /// 绑定工程树
        /// </summary>
        public void BindProjectTreeData(DesignProjectData ProjectData)
        {
            treeViewData.Nodes.Clear();//清空数据树所有结点(准备重新加载所有新结点)
            selNode = null;
            designProjectData = ProjectData;

            if (ProjectData != null)
            {
                lstWeightArithmetic = ProjectData.lstWeightArithmetic;
                lstAdjustmentResultData = ProjectData.lstAdjustmentResultData;
                lstCoreEnvelopeDesign = ProjectData.lstCoreEnvelopeDesign;
                lstCutResultData = ProjectData.lstCutResultData;
                weightAssessResultList = ProjectData.weightAssessResultList;//重量评估结果列表
                this.coreAssessResultList = ProjectData.CoreAssessResultList;

                TreeNode projectNode = null;
                projectNode = new TreeNode(designProjectData.projectName);
                treeViewData.Nodes.Add(projectNode);

                //重量设计结果列表
                if (lstWeightArithmetic != null && lstWeightArithmetic.Count > 0)
                {
                    TreeNode parentNode = new TreeNode("重量设计结果列表");
                    projectNode.Nodes.Add(parentNode);

                    for (int i = 0; i < lstWeightArithmetic.Count; i++)
                    {
                        TreeNode childNode = new TreeNode();
                        childNode.Name = lstWeightArithmetic[i].Name;
                        childNode.Text = lstWeightArithmetic[i].DataName;
                        childNode.ToolTipText = i.ToString();
                        parentNode.Nodes.Add(childNode);
                    }
                }

                //重量调整结果列表
                if (lstAdjustmentResultData != null && lstAdjustmentResultData.Count > 0)
                {
                    TreeNode parentNode = new TreeNode("重量调整结果列表");
                    projectNode.Nodes.Add(parentNode);

                    for (int i = 0; i < lstAdjustmentResultData.Count; i++)
                    {
                        TreeNode childNode = new TreeNode();
                        childNode.Name = lstAdjustmentResultData[i].WeightAdjustName;
                        childNode.Text = lstAdjustmentResultData[i].WeightAdjustName;
                        childNode.ToolTipText = i.ToString();
                        parentNode.Nodes.Add(childNode);
                    }
                }

                //重量评估结果列表
                if (weightAssessResultList != null && weightAssessResultList.Count > 0)
                {
                    TreeNode parentNode = new TreeNode("重量评估结果列表");
                    projectNode.Nodes.Add(parentNode);

                    for (int i = 0; i < weightAssessResultList.Count; i++)
                    {
                        TreeNode childNode = new TreeNode();
                        childNode.Name = weightAssessResultList[i].resultID;
                        childNode.Text = weightAssessResultList[i].resultName;
                        childNode.Tag = weightAssessResultList[i];
                        childNode.ToolTipText = i.ToString();
                        parentNode.Nodes.Add(childNode);
                    }
                }

                //重心包线设计结果列表
                if (lstCoreEnvelopeDesign != null && lstCoreEnvelopeDesign.Count > 0)
                {
                    TreeNode parentNode = new TreeNode("重心包线设计结果列表");
                    projectNode.Nodes.Add(parentNode);

                    for (int i = 0; i < lstCoreEnvelopeDesign.Count; i++)
                    {
                        TreeNode childNode = new TreeNode();
                        childNode.Name = lstCoreEnvelopeDesign[i].Name;
                        childNode.Text = lstCoreEnvelopeDesign[i].DataName;
                        childNode.ToolTipText = i.ToString();
                        parentNode.Nodes.Add(childNode);
                    }
                }

                //重心包线剪裁结果列表
                if (lstCutResultData != null && lstCutResultData.Count > 0)
                {
                    TreeNode parentNode = new TreeNode("重心包线剪裁结果列表");
                    projectNode.Nodes.Add(parentNode);

                    for (int i = 0; i < lstCutResultData.Count; i++)
                    {
                        TreeNode childNode = new TreeNode();
                        childNode.Name = lstCutResultData[i].cutResultName;
                        childNode.Text = lstCutResultData[i].cutResultName;
                        childNode.ToolTipText = i.ToString();
                        parentNode.Nodes.Add(childNode);
                    }
                }
                //重心包线评估结果列表
                if (this.coreAssessResultList != null && coreAssessResultList.Count > 0)
                {
                    TreeNode parentNode = new TreeNode("重心包线评估结果列表");
                    projectNode.Nodes.Add(parentNode);

                    for (int i = 0; i < coreAssessResultList.Count; i++)
                    {
                        TreeNode childNode = new TreeNode();
                        childNode.Name = coreAssessResultList[i].resultID;
                        childNode.Text = coreAssessResultList[i].resultName;
                        childNode.Tag = coreAssessResultList[i];
                        childNode.ToolTipText = i.ToString();
                        parentNode.Nodes.Add(childNode);
                    }
                }
                projectNode.ExpandAll();
            }
        }

        /// <summary>
        /// 获取工程数据对象
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        private DesignProjectData GetResultData(string strFilePath)
        {
            string path = string.Empty;
            XmlNode node = null;

            DesignProjectData projectData = new DesignProjectData();

            XmlDocument doc = new XmlDocument();
            doc.Load(strFilePath);

            //工程名称
            path = "PRJ/重量重心设计工程/工程名称";
            node = doc.SelectSingleNode(path);
            projectData.projectName = node.InnerText;

            //创建人
            path = "PRJ/重量重心设计工程/创建人";
            node = doc.SelectSingleNode(path);
            projectData.projectCreator = node.InnerText;

            //备注
            path = "PRJ/重量重心设计工程/备注";
            node = doc.SelectSingleNode(path);
            projectData.strRemark = node.InnerText;

            //重量设计结果列表
            List<Model.WeightArithmetic> lstWeightArithmeticData = GetWeightDesignReusltData(doc);
            projectData.lstWeightArithmetic = lstWeightArithmeticData;

            //重量调整结果列表
            List<WeightAdjustmentResultData> lstAdjustData = GetAdjustmentResultData(doc);
            projectData.lstAdjustmentResultData = lstAdjustData;

            //重量评估结果列表
            List<WeightAssessResult> warList = new CommonUtil().importWeightAssessXMLString(doc);
            projectData.weightAssessResultList = warList;

            //重心包线设计结果列表
            List<Model.CoreEnvelopeArithmetic> lstCoreEnvelopeData = GetCoreEnvelopeDesignReustData(doc);
            projectData.lstCoreEnvelopeDesign = lstCoreEnvelopeData;

            //重心包线剪裁结果列表
            List<Model.CoreEnvelopeCutResultData> lstCoreEnvelopteCutData = GetCoreEnvelopteCutData(doc);
            projectData.lstCutResultData = lstCoreEnvelopteCutData;

            //重心包线评估结果列表
            List<CoreAssessResult> carList = new CommonUtil().importCoreAssessXMLString(doc);
            projectData.CoreAssessResultList = carList;

            return projectData;
        }

        /// <summary>
        /// 获取重量设计结果列表
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private List<Model.WeightArithmetic> GetWeightDesignReusltData(XmlDocument doc)
        {
            string path = string.Empty;
            XmlNode node = null;

            //重量设计结果列表
            List<Model.WeightArithmetic> lstWeightArithmeticData = new List<Model.WeightArithmetic>();
            //重量设计结果
            path = "PRJ/重量重心设计工程/重量设计结果列表";
            node = doc.SelectSingleNode(path);

            if (node == null)
            {
                return lstWeightArithmeticData;
            }

            for (int m = 0; m < node.ChildNodes.Count; m++)
            {
                Model.WeightArithmetic arithmetic = new Model.WeightArithmetic();

                //重量设计数据名称
                arithmetic.DataName = node.ChildNodes[m].ChildNodes[0].InnerText;

                //重量算法
                arithmetic.Name = node.ChildNodes[m].ChildNodes[2].ChildNodes[0].InnerText;
                arithmetic.CreateTime = node.ChildNodes[m].ChildNodes[2].ChildNodes[1].InnerText;
                arithmetic.LastModifyTime = node.ChildNodes[m].ChildNodes[2].ChildNodes[2].InnerText;

                arithmetic.SortName = node.ChildNodes[m].ChildNodes[2].ChildNodes[3].ChildNodes[0].InnerText;
                arithmetic.Remark = node.ChildNodes[m].ChildNodes[2].ChildNodes[4].InnerText;

                //lstWeightFormula
                List<Model.WeightFormula> lstWeightFormula = new List<Model.WeightFormula>();

                XmlNodeList lstChildNode = node.ChildNodes[m].ChildNodes[2].ChildNodes[3].ChildNodes[2].ChildNodes;

                Dictionary<WeightParameter, WeightParameter> wpDict = new Dictionary<WeightParameter, WeightParameter>();

                for (int n = 0; n < lstChildNode.Count; n++)
                {
                    Model.WeightFormula formula = new Model.WeightFormula();

                    formula.NodePath = lstChildNode[n].ChildNodes[0].InnerText;
                    formula.Formula = lstChildNode[n].ChildNodes[1].InnerText;

                    List<Model.WeightParameter> lstWeightPara = new List<Model.WeightParameter>();

                    for (int i = 0; i < lstChildNode[n].ChildNodes[2].ChildNodes.Count; i++)
                    {
                        string strName = lstChildNode[n].ChildNodes[2].ChildNodes[i].ChildNodes[0].InnerText;
                        string strUnit = lstChildNode[n].ChildNodes[2].ChildNodes[i].ChildNodes[1].InnerText;

                        WeightParameter wpGlobal = WeightArithmetic.FindGlobleParameters(strName, strUnit)[0];

                        Model.WeightParameter para = null;
                        if (wpGlobal == null)
                        {
                            para = new WeightParameter();

                            para.ParaName = strName;
                            para.ParaUnit = strUnit;
                            para.ParaType = 10;
                            para.ParaValue = Convert.ToDouble(lstChildNode[n].ChildNodes[2].ChildNodes[i].ChildNodes[3].InnerText);
                            para.ParaRemark = lstChildNode[n].ChildNodes[2].ChildNodes[i].ChildNodes[4].InnerText;

                            WeightParameter temp10wp = new WeightParameter(para);
                            WeightParameter.GetWeightParameterList()[10].Add(temp10wp);
                            wpDict.Add(temp10wp, para);
                        }
                        else
                        {
                            if (!wpDict.ContainsKey(wpGlobal))
                            {
                                para = new WeightParameter(wpGlobal);
                                para.ParaValue = Convert.ToDouble(lstChildNode[n].ChildNodes[2].ChildNodes[i].ChildNodes[3].InnerText);
                                wpDict.Add(wpGlobal, para);
                            }
                            else
                            {
                                para = wpDict[wpGlobal];
                            }
                        }

                        formula.ParaList.Add(para);
                    }

                    if (lstChildNode[0].ChildNodes.Count >= 4)
                    {
                        formula.Value = Convert.ToDouble(lstChildNode[n].ChildNodes[3].InnerText);
                    }
                    arithmetic.FormulaList.Add(formula);
                }

                lstWeightArithmeticData.Add(arithmetic);
            }

            return lstWeightArithmeticData;
        }

        /// <summary>
        /// 获取重量调整结果列表
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private List<Model.WeightAdjustmentResultData> GetAdjustmentResultData(XmlDocument doc)
        {
            string path = string.Empty;
            XmlNode node = null;

            //重量设计结果列表
            List<Model.WeightAdjustmentResultData> lstAdjustData = new List<Model.WeightAdjustmentResultData>();
            //重量设计结果
            path = "PRJ/重量重心设计工程/重量调整结果列表";
            node = doc.SelectSingleNode(path);

            if (node == null)
            {
                return lstAdjustData;
            }
            XmlNodeList lstAdjust = node.ChildNodes;
            foreach (XmlNode justNode in lstAdjust)
            {
                WeightAdjustmentResultData adjustData = new WeightAdjustmentResultData();

                //重量设计数据名称
                adjustData.WeightAdjustName = justNode.ChildNodes[0].InnerText;

                //基础重量数据
                WeightSortData basicSortData = new WeightSortData();
                basicSortData.lstWeightData = new List<WeightData>();

                XmlNodeList lstBasicNode = justNode.ChildNodes[1].ChildNodes[0].ChildNodes;
                basicSortData.sortName = lstBasicNode[0].ChildNodes[0].InnerText;
                adjustData.SortName = lstBasicNode[0].ChildNodes[0].InnerText;

                foreach (XmlNode basicNode in lstBasicNode)
                {
                    if (basicNode.ChildNodes != null && basicNode.ChildNodes.Count > 1)
                    {
                        XmlNodeList lstWeightNode = basicNode.ChildNodes;
                        foreach (XmlNode weightNode in lstWeightNode)
                        {
                            WeightData data = new WeightData();
                            data.nID = Convert.ToInt32(weightNode.ChildNodes[0].InnerText);
                            data.weightName = weightNode.ChildNodes[1].InnerText;
                            data.weightValue = Convert.ToDouble(weightNode.ChildNodes[3].InnerText);
                            data.strRemark = weightNode.ChildNodes[4].InnerText;
                            data.nParentID = Convert.ToInt32(weightNode.ChildNodes[5].InnerText);
                            basicSortData.lstWeightData.Add(data);
                        }
                    }

                }
                adjustData.basicWeightData = basicSortData;

                //调整重量数据
                WeightSortData adjustSortData = new WeightSortData();
                adjustSortData.lstWeightData = new List<WeightData>();

                XmlNodeList lstAdjustNode = justNode.ChildNodes[2].ChildNodes[0].ChildNodes;
                adjustSortData.sortName = lstAdjustNode[0].ChildNodes[0].InnerText;

                foreach (XmlNode basicNode in lstAdjustNode)
                {

                    if (basicNode.ChildNodes != null && basicNode.ChildNodes.Count > 1)
                    {
                        XmlNodeList lstWeightNode = basicNode.ChildNodes;
                        foreach (XmlNode weightNode in lstWeightNode)
                        {
                            WeightData data = new WeightData();
                            data.nID = Convert.ToInt32(weightNode.ChildNodes[0].InnerText);
                            data.weightName = weightNode.ChildNodes[1].InnerText;
                            data.weightValue = Convert.ToDouble(weightNode.ChildNodes[3].InnerText);
                            data.strRemark = weightNode.ChildNodes[4].InnerText;
                            data.nParentID = Convert.ToInt32(weightNode.ChildNodes[5].InnerText);
                            adjustSortData.lstWeightData.Add(data);
                        }
                    }

                }
                adjustData.weightAdjustData = adjustSortData;

                //校核因子
                List<ParaData> lstJHRatio = null;
                XmlNodeList lstJHNode = justNode.ChildNodes[3].ChildNodes[0].ChildNodes;
                if (lstJHNode != null && lstJHNode.Count > 0)
                {
                    lstJHRatio = new List<ParaData>();
                    foreach (XmlNode childNode in lstJHNode)
                    {
                        ParaData data = new ParaData();
                        data.paraName = childNode.ChildNodes[0].InnerText;
                        data.paraUnit = childNode.ChildNodes[1].InnerText;
                        data.paraType = Convert.ToInt32(childNode.ChildNodes[2].InnerText);
                        data.paraValue = Convert.ToDouble(childNode.ChildNodes[3].InnerText);
                        data.strRemark = childNode.ChildNodes[4].InnerText;

                        lstJHRatio.Add(data);
                    }
                }
                adjustData.checkFactor = lstJHRatio;

                //技术因子
                List<ParaData> lstTechRatio = null;
                XmlNodeList lstTechNode = justNode.ChildNodes[4].ChildNodes[0].ChildNodes;
                if (lstTechNode != null && lstTechNode.Count > 0)
                {
                    lstTechRatio = new List<ParaData>();
                    foreach (XmlNode childNode in lstTechNode)
                    {
                        ParaData data = new ParaData();
                        data.paraName = childNode.ChildNodes[0].InnerText;
                        data.paraUnit = childNode.ChildNodes[1].InnerText;
                        data.paraType = Convert.ToInt32(childNode.ChildNodes[2].InnerText);
                        data.paraValue = Convert.ToDouble(childNode.ChildNodes[3].InnerText);
                        data.strRemark = childNode.ChildNodes[4].InnerText;

                        lstTechRatio.Add(data);
                    }
                }
                adjustData.technologyFactor = lstTechRatio;

                lstAdjustData.Add(adjustData);
            }

            return lstAdjustData;
        }

        /// <summary>
        /// 获取重心包线结果列表
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private List<Model.CoreEnvelopeArithmetic> GetCoreEnvelopeDesignReustData(XmlDocument doc)
        {
            string path = string.Empty;
            XmlNode node = null;

            List<Model.CoreEnvelopeArithmetic> lstCoreEnvelopeDesignData = new List<Model.CoreEnvelopeArithmetic>();

            path = "PRJ/重量重心设计工程/重心包线设计结果列表";
            node = doc.SelectSingleNode(path);
            if (node == null)
            {
                return lstCoreEnvelopeDesignData;
            }
            for (int m = 0; m < node.ChildNodes.Count; m++)
            {
                Model.CoreEnvelopeArithmetic arithmetic = new Model.CoreEnvelopeArithmetic();
                //重心包线设计数据名称
                arithmetic.DataName = node.ChildNodes[m].ChildNodes[0].InnerText;

                //重心算法
                arithmetic.Name = node.ChildNodes[m].ChildNodes[2].ChildNodes[0].InnerText;
                arithmetic.CreateTime = node.ChildNodes[m].ChildNodes[2].ChildNodes[1].InnerText;
                arithmetic.LastModifyTime = node.ChildNodes[m].ChildNodes[2].ChildNodes[2].InnerText;
                arithmetic.Remark = node.ChildNodes[m].ChildNodes[2].ChildNodes[5].InnerText;

                List<Model.NodeFormula> lstFormulaList = new List<Model.NodeFormula>();

                XmlNodeList lstNode = node.ChildNodes[m].ChildNodes[2].ChildNodes[4].ChildNodes;
                Dictionary<WeightParameter, WeightParameter> wpDict = new Dictionary<WeightParameter, WeightParameter>();

                for (int i = 0; i < lstNode.Count; i++)
                {
                    Model.NodeFormula formula = new Model.NodeFormula();
                    formula.NodeName = lstNode[i].ChildNodes[0].InnerText;

                    Model.WeightFormula XFormula = formula.XFormula;
                    XmlNodeList lstXNode = lstNode[i].ChildNodes[1].ChildNodes;


                    for (int k = 0; k < lstXNode.Count; k++)
                    {
                        //公式
                        if (lstXNode[k].LocalName == "公式")
                        {
                            XFormula.Formula = lstXNode[k].ChildNodes[0].InnerText;
                        }
                        //参数列表
                        if (lstXNode[k].LocalName == "参数列表")
                        {
                            //--------------------------------------------------------------------------------------//
                            foreach (XmlNode paraNode in lstXNode[k].ChildNodes)
                            {
                                string name = paraNode.ChildNodes[0].InnerText;
                                string unit = paraNode.ChildNodes[1].InnerText;
                                WeightParameter wpGlobal = WeightArithmetic.FindGlobleParameters(name, unit)[0];

                                WeightParameter wp = null;
                                if (wpGlobal == null)
                                {
                                    wp = new WeightParameter();
                                    wp.ParaName = name;
                                    wp.ParaUnit = unit;
                                    wp.ParaType = 10;
                                    wp.ParaValue = Convert.ToDouble(paraNode.ChildNodes[3].InnerText);
                                    wp.ParaRemark = paraNode.ChildNodes[4].InnerText;


                                    WeightParameter temp10wp = new WeightParameter(wp);
                                    WeightParameter.GetWeightParameterList()[10].Add(temp10wp);

                                    wpDict.Add(temp10wp, wp);
                                }
                                else
                                {
                                    if (!wpDict.ContainsKey(wpGlobal))
                                    {
                                        wp = new WeightParameter(wpGlobal);
                                        wp.ParaValue = Convert.ToDouble(paraNode.ChildNodes[3].InnerText);
                                        wpDict.Add(wpGlobal, wp);
                                    }
                                    else
                                    {
                                        wp = wpDict[wpGlobal];
                                    }
                                }
                                XFormula.ParaList.Add(wp);

                            }
                            //--------------------------------------------------------------------------------------//

                        }
                        //结果数值
                        if (lstXNode[k].LocalName == "结果数值")
                        {
                            XFormula.Value = Convert.ToDouble(lstXNode[k].InnerText);
                        }
                    }
                    //formula.XFormula = XFormula;


                    Model.WeightFormula YFormula = formula.YFormula;
                    XmlNodeList lstYNode = lstNode[i].ChildNodes[2].ChildNodes;
                    for (int k = 0; k < lstYNode.Count; k++)
                    {
                        //公式
                        if (lstYNode[k].LocalName == "公式")
                        {
                            YFormula.Formula = lstYNode[k].ChildNodes[0].InnerText;
                        }
                        //参数列表
                        if (lstYNode[k].LocalName == "参数列表")
                        {
                            //--------------------------------------------------------------------------------------//
                            foreach (XmlNode paraNode in lstYNode[k].ChildNodes)
                            {
                                string name = paraNode.ChildNodes[0].InnerText;
                                string unit = paraNode.ChildNodes[1].InnerText;
                                WeightParameter wpGlobal = WeightArithmetic.FindGlobleParameters(name, unit)[0];

                                WeightParameter wp = null;
                                if (wpGlobal == null)
                                {
                                    wp = new WeightParameter();
                                    wp.ParaName = name;
                                    wp.ParaUnit = unit;
                                    wp.ParaType = 10;
                                    wp.ParaValue = Convert.ToDouble(paraNode.ChildNodes[3].InnerText);
                                    wp.ParaRemark = paraNode.ChildNodes[4].InnerText;


                                    WeightParameter temp10wp = new WeightParameter(wp);
                                    WeightParameter.GetWeightParameterList()[10].Add(temp10wp);

                                    wpDict.Add(temp10wp, wp);
                                }
                                else
                                {
                                    if (!wpDict.ContainsKey(wpGlobal))
                                    {
                                        wp = new WeightParameter(wpGlobal);
                                        wp.ParaValue = Convert.ToDouble(paraNode.ChildNodes[3].InnerText);
                                        wpDict.Add(wpGlobal, wp);
                                    }
                                    else
                                    {
                                        wp = wpDict[wpGlobal];
                                    }
                                }
                                YFormula.ParaList.Add(wp);

                            }
                            //--------------------------------------------------------------------------------------//
                        }
                        //结果数值
                        if (lstYNode[k].LocalName == "结果数值")
                        {
                            YFormula.Value = Convert.ToDouble(lstYNode[k].InnerText);
                        }
                    }
                    //formula.YFormula = YFormula;

                    lstFormulaList.Add(formula);
                }
                arithmetic.FormulaList = lstFormulaList;
                lstCoreEnvelopeDesignData.Add(arithmetic);
            }
            return lstCoreEnvelopeDesignData;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="strFilePath"></param>
        private void SaveFile(string strFilePath)
        {
            List<string> lstContent = GetFileContent();
            XCommon.CommonFunction.mWriteListStringToFile(strFilePath, lstContent);
        }

        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <returns></returns>
        private List<string> GetFileContent()
        {
            List<string> lstContent = new List<string>();

            if (designProjectData != null)
            {
                string str = string.Empty;

                lstContent.Insert(0, "<?xml version=" + "\"1.0\"" + " encoding=" + "\"gb2312\"" + " ?> ");
                lstContent.Add("<PRJ>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<重量重心设计工程>");

                str = CommonFunction.mStrModifyToString8(2) + "<工程名称>" + designProjectData.projectName + "</工程名称>";
                lstContent.Add(str);
                str = CommonFunction.mStrModifyToString8(2) + "<创建人>" + designProjectData.projectCreator + "</创建人>";
                lstContent.Add(str);
                str = CommonFunction.mStrModifyToString8(2) + "<备注>" + designProjectData.projectCreator + "</备注>";
                lstContent.Add(str);

                //重量设计结果
                List<string> lstDesignResultContent = GetDesignResultContent();
                foreach (string strContent in lstDesignResultContent)
                {
                    lstContent.Add(strContent);
                }

                //重量调整结果
                List<string> lstAdjustResultContent = GetAdjustmentResultContent();
                foreach (string strContent in lstAdjustResultContent)
                {
                    lstContent.Add(strContent);
                }

                //重量评估结果
                if (designProjectData.weightAssessResultList != null && designProjectData.weightAssessResultList.Count > 0)
                {
                    lstContent.Add(new CommonUtil().exportWeightAssessXmlString(this.designProjectData.weightAssessResultList));
                }

                //重心包线设计结果
                List<string> lstEnvelopeDesignResultContent = GetEnvelopeDesignResultContent();
                foreach (string strContent in lstEnvelopeDesignResultContent)
                {
                    lstContent.Add(strContent);
                }

                //重心包线剪裁结果
                List<string> lstCutResultContent = GetCoreEnvelopeCutContent();
                foreach (string strContent in lstCutResultContent)
                {
                    lstContent.Add(strContent);
                }

                //重心包线评估结果
                if (designProjectData.CoreAssessResultList != null && designProjectData.CoreAssessResultList.Count > 0)
                {
                    lstContent.Add(new CommonUtil().exportCoreAssessXmlString(this.designProjectData.CoreAssessResultList));
                }

                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "</重量重心设计工程>");
                lstContent.Add("</PRJ>");
            }

            return lstContent;
        }

        /// <summary>
        /// 获取重量调整结果内容
        /// </summary>
        /// <returns></returns>
        private List<string> GetAdjustmentResultContent()
        {
            List<string> lstContent = new List<string>();

            List<WeightAdjustmentResultData> lstResult = designProjectData.lstAdjustmentResultData;
            if (lstResult != null && lstResult.Count > 0)
            {
                string str = string.Empty;
                lstContent.Add(CommonFunction.mStrModifyToString8(2) + "<重量调整结果列表>");

                foreach (WeightAdjustmentResultData adjustResult in lstResult)
                {
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<重量调整结果>");
                    str = CommonFunction.mStrModifyToString8(4) + "<重量调整数据名称>" + adjustResult.WeightAdjustName + "</重量调整数据名称>";
                    lstContent.Add(str);

                    #region 基础重量数据

                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "<基础重量数据>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(5) + "<重量分类>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(6) + "<重量分类名称 >" + adjustResult.basicWeightData.sortName + "</重量分类名称>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(6) + "<重量数据列表>");

                    //基础重量数据
                    List<WeightData> lstWeightData = adjustResult.basicWeightData.lstWeightData;
                    for (int m = 0; m < lstWeightData.Count; m++)
                    {
                        lstContent.Add(CommonFunction.mStrModifyToString8(7) + "<重量数据>");

                        str = CommonFunction.mStrModifyToString8(8) + "<ID>" + lstWeightData[m].nID.ToString() + "</ID>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(8) + "<重量名称>" + lstWeightData[m].weightName + "</重量名称>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(8) + "<重量单位>" + lstWeightData[m].weightUnit + "</重量单位>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(8) + "<重量数值>" + lstWeightData[m].weightValue.ToString() + "</重量数值>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(8) + "<备注>" + lstWeightData[m].strRemark + "</备注>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(8) + "<PARENTID>" + lstWeightData[m].nParentID + "</PARENTID>";
                        lstContent.Add(str);

                        lstContent.Add(CommonFunction.mStrModifyToString8(7) + "</重量数据>");
                    }

                    lstContent.Add(CommonFunction.mStrModifyToString8(6) + "</重量数据列表>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(5) + "</重量分类>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "</基础重量数据>");

                    #endregion

                    #region 调整重量数据

                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "<调整重量数据>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(5) + "<重量分类>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(6) + "<重量分类名称 >" + adjustResult.basicWeightData.sortName + "</重量分类名称>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(6) + "<重量数据列表>");

                    //基础重量数据
                    List<WeightData> lstAdjustWeightData = adjustResult.weightAdjustData.lstWeightData;
                    for (int m = 0; m < lstAdjustWeightData.Count; m++)
                    {
                        lstContent.Add(CommonFunction.mStrModifyToString8(7) + "<重量数据>");

                        str = CommonFunction.mStrModifyToString8(8) + "<ID>" + lstAdjustWeightData[m].nID.ToString() + "</ID>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(8) + "<重量名称>" + lstAdjustWeightData[m].weightName + "</重量名称>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(8) + "<重量单位>" + lstAdjustWeightData[m].weightUnit + "</重量单位>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(8) + "<重量数值>" + lstAdjustWeightData[m].weightValue.ToString() + "</重量数值>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(8) + "<备注>" + lstAdjustWeightData[m].strRemark + "</备注>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(8) + "<PARENTID>" + lstAdjustWeightData[m].nParentID + "</PARENTID>";
                        lstContent.Add(str);

                        lstContent.Add(CommonFunction.mStrModifyToString8(7) + "</重量数据>");
                    }

                    lstContent.Add(CommonFunction.mStrModifyToString8(6) + "</重量数据列表>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(5) + "</重量分类>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "</调整重量数据>");
                    #endregion

                    //校核因子
                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "<校核因子>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(5) + "<参数列表>");
                    if (adjustResult.checkFactor != null && adjustResult.checkFactor.Count > 0)
                    {
                        foreach (ParaData data in adjustResult.checkFactor)
                        {
                            lstContent.Add(CommonFunction.mStrModifyToString8(6) + "<参数>");

                            str = CommonFunction.mStrModifyToString8(7) + "<参数名称>" + data.paraName + "</参数名称>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(7) + "<参数单位>" + data.paraUnit + "</参数单位>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(7) + "<参数类型>" + data.paraType.ToString() + "</参数类型>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(7) + "<参数数值>" + data.paraValue.ToString() + "</参数数值>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(7) + "<参数备注>" + data.strRemark + "</参数备注>";
                            lstContent.Add(str);

                            lstContent.Add(CommonFunction.mStrModifyToString8(6) + "</参数>");
                        }
                    }
                    lstContent.Add(CommonFunction.mStrModifyToString8(5) + "</参数列表>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "</校核因子>");

                    //技术因子
                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "<技术因子>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(5) + "<参数列表>");
                    if (adjustResult.technologyFactor != null && adjustResult.technologyFactor.Count > 0)
                    {
                        foreach (ParaData data in adjustResult.technologyFactor)
                        {
                            lstContent.Add(CommonFunction.mStrModifyToString8(6) + "<参数>");

                            str = CommonFunction.mStrModifyToString8(7) + "<参数名称>" + data.paraName + "</参数名称>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(7) + "<参数单位>" + data.paraUnit + "</参数单位>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(7) + "<参数类型>" + data.paraType.ToString() + "</参数类型>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(7) + "<参数数值>" + data.paraValue.ToString() + "</参数数值>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(7) + "<参数备注>" + data.strRemark + "</参数备注>";
                            lstContent.Add(str);

                            lstContent.Add(CommonFunction.mStrModifyToString8(6) + "</参数>");
                        }
                    }
                    lstContent.Add(CommonFunction.mStrModifyToString8(5) + "</参数列表>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "</技术因子>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "</重量调整结果>");

                }
                lstContent.Add(CommonFunction.mStrModifyToString8(2) + "</重量调整结果列表>");
            }
            return lstContent;


        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        public void InitializePage(string strType)
        {
            if (strType == "new")
            {
                tabControlWork.TabPages.Clear();

                selNode = null;
            }
            fToolStripMenuItemTool.Enabled = true;
        }

        //private void SetPageControls(bool IsEnable, string strType)
        //{
        //    //重量设计
        //    if (strType == "weightDesign" || strType == "all")
        //    {
        //        btnExportWeightToFile.Enabled = IsEnable;
        //        //btnExportWeightToHJ.Enabled = IsEnable;
        //        btnExportWeightToDB.Enabled = IsEnable;
        //        gridDesignResult.ReadOnly = !IsEnable;
        //        btnWeightDesignPubilishToTde.Enabled = IsEnable;
        //    }
        //    //重心包线设计
        //    if (strType == "coreEnvelope" || strType == "all")
        //    {
        //        btnExportCoreDesignWeightToDataFile.Enabled = IsEnable;
        //        btnCoreEnvelopeDesignPublishToTde.Enabled = IsEnable;
        //        btnExportCoreDesignWeightToDB.Enabled = IsEnable;
        //        gridCoreEnvelopeDesign.ReadOnly = !IsEnable;
        //        btnCoreEnvelopeDesignPublishToTde.Enabled = IsEnable;
        //    }
        //    //重心包线剪裁
        //    if (strType == "coreEnvelopeCut" || strType == "all")
        //    {
        //        btnExportCutWeightToDataFile.Enabled = IsEnable;
        //        //btnExportCutWeightToZC.Enabled = IsEnable;
        //        btnExportCutWeightToDB.Enabled = IsEnable;
        //        gridViewCoreEnvelopeCutReuslt.ReadOnly = !IsEnable;
        //        btnCoreEnvelopeCutPublishToTde.Enabled = IsEnable;
        //    }
        //    //重量调整
        //    if (strType == "weightAdjust" || strType == "all")
        //    {
        //        btnExportAdjustmentWeightToDataFile.Enabled = IsEnable;
        //        //btnExportAdjustmentWeightToZC.Enabled = IsEnable;
        //        btnExportAdjustmentWeightToDB.Enabled = IsEnable;
        //        gridViewAdjustment.ReadOnly = !IsEnable;
        //        btnWeightAdjustPublishToTde.Enabled = IsEnable;
        //    }
        //    //重量评估
        //    if (strType == "weightAdjust" || strType == "all")
        //    {
        //        btnExportAdjustmentWeightToDataFile.Enabled = IsEnable;
        //        //btnExportAdjustmentWeightToZC.Enabled = IsEnable;
        //        btnExportAdjustmentWeightToDB.Enabled = IsEnable;
        //        gridViewAdjustment.ReadOnly = !IsEnable;
        //        btnWeightAdjustPublishToTde.Enabled = IsEnable;
        //    }
        //    //重心包线评估
        //    if (strType == "weightAdjust" || strType == "all")
        //    {
        //        btnExportAdjustmentWeightToDataFile.Enabled = IsEnable;
        //        //btnExportAdjustmentWeightToZC.Enabled = IsEnable;
        //        btnExportAdjustmentWeightToDB.Enabled = IsEnable;
        //        gridViewAdjustment.ReadOnly = !IsEnable;
        //        btnWeightAdjustPublishToTde.Enabled = IsEnable;
        //    }
        //}

        /// <summary>
        /// 绘制饼图
        /// </summary>
        /// <param name="chart1"></param>
        /// <param name="lstWeightData"></param>
        public static void DisplayPiePic(System.Windows.Forms.DataVisualization.Charting.Chart chart1, List<WeightData> lstWeightData, string strTitle)
        {
            if (lstWeightData != null && lstWeightData.Count > 0)
            {
                chart1.Titles.Clear();
                chart1.Series.Clear();
                chart1.ChartAreas.Clear();
                chart1.Legends.Clear();

                lstWeightData = GetSortListWeightData(lstWeightData);
                string[] arrayXValue = new string[lstWeightData.Count];
                double[] arrayYValue = new double[lstWeightData.Count];
                for (int i = 0; i < lstWeightData.Count; i++)
                {
                    arrayYValue[i] = Math.Round(lstWeightData[i].weightValue, picDigit);
                    arrayXValue[i] = lstWeightData[i].weightName;
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

        #region 重量设计结果

        public void SetWeightDesignTab()
        {
            if (tabControlWork.TabPages.Contains(tabPageWeightDesign) == false)
            {
                tabControlWork.TabPages.Add(tabPageWeightDesign);
            }
            tabPageWeightDesign.ToolTipText = selNode.ToolTipText;
            Model.WeightArithmetic weightArithmetic = GetWeightArithmetic();
            SetWeightDesignReuslt(weightArithmetic);

            tabControlWork.SelectedTab = tabPageWeightDesign;
        }

        public void SetWeightDesignTab(Model.WeightArithmetic weightArithmetic, int index)
        {
            if (tabControlWork.TabPages.Contains(tabPageWeightDesign) == false)
            {
                tabControlWork.TabPages.Add(tabPageWeightDesign);
            }

            tabPageWeightDesign.ToolTipText = index.ToString();
            SetWeightDesignReuslt(weightArithmetic);
            tabControlWork.SelectedTab = tabPageWeightDesign;

            TreeNode rootNode = treeViewData.Nodes[0];
            foreach (TreeNode parentNode in rootNode.Nodes)
            {
                if (parentNode.Text == "重量设计结果列表")
                {
                    foreach (TreeNode node in parentNode.Nodes)
                    {
                        if (index.ToString() == node.ToolTipText)
                        {
                            treeViewData.SelectedNode = node;
                            selNode = node;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取重量设计结果内容
        /// </summary>
        /// <returns></returns>
        private List<string> GetDesignResultContent()
        {
            List<string> lstContent = new List<string>();

            List<Model.WeightArithmetic> lstResult = designProjectData.lstWeightArithmetic;
            if (lstResult != null && lstResult.Count > 0)
            {
                string str = string.Empty;
                lstContent.Add(CommonFunction.mStrModifyToString8(2) + "<重量设计结果列表>");

                foreach (WeightArithmetic weightDesign in lstResult)
                {
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<重量设计结果>");
                    str = CommonFunction.mStrModifyToString8(4) + "<重量设计数据名称>" + weightDesign.DataName + "</重量设计数据名称>";
                    lstContent.Add(str);

                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "<参数列表>");

                    List<Model.WeightParameter> lstParaData = weightDesign.GetParaList();

                    for (int j = 0; j < lstParaData.Count; j++)
                    {
                        lstContent.Add(CommonFunction.mStrModifyToString8(5) + "<参数>");

                        str = CommonFunction.mStrModifyToString8(6) + "<参数名称>" + lstParaData[j].ParaName + "</参数名称>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(6) + "<参数单位>" + lstParaData[j].ParaUnit + "</参数单位>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(6) + "<参数类型>" + lstParaData[j].ParaType.ToString() + "</参数类型>";
                        lstContent.Add(str);

                        str = CommonFunction.mStrModifyToString8(6) + "<参数数值>" + lstParaData[j].ParaValue.ToString() + "</参数数值>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(6) + "<参数备注>" + lstParaData[j].ParaRemark + "</参数备注>";
                        lstContent.Add(str);

                        lstContent.Add(CommonFunction.mStrModifyToString8(5) + "</参数>");
                    }
                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "</参数列表>");

                    //-----------------------------------重量算法----------------------------------------------------//
                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "<重量算法>");

                    str = CommonFunction.mStrModifyToString8(5) + "<重量算法名称>" + weightDesign.Name + "</重量算法名称>";
                    lstContent.Add(str);
                    str = CommonFunction.mStrModifyToString8(5) + "<重量算法创建时间>" + weightDesign.CreateTime + "</重量算法创建时间>";
                    lstContent.Add(str);
                    str = CommonFunction.mStrModifyToString8(5) + "<重量算法最后修改时间>" + weightDesign.LastModifyTime + "</重量算法最后修改时间>";
                    lstContent.Add(str);

                    lstContent.Add(CommonFunction.mStrModifyToString8(5) + "<重量分类>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(6) + "<重量分类名称 >" + weightDesign.SortName + "</重量分类名称>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(6) + "<重量数据列表>");

                    //重量数据
                    List<string> lstWeightContent = GetDesignWeightDataContent(8, weightDesign.ExportDataToWeightSort());
                    foreach (string strContent in lstWeightContent)
                    {
                        lstContent.Add(strContent);
                    }

                    lstContent.Add(CommonFunction.mStrModifyToString8(6) + "</重量数据列表>");

                    //重量公式列表

                    List<string> lstWeightFormulaContent = GetDesignWeightFormulaContent(weightDesign);
                    foreach (string strContent in lstWeightFormulaContent)
                    {
                        lstContent.Add(strContent);
                    }

                    lstContent.Add(CommonFunction.mStrModifyToString8(5) + "</重量分类>");

                    str = CommonFunction.mStrModifyToString8(5) + "<算法备注>" + weightDesign.Remark + "</算法备注>";
                    lstContent.Add(str);

                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "</重量算法>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "</重量设计结果>");

                }
                lstContent.Add(CommonFunction.mStrModifyToString8(2) + "</重量设计结果列表>");
            }
            return lstContent;
        }

        /// <summary>
        /// 获取重量数据
        /// </summary>
        /// <param name="lstWeightData"></param>
        /// <returns></returns>
        public static List<string> GetDesignWeightDataContent(int index, WeightSortData weightSortData, DataGridView gridDesignResult)
        {
            List<string> lstContent = new List<string>();
            string str = string.Empty;
            double dCountValue = GetDesignResultCount(weightSortData, gridDesignResult);

            List<WeightData> lstWeightData = GetModifyListWeightData(weightSortData.lstWeightData, gridDesignResult);
            foreach (WeightData data in lstWeightData)
            {
                lstContent.Add(CommonFunction.mStrModifyToString8(index - 1) + "<重量数据>");
                lstContent.Add(CommonFunction.mStrModifyToString8(index) + "<ID>" + data.nID.ToString() + "</ID>");
                lstContent.Add(CommonFunction.mStrModifyToString8(index) + "<重量名称>" + data.weightName + "</重量名称>");
                lstContent.Add(CommonFunction.mStrModifyToString8(index) + "<重量单位>" + data.weightUnit + "</重量单位>");
                if (data.nParentID == -1)
                {
                    lstContent.Add(CommonFunction.mStrModifyToString8(index) + "<重量数值>" + dCountValue.ToString() + "</重量数值>");
                }
                else
                {
                    lstContent.Add(CommonFunction.mStrModifyToString8(index) + "<重量数值>" + data.weightValue.ToString() + "</重量数值>");
                }
                lstContent.Add(CommonFunction.mStrModifyToString8(index) + "<备注>" + data.strRemark + "</备注>");
                lstContent.Add(CommonFunction.mStrModifyToString8(index) + "<PARENTID>" + data.nParentID.ToString() + "</PARENTID>");
                lstContent.Add(CommonFunction.mStrModifyToString8(index - 1) + "</重量数据>");
            }
            return lstContent;
        }

        /// <summary>
        /// 获取重量数据
        /// </summary>
        /// <param name="lstWeightData"></param>
        /// <returns></returns>
        public static List<string> GetDesignWeightDataContent(int index, WeightSortData weightSortData)
        {
            List<string> lstContent = new List<string>();
            string str = string.Empty;

            if (weightSortData != null)
            {
                foreach (WeightData data in weightSortData.lstWeightData)
                {
                    lstContent.Add(CommonFunction.mStrModifyToString8(index - 1) + "<重量数据>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(index) + "<ID>" + data.nID.ToString() + "</ID>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(index) + "<重量名称>" + data.weightName + "</重量名称>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(index) + "<重量单位>" + data.weightUnit + "</重量单位>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(index) + "<重量数值>" + data.weightValue.ToString() + "</重量数值>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(index) + "<备注>" + data.strRemark + "</备注>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(index) + "<PARENTID>" + data.nParentID.ToString() + "</PARENTID>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(index - 1) + "</重量数据>");
                }
            }
            return lstContent;
        }

        /// <summary>
        /// 获取重量公式列表
        /// </summary>
        /// <returns></returns>
        private List<string> GetDesignWeightFormulaContent(Model.WeightArithmetic arithmetic)
        {
            List<string> lstContent = new List<string>();
            string str = string.Empty;

            lstContent.Add(CommonFunction.mStrModifyToString8(6) + "<重量公式列表>");

            List<Model.WeightFormula> lstFormula = arithmetic.FormulaList;

            foreach (Model.WeightFormula formula in lstFormula)
            {
                lstContent.Add(CommonFunction.mStrModifyToString8(7) + "<重量公式>");

                str = CommonFunction.mStrModifyToString8(8) + "<节点名称>" + formula.NodePath + "</节点名称>";
                lstContent.Add(str);
                str = CommonFunction.mStrModifyToString8(8) + "<公式>" + formula.Formula + "</公式>";
                lstContent.Add(str);

                lstContent.Add(CommonFunction.mStrModifyToString8(8) + "<参数列表>");
                foreach (Model.WeightParameter para in formula.ParaList)
                {
                    lstContent.Add(CommonFunction.mStrModifyToString8(9) + "<参数>");

                    str = CommonFunction.mStrModifyToString8(10) + "<参数名称>" + para.ParaName + "</参数名称>";
                    lstContent.Add(str);
                    str = CommonFunction.mStrModifyToString8(10) + "<参数单位>" + para.ParaUnit + "</参数单位>";
                    lstContent.Add(str);
                    str = CommonFunction.mStrModifyToString8(10) + "<参数类型>" + para.ParaType.ToString() + "</参数类型>";
                    lstContent.Add(str);
                    str = CommonFunction.mStrModifyToString8(10) + "<参数数值>" + para.ParaValue.ToString() + "</参数数值>";
                    lstContent.Add(str);
                    str = CommonFunction.mStrModifyToString8(10) + "<参数备注>" + para.ParaRemark + "</参数备注>";
                    lstContent.Add(str);

                    lstContent.Add(CommonFunction.mStrModifyToString8(9) + "</参数>");
                }

                lstContent.Add(CommonFunction.mStrModifyToString8(8) + "</参数列表>");
                str = CommonFunction.mStrModifyToString8(8) + "<结果数值>" + formula.Value + "</结果数值>";
                lstContent.Add(str);

                lstContent.Add(CommonFunction.mStrModifyToString8(7) + "</重量公式>");
            }
            lstContent.Add(CommonFunction.mStrModifyToString8(6) + "</重量公式列表>");
            return lstContent;
        }

        public static double GetDesignResultCount(WeightSortData weightSortData, DataGridView gridDesignResult)
        {
            double dTotalValue = 0;
            double dValue = 0;

            if (gridDesignResult.Rows.Count > 0)
            {
                List<WeightData> lstWeightData = weightSortData.lstWeightData;
                for (int j = 0; j < lstWeightData.Count; j++)
                {
                    for (int i = 0; i < gridDesignResult.ColumnCount; i++)
                    {
                        if (lstWeightData[j].nID.ToString() == gridDesignResult.Columns[i].Name)
                        {
                            dValue = gridDesignResult.Rows[0].Cells[i].Value is DBNull ? 0 : Convert.ToDouble(gridDesignResult.Rows[0].Cells[i].Value);
                            lstWeightData[j].weightValue = dValue;
                        }
                    }
                    if (j > 0)
                    {
                        dTotalValue += lstWeightData[j].weightValue;
                    }
                }
            }
            else
            {
                foreach (WeightData data in weightSortData.lstWeightData)
                {
                    dTotalValue += data.weightValue;
                }
            }
            return dTotalValue;
        }

        /// <summary>
        /// 绑定重量结构树数据子节点
        /// </summary>
        private static void BindTreeNode(TreeNode ParentNode, int nParentID, WeightSortData wsd)
        {
            IEnumerable<WeightData> selection = from wd in wsd.lstWeightData where wd.nParentID == nParentID select wd;
            foreach (WeightData wd in selection)
            {
                string strKey = ParentNode.Name + "\\" + wd.weightName;
                string strText = wd.weightName + "[" + Math.Round(wd.weightValue, digit).ToString() + " 千克" + "]";
                TreeNode node = ParentNode.Nodes.Add(strKey, strText);

                BindTreeNode(node, wd.nID, wsd);
            }
        }

        /// <summary>
        /// 绑定重量结构树数据子节点
        /// </summary>
        private static void BindTreeNodeNoValue(TreeNode ParentNode, int nParentID, WeightSortData wsd)
        {
            IEnumerable<WeightData> selection = from wd in wsd.lstWeightData where wd.nParentID == nParentID select wd;
            foreach (WeightData wd in selection)
            {
                string strKey = ParentNode.Name + "\\" + wd.weightName;
                string strText = wd.weightName;
                TreeNode node = ParentNode.Nodes.Add(strKey, strText);

                BindTreeNodeNoValue(node, wd.nID, wsd);
            }
        }

        /// <summary>
        /// 绑定重量结构树数据
        /// </summary>
        private static void BindTreeList(TreeView tree, WeightSortData sortData)
        {
            tree.Nodes.Clear();

            IEnumerable<WeightData> selection = from wd in sortData.lstWeightData where wd.nParentID == -1 select wd;
            foreach (WeightData wd in selection)
            {
                TreeNode node = new TreeNode();
                node.Name = wd.weightName;
                node.Text = wd.weightName + "[" + Math.Round(wd.weightValue, digit).ToString() + " 千克" + "]";
                tree.Nodes.Add(node);

                BindTreeNode(node, wd.nID, sortData);
            }
        }

        /// <summary>
        /// 绑定重量结构树数据
        /// </summary>
        private static void BindTreeListNoValue(TreeView tree, WeightSortData sortData)
        {
            tree.Nodes.Clear();

            IEnumerable<WeightData> selection = from wd in sortData.lstWeightData where wd.nParentID == -1 select wd;
            foreach (WeightData wd in selection)
            {
                TreeNode node = new TreeNode();
                node.Name = wd.weightName;
                node.Text = wd.weightName;
                tree.Nodes.Add(node);

                BindTreeNodeNoValue(node, wd.nID, sortData);
            }
        }

        /// <summary>
        /// 绑定重量分类
        /// </summary>
        /// <param name="strTitle"></param>
        public static void BindWeightDesignSort(WeightSortData sortData, TreeView treeViewSort)
        {
            treeViewSort.Nodes.Clear();

            if (sortData != null)
            {
                BindTreeList(treeViewSort, sortData);
                treeViewSort.ExpandAll();
            }
        }

        /// <summary>
        /// 绑定重量分类
        /// </summary>
        /// <param name="strTitle"></param>
        public static void BindWeightDesignSortNoValue(WeightSortData sortData, TreeView treeViewSort)
        {
            treeViewSort.Nodes.Clear();

            if (sortData != null)
            {
                BindTreeListNoValue(treeViewSort, sortData);
                treeViewSort.ExpandAll();
            }
        }

        private Model.WeightArithmetic GetWeightArithmetic()
        {
            Model.WeightArithmetic weightArithmetic = null;
            for (int i = 0; i < lstWeightArithmetic.Count; i++)
            {
                if (selNode != null && selNode.Level == 2 && selNode.ToolTipText == i.ToString())
                {
                    weightArithmetic = new Model.WeightArithmetic();
                    weightArithmetic = lstWeightArithmetic[i];
                }
            }
            return weightArithmetic;
        }

        private List<WeightData> GetModifyListWeightData(List<WeightData> lstWeightData)
        {
            List<WeightData> lstTempWeightData = new List<WeightData>();

            double dValue = 0;
            for (int i = 0; i < lstWeightData.Count; i++)
            {
                for (int j = 0; j < gridDesignResult.ColumnCount; j++)
                {
                    if (lstWeightData[i].nID.ToString() == gridDesignResult.Columns[j].Name)
                    {
                        dValue = gridDesignResult.Rows[0].Cells[j].Value is System.DBNull ? 0 : Convert.ToDouble(gridDesignResult.Rows[0].Cells[j].Value);
                        lstWeightData[i].weightValue = dValue;
                    }
                }
            }
            lstTempWeightData = lstWeightData;
            return lstTempWeightData;
        }

        public static List<WeightData> GetModifyListWeightData(List<WeightData> lstWeightData, DataGridView gridViewResult)
        {
            List<WeightData> lstTempWeightData = new List<WeightData>();

            double dValue = 0;
            for (int i = 0; i < lstWeightData.Count; i++)
            {
                for (int j = 0; j < gridViewResult.ColumnCount; j++)
                {
                    if (lstWeightData[i].nID.ToString() == gridViewResult.Columns[j].Name)
                    {
                        dValue = gridViewResult.Rows[0].Cells[j].Value is System.DBNull ? 0 : Convert.ToDouble(gridViewResult.Rows[0].Cells[j].Value);
                        lstWeightData[i].weightValue = dValue;
                        break;
                    }
                }
            }
            lstTempWeightData = lstWeightData;
            return lstTempWeightData;
        }

        /// <summary>
        /// 设置重量设计结果
        /// </summary>
        public void SetWeightDesignReuslt(Model.WeightArithmetic weightArithmetic)
        {
            if (weightArithmetic != null)
            {
                tabPageWeightDesign.Text = weightArithmetic.DataName;
                txtWeightDesingnDBName.Text = weightArithmetic.DataName;
                txtSortName.Text = weightArithmetic.SortName;

                //绑定GridView
                WeightSortData weightSortData = weightArithmetic.ExportDataToWeightSort();
                BindWeightDesignGridView(weightSortData, gridDesignResult);

                //绑定重量分类
                BindWeightDesignSort(weightSortData, treeViewSort);

                //绑定图形
                DisplayPiePic(chartWeightDesign, GetPicListWeightData(weightSortData, gridDesignResult), "重量分布");
            }
        }

        public static void SetWeightDesignGridView(WeightSortData weightSortData, DataGridView gridDesignResult)
        {
            gridDesignResult.Columns.Clear();
            gridDesignResult.DataSource = null;

            if (weightSortData != null && weightSortData.lstWeightData.Count > 0)
            {
                foreach (WeightData data in weightSortData.lstWeightData)
                {
                    if (IsLastWeightNode(data, weightSortData))
                    {
                        DataGridViewTextBoxColumn gridColumn = new DataGridViewTextBoxColumn();
                        gridColumn.Name = data.nID.ToString();
                        gridColumn.DataPropertyName = data.weightName;
                        gridColumn.HeaderText = data.weightName;
                        gridColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                        gridDesignResult.Columns.Add(gridColumn);
                    }
                }
            }

            if (gridDesignResult.ColumnCount > 0)
            {
                gridDesignResult.Rows.Add(1);
            }

            for (int i = 0; i < gridDesignResult.ColumnCount; i++)
            {
                gridDesignResult.Rows[0].Cells[i].Value = 0;
            }
        }

        public static void BindWeightDesignGridView(WeightSortData weightSortData, DataGridView gridDesignResult)
        {
            SetWeightDesignGridView(weightSortData, gridDesignResult);
            DataTable table = GetWeightDesignTableData(weightSortData);

            for (int i = 0; i < gridDesignResult.ColumnCount; i++)
            {
                gridDesignResult.Rows[0].Cells[i].Value = table.Rows[0][gridDesignResult.Columns[i].Name];
            }

        }

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

        private static DataTable GetWeightDesignTableStruct(WeightSortData weightSortData)
        {
            DataTable table = new DataTable();

            if (weightSortData != null && weightSortData.lstWeightData.Count > 0)
            {
                foreach (WeightData data in weightSortData.lstWeightData)
                {
                    if (IsLastWeightNode(data, weightSortData))
                    {
                        table.Columns.Add(data.nID.ToString());
                    }
                }
            }

            return table;
        }

        private static DataTable GetWeightDesignTableData(WeightSortData weightSortData)
        {
            DataTable table = GetWeightDesignTableStruct(weightSortData);

            if (weightSortData != null && weightSortData.lstWeightData.Count > 0)
            {
                DataRow dr = table.NewRow();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    foreach (WeightData data in weightSortData.lstWeightData)
                    {
                        if (data.nID.ToString() == table.Columns[i].ColumnName)
                        {
                            dr[data.nID.ToString()] = Math.Round(data.weightValue, digit);
                        }
                    }

                }
                table.Rows.Add(dr);
            }

            return table;
        }

        public static List<string> GetDesignResultFlieContent(WeightSortData weightSortData)
        {
            List<string> lstContent = null;
            if (weightSortData != null)
            {
                lstContent = new List<string>();
                lstContent.Insert(0, "<?xml version=" + "\"1.0\"" + " encoding=" + "\"gb2312\"" + " ?> ");

                lstContent.Add("<重量分类>");
                string str = CommonFunction.mStrModifyToString8(1) + "<重量分类名称>" + weightSortData.sortName + "</重量分类名称>";
                lstContent.Add(str);

                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<重量数据列表>");

                //重量数据
                List<string> lstWeightContent = GetDesignWeightDataContent(3, weightSortData);
                foreach (string strContent in lstWeightContent)
                {
                    lstContent.Add(strContent);
                }

                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "</重量数据列表>");

                str = CommonFunction.mStrModifyToString8(1) + "<重量分类备注>" + weightSortData.strRemark + "</重量分类备注>";
                lstContent.Add(str);

                lstContent.Add("</重量分类>");
            }
            return lstContent;
        }

        private bool IsContainWeightPara(string strName, WeightSortData weightSortData)
        {
            bool IsContain = false;
            foreach (WeightData data in weightSortData.lstWeightData)
            {
                if (strName == data.weightName + "重量")
                {
                    IsContain = true;
                }
            }

            return IsContain;
        }

        public static List<string> GetDesignResultExcleCloumn()
        {
            List<string> lstTitle = new List<string>();

            lstTitle.Add("重量分类名称");
            lstTitle.Add("ID");
            lstTitle.Add("重量名称");
            lstTitle.Add("重量单位");
            lstTitle.Add("重量数值");
            lstTitle.Add("备注");
            lstTitle.Add("PARENTID");

            return lstTitle;
        }

        private static DataTable GetTableDesignResultStruct()
        {
            DataTable table = new DataTable();

            table.Columns.Add("Sort_Name");
            table.Columns.Add("ID");
            table.Columns.Add("Weight_Name");
            table.Columns.Add("Weight_Unit");
            table.Columns.Add("Weight_Value");
            table.Columns.Add("Weight_Remark");
            table.Columns.Add("Prarent_ID");

            return table;
        }

        public static DataTable GetDesignResultTable(WeightSortData sortData)
        {
            DataTable table = GetTableDesignResultStruct();

            if (sortData != null)
            {
                foreach (WeightData data in sortData.lstWeightData)
                {
                    DataRow dr = table.NewRow();

                    if (data == sortData.lstWeightData.First())
                    {
                        dr["Sort_Name"] = sortData.sortName;
                    }
                    else
                    {
                        dr["Sort_Name"] = string.Empty;
                    }
                    dr["ID"] = data.nID;
                    dr["Weight_Name"] = data.weightName;
                    dr["Weight_Unit"] = "千克";
                    dr["Weight_Remark"] = data.strRemark;
                    dr["Prarent_ID"] = data.nParentID;
                    dr["Weight_Value"] = data.weightValue;

                    table.Rows.Add(dr);
                }
            }

            return table;
        }

        public static List<WeightData> GetPicListWeightData(WeightSortData sortData, DataGridView gridResult)
        {
            List<WeightData> lstWeightData = new List<WeightData>();

            if (sortData != null)
            {
                //获取第一个节点的重量数据对象
                IEnumerable<WeightData> selection = from wd in sortData.lstWeightData where wd.nParentID == 0 select wd;
                foreach (WeightData wd in selection)
                {
                    lstWeightData.Add(wd);
                }

                double dValue = 0;
                for (int i = 0; i < lstWeightData.Count; i++)
                {
                    for (int j = 0; j < gridResult.ColumnCount; j++)
                    {
                        if (lstWeightData[i].nID.ToString() == gridResult.Columns[j].Name)
                        {
                            dValue = gridResult.Rows[0].Cells[j].Value is System.DBNull ? 0 : Convert.ToDouble(gridResult.Rows[0].Cells[j].Value);
                            lstWeightData[i].weightValue = dValue;
                            break;
                        }
                    }
                }
            }

            return lstWeightData;
        }

        private static List<WeightData> GetSortListWeightData(List<WeightData> lstSourceData)
        {
            List<WeightData> lstWeightData = new List<WeightData>();

            List<WeightData> lstTempWeightData = lstSourceData;
            WeightData data = new WeightData();

            for (int i = 0; i < lstTempWeightData.Count; i++)
            {
                for (int j = i + 1; j < lstTempWeightData.Count; j++)
                {
                    if (lstTempWeightData[i].weightValue < lstTempWeightData[j].weightValue)
                    {
                        data = lstTempWeightData[i];
                        lstTempWeightData[i] = lstTempWeightData[j];
                        lstTempWeightData[j] = data;
                    }
                }
            }


            for (int i = 0; i < lstTempWeightData.Count / 2; i++)
            {
                lstWeightData.Add(lstTempWeightData[i]);
                lstWeightData.Add(lstTempWeightData[lstTempWeightData.Count - 1 - i]);
            }
            if (lstTempWeightData.Count % 2 != 0)
            {
                lstWeightData.Add(lstTempWeightData[lstTempWeightData.Count / 2]);
            }
            return lstWeightData;
        }

        /// <summary>
        /// 获取当前重量分类
        /// </summary>
        /// <param name="tempSortData"></param>
        /// <param name="gridResult"></param>
        /// <returns></returns>
        private WeightSortData GetCurrentSortData(WeightSortData tempSortData, DataGridView gridResult)
        {
            WeightSortData sortData = tempSortData.Clone();
            if (gridResult.Rows.Count > 0 && sortData != null)
            {
                foreach (WeightData data in sortData.lstWeightData)
                {
                    for (int i = 0; i < gridResult.ColumnCount; i++)
                    {
                        if (data.nID.ToString() == gridResult.Columns[i].Name)
                        {
                            data.weightValue = Convert.ToDouble(gridResult.Rows[0].Cells[i].Value);
                        }
                    }
                }
            }

            //求和
            if (sortData != null)
            {
                foreach (WeightData data in sortData.lstWeightData)
                {
                    IEnumerable<WeightData> selection = from wd in sortData.lstWeightData where wd.nParentID == data.nID select wd;

                    WeightDesignDataForm.GetSortDataTotal(data, sortData);
                }
            }

            return sortData;
        }

        #endregion

        #region 重心包线设计结果

        public void SetCoreEnvelopeDesignTab()
        {
            if (tabControlWork.TabPages.Contains(tabPageEnvelopeDesign) == false)
            {
                tabControlWork.TabPages.Add(tabPageEnvelopeDesign);
            }
            tabPageEnvelopeDesign.ToolTipText = selNode.ToolTipText;

            Model.CoreEnvelopeArithmetic coreEnvelope = GetCoreEnvelopeDesign();
            SetCoreEnvelopeDesignResult(coreEnvelope);
            tabControlWork.SelectedTab = tabPageEnvelopeDesign;
        }

        public void SetCoreEnvelopeDesignTab(Model.CoreEnvelopeArithmetic coreEnvelope, int index)
        {
            if (tabControlWork.TabPages.Contains(tabPageEnvelopeDesign) == false)
            {
                tabControlWork.TabPages.Add(tabPageEnvelopeDesign);
            }

            tabPageEnvelopeDesign.ToolTipText = index.ToString();
            SetCoreEnvelopeDesignResult(coreEnvelope);

            tabControlWork.SelectedTab = tabPageEnvelopeDesign;

            TreeNode rootNode = treeViewData.Nodes[0];
            foreach (TreeNode parentNode in rootNode.Nodes)
            {
                if (parentNode.Text == "重心包线设计结果列表")
                {
                    foreach (TreeNode node in parentNode.Nodes)
                    {
                        if (index.ToString() == node.ToolTipText)
                        {
                            treeViewData.SelectedNode = node;
                            selNode = node;
                        }
                    }
                }

            }
        }

        private Model.CoreEnvelopeArithmetic GetCoreEnvelopeDesign()
        {
            Model.CoreEnvelopeArithmetic coreEnvelope = null;

            if (lstCoreEnvelopeDesign != null && lstCoreEnvelopeDesign.Count > 0)
            {
                for (int i = 0; i < lstCoreEnvelopeDesign.Count; i++)
                {
                    if (selNode != null && selNode.Level == 2 && selNode.ToolTipText == i.ToString())
                    {
                        coreEnvelope = new CoreEnvelopeArithmetic();
                        coreEnvelope = lstCoreEnvelopeDesign[i];
                    }
                }
            }

            return coreEnvelope;
        }

        /// <summary>
        /// 设置重量设计结果
        /// </summary>
        public void SetCoreEnvelopeDesignResult(Model.CoreEnvelopeArithmetic coreEnvelope)
        {
            if (coreEnvelope != null)
            {
                tabPageEnvelopeDesign.Text = coreEnvelope.DataName;
                txtCoreEnvelopeDataName.Text = coreEnvelope.DataName;

                //绑定重心包线数据
                BindCoreEnvelopeData(coreEnvelope);

                //绑定图形
                DataTable table = GetTableCoreEnvelopteData(coreEnvelope);
                CoreEnvelopeDisplayInPicture(table, zedGraphControlCore);

                BindCoreEnvelopeTreeData();
            }
        }

        /// <summary>
        /// 绑定重心包线数据
        /// </summary>
        private void BindCoreEnvelopeData(Model.CoreEnvelopeArithmetic coreEnvelope)
        {
            SetCoreEnvelopeDesignGridView(coreEnvelope);
            DataTable table = GetTableCoreEnvelopteData(coreEnvelope);

            if (table.Rows.Count > 0)
            {
                for (int i = 1; i < gridCoreEnvelopeDesign.ColumnCount; i++)
                {
                    string[] strArray = table.Rows[0][i - 1].ToString().Split(',');
                    gridCoreEnvelopeDesign.Rows[0].Cells[i].Value = strArray[0];
                    if (strArray.Length > 1)
                    {
                        gridCoreEnvelopeDesign.Rows[1].Cells[i].Value = strArray[1];
                    }
                    else
                    {
                        gridCoreEnvelopeDesign.Rows[1].Cells[i].Value = 0;
                    }
                }
            }
        }

        private DataTable GetCoreEnvelopeTableStuctre(List<string> lstCoreEnvelope)
        {
            DataTable table = new DataTable();

            if (lstCoreEnvelope != null && lstCoreEnvelope.Count > 0)
            {
                foreach (string str in lstCoreEnvelope)
                {
                    table.Columns.Add(str);
                }
            }

            return table;
        }

        private DataTable GetTableCoreEnvelopteData(Model.CoreEnvelopeArithmetic coreEnvelope)
        {
            DataTable table = GetCoreEnvelopeTableStuctre(GetListCoreEnvelopeDesign(coreEnvelope));
            double dXValue = 0;
            double dYValue = 0;

            if (table.Columns.Count > 0)
            {
                DataRow dr = table.NewRow();
                if (gridCoreEnvelopeDesign.Rows[0].Cells[1].Value == null)
                {
                    foreach (Model.NodeFormula formula in coreEnvelope.FormulaList)
                    {
                        dXValue = Math.Round(formula.XFormula.Value, digit);
                        dYValue = Math.Round(formula.YFormula.Value, digit);
                        dr[formula.NodeName] = dXValue.ToString() + "," + dYValue.ToString();
                    }
                }
                else
                {
                    for (int i = 1; i < gridCoreEnvelopeDesign.ColumnCount; i++)
                    {
                        dXValue = Convert.ToDouble(gridCoreEnvelopeDesign.Rows[0].Cells[i].Value.ToString());
                        dXValue = Math.Round(dXValue, digit);

                        dYValue = Convert.ToDouble(gridCoreEnvelopeDesign.Rows[1].Cells[i].Value.ToString());
                        dYValue = Math.Round(dYValue, digit);

                        dr[gridCoreEnvelopeDesign.Columns[i].HeaderText] = dXValue.ToString() + "," + dYValue.ToString();
                    }
                }
                table.Rows.Add(dr);
            }
            return table;
        }

        private List<string> GetListCoreEnvelopeDesign(Model.CoreEnvelopeArithmetic coreEnvelope)
        {
            List<string> lstCoreEnvelope = new List<string>();
            foreach (Model.NodeFormula formula in coreEnvelope.FormulaList)
            {
                lstCoreEnvelope.Add(formula.NodeName);
            }
            return lstCoreEnvelope;
        }

        private void SetCoreEnvelopeDesignGridView(Model.CoreEnvelopeArithmetic coreEnvelope)
        {
            gridCoreEnvelopeDesign.Columns.Clear();
            gridCoreEnvelopeDesign.DataSource = null;

            //第一列
            DataGridViewTextBoxColumn firstColumn = new DataGridViewTextBoxColumn();
            firstColumn.HeaderText = string.Empty;
            firstColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridCoreEnvelopeDesign.Columns.Add(firstColumn);

            List<string> lstCoreEnvelope = GetListCoreEnvelopeDesign(coreEnvelope);

            for (int i = 0; i < lstCoreEnvelope.Count; i++)
            {
                DataGridViewTextBoxColumn txtColumn = new DataGridViewTextBoxColumn();
                txtColumn.DataPropertyName = lstCoreEnvelope[i];
                txtColumn.HeaderText = lstCoreEnvelope[i];
                txtColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                gridCoreEnvelopeDesign.Columns.Add(txtColumn);

                gridCoreEnvelopeDesign.Columns[i + 1].ValueType = System.Type.GetType("System.Decimal");
            }

            if (gridCoreEnvelopeDesign.ColumnCount > 0)
            {
                //添加行
                gridCoreEnvelopeDesign.Rows.Add(2);
            }

            gridCoreEnvelopeDesign.Rows[0].Cells[0].Value = "横坐标(毫米)";
            gridCoreEnvelopeDesign.Rows[1].Cells[0].Value = "纵坐标(千克)";
            gridCoreEnvelopeDesign.Columns[0].ReadOnly = true;
        }

        /// <summary>
        /// 绑定重心包线treeview数据
        /// </summary>
        private void BindCoreEnvelopeTreeData()
        {
            treeViewCoreEnvelope.Nodes.Clear();
            string strTitle = string.Empty;
            double dValue = 0;

            TreeNode node = new TreeNode("重心包线");
            treeViewCoreEnvelope.Nodes.Add(node);
            for (int i = 1; i < gridCoreEnvelopeDesign.ColumnCount; i++)
            {
                TreeNode parentNode = new TreeNode(gridCoreEnvelopeDesign.Columns[i].HeaderText);
                node.Nodes.Add(parentNode);

                dValue = (gridCoreEnvelopeDesign.Rows[0].Cells[i].Value is DBNull || gridCoreEnvelopeDesign.Rows[0].Cells[i].Value.ToString() == string.Empty)
                    ? 0 : Convert.ToDouble(gridCoreEnvelopeDesign.Rows[0].Cells[i].Value);
                dValue = Math.Round(dValue, digit);
                strTitle = "横坐标：[" + dValue.ToString() + "毫米]";
                TreeNode xNode = new TreeNode(strTitle);
                parentNode.Nodes.Add(xNode);

                dValue = (gridCoreEnvelopeDesign.Rows[1].Cells[i].Value is DBNull || gridCoreEnvelopeDesign.Rows[0].Cells[i].Value.ToString() == string.Empty)
                    ? 0 : Convert.ToDouble(gridCoreEnvelopeDesign.Rows[1].Cells[i].Value);
                dValue = Math.Round(dValue, digit);
                strTitle = "纵坐标：[" + dValue.ToString() + "千克]";
                TreeNode yNode = new TreeNode(strTitle);
                parentNode.Nodes.Add(yNode);
            }
            treeViewCoreEnvelope.ExpandAll();
        }

        /// <summary>
        /// 获取重心包线设计结果内容
        /// </summary>
        /// <returns></returns>
        private List<string> GetEnvelopeDesignResultContent()
        {
            List<string> lstContent = new List<string>();

            List<Model.CoreEnvelopeArithmetic> lstResult = designProjectData.lstCoreEnvelopeDesign;
            if (lstResult != null && lstResult.Count > 0)
            {
                string str = string.Empty;
                lstContent.Add(CommonFunction.mStrModifyToString8(2) + "<重心包线设计结果列表>");

                foreach (CoreEnvelopeArithmetic coreEnvelope in lstResult)
                {
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<重心包线设计结果>");

                    str = CommonFunction.mStrModifyToString8(4) + "<重心包线设计数据名称>" + coreEnvelope.DataName + "</重心包线设计数据名称>";
                    lstContent.Add(str);

                    List<Model.WeightParameter> lstPara = coreEnvelope.GetParaList();
                    if (lstPara != null && lstPara.Count > 0)
                    {
                        lstContent.Add(CommonFunction.mStrModifyToString8(4) + "<参数列表>");
                        for (int j = 0; j < lstPara.Count; j++)
                        {
                            lstContent.Add(CommonFunction.mStrModifyToString8(5) + "<参数>");

                            str = CommonFunction.mStrModifyToString8(6) + "<参数名称>" + lstPara[j].ParaName + "</参数名称>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(6) + "<参数单位>" + lstPara[j].ParaUnit + "</参数单位>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(6) + "<参数类型>" + lstPara[j].ParaType.ToString() + "</参数类型>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(6) + "<参数数值>" + lstPara[j].ParaValue + "</参数数值>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(6) + "<参数备注>" + lstPara[j].ParaRemark + "</参数备注>";
                            lstContent.Add(str);

                            lstContent.Add(CommonFunction.mStrModifyToString8(5) + "</参数>");
                        }
                        lstContent.Add(CommonFunction.mStrModifyToString8(4) + "</参数列表>");
                    }

                    //---------------------重心包线算法数据-------------------------------------//
                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "<重心包线算法>");

                    str = CommonFunction.mStrModifyToString8(5) + "<重心算法名称>" + coreEnvelope.Name + "</重心算法名称>";
                    lstContent.Add(str);
                    str = CommonFunction.mStrModifyToString8(5) + "<重心算法创建时间>" + coreEnvelope.CreateTime + "</重心算法创建时间>";
                    lstContent.Add(str);
                    str = CommonFunction.mStrModifyToString8(5) + "<重心算法最后修改时间>" + coreEnvelope.LastModifyTime + "</重心算法最后修改时间>";
                    lstContent.Add(str);

                    lstContent.Add(CommonFunction.mStrModifyToString8(5) + "<重心坐标列表>");


                    List<Model.NodeFormula> lstNodeFormula = coreEnvelope.FormulaList;
                    for (int m = 0; m < lstNodeFormula.Count; m++)
                    {
                        lstContent.Add(CommonFunction.mStrModifyToString8(6) + "<重心坐标>");

                        str = CommonFunction.mStrModifyToString8(7) + "<重心坐标点名称>" + lstNodeFormula[m].NodeName + "</重心坐标点名称>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(7) + "<X轴单位>" + "毫米" + "</X轴单位>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(7) + "<Y轴单位>" + "千克" + "</Y轴单位>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(7) + "<X轴数值>" + lstNodeFormula[m].XFormula.Value.ToString() + "</X轴数值>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(7) + "<Y轴数值>" + lstNodeFormula[m].YFormula.Value.ToString() + "</Y轴数值>";
                        lstContent.Add(str);

                        lstContent.Add(CommonFunction.mStrModifyToString8(6) + "</重心坐标>");
                    }

                    lstContent.Add(CommonFunction.mStrModifyToString8(5) + "</重心坐标列表>");

                    //重心包线计算公式列表
                    if (lstNodeFormula != null && lstNodeFormula.Count > 0)
                    {
                        lstContent.Add(CommonFunction.mStrModifyToString8(5) + "<节点列表>");
                        foreach (Model.NodeFormula data in lstNodeFormula)
                        {
                            lstContent.Add(CommonFunction.mStrModifyToString8(6) + "<节点>");
                            str = CommonFunction.mStrModifyToString8(7) + "<节点名称>" + data.NodeName + "</节点名称>";
                            lstContent.Add(str);

                            lstContent.Add(CommonFunction.mStrModifyToString8(7) + "<X轴坐标>");
                            str = CommonFunction.mStrModifyToString8(8) + "<公式>" + data.XFormula.Formula + "</公式>";
                            lstContent.Add(str);

                            List<Model.WeightParameter> lstXParaData = data.XFormula.ParaList;

                            if (lstXParaData != null && lstXParaData.Count > 0)
                            {
                                lstContent.Add(CommonFunction.mStrModifyToString8(8) + "<参数列表>");

                                foreach (Model.WeightParameter para in lstXParaData)
                                {
                                    lstContent.Add(CommonFunction.mStrModifyToString8(9) + "<参数>");

                                    str = CommonFunction.mStrModifyToString8(10) + "<参数名称>" + para.ParaName + "</参数名称>";
                                    lstContent.Add(str);
                                    str = CommonFunction.mStrModifyToString8(10) + "<参数单位>" + para.ParaUnit + "</参数单位>";
                                    lstContent.Add(str);
                                    str = CommonFunction.mStrModifyToString8(10) + "<参数类型>" + para.ParaType.ToString() + "</参数类型>";
                                    lstContent.Add(str);
                                    str = CommonFunction.mStrModifyToString8(10) + "<参数数值>" + para.ParaValue.ToString() + "</参数数值>";
                                    lstContent.Add(str);
                                    str = CommonFunction.mStrModifyToString8(10) + "<参数备注>" + para.ParaRemark + "</参数备注>";
                                    lstContent.Add(str);

                                    lstContent.Add(CommonFunction.mStrModifyToString8(9) + "</参数>");
                                }

                                lstContent.Add(CommonFunction.mStrModifyToString8(8) + "</参数列表>");
                            }
                            str = CommonFunction.mStrModifyToString8(8) + "<结果数值>" + data.XFormula.Value.ToString() + "</结果数值>";
                            lstContent.Add(str);
                            lstContent.Add(CommonFunction.mStrModifyToString8(7) + "</X轴坐标>");

                            lstContent.Add(CommonFunction.mStrModifyToString8(7) + "<Y轴坐标>");
                            str = CommonFunction.mStrModifyToString8(8) + "<公式>" + data.YFormula.Formula + "</公式>";
                            lstContent.Add(str);

                            List<Model.WeightParameter> lstYParaData = data.YFormula.ParaList;

                            if (lstYParaData != null && lstYParaData.Count > 0)
                            {
                                lstContent.Add(CommonFunction.mStrModifyToString8(8) + "<参数列表>");

                                foreach (Model.WeightParameter para in lstYParaData)
                                {
                                    lstContent.Add(CommonFunction.mStrModifyToString8(9) + "<参数>");

                                    str = CommonFunction.mStrModifyToString8(10) + "<参数名称>" + para.ParaName + "</参数名称>";
                                    lstContent.Add(str);
                                    str = CommonFunction.mStrModifyToString8(10) + "<参数单位>" + para.ParaUnit + "</参数单位>";
                                    lstContent.Add(str);
                                    str = CommonFunction.mStrModifyToString8(10) + "<参数类型>" + para.ParaType.ToString() + "</参数类型>";
                                    lstContent.Add(str);
                                    str = CommonFunction.mStrModifyToString8(10) + "<参数数值>" + para.ParaValue.ToString() + "</参数数值>";
                                    lstContent.Add(str);
                                    str = CommonFunction.mStrModifyToString8(10) + "<参数备注>" + para.ParaRemark + "</参数备注>";
                                    lstContent.Add(str);

                                    lstContent.Add(CommonFunction.mStrModifyToString8(9) + "</参数>");
                                }

                                lstContent.Add(CommonFunction.mStrModifyToString8(8) + "</参数列表>");
                            }

                            str = CommonFunction.mStrModifyToString8(8) + "<结果数值>" + data.YFormula.Value.ToString() + "</结果数值>";
                            lstContent.Add(str);
                            lstContent.Add(CommonFunction.mStrModifyToString8(7) + "</Y轴坐标>");
                            lstContent.Add(CommonFunction.mStrModifyToString8(6) + "</节点>");
                        }
                        lstContent.Add(CommonFunction.mStrModifyToString8(5) + "</节点列表>");
                    }
                    str = CommonFunction.mStrModifyToString8(5) + "<重心算法备注>" + coreEnvelope.Remark + "</重心算法备注>";
                    lstContent.Add(str);

                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "</重心包线算法>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "</重心包线设计结果>");
                }

                lstContent.Add(CommonFunction.mStrModifyToString8(2) + "</重心包线设计结果列表>");
            }
            return lstContent;
        }

        private static DataTable GetTableCoreEnvelopeDesignResultStruct()
        {
            DataTable table = new DataTable();

            table.Columns.Add("Core_Name");
            table.Columns.Add("Core_XUnit");
            table.Columns.Add("Core_YUnit");
            table.Columns.Add("Core_XValue");
            table.Columns.Add("Core_YValue");

            return table;
        }

        public static DataTable GetCoreEnvelopeDesignResultTable(DataGridView gridResult)
        {
            DataTable table = GetTableCoreEnvelopeDesignResultStruct();
            double dXValue = 0;
            double dYValue = 0;

            for (int m = 1; m < gridResult.ColumnCount; m++)
            {
                dXValue = gridResult.Rows[0].Cells[m].Value is DBNull
                    ? 0 : Convert.ToDouble(gridResult.Rows[0].Cells[m].Value);
                dYValue = gridResult.Rows[0].Cells[m].Value is DBNull
                    ? 0 : Convert.ToDouble(gridResult.Rows[1].Cells[m].Value);

                DataRow dr = table.NewRow();
                dr["Core_Name"] = gridResult.Columns[m].HeaderText;
                dr["Core_XUnit"] = "毫米";
                dr["Core_YUnit"] = "千克";
                dr["Core_XValue"] = dXValue;
                dr["Core_YValue"] = dYValue;

                table.Rows.Add(dr);
            }
            return table;
        }

        public static List<string> GetCoreEnvelopeDesignResultExcleCloumn()
        {
            List<string> lstTitle = new List<string>();

            lstTitle.Add("重心坐标点名称");
            lstTitle.Add("横轴单位");
            lstTitle.Add("纵轴单位");
            lstTitle.Add("横轴数值");
            lstTitle.Add("纵轴数值");

            return lstTitle;
        }

        public static List<string> GetCoreEnvelopeDesignFileContent(DataGridView gridResult)
        {
            string strXValue = string.Empty;
            string strYValue = string.Empty;

            List<string> lstContent = new List<string>();

            if (gridResult.Rows.Count > 0)
            {
                lstContent.Insert(0, "<?xml version=" + "\"1.0\"" + " encoding=" + "\"gb2312\"" + " ?> ");

                lstContent.Add("<重心数据>");
                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "<重心坐标列表>");
                for (int i = 1; i < gridResult.ColumnCount; i++)
                {

                    lstContent.Add(CommonFunction.mStrModifyToString8(2) + "<重心坐标>");

                    strXValue = gridResult.Rows[0].Cells[i].Value is DBNull ? "0" : gridResult.Rows[0].Cells[i].Value.ToString();
                    strYValue = gridResult.Rows[1].Cells[i].Value is DBNull ? "0" : gridResult.Rows[1].Cells[i].Value.ToString();

                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<重心坐标点名称>" + gridResult.Columns[i].HeaderText + "</重心坐标点名称>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<横轴单位>毫米</横轴单位>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<纵轴单位>千克</纵轴单位>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<横轴数值>" + strXValue + "</横轴数值>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<纵轴数值>" + strYValue + "</纵轴数值>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(2) + "</重心坐标>");
                }

                lstContent.Add(CommonFunction.mStrModifyToString8(1) + "</重心坐标列表>");
                lstContent.Add("</重心数据>");
            }

            return lstContent;
        }

        /// <summary>
        /// 修改重心包线设计结果
        /// </summary>
        /// <param name="envelope"></param>
        private void UpdateCoreEnvelopeData(CoreEnvelopeArithmetic envelope)
        {
            List<NodeFormula> lstNodeFormula = envelope.FormulaList;
            if (gridCoreEnvelopeDesign.Rows.Count > 0 && lstNodeFormula.Count > 0)
            {
                for (int i = 1; i < gridCoreEnvelopeDesign.ColumnCount; i++)
                {
                    for (int j = 0; j < lstNodeFormula.Count; j++)
                    {
                        if (gridCoreEnvelopeDesign.Columns[i].HeaderText == lstNodeFormula[j].NodeName)
                        {
                            lstNodeFormula[j].XFormula.Value = Convert.ToDouble(gridCoreEnvelopeDesign.Rows[0].Cells[i].Value);
                            lstNodeFormula[j].YFormula.Value = Convert.ToDouble(gridCoreEnvelopeDesign.Rows[1].Cells[i].Value);
                        }
                    }
                }
                envelope.FormulaList = lstNodeFormula;
            }
        }

        private void CoreEnvelopeDisplayInPicture(DataTable table, ZedGraphControl zedGraphControlCore)
        {
            GraphPane myPane = zedGraphControlCore.GraphPane;

            //清除原来的图形
            myPane.CurveList.Clear();
            myPane.GraphObjList.Clear();

            //设置网格线可见
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            //设置网格线颜色
            myPane.XAxis.MajorGrid.Color = Color.Chocolate;
            myPane.YAxis.MajorGrid.Color = Color.Chocolate;

            //设置网格线形式
            myPane.XAxis.MajorGrid.DashOff = 1;
            myPane.YAxis.MajorGrid.DashOff = 1;
            myPane.XAxis.MajorGrid.DashOn = 4;
            myPane.YAxis.MajorGrid.DashOn = 4;

            //设置显示坐标
            myPane.XAxis.Scale.IsUseTenPower = false;
            myPane.YAxis.Scale.IsUseTenPower = false;
            myPane.XAxis.Scale.MagAuto = true;
            myPane.YAxis.Scale.MagAuto = true;

            myPane.Title.Text = "重心包线";
            myPane.XAxis.Title.Text = "长度(毫米)";
            myPane.YAxis.Title.Text = "重量(千克)";

            PointPairList listCur = new PointPairList();

            double x = 0, y = 0;
            double x1 = 0, y1 = 0;
            string strTitle = string.Empty;
            string strValue = string.Empty;
            for (int j = 0; j < table.Columns.Count; j++)
            {
                string[] strArray = table.Rows[0][j].ToString().Split(',');
                x = strArray[0] == string.Empty ? 0 : Convert.ToDouble(strArray[0]);
                x = Math.Round(x, picDigit);
                if (strArray.Length > 1)
                {
                    y = strArray[1] == string.Empty ? 0 : Convert.ToDouble(strArray[1]);
                    y = Math.Round(y, picDigit);
                }

                listCur.Add(x, y);


                if (j == table.Columns.Count - 1)
                {
                    string[] strArrayValue = table.Rows[0][0].ToString().Split(',');
                    x1 = strArrayValue[0] == string.Empty ? 0 : Convert.ToDouble(strArrayValue[0]);
                    x1 = Math.Round(x1, picDigit);
                    if (strArrayValue.Length > 1)
                    {
                        y1 = strArrayValue[1] == string.Empty ? 0 : Convert.ToDouble(strArrayValue[1]);
                        y1 = Math.Round(y1, picDigit);
                    }
                    listCur.Add(x1, y1);
                }

                //显示名称
                strTitle = table.Columns[j].ColumnName;

                // 创建一个阴影区域，看起来有渐变
                TextObj text = new TextObj(strTitle, x, y,
                    CoordType.AxisXYScale, AlignH.Right, AlignV.Center);
                //是否有背景
                text.FontSpec.Fill.IsVisible = false;
                //是否有边框
                text.FontSpec.Border.IsVisible = false;
                //文字是否粗体
                text.FontSpec.IsBold = false;
                //文字是否斜体
                text.FontSpec.IsItalic = false;
                //填充
                myPane.GraphObjList.Add(text);
            }
            LineItem myCurveCur = myPane.AddCurve(string.Empty, listCur, Color.Blue, SymbolType.Default);
            myCurveCur.Symbol.Size = 6;
            myCurveCur.Symbol.Fill = new Fill(Color.Blue, Color.Blue);
            myCurveCur.Symbol.Border.IsVisible = true;
            myCurveCur.Line.IsVisible = true;

            zedGraphControlCore.AxisChange();
            zedGraphControlCore.Refresh();
        }

        #endregion

        #region 重心包线剪裁结果

        public void SetCoreEnvelopeCutTab()
        {
            if (tabControlWork.TabPages.Contains(tabPageEnvelopeCut) == false)
            {
                tabControlWork.TabPages.Add(tabPageEnvelopeCut);
            }

            tabPageEnvelopeCut.ToolTipText = selNode.ToolTipText;
            CoreEnvelopeCutResultData cutData = GetCoreEnvelopeCutResult();
            tabControlWork.SelectedTab = tabPageEnvelopeCut;
            SetCoreEnvelopeCutResult(cutData);
        }

        public void SetCoreEnvelopeCutTab(CoreEnvelopeCutResultData cutData, int index)
        {
            if (tabControlWork.TabPages.Contains(tabPageEnvelopeCut) == false)
            {
                tabControlWork.TabPages.Add(tabPageEnvelopeCut);
            }

            tabPageEnvelopeCut.ToolTipText = index.ToString();
            tabControlWork.SelectedTab = tabPageEnvelopeCut;
            SetCoreEnvelopeCutResult(cutData);

            TreeNode rootNode = treeViewData.Nodes[0];
            foreach (TreeNode parentNode in rootNode.Nodes)
            {
                if (parentNode.Text == "重心包线剪裁结果列表")
                {
                    foreach (TreeNode node in parentNode.Nodes)
                    {
                        if (index.ToString() == node.ToolTipText)
                        {
                            treeViewData.SelectedNode = node;
                            selNode = node;
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 获取重心包线剪裁结果
        /// </summary>
        /// <returns></returns>
        private List<Model.CoreEnvelopeCutResultData> GetCoreEnvelopteCutData(XmlDocument doc)
        {
            string path = string.Empty;
            XmlNode node = null;

            List<Model.CoreEnvelopeCutResultData> lstCoreEnvelopeCutData = new List<CoreEnvelopeCutResultData>();

            path = "PRJ/重量重心设计工程/重心包线剪裁结果列表";
            node = doc.SelectSingleNode(path);
            if (node == null)
            {
                return lstCoreEnvelopeCutData;
            }
            XmlNodeList cutNodeList = node.ChildNodes;

            foreach (XmlNode cutNode in cutNodeList)
            {
                Model.CoreEnvelopeCutResultData cutData = new CoreEnvelopeCutResultData(0);

                //重心包线剪裁数据名称
                cutData.cutResultName = cutNode.ChildNodes[0].InnerText;
                //剪裁方式
                XmlNode cutTypeNode = cutNode.SelectSingleNode("重心包线剪裁数据剪裁方式");
                if (cutTypeNode != null)
                {
                    cutData.nCutType = Convert.ToInt32(cutTypeNode.InnerText);
                }

                //离散设置
                XmlNodeList setNodeList = cutNode.SelectSingleNode("离散设置") == null
                    ? null : cutNode.SelectSingleNode("离散设置").ChildNodes;
                if (setNodeList != null && setNodeList.Count > 0)
                {
                    cutData.nDiscreteCircularPtCount = setNodeList.Count >= 1 ? Convert.ToInt32(setNodeList[0].InnerText) : 0;
                    cutData.nDiscreteRadialPtCount = setNodeList.Count >= 2 ? Convert.ToInt32(setNodeList[1].InnerText) : 0;
                    cutData.fDiscreteRadialFirstLen = setNodeList.Count >= 3 ? Convert.ToDouble(setNodeList[2].InnerText) : 0;
                    cutData.fDiscreteRadialRatio = setNodeList.Count >= 4 ? Convert.ToDouble(setNodeList[3].InnerText) : 0;
                    cutData.fRatioWidthVsHeight = setNodeList.Count >= 5 ? Convert.ToDouble(setNodeList[4].InnerText) : 0;
                }

                //基础重心包线数据
                XmlNodeList basicNodeList = cutNode.SelectSingleNode("基础重心包线数据/重心坐标列表") == null
                    ? null : cutNode.SelectSingleNode("基础重心包线数据/重心坐标列表").ChildNodes;
                List<CorePointData> lstBasicCorePoint = new List<CorePointData>();
                if (basicNodeList != null && basicNodeList.Count > 0)
                {
                    foreach (XmlNode childNode in basicNodeList)
                    {
                        string strPtName = childNode.ChildNodes[0].InnerText;
                        CorePointData pt = new CorePointData(strPtName);
                        pt.pointXValue = Convert.ToDouble(childNode.ChildNodes[3].InnerText);
                        pt.pointYValue = Convert.ToDouble(childNode.ChildNodes[4].InnerText);

                        lstBasicCorePoint.Add(pt);
                    }
                }
                cutData.lstBasicCoreEnvelope = lstBasicCorePoint;

                //离散重心数据
                XmlNodeList discreteCoreNodeList = cutNode.SelectSingleNode("离散重心数据/重心坐标列表") == null
                    ? null : cutNode.SelectSingleNode("离散重心数据/重心坐标列表").ChildNodes;
                List<CorePointData> lstDiscreteCoreData = new List<CorePointData>();
                if (discreteCoreNodeList != null && discreteCoreNodeList.Count > 0)
                {
                    foreach (XmlNode childNode in discreteCoreNodeList)
                    {
                        string strPtName = childNode.ChildNodes[0].InnerText;
                        CorePointData pt = new CorePointData(strPtName);
                        pt.pointXValue = Convert.ToDouble(childNode.ChildNodes[3].InnerText);
                        pt.pointYValue = Convert.ToDouble(childNode.ChildNodes[4].InnerText);

                        lstDiscreteCoreData.Add(pt);
                    }
                }
                cutData.lstDiscreteCore = lstDiscreteCoreData;

                //燃油重心数据
                XmlNodeList fuelCoreNodeList = cutNode.SelectSingleNode("燃油重心数据/重心坐标列表") == null
                ? null : cutNode.SelectSingleNode("燃油重心数据/重心坐标列表").ChildNodes;
                List<CorePointData> lstFuelCorePoint = new List<CorePointData>();
                if (fuelCoreNodeList != null && fuelCoreNodeList.Count > 0)
                {
                    foreach (XmlNode childNode in fuelCoreNodeList)
                    {
                        string strPtName = childNode.ChildNodes[0].InnerText;
                        CorePointData pt = new CorePointData(strPtName);
                        pt.pointXValue = Convert.ToDouble(childNode.ChildNodes[3].InnerText);
                        pt.pointYValue = Convert.ToDouble(childNode.ChildNodes[4].InnerText);

                        lstFuelCorePoint.Add(pt);
                    }
                }
                cutData.lstFuelCore = lstFuelCorePoint;

                //离散点评估数据
                XmlNodeList coreEvaluationNodeList = cutNode.SelectSingleNode("离散评估数据/离散评估数据列表") == null
                    ? null : cutNode.SelectSingleNode("离散评估数据/离散评估数据列表").ChildNodes;
                if (coreEvaluationNodeList != null && coreEvaluationNodeList.Count > 0)
                {
                    cutData.lstCoreEvaluation = new List<int>();
                    foreach (XmlNode childNode in coreEvaluationNodeList)
                    {
                        cutData.lstCoreEvaluation.Add(Convert.ToInt32(childNode.InnerText));
                    }
                }

                //剪裁重心包线数据
                XmlNodeList cutCoreNodeList = cutNode.SelectSingleNode("剪裁重心数据/重心坐标列表") == null
                    ? null : cutNode.SelectSingleNode("剪裁重心数据/重心坐标列表").ChildNodes;
                List<CorePointData> lstCutCorePoint = new List<CorePointData>();
                if (cutCoreNodeList != null && cutCoreNodeList.Count > 0)
                {
                    foreach (XmlNode childNode in cutCoreNodeList)
                    {
                        string strPtName = childNode.ChildNodes[0].InnerText;
                        CorePointData pt = new CorePointData(strPtName);
                        pt.pointXValue = Convert.ToDouble(childNode.ChildNodes[3].InnerText);
                        pt.pointYValue = Convert.ToDouble(childNode.ChildNodes[4].InnerText);

                        lstCutCorePoint.Add(pt);
                    }
                }
                cutData.lstCutEnvelopeCore = lstCutCorePoint;

                lstCoreEnvelopeCutData.Add(cutData);
            }

            return lstCoreEnvelopeCutData;
        }

        private List<string> GetCoreEnvelopeCutContent()
        {
            List<string> lstContent = new List<string>();

            List<CoreEnvelopeCutResultData> lstResult = designProjectData.lstCutResultData;
            if (lstResult != null && lstResult.Count > 0)
            {
                string str = string.Empty;
                lstContent.Add(CommonFunction.mStrModifyToString8(2) + "<重心包线剪裁结果列表>");

                foreach (CoreEnvelopeCutResultData cutResult in lstResult)
                {
                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "<重心包线剪裁结果>");

                    str = CommonFunction.mStrModifyToString8(4) + "<重心包线剪裁数据名称>" + cutResult.cutResultName + "</重心包线剪裁数据名称>";
                    lstContent.Add(str);

                    //剪裁方式
                    str = CommonFunction.mStrModifyToString8(4) + "<重心包线剪裁数据剪裁方式>" + cutResult.nCutType.ToString() + "</重心包线剪裁数据剪裁方式>";
                    lstContent.Add(str);

                    //离散设置
                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "<离散设置>");

                    str = CommonFunction.mStrModifyToString8(5) + "<周向离散个数>" + cutResult.nDiscreteCircularPtCount.ToString() + "</周向离散个数>";
                    lstContent.Add(str);
                    str = CommonFunction.mStrModifyToString8(5) + "<颈向离散个数>" + cutResult.nDiscreteRadialPtCount.ToString() + "</颈向离散个数>";
                    lstContent.Add(str);
                    str = CommonFunction.mStrModifyToString8(5) + "<径向离散首段长度>" + cutResult.fDiscreteRadialFirstLen.ToString() + "</径向离散首段长度>";
                    lstContent.Add(str);
                    str = CommonFunction.mStrModifyToString8(5) + "<径向离散梯度>" + cutResult.fDiscreteRadialRatio.ToString() + "</径向离散梯度>";
                    lstContent.Add(str);
                    str = CommonFunction.mStrModifyToString8(5) + "<宽高离散比例系数>" + cutResult.fRatioWidthVsHeight.ToString() + "</宽高离散比例系数>";
                    lstContent.Add(str);

                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "</离散设置>");

                    //基础重心包线数据
                    List<CorePointData> lstBasicCorePointData = cutResult.lstBasicCoreEnvelope;

                    if (cutResult.lstBasicCoreEnvelope != null && cutResult.lstBasicCoreEnvelope.Count > 0)
                    {
                        lstContent.Add(CommonFunction.mStrModifyToString8(4) + "<基础重心包线数据>");

                        lstContent.Add(CommonFunction.mStrModifyToString8(5) + "<重心坐标列表>");
                        for (int m = 0; m < lstBasicCorePointData.Count; m++)
                        {
                            lstContent.Add(CommonFunction.mStrModifyToString8(6) + "<重心坐标>");

                            str = CommonFunction.mStrModifyToString8(7) + "<重心坐标点名称>" + lstBasicCorePointData[m].pointName + "</重心坐标点名称>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(7) + "<X轴单位>毫米</X轴单位>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(7) + "<Y轴单位>千克</Y轴单位>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(7) + "<X轴数值>" + lstBasicCorePointData[m].pointXValue.ToString() + "</X轴数值>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(7) + "<Y轴数值>" + lstBasicCorePointData[m].pointYValue.ToString() + "</Y轴数值>";
                            lstContent.Add(str);

                            lstContent.Add(CommonFunction.mStrModifyToString8(6) + "</重心坐标>");
                        }
                        lstContent.Add(CommonFunction.mStrModifyToString8(5) + "</重心坐标列表>");
                        lstContent.Add(CommonFunction.mStrModifyToString8(4) + "</基础重心包线数据>");
                    }

                    //离散重心数据
                    List<CorePointData> lstDiscreteCoreData = cutResult.lstDiscreteCore;
                    if (cutResult.lstDiscreteCore != null && cutResult.lstDiscreteCore.Count > 0)
                    {
                        lstContent.Add(CommonFunction.mStrModifyToString8(4) + "<离散重心数据>");
                        lstContent.Add(CommonFunction.mStrModifyToString8(5) + "<重心坐标列表>");

                        foreach (CorePointData pt in lstDiscreteCoreData)
                        {
                            lstContent.Add(CommonFunction.mStrModifyToString8(6) + "<重心坐标>");

                            str = CommonFunction.mStrModifyToString8(7) + "<重心坐标点名称>" + pt.pointName + "</重心坐标点名称>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(7) + "<X轴单位>毫米</X轴单位>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(7) + "<Y轴单位>千克</Y轴单位>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(7) + "<X轴数值>" + pt.pointXValue.ToString() + "</X轴数值>";
                            lstContent.Add(str);
                            str = CommonFunction.mStrModifyToString8(7) + "<Y轴数值>" + pt.pointYValue.ToString() + "</Y轴数值>";
                            lstContent.Add(str);

                            lstContent.Add(CommonFunction.mStrModifyToString8(6) + "</重心坐标>");

                        }

                        lstContent.Add(CommonFunction.mStrModifyToString8(5) + "</重心坐标列表>");
                        lstContent.Add(CommonFunction.mStrModifyToString8(4) + "</离散重心数据>");

                    }

                    //燃油特性剪裁
                    if (cutResult.nCutType == 0)
                    {
                        List<CorePointData> lstFuelCorePointData = cutResult.lstFuelCore;

                        if (cutResult.lstFuelCore != null && cutResult.lstFuelCore.Count > 0)
                        {
                            lstContent.Add(CommonFunction.mStrModifyToString8(4) + "<燃油重心数据>");

                            lstContent.Add(CommonFunction.mStrModifyToString8(5) + "<重心坐标列表>");
                            for (int m = 0; m < lstFuelCorePointData.Count; m++)
                            {
                                lstContent.Add(CommonFunction.mStrModifyToString8(6) + "<重心坐标>");

                                str = CommonFunction.mStrModifyToString8(7) + "<重心坐标点名称>" + lstFuelCorePointData[m].pointName + "</重心坐标点名称>";
                                lstContent.Add(str);
                                str = CommonFunction.mStrModifyToString8(7) + "<X轴单位>毫米</X轴单位>";
                                lstContent.Add(str);
                                str = CommonFunction.mStrModifyToString8(7) + "<Y轴单位>千克</Y轴单位>";
                                lstContent.Add(str);
                                str = CommonFunction.mStrModifyToString8(7) + "<X轴数值>" + lstFuelCorePointData[m].pointXValue.ToString() + "</X轴数值>";
                                lstContent.Add(str);
                                str = CommonFunction.mStrModifyToString8(7) + "<Y轴数值>" + lstFuelCorePointData[m].pointYValue.ToString() + "</Y轴数值>";
                                lstContent.Add(str);

                                lstContent.Add(CommonFunction.mStrModifyToString8(6) + "</重心坐标>");
                            }
                            lstContent.Add(CommonFunction.mStrModifyToString8(5) + "</重心坐标列表>");
                            lstContent.Add(CommonFunction.mStrModifyToString8(4) + "</燃油重心数据>");

                        }
                    }
                    else
                    {
                        //评估数据
                        List<int> lstCoreEvaluation = cutResult.lstCoreEvaluation;

                        lstContent.Add(CommonFunction.mStrModifyToString8(4) + "<离散评估数据>");
                        if (lstCoreEvaluation != null && lstCoreEvaluation.Count > 0)
                        {
                            lstContent.Add(CommonFunction.mStrModifyToString8(5) + "<离散评估数据列表>");

                            foreach (int j in lstCoreEvaluation)
                            {
                                str = CommonFunction.mStrModifyToString8(6) + "<评估数据>" + j.ToString() + "</评估数据>";
                                lstContent.Add(str);

                            }
                            lstContent.Add(CommonFunction.mStrModifyToString8(5) + "</离散评估数据列表>");
                        }
                        lstContent.Add(CommonFunction.mStrModifyToString8(4) + "</离散评估数据>");
                    }

                    //剪裁重心数据
                    List<CorePointData> lstCutCorePointData = cutResult.lstCutEnvelopeCore;

                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "<剪裁重心数据>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(5) + "<重心坐标列表>");
                    for (int m = 0; m < lstCutCorePointData.Count; m++)
                    {
                        lstContent.Add(CommonFunction.mStrModifyToString8(6) + "<重心坐标>");

                        str = CommonFunction.mStrModifyToString8(7) + "<重心坐标点名称>" + lstCutCorePointData[m].pointName + "</重心坐标点名称>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(7) + "<X轴单位>毫米</X轴单位>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(7) + "<Y轴单位>千克</Y轴单位>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(7) + "<X轴数值>" + lstCutCorePointData[m].pointXValue.ToString() + "</X轴数值>";
                        lstContent.Add(str);
                        str = CommonFunction.mStrModifyToString8(7) + "<Y轴数值>" + lstCutCorePointData[m].pointYValue.ToString() + "</Y轴数值>";
                        lstContent.Add(str);

                        lstContent.Add(CommonFunction.mStrModifyToString8(6) + "</重心坐标>");
                    }
                    lstContent.Add(CommonFunction.mStrModifyToString8(5) + "</重心坐标列表>");
                    lstContent.Add(CommonFunction.mStrModifyToString8(4) + "</剪裁重心数据>");

                    lstContent.Add(CommonFunction.mStrModifyToString8(3) + "</重心包线剪裁结果>");
                }
                lstContent.Add(CommonFunction.mStrModifyToString8(2) + "</重心包线剪裁结果列表>");
            }
            return lstContent;
        }

        private Model.CoreEnvelopeCutResultData GetCoreEnvelopeCutResult()
        {
            CoreEnvelopeCutResultData cutData = null;

            for (int i = 0; i < lstCutResultData.Count; i++)
            {
                if (selNode != null && selNode.Level == 2 && selNode.ToolTipText == i.ToString())
                {
                    cutData = new CoreEnvelopeCutResultData(0);
                    cutData = lstCutResultData[i];
                }
            }
            return cutData;
        }

        private void BindCoreEnvelopeCutTreeData(string strType)
        {
            TreeView tree = null;

            if (strType == "basic")
            {
                tree = treeViewBasicCoreEnvelope;
            }
            if (strType == "cut")
            {
                tree = treeViewCutCoreEnvelope;
            }

            if (tree != null)
            {
                tree.Nodes.Clear();

                string strTitle = string.Empty;
                double dValue = 0;

                TreeNode node = new TreeNode("重心包线");
                tree.Nodes.Add(node);
                for (int i = 1; i < gridViewCoreEnvelopeCutReuslt.ColumnCount; i++)
                {
                    TreeNode parentNode = new TreeNode(gridViewCoreEnvelopeCutReuslt.Columns[i].HeaderText);
                    node.Nodes.Add(parentNode);

                    dValue = (gridViewCoreEnvelopeCutReuslt.Rows[0].Cells[i].Value is DBNull || gridViewCoreEnvelopeCutReuslt.Rows[0].Cells[i].Value.ToString() == string.Empty)
                        ? 0 : Convert.ToDouble(gridViewCoreEnvelopeCutReuslt.Rows[0].Cells[i].Value);
                    dValue = Math.Round(dValue, digit);
                    strTitle = "横坐标：[" + dValue.ToString() + "毫米]";
                    TreeNode xNode = new TreeNode(strTitle);
                    parentNode.Nodes.Add(xNode);

                    dValue = (gridViewCoreEnvelopeCutReuslt.Rows[1].Cells[i].Value is DBNull || gridViewCoreEnvelopeCutReuslt.Rows[0].Cells[i].Value.ToString() == string.Empty)
                        ? 0 : Convert.ToDouble(gridViewCoreEnvelopeCutReuslt.Rows[1].Cells[i].Value);
                    dValue = Math.Round(dValue, digit);
                    strTitle = "纵坐标：[" + dValue.ToString() + "千克]";
                    TreeNode yNode = new TreeNode(strTitle);
                    parentNode.Nodes.Add(yNode);
                }
                tree.ExpandAll();
            }
        }

        public void SetCoreEnvelopeCutResult(CoreEnvelopeCutResultData cutData)
        {
            if (cutData != null)
            {
                tabPageEnvelopeCut.Text = cutData.cutResultName;
                txtEnvelopeCutName.Text = cutData.cutResultName;

                string strType = GetCoreEnvelopeCutType();
                //绑定GridView
                BindCoreEnvelopeCutGridView(cutData, strType);

                //绑定重心包线数据
                BindCoreEnvelopeCutTreeData(strType);

                //剪裁重心包线图
                DataTable tableCut = GetTableCoreEnvelopeCutData(cutData, "cut");
                DisplayInPicture(tableCut, zedGraphControlCutCoreEnvelope);

                //绑定对比数据
                DataTable tableBasic = GetTableCoreEnvelopeCutData(cutData, "basic");
                DisplayCompareInPicture(cutData, zedGraphControlCoreCompare);
            }
        }

        private void BindCoreEnvelopeCutGridView(CoreEnvelopeCutResultData cutData, string strType)
        {
            //设置GridView
            SetCoreEnvelopeCutGridView(cutData, strType);
            DataTable table = GetTableCoreEnvelopeCutData(cutData, strType);

            if (table.Rows.Count > 0)
            {
                for (int i = 1; i < gridViewCoreEnvelopeCutReuslt.ColumnCount; i++)
                {
                    string[] strArray = table.Rows[0][i - 1].ToString().Split(',');
                    gridViewCoreEnvelopeCutReuslt.Rows[0].Cells[i].Value = strArray[0];
                    if (strArray.Length > 1)
                    {
                        gridViewCoreEnvelopeCutReuslt.Rows[1].Cells[i].Value = strArray[1];
                    }
                    else
                    {
                        gridViewCoreEnvelopeCutReuslt.Rows[1].Cells[i].Value = 0;
                    }
                }
            }
        }

        private DataTable GetTableCoreEnvelopeCutData(CoreEnvelopeCutResultData cutData, string strType)
        {
            DataTable table = GetTableStructCoreEnvelopeCut(cutData, strType);

            if (table.Columns.Count > 0)
            {
                //double dXValue = 0;
                //double dYValue = 0;

                List<CorePointData> lstCorePoint = new List<CorePointData>();
                if (strType == "basic")
                {
                    lstCorePoint = cutData.lstBasicCoreEnvelope;
                }
                if (strType == "cut")
                {
                    lstCorePoint = cutData.lstCutEnvelopeCore;
                }

                DataRow dr = table.NewRow();
                for (int i = 0; lstCorePoint != null && i < lstCorePoint.Count; i++)
                {
                    string strColumnName = "column" + (i + 1).ToString();
                    dr[strColumnName] = Math.Round(lstCorePoint[i].pointXValue, digit).ToString() + "," + Math.Round(lstCorePoint[i].pointYValue, digit).ToString();
                }
                //if (gridViewCoreEnvelopeCutReuslt.ColumnCount >= 2 && gridViewCoreEnvelopeCutReuslt.Rows[0].Cells[1].Value == null)
                //{
                //    for (int i = 0; i < lstCorePoint.Count; i++)
                //    {
                //        string strColumnName = "column" + (i + 1).ToString();
                //        dr[strColumnName] = Math.Round(lstCorePoint[i].pointXValue, digit).ToString() + "," + Math.Round(lstCorePoint[i].pointYValue, digit).ToString();
                //    }
                //}
                //else
                //{
                //    if (gridViewCoreEnvelopeCutReuslt.ColumnCount - 1 == lstCorePoint.Count)//?
                //    {
                //        for (int i = 1; i < gridViewCoreEnvelopeCutReuslt.ColumnCount; i++)
                //        {
                //            dXValue = Math.Round(Convert.ToDouble(gridViewCoreEnvelopeCutReuslt.Rows[0].Cells[i].Value.ToString()), digit);
                //            dYValue = Math.Round(Convert.ToDouble(gridViewCoreEnvelopeCutReuslt.Rows[1].Cells[i].Value.ToString()), digit);

                //            string strColumnName = "column" + i.ToString();
                //            dr[strColumnName] = dXValue.ToString() + "," + dYValue.ToString();
                //        }
                //    }
                //    else
                //    {
                //        for (int i = 0; i < lstCorePoint.Count; i++)
                //        {
                //            string strColumnName = "column" + (i + 1).ToString();
                //            dr[strColumnName] = Math.Round(lstCorePoint[i].pointXValue, digit).ToString() + "," + Math.Round(lstCorePoint[i].pointYValue, digit).ToString();
                //        }
                //    }
                //}

                table.Rows.Add(dr);
            }

            return table;
        }

        private DataTable GetTableStructCoreEnvelopeCut(CoreEnvelopeCutResultData cutData, string strType)
        {
            DataTable table = new DataTable();

            List<string> lstCoreEnvelope = GetListCoreEnvelopeCut(cutData, strType);

            if (lstCoreEnvelope != null && lstCoreEnvelope.Count > 0)
            {
                for (int i = 0; i < lstCoreEnvelope.Count; i++)
                {
                    DataColumn column = new DataColumn();
                    column.ColumnName = "column" + (i + 1).ToString();
                    column.Caption = lstCoreEnvelope[i];
                    table.Columns.Add(column);
                }
            }

            return table;
        }

        private void SetCoreEnvelopeCutGridView(Model.CoreEnvelopeCutResultData cutData, string strType)
        {
            if (cutData != null && strType != string.Empty)
            {
                gridViewCoreEnvelopeCutReuslt.Columns.Clear();
                gridViewCoreEnvelopeCutReuslt.DataSource = null;

                if (strType == "basic")
                {
                    //gridViewCoreEnvelopeCutReuslt.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                if (strType == "cut")
                {
                    //gridViewCoreEnvelopeCutReuslt.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                }

                //第一列
                DataGridViewTextBoxColumn firstColumn = new DataGridViewTextBoxColumn();
                firstColumn.HeaderText = string.Empty;
                firstColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                gridViewCoreEnvelopeCutReuslt.Columns.Add(firstColumn);

                List<string> lstCoreEnvelope = GetListCoreEnvelopeCut(cutData, strType);

                for (int i = 0; i < lstCoreEnvelope.Count; i++)
                {
                    DataGridViewTextBoxColumn txtColumn = new DataGridViewTextBoxColumn();
                    txtColumn.DataPropertyName = lstCoreEnvelope[i];
                    txtColumn.HeaderText = lstCoreEnvelope[i];
                    txtColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                    gridViewCoreEnvelopeCutReuslt.Columns.Add(txtColumn);

                    gridViewCoreEnvelopeCutReuslt.Columns[i + 1].ValueType = System.Type.GetType("System.Decimal");
                }

                //添加行
                if (gridViewCoreEnvelopeCutReuslt.ColumnCount > 0)
                {
                    gridViewCoreEnvelopeCutReuslt.Rows.Add(2);
                    gridViewCoreEnvelopeCutReuslt.Columns[0].ReadOnly = true;
                }

                if (gridViewCoreEnvelopeCutReuslt.Rows.Count > 0)
                {
                    gridViewCoreEnvelopeCutReuslt.Rows[0].Cells[0].Value = "横坐标(毫米)";
                    gridViewCoreEnvelopeCutReuslt.Rows[1].Cells[0].Value = "纵坐标(千克)";
                }
            }
        }

        private List<string> GetListCoreEnvelopeCut(Model.CoreEnvelopeCutResultData cutData, string strType)
        {
            List<string> lstCoreEnvelopeCut = new List<string>();

            if (cutData != null && strType != string.Empty)
            {
                if (strType == "basic")
                {
                    foreach (CorePointData pt in cutData.lstBasicCoreEnvelope)
                    {
                        lstCoreEnvelopeCut.Add(pt.pointName);
                    }
                }

                if (strType == "cut")
                {
                    foreach (CorePointData pt in cutData.lstCutEnvelopeCore)
                    {
                        lstCoreEnvelopeCut.Add(pt.pointName);
                    }
                }
            }

            return lstCoreEnvelopeCut;
        }

        private string GetCoreEnvelopeCutType()
        {
            string strType = string.Empty;
            if (tabControlWork.SelectedTab == tabPageEnvelopeCut)
            {
                if (tabControlCoreEnvelopeCut.SelectedTab == tabPageBasicCoreEnvelope)
                {
                    strType = "basic";
                }
                if (tabControlCoreEnvelopeCut.SelectedTab == tabPageCutCoreEnvelope)
                {
                    strType = "cut";
                }
            }
            return strType;
        }

        /// <summary>
        /// 绑定剪裁重心包线数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="zedGraphControlCore"></param>
        private void DisplayInPicture(DataTable table, ZedGraphControl zedGraphControlCore)
        {
            GraphPane myPane = zedGraphControlCore.GraphPane;

            //清除原来的图形
            myPane.CurveList.Clear();
            myPane.GraphObjList.Clear();

            //设置网格线可见
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            //设置网格线颜色
            myPane.XAxis.MajorGrid.Color = Color.Chocolate;
            myPane.YAxis.MajorGrid.Color = Color.Chocolate;

            //设置网格线形式
            myPane.XAxis.MajorGrid.DashOff = 1;
            myPane.YAxis.MajorGrid.DashOff = 1;
            myPane.XAxis.MajorGrid.DashOn = 4;
            myPane.YAxis.MajorGrid.DashOn = 4;

            //设置显示坐标
            myPane.XAxis.Scale.IsUseTenPower = false;
            myPane.YAxis.Scale.IsUseTenPower = false;
            myPane.XAxis.Scale.MagAuto = true;
            myPane.YAxis.Scale.MagAuto = true;

            myPane.Title.Text = "重心包线";
            myPane.XAxis.Title.Text = "长度(毫米)";
            myPane.YAxis.Title.Text = "重量(千克)";

            //AddMyCurve(zedGraphControlCore, Color.Black, table, SymbolType.Circle, "剪裁重心包线", false);
            // modified 2014 9 11
            AddMyCurve(zedGraphControlCore, Color.Black, table, SymbolType.Circle, "剪裁重心包线", true);


            zedGraphControlCore.RestoreScale(myPane);
            zedGraphControlCore.AxisChange();
            zedGraphControlCore.Refresh();
        }

        /// <summary>
        /// 绑定对比数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="zedGraphControlCore"></param>
        private void DisplayCompareInPicture(CoreEnvelopeCutResultData cutData, ZedGraphControl zedGraphControlCore)
        {
            DataTable tableBasic = GetTableCoreEnvelopeCutData(cutData, "basic");
            DataTable tableCut = GetTableCoreEnvelopeCutData(cutData, "cut");
            GraphPane myPane = zedGraphControlCore.GraphPane;

            //清除原来的图形
            myPane.CurveList.Clear();
            myPane.GraphObjList.Clear();

            //设置网格线可见
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            //设置网格线颜色
            myPane.XAxis.MajorGrid.Color = Color.Chocolate;
            myPane.YAxis.MajorGrid.Color = Color.Chocolate;

            //设置网格线形式
            myPane.XAxis.MajorGrid.DashOff = 1;
            myPane.YAxis.MajorGrid.DashOff = 1;
            myPane.XAxis.MajorGrid.DashOn = 4;
            myPane.YAxis.MajorGrid.DashOn = 4;

            //设置显示坐标
            myPane.XAxis.Scale.IsUseTenPower = false;
            myPane.YAxis.Scale.IsUseTenPower = false;
            myPane.XAxis.Scale.MagAuto = true;
            myPane.YAxis.Scale.MagAuto = true;

            myPane.Title.Text = "重心包线对比图";
            myPane.XAxis.Title.Text = "长度(毫米)";
            myPane.YAxis.Title.Text = "重量(千克)";

            AddMyCurve(zedGraphControlCore, Color.Blue, tableBasic, SymbolType.Diamond, "原始重心包线", true);
            //AddMyCurve(zedGraphControlCore, Color.Red, tableCut, SymbolType.Square, "剪裁重心包线", false);
            // modified 2014 9 11
            AddMyCurve(zedGraphControlCore, Color.Red, tableCut, SymbolType.Square, "剪裁重心包线", true);

            zedGraphControlCore.RestoreScale(myPane);
            zedGraphControlCore.AxisChange();
            zedGraphControlCore.Refresh();
        }

        private void AddMyCurve(ZedGraphControl zedGraphControlCore, Color color, DataTable table, SymbolType symbolType, string strLable, bool IsClosed)
        {
            GraphPane myPane = zedGraphControlCore.GraphPane;

            PointPairList listCur = new PointPairList();

            double x = 0, y = 0;
            string strTitle = string.Empty;
            string strValue = string.Empty;
            for (int j = 0; j < table.Columns.Count; j++)
            {
                string[] strArray = table.Rows[0][j].ToString().Split(',');
                x = strArray[0] == string.Empty ? 0 : Convert.ToDouble(strArray[0]);
                x = Math.Round(x, picDigit);
                if (strArray.Length > 1)
                {
                    y = strArray[1] == string.Empty ? 0 : Convert.ToDouble(strArray[1]);
                    y = Math.Round(y, picDigit);
                }

                listCur.Add(x, y);

                //显示名称
                strTitle = table.Columns[j].Caption;

                // 创建一个阴影区域，看起来有渐变
                TextObj text = new TextObj(strTitle, x, y,
                    CoordType.AxisXYScale, AlignH.Right, AlignV.Center);
                //是否有背景
                text.FontSpec.Fill.IsVisible = false;
                //是否有边框
                text.FontSpec.Border.IsVisible = false;
                //文字是否粗体
                text.FontSpec.IsBold = false;
                //文字是否斜体
                text.FontSpec.IsItalic = false;
                text.FontSpec.Size = 16;
                //填充
                myPane.GraphObjList.Add(text);
            }

            if (table != null && table.Rows.Count > 0 && IsClosed)
            {
                string[] strArrayValue = table.Rows[0][0].ToString().Split(',');
                x = strArrayValue[0] == string.Empty ? 0 : Convert.ToDouble(strArrayValue[0]);
                x = Math.Round(x, picDigit);
                if (strArrayValue.Length > 1)
                {
                    y = strArrayValue[1] == string.Empty ? 0 : Convert.ToDouble(strArrayValue[1]);
                    y = Math.Round(y, picDigit);
                }
                listCur.Add(x, y);
            }

            LineItem myCurveCur = myPane.AddCurve(strLable, listCur, color, symbolType);
            myCurveCur.Symbol.Size = 6;
            myCurveCur.Symbol.Fill = new Fill(color, color);
            myCurveCur.Symbol.Border.IsVisible = true;
            myCurveCur.Line.IsVisible = true;
        }

        /// <summary>
        /// 更新重心包线剪裁结果
        /// </summary>
        /// <param name="cutData"></param>
        /// <returns></returns>
        private void UpdateCoreEnvelopeCutResult(CoreEnvelopeCutResultData cutData)
        {
            if (cutData != null)
            {
                string strType = GetCoreEnvelopeCutType();
                if (gridViewCoreEnvelopeCutReuslt.Rows.Count > 0)
                {
                    List<CorePointData> lstCorePt = null;
                    List<CorePointData> lstBaicCorePt = cutData.lstBasicCoreEnvelope;

                    for (int i = 1; i < gridViewCoreEnvelopeCutReuslt.ColumnCount; i++)
                    {
                        //基础重心包线
                        if (strType == "basic")
                        {
                            lstCorePt = cutData.lstBasicCoreEnvelope;
                        }
                        //剪裁重心包线
                        if (strType == "cut")
                        {
                            lstCorePt = cutData.lstCutEnvelopeCore;
                        }

                        if (lstCorePt != null && lstCorePt.Count > 0)
                        {
                            foreach (CorePointData data in lstCorePt)
                            {
                                if (data.pointName == gridViewCoreEnvelopeCutReuslt.Columns[i].HeaderText)
                                {
                                    data.pointXValue = Convert.ToDouble(gridViewCoreEnvelopeCutReuslt.Rows[0].Cells[i].Value.ToString());
                                    data.pointYValue = Convert.ToDouble(gridViewCoreEnvelopeCutReuslt.Rows[1].Cells[i].Value.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }

        private string GetCoreEnvelope(List<CorePointData> lstCorePt)
        {
            string strContent = string.Empty;

            if (lstCorePt != null && lstCorePt.Count > 0)
            {
                foreach (CorePointData data in lstCorePt)
                {
                    if (data == lstCorePt.First())
                    {
                        strContent += data.pointName + ":" + "横坐标(毫米)、纵坐标(千克)";
                    }
                    else
                    {
                        strContent += "|" + data.pointName + ":" + "横坐标(毫米)、纵坐标(千克)";
                    }
                    strContent += "、" + data.pointXValue.ToString() + "、" + data.pointYValue.ToString();
                }
            }

            return strContent;
        }

        /// <summary>
        /// 显示剪裁重心包线
        /// </summary>
        /// <param name="chart1"></param>
        /// <param name="table"></param>
        private void DisplayLineCutPic(System.Windows.Forms.DataVisualization.Charting.Chart chart1, DataTable table)
        {
            chart1.Titles.Clear();
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Legends.Clear();

            if (table != null && table.Columns.Count > 0)
            {
                //标题
                Title title1 = new Title();
                title1.Text = "剪裁重心包线";
                title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
                chart1.Titles.Add(title1);

                AddChartSeries(chart1, table, "剪裁重心包线", MarkerStyle.Diamond, Color.Blue);

                if (chart1.ChartAreas.Count > 0)
                {
                    //chart1.ChartAreas[0].AxisX.Title = "毫米";
                    //chart1.ChartAreas[0].AxisY.Title = "千克";
                    //chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10);
                    //chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10);


                    double XMax = (double)chart1.Series[0].Points.OrderByDescending(p => p.XValue).First().XValue;
                    double XMin = (double)chart1.Series[0].Points.OrderBy(p => p.XValue).First().XValue;
                    chart1.ChartAreas[0].AxisX.Maximum = XMax + 10;
                    chart1.ChartAreas[0].AxisX.Minimum = XMin;
                }
            }
        }

        /// <summary>
        /// 显示对比重心包线
        /// </summary>
        /// <param name="chart1"></param>
        /// <param name="basicTable"></param>
        /// <param name="cutTable"></param>
        private void DisplayLineComparePic(System.Windows.Forms.DataVisualization.Charting.Chart chart1, DataTable basicTable, DataTable cutTable)
        {
            chart1.Titles.Clear();
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Legends.Clear();

            //标题
            Title title1 = new Title();
            title1.Text = "重心包线对比";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            chart1.Titles.Add(title1);

            AddChartSeries(chart1, basicTable, "基础重心包线", MarkerStyle.Diamond, Color.Blue);
            AddChartSeries(chart1, cutTable, "剪裁重心包线", MarkerStyle.Square, Color.Red);

            if (chart1.ChartAreas.Count > 1)
            {

                //设置X轴最大坐标
                double XValue1 = (double)chart1.Series[0].Points.OrderByDescending(p => p.XValue).First().XValue;
                double XValue2 = (double)chart1.Series[1].Points.OrderByDescending(p => p.XValue).First().XValue;
                //X轴最小值
                double XMin1 = (double)chart1.Series[0].Points.OrderBy(p => p.XValue).First().XValue;
                double XMin2 = (double)chart1.Series[1].Points.OrderBy(p => p.XValue).First().XValue;


                double XMax = 0;
                if (XValue1 >= XValue2)
                {
                    XMax = XValue1 + 10;
                }
                if (XValue1 < XValue2)
                {
                    XMax = XValue2 + 10;
                }

                double XMin = 0;
                if (XMin1 <= XMin2)
                {
                    XMin = XMin1;
                }
                if (XMin1 > XMin2)
                {
                    XMin = XMin2;
                }

                chart1.ChartAreas[0].AxisX.Maximum = XMax;
                chart1.ChartAreas[0].AxisX.Minimum = XMin;
            }
        }

        private void AddChartSeries(System.Windows.Forms.DataVisualization.Charting.Chart chart1, DataTable table, string strLable, MarkerStyle markerStyle, Color color)
        {
            if (table != null && table.Columns.Count > 0)
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

                if (chart1.Series.Count == 0)
                {
                    series.Name = "Series1";
                    legend.Name = "legend1";
                    if (chart1.Name == "chartCompareCoreEnvelope")
                    {
                        series.LegendText = "基础重心包线";
                    }
                    legend.Docking = Docking.Top;
                }
                else
                {
                    series.Name = "Series2";
                    legend.Name = "legend2";
                    series.LegendText = "剪裁重心包线";
                    legend.Docking = Docking.Top;

                }

                if (chart1.Name == "chartCompareCoreEnvelope")
                {
                    chart1.Legends.Add(legend);
                }

                int count = table.Columns.Count;
                List<double> lstXValue = new List<double>();
                List<double> lstYValue = new List<double>();
                double x = 0, y = 0;
                for (int j = 0; j < count; j++)
                {
                    string[] strArray = table.Rows[0][j].ToString().Split(',');
                    x = strArray[0] == string.Empty ? 0 : Convert.ToDouble(strArray[0]);
                    x = Math.Round(x, picDigit);
                    if (strArray.Length > 1)
                    {
                        y = strArray[1] == string.Empty ? 0 : Convert.ToDouble(strArray[1]);
                        y = Math.Round(y, picDigit);
                    }

                    lstXValue.Add(x);
                    lstYValue.Add(y);

                    if (j == count - 1)
                    {
                        double x1 = 0, y1 = 0;
                        string[] strArrayValue = table.Rows[0][0].ToString().Split(',');
                        x1 = strArrayValue[0] == string.Empty ? 0 : Convert.ToDouble(strArrayValue[0]);
                        x1 = Math.Round(x1, picDigit);
                        if (strArrayValue.Length > 1)
                        {
                            y1 = strArrayValue[1] == string.Empty ? 0 : Convert.ToDouble(strArrayValue[1]);
                            y1 = Math.Round(y1, picDigit);
                        }

                        lstXValue.Add(x1);
                        lstYValue.Add(y1);
                    }
                }

                series.Points.DataBindXY(lstXValue, lstYValue);

                for (int i = 0; i < count + 1; i++)
                {
                    series.Points[i].ToolTip = "(" + lstXValue[i].ToString() + "," + lstYValue[i].ToString() + ")";
                    series.Points[i].LabelToolTip = "(" + lstXValue[i].ToString() + "," + lstYValue[i].ToString() + ")";
                    if (i == count)
                    {
                        series.Points[i].IsValueShownAsLabel = false;
                    }
                    if (i < count)
                    {
                        series.Points[i].IsValueShownAsLabel = true;
                        series.Points[i].Label = table.Columns[i].Caption;
                    }
                }
                series.ChartArea = "ChartArea1";
                chart1.Series.Add(series);

                series.ChartType = SeriesChartType.Line;                //直线
                series.Color = color;                              //线条颜色   
                series.BorderWidth = 2;                                  //线条宽度   
                series.ShadowOffset = 1;                                 //阴影宽度   
                series.IsVisibleInLegend = true;                         //是否显示数据说明   
                //series.IsValueShownAsLabel = false;                        //显示数据
                series.MarkerStyle = markerStyle;               //线条上的数据点标志类型   
                series.MarkerSize = 8;
            }
        }
        #endregion

        #region 重量调整结果

        private void SetWeightAdjustTab()
        {
            if (tabControlWork.TabPages.Contains(tabPageAdjustment) == false)
            {
                tabControlWork.TabPages.Add(tabPageAdjustment);
            }

            tabPageAdjustment.ToolTipText = selNode.ToolTipText;
            WeightAdjustmentResultData resultData = GetWeightAdjustmentData();
            SetWeightAdjustmentResult(resultData);
            tabControlWork.SelectedTab = tabPageAdjustment;
        }

        public void SetWeightAdjustTab(WeightAdjustmentResultData adjustData, int index)
        {
            if (tabControlWork.TabPages.Contains(tabPageAdjustment) == false)
            {
                tabControlWork.TabPages.Add(tabPageAdjustment);
            }

            tabPageAdjustment.ToolTipText = index.ToString();
            SetWeightAdjustmentResult(adjustData);
            tabControlWork.SelectedTab = tabPageAdjustment;

            TreeNode rootNode = treeViewData.Nodes[0];
            foreach (TreeNode parentNode in rootNode.Nodes)
            {
                if (parentNode.Text == "重量调整结果列表")
                {
                    foreach (TreeNode node in parentNode.Nodes)
                    {
                        if (index.ToString() == node.ToolTipText)
                        {
                            treeViewData.SelectedNode = node;
                            selNode = node;
                        }
                    }
                }

            }
        }

        private WeightAdjustmentResultData GetWeightAdjustmentData()
        {
            WeightAdjustmentResultData resultData = new WeightAdjustmentResultData();

            for (int i = 0; i < lstAdjustmentResultData.Count; i++)
            {
                if (selNode != null && selNode.Level == 2 && selNode.ToolTipText == i.ToString())
                {
                    resultData = lstAdjustmentResultData[i];
                    break;
                }
            }

            return resultData;
        }

        /// <summary>
        /// 设置重量调整结果数据
        /// </summary>
        public void SetWeightAdjustmentResult(WeightAdjustmentResultData resultData)
        {
            if (resultData != null)
            {
                tabPageAdjustment.Text = resultData.WeightAdjustName;
                txtAdjustmentDataName.Text = resultData.WeightAdjustName;
                txtAdjustmentSortName.Text = resultData.SortName;

                //绑定重量分类 
                BindWeightDesignSort(resultData.basicWeightData, treeViewBasicSort);
                BindWeightDesignSort(resultData.weightAdjustData, treeViewAdjustmentSort);

                //绑定GridView
                BindWeightAdjustmentGridView(resultData);

                //圆饼图显示
                DisplayPiePic(chartWeightAdjust, GetPicListWeightData(resultData.basicWeightData, gridViewAdjustment, "基础重量数据"), "基础重量");

                //对比图显示
                DisplayAdjustmentInPic(WeightSortData.GetListWeightData(resultData.basicWeightData), WeightSortData.GetListWeightData(resultData.weightAdjustData));
            }
        }

        private void BindWeightAdjustmentGridView(WeightAdjustmentResultData adjustmentData)
        {
            //设置GridView
            SeWeightAdjustmentGridView(adjustmentData);

            DataTable tableBasic = GetWeightDesignTableData(adjustmentData.basicWeightData);
            DataTable tableAdjust = GetWeightDesignTableData(adjustmentData.weightAdjustData);

            for (int i = 1; i < gridViewAdjustment.ColumnCount; i++)
            {
                gridViewAdjustment.Rows[0].Cells[i].Value = tableBasic.Rows[0][gridViewAdjustment.Columns[i].Name];
                gridViewAdjustment.Rows[1].Cells[i].Value = tableAdjust.Rows[0][gridViewAdjustment.Columns[i].Name];
            }
        }

        private void SeWeightAdjustmentGridView(WeightAdjustmentResultData adjustmentData)
        {
            gridViewAdjustment.Columns.Clear();
            gridViewAdjustment.DataSource = null;

            if (adjustmentData != null)
            {
                //第一列
                DataGridViewTextBoxColumn firstColumn = new DataGridViewTextBoxColumn();
                firstColumn.HeaderText = string.Empty;
                firstColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                gridViewAdjustment.Columns.Add(firstColumn);

                List<WeightData> lstAdjustment = WeightSortData.GetListWeightData(adjustmentData.basicWeightData);

                for (int i = 0; i < lstAdjustment.Count; i++)
                {
                    DataGridViewTextBoxColumn txtColumn = new DataGridViewTextBoxColumn();
                    txtColumn.Name = lstAdjustment[i].nID.ToString();
                    txtColumn.DataPropertyName = lstAdjustment[i].weightName;
                    txtColumn.HeaderText = lstAdjustment[i].weightName;
                    txtColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                    gridViewAdjustment.Columns.Add(txtColumn);

                    gridViewAdjustment.Columns[i + 1].ValueType = System.Type.GetType("System.Decimal");
                }

                //添加行
                if (gridViewAdjustment.ColumnCount > 1)
                {
                    gridViewAdjustment.Rows.Add(2);
                }

                gridViewAdjustment.Rows[0].Cells[0].Value = "基础重量";
                gridViewAdjustment.Rows[1].Cells[0].Value = "调整重量";
                gridViewAdjustment.Columns[0].ReadOnly = true;
            }
        }

        /// <summary>
        /// 更新重量调整数据
        /// </summary>
        /// <param name="adjustmentData"></param>
        private void UpdateAdjustData(WeightAdjustmentResultData adjustmentData)
        {
            if (adjustmentData != null)
            {
                for (int i = 1; i < gridViewAdjustment.ColumnCount; i++)
                {
                    foreach (WeightData data in adjustmentData.basicWeightData.lstWeightData)
                    {
                        if (data.nID.ToString() == gridViewAdjustment.Columns[i].Name)
                        {
                            data.weightValue = Convert.ToDouble(gridViewAdjustment.Rows[0].Cells[i].Value);
                            break;
                        }
                    }
                    foreach (WeightData data in adjustmentData.weightAdjustData.lstWeightData)
                    {
                        if (data.nID.ToString() == gridViewAdjustment.Columns[i].Name)
                        {
                            data.weightValue = Convert.ToDouble(gridViewAdjustment.Rows[1].Cells[i].Value);
                            break;
                        }
                    }
                }

                //求和
                foreach (WeightData data in adjustmentData.basicWeightData.lstWeightData)
                {
                    WeightSortData.GetSortDataTotal(data, adjustmentData.basicWeightData);
                }

                //求和
                foreach (WeightData data in adjustmentData.weightAdjustData.lstWeightData)
                {
                    WeightSortData.GetSortDataTotal(data, adjustmentData.weightAdjustData);
                }
            }
        }

        private List<WeightData> GetPicListWeightData(WeightSortData sortData, DataGridView gridResult, string strType)
        {
            List<WeightData> lstWeightData = new List<WeightData>();

            if (sortData != null)
            {
                //获取第一个节点的重量数据对象
                IEnumerable<WeightData> selection = from wd in sortData.lstWeightData where wd.nParentID == 0 select wd;
                foreach (WeightData wd in selection)
                {
                    lstWeightData.Add(wd);
                }

                double dValue = 0;
                for (int i = 0; i < lstWeightData.Count; i++)
                {
                    for (int j = 0; j < gridResult.ColumnCount; j++)
                    {
                        if (lstWeightData[i].nID.ToString() == gridResult.Columns[j].Name)
                        {
                            if (strType == "基础重量数据")
                            {
                                dValue = gridResult.Rows[0].Cells[j].Value is System.DBNull ? 0 : Convert.ToDouble(gridResult.Rows[0].Cells[j].Value);
                            }
                            if (strType == "调整重量数据")
                            {
                                dValue = gridResult.Rows[1].Cells[j].Value is System.DBNull ? 0 : Convert.ToDouble(gridResult.Rows[1].Cells[j].Value);
                            }
                            lstWeightData[i].weightValue = dValue;
                            break;
                        }
                    }
                }
            }

            return lstWeightData;
        }

        private void DisplayAdjustmentInPic(List<WeightData> lstBasic, List<WeightData> lstAdjustData)
        {
            chartAdjustment.Titles.Clear();
            chartAdjustment.Series.Clear();
            chartAdjustment.ChartAreas.Clear();
            chartAdjustment.Legends.Clear();

            rightCount = 0;
            toolStripMenuItemDisplayData.Text = "显示数据";

            if (lstBasic != null && lstBasic.Count > 0 && lstBasic != null && lstAdjustData.Count > 0)
            {
                //标题
                Title title1 = new Title();
                title1.Text = "重量调整";
                title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
                chartAdjustment.Titles.Add(title1);

                AddChartSeries(chartAdjustment, lstBasic, "基础重量", Color.Blue);
                AddChartSeries(chartAdjustment, lstAdjustData, "调整重量", Color.Red);

                if (chartAdjustment.ChartAreas.Count > 0)
                {
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
        }

        private void AddChartSeries(System.Windows.Forms.DataVisualization.Charting.Chart chart1, List<WeightData> lstWeightData, string strLable, Color color)
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
                    yValue = Math.Round(data.weightValue, picDigit);
                    series.Points.AddXY(data.weightName, yValue);
                }

                string strSign = string.Empty;
                if (series.Name == "Series1")
                {
                    strSign = "基础重量-";
                }
                if (series.Name == "Series2")
                {
                    strSign = "调整重量-";
                }

                for (int i = 0; i < lstWeightData.Count; i++)
                {
                    yValue = Math.Round(lstWeightData[i].weightValue, picDigit);
                    series.Points[i].ToolTip = strSign + lstWeightData[i].weightName + ":" + yValue.ToString();
                }

                series.ChartArea = "ChartArea1";
                chart1.Series.Add(series);

                series.ChartType = SeriesChartType.Column;                //柱形图
                series.Color = color;                              //线条颜色   
                //series.BorderWidth = 1;                                  //线条宽度   
                //series.ShadowOffset = 1;                                 //阴影宽度   
                //series.IsVisibleInLegend = true;                         //是否显示数据说明   
                //series.MarkerStyle = MarkerStyle.Circle;               //线条上的数据点标志类型   
                //series.MarkerSize = 8;

                //series["DrawingStyle"] = "Cylinder";                 //圆柱

                //柱形宽度
                series["PixelPointWidth"] = "30";
                //像素点深度
                series["PixelPointDepth"] = "80";
                //像素点间隙深度
                series["PixelPointGapDepth"] = "10";

                //series["PointWidth"] = "0.8";
            }
        }

        #endregion

        #region 重量评估结果

        public void SetWeightEstimatedTab(WeightAssessResult result)
        {
            if (tabControlWork.TabPages.Contains(tabPageWeightAssessment) == false)
            {
                tabControlWork.TabPages.Add(tabPageWeightAssessment);
            }
            tabPageWeightAssessment.ToolTipText = selNode.ToolTipText;
            tabPageWeightAssessment.Text = result.resultName;
            SetWeightEstimatedResult(result);
            tabControlWork.SelectedTab = tabPageWeightAssessment;
        }

        public void SetWeightEstimatedTab(WeightAssessResult result, int index)
        {
            if (tabControlWork.TabPages.Contains(tabPageWeightAssessment) == false)
            {
                tabControlWork.TabPages.Add(tabPageWeightAssessment);
            }

            tabPageWeightAssessment.ToolTipText = index.ToString();
            tabPageWeightAssessment.Text = result.resultName;
            SetWeightEstimatedResult(result);
            tabControlWork.SelectedTab = tabPageWeightAssessment;

            TreeNode rootNode = treeViewData.Nodes[0];
            foreach (TreeNode parentNode in rootNode.Nodes)
            {
                if (parentNode.Text == "重量评估结果列表")
                {
                    foreach (TreeNode node in parentNode.Nodes)
                    {
                        if (node.Name == result.resultID)
                        {
                            treeViewData.SelectedNode = node;
                            selNode = node;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置重量评估结果
        /// </summary>
        /// <param name="result"></param>
        private void SetWeightEstimatedResult(WeightAssessResult result)
        {
            txtEstimateName.Text = result.resultName;

            //绑定基准重量
            BindWeightData(GetFirstWeightData(result.datumWeightDataList), treeViewDatumSort);
            //绑定评估重量
            BindWeightData(result.assessWeightDataList, treeViewAssessSort);

            //绑定重量对比
            CommonUtil.DisplayColumnInPic(chartWeightCompare, result);
            //合理性评估
            CommonUtil.DisplayAssessLinePic(chartRationalityAssess, result.weightAssessParamList, "合理性评估");
            //先进性评估
            CommonUtil.DisplayAssessLinePic(chartAdvancedAssess, result.weightAssessParamList, "先进性评估");

            //绑定
            BindWeightEstimatedGridView(result.weightAssessParamList, gridWeightEstimateResult);

            txtDatumWeightSum.Text = Math.Round(result.datumWeightTotal, digit).ToString();
            txtAssessWeightSum.Text = Math.Round(result.assessWeightTotal, digit).ToString();
            txtAdvancedSum.Text = Math.Round(result.advancedInflationTotal, digit).ToString();
            txtRationalitySum.Text = Math.Round(result.rationalityInflationTotal, digit).ToString();

            txtEstimateName.Enabled = false;
            txtDatumWeightSum.Enabled = false;
            txtAssessWeightSum.Enabled = false;
            txtAdvancedSum.Enabled = false;
            txtRationalitySum.Enabled = false;
        }

        /// <summary>
        /// 获取第一层节点数据
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        private List<WeightData> GetFirstWeightData(List<WeightData> lstData)
        {
            List<WeightData> lstWeightData = new List<WeightData>();
            foreach (WeightData data in lstData)
            {
                if (data.nParentID == -1 || data.nParentID == 0)
                {
                    lstWeightData.Add(data);
                }
            }
            return lstWeightData;
        }

        /// <summary>
        /// 绑定重量数据
        /// </summary>
        /// <param name="strTitle"></param>
        private void BindWeightData(List<WeightData> lstWeightData, TreeView treeViewSort)
        {
            treeViewSort.Nodes.Clear();

            if (lstWeightData != null && lstWeightData.Count > 0)
            {
                BindTreeList(treeViewSort, lstWeightData);
                treeViewSort.ExpandAll();
            }
        }

        /// <summary>
        /// 绑定重量结构树数据
        /// </summary>
        private void BindTreeList(TreeView tree, List<WeightData> lstWeightData)
        {
            tree.Nodes.Clear();

            IEnumerable<WeightData> selection = from wd in lstWeightData where wd.nParentID == -1 select wd;
            foreach (WeightData wd in selection)
            {
                TreeNode node = new TreeNode();
                node.Name = wd.weightName;
                node.Text = wd.weightName + "[" + Math.Round(wd.weightValue, digit).ToString() + " 千克" + "]";
                tree.Nodes.Add(node);

                BindTreeNode(node, wd.nID, lstWeightData);
            }
        }

        /// <summary>
        /// 绑定重量结构树数据子节点
        /// </summary>
        private static void BindTreeNode(TreeNode ParentNode, int nParentID, List<WeightData> lstWeightData)
        {
            IEnumerable<WeightData> selection = from wd in lstWeightData where wd.nParentID == nParentID select wd;
            foreach (WeightData wd in selection)
            {
                string strKey = ParentNode.Name + "\\" + wd.weightName;
                string strText = wd.weightName + "[" + Math.Round(wd.weightValue, digit).ToString() + " 千克" + "]";
                TreeNode node = ParentNode.Nodes.Add(strKey, strText);

                BindTreeNode(node, wd.nID, lstWeightData);
            }
        }

        private void SetWeightEstimatedGridView(List<WeightAssessParameter> lstPara, DataGridView gridView)
        {
            gridView.Columns.Clear();
            gridView.DataSource = null;

            if (lstPara != null && lstPara.Count > 0)
            {
                //添加第一列
                DataGridViewTextBoxColumn gridColumnFirst = new DataGridViewTextBoxColumn();
                gridColumnFirst.Name = "columnFirst";
                gridColumnFirst.HeaderText = string.Empty;
                gridColumnFirst.SortMode = DataGridViewColumnSortMode.NotSortable;
                gridColumnFirst.ReadOnly = true;
                gridView.Columns.Add(gridColumnFirst);
                for (int i = 0; i < lstPara.Count; i++)
                {
                    DataGridViewTextBoxColumn gridColumn = new DataGridViewTextBoxColumn();
                    gridColumn.Name = "column" + (i + 1).ToString();
                    gridColumn.HeaderText = lstPara[i].weightName;
                    gridColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                    gridColumn.ValueType = System.Type.GetType("System.Decimal");
                    gridView.Columns.Add(gridColumn);
                }
            }

            if (gridView.ColumnCount > 0)
            {
                gridView.Rows.Add(1);
                gridView.Rows[0].Cells[0].Value = "合理性指标";
                gridView.Rows[1].Cells[0].Value = "先进性指标";
            }
        }

        private void BindWeightEstimatedGridView(List<WeightAssessParameter> lstPara, DataGridView gridView)
        {
            SetWeightEstimatedGridView(lstPara, gridView);
            foreach (WeightAssessParameter para in lstPara)
            {
                foreach (DataGridViewColumn col in gridView.Columns)
                {
                    if (para.weightName == col.HeaderText)
                    {
                        gridView.Rows[0].Cells[col.Name].Value = Math.Round(para.rationalityInflation, digit);
                        gridView.Rows[1].Cells[col.Name].Value = Math.Round(para.advancedInflation, digit);
                    }
                }
            }
        }

        private void UpDateWeighEstimatedData()
        {
            WeightAssessResult result = (WeightAssessResult)selNode.Tag;

            double rationalitySum = 0;
            double advancedSum = 0;

            foreach (WeightAssessParameter para in result.weightAssessParamList)
            {
                foreach (DataGridViewColumn col in gridWeightEstimateResult.Columns)
                {
                    if (para.weightName == col.HeaderText)
                    {
                        para.rationalityInflation = double.Parse(gridWeightEstimateResult.Rows[0].Cells[col.Name].Value.ToString());
                        para.advancedInflation = double.Parse(gridWeightEstimateResult.Rows[1].Cells[col.Name].Value.ToString());

                        rationalitySum += para.rationalityInflation;
                        advancedSum += para.advancedInflation;

                        break;
                    }
                }
            }

            result.rationalityInflationTotal = rationalitySum;
            result.advancedInflationTotal = advancedSum;
        }

        #endregion

        #region 重心评估评估结果

        public void SetCoreEstimatedTab(CoreAssessResult result)
        {
            if (tabControlWork.TabPages.Contains(tabPageCoreEnvelopeEstimated) == false)
            {
                tabControlWork.TabPages.Add(tabPageCoreEnvelopeEstimated);
            }
            tabPageCoreEnvelopeEstimated.ToolTipText = selNode.ToolTipText;
            tabPageCoreEnvelopeEstimated.Text = result.resultName;
            SetCoreEstimatedResult(result);
            tabControlWork.SelectedTab = tabPageCoreEnvelopeEstimated;
        }

        public void SetCoreEstimatedTab(CoreAssessResult result, int index)
        {
            if (tabControlWork.TabPages.Contains(tabPageCoreEnvelopeEstimated) == false)
            {
                tabControlWork.TabPages.Add(tabPageCoreEnvelopeEstimated);
            }

            tabPageCoreEnvelopeEstimated.ToolTipText = index.ToString();
            tabPageCoreEnvelopeEstimated.Text = result.resultName;
            SetCoreEstimatedResult(result);
            tabControlWork.SelectedTab = tabPageCoreEnvelopeEstimated;

            TreeNode rootNode = treeViewData.Nodes[0];
            foreach (TreeNode parentNode in rootNode.Nodes)
            {
                if (parentNode.Text == "重心包线评估结果列表")
                {
                    foreach (TreeNode node in parentNode.Nodes)
                    {
                        if (node.Name == result.resultID)
                        {
                            treeViewData.SelectedNode = node;
                            selNode = node;
                        }
                    }
                }
            }
        }

        private void SetCoreEstimatedResult(CoreAssessResult result)
        {
            txtCoreDataName.Text = result.resultName;
            txtCoreDataName.Enabled = false;
            //绑定基准重心包线
            BindCoreEstimatedTree(treeBasicCore, "basic", result);
            //绑定待评估重心包线
            BindCoreEstimatedTree(treeEstimatedCore, "estimate", result);

            //合理性评估结果
            txtRationalityResult.Text = Math.Round(result.evaluationResult, digit).ToString();
            txtRationalityResult.Enabled = false;
            //绑定重心评估结果
            BindCoreEstimatedGridView(result.assessCoreDataList);

            DisplayCoreEstimatedPic(result, zedGraphCoreEstimated, true);
        }

        private void BindCoreEstimatedTree(TreeView tree, string strType, CoreAssessResult result)
        {
            List<CorePointExt> lstCorePt = new List<CorePointExt>();
            //基准重心包线
            if (strType == "basic")
            {
                lstCorePt = result.datumCoreDataList;
            }
            //待评估重心包线
            else if (strType == "estimate")
            {
                lstCorePt = result.assessCoreDataList;
            }

            if (lstCorePt != null && lstCorePt.Count > 0)
            {
                tree.Nodes.Clear();

                TreeNode parentNode = new TreeNode("重心包线");
                tree.Nodes.Add(parentNode);
                foreach (CorePointExt pt in lstCorePt)
                {
                    TreeNode node = new TreeNode(pt.pointName);
                    parentNode.Nodes.Add(node);

                    string strXTitle = "横坐标:[" + Math.Round(pt.pointXValue, digit).ToString() + " 毫米" + "]";
                    TreeNode xNode = new TreeNode(strXTitle);
                    node.Nodes.Add(xNode);

                    string strYTitle = "纵坐标:[" + Math.Round(pt.pointYValue, digit).ToString() + " 千克" + "]";
                    TreeNode yNode = new TreeNode(strYTitle);
                    node.Nodes.Add(yNode);
                }
                parentNode.ExpandAll();
            }
        }

        private void SetCoreEstimatedGridView(List<CorePointExt> lstCorePt)
        {
            gridCoreEstimatedResult.Columns.Clear();
            gridCoreEstimatedResult.DataSource = null;

            if (lstCorePt != null && lstCorePt.Count > 0)
            {
                DataGridViewTextBoxColumn firstColumn = new DataGridViewTextBoxColumn();
                firstColumn.Name = "columnFirst";
                firstColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                firstColumn.HeaderText = string.Empty;
                firstColumn.ReadOnly = true;
                gridCoreEstimatedResult.Columns.Add(firstColumn);

                foreach (CorePointExt pt in lstCorePt)
                {
                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                    column.Name = pt.id;
                    column.HeaderText = pt.pointName;
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.ValueType = System.Type.GetType("System.Decimal");
                    gridCoreEstimatedResult.Columns.Add(column);
                }
            }

            if (gridCoreEstimatedResult.ColumnCount > 0)
            {
                gridCoreEstimatedResult.Rows.Add(2);
                gridCoreEstimatedResult.Rows[0].Cells[0].Value = "横坐标(毫米)";
                gridCoreEstimatedResult.Rows[1].Cells[0].Value = "纵坐标(千克)";
                gridCoreEstimatedResult.Rows[2].Cells[0].Value = "评估结果";
            }
        }

        /// <summary>
        /// 绑定重心评估结果
        /// </summary>
        /// <param name="lstCorePt"></param>
        private void BindCoreEstimatedGridView(List<CorePointExt> lstCorePt)
        {
            SetCoreEstimatedGridView(lstCorePt);

            if (lstCorePt != null && lstCorePt.Count > 0)
            {
                foreach (CorePointExt pt in lstCorePt)
                {
                    foreach (DataGridViewColumn col in gridCoreEstimatedResult.Columns)
                    {
                        if (pt.id == col.Name)
                        {
                            gridCoreEstimatedResult.Rows[0].Cells[col.Name].Value = Math.Round(pt.pointXValue, digit).ToString();
                            gridCoreEstimatedResult.Rows[1].Cells[col.Name].Value = Math.Round(pt.pointYValue, digit).ToString();
                            gridCoreEstimatedResult.Rows[2].Cells[col.Name].Value = Math.Round(pt.assessValue, digit).ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 显示重心包线评估图
        /// </summary>
        /// <param name="result"></param>
        /// <param name="zedGraphControlCore"></param>
        /// <param name="IsClosed"></param>
        private void DisplayCoreEstimatedPic(CoreAssessResult result, ZedGraphControl zedGraphControlCore, bool IsClosed)
        {
            GraphPane myPane = zedGraphControlCore.GraphPane;

            //清除原来的图形
            myPane.CurveList.Clear();
            myPane.GraphObjList.Clear();

            //设置网格线可见
            myPane.XAxis.MajorGrid.IsVisible = false;
            myPane.YAxis.MajorGrid.IsVisible = false;

            //设置网格线颜色
            //myPane.XAxis.MajorGrid.Color = Color.Chocolate;
            //myPane.YAxis.MajorGrid.Color = Color.Chocolate;

            //设置网格线形式
            myPane.XAxis.MajorGrid.DashOff = 1;
            myPane.YAxis.MajorGrid.DashOff = 1;
            myPane.XAxis.MajorGrid.DashOn = 4;
            myPane.YAxis.MajorGrid.DashOn = 4;

            //设置显示坐标
            myPane.XAxis.Scale.IsUseTenPower = false;
            myPane.YAxis.Scale.IsUseTenPower = false;
            myPane.XAxis.Scale.MagAuto = true;
            myPane.YAxis.Scale.MagAuto = true;

            myPane.Title.Text = "重心包线";
            myPane.XAxis.Title.Text = "长度(毫米)";
            myPane.YAxis.Title.Text = "重量(千克)";

            AddCurve(myPane, result.datumCoreDataList, Color.Blue, true);
            AddCurve(myPane, result.assessCoreDataList, Color.Red, false);

            zedGraphControlCore.AxisChange();
            zedGraphControlCore.Refresh();
        }

        private void AddCurve(GraphPane myPane, List<CorePointExt> lstCorePt, Color color, bool IsClosed)
        {
            PointPairList listCur = new PointPairList();

            double x = 0, y = 0;
            if (lstCorePt != null && lstCorePt.Count > 0)
            {
                foreach (CorePointExt pt in lstCorePt)
                {
                    x = Math.Round(pt.pointXValue, picDigit);
                    y = Math.Round(pt.pointYValue, picDigit);
                    listCur.Add(x, y);

                    // 创建一个阴影区域，看起来有渐变
                    TextObj text = new TextObj(pt.pointName, x, y,
                        CoordType.AxisXYScale, AlignH.Right, AlignV.Center);
                    //是否有背景
                    text.FontSpec.Fill.IsVisible = false;
                    //是否有边框
                    text.FontSpec.Border.IsVisible = false;
                    //文字是否粗体
                    text.FontSpec.IsBold = true;
                    //文字是否斜体
                    text.FontSpec.IsItalic = false;
                    //填充
                    myPane.GraphObjList.Add(text);
                }

                //是否成环形图形
                if (IsClosed)
                {
                    listCur.Add(Math.Round(lstCorePt[0].pointXValue, picDigit), Math.Round(lstCorePt[0].pointYValue, picDigit));
                }
                LineItem myCurveCur = myPane.AddCurve(string.Empty, listCur, color, SymbolType.None);
                myCurveCur.Symbol.Size = 6;
                myCurveCur.Symbol.Fill = new Fill(color, color);
                myCurveCur.Symbol.Border.IsVisible = true;
                myCurveCur.Line.IsVisible = true;
            }
        }

        /// <summary>
        /// 更新重心包线评估对象
        /// </summary>
        private void UpdateCoreEstimatedResult()
        {
            CoreAssessResult result = (CoreAssessResult)selNode.Tag;

            foreach (CorePointExt pt in result.assessCoreDataList)
            {
                foreach (DataGridViewColumn col in gridCoreEstimatedResult.Columns)
                {
                    if (pt.id == col.Name)
                    {
                        pt.pointXValue = gridCoreEstimatedResult.Rows[0].Cells[col.Name].Value is DBNull ? 0 : double.Parse(gridCoreEstimatedResult.Rows[0].Cells[col.Name].Value.ToString());
                        pt.pointYValue = gridCoreEstimatedResult.Rows[1].Cells[col.Name].Value is DBNull ? 0 : double.Parse(gridCoreEstimatedResult.Rows[1].Cells[col.Name].Value.ToString());
                        pt.assessValue = gridCoreEstimatedResult.Rows[2].Cells[col.Name].Value is DBNull ? 0 : double.Parse(gridCoreEstimatedResult.Rows[2].Cells[col.Name].Value.ToString());
                        break;
                    }
                }
            }
        }

        #endregion

        #region 同步参数

        /// <summary>
        /// 获取单位名称
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetUnitName(string str)
        {
            string strName = string.Empty;

            if (str == "度")
            {
                strName = "deg";
            }
            if (str == "毫米")
            {
                strName = "mm";
            }
            if (str == "米/秒")
            {
                strName = "m/s";
            }
            if (str == "平方米")
            {
                strName = "m^2";
            }
            if (str == "千克")
            {
                strName = "kg";
            }
            if (str == "千米")
            {
                strName = "km";
            }
            if (str == "千瓦")
            {
                strName = "KW";
            }


            return strName;
        }

        public static bool IsExitPara(string strParaName, List<ParaData> lstPara)
        {
            bool IsExist = false;
            if (lstPara != null && lstPara.Count > 0)
            {
                foreach (ParaData para in lstPara)
                {
                    if (strParaName == para.paraName)
                    {
                        IsExist = true;
                        break;
                    }
                }
            }

            return IsExist;
        }

        /// <summary>
        /// 同步参数到参数列表
        /// </summary>
        public static void SynchronizationWeightPara()
        {
            //文件中参数列表
            List<WeightParameter> lstWeightParaData = new List<WeightParameter>();

            //参数表中的参数
            List<ParaData> lstPara = WeightEstimateForm.GetListParaData();

            List<List<WeightParameter>> lstWeightPara = WeightParameter.GetWeightParameterList();
            if (lstWeightPara != null && lstWeightPara.Count > 0)
            {
                foreach (List<WeightParameter> lstParaData in lstWeightPara)
                {
                    foreach (WeightParameter para in lstParaData)
                    {
                        lstWeightParaData.Add(para);

                        if (para.ParaUnit == "个" || para.ParaUnit == "台" || para.ParaUnit == "片")
                        {
                            if (IsExitPara(para.ParaName, lstPara) == false)
                            {
                                PubSyswareCom.CreateIntParameter(para.ParaName, para.ParaValue, true, true, false);
                            }
                        }
                        else
                        {
                            if (IsExitPara(para.ParaName, lstPara) == false)
                            {
                                PubSyswareCom.CreateDoubleParameter(para.ParaName, para.ParaValue, true, true, false);
                            }
                        }

                        //设置单位
                        PubSyswareCom.SetParameterUnit(string.Empty, para.ParaName, GetUnitName(para.ParaUnit));

                        //设置分组
                        if (para.ParaType == 0)
                        {
                            PubSyswareCom.SetParameterGroup(para.ParaName, "指标参数");
                        }
                        if (para.ParaType == 1)
                        {
                            PubSyswareCom.SetParameterGroup(para.ParaName, "构型和总体参数");
                        }
                        if (para.ParaType == 2)
                        {
                            PubSyswareCom.SetParameterGroup(para.ParaName, "旋翼参数");
                        }
                        if (para.ParaType == 3)
                        {
                            PubSyswareCom.SetParameterGroup(para.ParaName, "机身翼面参数");
                        }
                        if (para.ParaType == 4)
                        {
                            PubSyswareCom.SetParameterGroup(para.ParaName, "着陆装置参数");
                        }
                        if (para.ParaType == 5)
                        {
                            PubSyswareCom.SetParameterGroup(para.ParaName, "动力系统参数");
                        }
                        if (para.ParaType == 6)
                        {
                            PubSyswareCom.SetParameterGroup(para.ParaName, "传动系统参数");
                        }
                        if (para.ParaType == 7)
                        {
                            PubSyswareCom.SetParameterGroup(para.ParaName, "操纵系统参数");
                        }
                        if (para.ParaType == 8)
                        {
                            PubSyswareCom.SetParameterGroup(para.ParaName, "人工参数");
                        }
                        if (para.ParaType == 9)
                        {
                            PubSyswareCom.SetParameterGroup(para.ParaName, "其他类型参数");
                        }
                        if (para.ParaType == 10)
                        {
                            PubSyswareCom.SetParameterGroup(para.ParaName, "临时参数");
                        }
                    }
                }
            }

            /*------------------------------------------删除文件中没有的参数-----------------------------*/

            List<string> lstName = new List<string>();

            for (int i = 0; i < lstPara.Count; i++)
            {
                bool IsExit = false;
                foreach (WeightParameter data in lstWeightParaData)
                {
                    if (data.ParaName == lstPara[i].paraName)
                    {
                        IsExit = true;
                        break;
                    }
                }
                if (IsExit == false)
                {
                    lstName.Add(lstPara[i].paraName);
                }
            }

            //删除参数
            foreach (string str in lstName)
            {
                PubSyswareCom.DeleteParameter(string.Empty, str);
            }

            //----------------------------------------------------------------------------------------------//

        }

        #endregion

        /// <summary>
        /// 保存图片
        /// </summary>
        public static void SavePictureToFile(System.Windows.Forms.DataVisualization.Charting.Chart chart1)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            fileDialog.Filter = "BMP文件|*.bmp|JPEG文件|*.jpg|GIF文件|*.gif";
            fileDialog.RestoreDirectory = true;
            fileDialog.FilterIndex = 1;
            fileDialog.FileName = string.Empty;
            fileDialog.OverwritePrompt = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                ChartImageFormat imgFormat = ChartImageFormat.Bmp;
                if (fileDialog.FilterIndex == 1)
                {
                    imgFormat = ChartImageFormat.Bmp;
                }
                if (fileDialog.FilterIndex == 2)
                {
                    imgFormat = ChartImageFormat.Jpeg;
                }
                if (fileDialog.FilterIndex == 3)
                {
                    imgFormat = ChartImageFormat.Gif;
                }
                string strFileName = fileDialog.FileName + fileDialog.DefaultExt;
                chart1.SaveImage(fileDialog.FileName, imgFormat);

                XLog.Write("保存图片\"" + strFileName + "\"成功");
            }
        }


        /// <summary>
        /// 设置单位
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strUnitName"></param>
        public static void SetParameterUnit(string strName, string strUnitName)
        {
            if (PubSyswareCom.ParseUnit(strUnitName) == string.Empty)
            {
                PubSyswareCom.SetParameterDescription(string.Empty, strName, strUnitName);
            }
            else
            {
                PubSyswareCom.SetParameterUnit(string.Empty, strName, PubSyswareCom.ParseUnit(strUnitName));
            }
        }

        #endregion

        #region 事件

        private void MainForm_Load(object sender, EventArgs e)
        {
            //创建数据库
            string strDataBaseWork = System.IO.Directory.GetCurrentDirectory() + "\\DataBase";
            CommonFunction.CreateDirectory(strDataBaseWork);
            if (!File.Exists(strDataBasePath))
            {
                BLL.BLLDBOper dataOper = new BLL.BLLDBOper();
                dataOper.CreateDBTable();
            }

            XLog.Write("软件启动");

        }

        private void contextMenuStripEditProject_Click(object sender, System.EventArgs e)
        {
            CoreDesignProject project = new CoreDesignProject(this, "edit");
            project.ShowDialog();
        }

        /// <summary>
        /// 重量设计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemWeightDesign_Click(object sender, EventArgs e)
        {
            WeightEstimateForm form = new WeightEstimateForm(this, null);
            form.ShowDialog();
        }

        /// <summary>
        /// 重量调整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemWeightAdjustment_Click(object sender, EventArgs e)
        {
            WeightAdjustmentForm form = new WeightAdjustmentForm(this);
            form.ShowDialog();
        }

        /// <summary>
        /// 重心包线设计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemCoreEnvelopeDesign_Click(object sender, EventArgs e)
        {
            CoreEnvelopeDesignForm form = new CoreEnvelopeDesignForm(this);
            form.ShowDialog();
        }

        /// <summary>
        /// 重心包线裁剪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemCoreEnvelopeCut_Click(object sender, EventArgs e)
        {
            CoreEnvelopeCutForm form = new CoreEnvelopeCutForm(this);
            form.ShowDialog();
        }

        /// <summary>
        /// 修正因子求解
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemComputeCorrectionFactor_Click(object sender, EventArgs e)
        {
            ComputeCorrectionFactorFrm form = new ComputeCorrectionFactorFrm(this);
            form.ShowDialog();
        }

        /// <summary>
        /// 导出重心包线算法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemCenterofGravityEnvelopeMethod_Click(object sender, EventArgs e)
        {
            OutputCenterofGravityEnvelopeMethodFrm form = new OutputCenterofGravityEnvelopeMethodFrm();
            form.ShowDialog();
        }

        /// <summary>
        /// 导出重重量分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemWeightClassfication_Click(object sender, EventArgs e)
        {
            OutputWeightClassficationFrm form = new OutputWeightClassficationFrm();
            form.ShowDialog();
        }

        /// <summary>
        /// 重量分类管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemWeightSortManage_Click(object sender, EventArgs e)
        {
            WeightSortManageForm form = new WeightSortManageForm(this);
            form.ShowDialog();
        }

        /// <summary>
        /// 参数管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemParameterManage_Click(object sender, EventArgs e)
        {
            ParameterManageForm form = new ParameterManageForm();
            form.ShowDialog();
        }

        /// <summary>
        /// 数据库管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemDBManage_Click(object sender, EventArgs e)
        {
            DBManageForm form = new DBManageForm();
            form.ShowDialog();
        }

        /// <summary>
        /// 重量算法管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemWeightAtithmeticManage_Click(object sender, EventArgs e)
        {
            WeightArithmeticManageForm form = new WeightArithmeticManageForm(this);
            form.ShowDialog();
        }

        /// <summary>
        /// 重心包线算法管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemCoreEnvelopeAtithmeticManage_Click(object sender, EventArgs e)
        {
            CoreEnvelopeAtithmeticManageForm form = new CoreEnvelopeAtithmeticManageForm(this);
            form.ShowDialog();
        }

        /// <summary>
        /// 数据库服务器登录设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemDBServerSetting_Click(object sender, EventArgs e)
        {
            DBServerSettingFormcs form = new DBServerSettingFormcs();
            form.ShowDialog();
        }

        #region 文件操作

        /// <summary>
        /// 新建事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemNew_Click(object sender, EventArgs e)
        {
            if (treeViewData.Nodes.Count > 0)
            {
                //有工程提示保存
                DialogResult result = MessageBox.Show("是否保存当前设计工程？", "保存提示", MessageBoxButtons.OKCancel);

                if (result == DialogResult.OK)
                {
                    SaveFileDialog fileDialog = new SaveFileDialog();

                    fileDialog.Filter = "prj文件(*.prj)|(*prj)";
                    fileDialog.RestoreDirectory = true;
                    fileDialog.FilterIndex = 1;
                    fileDialog.FileName = treeViewData.Nodes[0].Text;

                    string strFileName = string.Empty;
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        strFileName = fileDialog.FileName + ".prj";

                        if (File.Exists(strFileName))
                        {
                            DialogResult resultSave = MessageBox.Show("已存在文件\"" + strFileName + "\"" + "是否替换它?", "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            if (resultSave == DialogResult.Yes)
                            {
                                SaveFile(strFileName);
                                XLog.Write("保存文件\"" + strFileName + "\"成功");
                            }
                        }
                        else
                        {
                            SaveFile(strFileName);
                            XLog.Write("保存文件\"" + strFileName + "\"成功");
                        }
                    }
                }

            }

            CoreDesignProject coreForm = new CoreDesignProject(this, "new");
            coreForm.ShowDialog();
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            try
            {

                if (treeViewData.Nodes.Count > 0)
                {
                    DialogResult result = MessageBox.Show("是否保存当前设计工程？", "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        //保存当前工程
                        SaveFileDialog saveFileDialog = new SaveFileDialog();

                        saveFileDialog.Filter = "prj文件(*.prj)|(*prj)";
                        saveFileDialog.RestoreDirectory = true;
                        saveFileDialog.FilterIndex = 1;
                        saveFileDialog.FileName = treeViewData.Nodes[0].Text;

                        string strFileName = string.Empty;
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            strFileName = saveFileDialog.FileName + ".prj";
                            strProjectFile = strFileName;

                            DialogResult resultFile = MessageBox.Show("已存在文件\"" + strFileName + "\"" + "是否替换它?", "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            if (resultFile == DialogResult.Yes)
                            {
                                SaveFile(strFileName);
                                XLog.Write("保存文件\"" + strFileName + "\"成功");
                            }
                        }
                    }
                }

                //打开文件
                OpenFileDialog fileDialog = new OpenFileDialog();

                fileDialog.Filter = "prj文件(*.prj)|*prj";
                fileDialog.RestoreDirectory = true;
                fileDialog.FilterIndex = 1;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    strProjectFile = fileDialog.FileName;
                    //检查文件格式
                    XmlNode node = null;
                    XmlDocument doc = new XmlDocument();
                    doc.Load(fileDialog.FileName);

                    node = doc.SelectSingleNode("PRJ/重量重心设计工程");
                    if (node == null)
                    {
                        XLog.Write("打开文件\"" + fileDialog.FileName + "\"格式错误");
                        return;
                    }

                    DesignProjectData projectData = GetResultData(fileDialog.FileName);
                    BindProjectTreeData(projectData);
                    XLog.Write("打开文件\"" + fileDialog.FileName + "\"成功");

                    InitializePage("new");
                }
            }
            catch (Exception ex)
            {
                XLog.Write("打开文件错误：" + ex.Message);
            }

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strFileName = string.Empty;
                if (treeViewData.Nodes.Count == 0)
                {
                    XLog.Write("无数据保存");
                    return;
                }

                SaveFile(this.strProjectFile);
                XLog.Write("保存文件\"" + strProjectFile + "\"成功");
            }
            catch (Exception ex)
            {
                XLog.Write("保存文件错误：" + ex.Message);
            }

        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAsSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strFileName = string.Empty;
                if (treeViewData.Nodes.Count == 0)
                {
                    XLog.Write("无数据保存");
                    return;
                }

                SaveFileDialog fileDialog = new SaveFileDialog();

                fileDialog.Filter = "prj文件(*.prj)|(*prj)";
                fileDialog.RestoreDirectory = true;
                fileDialog.FilterIndex = 1;
                fileDialog.FileName = treeViewData.Nodes[0].Text;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    strFileName = fileDialog.FileName + ".prj";


                    if (File.Exists(strFileName))
                    {
                        DialogResult result = MessageBox.Show("已存在文件\"" + strFileName + "\"" + "是否替换它?", "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (result == DialogResult.Yes)
                        {
                            this.strProjectFile = strFileName;
                            SaveFile(strFileName);
                            XLog.Write("另存为文件\"" + strFileName + "\"成功");
                        }
                    }
                    else
                    {
                        this.strProjectFile = strFileName;
                        SaveFile(strFileName);
                        XLog.Write("另存为文件\"" + strFileName + "\"成功");
                    }
                }
            }
            catch (Exception ex)
            {
                XLog.Write("另存为文件错误：" + ex.Message);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemClose_Click(object sender, EventArgs e)
        {
            if (treeViewData.Nodes.Count > 0)
            {
                //有工程提示保存
                DialogResult result = MessageBox.Show("是否保存当前设计工程？", "保存提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    SaveFileDialog fileDialog = new SaveFileDialog();

                    fileDialog.Filter = "prj文件(*.prj)|(*prj)";
                    fileDialog.RestoreDirectory = true;
                    fileDialog.FilterIndex = 1;
                    fileDialog.FileName = treeViewData.Nodes[0].Text;

                    string strFileName = string.Empty;
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        strFileName = fileDialog.FileName + ".prj";

                        if (File.Exists(strFileName))
                        {
                            DialogResult resultSave = MessageBox.Show("已存在文件\"" + strFileName + "\"" + "是否替换它?", "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            if (resultSave == DialogResult.Yes)
                            {
                                SaveFile(strFileName);
                                XLog.Write("保存文件\"" + strFileName + "\"成功");
                            }
                        }
                        else
                        {
                            SaveFile(strFileName);
                            XLog.Write("保存文件\"" + strFileName + "\"成功");
                        }
                    }
                    this.Close();
                }
                if (result == DialogResult.No)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        #endregion

        /// <summary>
        /// 节点单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewData_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            selNode = e.Node;
            treeViewData.SelectedNode = e.Node;

            if (selNode.Level == 0)
            {
                treeViewData.ContextMenuStrip = contextMenuStripEditProject;
            }
            if (selNode.Level == 2)
            {
                treeViewData.ContextMenuStrip = contextMenuStripDeleteResult;
            }

            if (selNode.Level == 2 && selNode.Parent.Text == "重量设计结果列表")
            {
                SetWeightDesignTab();
            }

            if (selNode.Level == 2 && selNode.Parent.Text == "重心包线设计结果列表")
            {
                SetCoreEnvelopeDesignTab();
            }

            if (selNode.Level == 2 && selNode.Parent.Text == "重心包线剪裁结果列表")
            {
                SetCoreEnvelopeCutTab();
            }
            if (selNode.Level == 2 && selNode.Parent.Text == "重量调整结果列表")
            {
                SetWeightAdjustTab();
            }
            if (selNode.Level == 2 && selNode.Parent.Text == "重量评估结果列表")
            {
                SetWeightEstimatedTab((WeightAssessResult)selNode.Tag);
            }
            if (selNode.Level == 2 && selNode.Parent.Text == "重心包线评估结果列表")
            {
                SetCoreEstimatedTab((CoreAssessResult)selNode.Tag);
            }
        }

        private void treeViewData_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (selNode.Level == 2 && selNode.Parent.Text == "重量设计结果列表")
            {
                Model.WeightArithmetic weightArithmetic = GetWeightArithmetic();

                WeightEstimateForm form = new WeightEstimateForm(this, weightArithmetic);
                form.ShowDialog();
            }
            if (selNode.Level == 2 && selNode.Parent.Text == "重量调整结果列表")
            {
                WeightAdjustmentResultData resultData = GetWeightAdjustmentData();

                WeightAdjustmentForm form = new WeightAdjustmentForm(this, resultData);
                form.ShowDialog();
            }
            if (selNode.Level == 2 && selNode.Parent.Text == "重量评估结果列表")
            {
                List<WeightAssessResult> warList = this.designProjectData.weightAssessResultList;
                for (int i = 0; i < warList.Count; i++)
                {
                    if (this.treeViewData.SelectedNode.Name == warList[i].resultID)
                    {
                        WeightAssessmentForm form = new WeightAssessmentForm(warList[i]);
                        form.ShowDialog(this);
                        break;
                    }
                }
            }
            if (selNode.Level == 2 && selNode.Parent.Text == "重心包线设计结果列表")
            {
                Model.CoreEnvelopeArithmetic coreArithmetic = GetCoreEnvelopeDesign();

                CoreEnvelopeDesignForm form = new CoreEnvelopeDesignForm(this, coreArithmetic);
                form.ShowDialog();
            }
            if (selNode.Level == 2 && selNode.Parent.Text == "重心包线剪裁结果列表")
            {
                CoreEnvelopeCutResultData coreCut = GetCoreEnvelopeCutResult();

                //if (coreCut.lstCutEnvelopeCore.Count > 0)
                //{
                //    // added 2014 9 11
                //    coreCut.lstCutEnvelopeCore.Add(coreCut.lstCutEnvelopeCore[0]);
                //}

                CoreEnvelopeCutForm form = new CoreEnvelopeCutForm(this, coreCut);
                form.ShowDialog();
            }
            if (selNode.Level == 2 && selNode.Parent.Text == "重心包线评估结果列表")
            {
                List<CoreAssessResult> carList = this.designProjectData.CoreAssessResultList;
                for (int i = 0; i < carList.Count; i++)
                {
                    if (this.treeViewData.SelectedNode.Name == carList[i].resultID)
                    {
                        CoreEnvelopeAssessForm form = new CoreEnvelopeAssessForm(carList[i]);
                        form.ShowDialog(this);
                        break;
                    }
                }
            }
        }

        private void gridDesignResult_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string strText = gridDesignResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (strText == string.Empty)
            {
                gridDesignResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
            }

            //绑定重量分类
            Model.WeightArithmetic weightArithmetic = GetWeightArithmetic();
            if (weightArithmetic != null)
            {
                WeightSortData sortData = GetCurrentSortData(weightArithmetic.ExportDataToWeightSort(), gridDesignResult);

                //设置重量设计结果中,重量分类的值
                weightArithmetic.SetDataFromWeightSort(sortData);

                BindWeightDesignSort(sortData, treeViewSort);

                //绑定图形
                DisplayPiePic(chartWeightDesign, GetPicListWeightData(sortData, gridDesignResult), "重量分布");
            }
        }

        private void gridCoreEnvelopeDesign_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string strText = gridCoreEnvelopeDesign.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (strText == string.Empty)
            {
                gridCoreEnvelopeDesign.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
            }
            //修改重心包线
            UpdateCoreEnvelopeData(GetCoreEnvelopeDesign());

            //绑定重心包线
            BindCoreEnvelopeTreeData();

            //绑定图形
            DataTable table = GetTableCoreEnvelopteData(GetCoreEnvelopeDesign());
            CoreEnvelopeDisplayInPicture(table, zedGraphControlCore);
        }

        private void gridCoreEnvelopeDesign_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("非法数字");
            e.Cancel = true;
        }

        private void gridDesignResult_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (Verification.IsDoubleNumer(e.FormattedValue.ToString()) == false)
            {
                MessageBox.Show("非法数字");
                e.Cancel = true;
            }
        }

        private void tabControlWork_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tabControlWork.TabPages.Remove(tabControlWork.SelectedTab);

            if (tabControlWork.TabPages.Count == 0)
            {
                treeViewData.SelectedNode = null;
                selNode = null;
            }
        }

        private void gridViewCoreEnvelopeCutReuslt_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string strText = gridViewCoreEnvelopeCutReuslt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (strText == string.Empty)
            {
                gridViewCoreEnvelopeCutReuslt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
            }

            //修改重心包线对象
            UpdateCoreEnvelopeCutResult(GetCoreEnvelopeCutResult());
            CoreEnvelopeCutResultData cutData = GetCoreEnvelopeCutResult();


            string strType = GetCoreEnvelopeCutType();
            //绑定重心包线数据
            BindCoreEnvelopeCutTreeData(strType);

            //剪裁重心包线图
            DataTable tableCut = GetTableCoreEnvelopeCutData(cutData, "cut");
            DisplayInPicture(tableCut, zedGraphControlCutCoreEnvelope);

            //绑定对比数据
            DataTable tableBasic = GetTableCoreEnvelopeCutData(cutData, "basic");
            DisplayCompareInPicture(cutData, zedGraphControlCoreCompare);
        }

        private void gridViewCoreEnvelopeCutReuslt_DataError(object sender, System.Windows.Forms.DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("非法数字");
            e.Cancel = true;
        }

        private void btnExportWeightToDB_Click(object sender, EventArgs e)
        {
            if (selNode == null || selNode.Level != 2)
            {
                XLog.Write("没有数据不能发布数据库");
                return;
            }
            else
            {
                Model.WeightArithmetic weightArithmetic = GetWeightArithmetic();

                ReusltIssueDBForm form = new ReusltIssueDBForm("weightDesign", weightArithmetic);
                form.ShowDialog();
            }
        }

        private void btnExportCoreDesignWeightToExel_Click(object sender, EventArgs e)
        {
            if (treeViewCoreEnvelope.Nodes.Count == 0)
            {
                XLog.Write("没有数据不能导出");
                return;
            }
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Xml文件 (*.xml)|*.xml|Excel文件 (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = txtCoreEnvelopeDataName.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dlg.FileName;
                if (strFilePath.EndsWith(".xml"))
                {
                    List<string> lstContent = GetCoreEnvelopeDesignFileContent(gridCoreEnvelopeDesign);
                    CommonFunction.mWriteListStringToFile(strFilePath, lstContent);
                }

                if (strFilePath.EndsWith(".xls"))
                {
                    DataTable table = GetCoreEnvelopeDesignResultTable(gridCoreEnvelopeDesign);
                    int result = XCommon.FileStatus.FileIsOpen(strFilePath);

                    if (result == 1)
                    {
                        MessageBox.Show("请关闭文件" + strFilePath + ",再导出");
                    }
                    else
                    {
                        CommonExcel commonExcel = new CommonExcel();
                        commonExcel.lstColumn = GetCoreEnvelopeDesignResultExcleCloumn();
                        commonExcel.CreateExcel("example", strFilePath, "sheet1", table);
                    }
                }
                XLog.Write("导出文件\"" + strFilePath + "\"成功");
            }
        }

        private void btnExportCoreDesignWeightToDB_Click(object sender, EventArgs e)
        {
            if (treeViewCoreEnvelope.Nodes.Count == 0)
            {
                XLog.Write("没有数据不能发布数据库");
                return;
            }
            else
            {
                ReusltIssueDBForm form = new ReusltIssueDBForm(gridCoreEnvelopeDesign, "coreEnvelopeDesign", txtCoreEnvelopeDataName.Text);
                form.ShowDialog();
            }
        }

        private void btnExportCutWeightToDB_Click(object sender, EventArgs e)
        {
            if (tabControlCoreEnvelopeCut.SelectedTab == tabPageCutCoreEnvelope
               && gridViewCoreEnvelopeCutReuslt.Rows.Count == 0)
            {
                XLog.Write("没有数据不能发布数据库");
                return;
            }

            ReusltIssueDBForm form = new ReusltIssueDBForm(gridViewCoreEnvelopeCutReuslt, "coreEnvelopeCut", txtEnvelopeCutName.Text);
            form.ShowDialog();
        }

        /// <summary>
        /// 发布重量调整数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportAdjustmentWeightToDB_Click(object sender, EventArgs e)
        {

            if (tabControlCoreEnvelopeCut.SelectedTab == tabPageAdjustment
              && gridViewAdjustment.Rows.Count == 0)
            {
                XLog.Write("没有数据不能发布数据库");
                return;
            }

            WeightAdjustmentResultData adjustData = GetWeightAdjustmentData();
            ReusltIssueDBForm form = new ReusltIssueDBForm("weightDesign", adjustData);
            form.ShowDialog();
        }

        private void toolStripMenuItemExportWeightDataToFile_Click(object sender, EventArgs e)
        {
            Model.WeightArithmetic weightArithmetic = GetWeightArithmetic();
            WeightSortData sortData = GetCurrentSortData(weightArithmetic.ExportDataToWeightSort(), gridDesignResult);

            if (sortData == null)
            {
                XLog.Write("没有数据不能导出");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml文件 (*.xml)|*.xml|Excel文件 (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = txtWeightDesingnDBName.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dlg.FileName;
                if (strFilePath.EndsWith(".xml"))
                {
                    List<string> lstContent = GetDesignResultFlieContent(sortData);
                    CommonFunction.mWriteListStringToFile(strFilePath, lstContent);
                }

                if (strFilePath.EndsWith(".xls"))
                {
                    DataTable table = GetDesignResultTable(sortData);
                    int result = XCommon.FileStatus.FileIsOpen(strFilePath);

                    if (result == 1)
                    {
                        MessageBox.Show("请关闭文件" + strFilePath + ",再导出");
                    }
                    else
                    {
                        CommonExcel commonExcel = new CommonExcel();
                        commonExcel.lstColumn = GetDesignResultExcleCloumn();
                        commonExcel.CreateExcel("example", strFilePath, "sheet1", table);
                    }
                }

                XLog.Write("导出文件\"" + strFilePath + "\"成功");
            }
        }

        private void toolStripMenuItemExportWeightToDB_Click(object sender, EventArgs e)
        {
            if (treeViewSort.Nodes.Count == 0)
            {
                XLog.Write("没有数据不能发布数据库");
                return;
            }
            else
            {
                ReusltIssueDBForm form = new ReusltIssueDBForm(gridDesignResult, "weightDesign", txtWeightDesingnDBName.Text);
                form.ShowDialog();
            }
        }

        private void toolStripMenuItemDeleteWeightDesignData_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否删除" + selNode.Text + "?", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                if (selNode.Level == 1)
                {
                    if (selNode.Text == "重量设计结果列表")
                    {
                        designProjectData.lstWeightArithmetic.Clear();
                    }
                    if (selNode.Text == "重心包线设计结果列表")
                    {
                        designProjectData.lstCoreEnvelopeDesign.Clear();
                    }
                    if (selNode.Text == "重心包线剪裁结果列表")
                    {
                        designProjectData.lstCutResultData.Clear();
                    }
                    if (selNode.Text == "重量调整结果列表")
                    {
                        designProjectData.lstAdjustmentResultData.Clear();
                    }
                    if (selNode.Text == "重量评估结果列表")
                    {
                        designProjectData.weightAssessResultList.Clear();
                    }
                    if (selNode.Text == "重心包线评估结果列表")
                    {
                        designProjectData.CoreAssessResultList.Clear();
                    }
                }
                if (selNode.Level == 2)
                {
                    if (selNode.Parent.Text == "重量设计结果列表")
                    {
                        WeightArithmetic arithmetic = GetWeightArithmetic();
                        designProjectData.lstWeightArithmetic.Remove(arithmetic);
                    }
                    if (selNode.Parent.Text == "重心包线设计结果列表")
                    {
                        CoreEnvelopeArithmetic core = GetCoreEnvelopeDesign();
                        designProjectData.lstCoreEnvelopeDesign.Remove(core);
                    }
                    if (selNode.Parent.Text == "重心包线剪裁结果列表")
                    {
                        CoreEnvelopeCutResultData cut = GetCoreEnvelopeCutResult();
                        designProjectData.lstCutResultData.Remove(cut);
                    }
                    if (selNode.Parent.Text == "重量调整结果列表")
                    {
                        WeightAdjustmentResultData adjustResult = GetWeightAdjustmentData();
                        designProjectData.lstAdjustmentResultData.Remove(adjustResult);
                    }
                    if (selNode.Parent.Text == "重量评估结果列表")
                    {
                        WeightAssessResult resultWeight = (WeightAssessResult)selNode.Tag;
                        designProjectData.weightAssessResultList.Remove(resultWeight);
                    }
                    if (selNode.Parent.Text == "重心包线评估结果列表")
                    {
                        CoreAssessResult resultCore = (CoreAssessResult)selNode.Tag;
                        designProjectData.CoreAssessResultList.Remove(resultCore);
                    }
                }

                if (tabControlWork.SelectedTab != null)
                {
                    tabControlWork.TabPages.Remove(tabControlWork.SelectedTab);
                }
                BindProjectTreeData(designProjectData);
            }
        }

        private void toolStripMenuItemExportCoreEnvelopeToFile_Click(object sender, EventArgs e)
        {
            if (treeViewCoreEnvelope.Nodes.Count == 0)
            {
                XLog.Write("没有数据不能导出");
                return;
            }
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml文件 (*.xml)|*.xml|Excel文件 (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = txtCoreEnvelopeDataName.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dlg.FileName;

                if (strFilePath.EndsWith(".xml"))
                {
                    List<string> lstContent = GetCoreEnvelopeDesignFileContent(gridCoreEnvelopeDesign);
                    CommonFunction.mWriteListStringToFile(strFilePath, lstContent);
                }
                if (strFilePath.EndsWith(".xls"))
                {
                    DataTable table = GetCoreEnvelopeDesignResultTable(gridCoreEnvelopeDesign);
                    int result = XCommon.FileStatus.FileIsOpen(strFilePath);

                    if (result == 1)
                    {
                        MessageBox.Show("请关闭文件" + strFilePath + ",再导出");
                    }
                    else
                    {
                        CommonExcel commonExcel = new CommonExcel();
                        commonExcel.lstColumn = GetCoreEnvelopeDesignResultExcleCloumn();
                        commonExcel.CreateExcel("example", strFilePath, "sheet1", table);
                    }
                }
                XLog.Write("导出文件\"" + strFilePath + "\"成功");
            }
        }

        private void toolStripMenuItemCoreEnvelopeToDB_Click(object sender, EventArgs e)
        {
            if (treeViewCoreEnvelope.Nodes.Count == 0)
            {
                XLog.Write("没有数据不能发布数据库");
                return;
            }
            else
            {
                ReusltIssueDBForm form = new ReusltIssueDBForm(gridCoreEnvelopeDesign, "coreEnvelopeDesign", txtCoreEnvelopeDataName.Text);
                form.ShowDialog();
            }
        }

        private void toolStripMenuItemExportCoreEnvelopeCutToFile_Click(object sender, EventArgs e)
        {
            if (tabControlCoreEnvelopeCut.SelectedTab == tabPageBasicCoreEnvelope
               && treeViewBasicCoreEnvelope.Nodes.Count == 0)
            {
                XLog.Write("没有数据不能导出");
                return;
            }

            if (tabControlCoreEnvelopeCut.SelectedTab == tabPageCutCoreEnvelope
               && treeViewCutCoreEnvelope.Nodes.Count == 0)
            {
                XLog.Write("没有数据不能导出");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml文件 (*.xml)|*.xml|Excel文件 (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = txtEnvelopeCutName.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dlg.FileName;

                if (strFilePath.EndsWith(".xml"))
                {
                    List<string> lstContent = GetCoreEnvelopeDesignFileContent(gridViewCoreEnvelopeCutReuslt);
                    CommonFunction.mWriteListStringToFile(strFilePath, lstContent);
                }
                if (strFilePath.EndsWith(".xml"))
                {
                    DataTable table = GetCoreEnvelopeDesignResultTable(gridViewCoreEnvelopeCutReuslt);
                    int result = XCommon.FileStatus.FileIsOpen(strFilePath);

                    if (result == 1)
                    {
                        MessageBox.Show("请关闭文件" + strFilePath + ",再导出");
                    }
                    else
                    {
                        CommonExcel commonExcel = new CommonExcel();
                        commonExcel.lstColumn = GetCoreEnvelopeDesignResultExcleCloumn();
                        commonExcel.CreateExcel("example", strFilePath, "sheet1", table);
                    }
                }
                XLog.Write("导出文件\"" + strFilePath + "\"成功");
            }
        }

        private void toolStripMenuItemExportCoreEnvelopeCutToDB_Click(object sender, EventArgs e)
        {
            if (tabControlCoreEnvelopeCut.SelectedTab == tabPageBasicCoreEnvelope
               && treeViewBasicCoreEnvelope.Nodes.Count == 0)
            {
                XLog.Write("没有数据不能发布数据库");
                return;
            }

            if (tabControlCoreEnvelopeCut.SelectedTab == tabPageCutCoreEnvelope
               && treeViewCutCoreEnvelope.Nodes.Count == 0)
            {
                XLog.Write("没有数据不能发布数据库");
                return;
            }

            ReusltIssueDBForm form = new ReusltIssueDBForm(gridViewCoreEnvelopeCutReuslt, "coreEnvelopeCut", txtEnvelopeCutName.Text);
            form.ShowDialog();
        }

        private void btnExportWeightToFile_Click(object sender, EventArgs e)
        {
            Model.WeightArithmetic weightArithmetic = GetWeightArithmetic();
            WeightSortData sortData = GetCurrentSortData(weightArithmetic.ExportDataToWeightSort(), gridDesignResult);

            if (sortData == null)
            {
                XLog.Write("没有数据不能导出");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml文件 (*.xml)|*.xml|Excel文件 (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = txtWeightDesingnDBName.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dlg.FileName;

                if (strFilePath.EndsWith(".xml"))
                {
                    List<string> lstContent = GetDesignResultFlieContent(sortData);
                    CommonFunction.mWriteListStringToFile(strFilePath, lstContent);
                }

                if (strFilePath.EndsWith(".xls"))
                {
                    DataTable table = GetDesignResultTable(sortData);
                    int result = XCommon.FileStatus.FileIsOpen(strFilePath);

                    if (result == 1)
                    {
                        MessageBox.Show("请关闭文件" + strFilePath + ",再导出");
                    }
                    else
                    {
                        CommonExcel commonExcel = new CommonExcel();
                        commonExcel.lstColumn = GetDesignResultExcleCloumn();
                        commonExcel.CreateExcel("example", strFilePath, "sheet1", table);
                    }
                }

                XLog.Write("导出文件\"" + strFilePath + "\"成功");
            }
        }

        private void treeViewData_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void tabControlWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage tabPage = tabControlWork.SelectedTab;

            TreeNode rootNode = null;
            if (treeViewData.Nodes.Count > 0)
            {
                rootNode = treeViewData.Nodes[0];
            }

            if (tabPage != null && rootNode != null)
            {
                //重量设计
                if (tabPage.Name == tabPageWeightDesign.Name)
                {
                    foreach (TreeNode parentNode in rootNode.Nodes)
                    {
                        if (parentNode.Text == "重量设计结果列表")
                        {
                            foreach (TreeNode node in parentNode.Nodes)
                            {
                                if (node.ToolTipText == tabPage.ToolTipText)
                                {
                                    treeViewData.SelectedNode = node;
                                    selNode = node;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }

                //重心包线设计
                if (tabPage.Name == tabPageEnvelopeDesign.Name)
                {
                    foreach (TreeNode parentNode in rootNode.Nodes)
                    {
                        if (parentNode.Text == "重心包线设计结果列表")
                        {
                            foreach (TreeNode node in parentNode.Nodes)
                            {
                                if (node.ToolTipText == tabPage.ToolTipText)
                                {
                                    treeViewData.SelectedNode = node;
                                    selNode = node;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }

                //重心包线剪裁
                if (tabPage.Name == tabPageEnvelopeCut.Name)
                {
                    foreach (TreeNode parentNode in rootNode.Nodes)
                    {
                        if (parentNode.Text == "重心包线剪裁结果列表")
                        {
                            foreach (TreeNode node in parentNode.Nodes)
                            {
                                if (node.ToolTipText == tabPage.ToolTipText)
                                {
                                    treeViewData.SelectedNode = node;
                                    selNode = node;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }

                //重量调整
                if (tabPage.Name == tabPageAdjustment.Name)
                {
                    foreach (TreeNode parentNode in rootNode.Nodes)
                    {
                        if (parentNode.Text == "重量调整结果列表")
                        {
                            foreach (TreeNode node in parentNode.Nodes)
                            {
                                if (node.ToolTipText == tabPage.ToolTipText)
                                {
                                    treeViewData.SelectedNode = node;
                                    selNode = node;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }

                //重量评估
                if (tabPage.Name == tabPageWeightAssessment.Name)
                {
                    foreach (TreeNode parentNode in rootNode.Nodes)
                    {
                        if (parentNode.Text == "重量评估结果列表")
                        {
                            foreach (TreeNode node in parentNode.Nodes)
                            {
                                if (node.ToolTipText == tabPageWeightAssessment.ToolTipText)
                                {
                                    treeViewData.SelectedNode = node;
                                    selNode = node;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }

                //重心评估
                if (tabPage.Name == tabPageCoreEnvelopeEstimated.Name)
                {
                    foreach (TreeNode parentNode in rootNode.Nodes)
                    {
                        if (parentNode.Text == "重心包线评估结果列表")
                        {
                            foreach (TreeNode node in parentNode.Nodes)
                            {
                                if (node.ToolTipText == tabPageWeightAssessment.ToolTipText)
                                {
                                    treeViewData.SelectedNode = node;
                                    selNode = node;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void btnExportCutWeightToDataFile_Click(object sender, EventArgs e)
        {
            if (tabControlCoreEnvelopeCut.SelectedTab == tabPageBasicCoreEnvelope
               && treeViewBasicCoreEnvelope.Nodes.Count == 0)
            {
                XLog.Write("没有数据不能导出");
                return;
            }

            if (tabControlCoreEnvelopeCut.SelectedTab == tabPageCutCoreEnvelope
               && treeViewCutCoreEnvelope.Nodes.Count == 0)
            {
                XLog.Write("没有数据不能导出");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml文件 (*.xml)|*.xml|Excel文件 (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = txtEnvelopeCutName.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dlg.FileName;

                if (strFilePath.EndsWith(".xml"))
                {
                    List<string> lstContent = GetCoreEnvelopeDesignFileContent(gridViewCoreEnvelopeCutReuslt);
                    CommonFunction.mWriteListStringToFile(strFilePath, lstContent);
                }
                if (strFilePath.EndsWith(".xls"))
                {
                    DataTable table = GetCoreEnvelopeDesignResultTable(gridViewCoreEnvelopeCutReuslt);
                    int result = XCommon.FileStatus.FileIsOpen(strFilePath);

                    if (result == 1)
                    {
                        MessageBox.Show("请关闭文件" + strFilePath + ",再导出");
                    }
                    else
                    {
                        CommonExcel commonExcel = new CommonExcel();
                        commonExcel.lstColumn = GetCoreEnvelopeDesignResultExcleCloumn();
                        commonExcel.CreateExcel("example", strFilePath, "sheet1", table);
                    }
                }
                XLog.Write("导出文件\"" + strFilePath + "\"成功");
            }
        }

        private void tabControlCoreEnvelopeCut_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strType = GetCoreEnvelopeCutType();
            if (strType == "basic")
            {
                gruCoreEnvelopeCutReuslt.Text = "基础重心包线结果";
            }
            else
            {
                gruCoreEnvelopeCutReuslt.Text = "剪裁重心包线结果";
            }

            //绑定GridView
            CoreEnvelopeCutResultData cutData = GetCoreEnvelopeCutResult();
            BindCoreEnvelopeCutGridView(cutData, strType);

            //绑定重心包线数据
            BindCoreEnvelopeCutTreeData(strType);
        }

        private void tabControlAdjustmentSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            WeightAdjustmentResultData resultData = GetWeightAdjustmentData();

            if (resultData != null)
            {
                //基础数据
                if (tabControlAdjustmentSort.SelectedTab == tabPageBasicWeight)
                {
                    DisplayPiePic(chartWeightAdjust, GetPicListWeightData(resultData.basicWeightData, gridViewAdjustment, "基础重量数据"), "基础重量");
                }
                //调整数据
                if (tabControlAdjustmentSort.SelectedTab == tabPageAdjustmentWeight)
                {
                    DisplayPiePic(chartWeightAdjust, GetPicListWeightData(resultData.weightAdjustData, gridViewAdjustment, "调整重量数据"), "调整重量");
                }
            }
        }

        /// <summary>
        /// 导出调整后的重量数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportAdjustmentWeightToDataFile_Click(object sender, EventArgs e)
        {
            WeightAdjustmentResultData adjustData = GetWeightAdjustmentData();
            WeightSortData sortData = adjustData.weightAdjustData;

            if (sortData == null)
            {
                XLog.Write("没有数据不能导出");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml文件 (*.xml)|*.xml|Excel文件 (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = txtAdjustmentDataName.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = dlg.FileName;

                if (strFilePath.EndsWith(".xml"))
                {
                    List<string> lstContent = GetDesignResultFlieContent(sortData);
                    CommonFunction.mWriteListStringToFile(strFilePath, lstContent);
                }

                if (strFilePath.EndsWith(".xls"))
                {
                    DataTable table = GetDesignResultTable(sortData);
                    int result = XCommon.FileStatus.FileIsOpen(strFilePath);

                    if (result == 1)
                    {
                        MessageBox.Show("请关闭文件" + strFilePath + ",再导出");
                    }
                    else
                    {
                        CommonExcel commonExcel = new CommonExcel();
                        commonExcel.lstColumn = GetDesignResultExcleCloumn();
                        commonExcel.CreateExcel("example", strFilePath, "sheet1", table);
                    }
                }

                XLog.Write("导出文件\"" + strFilePath + "\"成功");
            }
        }

        private void gridViewAdjustment_DataError(object sender, System.Windows.Forms.DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("非法数字");
            e.Cancel = true;
        }

        private void gridViewAdjustment_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string strText = gridViewAdjustment.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (strText == string.Empty)
            {
                gridViewAdjustment.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
            }
            //调整重量数据
            UpdateAdjustData(GetWeightAdjustmentData());

            WeightAdjustmentResultData resultData = GetWeightAdjustmentData();

            //绑定重量分类
            BindWeightDesignSort(resultData.basicWeightData, treeViewBasicSort);
            BindWeightDesignSort(resultData.weightAdjustData, treeViewAdjustmentSort);

            //圆饼图显示
            //基础数据
            if (tabControlAdjustmentSort.SelectedTab == tabPageBasicWeight)
            {
                DisplayPiePic(chartWeightAdjust, GetPicListWeightData(resultData.basicWeightData, gridViewAdjustment, "基础重量数据"), "基础重量");
            }
            //调整数据
            if (tabControlAdjustmentSort.SelectedTab == tabPageAdjustmentWeight)
            {
                DisplayPiePic(chartWeightAdjust, GetPicListWeightData(resultData.weightAdjustData, gridViewAdjustment, "调整重量数据"), "调整重量");
            }

            //对比图显示
            DisplayAdjustmentInPic(WeightSortData.GetListWeightData(resultData.basicWeightData), WeightSortData.GetListWeightData(resultData.weightAdjustData));
        }

        private void gridCoreEstimatedResult_DataError(object sender, System.Windows.Forms.DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("非法数字");
            e.Cancel = true;
        }

        #region 发布至Tde/Ide

        /// <summary>
        /// 重量设计结果发布TDE/IDE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWeightDesignPubilishToTde_Click(object sender, EventArgs e)
        {
            if (PubSyswareCom.IsRuntimeServerStarted())
            {
                WeightArithmetic result = GetWeightArithmetic();

                if (result != null)
                {
                    //PubSyswareCom.CreateStringParameter("weightData", string.Empty, true, true, false);
                    //PubSyswareCom.SetParameterGroup("weightData", "重量数据");

                    //string strContent = WeightDataMangeForm.GetMainSystemWeight(result.ExportDataToWeightSort());
                    //PubSyswareCom.mSetParameter(string.Empty, "weightData", strContent);

                    //创建结构化参数
                    PubSyswareCom.CreateStructParameter(string.Empty, "weightData", "sortdata", true, true, false);
                    PubSyswareCom.SetParameterGroup("weightData", "重量数据");

                    WeightSortData sortData = result.ExportDataToWeightSort();
                    PubSyswareCom.SetParameter(string.Empty, "weightData.name", sortData.sortName);

                    List<WeightData> lstWeightData = sortData.lstWeightData;
                    for (int i = 0; i < lstWeightData.Count; i++)
                    {
                        PubSyswareCom.SetParameter(string.Empty, "weightData.lstWeightData[" + i.ToString() + "].ID", lstWeightData[i].nID);
                        PubSyswareCom.SetParameter(string.Empty, "weightData.lstWeightData[" + i.ToString() + "].name", lstWeightData[i].weightName);
                        PubSyswareCom.SetParameter(string.Empty, "weightData.lstWeightData[" + i.ToString() + "].unit", lstWeightData[i].weightUnit);
                        PubSyswareCom.SetParameter(string.Empty, "weightData.lstWeightData[" + i.ToString() + "].value", lstWeightData[i].weightValue);
                        PubSyswareCom.SetParameter(string.Empty, "weightData.lstWeightData[" + i.ToString() + "].remark", lstWeightData[i].strRemark);
                        PubSyswareCom.SetParameter(string.Empty, "weightData.lstWeightData[" + i.ToString() + "].ParentID", lstWeightData[i].nParentID);
                    }


                    XLog.Write("重量数据发布TDE/IDE成功");
                }
            }
        }

        /// <summary>
        /// 重量调整结果发布至Tde/Ide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWeightAdjustPublishToTde_Click(object sender, EventArgs e)
        {
            if (PubSyswareCom.IsRuntimeServerStarted())
            {
                WeightAdjustmentResultData adjustResult = GetWeightAdjustmentData();

                if (adjustResult != null && adjustResult.weightAdjustData != null)
                {
                    PubSyswareCom.CreateStringParameter("weightData", string.Empty, true, true, false);
                    PubSyswareCom.SetParameterGroup("weightData", "重量数据");

                    string strContent = WeightDataMangeForm.GetMainSystemWeight(adjustResult.weightAdjustData);


                    PubSyswareCom.mSetParameter(string.Empty, "weightData", strContent);

                    XLog.Write("重量数据发布TDE/IDE成功");
                }
            }
        }

        /// <summary>
        /// 重心包线设计结果发布至Tde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCoreEnvelopeDesignPublishToTde_Click(object sender, EventArgs e)
        {
            if (PubSyswareCom.IsRuntimeServerStarted())
            {
                CoreEnvelopeArithmetic coreEnvelope = GetCoreEnvelopeDesign();


                if (coreEnvelope != null)
                {
                    PubSyswareCom.CreateStringParameter("coreEnvelopeData", string.Empty, true, true, false);
                    PubSyswareCom.SetParameterGroup("coreEnvelopeData", "重心包线数据");

                    string strValue = CoreEnvelopeDesignManageForm.GetCoreEnvelope(gridCoreEnvelopeDesign);


                    PubSyswareCom.mSetParameter(string.Empty, "coreEnvelopeData", strValue);
                    XLog.Write("重心包线数据发布TDE/IDE成功");
                }
            }
        }

        /// <summary>
        /// 重心包线剪裁结果发布至Tde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCoreEnvelopeCutPublishToTde_Click(object sender, EventArgs e)
        {
            if (PubSyswareCom.IsRuntimeServerStarted())
            {
                CoreEnvelopeCutResultData coreCut = GetCoreEnvelopeCutResult();

                if (coreCut != null)
                {
                    PubSyswareCom.CreateStringParameter("coreEnvelopeData", string.Empty, true, true, false);
                    PubSyswareCom.SetParameterGroup("coreEnvelopeData", "重心包线数据");

                    string strValue = GetCoreEnvelope(coreCut.lstCutEnvelopeCore);


                    PubSyswareCom.mSetParameter(string.Empty, "coreEnvelopeData", strValue);
                    XLog.Write("重心包线数据发布TDE/IDE成功");
                }
            }
        }

        #endregion

        /// <summary>
        /// 关于事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        #region 饼图拆分

        private void chartWeightDesign_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult result = chartWeightDesign.HitTest(e.X, e.Y);
            if (result.PointIndex < 0)
            {
                return;
            }

            bool exploded = false;

            if (chartWeightDesign.Series.Count > 0)
            {
                if (chartWeightDesign.Series[0].Points[result.PointIndex].CustomProperties == "Exploded=true")
                {
                    exploded = true;
                }
                else
                {
                    exploded = false;
                }

                foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint in chartWeightDesign.Series[0].Points)
                {
                    tpoint.CustomProperties = "";
                    if (exploded)
                    {
                        return;
                    }
                }

                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint = chartWeightDesign.Series[0].Points[result.PointIndex];
                    tpoint.CustomProperties = "Exploded = true";

                }
                if (result.ChartElementType == ChartElementType.LegendItem)
                {
                    System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint = chartWeightDesign.Series[0].Points[result.PointIndex];
                    tpoint.CustomProperties = "Exploded = true";
                }
            }
        }

        private void chartWeightDesign_MouseMove(object sender, MouseEventArgs e)
        {
            HitTestResult result = chartWeightDesign.HitTest(e.X, e.Y);

            if (chartWeightDesign.Series.Count > 0)
            {
                foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint in chartWeightDesign.Series[0].Points)
                {
                    tpoint.BackSecondaryColor = Color.Black;
                    tpoint.BackHatchStyle = ChartHatchStyle.None;
                    tpoint.BorderWidth = 1;
                }

                if (result.ChartElementType == ChartElementType.DataPoint || result.ChartElementType == ChartElementType.LegendItem)
                {
                    chartWeightDesign.Cursor = Cursors.Hand;
                    System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint = chartWeightDesign.Series[0].Points[result.PointIndex];

                    tpoint.BackSecondaryColor = Color.White;

                    tpoint.BackHatchStyle = ChartHatchStyle.Percent25;

                    tpoint.BorderWidth = 2;
                }
                else
                {
                    chartWeightDesign.Cursor = Cursors.Default;
                }

            }
        }

        private void chartWeightAdjust_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult result = chartWeightAdjust.HitTest(e.X, e.Y);
            if (result.PointIndex < 0)
            {
                return;
            }

            bool exploded = false;

            if (chartWeightAdjust.Series.Count > 0)
            {
                if (chartWeightAdjust.Series[0].Points[result.PointIndex].CustomProperties == "Exploded=true")
                {
                    exploded = true;
                }
                else
                {
                    exploded = false;
                }

                foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint in chartWeightAdjust.Series[0].Points)
                {
                    tpoint.CustomProperties = "";
                    if (exploded)
                    {
                        return;
                    }
                }

                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint = chartWeightAdjust.Series[0].Points[result.PointIndex];
                    tpoint.CustomProperties = "Exploded = true";

                }
                if (result.ChartElementType == ChartElementType.LegendItem)
                {
                    System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint = chartWeightAdjust.Series[0].Points[result.PointIndex];
                    tpoint.CustomProperties = "Exploded = true";
                }
            }
        }

        private void chartWeightAdjust_MouseMove(object sender, MouseEventArgs e)
        {
            HitTestResult result = chartWeightAdjust.HitTest(e.X, e.Y);

            if (chartWeightAdjust.Series.Count > 0)
            {
                foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint in chartWeightAdjust.Series[0].Points)
                {
                    tpoint.BackSecondaryColor = Color.Black;
                    tpoint.BackHatchStyle = ChartHatchStyle.None;
                    tpoint.BorderWidth = 1;
                }

                if (result.ChartElementType == ChartElementType.DataPoint || result.ChartElementType == ChartElementType.LegendItem)
                {
                    chartWeightDesign.Cursor = Cursors.Hand;
                    System.Windows.Forms.DataVisualization.Charting.DataPoint tpoint = chartWeightAdjust.Series[0].Points[result.PointIndex];

                    tpoint.BackSecondaryColor = Color.White;

                    tpoint.BackHatchStyle = ChartHatchStyle.Percent25;

                    tpoint.BorderWidth = 2;
                }
                else
                {
                    chartWeightDesign.Cursor = Cursors.Default;
                }
            }
        }

        #endregion

        #region 图片保存

        /// <summary>
        /// 重量设计保存图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemSaveImgeToFile_Click(object sender, EventArgs e)
        {
            SavePictureToFile(chartWeightDesign);
        }

        /// <summary>
        /// 重量调整基础重量图片保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAdjustImage_Click(object sender, EventArgs e)
        {
            SavePictureToFile(chartWeightAdjust);
        }

        /// <summary>
        /// 重量调整对比柱状图片保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAdjustCompareImage_Click(object sender, EventArgs e)
        {
            SavePictureToFile(chartAdjustment);
        }

        #endregion

        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void gridWeightEstimateResult_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string strText = gridWeightEstimateResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (strText == string.Empty)
            {
                gridWeightEstimateResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
            }

            //更新重量评估对象
            UpDateWeighEstimatedData();

            WeightAssessResult result = (WeightAssessResult)selNode.Tag;

            txtAdvancedSum.Text = Math.Round(result.advancedInflationTotal, digit).ToString();
            txtRationalitySum.Text = Math.Round(result.rationalityInflationTotal, digit).ToString();

            //合理性评估
            CommonUtil.DisplayAssessLinePic(chartRationalityAssess, result.weightAssessParamList, "合理性评估");
            //先进性评估
            CommonUtil.DisplayAssessLinePic(chartAdvancedAssess, result.weightAssessParamList, "先进性评估");
        }

        private void gridWeightEstimateResult_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("非法数字");
            e.Cancel = true;
        }

        private void gridCoreEstimatedResult_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string text = gridCoreEstimatedResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (text == string.Empty)
            {
                gridCoreEstimatedResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
            }

            //更新
            UpdateCoreEstimatedResult();

            CoreAssessResult result = (CoreAssessResult)selNode.Tag;
            //绑定待评估重心包线
            BindCoreEstimatedTree(treeEstimatedCore, "estimate", result);

            DisplayCoreEstimatedPic(result, zedGraphCoreEstimated, true);
        }

        /// <summary>
        /// 导出重心评估数据至数据文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExprotWeighEstimatedData_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml文件 (*.xml)|*.xml|Excel文件 (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = txtEstimateName.Text.Trim();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CommonUtil com = new CommonUtil();
                WeightAssessResult result = (WeightAssessResult)selNode.Tag;

                if (dlg.FileName.EndsWith(".xml"))
                {
                    com.ExportWeightEstimatedToXml(dlg.FileName, result);
                }
                if (dlg.FileName.EndsWith(".xls"))
                {
                    com.ExportWeightEstimatedToExcle(dlg.FileName, result);
                }
                XLog.Write("导出文件\"" + dlg.FileName + "\"成功");
            }
        }

        /// <summary>
        ///  发布重量评估数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWeightCorePublish_Click(object sender, EventArgs e)
        {
            if (PubSyswareCom.IsRuntimeServerStarted())
            {
                WeightAssessResult result = (WeightAssessResult)selNode.Tag;
                if (result != null)
                {
                    PubSyswareCom.CreateStringParameter("weightEstimatedData", string.Empty, true, true, false);
                    PubSyswareCom.SetParameterGroup("weightEstimatedData", "重量评估数据");

                    string strContent = CommonUtil.GetWeightEstimatedDataToString(result);

                    PubSyswareCom.mSetParameter(string.Empty, "weightEstimatedData", strContent);

                    XLog.Write("重量评估数据发布TDE/IDE成功");
                }
            }
        }

        private void btnExportCoreEstimated_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml文件 (*.xml)|*.xml|Excel文件 (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.FileName = txtCoreDataName.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CommonUtil com = new CommonUtil();
                CoreAssessResult result = (CoreAssessResult)selNode.Tag;

                if (dlg.FileName.EndsWith(".xml"))
                {
                    com.ExportCoreEstimatedToXml(dlg.FileName, result);
                }
                if (dlg.FileName.EndsWith(".xls"))
                {
                    com.ExportCoreEstimatedToExcle(dlg.FileName, result);
                }
                XLog.Write("导出文件\"" + dlg.FileName + "\"成功");
            }
        }

        /// <summary>
        /// 重心数据发布TDE/IDE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCoreEstimatedPulish_Click(object sender, EventArgs e)
        {
            if (PubSyswareCom.IsRuntimeServerStarted())
            {
                CoreAssessResult result = (CoreAssessResult)selNode.Tag;
                if (result != null)
                {
                    PubSyswareCom.CreateStringParameter("coreEstimatedData", string.Empty, true, true, false);
                    PubSyswareCom.SetParameterGroup("coreEstimatedData", "重心评估数据");

                    string strContent = CommonUtil.GetCoreEstimatedDataToString(result);

                    PubSyswareCom.mSetParameter(string.Empty, "coreEstimatedData", strContent);

                    XLog.Write("重心评估数据发布TDE/IDE成功");
                }
            }
        }

        #endregion

        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            txtLog.SelectionStart = this.txtLog.Text.Length;
            txtLog.SelectionLength = 0;
            txtLog.ScrollToCaret();
        }

        private void treeViewData_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selNode = e.Node;
        }

        private void BasicDBSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateSetForm form = new TemplateSetForm();
            form.ShowDialog();
        }

        private void ToolStripMenuItemWeightAssessment_Click(object sender, EventArgs e)
        {
            new WeightAssessmentForm().ShowDialog(this);
        }

        private void ToolStripMenuItemCore_Click(object sender, EventArgs e)
        {
            if (this.designProjectData.CoreAssessResultList == null)
            {
                this.designProjectData.CoreAssessResultList = new List<CoreAssessResult>();
            }
            new CoreEnvelopeAssessForm().ShowDialog(this);
        }

        private void toolStripMenuItemWeightCompare_Click(object sender, EventArgs e)
        {
            SavePictureToFile(chartWeightCompare);
        }

        private void toolStripMenuRationalityAssess_Click(object sender, EventArgs e)
        {
            SavePictureToFile(chartRationalityAssess);
        }

        private void toolStripMenuItemAdvancedAssess_Click(object sender, EventArgs e)
        {
            SavePictureToFile(chartAdvancedAssess);
        }

        private void wDMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new WDMSettingForm().ShowDialog(this);
        }
    }
}