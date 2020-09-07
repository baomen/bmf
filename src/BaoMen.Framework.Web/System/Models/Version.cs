/*
Author: WangXinBin
CreateTime: 2019/10/23 12:03:54
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaoMen.Framework.Web.System.Models
{
    #region class Version (generated)
    /// <summary>
    /// 系统版本模型
    /// </summary>
    public partial class Version
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
        
		/// <summary>
        /// 描述
        /// </summary>
		public string Description { get; set; }
        
    }
    #endregion

    #region class CreateVersion (generated)
    /// <summary>
    /// 系统版本模型
    /// </summary>
    public partial class CreateVersion
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

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

    #region class UpdateVersion (generated)
    /// <summary>
    /// 系统版本模型
    /// </summary>
    public partial class UpdateVersion
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32)]
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

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

    #region class DeleteVersion (generated)
    /// <summary>
    /// 系统版本模型
    /// </summary>
    public partial class DeleteVersion
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32)]
        public string Id { get; set; }

    }
    #endregion

    public partial class Version
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        public IList<string> ModuleIds { get; set; }
    }

    public partial class CreateVersion
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        public IList<string> ModuleIds { get; set; }
    }

    public partial class UpdateVersion
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        public IList<string> ModuleIds { get; set; }
    }
}