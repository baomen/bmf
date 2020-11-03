using BaoMen.Common.Model;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;

namespace BaoMen.Common.Extension
{
    //public static partial class DapperExtension
    //{
    //    public static IEnumerable<TReturn> Query<TReturn>(this IDbConnection connection, int startRowIndex, int maximumRows, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
    //    {
    //        System.Type connectionType = connection.GetType();
    //        if (connectionType == typeof(MySql.Data.MySqlClient.MySqlConnection))
    //        {
    //            sql = GetMySqlPageSql(sql, startRowIndex, maximumRows);
    //            return connection.Query<TReturn>(sql, param, transaction, buffered, commandTimeout, commandType);
    //        }
    //        else
    //        {
    //            throw new System.NotSupportedException($"not supported database type : {connectionType.FullName}");
    //        }
    //    }

    //    public static IEnumerable<TReturn> Query<TReturn>(this IDbConnection connection, int startRowIndex, int maximumRows, CommandDefinition command)
    //    {
    //        System.Type connectionType = connection.GetType();
    //        if (connectionType == typeof(MySql.Data.MySqlClient.MySqlConnection))
    //        {
    //            string sql = GetMySqlPageSql(command.CommandText, startRowIndex, maximumRows);
    //            CommandDefinition newCommand = new CommandDefinition(sql, command.Parameters, command.Transaction, command.CommandTimeout, command.CommandType, command.Flags, command.CancellationToken);
    //            return connection.Query<TReturn>(newCommand);
    //        }
    //        else
    //        {
    //            throw new System.NotSupportedException($"not supported database type : {connectionType.FullName}");
    //        }
    //    }

    //    public static IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(this IDbConnection connection, int startRowIndex, int maximumRows, CommandDefinition command, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id")
    //    {
    //        System.Type connectionType = connection.GetType();
    //        if (connectionType == typeof(MySql.Data.MySqlClient.MySqlConnection))
    //        {
    //            string sql = GetMySqlPageSql(command.CommandText, startRowIndex, maximumRows);
    //            CommandDefinition newCommand = new CommandDefinition(sql, command.Parameters, command.Transaction, command.CommandTimeout, command.CommandType, command.Flags, command.CancellationToken);
    //            return connection.Query<TFirst, TSecond, TReturn>(sql, map, command.Parameters, command.Transaction, command.Buffered, splitOn, command.CommandTimeout, command.CommandType);
    //        }
    //        else
    //        {
    //            throw new System.NotSupportedException($"not supported database type : {connectionType.FullName}");
    //        }
    //    }

    //    public static IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(this IDbConnection connection, CommandDefinition command, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id")
    //    {
    //        return connection.Query<TFirst, TSecond, TReturn>(command.CommandText, map, command.Parameters, command.Transaction, command.Buffered, splitOn, command.CommandTimeout, command.CommandType);
    //    }
    //}

    /// <summary>
    /// Dapper扩展
    /// </summary>
    public static partial class DapperExtension
    {
        /// <summary>
        /// 日志实例
        /// </summary>
        public static NLog.ILogger logger = NLog.LogManager.GetCurrentClassLogger();

        #region Execute

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="command">DapperCommand实例</param>
        /// <returns></returns>
        public static int Execute(this IDbConnection connection, DapperCommand command)
        {
            CheckDapperCommand(command);
            CommandDefinition commandDefinition = CreateCommandDefine(command);
            return connection.Execute(commandDefinition);
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。 忽略其他列或行。
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="command">DapperCommand实例</param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(this IDbConnection connection, DapperCommand command)
        {
            CheckDapperCommand(command);
            CommandDefinition commandDefinition = CreateCommandDefine(command);
            return connection.ExecuteScalar<T>(commandDefinition);
        }
        #endregion

        #region Query

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="command">DapperCommand实例</param>
        /// <returns></returns>
        public static T QuerySingle<T>(this IDbConnection connection, DapperCommand command)
        {
            CheckDapperCommand(command);
            CommandDefinition commandDefinition = CreateCommandDefine(command);
            return connection.QuerySingle<T>(commandDefinition);
        }

        /// <summary>
        /// 查询单条数据或默认值
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="command">DapperCommand实例</param>
        /// <returns></returns>
        public static T QuerySingleOrDefault<T>(this IDbConnection connection, DapperCommand command)
        {
            CheckDapperCommand(command);
            CommandDefinition commandDefinition = CreateCommandDefine(command);
            return connection.QuerySingleOrDefault<T>(commandDefinition);
        }

        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="command">DapperCommand实例</param>
        /// <returns></returns>
        public static T QueryFirst<T>(this IDbConnection connection, DapperCommand command)
        {
            CheckDapperCommand(command);
            CommandDefinition commandDefinition = CreateCommandDefine(command);
            return connection.QueryFirst<T>(commandDefinition);
        }

        /// <summary>
        /// 查询第一条数据或默认值
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="command">DapperCommand实例</param>
        /// <returns></returns>
        public static T QueryFirstOrDefault<T>(this IDbConnection connection, DapperCommand command)
        {
            CheckDapperCommand(command);
            CommandDefinition commandDefinition = CreateCommandDefine(command);
            return connection.QueryFirstOrDefault<T>(commandDefinition);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="TReturn">返回的类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="command">DapperCommand实例</param>
        /// <returns></returns>
        public static IEnumerable<TReturn> Query<TReturn>(this IDbConnection connection, DapperCommand command)
        {
            CheckDapperCommand(command);
            CommandDefinition commandDefinition = CreateCommandDefine(command);
            return connection.Query<TReturn>(commandDefinition);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="TReturn">返回的类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="command">DapperCommand实例</param>
        /// <param name="startRowIndex">从0开始的索引</param>
        /// <param name="maximumRows">最大的行数</param>
        /// <returns></returns>
        public static IEnumerable<TReturn> Query<TReturn>(this IDbConnection connection, DapperCommand command, int startRowIndex = 0, int maximumRows = int.MaxValue)
        {
            CheckDapperCommand(command);
            PreparePageCommand(connection, command, startRowIndex, maximumRows);
            CommandDefinition commandDefinition = CreateCommandDefine(command);
            return connection.Query<TReturn>(commandDefinition);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="TReturn">返回的类型</typeparam>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="command">DapperCommand实例</param>
        /// <param name="map">map委托</param>
        /// <param name="splitOn">分割的字段名称</param>
        /// <param name="startRowIndex">从0开始的索引</param>
        /// <param name="maximumRows">最大的行数</param>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(this IDbConnection connection, DapperCommand command, Func<TFirst, TSecond, TReturn> map, string splitOn, int startRowIndex = 0, int maximumRows = int.MaxValue)
        {
            CheckDapperCommand(command);
            PreparePageCommand(connection, command, startRowIndex, maximumRows);
            bool buffered = (command.Flags & CommandFlags.Buffered) != 0;
            return connection.Query<TFirst, TSecond, TReturn>(
                sql: command.CommandText,
                map: map,
                param: command.Parameters,
                transaction: command.Transaction,
                buffered: buffered,
                splitOn: splitOn,
                commandTimeout: command.CommandTimeout,
                commandType: command.CommandType);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="TReturn">返回的类型</typeparam>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="command">DapperCommand实例</param>
        /// <param name="map">map委托</param>
        /// <param name="splitOn">分割的字段名称</param>
        /// <param name="startRowIndex">从0开始的索引</param>
        /// <param name="maximumRows">最大的行数</param>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(this IDbConnection connection, DapperCommand command, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn, int startRowIndex = 0, int maximumRows = int.MaxValue)
        {
            CheckDapperCommand(command);
            PreparePageCommand(connection, command, startRowIndex, maximumRows);
            bool buffered = (command.Flags & CommandFlags.Buffered) != 0;
            return connection.Query(
                sql: command.CommandText,
                map: map,
                param: command.Parameters,
                transaction: command.Transaction,
                buffered: buffered,
                splitOn: splitOn,
                commandTimeout: command.CommandTimeout,
                commandType: command.CommandType);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="TReturn">返回的类型</typeparam>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <typeparam name="TFourth">第四个实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="command">DapperCommand实例</param>
        /// <param name="map">map委托</param>
        /// <param name="splitOn">分割的字段名称</param>
        /// <param name="startRowIndex">从0开始的索引</param>
        /// <param name="maximumRows">最大的行数</param>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(this IDbConnection connection, DapperCommand command, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, string splitOn, int startRowIndex = 0, int maximumRows = int.MaxValue)
        {
            CheckDapperCommand(command);
            PreparePageCommand(connection, command, startRowIndex, maximumRows);
            bool buffered = (command.Flags & CommandFlags.Buffered) != 0;
            return connection.Query(
                sql: command.CommandText,
                map: map,
                param: command.Parameters,
                transaction: command.Transaction,
                buffered: buffered,
                splitOn: splitOn,
                commandTimeout: command.CommandTimeout,
                commandType: command.CommandType);
        }
        #endregion


        private static void CheckDapperCommand(DapperCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (string.IsNullOrEmpty(command.CommandText)) throw new ArgumentNullException("command.CommandText");
        }

        private static void PreparePageCommand(IDbConnection connection, DapperCommand command, int startRowIndex, int maximumRows)
        {
            if (startRowIndex == 0 && maximumRows == int.MaxValue) return;
            switch (connection)
            {
                case MySqlConnection _:
                    PrepareMySqlPageCommand(command, startRowIndex, maximumRows);
                    break;
                default:
                    throw new System.NotSupportedException($"not supported database type : {connection.GetType().FullName}");
            }
        }

        private static void PrepareMySqlPageCommand(DapperCommand command, int startRowIndex, int maximumRows)
        {
            command.CommandText = $"{command.CommandText} LIMIT {startRowIndex},{maximumRows}";
        }

        /// <summary>
        /// 获取int类型的标识
        /// </summary>
        /// <param name="connection">数据库链接</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns></returns>
        public static int GetIntIdentity(this IDbConnection connection, IDbTransaction transaction)
        {
            return connection switch
            {
                //case MySql.Data.MySqlClient.MySqlConnection conn:
                MySqlConnector.MySqlConnection conn => conn.ExecuteScalar<int>(sql: "SELECT @@IDENTITY", transaction: transaction),
                System.Data.SqlClient.SqlConnection conn => conn.ExecuteScalar<int>(sql: "select @@identity", transaction: transaction),
                _ => throw new System.NotSupportedException($"not supported database type : {connection.GetType().FullName}"),
            };
        }

        /// <summary>
        /// 获取long类型的标识
        /// </summary>
        /// <param name="connection">数据库链接</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns></returns>
        public static long GetLongIdentity(this IDbConnection connection, IDbTransaction transaction)
        {
            return connection switch
            {
                //case MySql.Data.MySqlClient.MySqlConnection conn:
                MySqlConnector.MySqlConnection conn => conn.ExecuteScalar<long>(sql: "SELECT @@IDENTITY", transaction: transaction),
                System.Data.SqlClient.SqlConnection conn => conn.ExecuteScalar<long>(sql: "select @@identity", transaction: transaction),
                _ => throw new System.NotSupportedException($"not supported database type : {connection.GetType().FullName}"),
            };
        }

        /// <summary>
        /// 创建CommandDefinition实例
        /// </summary>
        /// <param name="dapperCommand"></param>
        /// <returns></returns>
        public static CommandDefinition CreateCommandDefine(DapperCommand dapperCommand)
        {
            return new CommandDefinition(
                    dapperCommand.CommandText,
                    dapperCommand.Parameters,
                    dapperCommand.Transaction,
                    dapperCommand.CommandTimeout,
                    dapperCommand.CommandType,
                    dapperCommand.Flags,
                    dapperCommand.CancellationToken);
        }
    }
}
