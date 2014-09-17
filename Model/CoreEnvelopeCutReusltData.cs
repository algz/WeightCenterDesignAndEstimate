using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 重心包线裁剪结果数据对象
    /// </summary>
    public class CoreEnvelopeCutResultData
    {
        private string cutResult_Name;

        /// <summary>
        /// 重心包线剪裁数据名称
        /// </summary>
        public string cutResultName
        {
            get
            {
                return cutResult_Name;
            }
            set
            {
                cutResult_Name = value;
            }
        }

        private List<CorePointData> lst_BasicCoreEnvelope;

        /// <summary>
        /// 基础重心包线数据
        /// </summary>
        public List<CorePointData> lstBasicCoreEnvelope
        {
            get
            {
                return lst_BasicCoreEnvelope;
            }
            set
            {
                lst_BasicCoreEnvelope = value;
            }
        }

        private int straggling_ZXCount;

        /// <summary>
        /// 周向离散个数
        /// </summary>
        public int stragglingZXCount
        {
            get
            {
                return straggling_ZXCount;
            }
            set
            {
                straggling_ZXCount = value;
            }
        }

        private int straggling_RadialCount;

        /// <summary>
        /// 径向离散个数
        /// </summary>
        public int stragglingRadialCount
        {
            get
            {
                return straggling_RadialCount;
            }
            set
            {
                straggling_RadialCount = value;
            }
        }

        private double strAggling_RadialGrads;

        /// <summary>
        /// 径向离散梯度
        /// </summary>
        public double strAgglingRadialGrads
        {
            get
            {
                return strAggling_RadialGrads;
            }
            set
            {
                strAggling_RadialGrads = value;
            }
        }

        private List<CorePointData> lst_StragglingCore;

        /// <summary>
        /// 离散重心数据
        /// </summary>
        public List<CorePointData> lstStragglingCore
        {
            get
            {
                return lst_StragglingCore;
            }
            set
            {
                lst_StragglingCore = value;
            }
        }

        private List<CorePointData> lst_FuelCore;

        /// <summary>
        /// 燃油重心数据
        /// </summary>
        public List<CorePointData> lstFuelCore
        {
            get
            {
                return lst_FuelCore;
            }
            set
            {
                lst_FuelCore = value;
            }
        }

        private int[,] array_Feedback;

        /// <summary>
        /// 反馈数据
        /// </summary>
        public int[,] arrayFeedback
        {
            get
            {
                return array_Feedback;
            }
            set
            {
                array_Feedback = value;
            }
        }

        private List<CorePointData> lst_CutEnvelopeCore;

        /// <summary>
        /// 裁剪重心包线数据
        /// </summary>
        public List<CorePointData> lstCutEnvelopeCore
        {
            get
            {
                return lst_CutEnvelopeCore;
            }
            set
            {
                lst_CutEnvelopeCore = value;
            }
        }

    }
}
