/*
Author: WangXinBin
CreateTime: 2019/11/1 12:38:36
*/

using System;
using System.ComponentModel.DataAnnotations;

namespace BaoMen.Framework.Web.System.Models
{
    #region class City (generated)
    /// <summary>
    /// 地市信息模型
    /// </summary>
    public partial class City
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
        /// 区号
        /// </summary>
		public string AreaCode { get; set; }
        
		/// <summary>
        /// 状态
        /// </summary>
		public int Status { get; set; }
        
    }
	#endregion
    
    #region class CreateCity (generated)
    /// <summary>
    /// 地市信息模型
    /// </summary>
    public partial class CreateCity
    {	
		/// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(50)]
		public string Name { get; set; }
        
		/// <summary>
        /// 区号
        /// </summary>
        
        [StringLength(20)]
		public string AreaCode { get; set; }
        
		/// <summary>
        /// 状态
        /// </summary>
        [Range(0, 1)]
		public int Status { get; set; }
        
    }
	#endregion
    
    #region class UpdateCity (generated)
    /// <summary>
    /// 地市信息模型
    /// </summary>
    public partial class UpdateCity
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
        /// 区号
        /// </summary>
        
        [StringLength(20)]
		public string AreaCode { get; set; }
        
		/// <summary>
        /// 状态
        /// </summary>
        [Range(0, 1)]
		public int Status { get; set; }
        
    }
	#endregion
    
    #region class DeleteCity (generated)
    /// <summary>
    /// 地市信息模型
    /// </summary>
    public partial class DeleteCity
    {	
		/// <summary>
        /// ID
        /// </summary>
        [Required]
        [StringLength(6)]
		public string Id { get; set; }
        
    }
    #endregion

    public partial class CreateCity
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string Id { get; set; }
    }
}