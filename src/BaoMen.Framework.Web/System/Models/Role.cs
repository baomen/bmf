/*
Author: WangXinBin
CreateTime: 2019/9/26 14:55:49
*/

using AutoMapper;
using AutoMapper.Configuration.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaoMen.Framework.Web.System.Models
{
    #region class Role (generated)
    /// <summary>
    /// 系统角色模型
    /// </summary>
    //[AutoMap(typeof(Entity.Role))]
    public partial class Role
    {	
		/// <summary>
        /// 标识
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

    #region class CreateRole (generated)
    /// <summary>
    /// 系统角色模型
    /// </summary>
    //[AutoMap(typeof(Entity.Role))]
    public partial class CreateRole
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

    #region class UpdateRole (generated)
    /// <summary>
    /// 系统角色模型
    /// </summary>
    public partial class UpdateRole
    {
        /// <summary>
        /// 标识
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

    #region class DeleteRole (generated)
    /// <summary>
    /// 系统角色模型
    /// </summary>
    public partial class DeleteRole
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32)]
        public string Id { get; set; }

    }
    #endregion

    public partial class Role
    {
        /// <summary>
        /// 拥有的模块
        /// </summary>
        public IList<string> ModuleIds { get; set; }
    }

    public partial class CreateRole
    {
        /// <summary>
        /// 系统模块ID列表
        /// </summary>
        //[SourceMember(nameof(Entity.Role.Modules))]
        //[ValueConverter(typeof(IdToModuleValueConverter))]
        public IList<string> ModuleIds { get; set; }
    }

    public partial class UpdateRole
    {
        /// <summary>
        /// 系统模块ID列表
        /// </summary>
        public IList<string> ModuleIds { get; set; }
    }
}