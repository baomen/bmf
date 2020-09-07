/*
Author: WangXinBin
CreateTime: 2020/1/10 14:14:24
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
    /// 系统上传文件数据访问
    /// </summary>
	#region class UploadFile (generated)
    public partial class UploadFile : DapperDataAccess<int,Entity.UploadFile, Entity.UploadFileFilter>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="providerName">数据提供程序名称</param>
        public UploadFile(string connectionString, string providerName) : base(connectionString, providerName)
        {

        }
               
        /// <summary>
        /// 数据库表名
        /// </summary>
        protected override string TableName { get { return "sys_upload_file"; } }
               
        /// <summary>
        /// 取得插入数据的数据库命令
        /// </summary>
        /// <param name="item">实体数据</param>
        /// <returns></returns> 
        protected override DapperCommand CreateInsertCommand(Entity.UploadFile item)
        {
            string sql = $"INSERT INTO {TableName} (Type,OriginalFileName,FileName,ExtentionName,RelativePath,CreateTime,CreateUserId,RelatedId) VALUES (@Type,@OriginalFileName,@FileName,@ExtentionName,@RelativePath,@CreateTime,@CreateUserId,@RelatedId)";
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
        protected override DapperCommand CreateUpdateCommand(Entity.UploadFile item)
        {
            string sql = $"UPDATE {TableName} SET Type=@Type,OriginalFileName=@OriginalFileName,FileName=@FileName,ExtentionName=@ExtentionName,RelativePath=@RelativePath,CreateUserId=@CreateUserId,RelatedId=@RelatedId WHERE Id=@Id";
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
        protected override DapperCommand CreateDeleteCommand(Entity.UploadFile item)
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
            string sql = $"SELECT {TableName}.Id,{TableName}.Type,{TableName}.OriginalFileName,{TableName}.FileName,{TableName}.ExtentionName,{TableName}.RelativePath,{TableName}.CreateTime,{TableName}.CreateUserId,{TableName}.RelatedId FROM {TableName}";
            sql += " WHERE Id=@Id";
            return new DapperCommand()
            {
                CommandText = sql,
                Parameters = new {Id=id}
            };
        }
        
        /// <summary>
        /// 创建过滤器的sql语句及参数
        /// </summary>
        /// <param name="filter">过滤器实例</param>
        /// <returns>where条件及参数</returns>
        protected override (string, DynamicParameters) CreateFilterSqlWhere(Entity.UploadFileFilter filter)
        {
            DynamicParameters parameter = new DynamicParameters();
            StringBuilder stringBuilder = new StringBuilder();
            		AddParameter(stringBuilder, "Id", "Id", filter.Id, parameter);
		AddParameter(stringBuilder, "Type", "Type", filter.Type, parameter);
		AddParameter(stringBuilder, "OriginalFileName", "OriginalFileName", filter.OriginalFileName, parameter);
		AddParameter(stringBuilder, "FileName", "FileName", filter.FileName, parameter);
		AddParameter(stringBuilder, "ExtentionName", "ExtentionName", filter.ExtentionName, parameter);
		AddParameter(stringBuilder, "RelativePath", "RelativePath", filter.RelativePath, parameter);
		AddParameter(stringBuilder, "CreateTime", "CreateTimeMin", filter.CreateTimeMin, parameter);
		AddParameter(stringBuilder, "CreateTime", "CreateTimeMax", filter.CreateTimeMax, parameter);
		AddParameter(stringBuilder, "CreateUserId", "CreateUserId", filter.CreateUserId, parameter);
		AddParameter(stringBuilder, "RelatedId", "RelatedId", filter.RelatedId, parameter);

            RemoveSqlConditionPrefix(stringBuilder);
            return (stringBuilder.ToString(), parameter);
        }
    }
	#endregion

    public partial class UploadFile
    {
        /// <summary>
        /// 重写方法，将数据库生成的ID赋值给实例
        /// </summary>
        /// <param name="item">实体</param>
        /// <param name="transaction"></param>
        /// <param name="getIdentity"></param>
        /// <returns></returns>
        protected override int DoInsert(Entity.UploadFile item, IDbTransaction transaction, Action<IDbConnection, IDbTransaction> getIdentity = null)
        {
            return base.DoInsert(item, transaction, (conn, trans) =>
            {
                item.Id = conn.GetIntIdentity(trans);
            });
        }
    }
}