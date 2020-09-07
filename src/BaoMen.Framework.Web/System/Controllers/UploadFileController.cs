using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BaoMen.Framework.Web.Util;
using BaoMen.Framework.System.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entity = BaoMen.Framework.System.Entity;
using BaoMen.Common.Model;

namespace BaoMen.Framework.Web.System.Controllers
{
    /// <summary>
    /// 上传文件
    /// </summary>
    public abstract class UploadFileController : BaseController<int, Entity.UploadFile, Entity.UploadFileFilter, Models.UploadFile, IUploadFileManager>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="manager">业务逻辑实例</param>
        /// <param name="mapper">AutoMapper实例</param>
        public UploadFileController(IUploadFileManager manager, IMapper mapper) : base(manager, mapper)
        {
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResponseData<Models.UploadFile> UploadFile([FromForm] Models.CreateUploadFile createUploadFile)
        {
            ResponseData<Models.UploadFile> responseData = new ResponseData<Models.UploadFile>();
            try
            {
                Framework.Util.ICurrentUserService currentUserService = GetRequiredService<Framework.Util.ICurrentUserService>();
                string createUserId = currentUserService.GetCurrentUser().Id;
                Entity.UploadFile uploadFile = manager.CreateUploadFile(createUserId, createUploadFile.RelatedId, createUploadFile.Type, createUploadFile.File.FileName);
                string physicalPath = manager.GetPhysicalPath(uploadFile.RelativePath);
                manager.SaveFile(physicalPath, createUploadFile.File);
                int rows = manager.Insert(uploadFile);
                if (rows > 0)
                    responseData.Data = mapper.Map<Models.UploadFile>(uploadFile);
                else
                {
                    responseData.ErrorNumber = 1001;
                    responseData.ErrorMessage = Properties.Resources.Error_1001;
                }
            }
            catch (Exception exception)
            {
                responseData.ErrorNumber = 1006;
                responseData.ErrorMessage = Properties.Resources.Error_1006;
                responseData.Exception = exception;
                logger.Error(exception);
            }
            return responseData;
        }
    }
}