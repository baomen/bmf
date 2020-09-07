namespace BaoMen.Common.Model
{
    /// <summary>
    /// 分层数据
    /// </summary>
    /// <typeparam name="T">标识类型</typeparam>
    public interface IHierarchicalData<T>
    {
        /// <summary>
        /// 获取或设置标识
        /// </summary>
        T Id { get; set; }

        /// <summary>
        /// 获取或设置父标识
        /// </summary>
        T ParentId { get; set; }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        string Name { get; set; }

    }
}
