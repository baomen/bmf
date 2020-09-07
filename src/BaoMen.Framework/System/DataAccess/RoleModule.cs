/*
Author: WangXinBin
CreateTime: 2019/9/23 14:38:40
*/

using Dapper;
using BaoMen.Common.Data;
using BaoMen.Common.Extension;
using BaoMen.Common.Model;
using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace BaoMen.Framework.System.DataAccess
{
    /// <summary>
    /// 角色模块数据访问
    /// </summary>
    #region class RoleModule (generated)
    public partial class RoleModule : DapperDataAccess<Tuple<string, string>, Entity.RoleModule, Entity.RoleModuleFilter>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="providerName">数据提供程序名称</param>
        public RoleModule(string connectionString, string providerName) : base(connectionString, providerName)
        {

        }

        /// <summary>
        /// 数据库表名
        /// </summary>
        protected override string TableName { get { return "sys_role_module"; } }

        /// <summary>
        /// 取得插入数据的数据库命令
        /// </summary>
        /// <param name="item">实体数据</param>
        /// <returns></returns> 
        protected override DapperCommand CreateInsertCommand(Entity.RoleModule item)
        {
            string sql = $"INSERT INTO {TableName} (RoleId,ModuleId) VALUES (@RoleId,@ModuleId)";
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
        protected override DapperCommand CreateUpdateCommand(Entity.RoleModule item)
        {
            string sql = $"UPDATE {TableName} SET WHERE RoleId=@RoleId And ModuleId=@ModuleId";
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
        protected override DapperCommand CreateDeleteCommand(Entity.RoleModule item)
        {
            string sql = $"DELETE FROM {TableName} WHERE RoleId=@RoleId And ModuleId=@ModuleId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("RoleId", item.RoleId);
            dynamicParameters.Add("ModuleId", item.ModuleId);
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
        protected override DapperCommand CreateGetCommand(Tuple<string, string> id)
        {
            if (id == null)
                return null;
            string sql = $"SELECT {TableName}.RoleId,{TableName}.ModuleId FROM {TableName}";
            sql += " WHERE RoleId=@RoleId And ModuleId=@ModuleId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("RoleId", id.Item1);
            dynamicParameters.Add("ModuleId", id.Item2);
            return new DapperCommand()
            {
                CommandText = sql,
                Parameters = dynamicParameters
            };
        }

        /// <summary>
        /// 创建过滤器的sql语句及参数
        /// </summary>
        /// <param name="filter">过滤器实例</param>
        /// <returns>where条件及参数</returns>
        protected override (string, DynamicParameters) CreateFilterSqlWhere(Entity.RoleModuleFilter filter)
        {
            DynamicParameters parameter = new DynamicParameters();
            StringBuilder stringBuilder = new StringBuilder();
            AddParameter(stringBuilder, "RoleId", "RoleId", filter.RoleId, parameter);
            AddParameter(stringBuilder, "ModuleId", "ModuleId", filter.ModuleId, parameter);

            RemoveSqlConditionPrefix(stringBuilder);
            return (stringBuilder.ToString(), parameter);
        }
    }
    #endregion

    public partial class RoleModule
    {
      
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="roleModules"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        internal int Insert(IList<Entity.RoleModule> roleModules, IDbTransaction transaction)
        {
            return ProcessInsert(() =>
            {
                string sql = $"INSERT INTO {TableName} (RoleId,ModuleId) VALUES ";
                for (int i = 0; i < roleModules.Count; i++)
                {
                    if (i == 0)
                    {
                        sql += $"(\"{roleModules[i].RoleId}\",\"{roleModules[i].ModuleId}\")";
                    }
                    else
                        sql += $",(\"{roleModules[i].RoleId}\",\"{roleModules[i].ModuleId}\")";

                }
                DapperCommand command = new DapperCommand
                {
                 CommandText = sql,
                 Transaction = transaction
                };
                return transaction.Connection.Execute(command);

            },
            (log) =>
            {
                log.Properties["roleModules"] = roleModules;
            });
        }

        internal int DeleteByRoleId(string roleId, IDbTransaction transaction)
        {
            return ProcessDelete(() =>
            {
                string sql = $"delete from {TableName} where RoleId=@RoleId";
                DapperCommand command = new DapperCommand
                {
                    CommandText = sql,
                    Parameters = new
                    {
                        RoleId = roleId
                    },
                    Transaction = transaction
                };
                return transaction.Connection.Execute(command);
            },
            (log) =>
            {
                log.Properties["roleId"] = roleId;
            });
        }

        internal int DeleteByModuleId(string moduleId, IDbTransaction transaction)
        {
            return ProcessDelete(() =>
            {
                string sql = $"delete from {TableName} where ModuleId=@ModuleId";
                DapperCommand command = new DapperCommand
                {
                    CommandText = sql,
                    Parameters = new
                    {
                        ModuleId = moduleId
                    },
                    Transaction = transaction
                };
                return transaction.Connection.Execute(command);
            },
            (log) =>
            {
                log.Properties["moduleId"] = moduleId;
            });
        }
    }
}