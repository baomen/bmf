namespace BaoMen.Common.Cache
{
    /// <summary>
    /// 缓存同步完成事件参数
    /// </summary>
    public class CacheSynchronizedEventArgs
    {
        /// <summary>
        /// 缓存的键
        /// </summary>
        public string Key { get; set; }
    }

    /// <summary>
    /// 缓存同步事件参数
    /// </summary>
    public class CacheSynchronizingEventArgs : CacheSynchronizedEventArgs
    {
        /// <summary>
        /// 是否取消同步
        /// </summary>
        public bool Cancel { get; set; }
    }
}
