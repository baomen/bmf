using BaoMen.Common.Data.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BaoMen.Common.Data.Helper
{
    /// <summary>
    /// 数据库实体帮助类
    /// </summary>
    public class DatabaseEntityHelper
    {
        /// <summary>
        /// 实体类型
        /// </summary>
        private Type entityType;

        /// <summary>
        /// 实体特性
        /// </summary>
        private DatabaseEntityAttribute databaseEntityAttribute;

        /// <summary>
        /// 实体属性的特性字典
        /// </summary>
        private Dictionary<string, DatabaseEntityPropertyAttribute> propertyAttributeDict;

        /// <summary>
        /// 属性信息
        /// </summary>
        private Dictionary<string, PropertyInfo> propertyInfoDict;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entityType">实体类型。实体必须是标记了<see cref="DatabaseEntityAttribute"/>性质的类型</param>
        /// <exception cref="ArgumentNullException">实体类型为空时产生</exception>
        /// <exception cref="ArgumentException">实体类型未标记为<see cref="DatabaseEntityAttribute"/>性质时产生</exception>
        public DatabaseEntityHelper(Type entityType)
        {
            if (entityType == null) throw new ArgumentNullException("entityType");
            object[] attributes = null;
            attributes = entityType.GetCustomAttributes(typeof(DatabaseEntityAttribute), false);
            if (attributes == null || attributes.Length < 1)
                throw new ArgumentException("entityType does not have DatabaseEntityAttribute", "entityType");
            this.databaseEntityAttribute = (DatabaseEntityAttribute)attributes[0];
            this.entityType = entityType;
        }

        /// <summary>
        /// 获取实体类型
        /// </summary>
        public Type EntityType { get { return this.entityType; } }

        /// <summary>
        /// 获取实体特性
        /// </summary>
        public DatabaseEntityAttribute DatabaseEntityAttribute { get { return databaseEntityAttribute; } }

        /// <summary>
        /// 获取实体属性的<see cref="DatabaseEntityPropertyAttribute"/>特性字典
        /// </summary>
        public Dictionary<string, DatabaseEntityPropertyAttribute> PropertyAttributeDict
        {
            get
            {
                if (propertyAttributeDict == null)
                {
                    propertyAttributeDict = new Dictionary<string, DatabaseEntityPropertyAttribute>();
                    foreach (PropertyInfo property in PropertyInfoDict.Values)
                    {
                        object[] attributes = property.GetCustomAttributes(typeof(DatabaseEntityPropertyAttribute), false);
                        if (attributes != null && attributes.Length > 0)
                        {
                            DatabaseEntityPropertyAttribute databaseEntityPropertyAttribute = (DatabaseEntityPropertyAttribute)attributes[0];
                            propertyAttributeDict.Add(property.Name, databaseEntityPropertyAttribute);
                        }
                    }
                }
                return propertyAttributeDict;
            }
        }

        /// <summary>
        /// 获取实体的属性信息
        /// </summary>
        public Dictionary<string, PropertyInfo> PropertyInfoDict
        {
            get
            {
                if (propertyInfoDict == null)
                {
                    PropertyInfo[] properties = entityType.GetProperties();
                    propertyInfoDict = properties.Where(p => p.CanRead && p.CanWrite).ToDictionary(p => p.Name);
                }
                return propertyInfoDict;
            }
        }
        /// <summary>
        /// 取得实体类标识（第一个主键）属性名称
        /// </summary>
        /// <returns>如果没有取到返回null</returns>
        public string GetIdentityPropertyName()
        {
            var q = PropertyAttributeDict.FirstOrDefault(p => p.Value.IsPrimaryKey == true);
            return q.Key;
        }

        /// <summary>
        /// 取得实体类行版本（第一个）属性名称
        /// </summary>
        /// <returns>如果没有取到返回null</returns>
        public string GetRowVersionPropertyName()
        {
            var q = PropertyAttributeDict.FirstOrDefault(p => p.Value.IsRowVersion == true);
            return q.Key;
        }

        /// <summary>
        /// 取得数据库字段名称
        /// </summary>
        /// <param name="includeIdentityColumn">是否包含标识字段</param>
        /// <param name="includeTableName">是否包含表名</param>
        /// <returns></returns>
        public string[] GetColumnNames(bool includeIdentityColumn, bool includeTableName)
        {
            IEnumerable<KeyValuePair<string, DatabaseEntityPropertyAttribute>> temp = PropertyAttributeDict;
            if (!includeIdentityColumn)
                temp = PropertyAttributeDict.Where(p => p.Value.IsPrimaryKey == false);
            if (includeTableName)
                return temp.Select(p => DatabaseEntityAttribute.TableName + "." + p.Value.ColumnName).ToArray();
            else
                return temp.Select(p => p.Value.ColumnName).ToArray();
        }

        /// <summary>
        /// 取得指定属性的类型
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public TypeCode GetPropertyTypeCode(string propertyName)
        {
            PropertyInfo property = entityType.GetProperty(propertyName);
            TypeCode code = Type.GetTypeCode(property.PropertyType);
            return code;
        }

        /// <summary>
        /// 取得实体程序集的简单名称
        /// </summary>
        /// <returns></returns>
        public string GetAssemblyName()
        {
            return entityType.Assembly.GetName().Name;
        }
    }
}
