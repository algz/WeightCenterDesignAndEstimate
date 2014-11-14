using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 重量评估对象
    /// </summary>
    public class WeightAssessParameter
    {
        /// <summary>
        /// 重量评估数据名称
        /// </summary>
        public string weightAssessName
        {
            get;
            set;
        }

        /// <summary>
        /// 重量名称
        /// </summary>
        public string weightName
        {
            get;
            set;
        }

        /// <summary>
        /// 基准重量
        /// </summary>
        public double datumWeight
        {
            get;
            set;
        }

        /// <summary>
        /// 评估重量
        /// </summary>
        public double assessWeight
        {
            get;
            set;
        }

        /// <summary>
        /// 重量单位
        /// </summary>
        public string weightUnit
        {
            get;
            set;
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public double minValue
        {
            get;
            set;
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public double maxValue
        {
            get;
            set;
        }

        /// <summary>
        /// 权重值
        /// </summary>
        public double weightedValue
        {
            get;
            set;
        }

        /// <summary>
        /// 合理指标
        /// </summary>
        public double rationalityInflation
        {
            get;
            set;
        }

        /// <summary>
        /// 先进指标
        /// </summary>
        public double advancedInflation
        {
            get;
            set;
        }
    }
}
