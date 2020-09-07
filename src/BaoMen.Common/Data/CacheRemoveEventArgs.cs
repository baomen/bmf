using System;

namespace BaoMen.Common.Data
{
    /// <summary>
    /// 缓存移除完成事件参数
    /// </summary>
    public class CacheRemovedEventArgs : EventArgs
    {
        ///// <summary>
        ///// 缓存列表的键
        ///// </summary>
        //public string ListKey { get; set; }

        /// <summary>
        /// 缓存的键
        /// </summary>
        public string CacheKey { get; set; }
    }

    /// <summary>
    /// 缓存移除事件参数
    /// </summary>
    public class CacheRemovingEventArgs : CacheRemovedEventArgs
    {
        /// <summary>
        /// 获取或设置是否取消操作
        /// </summary>
        public bool Cancel { get; set; }
    }
}
