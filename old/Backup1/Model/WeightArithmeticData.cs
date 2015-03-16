using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 重量算法数据对象
    /// </summary>
    public class WeightArithmeticData
    {
        private string arithmetic_Name;

        /// <summary>
        /// 算法名称
        /// </summary>
        public string arithmeticName
        {
            get
            {
                return arithmetic_Name;
            }
            set
            {
                arithmetic_Name = value;
            }
        }

        private string arithmetic_CreateTime;

        /// <summary>
        /// 算法创建时间
        /// </summary>
        public string arithmeticCreateTime
        {
            get
            {
                return arithmetic_CreateTime;
            }
            set
            {
                arithmetic_CreateTime = value;
            }
        }

        private string arithmetic_LastModifyTime;

        /// <summary>
        /// 算法最后修改时间
        /// </summary>
        public string arithmeticLastModifyTime
        {
            get
            {
                return arithmetic_LastModifyTime;
            }
            set
            {
                arithmetic_LastModifyTime = value;
            }
        }

        private WeightSortData sort_Data;

        /// <summary>
        /// 重量分类
        /// </summary>
        public WeightSortData sortData
        {
            get
            {
                return sort_Data;
            }
            set
            {
                sort_Data = value;
            }
        }

        private WeightComputationalFormulaData computationalFormula_Data;

        /// <summary>
        /// 重量计算公式
        /// </summary>
        public WeightComputationalFormulaData computationalFormulaData
        {
            get
            {
                return computationalFormula_Data;
            }
            set
            {
                computationalFormula_Data = value;
            }
        }

        private string arithmetic_Remark;

        /// <summary>
        /// 算法备注
        /// </summary>
        public string arithmeticRemark
        {
            get
            {
                return arithmetic_Remark;
            }
            set
            {
                arithmetic_Remark = value;
            }
        }
    }
}
