using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 重心坐标数据对象
    /// </summary>
    public class CorePointData
    {
        public CorePointData()
        {

        }
        public CorePointData(string name)
        {
            pointName = name;
        }

        public CorePointData(CorePointData cpd)
        {
            pointName = cpd.pointName;
            pointXValue = cpd.pointXValue;
            pointYValue = cpd.pointYValue;
        }

        public CorePointData(double fx, double fy)
        {
            pointName = "";
            pointXValue = fx;
            pointYValue = fy;
        }

        public CorePointData(double fx, double fy, string name)
        {
            pointName = name;
            pointXValue = fx;
            pointYValue = fy;
        }

        /// <summary>
        /// X轴单位
        /// </summary>
        static public string pointXUnit = "mm";

        /// <summary>
        /// Y轴单位
        /// </summary> 
        static public string pointYUnit = "kg";

        /// <summary>
        /// 重心坐标点名称
        /// </summary>
        public string pointName
        {
            get;
            set;
        }

        /// <summary>
        /// X数值
        /// </summary>
        public double pointXValue
        {
            get;
            set;
        }

        /// <summary>
        /// Y数值
        /// </summary>
        public double pointYValue
        {
            get;
            set;
        }

        /// <summary>
        /// 获取从字符串转换成重心数据List
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<CorePointData> GetStringToListCorePointData(string strCoreEnvelope)
        {
            List<CorePointData> lstCorePt = null;

            if (strCoreEnvelope != null && strCoreEnvelope != string.Empty)
            {
                lstCorePt = new List<CorePointData>();

                string[] strArray = strCoreEnvelope.Split('|');

                string strNodeName = string.Empty;
                foreach (string strCore in strArray)
                {
                    if (strCore != string.Empty)
                    {
                        int index = strCore.IndexOf(":");
                        strNodeName = strCore.Substring(0, index);

                        string[] strCoreArray = strCore.Split('、');
                        CorePointData data = new CorePointData();
                        data.pointName = strNodeName;
                        data.pointXValue = Convert.ToDouble(strCoreArray[2]);
                        data.pointYValue = Convert.ToDouble(strCoreArray[3]);
                        lstCorePt.Add(data);
                    }
                }
            }
            return lstCorePt;
        }

    }
}
