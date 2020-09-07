/*
Author: WangXinBin
CreateTime: 2019/10/23 12:23:14
*/

using BaoMen.Common.Data.Attribute;
using BaoMen.Common.Model;
using System;
using System.Collections.Generic;

namespace BaoMen.Framework.System.Entity
{
    #region class OperateHistory (generated)
    /// <summary>
    /// 系统操作日志实体
    /// </summary>
	[Serializable]
    [DatabaseEntity(TableName = "sys_operate_history")]
    public partial class OperateHistory
    {
        /// <summary>
        /// 流水号
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
        /// 系统用户ID
        /// </summary>
        /// <remarks>
        /// ColumnName: UserId 
        /// ColumnType: CHAR(32)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 32
        /// </remarks>
        public string UserId { get; set; }

        /// <summary>
        /// 操作类型
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
        /// 操作时间
        /// </summary>
        /// <remarks>
        /// ColumnName: OperateTime 
        /// ColumnType: DATETIME
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 0
        /// </remarks>
        public DateTime OperateTime { get; set; }

        /// <summary>
        /// 程序集
        /// </summary>
        /// <remarks>
        /// ColumnName: AssemblyName 
        /// ColumnType: VARCHAR(255)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 255
        /// </remarks>
        public string AssemblyName { get; set; }

        /// <summary>
        /// 实体类型
        /// </summary>
        /// <remarks>
        /// ColumnName: EntityType 
        /// ColumnType: VARCHAR(255)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 255
        /// </remarks>
        public string EntityType { get; set; }

        /// <summary>
        /// 相关ID
        /// </summary>
        /// <remarks>
        /// ColumnName: RelatedId 
        /// ColumnType: VARCHAR(100)
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 100
        /// </remarks>
        public string RelatedId { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        /// <remarks>
        /// ColumnName: Value 
        /// ColumnType: MEDIUMTEXT
        /// AllowDBNull: False
        /// Unique: False
        /// Size: 16777215
        /// </remarks>
        public string Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        /// <remarks>
        /// ColumnName: Description 
        /// ColumnType: VARCHAR(512)
        /// AllowDBNull: True
        /// Unique: False
        /// Size: 512
        /// </remarks>
        public string Description { get; set; }

    }
    #endregion

    #region class OperateHistoryFilter (generated)
    /// <summary>
    /// 系统操作日志实体过滤器
    /// </summary>
    [Serializable]
    [DatabaseEntityFilter(EntityType = typeof(OperateHistory))]
    public partial class OperateHistoryFilter
    {
        /// <summary>
        /// 流水号
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Id")]
        public FilterProperty<int> Id { get; set; }

        /// <summary>
        /// 系统用户ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "UserId")]
        public FilterProperty<string> UserId { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Type")]
        public FilterProperty<int> Type { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "OperateTime")]
        public FilterProperty<DateTime> OperateTime { get; set; }

        /// <summary>
        /// 操作时间最小值
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "OperateTime")]
        public FilterProperty<DateTime> OperateTimeMin { get; set; }

        /// <summary>
        /// 操作时间最大值
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "OperateTime")]
        public FilterProperty<DateTime> OperateTimeMax { get; set; }

        /// <summary>
        /// 程序集
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "AssemblyName")]
        public FilterProperty<string> AssemblyName { get; set; }

        /// <summary>
        /// 实体类型
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "EntityType")]
        public FilterProperty<string> EntityType { get; set; }

        /// <summary>
        /// 相关ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "RelatedId")]
        public FilterProperty<string> RelatedId { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Value")]
        public FilterProperty<string> Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Description")]
        public FilterProperty<string> Description { get; set; }

    }
    #endregion
}