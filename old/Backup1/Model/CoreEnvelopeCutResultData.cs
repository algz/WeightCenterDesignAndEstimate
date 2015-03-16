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
        public CoreEnvelopeCutResultData(int ncuttype)
        {
            cutResultName = "";
            lstBasicCoreEnvelope = new List<CorePointData>();
            lstDiscreteCore = new List<CorePointData>();

            lstCoreEvaluation = new List<int>();

            nCutType = ncuttype;
            
            lstFuelCore = new List<CorePointData>();
            
            lstCutEnvelopeCore = new List<CorePointData>();
        }

        /// <summary>
        /// 重心包线剪裁数据名称
        /// </summary>
        public string cutResultName
        {
            get;
            set;
        }

        /// <summary>
        /// 基础重心包线数据
        /// </summary>
        public List<CorePointData> lstBasicCoreEnvelope
        {
            get;
            set;
         }

        /// <summary>
        /// 剪裁类型
        /// 0:燃油特性剪裁
        /// 1:飞行品质剪裁
        /// 2:飞行载荷剪裁
        /// </summary>
        public int nCutType
        {
            get;
            set;
        }

        /// <summary>
        /// 周向离散个数
        /// </summary>
        public int nDiscreteCircularPtCount
        {
            get;
            set;
        }

        /// <summary>
        /// 径向离散个数
        /// </summary>
        public int nDiscreteRadialPtCount
        {
            get;
            set;
        }

        /// <summary>
        /// 径向离散首段长度(相对长度，从外向内离散)
        /// </summary>
        public double fDiscreteRadialFirstLen
        { 
            get; set;
        }
        
        /// <summary>
        /// 径向离散梯度
        /// </summary>
        public double fDiscreteRadialRatio
        {
            get;
            set;
        }

        /// <summary>
        /// 宽高离散比例系数
        /// </summary>
        public double fRatioWidthVsHeight
        {
            get;
            set;
        }

        /// <summary>
        /// 离散重心数据
        /// </summary>
        public List<CorePointData> lstDiscreteCore
        {
            get;
            set;
        }

        /// <summary>
        /// 燃油重心数据
        /// nCutType = 0 时有效
        /// </summary>
        public List<CorePointData> lstFuelCore
        {
            get;
            set;
        }

        /// <summary>
        /// 重心离散点评估数据 0，不符合，1 符合
        /// </summary>
        public List<int> lstCoreEvaluation
        {
            get;
            set;
        }

        /// <summary>
        /// 裁剪重心包线数据
        /// </summary>
        public List<CorePointData> lstCutEnvelopeCore
        {
            get;
            set;
        }

    }
}
