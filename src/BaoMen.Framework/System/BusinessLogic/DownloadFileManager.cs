/*
Author: WangXinBin
CreateTime: 2020/1/13 10:24:54
*/

using Microsoft.Extensions.Configuration;
using BaoMen.Framework.System.Entity;
using BaoMen.Common.Data;
using System;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;

namespace BaoMen.Framework.System.BusinessLogic
{
    #region class DownloadFileManager (generated)
    /// <summary>
    /// 系统下载文件业务逻辑
    /// </summary>
    public partial class DownloadFileManager : BusinessLogicBase<int,DownloadFile,DownloadFileFilter,DataAccess.DownloadFile>,IDownloadFileManager
    {
        private readonly IParameterManager parameterManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置实例</param>
        /// <param name="serviceProvider">服务提供程序</param>
        public DownloadFileManager(IConfiguration configuration, IServiceProvider serviceProvider) : base(configuration)
        {
            parameterManager = serviceProvider.GetRequiredService<IParameterManager>();
        }

        /// <summary>
        /// 获取下载文件的目录路径
        /// </summary>
        /// <returns></returns>
        public string GetDownloadPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return parameterManager.Get("0102010201").Value;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return parameterManager.Get("0102010201").Value;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
    #endregion
}