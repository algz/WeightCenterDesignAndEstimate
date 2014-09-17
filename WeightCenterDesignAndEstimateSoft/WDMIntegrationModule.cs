using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WeightCenterDesignAndEstimateSoft
{
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
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 65)]
            public String BB;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 65)]
            public String ID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 130)]
            public String MC;
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

        /// <summary>
        /// WDM:飞机型号特征数据表结构描述
        /// </summary>
        /// <param name="fileName">WDM文件路径</param>
        /// <returns></returns>
        public static Air[] getAircs(string fileName)
        {
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

            return airs;
        }

        /// <summary>
        /// WDM:飞机零组件质量特性数据结构表描述
        /// </summary>
        /// <param name="id">机型ID</param>
        /// <param name="fileName">wdm文件路径</param>
        /// <returns></returns>
        public static TOper[] GetOpers(string id, string fileName)
        {
            //string id = @"aaa2012-03-22 13:31:07NOO";
            id += "NOO";
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
        }

        /// <summary>
        /// WDM:装载状态飞机结构特性表描述
        /// </summary>
        /// <param name="id">机型ID</param>
        /// <param name="fileName">wdm文件路径</param>
        /// <returns></returns>
        public static TStmc[] GetStmcs(string id, string fileName)
        {
            //string id = @"aaa2012-03-22 13:31:07NOS";
            id += "NOS";
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
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    string id = "ad2012-05-06 09:26:33NOS";// @"aaa2012-03-22 13:31:07" + "NOS";
        //    int c = GetStmcCount("wdm.wdm", id);
        //    TStmc[] tstmc = new TStmc[c];

        //    IntPtr[] ptArray = new IntPtr[1];
        //    ptArray[0] = Marshal.AllocHGlobal((Marshal.SizeOf(typeof(TStmc))) * 1000);
        //    IntPtr pt = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(TStmc)));
        //    Marshal.Copy(ptArray, 0, pt, 1);
        //    GetStmc(pt, @"F:\金航数码文件\602所\dotnetcallDll\dotnetcallDll\WindowsFormsApplication1\bin\Debug\wdm.mdb", id);
        //    for (int i = 0; i < c; i++)
        //    {
        //        TStmc a = (TStmc)Marshal.PtrToStructure((IntPtr)((UInt32)ptArray[0] + i * 368), typeof(TStmc));
        //        tstmc[i] = a;
        //    }
        //    Marshal.FreeHGlobal(ptArray[0]);
        //    Marshal.FreeHGlobal(pt);

        //    this.textBox1.Text = c.ToString();
        //}



    }

}
