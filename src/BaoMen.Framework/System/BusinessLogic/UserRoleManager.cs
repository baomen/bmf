/*
Author: WangXinBin
CreateTime: 2019/9/23 14:38:41
*/

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using BaoMen.Framework.System.Entity;
using BaoMen.Common.Data;

namespace BaoMen.Framework.System.BusinessLogic
{
    #region class UserRoleManager (generated)
    /// <summary>
    /// 用户角色业务逻辑
    /// </summary>
    public partial class UserRoleManager : CacheableBusinessLogicBase<Tuple<string, string>, UserRole, UserRoleFilter, DataAccess.UserRole>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置实例</param>
        public UserRoleManager(IConfiguration configuration) : base(configuration)
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
        /// 数据访问
        /// </summary>
        internal DataAccess.UserRole Dal => dal;
    }
    #endregion
}