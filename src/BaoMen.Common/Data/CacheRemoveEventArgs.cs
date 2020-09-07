using System;

namespace BaoMen.Common.Data
{
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

    public class CacheRemovingEventArgs : CacheRemovedEventArgs
    {
        /// <summary>
        /// 获取或设置是否取消操作
        /// </summary>
        public bool Cancel { get; set; }
    }
}
