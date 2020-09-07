/*
Author: WangXinBin
CreateTime: 2019/9/23 14:27:06
*/

using BaoMen.Common.Data.Attribute;
using BaoMen.Common.Model;
using System;
using System.Collections.Generic;

namespace BaoMen.Framework.System.Entity
{
    #region class Parameter (generated)
    /// <summary>
    /// 系统参数实体
    /// </summary>
	[Serializable]
    [DatabaseEntity(TableName = "sys_parameter")]
    public partial class Parameter : IHierarchicalData<string>
    {
        /// <summary>
        /// 标识
        /// </summary>
        /// <remarks>
        /// ColumnName: Id (Primary Key)
        /// ColumnType: VARCHAR(100)
        /// AllowDBNull: False
        /// Unique: True
        /// Size: 100
        /// </remarks>
        public string Id { get; set; }

        /// <summary>
        /// 父标识
        /// </summary>
        /// <remarks>
        /// ColumnName: ParentId 
        /// ColumnType: VARCHAR(100)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 100
        /// </remarks>
        public string ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        /// <remarks>
        /// ColumnName: Name 
        /// ColumnType: VARCHAR(255)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 255
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        /// <remarks>
        /// ColumnName: Value 
        /// ColumnType: VARCHAR(2048)
        /// AllowDBNull: True
        /// Unique: False
        /// Size: 2048
        /// </remarks>
        public string Value { get; set; }

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

    #region class ParameterFilter (generated)
    /// <summary>
    /// 系统参数实体过滤器
    /// </summary>
    [Serializable]
    [DatabaseEntityFilter(EntityType = typeof(Parameter))]
    public partial class ParameterFilter
    {
        /// <summary>
        /// 标识
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Id")]
        public FilterProperty<string> Id { get; set; }

        /// <summary>
        /// 父标识
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "ParentId")]
        public FilterProperty<string> ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Name")]
        public FilterProperty<string> Name { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Value")]
        public FilterProperty<string> Value { get; set; }

        /// <summary>
        /// 是否节点
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "IsNode")]
        public FilterProperty<int> IsNode { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Description")]
        public FilterProperty<string> Description { get; set; }

    }
    #endregion
}