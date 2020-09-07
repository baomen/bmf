using System;

namespace BaoMen.Common.Cache
{
    /// <summary>
    /// 可同步的缓存接口
    /// </summary>
    public interface ISynchronizedCache : ICache
    {
        /// <summary>
        /// 开始同步
        /// </summary>
        event EventHandler<CacheSynchronizingEventArgs> OnSynchronizing;

        /// <summary>
        /// 结束同步
        /// </summary>
        event EventHandler<CacheSynchronizedEventArgs> OnSynchronized;
    }
}
