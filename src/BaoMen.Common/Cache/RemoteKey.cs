namespace BaoMen.Common.Cache
{
    /// <summary>
    /// 缓存远程的键
    /// </summary>
    [System.Serializable]
    public class RemoteKey
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 客户端连接的GUID
        /// </summary>
        public string Guid { get; set; }
    }
}
