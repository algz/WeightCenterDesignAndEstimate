using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.assessData
{
    public class WeightAssessResult
    {
        private string _resultID=Guid.NewGuid().ToString();
        public string resultID
        {
            get
            {
                return _resultID;
            }
        }

        /// <summary>
        /// 重量评估数据结果名称
        /// </summary>
        public string resultName
        {
            get;
            set;
        }

        /// <summary>
        /// 基准重量数据(设计/调整)
        /// </summary>
        public List<WeightData> datumWeightDataList
        {
            get;
            set;
        }

        /// <summary>
        /// 待评估重量数据
        /// </summary>
        public List<WeightData> assessWeightDataList
        {
            get;
            set;
        }

        /// <summary>
        /// 重量评估参数集合
        /// </summary>
        public List<WeightAssessParameter> weightAssessParamList
        {
            get;
            set;
        }

        /// <summary>
        /// 基准重量总和
        /// </summary>
        public double datumWeightTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 评估重量总和
        /// </summary>
        public double assessWeightTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 合理指标总和
        /// </summary>
        public double rationalityInflationTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 先进指标总和
        /// </summary>
        public double advancedInflationTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 是否评估过重量
        /// </summary>
        public bool isEvaluateWeight
        {
            get;
            set;
        }
    }
}
