using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Model.assessData
{
    //[Serializable]
    public class CorePointExt : CorePointData, ICloneable
    {
        public CorePointExt()
        {
        }

        private string _id = Guid.NewGuid().ToString();
        public string id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// 是否评估.true评估,false不评估
        /// </summary>
        public bool isAssess
        {
            get;
            set;
        }

        /// <summary>
        /// 权重值
        /// </summary>
        public double weightedValue
        {
            get;
            set;
        }
        /// <summary>
        /// 评估结果
        /// </summary>
        public double assessValue
        {
            get;
            set;
        }


        public object Clone()
        {
            return this.MemberwiseClone();//浅复制
        } 
    }
}
