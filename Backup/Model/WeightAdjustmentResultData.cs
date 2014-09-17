using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 重量调整结果数据对象
    /// </summary>
    public class WeightAdjustmentResultData
    {
        private string sort_Name;

        /// <summary>
        /// 重量分类名称
        /// </summary>
        public string SortName
        {
            get
            {
                return sort_Name;
            }
            set
            {
                sort_Name = value;
            }
        }

        private string weightAdjust_Name;

        /// <summary>
        /// 重量调整数据名称
        /// </summary>
        public string WeightAdjustName
        {
            get
            {
                return weightAdjust_Name;
            }
            set
            {
                weightAdjust_Name = value;
            }
        }

        private WeightSortData basic_WeightData;

        /// <summary>
        /// 基础重量数据
        /// </summary>
        public WeightSortData basicWeightData
        {
            get
            {
                return basic_WeightData;
            }
            set
            {
                basic_WeightData = value;
            }
        }

        private List<ParaData> check_Factor;

        /// <summary>
        /// 校核因子
        /// </summary>
        public List<ParaData> checkFactor
        {
            get
            {
                return check_Factor;
            }
            set
            {
                check_Factor = value;
            }
        }


        private List<ParaData> technology_Factor;

        /// <summary>
        /// 技术因子
        /// </summary>
        public List<ParaData> technologyFactor
        {
            get
            {
                return technology_Factor;
            }
            set
            {
                technology_Factor = value;
            }
        }

        private WeightSortData weightAdjust_Data;

        /// <summary>
        /// 调整重量数据
        /// </summary>
        public WeightSortData weightAdjustData
        {
            get
            {
                return weightAdjust_Data;
            }
            set
            {
                weightAdjust_Data = value;
            }
        }

    }
}
