using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 重心包线设计数据实体类
    /// </summary>
    public class CoreEnvelopeDesign
    {
        public CoreEnvelopeDesign()
        { }
        #region Model
        private int _id;
        private string _designdata_name;
        private string _designdata_submitter;
        private string _helicopter_name;
        private string _dataremark;
        private string _lastmodify_time;
        private double _designtaking_weight;
        private string _coreenvelope;

        public CoreEnvelopeDesign Clone()
        {
            CoreEnvelopeDesign core = new CoreEnvelopeDesign();

            core.Id = this.Id;
            core.DesignData_Name = this.DesignData_Name;
            core.DesignData_Submitter = this.DesignData_Submitter;
            core.Helicopter_Name = this.Helicopter_Name;
            core.DataRemark = this.DataRemark;
            core.LastModify_Time = this.LastModify_Time;
            core.DesignTaking_Weight = this.DesignTaking_Weight;
            core.CoreEnvelope = this.CoreEnvelope;

            return core;
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 设计数据名称
        /// </summary>
        public string DesignData_Name
        {
            set { _designdata_name = value; }
            get { return _designdata_name; }
        }
        /// <summary>
        /// 设计数据提交者
        /// </summary>
        public string DesignData_Submitter
        {
            set { _designdata_submitter = value; }
            get { return _designdata_submitter; }
        }
        /// <summary>
        ///  直升机名称
        /// </summary>
        public string Helicopter_Name
        {
            set { _helicopter_name = value; }
            get { return _helicopter_name; }
        }
        /// <summary>
        /// 数据备注
        /// </summary>
        public string DataRemark
        {
            set { _dataremark = value; }
            get { return _dataremark; }
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public string LastModify_Time
        {
            set { _lastmodify_time = value; }
            get { return _lastmodify_time; }
        }
        /// <summary>
        /// 直升机设计起飞重量
        /// </summary>
        public double DesignTaking_Weight
        {
            set { _designtaking_weight = value; }
            get { return _designtaking_weight; }
        }
        /// <summary>
        /// 直升机重心包线
        /// </summary>
        public string CoreEnvelope
        {
            set { _coreenvelope = value; }
            get { return _coreenvelope; }
        }

        #endregion Model

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
