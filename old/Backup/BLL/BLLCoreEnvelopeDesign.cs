using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 重心包线设计数据业务逻辑类
    /// </summary>
    public class BLLCoreEnvelopeDesign
    {

        private readonly IDAL.ICoreEnvelopeDesign dal = DALFactory.DataAccess.CreateCoreEnvelopeDesign();

        /// <summary>
        /// 获取最大ID
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
        public bool Add(Model.CoreEnvelopeDesign model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.CoreEnvelopeDesign model)
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
        public Model.CoreEnvelopeDesign GetModel(int Id)
        {
            return dal.GetModel(Id);
        }

        /// <summary>
        /// 获取所有对象实体
        /// </summary>
        /// <returns></returns>
        public List<Model.CoreEnvelopeDesign> GetListModel()
        {
            return dal.GetListModel();
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
    }
}
