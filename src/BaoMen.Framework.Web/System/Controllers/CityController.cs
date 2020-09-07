using AutoMapper;
using BaoMen.Common.Model;
using BaoMen.Framework.System.BusinessLogic;
using BaoMen.Framework.Web.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Entity = BaoMen.Framework.System.Entity;

namespace BaoMen.Framework.Web.System.Controllers
{
    /// <summary>
    /// 系统地市
    /// </summary>
    public abstract class CityController : BaseController<string, Entity.City, Entity.CityFilter, Models.City, Models.CreateCity, Models.UpdateCity, Models.DeleteCity, ICityManager>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="manager">业务逻辑实例</param>
        /// <param name="mapper">AutoMapper实例</param>
        public CityController(ICityManager manager, IMapper mapper) : base(manager, mapper)
        {
        }

        /// <summary>
        /// 获取地市选项列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<ICollection<TextValue<string>>> GetOptions([FromQuery] Entity.CityFilter filter)
        {
            filter.Status = 1;
            return DoGetList<TextValue<string>>(filter);
        }
    }
}
