using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 重量数据对象
    /// </summary>
    public class WeightData
    {
        public WeightData Clone()
        {
            WeightData data = new WeightData();

            data.nID = this.nID;
            data.weightName = this.weightName;
            data.weightValue = this.weightValue;
            data.strRemark = this.strRemark;
            data.nParentID = this.nParentID;

            return data;
        }

        /// <summary>
        /// 节点ID
        /// </summary>
        public int nID
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
        /// 重量单位
        /// </summary>
        public string weightUnit
        {
            get
            {
                return "千克";
            }
            //set;
        }

        /// <summary>
        /// 重量数值
        /// </summary>
        public double weightValue
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string strRemark
        {
            get;
            set;
        }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public int nParentID
        {
            get;
            set;
        }
    }
}
