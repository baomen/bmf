using AutoMapper;
using BaoMen.Common.Model;
using BaoMen.Framework.Web.Util;
using BaoMen.Framework.System.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Entity = BaoMen.Framework.System.Entity;
using Model = BaoMen.Framework.Web.System.Models;

namespace BaoMen.Framework.Web.System.Controllers
{
    /// <summary>
    /// 系统用户登录历史
    /// </summary>
    public abstract class UserLoginHistoryController : BaseController<int, Entity.UserLoginHistory, Entity.UserLoginHistoryFilter, Model.UserLoginHistory, IUserLoginHistoryManager>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="manager">业务逻辑实例</param>
        /// <param name="mapper">AutoMapper实例</param>
        public UserLoginHistoryController(IUserLoginHistoryManager manager, IMapper mapper) : base(manager, mapper)
        {
        }
    }
}
