using BaoMen.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaoMen.Framework.Web.System.Models
{
    /// <summary>
    /// 缓存模型
    /// </summary>
    public class Cache
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public string MerchantId { get; set; }
    }

    /// <summary>
    /// 缓存详情
    /// </summary>
    public class CacheDetail:Cache
    {
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
    }

    /// <summary>
    /// 缓存过滤器
    /// </summary>
    public class CacheFilter
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public FilterProperty<string> MerchantId { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        public FilterProperty<string> Key { get; set; }
    }
}
