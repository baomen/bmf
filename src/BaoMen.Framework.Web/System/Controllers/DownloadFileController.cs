using AutoMapper;
using BaoMen.Framework.System.BusinessLogic;
using BaoMen.Framework.Web.Util;
using Entity = BaoMen.Framework.System.Entity;
using Model = BaoMen.Framework.Web.System.Models;
namespace BaoMen.Framework.Web.System.Controllers
{
    /// <summary>
    /// 下载文件
    /// </summary>
    public abstract class DownloadFileController : BaseController<int, Entity.DownloadFile, Entity.DownloadFileFilter, Model.DownloadFile, IDownloadFileManager>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="manager">业务逻辑实例</param>
        /// <param name="mapper">AutoMapper实例</param>
        public DownloadFileController(IDownloadFileManager manager, IMapper mapper) : base(manager, mapper)
        {
        }
    }
}