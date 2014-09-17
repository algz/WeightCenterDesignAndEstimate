using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 重量计算公式
    /// </summary>
    public class WeightComputationalFormulaData
    {
        private string formula_Name;

        /// <summary>
        /// 公式名称
        /// </summary>
        public string formulaName
        {
            get
            {
                return formula_Name;
            }
            set
            {
                formula_Name = value;
            }
        }

        private ParaData para_Data;

        /// <summary>
        /// 参数列表
        /// </summary>
        public ParaData paraData
        {
            get
            {
                return para_Data;
            }
            set
            {
                para_Data = value;
            }
        }

        private string str_Formula;

        /// <summary>
        /// 公式
        /// </summary>
        public string strFormula
        {
            get
            {
                return str_Formula;
            }
            set
            {
                str_Formula = value;
            }
        }
    }
}
