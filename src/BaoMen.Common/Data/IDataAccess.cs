using System.Collections.Generic;
using System.Data;

namespace BaoMen.Common.Data
{
    /// <summary>
    /// 数据访问接口
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// 创建数据库链接
        /// </summary>
        /// <returns></returns>
        IDbConnection CreateConnection();
    }

    /// <summary>
    /// 数据访问接口
    /// </summary>
    public interface IDataAccess<TEntity> : IDataAccess
        where TEntity : class, new()
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="item">实体实例</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>成功增加的数量</returns>
        int Insert(TEntity item, IDbTransaction transaction = null);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="item">实体实例</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>成功更新的数量</returns>
        int Update(TEntity item, IDbTransaction transaction = null);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="item">实体实例</param>
        /// <param name="transaction">数据库事务实例</param>
        /// <returns>成功删除的数量</returns>
        int Delete(TEntity item, IDbTransaction transaction = null);
    }

    /// <summary>
    /// 数据访问接口
    /// </summary>
    public interface IDataAccess<TKey, TEntity, TFilter>: IDataAccess<TEntity>
        where TFilter : class
        where TEntity : class, new()
    {
        #region CRUD
        /// <summary>
        /// 根据实体标识获取实体实例
        /// </summary>
        /// <typeparam name="TKey">实体标识类型</typeparam>
        /// <param name="id">实体标识</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>实体类的实例</returns>
        TEntity Get(TKey id, IDbTransaction transaction = null);

        /// <summary>
        /// 取得实体列表
        /// </summary>
        /// <param name="filter">实体过滤器实例</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <param name="startRowIndex">开始索引</param>
        /// <param name="maximumRows">页大小</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>实体列表</returns>
        ICollection<TEntity> GetList(TFilter filter = null, string sortExpression = null, int startRowIndex = 0, int maximumRows = int.MaxValue, IDbTransaction transaction = null);

        /// <summary>
        /// 取得实体列表合计数量
        /// </summary>
        /// <param name="filter">实体过滤器实例</param>
        /// <returns>合计数量</returns>
        int GetListCount(TFilter filter = null);
        #endregion
    }
}
