/*
Author: WangXinBin
CreateTime: 2019/9/23 14:38:42
*/

using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using BaoMen.Framework.System.Entity;
using BaoMen.Common.Data;

namespace BaoMen.Framework.System.BusinessLogic
{
    #region class UserTokenManager (generated)
    /// <summary>
    /// 系统用户令牌业务逻辑
    /// </summary>
    public partial class UserTokenManager : CacheableBusinessLogicBase<string, UserToken, UserTokenFilter, DataAccess.UserToken>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置实例</param>
        public UserTokenManager(IConfiguration configuration) : base(configuration)
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
            get { return "UserId"; }
        }

        /// <summary>
        /// 内部使用的数据访问实体
        /// </summary>
        internal DataAccess.UserToken Dal => dal;
    }
    #endregion
}