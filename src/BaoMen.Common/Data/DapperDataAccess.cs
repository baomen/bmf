using Dapper;
using BaoMen.Common.Constant;
using BaoMen.Common.Extension;
using BaoMen.Common.Model;
using Microsoft.Extensions.Caching.Memory;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace BaoMen.Common.Data
{
    /// <summary>
    /// Dapper数据访问基类
    /// </summary>
    public abstract class DapperDataAccess
    {
        /// <summary>
        /// 日志实例
        /// </summary>
        protected readonly ILogger logger;

        /// <summary>
        /// 缓存
        /// </summary>
        protected static readonly Microsoft.Extensions.Caching.Memory.MemoryCache memoryCache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());

        /// <summary>
        /// 类型
        /// </summary>
        protected readonly Type type;

        /// <summary>
        /// 数据库的Provider工厂实例
        /// </summary>
        protected readonly DbProviderFactory dbProviderFactory;

        /// <summary>
        /// 数据库链接串
        /// </summary>
        protected readonly string connectionString;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DapperDataAccess()
        {
            type = GetType();
            logger = LogManager.GetLogger(type.FullName);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="providerName">数据库提供程序名称</param>
        public DapperDataAccess(string connectionString, string providerName) : this()
        {
            //.net core 不支持
            //dbProviderFactory = DbProviderFactories.GetFactory(providerName);
            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(providerName))
            {
                throw new ArgumentNullException("connectionString", "connectionString is null or providerName is null.");
            }
            dbProviderFactory = GetDbProviderFactory(providerName);
            this.connectionString = connectionString;
        }

        /// <summary>
        /// 创建一个32位的ID
        /// </summary>
        /// <returns></returns>
        protected string CreateId()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 获取数据库提供程序工厂
        /// </summary>
        /// <param name="providerName"></param>
        /// <returns></returns>
        protected DbProviderFactory GetDbProviderFactory(string providerName)
        {
            if (string.IsNullOrEmpty(providerName)) throw new ArgumentNullException("providerName");

            string providerLowerName = providerName.ToLower();
            return providerLowerName switch
            {
                "mysql.data.mysqlclient" => MySqlConnector.MySqlConnectorFactory.Instance,
                //return MySql.Data.MySqlClient.MySqlClientFactory.Instance;
                "system.data.sqlclient" => System.Data.SqlClient.SqlClientFactory.Instance,
                _ => throw new NotImplementedException($"unsupported provider factory {providerName}"),
            };
        }

        /// <summary>
        /// 移除sql条件语句的前缀
        /// <para>前缀为" and "或者" or "</para>
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/>实例</param>
        protected virtual void RemoveSqlConditionPrefix(StringBuilder sb)
        {
            if (sb != null && sb.Length > 3)
            {
                string tmp = sb.ToString();
                if (tmp.StartsWith(" and ", StringComparison.OrdinalIgnoreCase))
                {
                    sb.Remove(0, 5);
                }
                else if (tmp.StartsWith(" or ", StringComparison.OrdinalIgnoreCase))
                {
                    sb.Remove(0, 4);
                }
            }
        }

        ///// <summary>
        ///// 从DapperCommand转为CommandDefinition
        ///// </summary>
        ///// <param name="dapperCommand"></param>
        ///// <returns></returns>
        //protected CommandDefinition ConvertFromDapperCommand(DapperCommand dapperCommand)
        //{
        //    return new CommandDefinition(
        //            dapperCommand.CommandText,
        //            dapperCommand.Parameters,
        //            dapperCommand.Transaction,
        //            dapperCommand.CommandTimeout,
        //            dapperCommand.CommandType,
        //            dapperCommand.Flags,
        //            dapperCommand.CancellationToken);
        //}

        /// <summary>
        /// 从在 DbCommand 中指定的存储过程中检索参数信息并填充指定的 Parameters 对象的 DbCommand 集合
        /// </summary>
        /// <param name="command"></param>
        protected void DeriveParameters(DbCommand command)
        {
            switch (command)
            {
                case MySqlConnector.MySqlCommand mySqlCommand:
                    MySqlConnector.MySqlCommandBuilder.DeriveParameters(mySqlCommand);
                    break;
                //case MySql.Data.MySqlClient.MySqlCommand mySqlCommand:
                //    MySql.Data.MySqlClient.MySqlCommandBuilder.DeriveParameters(mySqlCommand);
                //    break;
                case System.Data.SqlClient.SqlCommand sqlCommand:
                    System.Data.SqlClient.SqlCommandBuilder.DeriveParameters(sqlCommand);
                    break;
            }
        }

        /// <summary>
        /// 获取存储过程的参数（注意：要链接数据库，高耗费资源的操作）
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="dbProviderFactory"></param>
        /// <returns></returns>
        protected virtual DbParameterCollection GetStoredProcedureParameters(string procedureName, string connectionString = null, DbProviderFactory dbProviderFactory = null)
        {

            if (string.IsNullOrEmpty(procedureName)) throw new ArgumentNullException("procedureName");
            //if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("connectionString");
            // if (dbProviderFactory == null) throw new ArgumentNullException("dbProviderFactory");
            if (string.IsNullOrEmpty(connectionString)) connectionString = this.connectionString;
            if (dbProviderFactory == null) dbProviderFactory = this.dbProviderFactory;
            string cacheKey = $"DatabaseProcedureParameters_{procedureName}";
            DbParameterCollection cachedDbParameterCollection = memoryCache.Get<DbParameterCollection>(cacheKey);
            if (cachedDbParameterCollection == null)
            {
                DbCommand dbCommand = dbProviderFactory.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = procedureName;
                using (DbConnection connection = dbProviderFactory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    dbCommand.Connection = connection;
                    DeriveParameters(dbCommand);
                    connection.Close();
                }
                cachedDbParameterCollection = dbCommand.Parameters;
#warning 暂时写死5分钟缓存过期
                memoryCache.Set(cacheKey, cachedDbParameterCollection, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(5) });
            }
            return cachedDbParameterCollection;
        }

        /// <summary>
        /// 创建默认的数据库链接
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            IDbConnection dbConnection = dbProviderFactory.CreateConnection();
            dbConnection.ConnectionString = connectionString;
            return dbConnection;
        }

        #region process CRUD
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="dataOperationType">数据操作类型</param>
        /// <param name="func">执行的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns></returns>
        protected TResult Process<TResult>(DataOperationType dataOperationType, Func<LogEventInfo, TResult> func, Action<LogEventInfo> action = null)
        {
            LogEventInfo log = new LogEventInfo() { LoggerName = logger.Name };
            log.Properties["method"] = "Process<TResult>(Func<TResult> func, IDictionary arguments, DataOperationType dataOperationType)";
            log.Properties["dataOperationType"] = dataOperationType;
            log.Properties["func.Target"] = func.Target?.ToString();
            log.Properties["func.Method.Name"] = func.Method.Name;
            action?.Invoke(log);
            try
            {
                TResult result = func(log);
                log.Level = LogLevel.Debug;
                log.Message = "Process success.";
#if DEBUG
                log.Properties["returnValue"] = result;
#endif
                return result;
            }
            catch (NoneRowModifiedException noneRowModifiedException)
            {
                log.Level = LogLevel.Warn;
                log.Exception = noneRowModifiedException;
                log.Message = noneRowModifiedException.Message;
                throw noneRowModifiedException;
            }
            catch (Exception e)
            {
                DataAccessException dataAccessException = new DataAccessException(e.Message, e);
                log.Level = LogLevel.Warn;
                log.Exception = dataAccessException;
                log.Message = dataAccessException.Message;
                throw dataAccessException;
            }
            finally
            {
                logger.Log(log);
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
            return Process(DataOperationType.Select, (log) => func.Invoke(), action);
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
            return Process(DataOperationType.Select, func, action);
        }

        /// <summary>
        /// 执行插入操作
        /// </summary>
        /// <param name="func">插入的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns>响应的行数</returns>
        protected TResult ProcessInsert<TResult>(Func<TResult> func, Action<LogEventInfo> action = null)
        {
            return Process(DataOperationType.Insert, (log) => func.Invoke(), action);
        }

        /// <summary>
        /// 执行插入操作
        /// </summary>
        /// <param name="func">插入的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns>响应的行数</returns>
        protected TResult ProcessInsert<TResult>(Func<LogEventInfo, TResult> func, Action<LogEventInfo> action = null)
        {
            return Process(DataOperationType.Insert, func, action);
        }

        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <param name="func">更新的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns>响应的行数</returns>
        protected TResult ProcessUpdate<TResult>(Func<TResult> func, Action<LogEventInfo> action = null)
        {
            return Process(DataOperationType.Update, (log) => func.Invoke(), action);
        }

        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <param name="func">更新的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns>响应的行数</returns>
        protected TResult ProcessUpdate<TResult>(Func<LogEventInfo, TResult> func, Action<LogEventInfo> action = null)
        {
            return Process(DataOperationType.Update, func, action);
        }

        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <param name="func">删除的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns>响应的行数</returns>
        protected TResult ProcessDelete<TResult>(Func<TResult> func, Action<LogEventInfo> action = null)
        {
            return Process(DataOperationType.Delete, (log) => func.Invoke(), action);
        }

        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <param name="func">删除的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns>响应的行数</returns>
        protected TResult ProcessDelete<TResult>(Func<LogEventInfo, TResult> func, Action<LogEventInfo> action = null)
        {
            return Process(DataOperationType.Delete, func, action);
        }

        #endregion

    }

    /// <summary>
    /// 数据访问基类
    /// </summary>
    public abstract class DapperDataAccess<TKey, TEntity, TFilter> : DapperDataAccess, IDataAccess<TKey, TEntity, TFilter>
        where TEntity : class, new()
        where TFilter : class
    {
        /// <summary>
        /// 数据库表名称
        /// </summary>
        protected abstract string TableName { get; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="providerName">数据库提供程序名称</param>
        public DapperDataAccess(string connectionString, string providerName) : base(connectionString, providerName)
        {

        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="propertyName">参数名称</param>
        /// <param name="filterProperty">过滤器属性实例</param>
        /// <param name="parameters">参数</param>
        protected virtual string AddParameter<T>(string columnName, string propertyName, FilterProperty<T> filterProperty, DynamicParameters parameters)
        {
            if (filterProperty == null || string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(columnName)) return string.Empty;
            switch (filterProperty.CompareOperator)
            {
                case Constant.DbCompareOperator.Equal:
                    parameters.Add(propertyName, filterProperty.Value);
                    return string.Format(" {0} {1}=@{2}", filterProperty.LogicOperator, columnName, propertyName);
                case Constant.DbCompareOperator.NotEqual:
                    parameters.Add(propertyName, filterProperty.Value);
                    return string.Format(" {0} {1}<>@{2}", filterProperty.LogicOperator, columnName, propertyName);
                case Constant.DbCompareOperator.GreaterThan:
                    parameters.Add(propertyName, filterProperty.Value);
                    return string.Format(" {0} {1}>@{2}", filterProperty.LogicOperator, columnName, propertyName);
                case Constant.DbCompareOperator.GreaterThanOrEqual:
                    parameters.Add(propertyName, filterProperty.Value);
                    return string.Format(" {0} {1}>=@{2}", filterProperty.LogicOperator, columnName, propertyName);
                case Constant.DbCompareOperator.LessThan:
                    parameters.Add(propertyName, filterProperty.Value);
                    return string.Format(" {0} {1}<@{2}", filterProperty.LogicOperator, columnName, propertyName);
                case Constant.DbCompareOperator.LessThanOrEqual:
                    parameters.Add(propertyName, filterProperty.Value);
                    return string.Format(" {0} {1}<=@{2}", filterProperty.LogicOperator, columnName, propertyName);
                case Constant.DbCompareOperator.IsNull:
                    return string.Format(" {0} {1} is null", filterProperty.LogicOperator, columnName, propertyName);
                case Constant.DbCompareOperator.IsNotNull:
                    return string.Format(" {0} {1} is not null", filterProperty.LogicOperator, columnName, propertyName);
                case Constant.DbCompareOperator.StartWith:
                    parameters.Add(propertyName, $"%{filterProperty.Value}");
                    return string.Format(" {0} {1} like @{2}", filterProperty.LogicOperator, columnName, propertyName);
                case Constant.DbCompareOperator.EndWith:
                    parameters.Add(propertyName, $"{filterProperty.Value}%");
                    return string.Format(" {0} {1} like @{2}", filterProperty.LogicOperator, columnName, propertyName);
                case Constant.DbCompareOperator.Contains:
                    parameters.Add(propertyName, $"%{filterProperty.Value}%");
                    return string.Format(" {0} {1} like @{2}", filterProperty.LogicOperator, columnName, propertyName);
                case Constant.DbCompareOperator.In:
                    parameters.Add(propertyName, filterProperty.Value);
                    return string.Format(" {0} {1} in @{2}", filterProperty.LogicOperator, columnName, propertyName);
                case Constant.DbCompareOperator.NotIn:
                    parameters.Add(propertyName, filterProperty.Value);
                    return string.Format(" {0} {1} not in @{2}", filterProperty.LogicOperator, columnName, propertyName);
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="stringBuilder"><see cref="StringBuilder"/>实例</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="propertyName">参数名称</param>
        /// <param name="filterProperty">过滤器属性实例</param>
        /// <param name="parameters">参数</param>
        protected virtual void AddParameter<T>(StringBuilder stringBuilder, string columnName, string propertyName, FilterProperty<T> filterProperty, DynamicParameters parameters)
        {
            //if (filterProperty == null || string.IsNullOrEmpty(columnName)) return;
            if (filterProperty == null || string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(columnName)) return;
            stringBuilder.Append(AddParameter(columnName, propertyName, filterProperty, parameters));
        }

        #region CRUD
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="item">实体实例</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>成功增加的数量</returns>
        public int Insert(TEntity item, IDbTransaction transaction = null)
        {
            //ListDictionary arguments = new ListDictionary();
            //arguments.Add("item", item);
            //arguments.Add("inTransaction", transaction != null);
            return ProcessInsert(
                () => { return DoInsert(item, transaction); },
                (log) =>
                {
                    log.Properties["item"] = item;
                    log.Properties["inTransaction"] = transaction != null;
                }
            );
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="item">实体实例</param>
        /// <param name="transaction">数据库事务</param>
        /// <param name="getIdentity">获取插入的自增ID的委托</param>
        /// <returns>成功增加的数量</returns>
        protected virtual int DoInsert(TEntity item, IDbTransaction transaction, Action<IDbConnection, IDbTransaction> getIdentity = null)
        {
            DapperCommand dapperCommand = CreateInsertCommand(item);
            logger.Debug("dapper command:{command}", dapperCommand);
            dapperCommand.Transaction = transaction;
            IDbConnection connection = transaction?.Connection;
            if (connection == null) connection = CreateConnection();
            if (getIdentity == null)
            {
                return connection.Execute(dapperCommand);
            }
            int rows;
            if (transaction == null)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    transaction = connection.BeginTransaction();
                    dapperCommand.Transaction = transaction;
                    rows = connection.Execute(dapperCommand);
                    getIdentity(connection, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            else
            {
                rows = connection.Execute(dapperCommand);
                getIdentity(connection, transaction);
            }
            return rows;
        }

        /// <summary>
        /// 取得插入数据的数据库命令
        /// </summary>
        /// <param name="item">实体数据</param>
        /// <returns></returns>
        protected abstract DapperCommand CreateInsertCommand(TEntity item);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="item">实体实例</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>成功更新的数量</returns>
        public int Update(TEntity item, IDbTransaction transaction = null)
        {
            return ProcessUpdate(
                () => { return DoUpdate(item, transaction); },
                (log) =>
                {
                    log.Properties["item"] = item;
                    log.Properties["inTransaction"] = transaction != null;
                }
            );
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="item">实体实例</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>成功更新的数量</returns>
        protected virtual int DoUpdate(TEntity item, IDbTransaction transaction = null)
        {
            DapperCommand dapperCommand = CreateUpdateCommand(item);
            logger.Debug("dapper command:{command}", dapperCommand);
            dapperCommand.Transaction = transaction;
            IDbConnection connection = transaction?.Connection ?? CreateConnection();
            return connection.Execute(dapperCommand);
        }

        /// <summary>
        /// 取得更新数据的数据库命令
        /// </summary>
        /// <param name="item">实体数据</param>
        /// <returns></returns>
        protected abstract DapperCommand CreateUpdateCommand(TEntity item);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="item">实体实例</param>
        /// <param name="transaction">数据库事务实例</param>
        /// <returns>成功删除的数量</returns>
        public int Delete(TEntity item, IDbTransaction transaction = null)
        {
            return ProcessDelete(
                () =>
                {
                    return DoDelete(item, transaction);
                },
                (log) =>
                {
                    log.Properties["item"] = item;
                    log.Properties["inTransaction"] = transaction != null;
                }
            );
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="item">实体实例</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>成功删除的数量</returns>
        protected virtual int DoDelete(TEntity item, IDbTransaction transaction = null)
        {
            DapperCommand dapperCommand = CreateDeleteCommand(item);
            logger.Debug("dapper command:{command}", dapperCommand);
            dapperCommand.Transaction = transaction;
            IDbConnection connection = transaction?.Connection;
            if (connection == null) connection = CreateConnection();
            return connection.Execute(dapperCommand);
        }

        /// <summary>
        /// 取得删除数据的数据库命令
        /// </summary>
        /// <param name="item">实体实例</param>
        /// <returns></returns>
        protected abstract DapperCommand CreateDeleteCommand(TEntity item);

        /// <summary>
        /// 根据实体标识获取实体实例
        /// </summary>
        /// <typeparam name="TKey">实体标识类型</typeparam>
        /// <param name="id">实体标识</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>实体类的实例</returns>
        public TEntity Get(TKey id, IDbTransaction transaction = null)
        {
            return ProcessSelect(
                () =>
                {
                    return DoGet(id, transaction);
                },
                (log) =>
                {
                    log.Properties["id"] = id;
                    log.Properties["inTransaction"] = transaction != null;
                }
            );
        }

        /// <summary>
        /// 根据实体标识获取实体实例
        /// </summary>
        /// <param name="id">实体标识</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>实体类的实例</returns>
        protected virtual TEntity DoGet(TKey id, IDbTransaction transaction = null)
        {
            DapperCommand dapperCommand = CreateGetCommand(id);
            logger.Debug("dapper command:{command}", dapperCommand);
            dapperCommand.Transaction = transaction;
            IDbConnection connection = transaction?.Connection;
            if (connection == null) connection = CreateConnection();
            return connection.QueryFirstOrDefault<TEntity>(dapperCommand);
        }

        /// <summary>
        /// 创建读取单条数据的数据库命令
        /// </summary>
        /// <param name="id">实体标识</param>
        /// <returns>数据库命令</returns>
        protected abstract DapperCommand CreateGetCommand(TKey id);

        /// <summary>
        /// 取得实体列表
        /// </summary>
        /// <param name="filter">实体过滤器实例</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <param name="startRowIndex">开始索引</param>
        /// <param name="maximumRows">页大小</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>实体列表</returns>
        public ICollection<TEntity> GetList(TFilter filter = null, string sortExpression = null, int startRowIndex = 0, int maximumRows = int.MaxValue, IDbTransaction transaction = null)
        {
            return ProcessSelect(
                () =>
                {
                    return DoGetList(filter, sortExpression, startRowIndex, maximumRows, transaction);
                },
                (log) =>
                {
                    log.Properties["filter"] = filter;
                    log.Properties["sortExpression"] = sortExpression;
                    log.Properties["startRowIndex"] = startRowIndex;
                    log.Properties["maximumRows"] = maximumRows;
                    log.Properties["inTransaction"] = transaction != null;
                }
            );
        }

        /// <summary>
        /// 取得实体列表
        /// </summary>
        /// <param name="filter">实体过滤器实例</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <param name="startRowIndex">开始索引</param>
        /// <param name="maximumRows">页大小</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>实体列表</returns>
        protected virtual ICollection<TEntity> DoGetList(TFilter filter, string sortExpression, int startRowIndex, int maximumRows, IDbTransaction transaction = null)
        {
            DapperCommand dapperCommand = CreateGetListCommand(filter, sortExpression);
            logger.Debug("dapper command:{command}", dapperCommand);
            dapperCommand.Transaction = transaction;
            IDbConnection connection = transaction?.Connection;
            if (connection == null) connection = CreateConnection();
            return connection.Query<TEntity>(dapperCommand, startRowIndex, maximumRows).AsList();
        }

        /// <summary>
        /// 创建读取多条数据的数据库命令
        /// </summary>
        /// <param name="filter">实体过滤器实例</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <returns>数据库命令</returns>
        protected virtual DapperCommand CreateGetListCommand(TFilter filter, string sortExpression)
        {
            string sql = $"SELECT * FROM {TableName}";
            DynamicParameters parameters = null;
            if (filter != null)
            {
                //(string Where, DynamicParameters Parameters) filterSql = CreateFilterSqlWhere(filter);
                //if (!string.IsNullOrEmpty(filterSql.Where))
                //{
                //    sql += $" WHERE {filterSql.Where}";
                //    parameters = filterSql.Parameters;
                //}
                (string Where, DynamicParameters Parameters) = CreateFilterSqlWhere(filter);
                if (!string.IsNullOrEmpty(Where))
                {
                    sql += $" WHERE {Where}";
                    parameters = Parameters;
                }
            }
            if (!string.IsNullOrEmpty(sortExpression))
                sql += $" ORDER BY {sortExpression}";
            return new DapperCommand() { CommandText = sql, Parameters = parameters };
        }

        /// <summary>
        /// 取得实体列表合计数量
        /// </summary>
        /// <param name="filter">实体过滤器实例</param>
        /// <returns>合计数量</returns>
        public int GetListCount(TFilter filter = null)
        {
            return ProcessSelect(
                () => { return DoGetListCount(filter); },
                (log) =>
                {
                    log.Properties["filter"] = filter;
                }
            );
        }

        /// <summary>
        /// 取得实体列表合计数量
        /// </summary>
        /// <param name="filter">实体过滤器实例</param>
        /// <returns>合计数量</returns>
        protected virtual int DoGetListCount(TFilter filter)
        {
            string sql = $"SELECT COUNT(*) FROM {TableName}";
            DynamicParameters parameters = null;
            if (filter != null)
            {
                (string Where, DynamicParameters Parameters) = CreateFilterSqlWhere(filter);
                if (!string.IsNullOrEmpty(Where))
                {
                    sql += $" WHERE {Where}";
                    parameters = Parameters;
                }
            }
            DapperCommand dapperCommand = new DapperCommand()
            {
                CommandText = sql,
                Parameters = parameters
            };
            logger.Debug("dapper command:{command}", dapperCommand);
            IDbConnection connection = CreateConnection();
            return connection.ExecuteScalar<int>(dapperCommand);
        }

        /// <summary>
        /// 创建过滤器的sql语句及参数
        /// </summary>
        /// <param name="filter">过滤器实例</param>
        /// <returns>where条件及参数</returns>
        protected abstract (string, DynamicParameters) CreateFilterSqlWhere(TFilter filter);
        #endregion
    }

    /// <summary>
    /// 异步数据访问基类
    /// </summary>
    public abstract class DapperDataAccessAsync<TKey, TEntity, TFilter> : DapperDataAccess<TKey, TEntity, TFilter>
            where TEntity : class, new()
        where TFilter : class
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="providerName">提供程序名称</param>
        public DapperDataAccessAsync(string connectionString, string providerName)
            : base(connectionString, providerName)
        {

        }

        /// <summary>
        /// 异步执行操作
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="dataOperationType">数据操作类型</param>
        /// <param name="func">执行的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns></returns>
        protected async Task<TResult> ProcessAsync<TResult>(DataOperationType dataOperationType, Func<Task<TResult>> func, Action<LogEventInfo> action = null)
        {
            LogEventInfo log = new LogEventInfo() { LoggerName = logger.Name };
            log.Properties["method"] = "Process<TResult>(Func<TResult> func, IDictionary arguments, DataOperationType dataOperationType)";
            log.Properties["dataOperationType"] = dataOperationType;
            log.Properties["func.Target"] = func.Target?.ToString();
            log.Properties["func.Method.Name"] = func.Method.Name;
            action?.Invoke(log);
            try
            {
                TResult result = await func();
                log.Level = LogLevel.Debug;
                log.Message = "Process success.";
#if DEBUG
                log.Properties["returnValue"] = result;
#endif
                return result;
            }
            catch (Exception e)
            {
                DataAccessException dataAccessException = new DataAccessException("Process error.", e);
                log.Level = LogLevel.Warn;
                log.Exception = dataAccessException;
                log.Message = dataAccessException.Message;
                throw dataAccessException;
            }
            finally
            {
                logger.Log(log);
            }
        }

        /// <summary>
        /// 异步执行查询操作
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="func">查询的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns></returns>
        protected async Task<TResult> ProcessSelectAsync<TResult>(Func<Task<TResult>> func, Action<LogEventInfo> action = null)
        {
            return await ProcessAsync(DataOperationType.Select, func, action);
        }

        /// <summary>
        /// 异步执行插入操作
        /// </summary>
        /// <param name="func">插入的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns>响应的行数</returns>
        protected async Task<int> ProcessInsertAsync(Func<Task<int>> func, Action<LogEventInfo> action = null)
        {
            return await ProcessAsync(DataOperationType.Insert, func, action);
        }

        /// <summary>
        /// 异步执行更新操作
        /// </summary>
        /// <param name="func">更新的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns>响应的行数</returns>
        protected async Task<int> ProcessUpdateAsync(Func<Task<int>> func, Action<LogEventInfo> action = null)
        {
            return await ProcessAsync(DataOperationType.Update, func, action);
        }

        /// <summary>
        /// 异步执行删除操作
        /// </summary>
        /// <param name="func">删除的方法</param>
        /// <param name="action">添加日志参数的方法</param>
        /// <returns>响应的行数</returns>
        protected async Task<int> ProcessDelete(Func<Task<int>> func, Action<LogEventInfo> action = null)
        {
            return await ProcessAsync(DataOperationType.Delete, func, action);
        }
    }
}
