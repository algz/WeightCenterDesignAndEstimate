using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    ///型号重量数据实体类
    /// </summary>
    public class TypeWeightData
    {
        public TypeWeightData()
        { }

        #region Model

        private int _id;
        private string _helicopter_name;
        private string _last_modifytime;
        private string _helicopter_type;
        private double _designtaking_weight;
        private double _maxtaking_weight;
        private double _emptyweight;
        private string _mainsystem_name;
        private string _helicoter_country;

        public TypeWeightData Clone()
        {
            TypeWeightData weightData = new TypeWeightData();

            weightData.Id = this.Id;
            weightData.Helicopter_Name = this.Helicopter_Name;
            weightData.Last_ModifyTime = this.Last_ModifyTime;
            weightData.Helicopter_Type = this.Helicopter_Type;
            weightData.DesignTaking_Weight = this.DesignTaking_Weight;
            weightData.MaxTaking_Weight = this.MaxTaking_Weight;
            weightData.EmptyWeight = this.EmptyWeight;
            weightData.MainSystem_Name = this.MainSystem_Name;
            weightData.Helicoter_Country = this.Helicoter_Country;

            return weightData;
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
        /// 直升机名称
        /// </summary>
        public string Helicopter_Name
        {
            set { _helicopter_name = value; }
            get { return _helicopter_name; }
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public string Last_ModifyTime
        {
            set { _last_modifytime = value; }
            get { return _last_modifytime; }
        }
        /// <summary>
        /// 直升机类型
        /// </summary>
        public string Helicopter_Type
        {
            set { _helicopter_type = value; }
            get { return _helicopter_type; }
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
        /// 直升机最大起飞重量
        /// </summary>
        public double MaxTaking_Weight
        {
            set { _maxtaking_weight = value; }
            get { return _maxtaking_weight; }
        }
        /// <summary>
        /// 直升机空重
        /// </summary>
        public double EmptyWeight
        {
            set { _emptyweight = value; }
            get { return _emptyweight; }
        }
        /// <summary>
        /// 直升机各主要系统重量
        /// </summary>
        public string MainSystem_Name
        {
            set { _mainsystem_name = value; }
            get { return _mainsystem_name; }
        }
        /// <summary>
        /// 直升机国籍
        /// </summary>
        public string Helicoter_Country
        {
            set { _helicoter_country = value; }
            get { return _helicoter_country; }
        }

        #endregion Model

    }
}
