﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    /// <summary>
    /// 重心包线设计数据访问类
    /// </summary>
    public class DALCoreEnvelopeDesign : IDAL.ICoreEnvelopeDesign
    {
        public DALCoreEnvelopeDesign()
        { }

        #region  SqlServer

        ///// <summary>
        ///// 获取最大ID
        ///// </summary>
        ///// <returns></returns>
        //public int GetMaxId()
        //{
        //    return DbHelperSQL.GetMaxID("Id", "T_CoreEnvelopeDesignData");
        //}

        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public bool Add(Model.CoreEnvelopeDesign model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("insert into T_CoreEnvelopeDesignData(");
        //    strSql.Append("Id,DesignData_Name,DesignData_Submitter,Helicopter_Name,DataRemark,LastModify_Time,DesignTaking_Weight,CoreEnvelope)");
        //    strSql.Append(" values (");
        //    strSql.Append("@Id,@DesignData_Name,@DesignData_Submitter,@Helicopter_Name,@DataRemark,@LastModify_Time,@DesignTaking_Weight,@CoreEnvelope)");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@Id", SqlDbType.Int,4),
        //            new SqlParameter("@DesignData_Name", SqlDbType.NVarChar,50),
        //            new SqlParameter("@DesignData_Submitter", SqlDbType.NVarChar,50),
        //            new SqlParameter("@Helicopter_Name", SqlDbType.NVarChar,50),
        //            new SqlParameter("@DataRemark", SqlDbType.NVarChar,-1),
        //            new SqlParameter("@LastModify_Time", SqlDbType.NVarChar,50),
        //            new SqlParameter("@DesignTaking_Weight", SqlDbType.Float,8),
        //            new SqlParameter("@CoreEnvelope", SqlDbType.NVarChar,-1)};
        //    parameters[0].Value = model.Id;
        //    parameters[1].Value = model.DesignData_Name;
        //    parameters[2].Value = model.DesignData_Submitter;
        //    parameters[3].Value = model.Helicopter_Name;
        //    parameters[4].Value = model.DataRemark;
        //    parameters[5].Value = model.LastModify_Time;
        //    parameters[6].Value = model.DesignTaking_Weight;
        //    parameters[7].Value = model.CoreEnvelope;

        //    int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public bool Update(Model.CoreEnvelopeDesign model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update T_CoreEnvelopeDesignData set ");
        //    strSql.Append("Id=@Id,");
        //    strSql.Append("DesignData_Name=@DesignData_Name,");
        //    strSql.Append("DesignData_Submitter=@DesignData_Submitter,");
        //    strSql.Append("Helicopter_Name=@Helicopter_Name,");
        //    strSql.Append("DataRemark=@DataRemark,");
        //    strSql.Append("LastModify_Time=@LastModify_Time,");
        //    strSql.Append("DesignTaking_Weight=@DesignTaking_Weight,");
        //    strSql.Append("CoreEnvelope=@CoreEnvelope");
        //    strSql.Append(" where Id=" + model.Id.ToString());
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@Id", SqlDbType.Int,4),
        //            new SqlParameter("@DesignData_Name", SqlDbType.NVarChar,50),
        //            new SqlParameter("@DesignData_Submitter", SqlDbType.NVarChar,50),
        //            new SqlParameter("@Helicopter_Name", SqlDbType.NVarChar,50),
        //            new SqlParameter("@DataRemark", SqlDbType.NVarChar,-1),
        //            new SqlParameter("@LastModify_Time", SqlDbType.NVarChar,50),
        //            new SqlParameter("@DesignTaking_Weight", SqlDbType.Float,8),
        //            new SqlParameter("@CoreEnvelope", SqlDbType.NVarChar,-1)};
        //    parameters[0].Value = model.Id;
        //    parameters[1].Value = model.DesignData_Name;
        //    parameters[2].Value = model.DesignData_Submitter;
        //    parameters[3].Value = model.Helicopter_Name;
        //    parameters[4].Value = model.DataRemark;
        //    parameters[5].Value = model.LastModify_Time;
        //    parameters[6].Value = model.DesignTaking_Weight;
        //    parameters[7].Value = model.CoreEnvelope;

        //    int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public bool Delete(int Id)
        //{
        //    //该表无主键信息，请自定义主键/条件字段
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("delete from T_CoreEnvelopeDesignData ");
        //    strSql.Append(" where Id=" + Id.ToString());
        //    SqlParameter[] parameters = {
        //    };

        //    int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public Model.CoreEnvelopeDesign GetModel(int Id)
        //{
        //    //该表无主键信息，请自定义主键/条件字段
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select  top 1 Id,DesignData_Name,DesignData_Submitter,Helicopter_Name,DataRemark,LastModify_Time,DesignTaking_Weight,CoreEnvelope from T_CoreEnvelopeDesignData ");
        //    strSql.Append(" where Id=" + Id.ToString());
        //    SqlParameter[] parameters = {
        //    };

        //    Model.CoreEnvelopeDesign model = new Model.CoreEnvelopeDesign();
        //    DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        return DataRowToModel(ds.Tables[0].Rows[0]);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// 获取所有对象实体
        ///// </summary>
        //public List<Model.CoreEnvelopeDesign> GetListModel()
        //{
        //    //该表无主键信息，请自定义主键/条件字段
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select Id,DesignData_Name,DesignData_Submitter,Helicopter_Name,DataRemark,LastModify_Time,DesignTaking_Weight,CoreEnvelope from T_CoreEnvelopeDesignData ");
        //    SqlParameter[] parameters = {
        //    };

        //    List<Model.CoreEnvelopeDesign> lstModel = new List<Model.CoreEnvelopeDesign>();
        //    DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            Model.CoreEnvelopeDesign model = DataRowToModel(ds.Tables[0].Rows[i]);
        //            lstModel.Add(model);
        //        }
        //    }

        //    return lstModel;
        //}


        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public Model.CoreEnvelopeDesign DataRowToModel(DataRow row)
        //{
        //    Model.CoreEnvelopeDesign model = new Model.CoreEnvelopeDesign();
        //    if (row != null)
        //    {
        //        if (row["Id"] != null && row["Id"].ToString() != "")
        //        {
        //            model.Id = int.Parse(row["Id"].ToString());
        //        }
        //        if (row["DesignData_Name"] != null)
        //        {
        //            model.DesignData_Name = row["DesignData_Name"].ToString();
        //        }
        //        if (row["DesignData_Submitter"] != null)
        //        {
        //            model.DesignData_Submitter = row["DesignData_Submitter"].ToString();
        //        }
        //        if (row["Helicopter_Name"] != null)
        //        {
        //            model.Helicopter_Name = row["Helicopter_Name"].ToString();
        //        }
        //        if (row["DataRemark"] != null)
        //        {
        //            model.DataRemark = row["DataRemark"].ToString();
        //        }
        //        if (row["LastModify_Time"] != null)
        //        {
        //            model.LastModify_Time = row["LastModify_Time"].ToString();
        //        }
        //        if (row["DesignTaking_Weight"] != null && row["DesignTaking_Weight"].ToString() != "")
        //        {
        //            model.DesignTaking_Weight = Convert.ToDouble(row["DesignTaking_Weight"]);
        //        }
        //        if (row["CoreEnvelope"] != null)
        //        {
        //            model.CoreEnvelope = row["CoreEnvelope"].ToString();
        //        }
        //    }
        //    return model;
        //}

        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        //public DataSet GetList(string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select Id,DesignData_Name,DesignData_Submitter,Helicopter_Name,DataRemark,LastModify_Time,DesignTaking_Weight,CoreEnvelope ");
        //    strSql.Append(" FROM T_CoreEnvelopeDesignData ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    return DbHelperSQL.Query(strSql.ToString());
        //}

        ///// <summary>
        ///// 获得前几行数据
        ///// </summary>
        //public DataSet GetList(int Top, string strWhere, string filedOrder)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select ");
        //    if (Top > 0)
        //    {
        //        strSql.Append(" top " + Top.ToString());
        //    }
        //    strSql.Append(" Id,DesignData_Name,DesignData_Submitter,Helicopter_Name,DataRemark,LastModify_Time,DesignTaking_Weight,CoreEnvelope ");
        //    strSql.Append(" FROM T_CoreEnvelopeDesignData ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    strSql.Append(" order by " + filedOrder);
        //    return DbHelperSQL.Query(strSql.ToString());
        //}

        ///// <summary>
        ///// 获取记录总数
        ///// </summary>
        //public int GetRecordCount(string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select count(1) FROM T_CoreEnvelopeDesignData ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    object obj = DbHelperSQL.GetSingle(strSql.ToString());
        //    if (obj == null)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return Convert.ToInt32(obj);
        //    }
        //}
        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT * FROM ( ");
        //    strSql.Append(" SELECT ROW_NUMBER() OVER (");
        //    if (!string.IsNullOrEmpty(orderby.Trim()))
        //    {
        //        strSql.Append("order by T." + orderby);
        //    }
        //    else
        //    {
        //        strSql.Append("order by T. desc");
        //    }
        //    strSql.Append(")AS Row, T.*  from T_CoreEnvelopeDesignData T ");
        //    if (!string.IsNullOrEmpty(strWhere.Trim()))
        //    {
        //        strSql.Append(" WHERE " + strWhere);
        //    }
        //    strSql.Append(" ) TT");
        //    strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
        //    return DbHelperSQL.Query(strSql.ToString());
        //}


        #endregion  BasicMethod

        #region Sqlite

        /// <summary>
        /// 获取最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxId()
        {
            return SQLiteDBHelper.GetMaxID("Id", "T_CoreEnvelopeDesignData");
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.CoreEnvelopeDesign model)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("insert into T_CoreEnvelopeDesignData(");
            strBuilder.Append("Id,DesignData_Name,DesignData_Submitter,Helicopter_Name,DataRemark,LastModify_Time,DesignTaking_Weight,CoreEnvelope)");
            strBuilder.Append(" values (");

            strBuilder.Append("" + model.Id + ",");
            strBuilder.Append("'" + model.DesignData_Name + "',");
            strBuilder.Append("'" + model.DesignData_Submitter + "',");
            strBuilder.Append("'" + model.Helicopter_Name + "',");
            strBuilder.Append("'" + model.DataRemark + "',");
            strBuilder.Append("'" + model.LastModify_Time + "',");
            strBuilder.Append("" + model.DesignTaking_Weight + ",");
            strBuilder.Append("'" + model.CoreEnvelope + "'");

            strBuilder.Append(")");

            int rows = SQLiteDBHelper.ExecuteNonQuery(strBuilder.ToString(), null);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.CoreEnvelopeDesign model)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("update T_CoreEnvelopeDesignData set ");
            strBuilder.Append("Id=" + model.Id + ",");
            strBuilder.Append("DesignData_Name='" + model.DesignData_Name + "',");
            strBuilder.Append("DesignData_Submitter='" + model.DesignData_Submitter + "',");
            strBuilder.Append("Helicopter_Name='" + model.Helicopter_Name + "',");
            strBuilder.Append("DataRemark='" + model.DataRemark + "',");
            strBuilder.Append("LastModify_Time='" + model.LastModify_Time + "',");
            strBuilder.Append("DesignTaking_Weight=" + model.DesignTaking_Weight + ",");
            strBuilder.Append("CoreEnvelope='" + model.CoreEnvelope + "'");
            strBuilder.Append(" where Id=" + model.Id.ToString());

            int rows = SQLiteDBHelper.ExecuteNonQuery(strBuilder.ToString(), null);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("delete from T_CoreEnvelopeDesignData ");
            strBuilder.Append(" where Id=" + Id.ToString());

            int rows = SQLiteDBHelper.ExecuteNonQuery(strBuilder.ToString(), null);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM T_CoreEnvelopeDesignData ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = SQLiteDBHelper.GetSingle(strSql.ToString(), null);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.CoreEnvelopeDesign GetModel(int Id)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,DesignData_Name,DesignData_Submitter,Helicopter_Name,DataRemark,LastModify_Time,DesignTaking_Weight,CoreEnvelope from T_CoreEnvelopeDesignData ");
            strSql.Append(" where Id=" + Id.ToString());

            Model.CoreEnvelopeDesign model = new Model.CoreEnvelopeDesign();
            DataTable table = SQLiteDBHelper.ExecuteDataTable(strSql.ToString(), null);
            if (table.Rows.Count > 0)
            {
                return DataRowToModel(table.Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取所有对象实体
        /// </summary>
        public List<Model.CoreEnvelopeDesign> GetListModel()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,DesignData_Name,DesignData_Submitter,Helicopter_Name,DataRemark,LastModify_Time,DesignTaking_Weight,CoreEnvelope from T_CoreEnvelopeDesignData ");

            List<Model.CoreEnvelopeDesign> lstModel = new List<Model.CoreEnvelopeDesign>();
            DataTable table = SQLiteDBHelper.ExecuteDataTable(strSql.ToString(), null);
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Model.CoreEnvelopeDesign model = DataRowToModel(table.Rows[i]);
                    lstModel.Add(model);
                }
            }

            return lstModel;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.CoreEnvelopeDesign DataRowToModel(DataRow row)
        {
            Model.CoreEnvelopeDesign model = new Model.CoreEnvelopeDesign();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["DesignData_Name"] != null)
                {
                    model.DesignData_Name = row["DesignData_Name"].ToString();
                }
                if (row["DesignData_Submitter"] != null)
                {
                    model.DesignData_Submitter = row["DesignData_Submitter"].ToString();
                }
                if (row["Helicopter_Name"] != null)
                {
                    model.Helicopter_Name = row["Helicopter_Name"].ToString();
                }
                if (row["DataRemark"] != null)
                {
                    model.DataRemark = row["DataRemark"].ToString();
                }
                if (row["LastModify_Time"] != null)
                {
                    model.LastModify_Time = row["LastModify_Time"].ToString();
                }
                if (row["DesignTaking_Weight"] != null && row["DesignTaking_Weight"].ToString() != "")
                {
                    model.DesignTaking_Weight = Convert.ToDouble(row["DesignTaking_Weight"]);
                }
                if (row["CoreEnvelope"] != null)
                {
                    model.CoreEnvelope = row["CoreEnvelope"].ToString();
                }
            }
            return model;
        }

        #endregion
    }
}
