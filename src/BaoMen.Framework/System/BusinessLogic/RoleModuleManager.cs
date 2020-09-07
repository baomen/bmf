/*
Author: WangXinBin
CreateTime: 2019/9/23 14:38:40
*/

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using BaoMen.Framework.System.Entity;
using BaoMen.Common.Data;

namespace BaoMen.Framework.System.BusinessLogic
{
    #region class RoleModuleManager (generated)
    /// <summary>
    /// 角色模块业务逻辑
    /// </summary>
    public partial class RoleModuleManager : CacheableBusinessLogicBase<Tuple<string, string>, RoleModule, RoleModuleFilter, DataAccess.RoleModule>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置实例</param>
        public RoleModuleManager(IConfiguration configuration) : base(configuration)
        {

        }

        /// <summary>
        /// 获取实体标识字段名称
        /// </summary>
        /// <remarks>
        /// <para>默认标识字段不为Id。覆盖此属性</para>
        /// </remarks>
        protected override string IdentityPropertyName
        {
            get { return "ComplexKey"; }
        }

        /// <summary>
        /// 角色模块数据访问实例
        /// </summary>
        internal DataAccess.RoleModule Dal => dal;
    }
    #endregion
}