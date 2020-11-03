namespace BaoMen.Common.Data
{
    /// <summary>
    /// 带过滤器的分层业务逻辑
    /// </summary>
    /// <typeparam name="TKey">键的类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TFilter">实体过滤器类型</typeparam>
    public interface IHierarchicalBusinessLogic<TKey, TEntity, TFilter> : IBusinessLogic<TKey, TEntity, TFilter>
        where TEntity : class, Model.IHierarchicalData<TKey>, new()
        where TFilter : class
    {
        /// <summary>
        /// 根据标识查询全名
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        string GetFullName(TKey id, string separator);

        /// <summary>
        /// 根据父标识查询实体列表
        /// </summary>
        /// <param name="id">父标识</param>
        /// <returns></returns>
        System.Collections.Generic.ICollection<TEntity> GetAllChildren(TKey id);

        /// <summary>
        /// 根据父标识查询所有子实体列表
        /// </summary>
        /// <param name="id">父标识</param>
        /// <returns></returns>
        System.Collections.Generic.ICollection<TEntity> GetChildren(TKey id);

        ///// <summary>
        ///// 获取分配了标识的新实体实例
        ///// </summary>
        ///// <param name="parent">父实体实例</param>
        ///// <returns></returns>
        //TEntity GetNew(TEntity parent);
    }
}
