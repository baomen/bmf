using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace BaoMen.Common.Cache
{
    /// <summary>
    /// 内存缓存类
    /// </summary>
    public class MemoryCache : ICache
    {
        private readonly static Microsoft.Extensions.Caching.Memory.MemoryCache memoryCache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());

        /// <summary>
        /// 获取缓存的数据
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public object Get(string key)
        {
            return memoryCache.Get(key);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">关键字</param>
        public void Remove(string key)
        {
            memoryCache.Remove(key);
        }

        /// <summary>
        /// 设置缓存的值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        public void Set(string key, object value)
        {
            memoryCache.Set(key, value);
        }
    }
}
