/*
Author: WangXinBin
CreateTime: 2019/10/10 11:42:56
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaoMen.Framework.Web.System.Models
{
    #region class Module (generated)
    /// <summary>
    /// 系统模块模型
    /// </summary>
    public partial class Module
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int VisibleIndex { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 工作流活动ID
        /// </summary>
        public int WorkflowActivityId { get; set; }

        /// <summary>
        /// 是否节点
        /// </summary>
        public int IsNode { get; set; }

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

    #region class CreateModule (generated)
    /// <summary>
    /// 系统模块模型
    /// </summary>
    public partial class CreateModule
    {
        /// <summary>
        /// 父ID
        /// </summary>
        [StringLength(32, MinimumLength = 32)]
        public string ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required]
        public int VisibleIndex { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        [StringLength(200)]
        public string Method { get; set; }

        /// <summary>
        /// 工作流活动ID
        /// </summary>
        public int WorkflowActivityId { get; set; }

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

    #region class UpdateModule (generated)
    /// <summary>
    /// 系统模块模型
    /// </summary>
    public partial class UpdateModule
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32)]
        public string Id { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        [StringLength(32, MinimumLength = 32)]
        public string ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required]
        public int VisibleIndex { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        [StringLength(200)]
        public string Method { get; set; }

        /// <summary>
        /// 工作流活动ID
        /// </summary>
        public int WorkflowActivityId { get; set; }

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

    #region class DeleteModule (generated)
    /// <summary>
    /// 系统模块模型
    /// </summary>
    public partial class DeleteModule
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32)]
        public string Id { get; set; }

    }
    #endregion


    public partial class Module
    {
        /// <summary>
        /// 子模块
        /// </summary>
        public IList<Module> Children { get; set; }
    }

    public partial class CreateModule
    {
        /// <summary>
        /// ID
        /// </summary>
        [StringLength(32, MinimumLength = 32)]
        public string Id { get; set; }
    }
}