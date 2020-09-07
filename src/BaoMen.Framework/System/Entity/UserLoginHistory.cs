/*
Author: WangXinBin
CreateTime: 2019/11/21 15:56:39
*/

using BaoMen.Common.Data.Attribute;
using BaoMen.Common.Model;
using System;
using System.Collections.Generic;

namespace BaoMen.Framework.System.Entity
{
    #region class UserLoginHistory (generated)
    /// <summary>
    /// 系统用户登录历史实体
    /// </summary>
	[Serializable]
    [DatabaseEntity(TableName = "sys_user_login_history")]
    public partial class UserLoginHistory
    {	
		/// <summary>
        /// ID
        /// </summary>
		/// <remarks>
		/// ColumnName: Id (Primary Key)
		/// ColumnType: INT(11)
		/// AllowDBNull: False
		/// Unique: True
        /// Size: 0
		/// </remarks>
		public int Id { get; set; }
        
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
        /// 登录时间
        /// </summary>
		/// <remarks>
		/// ColumnName: LoginTime 
		/// ColumnType: DATETIME
		/// AllowDBNull: False
		/// Unique: False
        /// Size: 0
		/// </remarks>
		public DateTime LoginTime { get; set; }
        
		/// <summary>
        /// 类型
        /// </summary>
		/// <remarks>
		/// ColumnName: Type 
		/// ColumnType: INT(11)
		/// AllowDBNull: False
		/// Unique: False
        /// Size: 0
		/// </remarks>
		public int Type { get; set; }
        
		/// <summary>
        /// 结果
        /// </summary>
		/// <remarks>
		/// ColumnName: Result 
		/// ColumnType: INT(11)
		/// AllowDBNull: False
		/// Unique: False
        /// Size: 0
		/// </remarks>
		public int Result { get; set; }
        
		/// <summary>
        /// 客户端
        /// </summary>
		/// <remarks>
		/// ColumnName: UserAgent 
		/// ColumnType: VARCHAR(1000)
		/// AllowDBNull: True
		/// Unique: False
        /// Size: 1000
		/// </remarks>
		public string UserAgent { get; set; }
        
		/// <summary>
        /// 客户端IP
        /// </summary>
		/// <remarks>
		/// ColumnName: ClientIp 
		/// ColumnType: VARCHAR(50)
		/// AllowDBNull: True
		/// Unique: False
        /// Size: 50
		/// </remarks>
		public string ClientIp { get; set; }
        
		/// <summary>
        /// 服务端IP
        /// </summary>
		/// <remarks>
		/// ColumnName: ServerIp 
		/// ColumnType: VARCHAR(50)
		/// AllowDBNull: True
		/// Unique: False
        /// Size: 50
		/// </remarks>
		public string ServerIp { get; set; }
        
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
    
	#region class UserLoginHistoryFilter (generated)
    /// <summary>
    /// 系统用户登录历史实体过滤器
    /// </summary>
	[Serializable]
    [DatabaseEntityFilter(EntityType = typeof(UserLoginHistory))]
	public partial class UserLoginHistoryFilter
	{		
		/// <summary>
        /// ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Id")]
        public FilterProperty<int> Id { get; set; }
        
		/// <summary>
        /// 用户名
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "UserName")]
        public FilterProperty<string> UserName { get; set; }
        
		/// <summary>
        /// 登录时间
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "LoginTime")]
		public FilterProperty<DateTime> LoginTime { get; set; }

		/// <summary>
        /// 登录时间最小值
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "LoginTime")]
		public FilterProperty<DateTime> LoginTimeMin { get; set; }
        
		/// <summary>
        /// 登录时间最大值
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "LoginTime")]
        public FilterProperty<DateTime> LoginTimeMax { get; set; }
		
		/// <summary>
        /// 类型
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Type")]
        public FilterProperty<int> Type { get; set; }
        
		/// <summary>
        /// 结果
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Result")]
        public FilterProperty<int> Result { get; set; }
        
		/// <summary>
        /// 客户端
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "UserAgent")]
        public FilterProperty<string> UserAgent { get; set; }
        
		/// <summary>
        /// 客户端IP
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "ClientIp")]
        public FilterProperty<string> ClientIp { get; set; }
        
		/// <summary>
        /// 服务端IP
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "ServerIp")]
        public FilterProperty<string> ServerIp { get; set; }
        
		/// <summary>
        /// 描述
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Description")]
        public FilterProperty<string> Description { get; set; }
        
	}
	#endregion
}