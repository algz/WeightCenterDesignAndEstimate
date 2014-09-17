using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 参数数据对象
    /// </summary>
    public class ParaData
    {
        private string para_Name;

        /// <summary>
        /// 参数名称
        /// </summary>
        public string paraName
        {
            get
            {
                return para_Name;
            }
            set
            {
                para_Name = value;
            }
        }

        private string para_Unit;

        /// <summary>
        /// 参数单位
        /// </summary>
        public string paraUnit
        {
            get
            {
                return para_Unit;
            }
            set
            {
                para_Unit = value;
            }
        }

        private int para_Type;

        /// <summary>
        /// 参数类型
        /// </summary>
        public int paraType
        {
            get
            {
                return para_Type;
            }
            set
            {
                para_Type = value;
            }
        }

        private double para_Value;

        /// <summary>
        /// 参数数值
        /// </summary>
        public double paraValue
        {
            get
            {
                return para_Value;
            }
            set
            {
                para_Value = value;
            }
        }

        private string str_Remark;

        /// <summary>
        /// 参数备注
        /// </summary>
        public string strRemark
        {
            get
            {
                return str_Remark;
            }
            set
            {
                str_Remark = value;
            }
        }

        public ParaData Clone()
        {
            ParaData data = new ParaData();
            data.paraName = this.paraName;
            data.paraType = this.paraType;
            data.paraUnit = this.paraUnit;
            data.strRemark = this.strRemark;
            return data;
        }
    }
}
