using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 重量设计结果对象
    /// </summary>
    public class WeightDesignReusltData
    {
        private string data_Name;

        /// <summary>
        /// 重量设计数据名称
        /// </summary>
        public string dataName
        {
            get
            {
                return data_Name;
            }
            set
            {
                data_Name = value;
            }
        }

        private List<ParaData> lst_ParaData;

        /// <summary>
        /// 参数数据对象
        /// </summary>
        public List<ParaData> lstParaData
        {
            get
            {
                return lst_ParaData;
            }
            set
            {
                lst_ParaData = value;
            }
        }

        private WeightArithmeticData arithmetic_Data;

        /// <summary>
        /// 重量算法设计对象
        /// </summary>
        public WeightArithmeticData arithmeticData
        {
            get
            {
                return arithmetic_Data;
            }
            set
            {
                arithmetic_Data = value;
            }
        }
    }
}
