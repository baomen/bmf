/*
Author: WangXinBin
CreateTime: 2019/9/23 14:38:41
*/

using BaoMen.Common.Data.Attribute;
using BaoMen.Common.Model;
using System;
using System.Collections.Generic;

namespace BaoMen.Framework.System.Entity
{
    #region class UserToken (generated)
    /// <summary>
    /// 系统用户令牌实体
    /// </summary>
	[Serializable]
    [DatabaseEntity(TableName = "sys_user_token")]
    public partial class UserToken
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        /// <remarks>
        /// ColumnName: UserId (Primary Key)
        /// ColumnType: CHAR(32)
        /// AllowDBNull: False
        /// Unique: True
        /// Size: 32
        /// </remarks>
        public string UserId { get; set; }

        /// <summary>
        /// 令牌
        /// </summary>
        /// <remarks>
        /// ColumnName: Token 
        /// ColumnType: CHAR(32)
        /// AllowDBNull: False
        /// Unique: True
        /// Size: 32
        /// </remarks>
        public string Token { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        /// <remarks>
        /// ColumnName: Expires 
        /// ColumnType: DATETIME
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 0
        /// </remarks>
        public DateTime Expires { get; set; }

    }
    #endregion

    #region class UserTokenFilter (generated)
    /// <summary>
    /// 系统用户令牌实体过滤器
    /// </summary>
    [Serializable]
    [DatabaseEntityFilter(EntityType = typeof(UserToken))]
    public partial class UserTokenFilter
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "UserId")]
        public FilterProperty<string> UserId { get; set; }

        /// <summary>
        /// 令牌
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Token")]
        public FilterProperty<string> Token { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Expires")]
        public FilterProperty<DateTime> Expires { get; set; }

        /// <summary>
        /// 到期时间最小值
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Expires")]
        public FilterProperty<DateTime> ExpiresMin { get; set; }

        /// <summary>
        /// 到期时间最大值
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Expires")]
        public FilterProperty<DateTime> ExpiresMax { get; set; }

    }
    #endregion
}