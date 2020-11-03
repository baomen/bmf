using System;
using System.Collections.Generic;

namespace BaoMen.Common.Data
{
    /// <summary>
    /// 业务逻辑接口
    /// </summary>
    public interface IBusinessLogic
    {
    }

    /// <summary>
    /// 带过滤器的业务逻辑
    /// </summary>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IBusinessLogic<TKey, TEntity>
        where TEntity : class, new()
    {
        #region 事件

        #region data select events
        /// <summary>
        /// 数据查询开始事件
        /// </summary>
        event EventHandler<DataOperatingEventArgs> OnDataSelecting;

        /// <summary>
        /// 数据查询成功事件
        /// </summary>
        event EventHandler<DataOperateSuccessEventArgs> OnDataSelectSuccess;

        /// <summary>
        /// 数据查询失败事件
        /// </summary>
        event EventHandler<DataOperateErrorEventArgs> OnDataSelectError;

        /// <summary>
        /// 数据查询完成事件
        /// </summary>
        event EventHandler<DataOperatedEventArgs> OnDataSelected;
        #endregion

        #region data insert events
        /// <summary>
        /// 数据插入开始事件
        /// </summary>
        event EventHandler<DataOperatingEventArgs> OnDataInserting;

        /// <summary>
        /// 数据插入成功事件
        /// </summary>
        event EventHandler<DataOperateSuccessEventArgs> OnDataInsertSuccess;

        /// <summary>
        /// 数据插入失败事件
        /// </summary>
        event EventHandler<DataOperateErrorEventArgs> OnDataInsertError;

        /// <summary>
        /// 数据插入完成事件
        /// </summary>
        event EventHandler<DataOperatedEventArgs> OnDataInserted;
        #endregion

        #region data update events
        /// <summary>
        /// 数据更新开始事件
        /// </summary>
        event EventHandler<DataOperatingEventArgs> OnDataUpdating;

        /// <summary>
        /// 数据更新成功事件
        /// </summary>
        event EventHandler<DataOperateSuccessEventArgs> OnDataUpdateSuccess;

        /// <summary>
        /// 数据更新失败事件
        /// </summary>
        event EventHandler<DataOperateErrorEventArgs> OnDataUpdateError;

        /// <summary>
        /// 数据更新完成事件
        /// </summary>
        event EventHandler<DataOperatedEventArgs> OnDataUpdated;
        #endregion

        #region data delete events
        /// <summary>
        /// 数据删除开始事件
        /// </summary>
        event EventHandler<DataOperatingEventArgs> OnDataDeleting;

        /// <summary>
        /// 数据删除成功事件
        /// </summary>
        event EventHandler<DataOperateSuccessEventArgs> OnDataDeleteSuccess;

        /// <summary>
        /// 数据删除失败事件
        /// </summary>
        event EventHandler<DataOperateErrorEventArgs> OnDataDeleteError;

        /// <summary>
        /// 数据删除完成事件
        /// </summary>
        event EventHandler<DataOperatedEventArgs> OnDataDeleted;
        #endregion
        #endregion

        #region 增删改
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <exception cref="BusinessLogicException">
        /// <para>如果在调用DoInsert方法时抛出异常，则抛出包装后的异常。</para>
        /// <para>如果在DataInsertedError事件中处理了异常，则不抛出异常</para>
        /// </exception>
        /// <param name="item">实体实例</param>
        /// <returns>成功插入的数量</returns>
        int Insert(TEntity item);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <exception cref="BusinessLogicException">
        /// <para>如果在调用DoUpdate方法时抛出异常，则抛出包装后的异常。</para>
        /// <para>如果在DataUpdateError事件中处理了异常，则不抛出异常</para>
        /// </exception>
        /// <param name="item">实体实例</param>
        /// <returns>成功更新的数量</returns>
        int Update(TEntity item);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <exception cref="BusinessLogicException">
        /// <para>如果在调用方法时抛出异常，则抛出包装后的异常。</para>
        /// <para>如果在DataChangeFailed事件中处理了异常，则不抛出异常</para>
        /// </exception>
        /// <param name="item">实体标识</param>
        /// <returns>成功删除的数量</returns>
        int Delete(TEntity item);
        #endregion

        #region 取单条数据
        /// <summary>
        /// 根据实体标识获取实体实例
        /// </summary>
        /// <exception cref="BusinessLogicException">
        /// <para>如果在调用DoGet方法时抛出异常，则抛出包装后的异常。</para>
        /// <para>如果在SelectedError事件中处理了异常，则不抛出异常</para>
        /// </exception>
        /// <param name="id">实体标识</param>
        /// <returns>实体类的实例</returns>
        TEntity Get(TKey id);
        #endregion
    }

    /// <summary>
    /// 带过滤器的业务逻辑
    /// </summary>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TFilter">实体过滤器类型</typeparam>
    public interface IBusinessLogic<TKey, TEntity, TFilter> : IBusinessLogic<TKey, TEntity>
        where TEntity : class, new()
        where TFilter : class
    {

        #region 取多条数据
        /// <summary>
        /// 取得实体列表
        /// </summary>
        /// <exception cref="BusinessLogicException">
        /// <para>如果数据访问层查询抛出异常，则抛出包装后的异常。</para>
        /// <para>如果在Selected事件中处理了异常，则不抛出异常</para>
        /// </exception>
        /// <param name="filter">实体过滤器实例</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <param name="startRowIndex">开始索引</param>
        /// <param name="maximumRows">最大记录数</param>
        /// <returns>实体列表</returns>
        ICollection<TEntity> GetList(TFilter filter = null, string sortExpression = null, int startRowIndex = 0, int maximumRows = int.MaxValue);
        #endregion

        #region 取合计
        /// <summary>
        /// 取得实体列表合计数量
        /// </summary>
        /// <exception cref="BusinessLogicException">
        /// <para>如果数据访问层查询抛出异常，则抛出包装后的异常。</para>
        /// <para>如果在Selected事件中处理了异常，则不抛出异常</para>
        /// </exception>
        /// <param name="filter">实体过滤器实例</param>
        /// <returns>合计数量.如果在DoGetListCount过程中产生了异常并且在Selected事件中处理了异常，返回-1</returns>
        int GetListCount(TFilter filter = null);

        #endregion

        #region 取得记录数及实体列表
        /// <summary>
        /// 取得记录数及实体列表
        /// </summary>
        /// <exception cref="BusinessLogicException">
        /// <para>如果数据访问层查询抛出异常，则抛出包装后的异常。</para>
        /// <para>如果在Selected事件中处理了异常，则不抛出异常</para>
        /// </exception>
        /// <param name="filter">实体过滤器实例</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <param name="startRowIndex">开始索引</param>
        /// <param name="maximumRows">最大记录数</param>
        /// <returns>实体列表</returns>
        Tuple<int, ICollection<TEntity>> GetCountAndList(TFilter filter = null, string sortExpression = null, int startRowIndex = 0, int maximumRows = int.MaxValue);
        #endregion
    }

    /// <summary>
    /// 缓存业务逻辑接口
    /// </summary>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TFilter">实体过滤器类型</typeparam>
    public interface ICacheableBusinessLogic<TKey, TEntity, TFilter> : IBusinessLogic<TKey, TEntity, TFilter>
        where TEntity : class, new()
        where TFilter : class
    {
        #region cache events
        /// <summary>
        /// 日志记录开始事件。日志记录前引发。
        /// </summary>
        event EventHandler<CacheRemovingEventArgs> OnCacheRemoving;

        /// <summary>
        /// 日志记录结束事件
        /// </summary>
        event EventHandler<CacheRemovedEventArgs> OnCacheRemoved;
        #endregion

        /// <summary>
        /// 移除缓存
        /// </summary>
        void RemoveCache();
    }
}
