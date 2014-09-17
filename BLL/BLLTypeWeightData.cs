using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 型号重量数据业务逻辑类
    /// </summary>
    public class BLLTypeWeightData
    {
        private readonly IDAL.ITypeWeightData dal = DALFactory.DataAccess.CreateTypeWeightData();

        /// <summary>
        /// 获取最大Id
        /// </summary>
        /// <returns></returns>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Model.TypeWeightData model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.TypeWeightData model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {
            return dal.Delete(Id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.TypeWeightData GetModel(int Id)
        {
            return dal.GetModel(Id);
        }

        /// <summary>
        /// 获得记录条数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns></returns>
        public List<Model.TypeWeightData> GetListModel()
        {
            return dal.GetListModel();
        }
    }
}
