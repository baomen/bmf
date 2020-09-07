/*
Author: WangXinBin
CreateTime: 2019/11/1 12:38:39
*/

using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using BaoMen.Framework.System.Entity;
using BaoMen.Common.Data;
using BaoMen.Common.Constant;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace BaoMen.Framework.System.BusinessLogic
{
    #region class ProvinceManager (generated)
    /// <summary>
    /// 省份信息业务逻辑
    /// </summary>
    public partial class ProvinceManager : CacheableBusinessLogicBase<string, Province, ProvinceFilter, DataAccess.Province>, IProvinceManager
    {
        private readonly IOperateHistoryManager operateHistoryManager;
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置实例</param>
        /// <param name="serviceProvider">服务提供程序</param>
        public ProvinceManager(IConfiguration configuration, IServiceProvider serviceProvider) : base(configuration)
        {
            operateHistoryManager = serviceProvider.GetRequiredService<IOperateHistoryManager>();
            this.serviceProvider = serviceProvider;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="item">系统角色实体</param>
        /// <returns></returns>
        protected override int DoInsert(Entity.Province item)
        {
            int affectRows = base.DoInsert(item);
            if (affectRows > 0)
            {
                operateHistoryManager.Insert(item.Id, item, DataOperationType.Insert);
            }
            return affectRows;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="item">系统角色实体</param>
        /// <returns></returns>
        protected override int DoUpdate(Entity.Province item)
        {
            int affectRows = base.DoUpdate(item);
            if (affectRows > 0)
            {
                operateHistoryManager.Insert(item.Id, item, DataOperationType.Update);
            }
            return affectRows;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="item">系统角色实体</param>
        /// <returns></returns>
        protected override int DoDelete(Entity.Province item)
        {
            ICityManager cityManager = serviceProvider.GetRequiredService<ICityManager>();
            int cityCount = cityManager.GetListCount(new CityFilter { Id = new Common.Model.FilterProperty<string> { Value = item.Id.Substring(0, 2), CompareOperator = DbCompareOperator.StartWith } });
            if (cityCount > 0) throw new ArgumentException("省份含有地市数据，无法删除");
            int affectRows = base.DoDelete(item);
            if (affectRows > 0)
            {
                operateHistoryManager.Insert(item.Id, item, DataOperationType.Delete);
            }
            return affectRows;
        }

        /// <summary>
        /// 根据ID取得名称
        /// </summary>
        /// <param name="key">ID</param>
        /// <returns></returns>
        public string GetName(string key)
        {
            return Get(key)?.Name;
        }

        /// <summary>
        /// 根据名称查找ID
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public string GetKey(string name)
        {
            if (string.IsNullOrEmpty(name)) return name;
            return GetList().Where(p => p.Name.StartsWith(name)).FirstOrDefault()?.Id;
        }
    }
    #endregion
}