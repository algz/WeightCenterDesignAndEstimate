using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class EvaluationData
    {
        public EvaluationData()
        { 
        }

        public EvaluationData(EvaluationData ed)
        {
            pointXValue = ed.pointXValue;
            pointYValue = ed.pointYValue;
            bEvalValue = ed.bEvalValue;
        }

        public EvaluationData(CorePointData cpd)
        {
            pointXValue = cpd.pointXValue;
            pointYValue = cpd.pointYValue;
        }

        /// <summary>
        /// X数值
        /// </summary>
        public double pointXValue
        {
            get;
            set;
        }

        /// <summary>
        /// Y数值
        /// </summary>
        public double pointYValue
        {
            get;
            set;
        }
        /// <summary>
        /// 评估数据，1 符合要求，0 不符合要求
        /// </summary>
        public int bEvalValue
        {
            get;
            set;
        }
    }
}
