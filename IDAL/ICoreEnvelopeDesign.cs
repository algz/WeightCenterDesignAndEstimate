using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL
{
    /// <summary>
    /// 重心包线设计数据接口
    /// </summary>
    public interface ICoreEnvelopeDesign
    {

        /// <summary>
        /// 获取最大ID
        /// </summary>
        /// <returns></returns>
        int GetMaxId();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Add(Model.CoreEnvelopeDesign model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Model.CoreEnvelopeDesign model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int Id);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.CoreEnvelopeDesign GetModel(int Id);

        /// <summary>
        /// 获取所有对象实体
        /// </summary>
        /// <returns></returns>
        List<Model.CoreEnvelopeDesign> GetListModel();

        /// <summary>
        /// 获得记录条数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        int GetRecordCount(string strWhere);

    }
}
