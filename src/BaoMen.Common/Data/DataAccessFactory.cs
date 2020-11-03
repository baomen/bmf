using Microsoft.Extensions.Configuration;
using NLog;
using System;

namespace BaoMen.Common.Data
{
    /// <summary>
    /// 泛型数据访问层工厂
    /// </summary>
    public static class DataAccessFactory
    {
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// 创建一个数据访问层实例
        /// </summary>
        /// <returns>数据访问层实例</returns>
        public static TDataAccess Create<TDataAccess>(IConfiguration configuration)
        {
            LogEventInfo log = new LogEventInfo() { LoggerName = logger.Name };
            log.Properties["method"] = "DataAccessFactory.Create()";
            log.Properties["TDataAccess"] = typeof(TDataAccess).ToString();
            try
            {
                log.Level = LogLevel.Debug;
                log.Message = "create DataAccess instance success";
                Type dataAccessType = typeof(TDataAccess);
                string assemblyName = dataAccessType.Assembly.GetName().Name;
                string connectionStringName = configuration.GetSection($"ApplicationConfiguration:Assemblies:{assemblyName}")["DefaultConnectionString"];
                if (string.IsNullOrEmpty(connectionStringName))
                    connectionStringName = configuration.GetSection("DatabaseConfiguration")["DefaultConnectionString"];
                string connectionString = configuration.GetSection($"DatabaseConfiguration:ConnectionStrings:{connectionStringName}")["ConnectionString"];
                //DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(configuration.GetSection($"DatabaseConfiguration:ConnectionStrings:{connectionStringName}")["ProviderName"]);
                return (TDataAccess)Activator.CreateInstance(dataAccessType, connectionString, configuration.GetSection($"DatabaseConfiguration:ConnectionStrings:{connectionStringName}")["ProviderName"]);
            }
            catch (Exception exception)
            {
                log.Level = LogLevel.Warn;
                log.Exception = exception;
                log.Message = "create DataAccess instance error";
                throw exception;
            }
            finally
            {
                logger.Log(log);
            }
        }

        ///// <summary>
        ///// 创建一个数据访问层实例
        ///// </summary>
        ///// <returns>数据访问层实例</returns>
        //public static TDataAccess Create<TEntity, TFilter, TDataAccess>(IConfiguration configuration)
        //    where TFilter : class
        //    where TEntity : class, new()
        //    //where TDataAccess : IDataAccess<TKey, TEntity, TFilter>
        //{
        //    LogEventInfo log = new LogEventInfo() { LoggerName = logger.Name };
        //    log.Properties["method"] = "DataAccessFactory.Create()";
        //    log.Properties["TFilter"] = typeof(TFilter).ToString();
        //    log.Properties["TEntity"] = typeof(TEntity).ToString();
        //    log.Properties["TDataAccess"] = typeof(TDataAccess).ToString();
        //    try
        //    {
        //        log.Level = LogLevel.Debug;
        //        log.Message = "create DataAccess instance success";
        //        Type dataAccessType = typeof(TDataAccess);
        //        string assemblyName = dataAccessType.Assembly.GetName().Name;
        //        string connectionStringName = configuration.GetSection($"ApplicationConfiguration:Assemblies:{assemblyName}")["DefaultConnectionString"];
        //        if (string.IsNullOrEmpty(connectionStringName))
        //            connectionStringName = configuration.GetSection("DatabaseConfiguration")["DefaultConnectionString"];
        //        string connectionString = configuration.GetSection($"DatabaseConfiguration:ConnectionStrings:{connectionStringName}")["ConnectionString"];
        //        //DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(configuration.GetSection($"DatabaseConfiguration:ConnectionStrings:{connectionStringName}")["ProviderName"]);
        //        return (TDataAccess)Activator.CreateInstance(dataAccessType, connectionString, configuration.GetSection($"DatabaseConfiguration:ConnectionStrings:{connectionStringName}")["ProviderName"]);
        //    }
        //    catch (Exception exception)
        //    {
        //        log.Level = LogLevel.Warn;
        //        log.Exception = exception;
        //        log.Message = "create DataAccess instance error";
        //        throw exception;
        //    }
        //    finally
        //    {
        //        logger.Log(log);
        //    }
        //}

        /// <summary>
        /// 创建一个数据访问层实例
        /// </summary>
        /// <returns>数据访问层实例</returns>
        public static DataAccess CreateRedis<DataAccess>(IConfiguration configuration)
        {
            LogEventInfo log = new LogEventInfo() { LoggerName = logger.Name };
            log.Properties["method"] = "DataAccessFactory.Create()";
            log.Properties["DataAccess"] = typeof(DataAccess).ToString();
            try
            {
                log.Level = LogLevel.Debug;
                log.Message = "create DataAccess instance success";

                Type dataAccessType = typeof(DataAccess);
                string assemblyName = dataAccessType.Assembly.GetName().Name;
                string connectionStringName = configuration.GetSection($"ApplicationConfiguration:Assemblies:{assemblyName}")["DefaultConnectionString"];
                if (string.IsNullOrEmpty(connectionStringName))
                    connectionStringName = configuration.GetSection("RedisConfiguration")["DefaultConnectionString"];
                string connectionString = configuration.GetSection($"RedisConfiguration:ConnectionStrings:{connectionStringName}")["ConnectionString"];
                //DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(configuration.GetSection($"DatabaseConfiguration:ConnectionStrings:{connectionStringName}")["ProviderName"]);
                return (DataAccess)Activator.CreateInstance(dataAccessType, connectionString);
            }
            catch (Exception exception)
            {
                log.Level = LogLevel.Warn;
                log.Exception = exception;
                log.Message = "create DataAccess instance error";
                throw exception;
            }
            finally
            {
                logger.Log(log);
            }
        }
    }
}
