using Microsoft.Extensions.Configuration;

namespace BaoMen.Common.Cache
{
    /// <summary>
    /// 缓存工厂
    /// </summary>
    public static class CacheFactory
    {
        private static ICache defaultCache;
        private static readonly object initLock = new object();

        /// <summary>
        /// 获得缓存实例
        /// </summary>
        /// <returns></returns>
        public static ICache Get(IConfiguration configuration)
        {
            if (defaultCache == null)
            {
                lock (initLock)
                {
                    if (defaultCache == null)
                    {
                        string cacheSetingValue = configuration.GetSection("Cache")["Type"];
                        if (cacheSetingValue == "RabbitMQSynchronizedCache")
                        {
                            defaultCache = new RabbitMQSynchronizedCache(configuration);
                        }
                        else
                        {
                            defaultCache = new MemoryCache();
                        }
                    }
                }
            }
            return defaultCache;
        }
    }
}
