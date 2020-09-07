/*
Author: WangXinBin
CreateTime: 2019/9/23 14:27:06
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
    /// 系统参数数据访问
    /// </summary>
    #region class Parameter (generated)
    public partial class Parameter : DapperDataAccess<string, Entity.Parameter, Entity.ParameterFilter>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="providerName">数据提供程序名称</param>
        public Parameter(string connectionString, string providerName) : base(connectionString, providerName)
        {

        }

        /// <summary>
        /// 数据库表名
        /// </summary>
        protected override string TableName { get { return "sys_parameter"; } }

        /// <summary>
        /// 取得插入数据的数据库命令
        /// </summary>
        /// <param name="item">实体数据</param>
        /// <returns></returns> 
        protected override DapperCommand CreateInsertCommand(Entity.Parameter item)
        {
            string sql = $"INSERT INTO {TableName} (Id,ParentId,Name,Value,IsNode,Description) VALUES (@Id,@ParentId,@Name,@Value,@IsNode,@Description)";
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
        protected override DapperCommand CreateUpdateCommand(Entity.Parameter item)
        {
            string sql = $"UPDATE {TableName} SET ParentId=@ParentId,Name=@Name,Value=@Value,IsNode=@IsNode,Description=@Description WHERE Id=@Id";
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
        protected override DapperCommand CreateDeleteCommand(Entity.Parameter item)
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
            string sql = $"SELECT {TableName}.Id,{TableName}.ParentId,{TableName}.Name,{TableName}.Value,{TableName}.IsNode,{TableName}.Description FROM {TableName}";
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
        protected override (string, DynamicParameters) CreateFilterSqlWhere(Entity.ParameterFilter filter)
        {
            DynamicParameters parameter = new DynamicParameters();
            StringBuilder stringBuilder = new StringBuilder();
            AddParameter(stringBuilder, "Id", "Id", filter.Id, parameter);
            AddParameter(stringBuilder, "ParentId", "ParentId", filter.ParentId, parameter);
            AddParameter(stringBuilder, "Name", "Name", filter.Name, parameter);
            AddParameter(stringBuilder, "Value", "Value", filter.Value, parameter);
            AddParameter(stringBuilder, "IsNode", "IsNode", filter.IsNode, parameter);
            AddParameter(stringBuilder, "Description", "Description", filter.Description, parameter);

            RemoveSqlConditionPrefix(stringBuilder);
            return (stringBuilder.ToString(), parameter);
        }
    }
    #endregion

    public partial class Parameter
    {
        /// <summary>
        /// 更新节点信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isNode">是否是节点</param>
        /// <param name="tranaction">数据库事务</param>
        /// <returns></returns>
        public int UpdateNode(string id, int isNode, IDbTransaction tranaction)
        {
            return ProcessUpdate(() =>
            {
                string sql = $"update {TableName} set IsNode=@IsNode where Id=@Id";
                DapperCommand dapperCommand = new DapperCommand
                {
                    CommandText = sql,
                    Parameters = new { IsNode = isNode, Id = id },
                    Transaction = tranaction
                };
                return tranaction.Connection.Execute(dapperCommand);
            },
            (log) =>
            {
                log.Properties["id"] = id;
                log.Properties["isNode"] = isNode;
            });
        }
    }
}