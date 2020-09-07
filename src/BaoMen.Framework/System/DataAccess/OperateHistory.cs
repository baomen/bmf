/*
Author: WangXinBin
CreateTime: 2019/10/23 12:23:16
*/

using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BaoMen.Common.Data;
using BaoMen.Common.Model;

namespace BaoMen.Framework.System.DataAccess
{
    /// <summary>
    /// 系统操作日志数据访问
    /// </summary>
    #region class OperateHistory (generated)
    public partial class OperateHistory : DapperDataAccess<int, Entity.OperateHistory, Entity.OperateHistoryFilter>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="providerName">数据提供程序名称</param>
        public OperateHistory(string connectionString, string providerName) : base(connectionString, providerName)
        {

        }

        /// <summary>
        /// 数据库表名
        /// </summary>
        protected override string TableName { get { return "sys_operate_history"; } }

        /// <summary>
        /// 取得插入数据的数据库命令
        /// </summary>
        /// <param name="item">实体数据</param>
        /// <returns></returns> 
        protected override DapperCommand CreateInsertCommand(Entity.OperateHistory item)
        {
            string sql = $"INSERT INTO {TableName} (UserId,Type,OperateTime,AssemblyName,EntityType,RelatedId,Value,Description) VALUES (@UserId,@Type,@OperateTime,@AssemblyName,@EntityType,@RelatedId,@Value,@Description)";
            return new DapperCommand()
            {
                CommandText = sql,
                Parameters = item
            };
        }

        /// <summary>
        /// 取得更新数据的数据库命令
        /// </summary>
        /// <param name="item">实体数据</param>
        /// <returns></returns>
        protected override DapperCommand CreateUpdateCommand(Entity.OperateHistory item)
        {
            string sql = $"UPDATE {TableName} SET UserId=@UserId,Type=@Type,OperateTime=@OperateTime,AssemblyName=@AssemblyName,EntityType=@EntityType,RelatedId=@RelatedId,Value=@Value,Description=@Description WHERE Id=@Id";
            return new DapperCommand()
            {
                CommandText = sql,
                Parameters = item
            };
        }

        /// <summary>
        /// 取得删除数据的数据库命令
        /// </summary>
        /// <param name="item">实体实例</param>
        /// <returns></returns>
        protected override DapperCommand CreateDeleteCommand(Entity.OperateHistory item)
        {
            string sql = $"DELETE FROM {TableName} WHERE Id=@Id";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("Id", item.Id);
            return new DapperCommand()
            {
                CommandText = sql,
                Parameters = dynamicParameters
            };
        }

        /// <summary>
        /// 创建读取单条数据的数据库命令
        /// </summary>
        /// <param name="id">实体标识</param>
        /// <returns>数据库命令</returns>
        protected override DapperCommand CreateGetCommand(int id)
        {
            string sql = $"SELECT {TableName}.Id,{TableName}.UserId,{TableName}.Type,{TableName}.OperateTime,{TableName}.AssemblyName,{TableName}.EntityType,{TableName}.RelatedId,{TableName}.Value,{TableName}.Description FROM {TableName}";
            sql += " WHERE Id=@Id";
            return new DapperCommand()
            {
                CommandText = sql,
                Parameters = new { Id = id }
            };
        }

        /// <summary>
        /// 创建过滤器的sql语句及参数
        /// </summary>
        /// <param name="filter">过滤器实例</param>
        /// <returns>where条件及参数</returns>
        protected override (string, DynamicParameters) CreateFilterSqlWhere(Entity.OperateHistoryFilter filter)
        {
            DynamicParameters parameter = new DynamicParameters();
            StringBuilder stringBuilder = new StringBuilder();
            AddParameter(stringBuilder, "Id", "Id", filter.Id, parameter);
            AddParameter(stringBuilder, "UserId", "UserId", filter.UserId, parameter);
            AddParameter(stringBuilder, "Type", "Type", filter.Type, parameter);
            AddParameter(stringBuilder, "OperateTime", "OperateTimeMin", filter.OperateTimeMin, parameter);
            AddParameter(stringBuilder, "OperateTime", "OperateTimeMax", filter.OperateTimeMax, parameter);
            AddParameter(stringBuilder, "AssemblyName", "AssemblyName", filter.AssemblyName, parameter);
            AddParameter(stringBuilder, "EntityType", "EntityType", filter.EntityType, parameter);
            AddParameter(stringBuilder, "RelatedId", "RelatedId", filter.RelatedId, parameter);
            AddParameter(stringBuilder, "Value", "Value", filter.Value, parameter);
            AddParameter(stringBuilder, "Description", "Description", filter.Description, parameter);

            RemoveSqlConditionPrefix(stringBuilder);
            return (stringBuilder.ToString(), parameter);
        }
    }
    #endregion
}