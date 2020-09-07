/*
Author: WangXinBin
CreateTime: 2020/1/13 10:24:54
*/

using Microsoft.Extensions.Configuration;
using BaoMen.Framework.System.Entity;
using BaoMen.Common.Data;

namespace BaoMen.Framework.System.BusinessLogic
{
    #region class DownloadFileManager (generated)
    /// <summary>
    /// 系统下载文件业务逻辑
    /// </summary>
    public partial class DownloadFileManager : BusinessLogicBase<int,DownloadFile,DownloadFileFilter,DataAccess.DownloadFile>,IDownloadFileManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置实例</param>
        public DownloadFileManager(IConfiguration configuration) : base(configuration)
        {

        }
    }
    #endregion
}