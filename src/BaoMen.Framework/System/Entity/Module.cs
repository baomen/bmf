/*
Author: WangXinBin
CreateTime: 2019/10/10 11:42:54
*/

using BaoMen.Common.Data.Attribute;
using BaoMen.Common.Model;
using System;
using System.Collections.Generic;

namespace BaoMen.Framework.System.Entity
{
    #region class Module (generated)
    /// <summary>
    /// 系统模块实体
    /// </summary>
	[Serializable]
    [DatabaseEntity(TableName = "sys_module")]
    public partial class Module
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <remarks>
        /// ColumnName: Id (Primary Key)
        /// ColumnType: CHAR(32)
        /// AllowDBNull: False
        /// Unique: True
        /// Size: 32
        /// </remarks>
        public string Id { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        /// <remarks>
        /// ColumnName: ParentId (Foreign Key)
        /// ColumnType: CHAR(32)
        /// AllowDBNull: True
        /// Unique: False
        /// Size: 32
        /// </remarks>
        public string ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        /// <remarks>
        /// ColumnName: Name 
        /// ColumnType: VARCHAR(100)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 100
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        /// <remarks>
        /// ColumnName: VisibleIndex 
        /// ColumnType: INT(11)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 0
        /// </remarks>
        public int VisibleIndex { get; set; }

        /// <summary>
        /// 类型
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
        /// 方法
        /// </summary>
        /// <remarks>
        /// ColumnName: Method 
        /// ColumnType: VARCHAR(200)
        /// AllowDBNull: True
        /// Unique: False
        /// Size: 200
        /// </remarks>
        public string Method { get; set; }

        /// <summary>
        /// 工作流活动ID
        /// </summary>
        /// <remarks>
        /// ColumnName: WorkflowActivityId 
        /// ColumnType: INT(11)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 0
        /// </remarks>
        public int WorkflowActivityId { get; set; }

        /// <summary>
        /// 是否节点
        /// </summary>
        /// <remarks>
        /// ColumnName: IsNode 
        /// ColumnType: INT(11)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 0
        /// </remarks>
        public int IsNode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        /// <remarks>
        /// ColumnName: Status 
        /// ColumnType: INT(11)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 0
        /// </remarks>
        public int Status { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        /// <remarks>
        /// ColumnName: Description 
        /// ColumnType: VARCHAR(500)
        /// AllowDBNull: True
        /// Unique: False
        /// Size: 500
        /// </remarks>
        public string Description { get; set; }

    }
    #endregion

    #region class ModuleFilter (generated)
    /// <summary>
    /// 系统模块实体过滤器
    /// </summary>
    [Serializable]
    [DatabaseEntityFilter(EntityType = typeof(Module))]
    public partial class ModuleFilter
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Id")]
        public FilterProperty<string> Id { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "ParentId")]
        public FilterProperty<string> ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Name")]
        public FilterProperty<string> Name { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "VisibleIndex")]
        public FilterProperty<int> VisibleIndex { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Type")]
        public FilterProperty<int> Type { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Method")]
        public FilterProperty<string> Method { get; set; }

        /// <summary>
        /// 工作流活动ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "WorkflowActivityId")]
        public FilterProperty<int> WorkflowActivityId { get; set; }

        /// <summary>
        /// 是否节点
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "IsNode")]
        public FilterProperty<int> IsNode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Status")]
        public FilterProperty<int> Status { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Description")]
        public FilterProperty<string> Description { get; set; }

    }
    #endregion

    public partial class Module : IHierarchicalData<string>
    {

    }

    public partial class ModuleFilter
    {

        /// <summary>
        /// 工作流活动ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "WorkflowActivityId")]
        public FilterProperty<ICollection<int>> WorkflowActivityIds { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Type")]
        public FilterProperty<ICollection<int>> Types { get; set; }
    }
}