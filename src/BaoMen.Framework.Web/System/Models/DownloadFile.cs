/*
Author: WangXinBin
CreateTime: 2020/1/13 10:24:54
*/

using System;
using System.ComponentModel.DataAnnotations;

namespace BaoMen.Framework.Web.System.Models
{
    #region class DownloadFile (generated)
    /// <summary>
    /// 系统下载文件模型
    /// </summary>
    public partial class DownloadFile
    {	
		/// <summary>
        /// ID
        /// </summary>
		public int Id { get; set; }
        
		/// <summary>
        /// 文件类型
        /// </summary>
		public int Type { get; set; }
        /// <summary>
        /// 文件类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 原始文件名
        /// </summary>
        public string OriginalFileName { get; set; }
        
		/// <summary>
        /// 文件名
        /// </summary>
		public string FileName { get; set; }
        
		/// <summary>
        /// 扩展名
        /// </summary>
		public string ExtentionName { get; set; }
        
		/// <summary>
        /// 相对路径
        /// </summary>
		public string RelativePath { get; set; }
        
		/// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime { get; set; }
        
		/// <summary>
        /// 关联ID
        /// </summary>
		public string RelatedId { get; set; }
        
    }
	#endregion
    
    #region class CreateDownloadFile (generated)
    /// <summary>
    /// 系统下载文件模型
    /// </summary>
    public partial class CreateDownloadFile
    {	
		/// <summary>
        /// 文件类型
        /// </summary>
        
		public int Type { get; set; }
        
		/// <summary>
        /// 原始文件名
        /// </summary>
        [Required]
        [StringLength(200)]
		public string OriginalFileName { get; set; }
        
		/// <summary>
        /// 文件名
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32)]
		public string FileName { get; set; }
        
		/// <summary>
        /// 扩展名
        /// </summary>
        [Required]
        [StringLength(20)]
		public string ExtentionName { get; set; }
        
		/// <summary>
        /// 相对路径
        /// </summary>
        [Required]
        [StringLength(200)]
		public string RelativePath { get; set; }
        
		/// <summary>
        /// 关联ID
        /// </summary>
        
        [StringLength(100)]
		public string RelatedId { get; set; }
        
    }
	#endregion
    
    #region class UpdateDownloadFile (generated)
    /// <summary>
    /// 系统下载文件模型
    /// </summary>
    public partial class UpdateDownloadFile
    {	
		/// <summary>
        /// ID
        /// </summary>
        
		public int Id { get; set; }
        
		/// <summary>
        /// 文件类型
        /// </summary>
        
		public int Type { get; set; }
        
		/// <summary>
        /// 原始文件名
        /// </summary>
        [Required]
        [StringLength(200)]
		public string OriginalFileName { get; set; }
        
		/// <summary>
        /// 文件名
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32)]
		public string FileName { get; set; }
        
		/// <summary>
        /// 扩展名
        /// </summary>
        [Required]
        [StringLength(20)]
		public string ExtentionName { get; set; }
        
		/// <summary>
        /// 相对路径
        /// </summary>
        [Required]
        [StringLength(200)]
		public string RelativePath { get; set; }
        
		/// <summary>
        /// 关联ID
        /// </summary>
        
        [StringLength(100)]
		public string RelatedId { get; set; }
        
    }
	#endregion
    
    #region class DeleteDownloadFile (generated)
    /// <summary>
    /// 系统下载文件模型
    /// </summary>
    public partial class DeleteDownloadFile
    {	
		/// <summary>
        /// ID
        /// </summary>
        
		public int Id { get; set; }
        
    }
	#endregion
    
}