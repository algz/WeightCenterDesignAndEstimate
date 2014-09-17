using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 重心包线设计结果对象
    /// </summary>
    public class CoreEnvelopeDesignData
    {
        private string envelopeDesign_Name;

        /// <summary>
        /// 重心包线设计数据名称
        /// </summary>
        public string envelopeDesignName
        {
            get
            {
                return envelopeDesign_Name;
            }
            set
            {
                envelopeDesign_Name = value;
            }
        }

        private List<ParaData> lst_ParaData;

        /// <summary>
        /// 参数列表
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

        private CoreEnvelopeArithmeticData arithmetic_Data;

        /// <summary>
        /// 重心算法
        /// </summary>
        public CoreEnvelopeArithmeticData arithmeticData
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
