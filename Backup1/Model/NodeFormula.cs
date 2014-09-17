using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class NodeFormula
    {
        public NodeFormula()
        {
            XFormula = new WeightFormula();
            XFormula.NodePath = "X轴坐标";
            YFormula = new WeightFormula();
            YFormula.NodePath = "Y轴坐标";
        }
        public NodeFormula(string name)
        {
            NodeName = name;
            XFormula = new WeightFormula();
            XFormula.NodePath = "X轴坐标";
            YFormula = new WeightFormula();
            YFormula.NodePath = "Y轴坐标";
        }
        public NodeFormula(string name, WeightFormula xf, WeightFormula yf)
        {
            NodeName = name;
            XFormula = xf;
            YFormula = yf;
        }

        public string NodeName { get; set; }
        public WeightFormula XFormula { get; set; }
        public WeightFormula YFormula { get; set; }
        public WeightFormula this[int index]
        {
            get
            {
                return (index == 0) ? XFormula : YFormula;
            }
            set
            {
                if (index == 0)
                {
                    XFormula = value;
                }
                else
                {
                    YFormula = value;
                }
            }
        }
    }
}
