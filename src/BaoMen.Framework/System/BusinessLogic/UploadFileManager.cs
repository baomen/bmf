/*
Author: WangXinBin
CreateTime: 2020/1/10 14:14:24
*/

using Microsoft.Extensions.Configuration;
using BaoMen.Framework.System.Entity;
using BaoMen.Common.Data;
using System;
using Microsoft.Extensions.DependencyInjection;
using BaoMen.Common.Constant;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace BaoMen.Framework.System.BusinessLogic
{
    #region class UploadFileManager (generated)
    /// <summary>
    /// 系统上传文件业务逻辑
    /// </summary>
    public partial class UploadFileManager : BusinessLogicBase<int,UploadFile,UploadFileFilter,DataAccess.UploadFile>,IUploadFileManager
    {
        private readonly IParameterManager parameterManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置实例</param>
        /// <param name="serviceProvider">服务提供程序</param>
        public UploadFileManager(IConfiguration configuration, IServiceProvider serviceProvider) : base(configuration)
        {
            parameterManager = serviceProvider.GetRequiredService<IParameterManager>();

        }

        /// <summary>
        /// 重写基类的方法
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        protected override int DoInsert(UploadFile item)
        {
            item.CreateTime = DateTime.Now;
            item.ExtentionName ??= string.Empty;
            return base.DoInsert(item);
        }

        /// <summary>
        /// 获取上传文件的目录路径
        /// </summary>
        /// <returns></returns>
        public string GetUploadPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return parameterManager.Get("0102010101").Value;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return parameterManager.Get("0102010102").Value;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 获取上传文件相对路径
        /// </summary>
        /// <returns></returns>
        public string GetRelativePath()
        {
            return parameterManager.Get("0102010103").Value;
        }

        /// <summary>
        /// 根据相对路径获取绝对路径
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public string GetPhysicalPath(string relativePath)
        {
            string uploadPath = GetUploadPath();
            string temp = relativePath.Replace(GetRelativePath(), uploadPath);
            return Path.Combine(temp);
        }

        /// <summary>
        /// 保存上传文件
        /// </summary>
        /// <param name="path">文件物理路径</param>
        /// <param name="file">文件流</param>
        /// <returns></returns>
        public void SaveFile(string path, IFormFile file)
        {
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(directory);
            }
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }

        /// <summary>
        /// 创建上传文件实例
        /// </summary>
        /// <param name="createUserId">创建人</param>
        /// <param name="relatedId">关联ID</param>
        /// <param name="type">类型</param>
        /// <param name="originalFileName">原始文件名</param>
        /// <returns></returns>
        public UploadFile CreateUploadFile(string createUserId, string relatedId, int type, string originalFileName)
        {
            DateTime now = DateTime.Now;
            string date = now.ToString("yyyyMMdd");
            string extentionName = Path.GetExtension(originalFileName) ?? string.Empty;
            string fileName = Guid.NewGuid().ToString("N");
            return new UploadFile
            {
                CreateUserId = createUserId,
                ExtentionName = extentionName,
                FileName = fileName,
                OriginalFileName = originalFileName,
                RelatedId = relatedId,
                Type = type,
                RelativePath = $"{GetRelativePath()}/{type}/{date}/{fileName}{extentionName}"
            };
        }

        /// <summary>
        /// 插入上传文件实例
        /// </summary>
        /// <param name="item">上传文件实例</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns></returns>
        public int Insert(Entity.UploadFile item, IDbTransaction transaction)
        {
            return ProcessInsert(() =>
            {
                item.CreateTime = DateTime.Now;
                item.ExtentionName ??= string.Empty;
                return dal.Insert(item, transaction);
            }, (log) =>
            {
                log.Properties[nameof(item)] = item;
            });
        }
    }
    #endregion
}