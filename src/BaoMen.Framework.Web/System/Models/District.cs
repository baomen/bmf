/*
Author: WangXinBin
CreateTime: 2019/11/1 12:38:37
*/

using System;
using System.ComponentModel.DataAnnotations;

namespace BaoMen.Framework.Web.System.Models
{
    #region class District (generated)
    /// <summary>
    /// 地区信息模型
    /// </summary>
    public partial class District
    {	
		/// <summary>
        /// ID
        /// </summary>
		public string Id { get; set; }
        
		/// <summary>
        /// 名称
        /// </summary>
		public string Name { get; set; }
        
		/// <summary>
        /// 状态
        /// </summary>
		public int Status { get; set; }
        
    }
	#endregion
    
    #region class CreateDistrict (generated)
    /// <summary>
    /// 地区信息模型
    /// </summary>
    public partial class CreateDistrict
    {	
		/// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(50)]
		public string Name { get; set; }
        
		/// <summary>
        /// 状态
        /// </summary>
        [Range(0, 1)]
		public int Status { get; set; }
        
    }
	#endregion
    
    #region class UpdateDistrict (generated)
    /// <summary>
    /// 地区信息模型
    /// </summary>
    public partial class UpdateDistrict
    {	
		/// <summary>
        /// ID
        /// </summary>
        [Required]
        [StringLength(6)]
		public string Id { get; set; }
        
		/// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(50)]
		public string Name { get; set; }
        
		/// <summary>
        /// 状态
        /// </summary>
        [Range(0, 1)]
		public int Status { get; set; }
        
    }
	#endregion
    
    #region class DeleteDistrict (generated)
    /// <summary>
    /// 地区信息模型
    /// </summary>
    public partial class DeleteDistrict
    {	
		/// <summary>
        /// ID
        /// </summary>
        [Required]
        [StringLength(6)]
		public string Id { get; set; }
        
    }
    #endregion

    public partial class CreateDistrict
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string Id { get; set; }
    }
}