/*
Author: WangXinBin
CreateTime: 2019/11/1 12:38:39
*/

using System;
using System.ComponentModel.DataAnnotations;

namespace BaoMen.Framework.Web.System.Models
{
    #region class Province (generated)
    /// <summary>
    /// 省份信息模型
    /// </summary>
    public partial class Province
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

    #region class CreateProvince (generated)
    /// <summary>
    /// 省份信息模型
    /// </summary>
    public partial class CreateProvince
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

    #region class UpdateProvince (generated)
    /// <summary>
    /// 省份信息模型
    /// </summary>
    public partial class UpdateProvince
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

    #region class DeleteProvince (generated)
    /// <summary>
    /// 省份信息模型
    /// </summary>
    public partial class DeleteProvince
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [StringLength(6)]
        public string Id { get; set; }

    }
    #endregion

    public partial class CreateProvince
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string Id { get; set; }
    }
}