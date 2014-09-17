using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;

namespace DAL
{
    public class SQLiteDBHelper
    {
        private static readonly string connectionString = "Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\DataBase\CoreDesignSoft.db";

        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql, null);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        ///
        /// 创建SQLite数据库文件
        /// 
        /// 要创建的SQLite数据库文件路径 
        public static void CreateDB(string dbPath)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + dbPath))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {

                    command.CommandText = "CREATE TABLE Demo(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE)";

                    command.ExecuteNonQuery();

                    command.CommandText = "DROP TABLE Demo";

                    command.ExecuteNonQuery();

                }

                connection.Close();
            }
        }

        ///


        /// 对SQLite数据库执行增删改操作，返回受影响的行数。

        /// 

        /// 要执行的增删改的SQL语句 

        /// 执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准 

        /// 
        public static int ExecuteNonQuery(string sql, SQLiteParameter[] parameters)
        {

            int affectedRows = 0;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {

                connection.Open();

                using (DbTransaction transaction = connection.BeginTransaction())
                {

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {

                        command.CommandText = sql;

                        if (parameters != null)
                        {

                            command.Parameters.AddRange(parameters);
                        }

                        affectedRows = command.ExecuteNonQuery();

                    }

                    transaction.Commit();

                }
                connection.Close();

            }

            return affectedRows;

        }

        ///


        /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例

        /// 

        /// 要执行的查询语句 

        /// 执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准 

        /// 
        public static SQLiteDataReader ExecuteReader(string sql, SQLiteParameter[] parameters)
        {

            SQLiteConnection connection = new SQLiteConnection(connectionString);

            SQLiteCommand command = new SQLiteCommand(sql, connection);

            if (parameters != null)
            {

                command.Parameters.AddRange(parameters);

            }

            connection.Open();

            return command.ExecuteReader(CommandBehavior.CloseConnection);

        }

        ///


        /// 执行一个查询语句，返回一个包含查询结果的DataTable

        /// 

        /// 要执行的查询语句 

        /// 执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准 

        /// 
        public static DataTable ExecuteDataTable(string sql, SQLiteParameter[] parameters)
        {

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {

                    if (parameters != null)
                    {

                        command.Parameters.AddRange(parameters);

                    }

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

                    DataTable data = new DataTable();

                    adapter.Fill(data);

                    return data;

                }

            }

        }

        ///


        /// 执行一个查询语句，返回查询结果的第一行第一列

        /// 

        /// 要执行的查询语句 

        /// 执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准 

        /// 
        public static Object ExecuteScalar(string sql, SQLiteParameter[] parameters)
        {

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {

                    if (parameters != null)
                    {

                        command.Parameters.AddRange(parameters);

                    }

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

                    DataTable data = new DataTable();

                    adapter.Fill(data);

                    return data;

                }

            }

        }


        public static Object GetSingle(string sql, SQLiteParameter[] parameters)
        {

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {

                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {

                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }

                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }

            }

        }

        ///


        /// 查询数据库中的所有数据类型信息

        /// 

        /// 
        public static DataTable GetSchema()
        {

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {

                connection.Open();

                DataTable data = connection.GetSchema("TABLES");

                connection.Close();

                //foreach (DataColumn column in data.Columns)

                //{

                // Console.WriteLine(column.ColumnName);

                //}

                return data;

            }

        }
    }

}



