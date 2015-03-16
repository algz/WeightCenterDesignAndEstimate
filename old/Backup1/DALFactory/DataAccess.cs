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
        /// ���������ӻ����ȡ
        /// </summary>
        public static object CreateObject(string path, string cachKey)
        {
            object objType = DataCache.GetCache(cachKey);//�ӻ����ȡ
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(path).CreateInstance(cachKey);//���䴴��
                    DataCache.SetCache(cachKey, objType);// д�뻺��
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            return objType;
        }

        /// <summary>
        /// �������İ���������ݲ�ӿ�
        /// </summary>
        public static IDAL.ICoreEnvelopeDesign CreateCoreEnvelopeDesign()
        {

            string cachKey = path + ".DALCoreEnvelopeDesign";
            object objType = CreateObject(path, cachKey);
            return (IDAL.ICoreEnvelopeDesign)objType;
        }

        /// <summary>
        /// �ͺ��������ݲ�ӿ�
        /// </summary>
        public static IDAL.ITypeWeightData CreateTypeWeightData()
        {

            string cachKey = path + ".DALTypeWeightData";
            object objType = CreateObject(path, cachKey);
            return (IDAL.ITypeWeightData)objType;
        }

        /// <summary>
        /// ������Ʊ����ݲ�ӿ�
        /// </summary>
        public static IDAL.IWeightDesignData CreateWeightDesignData()
        {

            string cachKey = path + ".DALWeightDesignData";
            object objType = CreateObject(path, cachKey);
            return (IDAL.IWeightDesignData)objType;
        }

        /// <summary>
        /// ���ݿ����ݲ�ӿ�
        /// </summary>
        public static IDAL.IDBOper CreateDBOperData()
        {

            string cachKey = path + ".DALDBOper";
            object objType = CreateObject(path, cachKey);
            return (IDAL.IDBOper)objType;
        }
      
    }
}
