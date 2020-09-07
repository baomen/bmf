/*
Author: WangXinBin
CreateTime: 2019/9/27 9:45:05
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaoMen.Framework.Web.System.Models
{
    #region class User (generated)
    /// <summary>
    /// 系统用户模型
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        ///// <summary>
        ///// 密码
        ///// </summary>
        //public string Password { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 微信OpenId
        /// </summary>
        public string WechatOpenId { get; set; }

        /// <summary>
        /// 微信小程序OpenId
        /// </summary>
        public string WechatMpOpenId { get; set; }

        /// <summary>
        /// 微信UnionId
        /// </summary>
        public string WechatUnionId { get; set; }

        /// <summary>
        /// 钉钉ID
        /// </summary>
        public string DingTalkId { get; set; }

        /// <summary>
        /// 支付宝ID
        /// </summary>
        public string AlipayId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

    }
    #endregion


    #region class CreateUser (generated)
    /// <summary>
    /// 系统用户模型
    /// </summary>
    public partial class CreateUser
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Mobile { get; set; }

        /// <summary>
        /// 头像
        /// </summary>

        [StringLength(200)]
        public string Avatar { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Range(0, 1)]
        public int Status { get; set; }

        /// <summary>
        /// 描述
        /// </summary>

        [StringLength(500)]
        public string Description { get; set; }

    }
    #endregion

    #region class UpdateUser (generated)
    /// <summary>
    /// 系统用户模型
    /// </summary>
    public partial class UpdateUser
    {
        /// <summary>
        /// 标识
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32)]
        public string Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Mobile { get; set; }

        /// <summary>
        /// 头像
        /// </summary>

        [StringLength(200)]
        public string Avatar { get; set; }


        /// <summary>
        /// 状态
        /// </summary>
        [Range(0, 1)]
        public int Status { get; set; }

        /// <summary>
        /// 描述
        /// </summary>

        [StringLength(500)]
        public string Description { get; set; }

    }
    #endregion

    #region class DeleteUser (generated)
    /// <summary>
    /// 系统用户模型
    /// </summary>
    public partial class DeleteUser
    {
        /// <summary>
        /// 标识
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32)]
        public string Id { get; set; }

    }
    #endregion

    public partial class User
    {
        /// <summary>
        /// 角色
        /// </summary>
        public IList<Models.Role> Roles { get; set; }
    }

    public partial class CreateUser
    {
        /// <summary>
        /// 电子邮件
        /// </summary>
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        /// <summary>
        /// 角色ID列表
        /// </summary>
        public IList<string> RoleIds { get; set; }
    }

    public partial class UpdateUser
    {
        /// <summary>
        /// 电子邮件
        /// </summary>
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// 角色ID列表
        /// </summary>
        public IList<string> RoleIds { get; set; }
    }

   /// <summary>
   /// 个人设置修改
   /// </summary>
    public partial class UpdateUserPersonalSetting
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32)]
        public string Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>

        [StringLength(500)]
        public string Description { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        [StringLength(100)]
        public string Email { get; set; }
    }
}