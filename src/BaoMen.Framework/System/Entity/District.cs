/*
Author: WangXinBin
CreateTime: 2019/11/1 12:38:36
*/

using BaoMen.Common.Data.Attribute;
using BaoMen.Common.Model;
using System;
using System.Collections.Generic;

namespace BaoMen.Framework.System.Entity
{
    #region class District (generated)
    /// <summary>
    /// 地区信息实体
    /// </summary>
	[Serializable]
    [DatabaseEntity(TableName = "sys_district")]
    public partial class District
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
    
	#region class DistrictFilter (generated)
    /// <summary>
    /// 地区信息实体过滤器
    /// </summary>
	[Serializable]
    [DatabaseEntityFilter(EntityType = typeof(District))]
	public partial class DistrictFilter
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