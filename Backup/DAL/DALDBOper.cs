using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DALDBOper : IDAL.IDBOper
    {
        private static readonly string strDataBasePath = (System.IO.Directory.GetCurrentDirectory() + @"\DataBase\CoreDesignSoft.db");

        /// <summary>
        /// 创建重量设计数据表
        /// </summary>
        public void CreateTableWeightDesignData()
        {
            SQLiteDBHelper.CreateDB(strDataBasePath);
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.Append("Create table T_WeightDesignData(");
            strBuilder.Append("Id integer NOT NULL PRIMARY KEY,");
            strBuilder.Append("DesignData_Name varchar(50),");
            strBuilder.Append("DesignData_Submitter varchar(50),");
            strBuilder.Append("Helicopter_Name varchar(50),");
            strBuilder.Append("DataRemark text,");
            strBuilder.Append("LastModify_Time varchar(50),");
            strBuilder.Append("DesignTaking_Weight float,");
            strBuilder.Append("MainSystem_Name text");
            strBuilder.Append(")");

            SQLiteDBHelper.ExecuteNonQuery(strBuilder.ToString(), null);
        }

        /// <summary>
        /// 创建型号重量数据表
        /// </summary>
        public void CreateTableTypeWeightData()
        {
            SQLiteDBHelper.CreateDB(strDataBasePath);
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.Append("Create table T_TypeWeightData(");
            strBuilder.Append("Id integer NOT NULL PRIMARY KEY,");
            strBuilder.Append("Helicopter_Name varchar(50),");
            strBuilder.Append("Last_ModifyTime varchar(50),");
            strBuilder.Append("Helicopter_Type varchar(50),");
            strBuilder.Append("DesignTaking_Weight float,");
            strBuilder.Append("MaxTaking_Weight float,");
            strBuilder.Append("EmptyWeight float,");
            strBuilder.Append("MainSystem_Name text,");
            strBuilder.Append("Helicoter_Country varchar(50)");
            strBuilder.Append(")");

            SQLiteDBHelper.ExecuteNonQuery(strBuilder.ToString(), null);
        }

        /// <summary>
        /// 创建重心包线设计数据表
        /// </summary>
        public void CreateTableCoreEnvelopeDesignData()
        {
            SQLiteDBHelper.CreateDB(strDataBasePath);
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.Append("Create table T_CoreEnvelopeDesignData(");
            strBuilder.Append("Id integer NOT NULL PRIMARY KEY,");
            strBuilder.Append("DesignData_Name varchar(50),");
            strBuilder.Append("DesignData_Submitter varchar(50),");
            strBuilder.Append("Helicopter_Name varchar(50),");
            strBuilder.Append("DataRemark text,");
            strBuilder.Append("LastModify_Time varchar(50),");
            strBuilder.Append("DesignTaking_Weight float,");
            strBuilder.Append("CoreEnvelope text");
            strBuilder.Append(")");

            SQLiteDBHelper.ExecuteNonQuery(strBuilder.ToString(), null);
        }
    }
}
