/*
Author: WangXinBin
CreateTime: 2019/9/23 14:38:40
*/

using BaoMen.Common.Data.Attribute;
using BaoMen.Common.Model;
using System;
using System.Collections.Generic;

namespace BaoMen.Framework.System.Entity
{
    #region class UserRole (generated)
    /// <summary>
    /// 用户角色实体
    /// </summary>
	[Serializable]
    [DatabaseEntity(TableName = "sys_user_role")]
    public partial class UserRole
    {
        private Tuple<string, string> complexKey;
        /// <summary>
        /// 复合主键Item1:UserId  Item2:RoleId  
        /// </summary>
        public Tuple<string, string> ComplexKey
        {
            get
            {
                if (complexKey == null)
                {
                    complexKey = Tuple.Create(UserId, RoleId);
                }
                return complexKey;
            }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        /// <remarks>
        /// ColumnName: UserId (Primary Key)
        /// ColumnType: CHAR(32)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 32
        /// </remarks>
        public string UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        /// <remarks>
        /// ColumnName: RoleId (Primary Key)
        /// ColumnType: CHAR(32)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 32
        /// </remarks>
        public string RoleId { get; set; }

    }
    #endregion

    #region class UserRoleFilter (generated)
    /// <summary>
    /// 用户角色实体过滤器
    /// </summary>
    [Serializable]
    [DatabaseEntityFilter(EntityType = typeof(UserRole))]
    public partial class UserRoleFilter
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "UserId")]
        public FilterProperty<string> UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "RoleId")]
        public FilterProperty<string> RoleId { get; set; }

    }
    #endregion
}