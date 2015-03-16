using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace Model
{
    /// <summary>
    /// 重心包线算法
    /// </summary>
    public class CoreEnvelopeArithmetic
    {
        public CoreEnvelopeArithmetic()
        {
            DataName = "";
            Name = "";
            CreateTime = "";
            LastModifyTime = "";
            Remark = "";
            FormulaList = new List<NodeFormula>();
        }
        public string DataName { get; set; }
        public string Name { get; set; }
        public string CreateTime { get; set; }
        public string LastModifyTime { get; set; }
        public string Remark { get; set; }
        public List<NodeFormula> FormulaList { get; set; }

        public CoreEnvelopeArithmetic Clone()
        {
            CoreEnvelopeArithmetic weight = new CoreEnvelopeArithmetic();

            weight.DataName = this.DataName;
            weight.Name = this.Name;
            weight.CreateTime = this.CreateTime;
            weight.LastModifyTime = this.LastModifyTime;
            weight.Remark = this.Remark;

            List<NodeFormula> lstWeightFormula = new List<NodeFormula>();
            foreach (NodeFormula formula in FormulaList)
            {
                lstWeightFormula.Add(new NodeFormula(formula.NodeName, formula.XFormula.Clone(), formula.YFormula.Clone()));
            }
            weight.FormulaList = lstWeightFormula;

            return weight;
        }


        static public CoreEnvelopeArithmetic ReadArithmeticData(string filename)
        {
            List<List<WeightParameter>> WeightParaList = WeightParameter.GetWeightParameterList();

            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(filename);
            }
            catch
            {
                MessageBox.Show("打开文件错误!");
                XCommon.XLog.Write("打开文件错误");
                return null;
            }


            XmlNode xmlnode = doc.SelectSingleNode("重量算法/算法名称");

            if (xmlnode == null)
            {
                MessageBox.Show("错误的算法文件!");
                XCommon.XLog.Write("错误的算法文件!");
                return null;
            }

            CoreEnvelopeArithmetic wa = new CoreEnvelopeArithmetic();

            wa.Name = xmlnode.InnerText;

            xmlnode = doc.SelectSingleNode("重量算法/算法创建时间");
            wa.CreateTime = xmlnode.InnerText;
            //xmlnode = doc.SelectSingleNode("重量算法/重量分类");
            //wa.SortName = xmlnode.InnerText;
            xmlnode = doc.SelectSingleNode("重量算法/算法最后修改时间");
            wa.LastModifyTime = xmlnode.InnerText;
            xmlnode = doc.SelectSingleNode("重量算法/算法备注");
            wa.Remark = xmlnode.InnerText;
            xmlnode = doc.SelectSingleNode("重量算法/节点列表");

            Dictionary<WeightParameter, WeightParameter> wpDict = new Dictionary<WeightParameter, WeightParameter>();

            foreach (XmlNode xmlsubnode in xmlnode.ChildNodes)
            {
                NodeFormula nf = new NodeFormula();
                nf.NodeName = xmlsubnode.ChildNodes[0].InnerText;

                for (int i = 0; i < 2; ++i)
                {
                    WeightFormula wf = nf[i];
                    XmlNode wfNode = xmlsubnode.SelectSingleNode(wf.NodePath); ;
                    if (wfNode == null)
                    {
                        MessageBox.Show("错误的算法文件!");
                        return null;
                    }

                    wf.Formula = wfNode.ChildNodes[0].InnerText;

                    foreach (XmlNode paranode in wfNode.ChildNodes[1].ChildNodes)
                    {
                        string name = paranode.ChildNodes[0].InnerText;
                        string unit = paranode.ChildNodes[2].InnerText;

                        WeightParameter wpGlobal = WeightArithmetic.FindGlobleParameters(name, unit)[0];

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

                            WeightParaList[10].Add(temp10wp);

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

                    wf.Value = double.Parse(wfNode.ChildNodes[2].InnerText);
                }

                wa.FormulaList.Add(nf);
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

                //writeXml.WriteStartElement("重量分类");
                //writeXml.WriteString(SortName);
                //writeXml.WriteEndElement();

                writeXml.WriteStartElement("算法创建时间");
                writeXml.WriteString(CreateTime);
                writeXml.WriteEndElement();

                writeXml.WriteStartElement("算法最后修改时间");
                writeXml.WriteString(LastModifyTime);
                writeXml.WriteEndElement();

                writeXml.WriteStartElement("算法备注");
                writeXml.WriteString(Remark);
                writeXml.WriteEndElement();

                writeXml.WriteStartElement("节点列表");
                {
                    foreach (NodeFormula nf in FormulaList)
                    {
                        writeXml.WriteStartElement("节点");
                        {
                            writeXml.WriteStartElement("节点名称");
                            writeXml.WriteString(nf.NodeName);
                            writeXml.WriteEndElement();

                            for (int i = 0; i < 2; ++i)
                            {
                                WeightFormula wf = nf[i];
                                writeXml.WriteStartElement(wf.NodePath);
                                {
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
            foreach (NodeFormula nf in FormulaList)
            {
                for (int i = 0; i < 2; ++i)
                {
                    WeightFormula wf = nf[i];
                    paraList.AddRange(
                        from item in wf.ParaList
                        where !paraList.Contains(item)
                        select item
                        );
                }
            }

            return paraList.OrderBy(l => l.ParaType).ToList();
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

    }
}
