using BaoMen.Common.Data.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BaoMen.Common.Data.Helper
{
    /// <summary>
    /// 数据库实例过滤器帮助类
    /// </summary>
    public class DatabaseEntityFilterHelper
    {
        /// <summary>
        /// 过滤器类型
        /// </summary>
        private Type filterType;

        /// <summary>
        /// 实体帮助类
        /// </summary>
        private DatabaseEntityHelper entityHelper;

        /// <summary>
        /// 过滤器特性
        /// </summary>
        private DatabaseEntityFilterAttribute databaseEntityFilterAttribute;

        /// <summary>
        /// 过滤器属性的特性字典
        /// </summary>
        private Dictionary<string, DatabaseEntityFilterPropertyAttribute> propertyAttributeDict;

        /// <summary>
        /// 过滤器属性信息字典
        /// </summary>
        private Dictionary<string, PropertyInfo> propertyInfoDict;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filterType">过滤器类型</param>
        /// <exception cref="ArgumentNullException">实体类型为空时产生</exception>
        /// <exception cref="ArgumentException">实体类型未标记为<see cref="DatabaseEntityAttribute"/>性质时产生</exception>
        public DatabaseEntityFilterHelper(Type filterType)
        {
            if (filterType == null)
            {
                throw new ArgumentNullException("filterType");
            }
            object[] customAttributes = null;
            customAttributes = filterType.GetCustomAttributes(typeof(DatabaseEntityFilterAttribute), false);
            if ((customAttributes == null) || (customAttributes.Length < 1))
            {
                throw new ArgumentException("filterType does not have DatabaseEntityFilterAttribute", "filterType");
            }
            this.databaseEntityFilterAttribute = (DatabaseEntityFilterAttribute)customAttributes[0];
            if (this.databaseEntityFilterAttribute.EntityType == null)
            {
                throw new ArgumentException("filter type does not have EntityType");
            }
            this.entityHelper = new DatabaseEntityHelper(this.databaseEntityFilterAttribute.EntityType);
            this.filterType = filterType;

        }

        /// <summary>
        /// 获取过滤器类型
        /// </summary>
        public Type FilterType { get { return this.filterType; } }

        /// <summary>
        /// 获取过滤器特性
        /// </summary>
        public DatabaseEntityFilterAttribute DatabaseEntityFilterAttribute { get { return this.databaseEntityFilterAttribute; } }

        /// <summary>
        /// 获取属性字典
        /// </summary>
        public Dictionary<string, DatabaseEntityFilterPropertyAttribute> PropertyAttributeDict
        {
            get
            {
                if (propertyAttributeDict == null)
                {
                    propertyAttributeDict = new Dictionary<string, DatabaseEntityFilterPropertyAttribute>();
                    foreach (PropertyInfo property in PropertyInfoDict.Values)
                    {
                        object[] attributes = property.GetCustomAttributes(typeof(DatabaseEntityFilterPropertyAttribute), false);
                        if (attributes != null && attributes.Length > 0)
                        {
                            DatabaseEntityFilterPropertyAttribute eop = (DatabaseEntityFilterPropertyAttribute)attributes[0];
                            propertyAttributeDict.Add(property.Name, eop);
                        }
                    }
                }
                return propertyAttributeDict;
            }
        }

        /// <summary>
        /// 获取过滤器属性信息字典
        /// </summary>
        public Dictionary<string, PropertyInfo> PropertyInfoDict
        {
            get
            {
                if (propertyInfoDict == null)
                {
                    PropertyInfo[] properties = filterType.GetProperties();
                    propertyInfoDict = properties.Where(p => p.CanRead && p.CanWrite).ToDictionary(p => p.Name);
                }
                return propertyInfoDict;
            }
        }

        /// <summary>
        /// 获取实体帮助类
        /// </summary>
        public DatabaseEntityHelper EntityHelper
        {
            get
            {
                return this.entityHelper;
            }
        }

        /// <summary>
        /// 取得指定属性的类型
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public TypeCode GetPropertyTypeCode(string propertyName)
        {
            return Type.GetTypeCode(PropertyInfoDict[propertyName].PropertyType);
        }
    }
}
