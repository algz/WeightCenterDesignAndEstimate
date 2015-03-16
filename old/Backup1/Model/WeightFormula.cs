using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class WeightFormula
    {
        public WeightFormula()
        {
            ParaList = new List<WeightParameter>();
            NodePath = "";
            Formula = "";
            Value = 0.0;
            nAttach = 1;
        }

        public WeightFormula Clone()
        {
            WeightFormula formula = new WeightFormula();
            formula.NodePath = this.NodePath;
            formula.Formula = this.Formula;
            formula.Value = this.Value;

            List<WeightParameter> paraList = new List<WeightParameter>();
            foreach (WeightParameter para in ParaList)
            {
                paraList.Add(para.Clone());
            }
            formula.ParaList = paraList;

            return formula;
        }
        public string NodePath { get; set; }
        public List<WeightParameter> ParaList { get; set; }
        public string Formula { get; set; }
        public double Value { get; set; }
        //附加信息
        public int nAttach { get; set; }
    }
}
