namespace BaoMen.Common.Cache
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 获取缓存的数据
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">关键字</param>
        void Remove(string key);

        /// <summary>
        /// 设置缓存的值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        void Set(string key, object value);
    }
}
