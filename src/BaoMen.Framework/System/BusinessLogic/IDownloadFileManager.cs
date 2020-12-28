/*
Author: WangXinBin
CreateTime: 2020/1/13 10:24:54
*/

using BaoMen.Framework.System.Entity;
using BaoMen.Common.Data;

namespace BaoMen.Framework.System.BusinessLogic
{
    #region interface IDownloadFileManager (generated)
    /// <summary>
    /// 系统下载文件业务逻辑接口
    /// </summary>
    public interface IDownloadFileManager : IBusinessLogic<int,DownloadFile,DownloadFileFilter>
    {
        /// <summary>
        /// 获取上传文件的目录路径
        /// </summary>
        /// <returns></returns>
        string GetDownloadPath();
    }
    #endregion
}