/*
Author: WangXinBin
CreateTime: 2019/9/23 14:40:35
*/

using Microsoft.Extensions.Configuration;
using BaoMen.Framework.System.Entity;
using BaoMen.Common.Data;
using BaoMen.Common.Constant;
using System.Data;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace BaoMen.Framework.System.BusinessLogic
{
    #region class OperateHistoryManager (generated)
    /// <summary>
    /// 系统操作日志业务逻辑
    /// </summary>
    public partial class OperateHistoryManager : BusinessLogicBase<int, OperateHistory, OperateHistoryFilter, DataAccess.OperateHistory>, IOperateHistoryManager
    {
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置实例</param>
        /// <param name="serviceProvider">服务提供程序</param>
        public OperateHistoryManager(IConfiguration configuration, IServiceProvider serviceProvider) : base(configuration)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 插入操作历史
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="relatedId">相关ID</param>
        /// <param name="value">实体实例</param>
        /// <param name="dataOperationType">操作类型</param>
        /// <param name="user">操作人,为空时代表当前操作人</param>
        /// <param name="description">描述信息（可空）</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns></returns>
        public int Insert<T>(string relatedId, T value, DataOperationType dataOperationType, Util.IUser user = null, string description = null, IDbTransaction transaction = null)
        {
            return ProcessInsert(
                () =>
                {
                    OperateHistory item = new OperateHistory();
                    if (user == null)
                    {
                        Util.ICurrentUserService currentUserService = serviceProvider.GetRequiredService<Util.ICurrentUserService>();
                        user = currentUserService.GetCurrentUser();
                        if (user == null || string.IsNullOrEmpty(user.Id))
                        {
                            throw new ArgumentNullException("userId");
                        }
                    }
                    if (value == null)
                        throw new ArgumentNullException("value");
                    Type type = typeof(T);
                    item.RelatedId = relatedId;
                    item.AssemblyName = type.Assembly.FullName;
                    item.EntityType = type.FullName;
                    item.OperateTime = DateTime.Now;
                    item.Type = (int)dataOperationType;
                    item.Description = description;
                    item.UserId = user.Id;
                    item.Value = Newtonsoft.Json.JsonConvert.SerializeObject(value);
                    return dal.Insert(item, transaction);
                },
                (log) =>
                {
                    log.Properties[nameof(value)] = value;
                    log.Properties[nameof(user)] = user;
                    log.Properties[nameof(relatedId)] = relatedId;
                    log.Properties[nameof(dataOperationType)] = dataOperationType;
                    log.Properties[nameof(description)] = description;
                });
        }
    }
    #endregion
}