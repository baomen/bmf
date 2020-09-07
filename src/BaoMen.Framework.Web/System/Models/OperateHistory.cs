/*
Author: WangXinBin
CreateTime: 2019/9/27 9:45:00
*/

using System;
using System.ComponentModel.DataAnnotations;

namespace BaoMen.Framework.Web.System.Models
{
    #region class OperateHistory (generated)
    /// <summary>
    /// 系统操作日志模型
    /// </summary>
    public partial class OperateHistory
    {	
		/// <summary>
        /// 流水号
        /// </summary>
		public string Id { get; set; }
        
		/// <summary>
        /// 系统用户ID
        /// </summary>
		public string UserId { get; set; }
        
		/// <summary>
        /// 操作类型
        /// </summary>
		public int Type { get; set; }
        
		/// <summary>
        /// 操作时间
        /// </summary>
		public DateTime OperateTime { get; set; }
        
		/// <summary>
        /// 程序集
        /// </summary>
		public string AssemblyName { get; set; }
        
		/// <summary>
        /// 实体类型
        /// </summary>
		public string EntityType { get; set; }

        /// <summary>
        /// 关联ID
        /// </summary>
        public string RelatedId { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        
		/// <summary>
        /// 描述
        /// </summary>
		public string Description { get; set; }
        
    }
    #endregion

    public partial class OperateHistory
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

    }
}