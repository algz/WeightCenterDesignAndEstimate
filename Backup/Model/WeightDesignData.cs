using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 重量设计数据实体类
    /// </summary>
    public class WeightDesignData
    {
        public WeightDesignData()
        { }
        #region Model
        private int _id;
        private string _designdata_name;
        private string _designdata_submitter;
        private string _helicopter_name;
        private string _dataremark;
        private string _lastmodify_time;
        private double _designtaking_weight;
        private string _mainsystem_name;

        public WeightDesignData Clone()
        {
            WeightDesignData data = new WeightDesignData();

            data.Id = this.Id;
            data.DesignData_Name = this.DesignData_Name;
            data.DesignData_Submitter = this.DesignData_Submitter;
            data.Helicopter_Name = this.Helicopter_Name;
            data.DataRemark = this.DataRemark;
            data.LastModify_Time = this.LastModify_Time;
            data.DesignTaking_Weight = this.DesignTaking_Weight;
            data.MainSystem_Name = this.MainSystem_Name;

            return data;
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
        /// 直升机名称
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
        /// 直升机各主要系统重量
        /// </summary>
        public string MainSystem_Name
        {
            set { _mainsystem_name = value; }
            get { return _mainsystem_name; }
        }

        #endregion Model
    }
}