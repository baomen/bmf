using System;

namespace BaoMen.Common.Data.Attribute
{
    /// <summary>
    /// 数据过滤器性质
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DatabaseEntityFilterAttribute : System.Attribute
    {
        /// <summary>
        /// 获取或设置实体类型
        /// </summary>
        public Type EntityType { get; set; }
    }

    /// <summary>
    /// 数据库过滤器属性的性质
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DatabaseEntityFilterPropertyAttribute : System.Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DatabaseEntityFilterPropertyAttribute()
        {
        }

        /// <summary>
        /// 获取或设置实体属性名称
        /// </summary>
        public string EntityPropertyName { get; set; }

    }
}
