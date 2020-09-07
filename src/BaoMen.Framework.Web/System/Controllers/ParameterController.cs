using AutoMapper;
using BaoMen.Framework.Web.Util;
using BaoMen.Framework.System.BusinessLogic;
using BaoMen.Common.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using Entity = BaoMen.Framework.System.Entity;
using Model = BaoMen.Framework.Web.System.Models;
using System.Collections.Generic;
using System.Linq;

namespace BaoMen.Framework.Web.System.Controllers
{
    /// <summary>
    /// 系统参数
    /// </summary>
    public abstract class ParameterController : BaseHierarchicalController<string, Entity.Parameter, Entity.ParameterFilter, Model.Parameter, Model.CreateParameter, Model.UpdateParameter, Model.DeleteParameter, IParameterManager>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="manager">业务逻辑实例</param>
        /// <param name="mapper">AutoMapper实例</param>
        public ParameterController(IParameterManager manager, IMapper mapper) : base(manager, mapper)
        {

        }

        /// <summary>
        /// 创建数据
        /// </summary>
        /// <param name="model">创建模型</param>
        /// <returns></returns>
        public override ResponseData<Model.Parameter> Create([FromBody] Model.CreateParameter model)
        {
            ResponseData<Model.Parameter> responseData = new ResponseData<Model.Parameter>();
            try
            {
                Entity.Parameter entity = mapper.Map<Entity.Parameter>(model);
                entity.Id = manager.CreateId(model.ParentId);
                int rows = manager.Insert(entity);
                if (rows > 0)
                    responseData.Data = mapper.Map<Model.Parameter>(entity);
                else
                {
                    responseData.ErrorNumber = 1001;
                    responseData.ErrorMessage = Properties.Resources.Error_1001;
                }
            }
            catch (Exception exception)
            {
                responseData.ErrorNumber = 1000;
                responseData.ErrorMessage = Properties.Resources.Error_1000;
                responseData.Exception = exception;
                logger.Error(exception);
            }
            return responseData;
        }

        ///// <summary>
        ///// 取得嵌套的列表（包含子模块)
        ///// </summary>
        ///// <param name="filter">过滤器</param>
        ///// <param name="sort">排序规则</param>
        ///// <returns></returns>
        //[HttpGet]
        //public ResponseData<ICollection<Model.Parameter>> GetNestedList([FromQuery]Entity.ParameterFilter filter, string sort)
        //{
        //    ResponseData<ICollection<Model.Parameter>> responseData = new ResponseData<ICollection<Model.Parameter>>();
        //    try
        //    {
        //        ICollection<Entity.Parameter> entityParameters = manager.GetList(filter, sort);
        //        if (entityParameters?.Count > 0)
        //        {
        //            ICollection<Model.Parameter> modules = mapper.Map<ICollection<Model.Parameter>>(entityParameters);
        //            ICollection<Model.Parameter> nestedParameters = new List<Model.Parameter>();
        //            foreach (Model.Parameter module in modules)
        //            {
        //                if (!modules.Any(p => p.Id == module.ParentId))
        //                {
        //                    nestedParameters.Add(module);
        //                }
        //            }
        //            foreach (Model.Parameter module in nestedParameters)
        //            {
        //                AppendChildren(module, modules);
        //            }
        //            responseData.Data = nestedParameters;
        //        }
        //        else
        //        {
        //            responseData.Data = new List<Model.Parameter>();
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        responseData.Exception = exception;
        //        responseData.ErrorNumber = 1000;
        //        responseData.ErrorMessage = Properties.Resources.Error_1000;
        //        logger.Error(exception);
        //    }
        //    return responseData;
        //}

        //private void AppendChildren(Model.Parameter module, ICollection<Model.Parameter> modules)
        //{
        //    foreach (Model.Parameter item in modules)
        //    {
        //        if (item.ParentId == module.Id)
        //        {
        //            if (module.Children == null)
        //            {
        //                module.Children = new List<Model.Parameter>();
        //            }
        //            module.Children.Add(item);
        //            AppendChildren(item, modules);
        //        }
        //    }
        //}

    }
}
