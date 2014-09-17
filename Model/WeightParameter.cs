using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Model
{
    public class WeightParameter
    {
        static public string[] ParaTypeList = new string[] { "指标参数", "构型和总体参数", "旋翼参数", "机身翼面参数", "着陆装置参数", "动力系统参数", "传动系统参数", "操纵系统参数", "人工参数", "其他类型参数", "临时参数" };
        static private List<List<WeightParameter>> WeightParaList = null;

        public WeightParameter Clone()
        {
            WeightParameter para = new WeightParameter();
            para.ParaName = this.ParaName;
            para.ParaUnit = this.ParaUnit;
            para.ParaType = this.ParaType;
            para.ParaValue = this.ParaValue;
            para.ParaRemark = this.ParaRemark;

            return para;
        }

        public WeightParameter()
        {
            ParaName = "";
            ParaUnit = "";
            ParaType = -1;
            ParaRemark = "";
            ParaValue = 0;
        }

        public WeightParameter(WeightParameter wp)
        {
            ParaName = wp.ParaName;
            ParaUnit = wp.ParaUnit;
            ParaType = wp.ParaType;
            ParaRemark = wp.ParaRemark;
            ParaValue = wp.ParaValue;
        }

        static public void ResetWeightParameterList()
        {
            WeightParaList = null;
        }

        static public List<List<WeightParameter>> GetWeightParameterList()
        {
            if (WeightParaList != null)
            {
                return WeightParaList;
            }

            WeightParaList = new List<List<WeightParameter>>();
            for (int i = 0; i < ParaTypeList.Length; ++i)
            {
                WeightParaList.Add(new List<WeightParameter>());
            }

            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            string strname = "ParameterCollection\\ParameterCollection.PMC";
            if (!System.IO.File.Exists(strname))
            {
                System.Windows.Forms.MessageBox.Show("文件 " + strname + " 不存在！");
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(strname);
                XmlNode xmlnode = doc.SelectSingleNode("PMC/参数列表");

                for (int i = 0; i < xmlnode.ChildNodes.Count; ++i)
                {
                    WeightParameter para = new WeightParameter();
                    XmlNode curxmlnode = xmlnode.ChildNodes[i];
                    para.ParaName = curxmlnode.ChildNodes[0].InnerText;
                    para.ParaUnit = curxmlnode.ChildNodes[1].InnerText;
                    para.ParaValue = double.Parse(curxmlnode.ChildNodes[3].InnerText);
                    para.ParaRemark = curxmlnode.ChildNodes[4].InnerText;

                    para.ParaType = int.Parse(curxmlnode.ChildNodes[2].InnerText);

                    WeightParaList[para.ParaType].Add(para);
                }
            }
            return WeightParaList;
        }

        public string ParaName { get; set; }
        public string ParaUnit { get; set; }
        public int ParaType { get; set; }
        public string ParaRemark { get; set; }
        public double ParaValue { get; set; }
    };
}
