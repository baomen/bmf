/*
Author: WangXinBin
CreateTime: 2020/1/13 10:24:54
*/

using BaoMen.Common.Data.Attribute;
using BaoMen.Common.Model;
using System;
using System.Collections.Generic;

namespace BaoMen.Framework.System.Entity
{
    #region class DownloadFile (generated)
    /// <summary>
    /// 系统下载文件实体
    /// </summary>
	[Serializable]
    [DatabaseEntity(TableName = "sys_download_file")]
    public partial class DownloadFile
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
        /// 文件类型
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
        /// 原始文件名
        /// </summary>
		/// <remarks>
		/// ColumnName: OriginalFileName 
		/// ColumnType: VARCHAR(200)
		/// AllowDBNull: False
		/// Unique: False
        /// Size: 200
		/// </remarks>
		public string OriginalFileName { get; set; }
        
		/// <summary>
        /// 文件名
        /// </summary>
		/// <remarks>
		/// ColumnName: FileName 
		/// ColumnType: CHAR(32)
		/// AllowDBNull: False
		/// Unique: False
        /// Size: 32
		/// </remarks>
		public string FileName { get; set; }
        
		/// <summary>
        /// 扩展名
        /// </summary>
		/// <remarks>
		/// ColumnName: ExtentionName 
		/// ColumnType: VARCHAR(20)
		/// AllowDBNull: False
		/// Unique: False
        /// Size: 20
		/// </remarks>
		public string ExtentionName { get; set; }
        
		/// <summary>
        /// 相对路径
        /// </summary>
		/// <remarks>
		/// ColumnName: RelativePath 
		/// ColumnType: VARCHAR(200)
		/// AllowDBNull: False
		/// Unique: False
        /// Size: 200
		/// </remarks>
		public string RelativePath { get; set; }
        
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
        /// 关联ID
        /// </summary>
		/// <remarks>
		/// ColumnName: RelatedId 
		/// ColumnType: VARCHAR(100)
		/// AllowDBNull: True
		/// Unique: False
        /// Size: 100
		/// </remarks>
		public string RelatedId { get; set; }
        
    }
	#endregion
    
	#region class DownloadFileFilter (generated)
    /// <summary>
    /// 系统下载文件实体过滤器
    /// </summary>
	[Serializable]
    [DatabaseEntityFilter(EntityType = typeof(DownloadFile))]
	public partial class DownloadFileFilter
	{		
		/// <summary>
        /// ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Id")]
        public FilterProperty<int> Id { get; set; }
        
		/// <summary>
        /// 文件类型
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Type")]
        public FilterProperty<int> Type { get; set; }
        
		/// <summary>
        /// 原始文件名
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "OriginalFileName")]
        public FilterProperty<string> OriginalFileName { get; set; }
        
		/// <summary>
        /// 文件名
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "FileName")]
        public FilterProperty<string> FileName { get; set; }
        
		/// <summary>
        /// 扩展名
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "ExtentionName")]
        public FilterProperty<string> ExtentionName { get; set; }
        
		/// <summary>
        /// 相对路径
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "RelativePath")]
        public FilterProperty<string> RelativePath { get; set; }
        
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
        /// 关联ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "RelatedId")]
        public FilterProperty<string> RelatedId { get; set; }
        
	}
	#endregion
}