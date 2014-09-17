using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL
{
    /// <summary>
    /// 重量设计表数据接口
    /// </summary>
    public interface IWeightDesignData
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <returns></returns>
        int GetMaxId();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Add(Model.WeightDesignData model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Model.WeightDesignData model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int Id);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.WeightDesignData GetModel(int Id);

        /// <summary>
        /// 获取所有对象实体
        /// </summary>
        List<Model.WeightDesignData> GetListModel();

        /// <summary>
        /// 获得记录条数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        int GetRecordCount(string strWhere);
    }
}
