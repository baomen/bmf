/*
Author: WangXinBin
CreateTime: 2019/11/1 12:38:34
*/

using BaoMen.Common.Data.Attribute;
using BaoMen.Common.Model;
using System;
using System.Collections.Generic;

namespace BaoMen.Framework.System.Entity
{
    #region class City (generated)
    /// <summary>
    /// 地市信息实体
    /// </summary>
	[Serializable]
    [DatabaseEntity(TableName = "sys_city")]
    public partial class City
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
        /// 区号
        /// </summary>
		/// <remarks>
		/// ColumnName: AreaCode 
		/// ColumnType: VARCHAR(20)
		/// AllowDBNull: True
		/// Unique: False
        /// Size: 20
		/// </remarks>
		public string AreaCode { get; set; }
        
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
    
	#region class CityFilter (generated)
    /// <summary>
    /// 地市信息实体过滤器
    /// </summary>
	[Serializable]
    [DatabaseEntityFilter(EntityType = typeof(City))]
	public partial class CityFilter
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
        /// 区号
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "AreaCode")]
        public FilterProperty<string> AreaCode { get; set; }
        
		/// <summary>
        /// 状态
        /// </summary>
        [DatabaseEntityFilterProperty(EntityPropertyName = "Status")]
        public FilterProperty<int> Status { get; set; }
        
	}
	#endregion
}