using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using WeightCenterDesignAndEstimateSoft.Properties;

namespace WeightCenterDesignAndEstimateSoft
{
    /// <summary>
    /// WDM集成模块
    /// </summary>
    public class WDMIntegrationModule
    {

        /// <summary>
        /// 不能设置结构体在内存中的对齐方式Pack = 1，应为0（默认），即按平台默认对齐
        ///  CharSet = CharSet.Ansi 可选
        ///  结构体位置必须与DLL中一致
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct Air
        {
            /// <summary>
            /// 飞机型号唯一标识符 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 65)]
            public String ID;
            /// <summary>
            ///  型号名称
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 130)]
            public String MC;
            [MarshalAs(UnmanagedType.R4)]
            public float LMAC;
            [MarshalAs(UnmanagedType.R4)]
            public float XMAC;
            [MarshalAs(UnmanagedType.R4)]
            public float WTNOL;
            [MarshalAs(UnmanagedType.R4)]
            public float WTMAX;
            [MarshalAs(UnmanagedType.R4)]
            public float WTMIN;
            [MarshalAs(UnmanagedType.R4)]
            public float stdFWD;
            [MarshalAs(UnmanagedType.R4)]
            public float stdAFT;
            [MarshalAs(UnmanagedType.R4)]
            public float stdLFT;
            [MarshalAs(UnmanagedType.R4)]
            public float stdRIT;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct TStmc
        {
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 65)]
            public String BB;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 65)]
            public String ID;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 130)]
            public String MC;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.R4)]
            public float ZTW;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.R4)]
            public float XCG;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.R4)]
            public float YCG;
            [MarshalAs(UnmanagedType.R4)]
            public float ZCG;
            [MarshalAs(UnmanagedType.R4)]
            public float IXX;
            [MarshalAs(UnmanagedType.R4)]
            public float IYY;
            [MarshalAs(UnmanagedType.R4)]
            public float IZZ;
            [MarshalAs(UnmanagedType.R4)]
            public float IXY;
            [MarshalAs(UnmanagedType.R4)]
            public float IYZ;
            [MarshalAs(UnmanagedType.R4)]
            public float IZX;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 65)]
            public String LOCK;
        }

        /// <summary>
        /// 飞机零组件质量特性数据结构表描述
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct TOper
        {
            /// <summary>
            /// 上级组件（或机型ID）标识符
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 65)]
            public String BB;
            /// <summary>
            /// 零件唯一主键标识符
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 65)]
            public String ID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public String TH;
            /// <summary>
            /// 组件名称
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 130)]
            public String MC;
            [MarshalAs(UnmanagedType.I4)]
            public int JS;
            [MarshalAs(UnmanagedType.Bool)]
            public bool DC;
            /// <summary>
            /// 目标重量
            /// </summary>
            [MarshalAs(UnmanagedType.R4)]
            public float TGW;
            /// <summary>
            /// 模型重量
            /// </summary>
            [MarshalAs(UnmanagedType.R4)]
            public float DGW;
            /// <summary>
            /// 实测重量
            /// </summary>
            [MarshalAs(UnmanagedType.R4)]
            public float SCW;
            /// <summary>
            /// 设计重量
            /// </summary>
            [MarshalAs(UnmanagedType.R4)]
            public float ZTW;
            [MarshalAs(UnmanagedType.R4)]
            public float XCG;
            [MarshalAs(UnmanagedType.R4)]
            public float YCG;
            [MarshalAs(UnmanagedType.R4)]
            public float ZCG;
            [MarshalAs(UnmanagedType.R4)]
            public float IXX;
            [MarshalAs(UnmanagedType.R4)]
            public float IYY;
            [MarshalAs(UnmanagedType.R4)]
            public float IZZ;
            [MarshalAs(UnmanagedType.R4)]
            public float IXY;
            [MarshalAs(UnmanagedType.R4)]
            public float IYZ;
            [MarshalAs(UnmanagedType.R4)]
            public float IZX;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
            public string CL;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
            public string JG;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
            public string QY;
            [MarshalAs(UnmanagedType.Bool)]
            public bool CC;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 65)]
            public String LOCK;
        }
/*
        //引入DLL函数
        [DllImport("AccessWDM.dll", EntryPoint = "GetAircCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern int GetAircCount(string mystr);

        [DllImport("AccessWDM.dll", EntryPoint = "GetAirc", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern int GetAirc(IntPtr p, string mystr);

        [DllImport("AccessWDM.dll", EntryPoint = "GetStmcCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern int GetStmcCount(string mystr, string str);

        [DllImport("AccessWDM.dll", EntryPoint = "GetStmc", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern int GetStmc(IntPtr p, string mystr, string str);

        [DllImport("AccessWDM.dll", EntryPoint = "GetOperCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern int GetOperCount(string mystr, string str);

        [DllImport("AccessWDM.dll", EntryPoint = "GetOper", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern int GetOper(IntPtr p, string mystr, string str);
        */
        /// <summary>
        /// WDM:飞机型号特征数据表结构描述
        /// </summary>
        /// <param name="fileName">WDM文件路径</param>
        /// <returns></returns>
        public static Air[] getAircs()
        {
            /*
             if (!File.Exists(fileName))
            {
                return null;
            }
            int c = GetAircCount(fileName);
            Air[] airs = new Air[c];
            IntPtr[] ptArray = new IntPtr[1];
            ptArray[0] = Marshal.AllocHGlobal((Marshal.SizeOf(typeof(Air))) * 100);
            IntPtr pt = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Air)));
            Marshal.Copy(ptArray, 0, pt, 1);
            GetAirc(pt, fileName);
            for (int i = 0; i < c; i++)
            {
                Air a = (Air)Marshal.PtrToStructure((IntPtr)((UInt32)ptArray[0] + i * 232), typeof(Air));
                airs[i] = a;
            }
            Marshal.FreeHGlobal(ptArray[0]);
            Marshal.FreeHGlobal(pt);
             */


            string connectString = getWDMDBConnectionStrings();
            if (connectString == "")
            {
                return null;
            }
            using (SqlConnection sqlCnt = new SqlConnection(connectString))
            {
                using (SqlCommand command = sqlCnt.CreateCommand())
                {
                    try
                    {
                        sqlCnt.Open();
                        string tableName = getWDMDBQueryName("WDM_AIRC");
                        if (tableName == "")
                        {
                            return null;
                        }
                        command.CommandText = "select count(1) from " + tableName;
                        int c = Convert.ToInt32(command.ExecuteScalar());
                        Air[] airs = new Air[c];

                        command.CommandText = "Select * from " + tableName;
                        SqlDataReader reader = command.ExecuteReader();		//执行SQL，返回一个“流”
                        int i = 0;
                        while (reader.Read())
                        {
                            Air a = new Air();
                            a.ID = reader["id"].ToString();
                            a.MC = reader["MC"].ToString();
                            //a.XMAC = Convert.ToSingle(reader["XMAC"]); ;
                            //a.WTNOL = Convert.ToSingle(reader["stdRIT"]);
                            //a.WTMAX = Convert.ToSingle(reader["stdRIT"]);
                            //a.WTMIN = Convert.ToSingle(reader["stdRIT"]);
                            //a.stdFWD = Convert.ToSingle(reader["stdRIT"]);
                            //a.stdAFT = Convert.ToSingle(reader["stdRIT"]);
                            //a.stdLFT = Convert.ToSingle(reader["stdRIT"]);
                            //a.stdRIT = Convert.ToSingle(reader["stdRIT"]);
                            airs[i++] = a;
                        }
                        return airs;
                    }
                    catch
                    {
                        return null;
                    }
                    
                }
            }

            
        }

        /// <summary>
        /// WDM:飞机零组件质量特性数据结构表描述
        /// </summary>
        /// <param name="id">机型ID</param>
        /// <param name="fileName">wdm文件路径</param>
        /// <returns></returns>
        public static TOper[] GetOpers(string id)
        {
            //string id = @"aaa2012-03-22 13:31:07NOO";
            id += "NOO";
            string connectString = getWDMDBConnectionStrings();
            if (connectString == "")
            {
                return null;
            }
            using (SqlConnection sqlCnt = new SqlConnection(connectString))
            {
                using (SqlCommand command = sqlCnt.CreateCommand())
                {
                    sqlCnt.Open();
                    string tableName=getWDMDBQueryName("WDM_OPER");//"select count(1) from mm_oper";
                    if (tableName == "")
                    {
                        return null;
                    }
                    command.CommandText = "select count(1) from " + tableName+" where bb='"+id+"' and cc=0";
                    int c = Convert.ToInt32(command.ExecuteScalar());
                    TOper[] topers = new TOper[c];

                    command.CommandText = "Select * from " + tableName+" where bb='"+id+"' and cc=0";
                    SqlDataReader reader = command.ExecuteReader();		//执行SQL，返回一个“流”
                    int i = 0;
                    while (reader.Read())
                    {
                        TOper to = new TOper();
                        to.ID = reader["id"].ToString();
                        to.BB = reader["BB"].ToString();
                        to.TH = reader["TH"].ToString();
                        to.MC = reader["MC"].ToString();
                        to.TGW = Convert.ToSingle(reader["TGW"]);
                        to.DGW = Convert.ToSingle(reader["DGW"]);
                        to.SCW = Convert.ToSingle(reader["SCW"]);
                        to.ZTW = Convert.ToSingle(reader["ZTW"]);
                        topers[i++] = to;
                    }
                    return topers;
                }
            }

            /*
            int c = GetOperCount(fileName, id);
            TOper[] topers = new TOper[c];

            IntPtr[] ptArray = new IntPtr[1];
            ptArray[0] = Marshal.AllocHGlobal((Marshal.SizeOf(typeof(TOper))) * 10000);
            IntPtr pt = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(TOper)));
            Marshal.Copy(ptArray, 0, pt, 1);
            GetOper(pt, fileName, id);
            for (int i = 0; i < c; i++)
            {
                TOper a = (TOper)Marshal.PtrToStructure((IntPtr)((UInt32)ptArray[0] + i * 1036), typeof(TOper));
                topers[i] = a;
            }
            Marshal.FreeHGlobal(ptArray[0]);
            Marshal.FreeHGlobal(pt);

            return topers;
             */
        }

        /// <summary>
        /// WDM:装载状态飞机结构特性表描述
        /// </summary>
        /// <param name="id">机型ID</param>
        /// <param name="fileName">wdm文件路径</param>
        /// <returns></returns>
        public static TStmc[] GetStmcs(string id)
        {
            //string id = @"aaa2012-03-22 13:31:07NOS";
            id += "NOS";
            string connectString = getWDMDBConnectionStrings();
            if (connectString == "")
            {
                return null;
            }
            using (SqlConnection sqlCnt = new SqlConnection(connectString))
            {
                using (SqlCommand command = sqlCnt.CreateCommand())
                {
                    sqlCnt.Open();
                    string tableName = getWDMDBQueryName("WDM_STMC");
                    if (tableName == "")
                    {
                        return null;
                    }
                    command.CommandText = "select count(1) from " + tableName+" where bb='"+id+"'";
                    int c = Convert.ToInt32(command.ExecuteScalar());
                    TStmc[] tstmcs = new TStmc[c];

                    command.CommandText = "Select * from " + tableName+" where bb='"+id+"'";
                    SqlDataReader reader = command.ExecuteReader();		//执行SQL，返回一个“流”
                    int i = 0;
                    while (reader.Read())
                    {
                        TStmc ts = new TStmc();
                        ts.ID = reader["id"].ToString();
                        ts.BB = reader["BB"].ToString();
                        ts.MC = reader["MC"].ToString();
                        ts.ZTW = Convert.ToSingle(reader["ZTW"]);
                        ts.XCG = Convert.ToSingle(reader["XCG"]);
                        ts.XCG = Convert.ToSingle(reader["XCG"]);
                        tstmcs[i++] = ts;
                    }
                    return tstmcs;
                }
            }

            /*
            int c = GetStmcCount(fileName, id);
            TStmc[] tstmc = new TStmc[c];

            IntPtr[] ptArray = new IntPtr[1];
            ptArray[0] = Marshal.AllocHGlobal((Marshal.SizeOf(typeof(TStmc))) * 1000);
            IntPtr pt = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(TStmc)));
            Marshal.Copy(ptArray, 0, pt, 1);
            GetStmc(pt, fileName, id);
            for (int i = 0; i < c; i++)
            {
                TStmc a = (TStmc)Marshal.PtrToStructure((IntPtr)((UInt32)ptArray[0] + i * 368), typeof(TStmc));
                tstmc[i] = a;
            }
            Marshal.FreeHGlobal(ptArray[0]);
            Marshal.FreeHGlobal(pt);
            return tstmc;
             * */
        }


        #region 从系统配置文件读取WDM参数
        /// <summary>
        /// 获取WDM数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string getWDMDBConnectionStrings()
        {
            //System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            //string strFileName = System.AppDomain.CurrentDomain.BaseDirectory + "WES.exe";
            //AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
            string dirPath = Environment.CurrentDirectory;// Application.ExecutablePath;
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);//OpenExeConfiguration(strFileName);
            ConnectionStringSettings connectStringSetting = config.ConnectionStrings.ConnectionStrings["WDMConnectionString"];
            if (connectStringSetting == null)
            {
                return "";
            }
            return connectStringSetting.ConnectionString;//config.AppSettings.Settings["WDMFileName"].Value;
        }

        /// <summary>
        /// 设置WDM数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static bool setWDMDBConnectionStrings(string connectString)
        {
            //string connectString = "Data Source=" + ip + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password="+password;
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringSettings connectStringSetting = config.ConnectionStrings.ConnectionStrings["WDMConnectionString"];
            if (connectStringSetting == null)
            {
                connectStringSetting = new ConnectionStringSettings("WDMConnectionString", connectString);
                config.ConnectionStrings.ConnectionStrings.Add(connectStringSetting);
            }
            connectStringSetting.ConnectionString=connectString;
            config.Save(ConfigurationSaveMode.Modified,true);// 重新载入配置文件的配置节
            return true;
        }

        /// <summary>
        /// 获取WDM查询字符串
        /// </summary>
        /// <param name="queryName"></param>
        /// <returns></returns>
        public static string getWDMDBQueryName(string queryName)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationElement element = config.AppSettings.Settings[queryName];
            if (element == null)
            {
                return "";
            }
            return element.Value;
        }

        /// <summary>
        /// 设置WDM查询字符串
        /// </summary>
        /// <param name="queryName"></param>
        /// <returns></returns>
        public static bool setWDMDBQueryName(string queryName,string queryValue)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationElement element = config.AppSettings.Settings[queryName];
            if (element == null)
            {
                config.AppSettings.Settings.Add(new KeyValueConfigurationElement(queryName, queryValue));
            }
            else
            {
                element.Value = queryValue;
            }
            config.Save(ConfigurationSaveMode.Modified);// 重新载入配置文件的配置节
            return true;
        }

        /// <summary>
        /// 测试WDM数据库连接
        /// </summary>
        /// <returns></returns>
        public static bool testWDMDBConnection(string connectString,string tableName)
        {
            //string connectString = getWDMDBConnectionStrings();
            if (connectString == "")
            {
                return false;
            }
            using (SqlConnection sqlCnt = new SqlConnection(connectString))
            {
                using (SqlCommand command = sqlCnt.CreateCommand())
                {
                    try
                    {
                        sqlCnt.Open();
                        if (tableName != null && tableName != "")
                        {
                            command.CommandText = "Select count(1) from "+tableName;
                            command.ExecuteNonQuery();		//执行SQL，返回一个“流”
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
   
                }
            }
        }

        /*
        /// <summary>
        /// 设置WDM文件路径
        /// </summary>
        /// <param name="path"></param>
        public static void setWDMDBFilePath(string path)
        {
            string appDomainConfigFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            //string strFileName = "WES.exe";
            string name = Process.GetCurrentProcess().MainModule.FileName;
            //AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);//.OpenExeConfiguration(name);//
            config.AppSettings.Settings["WDMFileName"].Value = path;
            config.Save(ConfigurationSaveMode.Modified);// 重新载入配置文件的配置节
            //System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 获取WDM文件路径
        /// </summary>
        /// <returns></returns>
        public static string getWDMDBFilePath()
        {
            //System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            //string strFileName = System.AppDomain.CurrentDomain.BaseDirectory + "WES.exe";
            //AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
            string dirPath = Environment.CurrentDirectory;// Application.ExecutablePath;
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);//OpenExeConfiguration(strFileName);
            string filePath = config.AppSettings.Settings["WDMFileName"].Value;
            if (filePath == null || filePath.Equals(""))
            {
                string[] files = Directory.GetFiles(dirPath, "*.wdm");
                filePath = files.Count() != 0 ? files[0] : "wdm.wdm";
            }
            if (!File.Exists(filePath))
            {
                //MessageBox.Show("WDM文件不存在!");
                return "";
            }
            return filePath;
        }*/
        #endregion

    }

}
