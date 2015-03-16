using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 重心计算公式数据对象
    /// </summary>
    public class CoreCalFormulaData
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

        private List<ParaData> lst_CoreParaData;

        /// <summary>
        /// 参数列表
        /// </summary>
        public List<ParaData> lstCoreParaData
        {
            get
            {
                return lst_CoreParaData;
            }
            set
            {
                lst_CoreParaData = value;
            }
        }

        private string cal_Formula;

        /// <summary>
        /// 公式
        /// </summary>
        public string calFormula
        {
            get
            {
                return cal_Formula;
            }
            set
            {
                cal_Formula = value;
            }
        }
    }
}
