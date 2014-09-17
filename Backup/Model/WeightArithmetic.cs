using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace Model
{
    public class WeightArithmetic
    {
        public WeightArithmetic()
        {
            DataName = "";
            Name = "";
            SortName = "";
            CreateTime = "";
            LastModifyTime = "";
            Remark = "";
            FormulaList = new List<WeightFormula>();
        }
        public string DataName { get; set; }
        public string Name { get; set; }
        public string SortName { get; set; }
        public string CreateTime { get; set; }
        public string LastModifyTime { get; set; }
        public string Remark { get; set; }
        public List<WeightFormula> FormulaList { get; set; }

        public WeightArithmetic Clone()
        {
            WeightArithmetic weight = new WeightArithmetic();

            weight.DataName = this.DataName;
            weight.Name = this.Name;
            weight.SortName = this.SortName;
            weight.CreateTime = this.CreateTime;
            weight.LastModifyTime = this.LastModifyTime;
            weight.Remark = this.Remark;

            List<WeightFormula> lstWeightFormula = new List<WeightFormula>();
            foreach(WeightFormula formula in FormulaList)
            {
                lstWeightFormula.Add(formula.Clone());
            }
            weight.FormulaList = lstWeightFormula;

            return weight;
        }

        static public WeightArithmetic ReadArithmeticData(string filename)
        {
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(filename);
            }
            catch
            {
                MessageBox.Show("打开文件错误!");
                return null;
            }


            XmlNode xmlnode = doc.SelectSingleNode("重量算法/算法名称");

            if (xmlnode == null)
            {
                MessageBox.Show("错误的算法文件!");
                return null;
            }

            WeightArithmetic wa = new WeightArithmetic();

            wa.Name = xmlnode.InnerText;

            xmlnode = doc.SelectSingleNode("重量算法/算法创建时间");
            wa.CreateTime = xmlnode.InnerText;
            xmlnode = doc.SelectSingleNode("重量算法/重量分类");
            wa.SortName = xmlnode.InnerText;
            xmlnode = doc.SelectSingleNode("重量算法/算法最后修改时间");
            wa.LastModifyTime = xmlnode.InnerText;
            xmlnode = doc.SelectSingleNode("重量算法/算法备注");
            wa.Remark = xmlnode.InnerText;
            xmlnode = doc.SelectSingleNode("重量算法/公式列表");

            Dictionary<WeightParameter, WeightParameter> wpDict = new Dictionary<WeightParameter, WeightParameter>();

            foreach (XmlNode xmlsubnode in xmlnode.ChildNodes)
            {
                WeightFormula wf = new WeightFormula();
                wf.NodePath = xmlsubnode.ChildNodes[0].InnerText;
                wf.Formula = xmlsubnode.ChildNodes[1].InnerText;

                foreach (XmlNode paranode in xmlsubnode.ChildNodes[2].ChildNodes)
                {
                    string name = paranode.ChildNodes[0].InnerText;
                    string unit = paranode.ChildNodes[2].InnerText;

                    WeightParameter wpGlobal = FindGlobleParameters(name, unit)[0];

                    WeightParameter wp = null;
                    if (wpGlobal == null)
                    {
                        wp = new WeightParameter();
                        wp.ParaName = name;
                        wp.ParaUnit = unit;
                        wp.ParaType = 10;// int.Parse(paranode.ChildNodes[2].InnerText);
                        wp.ParaValue = double.Parse(paranode.ChildNodes[1].InnerText);
                        wp.ParaRemark = paranode.ChildNodes[4].InnerText;

                        WeightParameter temp10wp = new WeightParameter(wp);

                        WeightParameter.GetWeightParameterList()[10].Add(temp10wp);

                        wpDict.Add(temp10wp, wp);
                    }
                    else
                    {
                        if (!wpDict.ContainsKey(wpGlobal))
                        {
                            wp = new WeightParameter(wpGlobal);
                            wpDict.Add(wpGlobal, wp);
                        }
                        else
                        {
                            wp = wpDict[wpGlobal];
                        }
                    }
                    wf.ParaList.Add(wp);
                }

                if (xmlsubnode.ChildNodes.Count >= 4)
                {
                    wf.Value = double.Parse(xmlsubnode.ChildNodes[3].InnerText);
                }

                wa.FormulaList.Add(wf);
            }

            return wa;
        }

        public bool WriteArithmeticFile(string filepath, bool bOverWritePrompt)
        {
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);

            if (bOverWritePrompt)
            {
                if (System.IO.File.Exists(filepath))
                {
                    if (MessageBox.Show("文件\"" + filepath + "\"已存在，是否覆盖？", "文件已存在", MessageBoxButtons.YesNo) == DialogResult.No)
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

            writeXml.WriteStartElement("重量算法");
            {
                writeXml.WriteStartElement("算法名称");
                writeXml.WriteString(Name);
                writeXml.WriteEndElement();

                writeXml.WriteStartElement("重量分类");
                writeXml.WriteString(SortName);
                writeXml.WriteEndElement();

                writeXml.WriteStartElement("算法创建时间");
                writeXml.WriteString(CreateTime);
                writeXml.WriteEndElement();

                writeXml.WriteStartElement("算法最后修改时间");
                writeXml.WriteString(LastModifyTime);
                writeXml.WriteEndElement();

                writeXml.WriteStartElement("算法备注");
                writeXml.WriteString(Remark);
                writeXml.WriteEndElement();

                writeXml.WriteStartElement("公式列表");
                {
                    foreach (WeightFormula wf in FormulaList)
                    {
                        if (wf.nAttach == 0)
                        {
                            continue;
                        }
                        writeXml.WriteStartElement("公式");
                        {
                            writeXml.WriteStartElement("节点路径");
                            writeXml.WriteString(wf.NodePath);
                            writeXml.WriteEndElement();
                            writeXml.WriteStartElement("公式");
                            writeXml.WriteString(wf.Formula);
                            writeXml.WriteEndElement();

                            writeXml.WriteStartElement("参数表");
                            {
                                foreach (WeightParameter wp in wf.ParaList)
                                {
                                    writeXml.WriteStartElement("参数");
                                    {
                                        writeXml.WriteStartElement("参数名称");
                                        writeXml.WriteString(wp.ParaName);
                                        writeXml.WriteEndElement();
                                        writeXml.WriteStartElement("参数数值");
                                        writeXml.WriteValue(wp.ParaValue);
                                        writeXml.WriteEndElement();
                                        writeXml.WriteStartElement("单位");
                                        writeXml.WriteString(wp.ParaUnit);
                                        writeXml.WriteEndElement();
                                        writeXml.WriteStartElement("参数分类");
                                        writeXml.WriteValue(wp.ParaType);
                                        writeXml.WriteEndElement();
                                        writeXml.WriteStartElement("备注");
                                        writeXml.WriteString(wp.ParaRemark);
                                        writeXml.WriteEndElement();
                                    }
                                    writeXml.WriteEndElement();
                                }
                            }
                            writeXml.WriteEndElement();

                            writeXml.WriteStartElement("结果数值");
                            writeXml.WriteValue(wf.Value);
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

        public List<WeightParameter> GetParaList()
        {
            List<WeightParameter> paraList = new List<WeightParameter>();
            foreach (WeightFormula wf in FormulaList)
            {
                if (wf.nAttach == 1)
                {
                    paraList.AddRange(
                        from item in wf.ParaList
                        where !paraList.Contains(item)
                        select item
                        );
                }
            }

            return paraList.OrderBy(l => l.ParaType).ToList();
        }

        public bool MatchWeightSort(WeightSortData wsd, bool bMatchName)
        {
            if (bMatchName)
            {
                if (Name != wsd.sortName)
                {
                    return false;
                }
            }

            List<string> NodeNames = new List<string>();
            wsd.GetFinalNodeNameList(-1, ref NodeNames, "");

            int nformulanum = 0;
            foreach (WeightFormula wf in FormulaList)
            {
                nformulanum += wf.nAttach;
            }

            bool bRet = false;
            if (NodeNames.Count == nformulanum)
            {
                foreach (WeightFormula wf in FormulaList)
                {
                    if (wf.nAttach == 1)
                    {
                        if (!NodeNames.Contains(wf.NodePath))
                        {
                            break;
                        }
                    }
                }
                bRet = true;
            }

            return bRet;
        }

        public WeightSortData MakeNewWeightSort()
        {
            return MakeNewWeightSort(false);
        }

        public WeightSortData MakeNewWeightSort(bool bSetValue)
        {
            WeightSortData wsd = new WeightSortData();

            wsd.sortName = SortName;

            Dictionary<string, int> SortNodes = new Dictionary<string, int>();

            int nflowid = -1;

            foreach (WeightFormula wf in FormulaList)
            {
                if (wf.nAttach == 0)
                {
                    continue;
                }

                string[] nodelist = wf.NodePath.Split('\\');
                int nparentid = -1;
                string path = "";

                int i = 0;
                for (i = 0; i < nodelist.Length - 1; ++i)
                {
                    if (path != "")
                    {
                        path += "\\";
                    }
                    path += nodelist[i];

                    if (SortNodes.ContainsKey(path))
                    {
                        nparentid = SortNodes[path];
                    }
                    else
                    {
                        WeightData wd = new WeightData();
                        wd.nID = ++nflowid;
                        wd.weightName = nodelist[i];
                        wd.nParentID = nparentid;
                        nparentid = wd.nID;
                        wd.weightValue = 0;

                        wsd.lstWeightData.Add(wd);
                        SortNodes.Add(path, wd.nID);
                    }
                }

                if (i == 0)
                {
                    path = nodelist[0];
                }
                else
                {
                    path += "\\" + nodelist[i];
                }
                WeightData wd1 = new WeightData();
                wd1.nID = ++nflowid;
                wd1.weightName = nodelist[i];
                wd1.nParentID = nparentid;
                nparentid = wd1.nID;
                wsd.lstWeightData.Add(wd1);
                SortNodes.Add(path, wd1.nID);

                if (bSetValue)
                {
                    wd1.weightValue = wf.Value;
                    if (i != 0)
                    {
                        int npid = wd1.nParentID;
                        while (npid != -1)
                        {
                            WeightData wd = wsd.lstWeightData[npid];
                            wd.weightValue += wf.Value;
                            npid = wd.nParentID;
                        }
                    }
                }
            }

            return wsd;
        }

        public WeightSortData ExportDataToWeightSort()
        {
            //List<WeightSortData> lstWsd = WeightSortManageForm.GetListWeightSortData();
            //
            WeightSortData
                //    wsd = lstWsd.Find(lst => MatchWeightSort(lst,true));
                //if (wsd == null)
                //{
                wsd = MakeNewWeightSort(true);
            //}
            //else
            //{ 
            // //传入数据
            //}

            return wsd;
        }

        public WeightParameter FindParameter(string name, string unit)
        {
            List<WeightParameter> WeightParaList = GetParaList();
            List<WeightParameter> lstPara = new List<WeightParameter>();

            foreach (WeightParameter temp in WeightParaList)
            {
                if (temp.ParaName == name)
                {
                    if (unit == null || (temp.ParaUnit == unit))
                    {
                        return temp;
                    }
                }
            }

            return null;
        }

        public static List<WeightParameter> FindGlobleParameters(string name, string unit)
        {
            List<List<WeightParameter>> WeightParaList = WeightParameter.GetWeightParameterList();
            List<WeightParameter> lstPara = new List<WeightParameter>();

            for (int i = 0; i < WeightParaList.Count; ++i)
            {
                foreach (WeightParameter temp in WeightParaList[i])
                {
                    if (temp.ParaName == name)
                    {
                        if (unit == null || (temp.ParaUnit == unit))
                        {
                            lstPara.Add(temp);
                        }
                    }
                }
            }

            if (lstPara.Count == 0)
            {
                lstPara.Add(null);
            }

            return lstPara;
        }


        public bool SetDataFromWeightSort(WeightSortData wsd)
        {
            if (!MatchWeightSort(wsd, false))
            {
                return false;
            }

            Dictionary<string, double> NodeValues = new Dictionary<string, double>();
            wsd.GetFinalNodeValueMap(-1, ref NodeValues, "");

            foreach (WeightFormula wf in FormulaList)
            {
                if (wf.nAttach == 1)
                {
                    wf.Value = NodeValues[wf.NodePath];
                }
            }

            return true;
        }

   
    }

}
