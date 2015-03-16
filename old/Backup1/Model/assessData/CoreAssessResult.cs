using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using System.ComponentModel;
using System.Collections;

namespace Model.assessData
{
    public class CoreAssessResult
    {
        private string _resultID = Guid.NewGuid().ToString();
        public string resultID
        {
            get
            {
                return _resultID;
            }
        }

        /// <summary>
        /// 重心评估数据结果名称
        /// </summary>
        public string resultName
        {
            get;
            set;
        }

        /// <summary>
        /// 基准重心包线数据
        /// </summary>
        public List<CorePointExt> datumCoreDataList
        {
            get;
            set;
        }

        /// <summary>
        /// 评估重心包线数据
        /// </summary>
        public List<CorePointExt> assessCoreDataList
        {
            get;
            set;
        }

        /// <summary>
        /// 前裕度最小值
        /// </summary>
        public double topMarginMin
        {
            get;
            set;
        }

        /// <summary>
        /// 前裕度最大值
        /// </summary>
        public double topMarginMax
        {
            get;
            set;
        }

        /// <summary>
        /// 后裕度最小值
        /// </summary>
        public double downMarginMin
        {
            get;
            set;
        }

        /// <summary>
        /// 后裕度最大值
        /// </summary>
        public double downMarginMax
        {
            get;
            set;
        }
        /// <summary>
        /// 评估结果
        /// </summary>
        public double evaluationResult
        {
            get;
            set;
        }
    }
}
