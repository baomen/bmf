/*
Author: WangXinBin
CreateTime: 2019/10/10 11:42:56
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
using BaoMen.Common.Extension;

namespace BaoMen.Framework.System.DataAccess
{
    /// <summary>
    /// 系统模块数据访问
    /// </summary>
    #region class Module (generated)
    public partial class Module : DapperDataAccess<string, Entity.Module, Entity.ModuleFilter>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="providerName">数据提供程序名称</param>
        public Module(string connectionString, string providerName) : base(connectionString, providerName)
        {

        }

        /// <summary>
        /// 数据库表名
        /// </summary>
        protected override string TableName { get { return "sys_module"; } }

        /// <summary>
        /// 取得更新数据的数据库命令
        /// </summary>
        /// <param name="item">实体数据</param>
        /// <returns></returns>
        protected override DapperCommand CreateUpdateCommand(Entity.Module item)
        {
            string sql = $"UPDATE {TableName} SET ParentId=@ParentId,Name=@Name,VisibleIndex=@VisibleIndex,Type=@Type,Method=@Method,WorkflowActivityId=@WorkflowActivityId,IsNode=@IsNode,Status=@Status,Description=@Description WHERE Id=@Id";
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
        protected override DapperCommand CreateDeleteCommand(Entity.Module item)
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
        protected override DapperCommand CreateGetCommand(string id)
        {
            string sql = $"SELECT {TableName}.Id,{TableName}.ParentId,{TableName}.Name,{TableName}.VisibleIndex,{TableName}.Type,{TableName}.Method,{TableName}.WorkflowActivityId,{TableName}.IsNode,{TableName}.Status,{TableName}.Description FROM {TableName}";
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
        protected override (string, DynamicParameters) CreateFilterSqlWhere(Entity.ModuleFilter filter)
        {
            DynamicParameters parameter = new DynamicParameters();
            StringBuilder stringBuilder = new StringBuilder();
            AddParameter(stringBuilder, $"{TableName}.Id", "Id", filter.Id, parameter);
            AddParameter(stringBuilder, $"{TableName}.ParentId", "ParentId", filter.ParentId, parameter);
            AddParameter(stringBuilder, $"{TableName}.Name", "Name", filter.Name, parameter);
            AddParameter(stringBuilder, $"{TableName}.VisibleIndex", "VisibleIndex", filter.VisibleIndex, parameter);
            AddParameter(stringBuilder, $"{TableName}.Type", "Type", filter.Type, parameter);
            AddParameter(stringBuilder, $"{TableName}.Method", "Method", filter.Method, parameter);
            AddParameter(stringBuilder, $"{TableName}.WorkflowActivityId", "WorkflowActivityId", filter.WorkflowActivityId, parameter);
            AddParameter(stringBuilder, $"{TableName}.IsNode", "IsNode", filter.IsNode, parameter);
            AddParameter(stringBuilder, $"{TableName}.Status", "Status", filter.Status, parameter);
            AddParameter(stringBuilder, $"{TableName}.Description", "Description", filter.Description, parameter);

            RemoveSqlConditionPrefix(stringBuilder);
            return (stringBuilder.ToString(), parameter);
        }
    }
    #endregion

    public partial class Module
    {
        /// <summary>
        /// 取得插入数据的数据库命令
        /// </summary>
        /// <param name="item">实体数据</param>
        /// <returns></returns> 
        protected override DapperCommand CreateInsertCommand(Entity.Module item)
        {
            // 允许自定义ID
            if(string.IsNullOrEmpty(item.Id) || item.Id.Length != 32){
                item.Id = CreateId();
            }
            string sql = $"INSERT INTO {TableName} (Id,ParentId,Name,VisibleIndex,Type,Method,WorkflowActivityId,IsNode,Status,Description) VALUES (@Id,@ParentId,@Name,@VisibleIndex,@Type,@Method,@WorkflowActivityId,@IsNode,@Status,@Description)";
            return new DapperCommand()
            {
                CommandText = sql,
                Parameters = item
            };
        }

        /// <summary>
        /// 更新IsNode字段
        /// </summary>
        /// <param name="id">模块ID</param>
        /// <param name="isNode">IsNode字段的值</param>
        /// <param name="tranaction">数据库事物</param>
        /// <returns></returns>
        internal int UpdateNode(string id, int isNode, IDbTransaction tranaction)
        {
            return ProcessUpdate(() =>
            {
                string sql = $"update {TableName} set IsNode=@IsNode where Id=@Id";
                DapperCommand command = new DapperCommand
                {
                    Transaction = tranaction,
                    CommandText = sql,
                    Parameters = new
                    {
                        IsNode = isNode,
                        Id = id
                    }
                };
                return tranaction.Connection.Execute(command);
            },
            (log) =>
            {
                log.Properties["id"] = id;
                log.Properties["isNode"] = isNode;
            });
        }
    }
}