using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL
{
    /// <summary>
    /// 型号重量数据接口
    /// </summary>
    public interface ITypeWeightData
    {
        /// <summary>
        /// 获取最大Id
        /// </summary>
        /// <returns></returns>
        int GetMaxId();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Add(Model.TypeWeightData model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Model.TypeWeightData model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int Id);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.TypeWeightData GetModel(int Id);

        /// <summary>
        /// 获得记录条数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        int GetRecordCount(string strWhere);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns></returns>
        List<Model.TypeWeightData> GetListModel();
    }
}
