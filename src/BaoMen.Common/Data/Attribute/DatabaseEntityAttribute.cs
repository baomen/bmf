using System;
using System.Data;

namespace BaoMen.Common.Data.Attribute
{
    /// <summary>
    /// 实体类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DatabaseEntityAttribute : System.Attribute
    {
        /// <summary>
        /// 获取或设置数据表名称
        /// </summary>
        public string TableName { get; set; }

    }

    /// <summary>
    /// 实体类属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DatabaseEntityPropertyAttribute : System.Attribute
    {
        private bool isPrimaryKey;
        /// <summary>
        /// 获取或设置是否是主键。
        /// <para>一个实体只能有一个属性将此值设置为true</para>
        /// <para>默认值false</para>
        /// </summary>
        public bool IsPrimaryKey
        {
            get { return this.isPrimaryKey; }
            set { this.isPrimaryKey = value; }
        }

        private bool isRowVersion;
        /// <summary>
        /// 获取或设置是否是行版本字段
        /// <para>一个实体只能有一个属性将此值设置为true</para>
        /// <para>默认值false</para>
        /// </summary>
        public bool IsRowVersion
        {
            get { return this.isRowVersion; }
            set { this.isRowVersion = value; }
        }

        private bool isUnique;
        /// <summary>
        /// 获取或设置是否唯一
        /// <para>默认值false</para>
        /// </summary>
        public bool IsUnique
        {
            get { return isUnique; }
            set { isUnique = value; }
        }

        private string displayNameResource;
        /// <summary>
        /// 显示时的资源名称
        /// </summary>
        public string DisplayNameResource
        {
            get { return displayNameResource; }
            set { displayNameResource = value; }
        }

        private string editNameResource;
        /// <summary>
        /// 编辑时的资源名称
        /// </summary>
        public string EditNameResource
        {
            get { return editNameResource; }
            set { editNameResource = value; }
        }


        private string columnName;
        /// <summary>
        /// 获取或设置字段名
        /// </summary>
        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }

        private DbType columnDbType;
        /// <summary>
        /// 获取或设置字段数据类型
        /// </summary>
        public DbType ColumnDbType
        {
            get { return columnDbType; }
            set { columnDbType = value; }
        }

        private int columnLength;
        /// <summary>
        /// 获取或设置字段长度
        /// </summary>
        public int ColumnLength
        {
            get { return columnLength; }
            set { columnLength = value; }
        }

        private bool allowNull;
        /// <summary>
        /// 获取或设置字段是否可空
        /// <para>默认值false</para>
        /// </summary>
        public bool AllowNull
        {
            get { return allowNull; }
            set { allowNull = value; }
        }
    }
}
