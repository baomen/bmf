using AutoMapper;
using BaoMen.Framework.Web.Util;
using BaoMen.Framework.System.BusinessLogic;
using BaoMen.Common.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Entity = BaoMen.Framework.System.Entity;
using Model = BaoMen.Framework.Web.System.Models;

namespace BaoMen.Framework.Web.System.Controllers
{
    /// <summary>
    /// 系统模块
    /// </summary>
    public abstract class ModuleController : BaseHierarchicalController<string, Entity.Module, Entity.ModuleFilter, Model.Module, Model.CreateModule, Model.UpdateModule, Model.DeleteModule, IModuleManager>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="manager">业务逻辑实例</param>
        /// <param name="mapper">AutoMapper实例</param>
        public ModuleController(IModuleManager manager, IMapper mapper) : base(manager, mapper)
        {

        }

        /// <summary>
        /// 取得嵌套的列表（包含子模块)
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序规则</param>
        /// <returns></returns>
        [HttpGet]
        public virtual ResponseData<ICollection<Model.Module>> GetNestedList([FromQuery]Entity.ModuleFilter filter, string sort)
        {
            ResponseData<ICollection<Model.Module>> responseData = new ResponseData<ICollection<Model.Module>>();
            try
            {
                ICollection<Entity.Module> entityModules = manager.GetList(filter, sort);
                if (entityModules?.Count > 0)
                {
                    ICollection<Model.Module> modules = mapper.Map<ICollection<Model.Module>>(entityModules);
                    ICollection<Model.Module> nestedModules = new List<Model.Module>();
                    foreach (Model.Module module in modules)
                    {
                        if (!modules.Any(p => p.Id == module.ParentId))
                        {
                            nestedModules.Add(module);
                        }
                    }
                    foreach (Model.Module module in nestedModules)
                    {
                        AppendChildren(module, modules);
                    }
                    responseData.Data = nestedModules;
                }
                else
                {
                    responseData.Data = new List<Model.Module>();
                }
            }
            catch (Exception exception)
            {
                responseData.Exception = exception;
                responseData.ErrorNumber = 1000;
                responseData.ErrorMessage = Properties.Resources.Error_1000;
                logger.Error(exception);
            }
            return responseData;
        }

        private void AppendChildren(Model.Module module, ICollection<Model.Module> modules)
        {
            foreach (Model.Module item in modules)
            {
                if (item.ParentId == module.Id)
                {
                    if (module.Children == null)
                    {
                        module.Children = new List<Model.Module>();
                    }
                    module.Children.Add(item);
                    AppendChildren(item, modules);
                }
            }
        }

    }
}
