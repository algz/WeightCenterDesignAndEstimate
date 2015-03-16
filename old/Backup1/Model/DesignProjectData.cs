using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.assessData;

namespace Model
{
    /// <summary>
    /// 重量设计工程数据对象
    /// </summary>
    public class DesignProjectData
    {
        private string project_Name;

        /// <summary>
        /// 工程名称
        /// </summary>
        public string projectName
        {
            get
            {
                return project_Name;
            }
            set
            {
                project_Name = value;
            }
        }

        private string project_Creator;

        /// <summary>
        /// 创建人
        /// </summary>
        public string projectCreator
        {
            get
            {
                return project_Creator;
            }
            set
            {
                project_Creator = value;
            }
        }

        private string str_Remark;

        /// <summary>
        /// 备注
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

        private List<WeightArithmetic> lst_WeightArithmetic;

        /// <summary>
        /// 重量设计结果列表
        /// </summary>
        public List<WeightArithmetic> lstWeightArithmetic
        {
            get
            {
                return lst_WeightArithmetic;
            }
            set
            {
                lst_WeightArithmetic = value;
            }
        }

        private List<WeightAdjustmentResultData> lst_AdjustmentResultData;

        /// <summary>
        /// 重量调整结果列表
        /// </summary>
        public List<WeightAdjustmentResultData> lstAdjustmentResultData
        {
            get
            {
                return lst_AdjustmentResultData;
            }
            set
            {
                lst_AdjustmentResultData = value;
            }
        }

        /// <summary>
        /// 重量评估结果列表
        /// </summary> 
        public List<WeightAssessResult> weightAssessResultList
        {
            get;
            set;
        }

        private List<CoreEnvelopeDesignData> lst_EnvelopeDesignData;

        /// <summary>
        /// 重心包线设计结果列表
        /// </summary>
        public List<CoreEnvelopeDesignData> lstEnvelopeDesignData
        {
            get
            {
                return lst_EnvelopeDesignData;
            }
            set
            {
                lst_EnvelopeDesignData = value;
            }
        }

        private List<CoreEnvelopeArithmetic> lst_CoreEnvelopeDesign;

        /// <summary>
        /// 重心包线设计结果列表
        /// </summary>
        public List<CoreEnvelopeArithmetic> lstCoreEnvelopeDesign
        {
            get
            {
                return lst_CoreEnvelopeDesign;
            }
            set
            {
                lst_CoreEnvelopeDesign = value;
            }
        }

        private List<CoreEnvelopeCutResultData> lst_CutResultData;

        /// <summary>
        /// 重心包线剪裁结果列表
        /// </summary>
        public List<CoreEnvelopeCutResultData> lstCutResultData
        {
            get
            {
                return lst_CutResultData;
            }
            set
            {
                lst_CutResultData = value ;
            }
        }

        /// <summary>
        /// 重心包线评估结果集
        /// </summary>
        public List<CoreAssessResult> CoreAssessResultList
        {
            get;
            set;
        }

    }
}
