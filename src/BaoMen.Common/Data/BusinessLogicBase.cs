using BaoMen.Common.Cache;
using BaoMen.Common.Constant;
using BaoMen.Common.Data.Helper;
using BaoMen.Common.Model;
using DotNetCore.CAP;
using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace BaoMen.Common.Data
{
    /// <summary>
    /// 业务逻辑
    /// </summary>
    [DataObject]
    public abstract class BusinessLogicBase<TKey, TEntity, TFilter, TDataAccess> : IBusinessLogic<TKey, TEntity, TFilter>
        where TFilter : class
        where TEntity : class, new()
        where TDataAccess : IDataAccess<TKey, TEntity, TFilter>
    {
        #region events

        #region log events
        /// <summary>
        /// 日志记录开始事件。日志记录前引发。
        /// </summary>
        public event EventHandler<LogEventArgs> OnLogWriting;

        /// <summary>
        /// 日志记录结束事件
        /// </summary>
        public event EventHandler<LogEventArgs> OnLogWrited;
        #endregion

        #region data select events
        /// <summary>
        /// 数据查询开始事件
        /// </summary>
        public event EventHandler<DataOperatingEventArgs> OnDataSelecting;

        /// <summary>
        /// 数据查询成功事件
        /// </summary>
        public event EventHandler<DataOperateSuccessEventArgs> OnDataSelectSuccess;

        /// <summary>
        /// 数据查询失败事件
        /// </summary>
        public event EventHandler<DataOperateErrorEventArgs> OnDataSelectError;

        /// <summary>
        /// 数据查询完成事件
        /// </summary>
        public event EventHandler<DataOperatedEventArgs> OnDataSelected;
        #endregion

        #region data insert events
        /// <summary>
        /// 数据插入开始事件
        /// </summary>
        public event EventHandler<DataOperatingEventArgs> OnDataInserting;

        /// <summary>
        /// 数据插入成功事件
        /// </summary>
        public event EventHandler<DataOperateSuccessEventArgs> OnDataInsertSuccess;

        /// <summary>
        /// 数据插入失败事件
        /// </summary>
        public event EventHandler<DataOperateErrorEventArgs> OnDataInsertError;

        /// <summary>
        /// 数据插入完成事件
        /// </summary>
        public event EventHandler<DataOperatedEventArgs> OnDataInserted;
        #endregion

        #region data update events
        /// <summary>
        /// 数据更新开始事件
        /// </summary>
        public event EventHandler<DataOperatingEventArgs> OnDataUpdating;

        /// <summary>
        /// 数据更新成功事件
        /// </summary>
        public event EventHandler<DataOperateSuccessEventArgs> OnDataUpdateSuccess;

        /// <summary>
        /// 数据更新失败事件
        /// </summary>
        public event EventHandler<DataOperateErrorEventArgs> OnDataUpdateError;

        /// <summary>
        /// 数据更新完成事件
        /// </summary>
        public event EventHandler<DataOperatedEventArgs> OnDataUpdated;
        #endregion

        #region data delete events
        /// <summary>
        /// 数据删除开始事件
        /// </summary>
        public event EventHandler<DataOperatingEventArgs> OnDataDeleting;

        /// <summary>
        /// 数据删除成功事件
        /// </summary>
        public event EventHandler<DataOperateSuccessEventArgs> OnDataDeleteSuccess;

        /// <summary>
        /// 数据删除失败事件
        /// </summary>
        public event EventHandler<DataOperateErrorEventArgs> OnDataDeleteError;

        /// <summary>
        /// 数据删除完成事件
        /// </summary>
        public event EventHandler<DataOperatedEventArgs> OnDataDeleted;
        #endregion

        #endregion

        #region permission
        /// <summary>
        /// 检查读取权限
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckReadPermission()
        {
            return true;
        }

        /// <summary>
        /// 检查写入权限（insert/update/delete 操作需要）
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckWritePermission()
        {
            return true;
        }
        #endregion

        #region process CRUD

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="dataOperationType">数据操作类型</param>
        /// <param name="func">执行的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <param name="hasPermission">检查权限的方法</param>
        /// <returns></returns>
        protected TResult Process<TResult>(DataOperationType dataOperationType, Func<LogEventInfo, TResult> func, Action<LogEventInfo> action, Func<bool> hasPermission)
        {
            LogEventInfo log = CreateLog("Process<TResult>(Func<TResult> func, Action<LogEventInfo> action, Func<bool> hasPermission, DataOperationType dataOperationType)");
            log.Properties["dataOperationType"] = dataOperationType;
            if (func.Target != null)
                log.Properties["func.Target"] = func.Target.ToString();
            log.Properties["func.Method.Name"] = func.Method.Name;
            action?.Invoke(log);
            EventHandler<DataOperatingEventArgs> onProcessing = null;
            EventHandler<DataOperatedEventArgs> onProcessed = null;
            EventHandler<DataOperateErrorEventArgs> onProcessError = null;
            EventHandler<DataOperateSuccessEventArgs> onProcessSuccess = null;
            switch (dataOperationType)
            {
                case DataOperationType.Select:
                    onProcessing = OnDataSelecting;
                    onProcessed = OnDataSelected;
                    onProcessError = OnDataSelectError;
                    onProcessSuccess = OnDataSelectSuccess;
                    break;

                case DataOperationType.Insert:
                    onProcessing = OnDataInserting;
                    onProcessed = OnDataInserted;
                    onProcessError = OnDataInsertError;
                    onProcessSuccess = OnDataInsertSuccess;
                    break;

                case DataOperationType.Update:
                    onProcessing = OnDataUpdating;
                    onProcessed = OnDataUpdated;
                    onProcessError = OnDataUpdateError;
                    onProcessSuccess = OnDataUpdateSuccess;
                    break;

                case DataOperationType.Delete:
                    onProcessing = OnDataDeleting;
                    onProcessed = OnDataDeleted;
                    onProcessError = OnDataDeleteError;
                    onProcessSuccess = OnDataDeleteSuccess;
                    break;
            }
            if (onProcessing != null)
            {
                DataOperatingEventArgs dataOperatingEventArgs = new DataOperatingEventArgs(dataOperationType);
                log.Properties["dataOperatingEventArgs"] = dataOperatingEventArgs;
                onProcessing(this, dataOperatingEventArgs);
                if (dataOperatingEventArgs.Cancel)
                {
                    log.Level = LogLevel.Debug;
                    log.Message = "operation canceled";
                    return default;
                }
            }
            try
            {
                if (hasPermission != null && !hasPermission())
                    throw new ArgumentException("no permission");
                TResult result = func(log);
                log.Level = LogLevel.Debug;
                log.Message = "process success";
#if DEBUG
                log.Properties["returnValue"] = result;
#endif
                if (onProcessSuccess != null)
                {
                    DataOperateSuccessEventArgs dataOperateSuccessEventArgs = new DataOperateSuccessEventArgs(dataOperationType, result);
                    log.Properties["dataOperateSuccessEventArgs"] = dataOperateSuccessEventArgs;
                    onProcessSuccess(this, dataOperateSuccessEventArgs);
                }
                return result;
            }
            catch (Exception exception)
            {
                BusinessLogicException businessLogicException = new BusinessLogicException(exception.Message, exception);
                log.Level = LogLevel.Warn;
                log.Exception = businessLogicException;
                log.Message = "process error";
                if (onProcessError != null)
                {
                    DataOperateErrorEventArgs dataOperateErrorEventArgs = new DataOperateErrorEventArgs(dataOperationType, businessLogicException);
                    log.Properties["dataOperateErrorEventArgs"] = dataOperateErrorEventArgs;
                    onProcessError(this, dataOperateErrorEventArgs);
                    if (dataOperateErrorEventArgs.Handled)
                    {
                        return default;
                    }
                }
                throw businessLogicException;
            }
            finally
            {
                if (onProcessed != null)
                {
                    DataOperatedEventArgs dataOperatedEventArgs = new DataOperatedEventArgs(dataOperationType);
                    log.Properties["dataOperatedEventArgs"] = dataOperatedEventArgs;
                    onProcessed(this, dataOperatedEventArgs);
                }
                WriteLog(log);
            }
        }

        /// <summary>
        /// 执行查询操作
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="func">查询的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns></returns>
        protected TResult ProcessSelect<TResult>(Func<TResult> func, Action<LogEventInfo> action = null)
        {
            return ProcessSelect(func, action, CheckReadPermission);
        }

        /// <summary>
        /// 执行查询操作
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="func">查询的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns></returns>
        protected TResult ProcessSelect<TResult>(Func<LogEventInfo, TResult> func, Action<LogEventInfo> action = null)
        {
            return ProcessSelect(func, action, CheckReadPermission);
        }

        /// <summary>
        /// 执行查询操作
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="func">查询的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <param name="hasPermission">检查权限的方法</param>
        /// <returns></returns>
        protected TResult ProcessSelect<TResult>(Func<TResult> func, Action<LogEventInfo> action, Func<bool> hasPermission)
        {
            return Process<TResult>(DataOperationType.Select, (log) => func.Invoke(), action, hasPermission);
        }

        /// <summary>
        /// 执行查询操作
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="func">查询的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <param name="hasPermission">检查权限的方法</param>
        /// <returns></returns>
        protected TResult ProcessSelect<TResult>(Func<LogEventInfo, TResult> func, Action<LogEventInfo> action, Func<bool> hasPermission)
        {
            return Process<TResult>(DataOperationType.Select, func, action, hasPermission);
        }

        /// <summary>
        /// 执行插入操作
        /// </summary>
        /// <param name="func">插入的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns></returns>
        protected TResult ProcessInsert<TResult>(Func<TResult> func, Action<LogEventInfo> action = null)
        {
            return ProcessInsert(func, action, CheckWritePermission);
        }

        /// <summary>
        /// 执行插入操作
        /// </summary>
        /// <param name="func">插入的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns></returns>
        protected TResult ProcessInsert<TResult>(Func<LogEventInfo, TResult> func, Action<LogEventInfo> action = null)
        {
            return ProcessInsert(func, action, CheckWritePermission);
        }

        /// <summary>
        /// 执行插入操作
        /// </summary>
        /// <param name="func">插入的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <param name="hasPermission">检查权限的方法</param>
        /// <returns></returns>
        protected TResult ProcessInsert<TResult>(Func<TResult> func, Action<LogEventInfo> action, Func<bool> hasPermission)
        {
            return Process<TResult>(DataOperationType.Insert, (log) => func.Invoke(), action, hasPermission);
        }

        /// <summary>
        /// 执行插入操作
        /// </summary>
        /// <param name="func">插入的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <param name="hasPermission">检查权限的方法</param>
        /// <returns></returns>
        protected TResult ProcessInsert<TResult>(Func<LogEventInfo, TResult> func, Action<LogEventInfo> action, Func<bool> hasPermission)
        {
            return Process<TResult>(DataOperationType.Insert, func, action, hasPermission);
        }

        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <param name="func">更新的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns></returns>
        protected TResult ProcessUpdate<TResult>(Func<TResult> func, Action<LogEventInfo> action = null)
        {
            return ProcessUpdate(func, action, CheckWritePermission);
        }

        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <param name="func">更新的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns></returns>
        protected TResult ProcessUpdate<TResult>(Func<LogEventInfo, TResult> func, Action<LogEventInfo> action = null)
        {
            return ProcessUpdate(func, action, CheckWritePermission);
        }

        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <param name="func">更新的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <param name="hasPermission">检查权限的方法</param>
        /// <returns></returns>
        protected TResult ProcessUpdate<TResult>(Func<TResult> func, Action<LogEventInfo> action, Func<bool> hasPermission)
        {
            return Process<TResult>(DataOperationType.Update, (log) => func.Invoke(), action, hasPermission);
        }

        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <param name="func">更新的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <param name="hasPermission">检查权限的方法</param>
        /// <returns></returns>
        protected TResult ProcessUpdate<TResult>(Func<LogEventInfo, TResult> func, Action<LogEventInfo> action, Func<bool> hasPermission)
        {
            return Process<TResult>(DataOperationType.Update, func, action, hasPermission);
        }

        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <param name="func">删除的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns></returns>
        protected TResult ProcessDelete<TResult>(Func<TResult> func, Action<LogEventInfo> action = null)
        {
            return ProcessDelete(func, action, CheckWritePermission);
        }

        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <param name="func">删除的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns></returns>
        protected TResult ProcessDelete<TResult>(Func<LogEventInfo, TResult> func, Action<LogEventInfo> action = null)
        {
            return ProcessDelete(func, action, CheckWritePermission);
        }

        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <param name="func">删除的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <param name="hasPermission">检查权限的方法</param>
        /// <returns></returns>
        protected TResult ProcessDelete<TResult>(Func<TResult> func, Action<LogEventInfo> action, Func<bool> hasPermission)
        {
            return Process<TResult>(DataOperationType.Delete, (log) => func.Invoke(), action, hasPermission);
        }

        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <param name="func">删除的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <param name="hasPermission">检查权限的方法</param>
        /// <returns></returns>
        protected TResult ProcessDelete<TResult>(Func<LogEventInfo, TResult> func, Action<LogEventInfo> action, Func<bool> hasPermission)
        {
            return Process<TResult>(DataOperationType.Delete, func, action, hasPermission);
        }

        /// <summary>
        /// 执行数据库事务操作
        /// </summary>
        /// <param name="func">要执行的方法</param>
        /// <param name="autoCommit">是否自动提交事务。默认值true</param>
        protected void ProcessWithTransaction(Action<IDbTransaction> func, bool autoCommit = true)
        {
            IDbConnection conn = null;
            IDbTransaction transaction = null;
            try
            {
                conn = dal.CreateConnection();
                conn.Open();
                transaction = conn.BeginTransaction();
                func?.Invoke(transaction);
                if (autoCommit)
                    transaction.Commit();
            }
            catch (NoneRowModifiedException noneRowModifiedException)
            {
                if (transaction != null)
                    transaction.Rollback();
                logger.Warn(noneRowModifiedException, "process transaction failure.none row modified.");
            }
            catch
            {
                if (transaction != null && conn.State == ConnectionState.Open)
                    transaction.Rollback();
                throw;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行数据库事务操作
        /// </summary>
        /// <param name="func">要执行的方法</param>
        /// <param name="autoCommit">是否自动提交事务。默认值true</param>
        protected int ProcessWithTransaction(Func<IDbTransaction, int> func, bool autoCommit = true)
        {
            IDbConnection conn = null;
            IDbTransaction transaction = null;
            try
            {
                conn = dal.CreateConnection();
                conn.Open();
                transaction = conn.BeginTransaction();
                int affectRows = 0;
                if (func != null)
                    affectRows = func(transaction);
                if (autoCommit)
                    transaction.Commit();
                return affectRows;
            }
            catch (NoneRowModifiedException noneRowModifiedException)
            {
                if (transaction != null)
                    transaction.Rollback();
                logger.Warn(noneRowModifiedException, "process transaction failure.none row modified.");
                return 0;
            }
            catch
            {
                if (transaction != null && conn.State == ConnectionState.Open)
                    transaction.Rollback();
                throw;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行Cap数据库事务操作
        /// </summary>
        /// <param name="func">要执行的方法</param>
        /// <param name="autoCommit">是否自动提交事务。默认值true</param>
        protected int ProcessWithCapTransaction(Func<IDbTransaction, int> func, ICapPublisher capPublisher, bool autoCommit = true)
        {
            IDbConnection conn = null;
            IDbTransaction transaction = null;
            try
            {
                conn = dal.CreateConnection();
                conn.Open();
                transaction = conn.BeginTransaction();
                capPublisher.Transaction.Value = capPublisher.ServiceProvider.GetService<ICapTransaction>();
                capPublisher.Transaction.Value.DbTransaction = transaction;
                capPublisher.Transaction.Value.AutoCommit = autoCommit;
                int affectRows = 0;
                if (func != null)
                    affectRows = func(transaction);
                return affectRows;
            }
            catch (NoneRowModifiedException noneRowModifiedException)
            {
                if (transaction != null)
                    capPublisher.Transaction.Value.Rollback();
                logger.Warn(noneRowModifiedException, "process cap transaction failure.none row modified.");
                throw;
            }
            catch
            {
                if (transaction != null)
                    capPublisher.Transaction.Value.Rollback();
                throw;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
                capPublisher?.Transaction?.Value?.Dispose();
            }
        }

        /// <summary>
        /// 执行Cap数据库事务操作
        /// </summary>
        /// <param name="action">要执行的方法</param>
        /// <param name="autoCommit">是否自动提交事务。默认值true</param>
        protected void ProcessWithCapTransaction(Action<IDbTransaction> action, ICapPublisher capPublisher, bool autoCommit = true)
        {
            IDbConnection conn = null;
            IDbTransaction transaction = null;
            try
            {
                conn = dal.CreateConnection();
                conn.Open();
                transaction = conn.BeginTransaction();
                capPublisher.Transaction.Value = capPublisher.ServiceProvider.GetService<ICapTransaction>();
                capPublisher.Transaction.Value.DbTransaction = transaction;
                capPublisher.Transaction.Value.AutoCommit = autoCommit;
                action(transaction);
            }
            catch (NoneRowModifiedException noneRowModifiedException)
            {
                if (transaction != null)
                    capPublisher.Transaction.Value.Rollback();
                logger.Warn(noneRowModifiedException, "process cap transaction failure.none row modified.");
                throw;
            }
            catch
            {
                if (transaction != null)
                    capPublisher.Transaction.Value.Rollback();
                throw;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
                capPublisher?.Transaction?.Value?.Dispose();
            }
        }
        #endregion

        /// <summary>
        /// <see cref="NLog.Logger"/>实例
        /// </summary>
        protected readonly Logger logger;

        /// <summary>
        /// 数据访问实例
        /// </summary>
        protected readonly TDataAccess dal;

        /// <summary>
        /// 类型实例
        /// </summary>
        protected readonly Type type;

        /// <summary>
        /// 构造函数。返回一个BusinessLogic实例
        /// </summary>
        public BusinessLogicBase(IConfiguration configuration)
        {
            type = GetType();
            logger = LogManager.GetLogger(type.FullName);
            //logger = LogManager.GetCurrentClassLogger();
            dal = DataAccessFactory.Create<TEntity, TFilter, TDataAccess>(configuration);
        }

        /// <summary>
        /// 检查分页参数
        /// </summary>
        /// <param name="startRowIndex">开始索引</param>
        /// <param name="maximumRows">最大记录数</param>
        protected void CheckPageParameter(ref int startRowIndex, ref int maximumRows)
        {
            if (maximumRows < 1)
                maximumRows = int.MaxValue;
            if (startRowIndex < 0)
                startRowIndex = 0;
        }

        /// <summary>
        /// 创建新的LogEventInfo实例
        /// </summary>
        /// <returns></returns>
        protected LogEventInfo CreateLog()
        {
            return new LogEventInfo { LoggerName = logger.Name };
        }

        /// <summary>
        /// 创建新的LogEventInfo实例
        /// </summary>
        /// <param name="method">方法名称</param>
        /// <returns></returns>
        protected LogEventInfo CreateLog(string method)
        {
            LogEventInfo logEventInfo = CreateLog();
            logEventInfo.Properties["method"] = method;
            return logEventInfo;
        }

        /// <summary>
        /// 写日志并引发事件
        /// </summary>
        /// <param name="log"></param>
        protected void WriteLog(LogEventInfo log)
        {
            OnLogWriting?.Invoke(this, new LogEventArgs(log));
            logger.Log(log);
            OnLogWrited?.Invoke(this, new LogEventArgs(log));
        }

        #region CRUD
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <exception cref="BusinessLogicException">
        /// <para>如果在调用DoInsert方法时抛出异常，则抛出包装后的异常。</para>
        /// <para>如果在DataInsertedError事件中处理了异常，则不抛出异常</para>
        /// </exception>
        /// <param name="item">实体实例</param>
        /// <returns>成功插入的数量</returns>
        public virtual int Insert(TEntity item)
        {
            return ProcessInsert(
                () =>
                {
                    if (item == null)
                        throw new ArgumentNullException("item");
                    return DoInsert(item);
                },
                (log) =>
                {
                    log.Properties["item"] = item;
                });
        }

        /// <summary>
        /// 插入数据
        /// <para>仅内部使用。注意：如果是自增字段并且使用了缓存，一定要返回自增字段的值</para>
        /// </summary>
        /// <param name="item">实体实例</param>
        /// <returns>成功增加的数量</returns>
        protected virtual int DoInsert(TEntity item)
        {
            return dal.Insert(item);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <exception cref="BusinessLogicException">
        /// <para>如果在调用DoUpdate方法时抛出异常，则抛出包装后的异常。</para>
        /// <para>如果在DataUpdateError事件中处理了异常，则不抛出异常</para>
        /// </exception>
        /// <param name="item">实体实例</param>
        /// <returns>成功更新的数量</returns>
        public virtual int Update(TEntity item)
        {
            return ProcessUpdate(() =>
            {
                if (item == null)
                    throw new ArgumentNullException("item");
                return DoUpdate(item);
            },
            (log) =>
            {
                log.Properties["item"] = item;
            });
        }

        /// <summary>
        /// 更新数据
        /// <para>仅内部使用。</para>
        /// </summary>
        /// <param name="item">实体实例</param>
        /// <returns>成功更新的数量</returns>
        protected virtual int DoUpdate(TEntity item)
        {
            return dal.Update(item);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <exception cref="BusinessLogicException">
        /// <para>如果在调用方法时抛出异常，则抛出包装后的异常。</para>
        /// <para>如果在DataChangeFailed事件中处理了异常，则不抛出异常</para>
        /// </exception>
        /// <param name="item">实体实例</param>
        /// <returns>成功删除的数量</returns>
        public virtual int Delete(TEntity item)
        {
            return ProcessDelete(() =>
            {
                if (item == null)
                    throw new ArgumentNullException("item");
                return DoDelete(item);
            },
            (log) =>
            {
                log.Properties["item"] = item;
            });
        }

        /// <summary>
        /// 删除数据。包含数据验证。
        /// <para>仅内部使用。</para>
        /// </summary>
        /// <param name="item">实体实例</param>
        /// <returns>成功删除的数量</returns>
        protected virtual int DoDelete(TEntity item)
        {
            return dal.Delete(item);
        }

        /// <summary>
        /// 根据实体标识获取实体实例
        /// </summary>
        /// <exception cref="BusinessLogicException">
        /// <para>如果在调用DoGet方法时抛出异常，则抛出包装后的异常。</para>
        /// <para>如果在SelectedError事件中处理了异常，则不抛出异常</para>
        /// </exception>
        /// <param name="id">实体标识</param>
        /// <typeparam name="TKey">实体标识类型</typeparam>
        /// <returns>实体类的实例</returns>
        public virtual TEntity Get(TKey id)
        {
            return ProcessSelect<TEntity>(
                () =>
                {
                    return DoGet(id);
                },
                (log) =>
                {
                    log.Properties["id"] = id;
                });
        }

        /// <summary>
        /// 根据实体标识获取实体实例。
        /// <para>仅内部使用。</para>
        /// </summary>
        /// <param name="id">实体标识</param>
        /// <typeparam name="TKey">实体标识类型</typeparam>
        /// <returns>实体类的实例</returns>
        protected virtual TEntity DoGet(TKey id)
        {
            TEntity item = dal.Get(id);
            if (item != null)
            {
                AppendExtention(item);
            }
            return item;
        }

        /// <summary>
        /// 通过比较两个数组，获取需要插入，更新，删除的新数组
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="datas">新数据</param>
        /// <param name="existDatas">已有数据</param>
        /// <param name="compareKey">比较键是否相等</param>
        /// <param name="compareValue">比较值是否相等</param>
        /// <returns></returns>
        protected (List<T> Create, List<T> Update, List<T> Delete, List<T> Same) GetUpdateData<T>(ICollection<T> datas, ICollection<T> existDatas, Func<T, T, bool> compareKey, Func<T, T, bool> compareValue)
        {
            List<T> sameList = new List<T>();
            List<T> createList = new List<T>();
            List<T> updateList = new List<T>();
            List<T> deleteList = existDatas == null ? new List<T>() : existDatas.ToList();
            if (datas != null)
            {
                foreach (T data in datas)
                {
                    T existData = deleteList.SingleOrDefault(p => compareKey(p, data));
                    if (existData == null)
                    {
                        createList.Add(data);
                    }
                    else
                    {
                        if (compareValue(data, existData))
                        {
                            sameList.Add(data);
                        }
                        else
                        {
                            updateList.Add(data);
                        }
                        deleteList.Remove(existData);
                    }
                }
            }
            return (createList, updateList, deleteList, sameList);
        }
        #endregion

        #region GetList

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
        public ICollection<TEntity> GetList(TFilter filter = null, string sortExpression = null, int startRowIndex = 0, int maximumRows = int.MaxValue)
        {
            CheckPageParameter(ref startRowIndex, ref maximumRows);
            return ProcessSelect(
                () =>
                {
                    return DoGetList(filter, sortExpression, startRowIndex, maximumRows);
                },
                (log) =>
                {
                    log.Properties["filter"] = filter;
                    log.Properties["sortExpression"] = sortExpression;
                    log.Properties["maximumRows"] = maximumRows;
                    log.Properties["startRowIndex"] = startRowIndex;
                });
        }

        /// <summary>
        /// 取得实体列表
        /// <para>仅内部使用。</para>
        /// </summary>
        /// <param name="filter">过滤器实体</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <param name="startRowIndex">开始索引</param>
        /// <param name="maximumRows">最大记录数</param>
        /// <returns>实体列表</returns>
        protected virtual ICollection<TEntity> DoGetList(TFilter filter, string sortExpression, int startRowIndex, int maximumRows)
        {
            PrepareFilter(filter);
            ICollection<TEntity> items = dal.GetList(filter, sortExpression, startRowIndex, maximumRows);
            if (items?.Count > 0)
            {
                foreach (TEntity item in items)
                {
                    if (item != null)
                        AppendExtention(item);
                }
            }
            return items;
        }
        #endregion

        #region GetListCount

        /// <summary>
        /// 取得实体列表合计数量
        /// </summary>
        /// <exception cref="BusinessLogicException">
        /// <para>如果数据访问层查询抛出异常，则抛出包装后的异常。</para>
        /// <para>如果在Selected事件中处理了异常，则不抛出异常</para>
        /// </exception>
        /// <param name="filter">实体过滤器实例</param>
        /// <returns>合计数量.如果在DoGetListCount过程中产生了异常并且在Selected事件中处理了异常，返回-1</returns>
        public int GetListCount(TFilter filter = null)
        {
            return ProcessSelect(() => { return DoGetListCount(filter); },
                (log) =>
                {
                    log.Properties["filter"] = filter;
                });
        }

        /// <summary>
        /// 取得实体列表合计数量
        /// <para>仅内部使用。</para>
        /// </summary>
        /// <param name="filter">实体过滤器实例</param>
        /// <returns>合计数量</returns>
        protected virtual int DoGetListCount(TFilter filter)
        {
            PrepareFilter(filter);
            return dal.GetListCount(filter);
        }
        #endregion

        #region GetCountAndList

        /// <summary>
        /// 取得记录数及实体列表
        /// </summary>
        /// <exception cref="BusinessLogicException">
        /// <para>如果在调用DoGetList方法时抛出异常，则抛出包装后的异常。</para>
        /// <para>如果在Selected事件中处理了异常，则不抛出异常</para>
        /// </exception>
        /// <param name="filter">实体过滤器实例</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <param name="startRowIndex">开始索引</param>
        /// <param name="maximumRows">最大记录数</param>
        /// <returns>实体列表</returns>
        public virtual Tuple<int, ICollection<TEntity>> GetCountAndList(TFilter filter = null, string sortExpression = null, int startRowIndex = 0, int maximumRows = int.MaxValue)
        {
            return ProcessSelect(() =>
            {
                int rows = DoGetListCount(filter);
                if (rows == 0)
                    return Tuple.Create<int, ICollection<TEntity>>(0, new List<TEntity>());
                CheckPageParameter(ref startRowIndex, ref maximumRows);
                return Tuple.Create(rows, DoGetList(filter, sortExpression, startRowIndex, maximumRows));
            },
            (log) =>
            {
                log.Properties[nameof(filter)] = filter;
                log.Properties[nameof(sortExpression)] = sortExpression;
                log.Properties["startRowIndex"] = startRowIndex;
                log.Properties["maximumRows"] = maximumRows;
            });
        }
        #endregion

        /// <summary>
        /// 添加实体的扩展属性
        /// </summary>
        /// <param name="item">实体实例</param>
        protected virtual void AppendExtention(TEntity item)
        {

        }

        /// <summary>
        /// 准备过滤器
        /// </summary>
        /// <param name="filter"></param>
        protected virtual void PrepareFilter(TFilter filter)
        {

        }
    }

    /// <summary>
    /// 缓存业务逻辑
    /// </summary>
    [DataObject]
    public abstract class CacheableBusinessLogicBase<TKey, TEntity, TFilter, TDataAccess> : BusinessLogicBase<TKey, TEntity, TFilter, TDataAccess>, ICacheableBusinessLogic<TKey, TEntity, TFilter>
        where TEntity : class, new()
        where TFilter : class
        where TDataAccess : IDataAccess<TKey, TEntity, TFilter>
    {
        /// <summary>
        /// 日志记录开始事件。日志记录前引发。
        /// </summary>
        public event EventHandler<CacheRemovingEventArgs> OnCacheRemoving;

        /// <summary>
        /// 日志记录结束事件
        /// </summary>
        public event EventHandler<CacheRemovedEventArgs> OnCacheRemoved;

        /// <summary>
        /// 缓存管理实例
        /// </summary>
        protected ICache cache;

        /// <summary>
        /// 字典缓存的键
        /// </summary>
        protected readonly string cacheKey;

        /// <summary>
        /// 获取实体标识字段名称
        /// <para>默认为Id。如果不同，请覆盖此属性</para>
        /// </summary>
        /// <remarks>
        /// </remarks>
        protected virtual string IdentityPropertyName { get { return "Id"; } }

        /// <summary>
        /// 构造函数。返回一个BusinessLogic实例
        /// </summary>
        public CacheableBusinessLogicBase(IConfiguration configuration)
            : base(configuration)
        {
            cacheKey = string.Format("{0}.CacheKey", type.FullName);
            cache = CacheFactory.Get(configuration);
        }

        /// <summary>
        /// 获取缓存的键
        /// </summary>
        /// <returns></returns>
        protected virtual string GetCacheKey()
        {
            return cacheKey;
        }

        /// <summary>
        /// 生成取得Key的表达式
        /// </summary>
        /// <returns></returns>
        protected Expression<Func<TEntity, TKey>> CreateKeyExpression()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "p");
            var property = Expression.Property(parameter, IdentityPropertyName);
            return Expression.Lambda<Func<TEntity, TKey>>(property, parameter);
        }

        /// <summary>
        /// 清除缓存。
        /// </summary>
        /// <remarks>
        /// 重写时一定要先调用此方法
        /// </remarks>
        public virtual void RemoveCache()
        {
            string cacheKey = GetCacheKey();
            if (OnCacheRemoving != null)
            {
                CacheRemovingEventArgs e = new CacheRemovingEventArgs()
                {
                    CacheKey = cacheKey
                };
                OnCacheRemoving.Invoke(this, e);
                if (!e.Cancel)
                {
                    cache.Remove(cacheKey);
                }
            }
            else
            {
                cache.Remove(cacheKey);
            }
            OnCacheRemoved?.Invoke(this, new CacheRemovedEventArgs()
            {
                CacheKey = cacheKey
            });
        }

        /// <summary>
        /// 已重写。插入数据
        /// </summary>
        /// <exception cref="BusinessLogicException">
        /// <para>如果插入成功并且响应的行数大于0则清空缓存，直至下次读取数据时再次将数据读入到缓存中。反之则对缓存中的数据没有影响</para>
        /// <para>如果在调用DoInsert方法时抛出异常，则抛出包装后的异常。</para>
        /// <para>如果在DataInsertedError事件中处理了异常，则不抛出异常</para>
        /// </exception>
        /// <param name="item">实体实例</param>
        /// <returns>成功插入的数量</returns>
        public override int Insert(TEntity item)
        {
            return ProcessInsert(
                () =>
                {
                    if (item == null)
                        throw new ArgumentNullException("item");
                    int rows = DoInsert(item);
                    if (rows > 0)
                    {
                        RemoveCache();
                    }
                    return rows;
                },
                (log) =>
                {
                    log.Properties["item"] = item;
                });
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <exception cref="BusinessLogicException">
        /// <para>如果更新成功并且响应的行数大于0则清空缓存，直至下次读取数据时再次将数据读入到缓存中。反之则对缓存中的数据没有影响</para>
        /// <para>如果在调用DoUpdate方法时抛出异常，则抛出包装后的异常。</para>
        /// <para>如果在DataUpdateError事件中处理了异常，则不抛出异常</para>
        /// </exception>
        /// <param name="item">实体实例</param>
        /// <returns>成功更新的数量</returns>
        public override int Update(TEntity item)
        {
            return ProcessUpdate(() =>
            {
                if (item == null)
                    throw new ArgumentNullException("item");
                int rows = DoUpdate(item);
                if (rows > 0)
                {
                    RemoveCache();
                }
                return rows;
            },
            (log) =>
            {
                log.Properties["item"] = item;
            });
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <exception cref="BusinessLogicException">
        /// <para>如果删除成功并且响应的行数大于0则清空缓存，直至下次读取数据时再次将数据读入到缓存中。反之则对缓存中的数据没有影响</para>
        /// <para>如果在调用方法时抛出异常，则抛出包装后的异常。</para>
        /// <para>如果在DataChangeFailed事件中处理了异常，则不抛出异常</para>
        /// </exception>
        /// <param name="item">实体实例</param>
        /// <returns>成功删除的数量</returns>
        public override int Delete(TEntity item)
        {
            return ProcessDelete(() =>
            {
                if (item == null)
                    throw new ArgumentNullException("item");
                int rows = DoDelete(item);
                if (rows > 0)
                {
                    RemoveCache();
                }
                return rows;
            },
            (log) =>
            {
                log.Properties["item"] = item;
            });
        }

        /// <summary>
        /// 已重写。根据实例标识获取实例
        /// </summary>
        /// <param name="id">标识值</param>
        /// <returns>检索到的实例。若不存在返回null</returns>
        protected override TEntity DoGet(TKey id)
        {
            if (id == null) return null;
            IDictionary<TKey, TEntity> cacheData = DoGetCacheData();
            cacheData.TryGetValue(id, out TEntity value);
            return value;
        }

        /// <summary>
        /// 取得实体列表
        /// <para>仅内部使用。建议重载时覆盖此方法而不是公共的GetList方法</para>
        /// </summary>
        /// <param name="filter">实体过滤器实例</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <param name="maximumRows">最大记录数</param>
        /// <param name="startRowIndex">开始索引</param>
        /// <returns>实体列表</returns>
        protected override ICollection<TEntity> DoGetList(TFilter filter, string sortExpression, int startRowIndex, int maximumRows)
        {
            IDictionary<TKey, TEntity> cacheData = DoGetCacheData();
            if (cacheData.Values == null || cacheData.Values.Count == 0)
            {
                return cacheData.Values;
            }
            PrepareFilter(filter);
            IEnumerable<TEntity> tempItems = cacheData.Values;
            if (filter != null)
            {
                Expression<Func<TEntity, bool>> expression = CreateFilterExpression(filter);
                if (expression != null)
                {
                    tempItems = tempItems.Where(expression.Compile());
                }
                tempItems = ExecuteCustomFilter(tempItems, filter);
            }
            if (!string.IsNullOrEmpty(sortExpression))
            {
                string[] expressions = sortExpression.Split(',');
                foreach (string expression in expressions)
                {
                    string[] sort = expression.Split(' ');
                    if (sort.Length == 2 && sort[1].ToUpper() == "DESC")
                    {
                        tempItems = tempItems.OrderByDescending(CreateSortExpression(sort[0]).Compile());
                    }
                    else
                    {
                        tempItems = tempItems.OrderBy(CreateSortExpression(sort[0]).Compile());
                    }
                }
            }
            CheckPageParameter(ref startRowIndex, ref maximumRows);
            if (maximumRows != int.MaxValue)
            {
                if (startRowIndex == 0)
                {
                    tempItems = tempItems.Take(maximumRows);
                }
                else
                {
                    tempItems = tempItems.Skip(startRowIndex).Take(maximumRows);
                }
            }
            return tempItems.ToList();
        }

        /// <summary>
        /// 执行自定义的过滤
        /// </summary>
        /// <param name="items">实体枚举数</param>
        /// <param name="filter">实体过滤器</param>
        protected virtual IEnumerable<TEntity> ExecuteCustomFilter(IEnumerable<TEntity> items, TFilter filter)
        {
            return items;
        }

        /// <summary>
        /// 取得缓存的数据
        /// </summary>
        /// <returns></returns>
        protected virtual IDictionary<TKey, TEntity> DoGetCacheData()
        {
            string cacheKey = GetCacheKey();
            IDictionary<TKey, TEntity> cacheData = (IDictionary<TKey, TEntity>)cache.Get(cacheKey);
            if (cacheData == null)
            {
                ICollection<TEntity> items = dal.GetList();
                foreach (TEntity item in items)
                {
                    if (item != null)
                        AppendExtention(item);
                }
                cacheData = items.ToDictionary(CreateKeyExpression().Compile());
                cache.Set(cacheKey, cacheData);
            }
            return cacheData;
        }

        /// <summary>
        /// 取得实体列表合计数量
        /// <para>仅内部使用。</para>
        /// </summary>
        /// <param name="filter">实体过滤器实例</param>
        /// <returns>合计数量</returns>
        protected override int DoGetListCount(TFilter filter)
        {
            ICollection<TEntity> items = DoGetList(filter, null, 0, int.MaxValue);
            return items.Count;
        }

        /// <summary>
        /// 添加表达式
        /// <para>添加比较操作为Equl逻辑操作为ConditionAnd的表达式</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="propertyExpressioin">实体属性表达式</param>
        /// <param name="valueExpression">值表达式</param>
        protected void AddExpression(ref Expression expression, Expression propertyExpressioin, Expression valueExpression)
        {
            AddExpression(ref expression, propertyExpressioin, valueExpression, DbCompareOperator.Equal, DbLogicOperator.And);
        }

        /// <summary>
        /// 添加表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="propertyExpressioin">实体属性表达式</param>
        /// <param name="valueExpression">值表达式</param>
        /// <param name="compareOperator">比较操作类型</param>
        /// <param name="logicOperator">逻辑操作类型</param>
        protected void AddExpression(ref Expression expression, Expression propertyExpressioin, Expression valueExpression, DbCompareOperator compareOperator, DbLogicOperator logicOperator)
        {
            //var p = Expression.Property(parameter, propertyName);
            //var v = Expression.Constant(value);
            Expression newExpression;
            ExpressionType compareType = ExpressionHelper.ConvertCompareOperator(compareOperator);
            if (compareType == ExpressionType.Default)
            {
                //成功转换
                if (valueExpression is ConstantExpression constantValueExpression)
                {
                    if (constantValueExpression.Type == typeof(string))
                    {
                        switch (compareOperator)
                        {
                            case DbCompareOperator.StartWith:
                                newExpression = Expression.Call(propertyExpressioin, typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }), new Expression[] { constantValueExpression });
                                break;
                            case DbCompareOperator.EndWith:
                                newExpression = Expression.Call(propertyExpressioin, typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) }), new Expression[] { constantValueExpression });
                                break;
                            case DbCompareOperator.Contains:
                                //newExpression = Expression.Call(propertyExpressioin, typeof(string).GetMethod("IndexOf", new Type[] { typeof(string) }), new Expression[] { constantValueExpression });
                                //newExpression = Expression.MakeBinary(ExpressionType.NotEqual, newExpression, Expression.Constant(-1));
                                newExpression = Expression.Call(propertyExpressioin, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), new Expression[] { constantValueExpression });
                                break;
                            default:
                                throw new ArgumentException("valueExpression.Value type must be string", "valueExpression");
                        }
                        Expression equalExpr = Expression.NotEqual(
                            propertyExpressioin,
                            Expression.Constant(null)
                        );
                        Expression conditionExpr = Expression.Condition(
                           equalExpr,
                           newExpression,
                           Expression.Constant(false)
                         );
                        newExpression = conditionExpr;
                    }
                    else
                    {
                        if (constantValueExpression.Type.IsGenericType)
                        {
                            Type argumentType = constantValueExpression.Type.GetGenericArguments()[0];
                            Type collectionType = typeof(ICollection<>).MakeGenericType(argumentType);
                            if (collectionType.IsAssignableFrom(constantValueExpression.Type))
                            {
                                switch (compareOperator)
                                {
                                    case DbCompareOperator.In:
                                        newExpression = Expression.Call(constantValueExpression, collectionType.GetMethod("Contains"), new Expression[] { propertyExpressioin });
                                        break;
                                    case DbCompareOperator.NotIn:
                                        newExpression = Expression.Call(constantValueExpression, collectionType.GetMethod("Contains"), new Expression[] { propertyExpressioin });
                                        Expression conditionExpression = Expression.Condition(
                                              newExpression,
                                              Expression.Constant(false),
                                              Expression.Constant(true)
                                            );
                                        newExpression = conditionExpression;
                                        break;
                                    default:
                                        throw new ArgumentException("ICollection type compare operator must be In or NotIn");
                                }

                            }
                            else
                            {
                                throw new ArgumentException("unsupported filter property type");
                            }
                        }
                        else
                        {
                            throw new ArgumentException("valueExpression");
                        }
                    }
                }
                else
                {
                    throw new ArgumentNullException("valueExpression");
                }
            }
            else
            {
                //BinaryExpression e = Expression.MakeBinary(compareType, propertyExpressioin, valueExpression);
                newExpression = Expression.MakeBinary(compareType, propertyExpressioin, valueExpression);
            }
            if (expression == null)
                expression = newExpression;
            else
            {
                ExpressionType logicType = ExpressionHelper.CovertDbLogicOperator(logicOperator);
                expression = Expression.MakeBinary(logicType, expression, newExpression);
            }
        }

        /// <summary>
        /// 创建过滤表达式
        /// <para>提供一个简单的默认实现。判断实体过滤器中所有public的属性。如果值不等于null，则提供一个默认的相等比较表达式。
        /// 如果有多个比较表达式，则之间的关系为条件与。如果实体过滤器需要更高级的操作，请重新实现此方法</para>
        /// </summary>
        /// <param name="filter">过滤器实例</param>
        /// <returns>过滤表达式</returns>
        protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression(TFilter filter)
        {
            Expression expression = null;
            var parameter = Expression.Parameter(typeof(TEntity), "p");
            DatabaseEntityFilterHelper filterHelper = new DatabaseEntityFilterHelper(typeof(TFilter));
            foreach (PropertyInfo propertyInfo in filterHelper.PropertyInfoDict.Values)
            {
                if (!filterHelper.PropertyAttributeDict.ContainsKey(propertyInfo.Name) || propertyInfo.CanRead == false || propertyInfo.GetIndexParameters().Length > 0)
                    continue;
                object value = propertyInfo.GetValue(filter, null);
                if (value != null)
                {
                    Expression propertyExpression = Expression.Property(parameter, filterHelper.PropertyAttributeDict[propertyInfo.Name].EntityPropertyName);
                    //if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(FilterProperty<object>).GetGenericTypeDefinition())
                    if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(FilterProperty<>).GetGenericTypeDefinition())
                    {
                        dynamic d = value;
                        Type entityPropertyType = filterHelper.EntityHelper.PropertyInfoDict[filterHelper.PropertyAttributeDict[propertyInfo.Name].EntityPropertyName].PropertyType;
                        if (d.CompareOperator == DbCompareOperator.IsNull || d.CompareOperator == DbCompareOperator.IsNotNull)
                        {
                            if (d.Value != null) d.Value = null;
                        }
                        if (entityPropertyType.IsGenericType && entityPropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                        {
                            //Type[] filterTypeArguments = propertyInfo.PropertyType.GetGenericArguments();
                            //System.Linq.Expressions.UnaryExpression typeAsExpression = System.Linq.Expressions.Expression.TypeAs(System.Linq.Expressions.Expression.Constant(d.Value, filterTypeArguments[0]), entityPropertyType);
                            //AddExpression(ref expression, parameter, filterHelper.PropertyAttributeDict[propertyInfo.Name].EntityPropertyName, typeAsExpression, d.CompareOperator, d.LogicOperator);
                            AddExpression(ref expression, propertyExpression, Expression.Constant(d.Value, entityPropertyType), d.CompareOperator, d.LogicOperator);
                        }
                        else
                            AddExpression(ref expression, propertyExpression, Expression.Constant(d.Value), d.CompareOperator, d.LogicOperator);
                    }
                    else if (Type.GetTypeCode(propertyInfo.PropertyType) == TypeCode.String)
                    {
                        string s = (string)value;
                        if (s != string.Empty)
                        {
                            AddExpression(ref expression, propertyExpression, Expression.Constant(value));
                        }
                    }
                    else
                    {
                        //此处未处理普通类型的转换，可能引发异常。例如filter属性为int?而实体属性为int时
                        AddExpression(ref expression, propertyExpression, Expression.Constant(value));
                    }
                }
            }
            if (expression == null)
                return null;
            else
                return Expression.Lambda<Func<TEntity, bool>>(expression, parameter);
        }

        /// <summary>
        /// 取得排序表达式
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns>排序表达式</returns>
        protected Expression<Func<TEntity, object>> CreateSortExpression(string propertyName)
        {
            Type entityType = typeof(TEntity);
            PropertyInfo property = entityType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (property != null)
            {
                var paramerter = Expression.Parameter(typeof(TEntity), "p");
                var propertyExpression = Expression.Property(paramerter, property);
                var unaryExpression = Expression.TypeAs(propertyExpression, typeof(object));
                var expression = Expression.Lambda<Func<TEntity, object>>(unaryExpression, paramerter);
                return expression;
            }
            else
            {
                throw new ArgumentException($"invalid propertyName.Entity has no specified property \"{propertyName}\" ");
            }
        }
    }

    /// <summary>
    /// 带缓存和过滤器的分层结构业务逻辑
    /// </summary>
    /// <typeparam name="TEntity">实体类型。必须实现IHierarchicalData接口</typeparam>
    /// <typeparam name="TFilter">实体过滤器类型</typeparam>
    /// <typeparam name="TDataAccess">数据访问类型</typeparam>
    public abstract class HierarchicalCacheableBusinessLogicBase<TKey, TEntity, TFilter, TDataAccess> : CacheableBusinessLogicBase<TKey, TEntity, TFilter, TDataAccess>, IHierarchicalBusinessLogic<TKey, TEntity, TFilter>
        where TEntity : class, IHierarchicalData<TKey>, new()
        where TFilter : class
        where TDataAccess : IDataAccess<TKey, TEntity, TFilter>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected HierarchicalCacheableBusinessLogicBase(IConfiguration configuration)
            : base(configuration)
        {
        }

        /// <summary>
        /// 根据标识查询全名
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public virtual string GetFullName(TKey id, string separator)
        {
            StringBuilder stringBuilder = new StringBuilder();
            TEntity item = Get(id);
            if (item != null)
            {
                stringBuilder.Append(item.Name);
                item = Get(item.ParentId);
                while (item != null)
                {
                    stringBuilder.Insert(0, separator);
                    stringBuilder.Insert(0, item.Name);
                    item = Get(item.ParentId);
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 根据父标识查询实体列表
        /// </summary>
        /// <param name="id">父标识</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public ICollection<TEntity> GetChildren(TKey id)
        {
            //#warning for test loading only,remember to remove this line.
            //            System.Threading.Thread.Sleep(1000); 
            return ProcessSelect(
                () => { return DoGetChildren(id); },
                (log) =>
                {
                    log.Properties["id"] = id;
                }
            );
        }

        /// <summary>
        /// 根据父标识查询实体列表
        /// </summary>
        /// <param name="id">父标识</param>
        /// <returns></returns>
        protected virtual ICollection<TEntity> DoGetChildren(TKey id)
        {
            //if (string.IsNullOrWhiteSpace(id))
            //    id = "00";
            ICollection<TEntity> items = DoGetList(null, null, 0, int.MaxValue).Where(p => Equals(id, p.ParentId)).ToList();
            //if (items.Count == 0)
            //    items.Add(DoCreateRootNode());
            return items;
        }

        /// <summary>
        /// 根据父标识查询所有子实体列表
        /// </summary>
        /// <param name="id">父标识</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public ICollection<TEntity> GetAllChildren(TKey id)
        {
            return ProcessSelect(
                () => { return DoGetAllChildren(id); },
                (log) =>
                {
                    log.Properties["id"] = id;
                }
            );
        }

        /// <summary>
        /// 根据父标识查询所有子实体列表
        /// </summary>
        /// <param name="id">父标识</param>
        /// <returns></returns>
        protected virtual ICollection<TEntity> DoGetAllChildren(TKey id)
        {
            //ICollection<TEntity> items = DoGetList(null, null, 0, int.MaxValue);
            //if (string.IsNullOrWhiteSpace(id))
            //    return items;
            ////增加是否是系统编号或者GUID的判断
            //if (Regex.IsMatch(id, @"^\d*$"))
            //    return items.Where(p => p.ParentId.StartsWith(id)).ToList();

            //guid，
            List<TEntity> newItmes = new List<TEntity>();
            AppendChildren(newItmes, id);
            return newItmes;
        }

        /// <summary>
        /// 递归附加子节点
        /// </summary>
        /// <param name="items">要添加的列表</param>
        /// <param name="parentId">子节点</param>
        protected virtual void AppendChildren(ICollection<TEntity> items, TKey parentId)
        {
            ICollection<TEntity> children = DoGetChildren(parentId);
            if (children?.Count > 0)
            {
                foreach (var child in children)
                {
                    items.Add(child);
                    AppendChildren(items, child.Id);
                }
            }
        }

        ///// <summary>
        ///// 获取分配了标识的新实体实例
        ///// </summary>
        ///// <param name="parent">父实体实例</param>
        ///// <returns></returns>
        //public TEntity GetNew(TEntity parent)
        //{
        //    return ProcessSelect(
        //        () => { return DoGetNew(parent); },
        //        (log) =>
        //        {
        //            log.Properties["parent"] = parent;
        //        }
        //    );
        //}

        /////// <summary>
        /////// 获取分配了标识的新实体实例
        /////// </summary>
        /////// <param name="parent">父实体实例</param>
        /////// <returns></returns>
        //protected abstract TEntity DoGetNew(TEntity parent);

        ///// <summary>
        ///// 获取分配了标识的新实体实例
        ///// </summary>
        ///// <param name="parent">父实体实例</param>
        ///// <returns></returns>
        //protected virtual TEntity DoGetNew(TEntity parent)
        //{
        //    TEntity newItem = new TEntity();
        //    if (parent == null)
        //        newItem.ParentId = "00";
        //    else
        //        newItem.ParentId = parent.Id;
        //    //ICollection<TEntity> children = DoGetChildren(newItem.ParentId);
        //    for (int i = 1; i < 100; i++)
        //    {
        //        string id = i.ToString("00");
        //        if (newItem.ParentId != "00")
        //            id = newItem.ParentId + id;
        //        TEntity item = DoGet(id);
        //        if (item == null)
        //        {
        //            newItem.Id = id;
        //            break;
        //        }
        //    }
        //    if (string.IsNullOrEmpty(newItem.Id))
        //        throw new ArgumentException("数据已达99条，无法生成新Id");
        //    return newItem;
        //}

        /// <summary>
        /// 重新删除的方法。检查是否有子节点，如果有抛出异常。
        /// </summary>
        /// <param name="item">实体实例</param>
        /// <returns></returns>
        protected override int DoDelete(TEntity item)
        {
            TEntity entity = Get(item.Id);
            if (entity == null)
                return 0;
            ICollection<TEntity> entityList = GetChildren(item.Id);
            if (entityList.Count > 0)
                throw new BusinessLogicException("该节点包含子节点，请先删除子节点。");
            return base.DoDelete(item);
        }
    }
}