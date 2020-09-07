using BaoMen.Framework.Web.Util;
using BaoMen.Common.Cache;
using BaoMen.Common.Extension;
using BaoMen.Common.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BaoMen.Framework.Web.System.Controllers
{
    /// <summary>
    /// 缓存控制器
    /// </summary>
    public abstract class CacheController : BaseController
    {
        /// <summary>
        /// 缓存实例
        /// </summary>
        private readonly ICache cache;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置实例</param>
        public CacheController(IConfiguration configuration)
        {
            cache = CacheFactory.Get(configuration);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<object> Get(string key)
        {
            ResponseData<object> responseData = new ResponseData<object>();
            try
            {
                responseData.Data = cache.Get(key);
            }
            catch (Exception exception)
            {
                responseData.ErrorNumber = 1000;
                responseData.ErrorMessage = Properties.Resources.Error_1000;
                responseData.Exception = exception;
                logger.Error(exception);
            }
            return responseData;
        }

        /// <summary>
        /// 获取所有缓存
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseData Remove(string key)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                cache.Remove(key);
            }
            catch (Exception exception)
            {
                responseData.ErrorNumber = 1000;
                responseData.ErrorMessage = Properties.Resources.Error_1000;
                responseData.Exception = exception;
                logger.Error(exception);
            }
            return responseData;
        }

        /// <summary>
        /// 获取所有缓存
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<object> GetKeys()
        {
            ResponseData<object> responseData = new ResponseData<object>();
            try
            {
                responseData.Data = GetCacheKeys();
            }
            catch (Exception exception)
            {
                responseData.ErrorNumber = 1000;
                responseData.ErrorMessage = Properties.Resources.Error_1000;
                responseData.Exception = exception;
                logger.Error(exception);
            }
            return responseData;
        }

        /// <summary>
        /// 获取所有缓存
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<IDictionary<string, object>> GetAll()
        {
            ResponseData<IDictionary<string, object>> responseData = new ResponseData<IDictionary<string, object>>();
            try
            {
                IList<string> keys = GetCacheKeys();
                IDictionary<string, object> dict = new Dictionary<string, object>();
                foreach (string key in keys)
                {
                    dict.Add(key, cache.Get(key));
                }
                responseData.Data = dict;
            }
            catch (Exception exception)
            {
                responseData.ErrorNumber = 1000;
                responseData.ErrorMessage = Properties.Resources.Error_1000;
                responseData.Exception = exception;
                logger.Error(exception);
            }
            return responseData;
        }

        private List<string> GetCacheKeys()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            Type cacheType = cache.GetType();
            MemoryCache memoryCache;
            if (cacheType == typeof(RabbitMQSynchronizedCache))
            {
                memoryCache = (MemoryCache)cacheType.GetField("memoryCache", flags).GetValue(cache);
            }
            else if (cacheType == typeof(MemoryCache))
            {
                memoryCache = (MemoryCache)cache;
            }
            else
            {
                throw new ArgumentException("不支持的缓存类型");
            }
            var field = memoryCache.GetType().GetField("memoryCache", BindingFlags.NonPublic | BindingFlags.Static);
            Microsoft.Extensions.Caching.Memory.MemoryCache subCache = (Microsoft.Extensions.Caching.Memory.MemoryCache)field.GetValue(memoryCache);
            var entries = subCache.GetType().GetField("_entries", flags).GetValue(subCache);
            List<string> keys = new List<string>();
            if (entries is IDictionary cacheItems)
            {
                foreach (DictionaryEntry cacheItem in cacheItems)
                {
                    string key = cacheItem.Key.ToString();
                    keys.Add(key);
                }
            }
            return keys;
        }

        private Models.Cache GetCacheFromKey(string key)
        {
            if (string.IsNullOrEmpty(key)) return null;
            Models.Cache cache = new Models.Cache
            {
                Key = key
            };
            string[] temp = key.Split('.');
            if (temp.Length > 0)
            {
                string merchantId = temp[^1];
                if (merchantId.Length == 32)
                {
                    cache.MerchantId = merchantId;
                    cache.Key = string.Join('.', temp.Take(temp.Length - 1));
                }
            }
            return cache;
        }

        /// <summary>
        /// 获取多个实例的数量和ICollection列表
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序字串</param>
        /// <param name="pageIndex">开始的页数（从1开始）</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        [HttpGet]
        public virtual ResponseData<TotalAndItem<Models.Cache>> GetList([FromQuery]Models.CacheFilter filter, string sort, int pageIndex = 1, int pageSize = 10)
        {
            ResponseData<TotalAndItem<Models.Cache>> responseData = new ResponseData<TotalAndItem<Models.Cache>>();
            try
            {
                pageSize = CheckPageSize(pageSize);
                int startRowIndex = GetStartRowIndex(pageIndex, pageSize);
                IEnumerable<Models.Cache> caches = GetCacheKeys().Select(p => GetCacheFromKey(p)).DistinctBy(p => p.Key).OrderBy(p => p.Key);
                int total = caches.Count();
                if (filter != null)
                {
                    if (filter.Key != null)
                    {
                        caches = caches.Where(p => p.Key.Contains(filter.Key.Value));
                    }
                    if (filter.MerchantId != null)
                    {
                        caches = caches.Where(p => p.MerchantId == null || p.MerchantId == filter.MerchantId);
                    }
                }
                List<Models.Cache> cacheList = caches.Skip(startRowIndex).Take(pageSize).ToList();
                responseData.Data = new TotalAndItem<Models.Cache>
                {
                    Total = total,
                    Items = cacheList
                };
            }
            catch (Exception exception)
            {
                responseData.ErrorNumber = 1000;
                responseData.ErrorMessage = Properties.Resources.Error_1000;
                responseData.Exception = exception;
                logger.Error(exception);
            }
            return responseData;
        }
    }
}
