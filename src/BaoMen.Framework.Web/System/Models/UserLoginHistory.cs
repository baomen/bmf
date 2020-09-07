/*
Author: WangXinBin
CreateTime: 2019/11/21 15:56:41
*/

using System;
using System.ComponentModel.DataAnnotations;

namespace BaoMen.Framework.Web.System.Models
{
    #region class UserLoginHistory (generated)
    /// <summary>
    /// 系统用户登录历史模型
    /// </summary>
    public partial class UserLoginHistory
    {	
		/// <summary>
        /// ID
        /// </summary>
		public int Id { get; set; }
        
		/// <summary>
        /// 用户名
        /// </summary>
		public string UserName { get; set; }
        
		/// <summary>
        /// 登录时间
        /// </summary>
		public DateTime LoginTime { get; set; }
        
		/// <summary>
        /// 类型
        /// </summary>
		public int Type { get; set; }
        
		/// <summary>
        /// 结果
        /// </summary>
		public int Result { get; set; }
        
		/// <summary>
        /// 客户端
        /// </summary>
		public string UserAgent { get; set; }
        
		/// <summary>
        /// 客户端IP
        /// </summary>
		public string ClientIp { get; set; }
        
		/// <summary>
        /// 服务端IP
        /// </summary>
		public string ServerIp { get; set; }
        
		/// <summary>
        /// 描述
        /// </summary>
		public string Description { get; set; }
        
    }
	#endregion
    
    #region class CreateUserLoginHistory (generated)
    /// <summary>
    /// 系统用户登录历史模型
    /// </summary>
    public partial class CreateUserLoginHistory
    {	
		/// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(100)]
		public string UserName { get; set; }
        
		/// <summary>
        /// 登录时间
        /// </summary>
		public DateTime LoginTime { get; set; }
        
		/// <summary>
        /// 类型
        /// </summary>
        
		public int Type { get; set; }
        
		/// <summary>
        /// 结果
        /// </summary>
        
		public int Result { get; set; }
        
		/// <summary>
        /// 客户端
        /// </summary>
        
        [StringLength(1000)]
		public string UserAgent { get; set; }
        
		/// <summary>
        /// 客户端IP
        /// </summary>
        
        [StringLength(50)]
		public string ClientIp { get; set; }
        
		/// <summary>
        /// 服务端IP
        /// </summary>
        
        [StringLength(50)]
		public string ServerIp { get; set; }
        
		/// <summary>
        /// 描述
        /// </summary>
        
        [StringLength(500)]
		public string Description { get; set; }
        
    }
	#endregion
    
    #region class UpdateUserLoginHistory (generated)
    /// <summary>
    /// 系统用户登录历史模型
    /// </summary>
    public partial class UpdateUserLoginHistory
    {	
		/// <summary>
        /// ID
        /// </summary>
        
		public int Id { get; set; }
        
		/// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(100)]
		public string UserName { get; set; }
        
		/// <summary>
        /// 登录时间
        /// </summary>
		public DateTime LoginTime { get; set; }
        
		/// <summary>
        /// 类型
        /// </summary>
        
		public int Type { get; set; }
        
		/// <summary>
        /// 结果
        /// </summary>
        
		public int Result { get; set; }
        
		/// <summary>
        /// 客户端
        /// </summary>
        
        [StringLength(1000)]
		public string UserAgent { get; set; }
        
		/// <summary>
        /// 客户端IP
        /// </summary>
        
        [StringLength(50)]
		public string ClientIp { get; set; }
        
		/// <summary>
        /// 服务端IP
        /// </summary>
        
        [StringLength(50)]
		public string ServerIp { get; set; }
        
		/// <summary>
        /// 描述
        /// </summary>
        
        [StringLength(500)]
		public string Description { get; set; }
        
    }
	#endregion
    
    #region class DeleteUserLoginHistory (generated)
    /// <summary>
    /// 系统用户登录历史模型
    /// </summary>
    public partial class DeleteUserLoginHistory
    {	
		/// <summary>
        /// ID
        /// </summary>
        
		public int Id { get; set; }
        
    }
	#endregion
    
    public partial class UserLoginHistory
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 结果名称
        /// </summary>
        public string ResultName { get; set; }
    }
}