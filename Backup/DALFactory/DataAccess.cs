using System;
using System.Reflection;
using System.Configuration;

using IDAL;

namespace DALFactory
{
    public sealed class DataAccess
    {
        private static readonly string path =System.Configuration.ConfigurationManager.AppSettings["DAL"];

        /// <summary>
        /// 创建对象或从缓存获取
        /// </summary>
        public static object CreateObject(string path, string cachKey)
        {
            object objType = DataCache.GetCache(cachKey);//从缓存读取
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(path).CreateInstance(cachKey);//反射创建
                    DataCache.SetCache(cachKey, objType);// 写入缓存
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            return objType;
        }

        /// <summary>
        /// 创建重心包线设计数据层接口
        /// </summary>
        public static IDAL.ICoreEnvelopeDesign CreateCoreEnvelopeDesign()
        {

            string cachKey = path + ".DALCoreEnvelopeDesign";
            object objType = CreateObject(path, cachKey);
            return (IDAL.ICoreEnvelopeDesign)objType;
        }

        /// <summary>
        /// 型号重量数据层接口
        /// </summary>
        public static IDAL.ITypeWeightData CreateTypeWeightData()
        {

            string cachKey = path + ".DALTypeWeightData";
            object objType = CreateObject(path, cachKey);
            return (IDAL.ITypeWeightData)objType;
        }

        /// <summary>
        /// 重量设计表数据层接口
        /// </summary>
        public static IDAL.IWeightDesignData CreateWeightDesignData()
        {

            string cachKey = path + ".DALWeightDesignData";
            object objType = CreateObject(path, cachKey);
            return (IDAL.IWeightDesignData)objType;
        }

        /// <summary>
        /// 数据库数据层接口
        /// </summary>
        public static IDAL.IDBOper CreateDBOperData()
        {

            string cachKey = path + ".DALDBOper";
            object objType = CreateObject(path, cachKey);
            return (IDAL.IDBOper)objType;
        }
      
    }
}
