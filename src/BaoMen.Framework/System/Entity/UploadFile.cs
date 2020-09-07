/*
Author: WangXinBin
CreateTime: 2020/1/10 14:14:21
*/

using BaoMen.Common.Data.Attribute;
using BaoMen.Common.Model;
using System;
using System.Collections.Generic;

namespace BaoMen.Framework.System.Entity
{
    #region class UploadFile (generated)
    /// <summary>
    /// 系统上传文件实体
    /// </summary>
	[Serializable]
    [DatabaseEntity(TableName = "sys_upload_file")]
    public partial class UploadFile
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
        /// 创建人
        /// </summary>
		/// <remarks>
		/// ColumnName: CreateUserId 
		/// ColumnType: CHAR(32)
		/// AllowDBNull: False
		/// Unique: False
        /// Size: 32
		/// </remarks>
		public string CreateUserId { get; set; }
        
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
    
	#region class UploadFileFilter (generated)
    /// <summary>
    /// 系统上传文件实体过滤器
    /// </summary>
	[Serializable]
    [DatabaseEntityFilter(EntityType = typeof(UploadFile))]
	public partial class UploadFileFilter
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
        /// 创建人
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "CreateUserId")]
        public FilterProperty<string> CreateUserId { get; set; }
        
		/// <summary>
        /// 关联ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "RelatedId")]
        public FilterProperty<string> RelatedId { get; set; }
        
	}
	#endregion
}