/*
Author: WangXinBin
CreateTime: 2020/1/10 14:14:24
*/

using BaoMen.Framework.System.Entity;
using BaoMen.Common.Data;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace BaoMen.Framework.System.BusinessLogic
{
    #region interface IUploadFileManager (generated)
    /// <summary>
    /// 系统上传文件业务逻辑接口
    /// </summary>
    public interface IUploadFileManager : IBusinessLogic<int,UploadFile,UploadFileFilter>
    {
        /// <summary>
        /// 获取上传文件相对路径
        /// </summary>
        /// <returns></returns>
        string GetRelativePath();
        /// <summary>
        /// 获取上传文件的目录路径
        /// </summary>
        /// <returns></returns>
        string GetUploadPath();

        /// <summary>
        /// 创建上传文件实例
        /// </summary>
        /// <param name="createUserId">创建人</param>
        /// <param name="relatedId">关联ID</param>
        /// <param name="type">类型</param>
        /// <param name="originalFileName">原始文件名</param>
        /// <returns></returns>
        UploadFile CreateUploadFile(string createUserId, string relatedId, int type, string originalFileName);

        /// <summary>
        /// 保存上传文件
        /// </summary>
        /// <param name="path">文件物理路径</param>
        /// <param name="file">文件流</param>
        /// <returns></returns>
        void SaveFile(string path, IFormFile file);

        /// <summary>
        /// 根据相对路径获取绝对路径
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        string GetPhysicalPath(string relativePath);

        /// <summary>
        /// 插入上传文件实例
        /// </summary>
        /// <param name="item">上传文件实例</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns></returns>
        int Insert(Entity.UploadFile item, IDbTransaction transaction);
    }
    #endregion
}