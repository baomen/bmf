using BaoMen.Common.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using BaoMen.Common.Model;
using System.Linq;

namespace BaoMen.Framework.Util
{
    /// <summary>
    /// 可缓存的商户数据业务逻辑积累
    /// </summary>
    /// <typeparam name="TKey">键的类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TFilter">过滤器类型</typeparam>
    /// <typeparam name="TDataAccess">数据访问类型</typeparam>
    public abstract class MerchantBusinessLogicBase<TKey, TEntity, TFilter, TDataAccess> : BusinessLogicBase<TKey, TEntity, TFilter, TDataAccess>
    where TEntity : class, new()
    where TFilter : class, IMerchantFilter, new()
    where TDataAccess : IDataAccess<TKey, TEntity, TFilter>
    {
        /// <summary>
        /// 服务提供程序
        /// </summary>
        protected readonly IServiceProvider serviceProvider;

        /// <summary>
        /// http上下文访问
        /// </summary>
        protected readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置实例</param>
        /// <param name="serviceProvider">服务提供程序</param>
        //protected MerchantBusinessLogicBase(IConfiguration configuration, IServiceProvider serviceProvider)
        protected MerchantBusinessLogicBase(IConfiguration configuration, IServiceProvider serviceProvider)
            : base(configuration)
        {
            this.serviceProvider = serviceProvider;
            httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
        }

        /// <summary>
        /// 获取商户服务
        /// </summary>
        /// <returns></returns>
        protected IMerchantService GetMerchantService()
        {
            IMerchantService merchantService;
            if (httpContextAccessor != null)
                merchantService = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IMerchantService>();
            else
            {
                //using (IServiceScope serviceScope = serviceProvider.CreateScope())
                //{
                //    merchantService = serviceScope.ServiceProvider.GetRequiredService<IMerchantService>();
                //}
                merchantService = serviceProvider.GetRequiredService<IMerchantService>();
            }
            return merchantService;
        }

        /// <summary>
        /// 获取商户ID
        /// </summary>
        /// <returns></returns>
        protected virtual string GetMerchantId()
        {
            IMerchantService merchantService = GetMerchantService();
            // string merchantId = merchantService.GetMerchantId();
            string merchantId = merchantService.MerchantId;
            if (string.IsNullOrEmpty(merchantId) || merchantId.Length != 32)
            {
                throw new ArgumentException("invalid merchant id");
            }
            return merchantId;
        }

        /// <summary>
        /// 准备过滤器
        /// </summary>
        /// <param name="filter"></param>
        protected override void PrepareFilter(TFilter filter)
        {
            if (filter == null)
            {
                filter = new TFilter();
            }
            filter.MerchantId = GetMerchantId();
        }

        ///// <summary>
        ///// 根据实体标识获取实体实例。
        ///// <para>仅内部使用。</para>
        ///// </summary>
        ///// <param name="id">实体标识</param>
        ///// <returns>实体类的实例</returns>
        //protected override TEntity DoGet(TKey id)
        //{
        //    TEntity entity = base.DoGet(id);
        //    if (entity == null) return null;
        //    string merchantId = GetMerchantId();
        //    if (entity.MerchantId != merchantId) return null;
        //    return entity;
        //}
    }

    /// <summary>
    /// 可缓存的商户数据业务逻辑积累
    /// </summary>
    /// <typeparam name="TKey">键的类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TFilter">过滤器类型</typeparam>
    /// <typeparam name="TDataAccess">数据访问类型</typeparam>
    public abstract class MerchantCacheableBusinessLogicBase<TKey, TEntity, TFilter, TDataAccess> : CacheableBusinessLogicBase<TKey, TEntity, TFilter, TDataAccess>
    where TEntity : class, new()
    where TFilter : class, IMerchantFilter, new()
    where TDataAccess : IDataAccess<TKey, TEntity, TFilter>
    {
        /// <summary>
        /// 服务提供程序
        /// </summary>
        protected readonly IServiceProvider serviceProvider;

        /// <summary>
        /// http上下文访问
        /// </summary>
        protected readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置实例</param>
        /// <param name="serviceProvider">服务提供程序</param>
        protected MerchantCacheableBusinessLogicBase(IConfiguration configuration, IServiceProvider serviceProvider)
            : base(configuration)
        {
            this.serviceProvider = serviceProvider;
            httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
        }

        /// <summary>
        /// 获取缓存的键
        /// </summary>
        /// <returns></returns>
        protected override string GetCacheKey()
        {
            string cacheKey = this.cacheKey;
            string merchantId = GetMerchantId();
            cacheKey = $"{cacheKey}.{merchantId}";
            return cacheKey;
        }

        /// <summary>
        /// 获取商户ID
        /// </summary>
        /// <returns></returns>
        protected virtual string GetMerchantId()
        {
            IMerchantService merchantService;
            if (httpContextAccessor != null)
                merchantService = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IMerchantService>();
            else
            {
                merchantService = serviceProvider.GetRequiredService<IMerchantService>();
            }
            string merchantId = merchantService.MerchantId;
            if (string.IsNullOrEmpty(merchantId) || merchantId.Length != 32)
            {
                throw new ArgumentException("invalid merchant id");
            }
            return merchantId;
        }

        /// <summary>
        /// 取得缓存的数据
        /// </summary>
        /// <returns></returns>
        protected override IDictionary<TKey, TEntity> DoGetCacheData()
        {
            string cacheKey = GetCacheKey();
            IDictionary<TKey, TEntity> cacheData = (IDictionary<TKey, TEntity>)cache.Get(cacheKey);
            if (cacheData == null)
            {
                ICollection<TEntity> items = dal.GetList(new TFilter { MerchantId = GetMerchantId() });
                foreach (TEntity item in items)
                {
                    if (item != null)
                        AppendExtention(item);
                }
                cacheData = items.ToDictionary(CreateKeyExpression().Compile());
                cache.Set(cacheKey, cacheData);
            }
            return cacheData;
        }
    }

    /// <summary>
    /// 带缓存和过滤器的分层结构业务逻辑
    /// </summary>
    /// <typeparam name="TKey">键的类型</typeparam>
    /// <typeparam name="TEntity">实体类型。必须实现IHierarchicalData接口</typeparam>
    /// <typeparam name="TFilter">实体过滤器类型</typeparam>
    /// <typeparam name="TDataAccess">数据访问类型</typeparam>
    public abstract class MerchantHierarchicalCacheableBusinessLogicBase<TKey, TEntity, TFilter, TDataAccess> : HierarchicalCacheableBusinessLogicBase<TKey, TEntity, TFilter, TDataAccess>
        where TEntity : class, IHierarchicalData<TKey>, new()
        where TFilter : class, IMerchantFilter, new()
        where TDataAccess : IDataAccess<TKey, TEntity, TFilter>
    {
        /// <summary>
        /// http上下文访问
        /// </summary>
        protected readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// 服务提供程序
        /// </summary>
        protected readonly IServiceProvider serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置实例</param>
        /// <param name="serviceProvider">服务提供程序</param>
        protected MerchantHierarchicalCacheableBusinessLogicBase(IConfiguration configuration, IServiceProvider serviceProvider)
            : base(configuration)
        {
            this.serviceProvider = serviceProvider;
            httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
        }

        /// <summary>
        /// 获取缓存的键
        /// </summary>
        /// <returns></returns>
        protected override string GetCacheKey()
        {
            string cacheKey = this.cacheKey;
            string merchantId = GetMerchantId();
            cacheKey = $"{cacheKey}.{merchantId}";
            return cacheKey;
        }

        /// <summary>
        /// 获取商户ID
        /// </summary>
        /// <returns></returns>
        protected virtual string GetMerchantId()
        {
            IMerchantService merchantService;
            if (httpContextAccessor != null)
                merchantService = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IMerchantService>();
            else
                merchantService = serviceProvider.GetRequiredService<IMerchantService>();
            string merchantId = merchantService.MerchantId;
            if (string.IsNullOrEmpty(merchantId) || merchantId.Length != 32)
            {
                throw new ArgumentException("invalid merchant id");
            }
            return merchantId;
        }

        /// <summary>
        /// 取得缓存的数据
        /// </summary>
        /// <returns></returns>
        protected override IDictionary<TKey, TEntity> DoGetCacheData()
        {
            string cacheKey = GetCacheKey();
            IDictionary<TKey, TEntity> cacheData = (IDictionary<TKey, TEntity>)cache.Get(cacheKey);
            if (cacheData == null)
            {
                ICollection<TEntity> items = dal.GetList(new TFilter { MerchantId = GetMerchantId() });
                foreach (TEntity item in items)
                {
                    if (item != null)
                        AppendExtention(item);
                }
                cacheData = items.ToDictionary(CreateKeyExpression().Compile());
                cache.Set(cacheKey, cacheData);
            }
            return cacheData;
        }
    }
}
