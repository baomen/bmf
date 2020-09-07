/*
Author: WangXinBin
CreateTime: 2019/9/23 14:34:58
*/

using BaoMen.Framework.System.Entity;
using BaoMen.Common.Constant;
using BaoMen.Common.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace BaoMen.Framework.System.BusinessLogic
{

    #region class RoleManager (generated)
    /// <summary>
    /// 系统角色业务逻辑
    /// </summary>
    public partial class RoleManager : CacheableBusinessLogicBase<string, Role, RoleFilter, DataAccess.Role>, IRoleManager
    {
        private readonly IServiceProvider serviceProvider;
        private readonly RoleModuleManager roleModuleManager;
        private readonly UserRoleManager userRoleManager;
        private readonly IOperateHistoryManager operateHistoryManager;
        private readonly IModuleManager moduleManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置实例</param>
        /// <param name="serviceProvider">数据提供程序</param>
        public RoleManager(IConfiguration configuration, IServiceProvider serviceProvider) : base(configuration)
        {
            this.serviceProvider = serviceProvider;
            roleModuleManager = serviceProvider.GetRequiredService<RoleModuleManager>();
            userRoleManager = serviceProvider.GetRequiredService<UserRoleManager>();
            operateHistoryManager = serviceProvider.GetRequiredService<IOperateHistoryManager>();
            moduleManager = serviceProvider.GetRequiredService<IModuleManager>();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="item">系统角色实体</param>
        /// <returns></returns>
        protected override int DoInsert(Role item)
        {
            int affectRows = ProcessWithTransaction((transaction) =>
            {
                int rows = dal.Insert(item, transaction);
                rows += InsertExtention(item, transaction);
                return rows;
            });
            if (affectRows > 0)
            {
                operateHistoryManager.Insert(item.Id, item, DataOperationType.Insert);
            }
            return affectRows;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="item">系统角色实体</param>
        /// <returns></returns>
        protected override int DoDelete(Role item)
        {
            int affectRows = ProcessWithTransaction((transaction) =>
            {
                int rows = DeleteExtention(item, transaction);
                rows += userRoleManager.Dal.DeleteByRoleId(item.Id, transaction);
                int deleteRows = dal.Delete(item, transaction);
                if (deleteRows == 0)
                    throw new NoneRowModifiedException("原始数据已修改");
                rows += deleteRows;
                return rows;
            });
            if (affectRows > 0)
            {
                operateHistoryManager.Insert(item.Id, item, DataOperationType.Delete);
            }
            return affectRows;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="item">系统角色实体</param>
        /// <returns></returns>
        protected override int DoUpdate(Role item)
        {
            int affectRows = ProcessWithTransaction((transaction) =>
            {
                int rows = dal.Update(item, transaction);
                if (rows == 0)
                    throw new NoneRowModifiedException("原始数据已修改");
                rows += DeleteExtention(item, transaction);
                rows += InsertExtention(item, transaction);
                return rows;
            });

            if (affectRows > 0)
            {
                operateHistoryManager.Insert(item.Id, item, DataOperationType.Update);
            }
            return affectRows;
        }

        /// <summary>
        /// 添加扩展属性
        /// </summary>
        /// <param name="item">系统角色实体</param>
        protected override void AppendExtention(Role item)
        {
            base.AppendExtention(item);
            item.Modules = roleModuleManager.GetList(new RoleModuleFilter { RoleId = item.Id }).Select(p => moduleManager.Get(p.ModuleId)).Where(p => p != null).ToList();
        }

        private int InsertExtention(Role item, IDbTransaction transaction)
        {
            int rows = 0;
            if (item.Modules?.Count > 0)
            {
                var roleModules = from p in item.Modules select new Entity.RoleModule() { ModuleId = p.Id, RoleId = item.Id };
                rows += roleModuleManager.Dal.Insert(roleModules.ToList(), transaction);
            }
            return rows;
        }

        private int DeleteExtention(Role item, IDbTransaction transaction)
        {
            int rows = roleModuleManager.Dal.DeleteByRoleId(item.Id, transaction);
            return rows;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        public override void RemoveCache()
        {
            roleModuleManager.RemoveCache();
            base.RemoveCache();
            IUserManager userManager = serviceProvider.GetRequiredService<IUserManager>();
            userManager.RemoveCache();
        }

    }
    #endregion
}