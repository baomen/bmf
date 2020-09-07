/*
Author: WangXinBin
CreateTime: 2019/9/25 9:00:20
*/

using BaoMen.Common.Data.Attribute;
using BaoMen.Common.Model;
using System;
using System.Collections.Generic;

namespace BaoMen.Framework.System.Entity
{
    #region class User (generated)
    /// <summary>
    /// 系统用户实体
    /// </summary>
	[Serializable]
    [DatabaseEntity(TableName = "sys_user")]
    public partial class User
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <remarks>
        /// ColumnName: Id (Primary Key)
        /// ColumnType: CHAR(32)
        /// AllowDBNull: False
        /// Unique: True
        /// Size: 32
        /// </remarks>
        public string Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        /// <remarks>
        /// ColumnName: UserName 
        /// ColumnType: VARCHAR(100)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 100
        /// </remarks>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        /// <remarks>
        /// ColumnName: Password 
        /// ColumnType: CHAR(32)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 32
        /// </remarks>
        public string Password { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        /// <remarks>
        /// ColumnName: Name 
        /// ColumnType: VARCHAR(100)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 100
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        /// <remarks>
        /// ColumnName: Mobile 
        /// ColumnType: VARCHAR(50)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 50
        /// </remarks>
        public string Mobile { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        /// <remarks>
        /// ColumnName: Email 
        /// ColumnType: VARCHAR(100)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 100
        /// </remarks>
        public string Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        /// <remarks>
        /// ColumnName: Avatar 
        /// ColumnType: VARCHAR(200)
        /// AllowDBNull: True
        /// Unique: False
        /// Size: 200
        /// </remarks>
        public string Avatar { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <remarks>
        /// ColumnName: CreateTime 
        /// ColumnType: DATETIME
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 0
        /// </remarks>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        /// <remarks>
        /// ColumnName: Status 
        /// ColumnType: INT(11)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 0
        /// </remarks>
        public int Status { get; set; }

        /// <summary>
        /// 微信OpenId
        /// </summary>
        /// <remarks>
        /// ColumnName: WechatOpenId 
        /// ColumnType: VARCHAR(50)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 50
        /// </remarks>
        public string WechatOpenId { get; set; }

        /// <summary>
        /// 微信小程序OpenId
        /// </summary>
        /// <remarks>
        /// ColumnName: WechatMpOpenId 
        /// ColumnType: VARCHAR(50)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 50
        /// </remarks>
        public string WechatMpOpenId { get; set; }

        /// <summary>
        /// 微信UnionId
        /// </summary>
        /// <remarks>
        /// ColumnName: WechatUnionId 
        /// ColumnType: VARCHAR(50)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 50
        /// </remarks>
        public string WechatUnionId { get; set; }

        /// <summary>
        /// 钉钉ID
        /// </summary>
        /// <remarks>
        /// ColumnName: DingTalkId 
        /// ColumnType: VARCHAR(50)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 50
        /// </remarks>
        public string DingTalkId { get; set; }

        /// <summary>
        /// 支付宝ID
        /// </summary>
        /// <remarks>
        /// ColumnName: AlipayId 
        /// ColumnType: VARCHAR(50)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 50
        /// </remarks>
        public string AlipayId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        /// <remarks>
        /// ColumnName: Description 
        /// ColumnType: VARCHAR(500)
        /// AllowDBNull: True
        /// Unique: False
        /// Size: 500
        /// </remarks>
        public string Description { get; set; }

    }
    #endregion

    #region class UserFilter (generated)
    /// <summary>
    /// 系统用户实体过滤器
    /// </summary>
    [Serializable]
    [DatabaseEntityFilter(EntityType = typeof(User))]
    public partial class UserFilter
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Id")]
        public FilterProperty<string> Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "UserName")]
        public FilterProperty<string> UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Password")]
        public FilterProperty<string> Password { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Name")]
        public FilterProperty<string> Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Mobile")]
        public FilterProperty<string> Mobile { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Email")]
        public FilterProperty<string> Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Avatar")]
        public FilterProperty<string> Avatar { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "CreateTime")]
        public FilterProperty<DateTime> CreateTime { get; set; }

        /// <summary>
        /// 创建时间最小值
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "CreateTime")]
        public FilterProperty<DateTime> CreateTimeMin { get; set; }

        /// <summary>
        /// 创建时间最大值
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "CreateTime")]
        public FilterProperty<DateTime> CreateTimeMax { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Status")]
        public FilterProperty<int> Status { get; set; }

        /// <summary>
        /// 微信OpenId
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "WechatOpenId")]
        public FilterProperty<string> WechatOpenId { get; set; }

        /// <summary>
        /// 微信小程序OpenId
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "WechatMpOpenId")]
        public FilterProperty<string> WechatMpOpenId { get; set; }

        /// <summary>
        /// 微信UnionId
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "WechatUnionId")]
        public FilterProperty<string> WechatUnionId { get; set; }

        /// <summary>
        /// 钉钉ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "DingTalkId")]
        public FilterProperty<string> DingTalkId { get; set; }

        /// <summary>
        /// 支付宝ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "AlipayId")]
        public FilterProperty<string> AlipayId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Description")]
        public FilterProperty<string> Description { get; set; }

    }
    #endregion


    public partial class User : Util.IUser
    {
        /// <summary>
        /// 所属的角色
        /// </summary>
        public ICollection<Role> Roles { get; set; }
    }
}