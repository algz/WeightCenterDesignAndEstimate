using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    /// <summary>
    /// 型号重量数据访问类
    /// </summary>
    public class DALTypeWeightData : IDAL.ITypeWeightData
    {
        public DALTypeWeightData()
        { }

        #region  SqlServer

        ///// <summary>
        ///// 获取最大ID
        ///// </summary>
        ///// <returns></returns>
        //public int GetMaxId()
        //{
        //    return DbHelperSQL.GetMaxID("Id", "T_TypeWeightData");
        //}

        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public bool Add(Model.TypeWeightData model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("insert into T_TypeWeightData(");
        //    strSql.Append("Id,Helicopter_Name,Last_ModifyTime,Helicopter_Type,DesignTaking_Weight,MaxTaking_Weight,EmptyWeight,MainSystem_Name,Helicoter_Country)");
        //    strSql.Append(" values (");
        //    strSql.Append("@Id,@Helicopter_Name,@Last_ModifyTime,@Helicopter_Type,@DesignTaking_Weight,@MaxTaking_Weight,@EmptyWeight,@MainSystem_Name,@Helicoter_Country)");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@Id", SqlDbType.Int,4),
        //            new SqlParameter("@Helicopter_Name", SqlDbType.NVarChar,50),
        //            new SqlParameter("@Last_ModifyTime", SqlDbType.NVarChar,50),
        //            new SqlParameter("@Helicopter_Type", SqlDbType.NVarChar,50),
        //            new SqlParameter("@DesignTaking_Weight", SqlDbType.Float,8),
        //            new SqlParameter("@MaxTaking_Weight", SqlDbType.Float,8),
        //            new SqlParameter("@EmptyWeight", SqlDbType.Float,8),
        //            new SqlParameter("@MainSystem_Name", SqlDbType.NVarChar,-1),
        //            new SqlParameter("@Helicoter_Country", SqlDbType.NVarChar,50)};
        //    parameters[0].Value = model.Id;
        //    parameters[1].Value = model.Helicopter_Name;
        //    parameters[2].Value = model.Last_ModifyTime;
        //    parameters[3].Value = model.Helicopter_Type;
        //    parameters[4].Value = model.DesignTaking_Weight;
        //    parameters[5].Value = model.MaxTaking_Weight;
        //    parameters[6].Value = model.EmptyWeight;
        //    parameters[7].Value = model.MainSystem_Name;
        //    parameters[8].Value = model.Helicoter_Country;

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
        //public bool Update(Model.TypeWeightData model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update T_TypeWeightData set ");
        //    strSql.Append("Id=@Id,");
        //    strSql.Append("Helicopter_Name=@Helicopter_Name,");
        //    strSql.Append("Last_ModifyTime=@Last_ModifyTime,");
        //    strSql.Append("Helicopter_Type=@Helicopter_Type,");
        //    strSql.Append("DesignTaking_Weight=@DesignTaking_Weight,");
        //    strSql.Append("MaxTaking_Weight=@MaxTaking_Weight,");
        //    strSql.Append("EmptyWeight=@EmptyWeight,");
        //    strSql.Append("MainSystem_Name=@MainSystem_Name,");
        //    strSql.Append("Helicoter_Country=@Helicoter_Country");
        //    strSql.Append(" where Id=" + model.Id.ToString());
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@Id", SqlDbType.Int,4),
        //            new SqlParameter("@Helicopter_Name", SqlDbType.NVarChar,50),
        //            new SqlParameter("@Last_ModifyTime", SqlDbType.NVarChar,50),
        //            new SqlParameter("@Helicopter_Type", SqlDbType.NVarChar,50),
        //            new SqlParameter("@DesignTaking_Weight", SqlDbType.Float,8),
        //            new SqlParameter("@MaxTaking_Weight", SqlDbType.Float,8),
        //            new SqlParameter("@EmptyWeight", SqlDbType.Float,8),
        //            new SqlParameter("@MainSystem_Name", SqlDbType.NVarChar,-1),
        //            new SqlParameter("@Helicoter_Country", SqlDbType.NVarChar,50)};
        //    parameters[0].Value = model.Id;
        //    parameters[1].Value = model.Helicopter_Name;
        //    parameters[2].Value = model.Last_ModifyTime;
        //    parameters[3].Value = model.Helicopter_Type;
        //    parameters[4].Value = model.DesignTaking_Weight;
        //    parameters[5].Value = model.MaxTaking_Weight;
        //    parameters[6].Value = model.EmptyWeight;
        //    parameters[7].Value = model.MainSystem_Name;
        //    parameters[8].Value = model.Helicoter_Country;

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
        //    strSql.Append("delete from T_TypeWeightData ");
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
        //public Model.TypeWeightData GetModel(int Id)
        //{
        //    //该表无主键信息，请自定义主键/条件字段
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select  top 1 Id,Helicopter_Name,Last_ModifyTime,Helicopter_Type,DesignTaking_Weight,MaxTaking_Weight,EmptyWeight,MainSystem_Name,Helicoter_Country from T_TypeWeightData ");
        //    strSql.Append(" where Id=" + Id.ToString());
        //    SqlParameter[] parameters = {
        //    };

        //    Model.TypeWeightData model = new Model.TypeWeightData();
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
        ///// 获取所有实体
        ///// </summary>
        ///// <returns></returns>
        //public List<Model.TypeWeightData> GetListModel()
        //{
        //    //该表无主键信息，请自定义主键/条件字段
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select Id,Helicopter_Name,Last_ModifyTime,Helicopter_Type,DesignTaking_Weight,MaxTaking_Weight,EmptyWeight,MainSystem_Name,Helicoter_Country from T_TypeWeightData ");
        //    SqlParameter[] parameters = {
        //    };

        //    List<Model.TypeWeightData> lstWeightData = new List<Model.TypeWeightData>();
        //    DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            Model.TypeWeightData model = DataRowToModel(ds.Tables[0].Rows[i]);
        //            lstWeightData.Add(model);
        //        }
        //    }

        //    return lstWeightData;
        //}

        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public Model.TypeWeightData DataRowToModel(DataRow row)
        //{
        //    Model.TypeWeightData model = new Model.TypeWeightData();
        //    if (row != null)
        //    {
        //        if (row["Id"] != null && row["Id"].ToString() != "")
        //        {
        //            model.Id = int.Parse(row["Id"].ToString());
        //        }
        //        if (row["Helicopter_Name"] != null)
        //        {
        //            model.Helicopter_Name = row["Helicopter_Name"].ToString();
        //        }
        //        if (row["Last_ModifyTime"] != null)
        //        {
        //            model.Last_ModifyTime = row["Last_ModifyTime"].ToString();
        //        }
        //        if (row["Helicopter_Type"] != null)
        //        {
        //            model.Helicopter_Type = row["Helicopter_Type"].ToString();
        //        }
        //        if (row["DesignTaking_Weight"] != null && row["DesignTaking_Weight"].ToString() != "")
        //        {
        //            model.DesignTaking_Weight = Convert.ToDouble(row["DesignTaking_Weight"]);
        //        }
        //        if (row["MaxTaking_Weight"] != null && row["MaxTaking_Weight"].ToString() != "")
        //        {
        //            model.MaxTaking_Weight = Convert.ToDouble(row["MaxTaking_Weight"]);
        //        }
        //        if (row["EmptyWeight"] != null && row["EmptyWeight"].ToString() != "")
        //        {
        //            model.EmptyWeight = Convert.ToDouble(row["EmptyWeight"]);
        //        }
        //        if (row["MainSystem_Name"] != null)
        //        {
        //            model.MainSystem_Name = row["MainSystem_Name"].ToString();
        //        }
        //        if (row["Helicoter_Country"] != null)
        //        {
        //            model.Helicoter_Country = row["Helicoter_Country"].ToString();
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
        //    strSql.Append("select Id,Helicopter_Name,Last_ModifyTime,Helicopter_Type,DesignTaking_Weight,MaxTaking_Weight,EmptyWeight,MainSystem_Name,Helicoter_Country ");
        //    strSql.Append(" FROM T_TypeWeightData ");
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
        //    strSql.Append(" Id,Helicopter_Name,Last_ModifyTime,Helicopter_Type,DesignTaking_Weight,MaxTaking_Weight,EmptyWeight,MainSystem_Name,Helicoter_Country ");
        //    strSql.Append(" FROM T_TypeWeightData ");
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
        //    strSql.Append("select count(1) FROM T_TypeWeightData ");
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
        //    strSql.Append(")AS Row, T.*  from T_TypeWeightData T ");
        //    if (!string.IsNullOrEmpty(strWhere.Trim()))
        //    {
        //        strSql.Append(" WHERE " + strWhere);
        //    }
        //    strSql.Append(" ) TT");
        //    strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
        //    return DbHelperSQL.Query(strSql.ToString());
        //}

        #endregion

        #region Sqlite

        /// <summary>
        /// 获取最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxId()
        {
            return SQLiteDBHelper.GetMaxID("Id", "T_TypeWeightData");
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.TypeWeightData model)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("insert into T_TypeWeightData(");
            strBuilder.Append("Id,Helicopter_Name,Last_ModifyTime,Helicopter_Type,DesignTaking_Weight,MaxTaking_Weight,EmptyWeight,MainSystem_Name,Helicoter_Country)");
            strBuilder.Append(" values (");

            strBuilder.Append("" + model.Id + ",");
            strBuilder.Append("'" + model.Helicopter_Name + "',");
            strBuilder.Append("'" + model.Last_ModifyTime + "',");
            strBuilder.Append("'" + model.Helicopter_Type + "',");
            strBuilder.Append("" + model.DesignTaking_Weight + ",");
            strBuilder.Append("" + model.MaxTaking_Weight + ",");
            strBuilder.Append("" + model.EmptyWeight + ",");
            strBuilder.Append("'" + model.MainSystem_Name + "',");
            strBuilder.Append("'" + model.Helicoter_Country + "'");

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
        public bool Update(Model.TypeWeightData model)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("update T_TypeWeightData set ");
            strBuilder.Append("Id=" + model.Id + ",");
            strBuilder.Append("Helicopter_Name='" + model.Helicopter_Name + "',");
            strBuilder.Append("Last_ModifyTime='" + model.Last_ModifyTime + "',");
            strBuilder.Append("Helicopter_Type='" + model.Helicopter_Type + "',");
            strBuilder.Append("DesignTaking_Weight=" + model.DesignTaking_Weight + ",");
            strBuilder.Append("MaxTaking_Weight=" + model.MaxTaking_Weight + ",");
            strBuilder.Append("EmptyWeight=" + model.EmptyWeight + ",");
            strBuilder.Append("MainSystem_Name='" + model.MainSystem_Name + "',");
            strBuilder.Append("Helicoter_Country='" + model.Helicoter_Country + "'");

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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_TypeWeightData ");
            strSql.Append(" where Id=" + Id.ToString());

            int rows = SQLiteDBHelper.ExecuteNonQuery(strSql.ToString(), null);
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
        /// 得到一个对象实体
        /// </summary>
        public Model.TypeWeightData GetModel(int Id)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Helicopter_Name,Last_ModifyTime,Helicopter_Type,DesignTaking_Weight,MaxTaking_Weight,EmptyWeight,MainSystem_Name,Helicoter_Country from T_TypeWeightData ");
            strSql.Append(" where Id=" + Id.ToString());

            Model.TypeWeightData model = new Model.TypeWeightData();
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
        /// 获取所有实体
        /// </summary>
        /// <returns></returns>
        public List<Model.TypeWeightData> GetListModel()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Helicopter_Name,Last_ModifyTime,Helicopter_Type,DesignTaking_Weight,MaxTaking_Weight,EmptyWeight,MainSystem_Name,Helicoter_Country from T_TypeWeightData ");

            List<Model.TypeWeightData> lstWeightData = new List<Model.TypeWeightData>();
            DataTable table = SQLiteDBHelper.ExecuteDataTable(strSql.ToString(), null);

            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Model.TypeWeightData model = DataRowToModel(table.Rows[i]);
                    lstWeightData.Add(model);
                }
            }
            return lstWeightData;
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM T_TypeWeightData ");
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
        public Model.TypeWeightData DataRowToModel(DataRow row)
        {
            Model.TypeWeightData model = new Model.TypeWeightData();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["Helicopter_Name"] != null)
                {
                    model.Helicopter_Name = row["Helicopter_Name"].ToString();
                }
                if (row["Last_ModifyTime"] != null)
                {
                    model.Last_ModifyTime = row["Last_ModifyTime"].ToString();
                }
                if (row["Helicopter_Type"] != null)
                {
                    model.Helicopter_Type = row["Helicopter_Type"].ToString();
                }
                if (row["DesignTaking_Weight"] != null && row["DesignTaking_Weight"].ToString() != "")
                {
                    model.DesignTaking_Weight = Convert.ToDouble(row["DesignTaking_Weight"]);
                }
                if (row["MaxTaking_Weight"] != null && row["MaxTaking_Weight"].ToString() != "")
                {
                    model.MaxTaking_Weight = Convert.ToDouble(row["MaxTaking_Weight"]);
                }
                if (row["EmptyWeight"] != null && row["EmptyWeight"].ToString() != "")
                {
                    model.EmptyWeight = Convert.ToDouble(row["EmptyWeight"]);
                }
                if (row["MainSystem_Name"] != null)
                {
                    model.MainSystem_Name = row["MainSystem_Name"].ToString();
                }
                if (row["Helicoter_Country"] != null)
                {
                    model.Helicoter_Country = row["Helicoter_Country"].ToString();
                }
            }
            return model;
        }

        #endregion
    }
}
