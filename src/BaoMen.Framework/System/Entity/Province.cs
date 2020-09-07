/*
Author: WangXinBin
CreateTime: 2019/11/1 12:38:37
*/

using BaoMen.Common.Data.Attribute;
using BaoMen.Common.Model;
using System;
using System.Collections.Generic;

namespace BaoMen.Framework.System.Entity
{
    #region class Province (generated)
    /// <summary>
    /// 省份信息实体
    /// </summary>
	[Serializable]
    [DatabaseEntity(TableName = "sys_province")]
    public partial class Province
    {	
		/// <summary>
        /// ID
        /// </summary>
		/// <remarks>
		/// ColumnName: Id (Primary Key)
		/// ColumnType: CHAR(6)
		/// AllowDBNull: False
		/// Unique: True
        /// Size: 6
		/// </remarks>
		public string Id { get; set; }
        
		/// <summary>
        /// 名称
        /// </summary>
		/// <remarks>
		/// ColumnName: Name 
		/// ColumnType: VARCHAR(50)
		/// AllowDBNull: False
		/// Unique: False
        /// Size: 50
		/// </remarks>
		public string Name { get; set; }
        
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
        
    }
	#endregion
    
	#region class ProvinceFilter (generated)
    /// <summary>
    /// 省份信息实体过滤器
    /// </summary>
	[Serializable]
    [DatabaseEntityFilter(EntityType = typeof(Province))]
	public partial class ProvinceFilter
	{		
		/// <summary>
        /// ID
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Id")]
        public FilterProperty<string> Id { get; set; }
        
		/// <summary>
        /// 名称
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Name")]
        public FilterProperty<string> Name { get; set; }
        
		/// <summary>
        /// 状态
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Status")]
        public FilterProperty<int> Status { get; set; }
        
	}
	#endregion
}