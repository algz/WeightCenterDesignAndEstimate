using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 重心包线算法数据对象
    /// </summary>
    public class CoreEnvelopeArithmeticData
    {
        private string arithmetic_Name;

        /// <summary>
        /// 重心包线算法名称
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
        /// 重心包线算法创建时间
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
        /// 重心包线算法最后修改时间
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

        private List<CorePointData> lst_CorePoint;

        /// <summary>
        /// 重心坐标点列表
        /// </summary>
        public List< CorePointData> lstCorePoint
        {
            get
            {
                return lst_CorePoint;
            }
            set
            {
                lst_CorePoint = value;
            }
        }

        private List< CoreCalFormulaData> lst_CalFormulaData;

        /// <summary>
        /// 重心计算公式列表
        /// </summary>
        public List<CoreCalFormulaData> lstCalFormulaData
        {
            get
            {
                return lst_CalFormulaData;
            }
            set
            {
                lst_CalFormulaData = value;
            }
        }

        private string str_ArithmeticRemark;

        /// <summary>
        /// 算法备注
        /// </summary>
        public string strArithmeticRemark
        {
            get
            {
                return str_ArithmeticRemark;
            }
            set
            {
                str_ArithmeticRemark = value;
            }
        }
    }
}
