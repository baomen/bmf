using AutoMapper;
using BaoMen.Common.Data;
using BaoMen.Common.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaoMen.Framework.Web.Util
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    [ApiVersion("1.0")]
    //[Authorize]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// 日志实例
        /// </summary>
        protected readonly ILogger logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseController()
        {
            logger = LogManager.GetLogger(GetType().FullName);
        }

        /// <summary>
        /// 从DI获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GetRequiredService<T>()
        {
            return HttpContext.RequestServices.GetRequiredService<T>();
        }

        /// <summary>
        /// 获取其实索引
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        protected int GetStartRowIndex(int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                pageIndex = 1;
            if (pageSize == int.MaxValue) return 0;
            pageSize = CheckPageSize(pageSize);
            return pageSize * (pageIndex - 1);
        }

        /// <summary>
        /// 检查页大小
        /// </summary>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        protected int CheckPageSize(int pageSize)
        {
            if (pageSize <= 0)
                return 10;
            return pageSize;
        }

        /// <summary>
        /// 调用
        /// </summary>
        /// <typeparam name="T">返回的Data的类型</typeparam>
        /// <returns></returns>
        protected ResponseData Execute(Action<ResponseData> action = null)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                action?.Invoke(responseData);
            }
            catch (AutoMapperMappingException autoMapperMappingException)
            {
                responseData.ErrorNumber = 1009;
                responseData.ErrorMessage = Properties.Resources.Error_1009;
                //responseData.Exception = autoMapperMappingException;
                logger.Error(autoMapperMappingException);
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

        /// <summary>
        /// 调用
        /// </summary>
        /// <typeparam name="T">返回的Data的类型</typeparam>
        /// <returns></returns>
        protected ResponseData<T> Execute<T>(Action<ResponseData<T>> action = null)
        {
            ResponseData<T> responseData = new ResponseData<T>();
            try
            {
                action?.Invoke(responseData);
            }
            catch (AutoMapperMappingException autoMapperMappingException)
            {
                responseData.ErrorNumber = 1009;
                responseData.ErrorMessage = Properties.Resources.Error_1009;
                //responseData.Exception = autoMapperMappingException;
                logger.Error(autoMapperMappingException);
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
    }

    /// <summary>
    /// 控制器基类
    /// </summary>
    /// <typeparam name="TKey">实体键的类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TFilter">实体过滤器类型</typeparam>
    /// <typeparam name="TModel">实体模型</typeparam>
    /// <typeparam name="TManager">业务逻辑类型</typeparam>
    [ApiVersion("1.0")]
    //[Authorize]
    [ApiController]
    public abstract class BaseController<TKey, TEntity, TFilter, TModel, TManager> : BaseController
        where TEntity : class, new()
        where TFilter : class
        where TModel : class
        where TManager : IBusinessLogic<TKey, TEntity, TFilter>
    {
        /// <summary>
        /// 业务逻辑实例
        /// </summary>
        protected TManager manager;

        /// <summary>
        /// AutoMapper实例
        /// </summary>
        protected readonly IMapper mapper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="manager">业务逻辑实例</param>
        /// <param name="mapper">AutoMapper实例</param>
        public BaseController(TManager manager, IMapper mapper)
        {
            this.mapper = mapper;
            this.manager = manager;
        }

        #region CRUD Actions

        #region protected
        /// <summary>
        /// 获取单个实例
        /// </summary>
        /// <param name="id">标识</param>
        /// <returns></returns>
        protected virtual ResponseData<T> DoGet<T>(TKey id)
            where T : class
        {
            return Execute<T>((responseData) =>
            {
                TEntity entity = manager.Get(id);
                if (entity != null)
                {
                    responseData.Data = mapper.Map<T>(entity);
                }
            });
            //ResponseData<T> responseData = new ResponseData<T>();
            //try
            //{
            //    TEntity entity = manager.Get(id);
            //    if (entity != null)
            //    {
            //        responseData.Data = mapper.Map<T>(entity);
            //    }
            //}
            //catch (Exception exception)
            //{
            //    responseData.ErrorNumber = 1000;
            //    responseData.ErrorMessage = Properties.Resources.Error_1000;
            //    responseData.Exception = exception;
            //    logger.Error(exception);
            //}
            //return responseData;
        }

        /// <summary>
        /// 获取实例的ICollection列表
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sortExpression">排序字串</param>
        /// <returns></returns>
        // GET api/<controller>/<action>
        protected virtual ResponseData<ICollection<T>> DoGetList<T>(TFilter filter, string sortExpression = null)
            where T : class
        {
            return Execute<ICollection<T>>((responseData) =>
            {
                responseData.Data = mapper.Map<ICollection<T>>(manager.GetList(filter, sortExpression));
            });
            //ResponseData<ICollection<T>> responseData = new ResponseData<ICollection<T>>();
            //try
            //{
            //    responseData.Data = mapper.Map<ICollection<T>>(manager.GetList(filter, sortExpression));
            //}
            //catch (Exception e)
            //{
            //    responseData.ErrorNumber = 1000;
            //    responseData.ErrorMessage = Properties.Resources.Error_1000;
            //    responseData.Exception = e;
            //}
            //return responseData;
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        protected virtual ResponseData<int> DoGetListCount(TFilter filter)
        {
            return Execute<int>((responseData) =>
            {
                responseData.Data = manager.GetListCount(filter);
            });
            //ResponseData<int> responseData = new ResponseData<int>();
            //try
            //{
            //    responseData.Data = manager.GetListCount(filter);
            //}
            //catch (Exception e)
            //{
            //    responseData.ErrorNumber = 1000;
            //    responseData.ErrorMessage = Properties.Resources.Error_1000;
            //    responseData.Exception = e;
            //}
            //return responseData;
        }
        #endregion

        /// <summary>
        /// 获取多个实例的数量和ICollection列表
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序字串</param>
        /// <param name="pageIndex">开始的页数（从1开始）</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        [HttpGet]
        public virtual ResponseData<TotalAndItem<TModel>> GetList([FromQuery] TFilter filter, string sort, int pageIndex = 1, int pageSize = 10)
        {
            return Execute<TotalAndItem<TModel>>((responseData) =>
            {
                pageSize = CheckPageSize(pageSize);
                Tuple<int, ICollection<TEntity>> entityListAndCount = manager.GetCountAndList(filter, sort, GetStartRowIndex(pageIndex, pageSize), pageSize);
                responseData.Data = new TotalAndItem<TModel>
                {
                    Total = entityListAndCount.Item1,
                    Items = mapper.Map<ICollection<TModel>>(entityListAndCount.Item2)
                };
            });
            //ResponseData<TotalAndItem<TModel>> responseData = new ResponseData<TotalAndItem<TModel>>();
            //try
            //{
            //    pageSize = CheckPageSize(pageSize);
            //    Tuple<int, ICollection<TEntity>> entityListAndCount = manager.GetCountAndList(filter, sort, GetStartRowIndex(pageIndex, pageSize), pageSize);
            //    responseData.Data = new TotalAndItem<TModel>
            //    {
            //        Total = entityListAndCount.Item1,
            //        Items = mapper.Map<ICollection<TModel>>(entityListAndCount.Item2)
            //    };
            //}
            //catch (Exception exception)
            //{
            //    responseData.ErrorNumber = 1000;
            //    responseData.ErrorMessage = Properties.Resources.Error_1000;
            //    responseData.Exception = exception;
            //    logger.Error(exception);
            //}
            //return responseData;
        }

        #endregion
    }

    /// <summary>
    /// 控制器基类
    /// </summary>
    /// <typeparam name="TKey">实体键的类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TFilter">实体过滤器类型</typeparam>
    /// <typeparam name="TModel">实体模型</typeparam>
    /// <typeparam name="TCreate">创建实体模型</typeparam>
    /// <typeparam name="TUpdate">更新实体模型</typeparam>
    /// <typeparam name="TDelete">删除实体模型</typeparam>
    /// <typeparam name="TManager">业务逻辑类型</typeparam>
    [ApiVersion("1.0")]
    //[Authorize]
    [ApiController]
    //[EnableCors("any")]
    public abstract class BaseController<TKey, TEntity, TFilter, TModel, TCreate, TUpdate, TDelete, TManager> : BaseController<TKey, TEntity, TFilter, TModel, TManager>
        where TEntity : class, new()
        where TFilter : class
        where TModel : class
        where TCreate : class
        where TUpdate : class
        where TDelete : class
        where TManager : IBusinessLogic<TKey, TEntity, TFilter>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="manager">业务逻辑实例</param>
        /// <param name="mapper">AutoMapper实例</param>
        public BaseController(TManager manager, IMapper mapper) : base(manager, mapper)
        {
        }

        #region CRUD Actions

        /// <summary>
        /// 创建数据
        /// </summary>
        /// <param name="model">创建模型</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ResponseData<TModel> Create([FromBody] TCreate model)
        {
            return Execute<TModel>((responseData) =>
            {
                TEntity entity = mapper.Map<TEntity>(model);
                int rows = manager.Insert(entity);
                if (rows > 0)
                    responseData.Data = mapper.Map<TModel>(entity);
                else
                {
                    responseData.ErrorNumber = 1001;
                    responseData.ErrorMessage = Properties.Resources.Error_1001;
                    logger.Warn("Create Warn: inert rows=0, model={model}", model);
                }
            });
            //ResponseData<TModel> responseData = new ResponseData<TModel>();
            //ResponseData<TModel> responseData = new ResponseData<TModel>();
            //try
            //{
            //    TEntity entity = mapper.Map<TEntity>(model);
            //    int rows = manager.Insert(entity);
            //    if (rows > 0)
            //        responseData.Data = mapper.Map<TModel>(entity);
            //    else
            //    {
            //        responseData.ErrorNumber = 1001;
            //        responseData.ErrorMessage = Properties.Resources.Error_1001;
            //        logger.Warn("Create Warn: inert rows=0, model={model}", model);
            //    }
            //}
            //catch (Exception exception)
            //{
            //    responseData.ErrorNumber = 1000;
            //    responseData.ErrorMessage = Properties.Resources.Error_1000;
            //    //responseData.Exception = exception;
            //    string e = Newtonsoft.Json.JsonConvert.SerializeObject(exception, new Newtonsoft.Json.JsonSerializerSettings
            //    {
            //        DateFormatString = "yyyy-MM-dd HH:mm:ss",
            //        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
            //        MaxDepth = 10,
            //        PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects
            //    });
            //    logger.Error(exception);
            //}
            //return responseData;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model">更新模型</param>
        /// <returns></returns>
        [HttpPut]
        public virtual ResponseData Update([FromBody] TUpdate model)
        {
            return Execute((responseData) =>
            {
                TEntity entity = mapper.Map<TEntity>(model);
                int rows = manager.Update(entity);
                if (rows == 0)
                {
                    responseData.ErrorNumber = 1002;
                    responseData.ErrorMessage = Properties.Resources.Error_1002;
                    logger.Warn("Update Warn: inert rows=0, model={model}", model);
                }
            });
            //ResponseData responseData = new ResponseData();
            //try
            //{
            //    TEntity entity = mapper.Map<TEntity>(model);
            //    int rows = manager.Update(entity);
            //    if (rows == 0)
            //    {
            //        responseData.ErrorNumber = 1002;
            //        responseData.ErrorMessage = Properties.Resources.Error_1002;
            //        logger.Warn("Update Warn: inert rows=0, model={model}", model);
            //    }
            //}
            //catch (Exception exception)
            //{
            //    responseData.ErrorNumber = 1000;
            //    responseData.ErrorMessage = Properties.Resources.Error_1000;
            //    responseData.Exception = exception;
            //    logger.Error(exception);
            //}
            //return responseData;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="model">实体实例</param>
        /// <returns></returns>
        [HttpDelete]
        //[System.Web.Http.Cors.DisableCors]
        public virtual ResponseData Delete([FromBody] TDelete model)
        {
            return Execute((responseData) =>
            {
                TEntity entity = mapper.Map<TEntity>(model);
                int rows = manager.Delete(entity);
                if (rows == 0)
                {
                    responseData.ErrorNumber = 1003;
                    responseData.ErrorMessage = Properties.Resources.Error_1003;
                    logger.Warn("Delete Warn: inert rows=0, model={model}", model);
                }
            });
            //ResponseData responseData = new ResponseData();
            //try
            //{
            //    TEntity entity = mapper.Map<TEntity>(model);
            //    int rows = manager.Delete(entity);
            //    if (rows == 0)
            //    {
            //        responseData.ErrorNumber = 1003;
            //        responseData.ErrorMessage = Properties.Resources.Error_1003;
            //        logger.Warn("Delete Warn: inert rows=0, model={model}", model);
            //    }
            //}
            //catch (Exception exception)
            //{
            //    responseData.ErrorNumber = 1000;
            //    responseData.ErrorMessage = Properties.Resources.Error_1000;
            //    responseData.Exception = exception;
            //    logger.Error(exception);
            //}
            //return responseData;
        }
        #endregion
    }

    /// <summary>
    /// 控制器基类
    /// </summary>
    /// <typeparam name="TKey">实体键的类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TFilter">实体过滤器类型</typeparam>
    /// <typeparam name="TModel">实体模型</typeparam>
    /// <typeparam name="TCreate">创建实体模型</typeparam>
    /// <typeparam name="TUpdate">更新实体模型</typeparam>
    /// <typeparam name="TDelete">删除实体模型</typeparam>
    /// <typeparam name="TManager">业务逻辑类型</typeparam>
    public abstract class BaseHierarchicalController<TKey, TEntity, TFilter, TModel, TCreate, TUpdate, TDelete, TManager> : BaseController<TKey, TEntity, TFilter, TModel, TCreate, TUpdate, TDelete, TManager>
        where TEntity : class, IHierarchicalData<TKey>, new()
        where TFilter : class
        where TModel : class
        where TCreate : class
        where TUpdate : class
        where TDelete : class
        where TManager : IHierarchicalBusinessLogic<TKey, TEntity, TFilter>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="manager">业务逻辑实例</param>
        /// <param name="mapper">AutoMapper实例</param>
        public BaseHierarchicalController(TManager manager, IMapper mapper)
            : base(manager, mapper)
        {
        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="id">标识</param>
        /// <returns></returns>
        [HttpGet]
        public virtual ResponseData<ICollection<TModel>> GetChildren([FromQuery] TKey id)
        {
            return Execute<ICollection<TModel>>((responseData) =>
            {
                responseData.Data = mapper.Map<ICollection<TModel>>(manager.GetChildren(id));
            });
            //ResponseData<ICollection<TModel>> responseData = new ResponseData<ICollection<TModel>>();
            //try
            //{
            //    responseData.Data = mapper.Map<ICollection<TModel>>(manager.GetChildren(id));
            //}
            //catch (Exception exception)
            //{
            //    responseData.ErrorNumber = 1000;
            //    responseData.ErrorMessage = Properties.Resources.Error_1000;
            //    responseData.Exception = exception;
            //    logger.Error(exception);
            //}
            //return responseData;
        }

        /// <summary>
        /// 获取所有子节点
        /// </summary>
        /// <param name="id">标识</param>
        /// <returns></returns>
        [HttpGet]
        public virtual ResponseData<ICollection<TModel>> GetAllChildren([FromQuery] TKey id)
        {
            return Execute<ICollection<TModel>>((responseData) =>
            {
                responseData.Data = mapper.Map<ICollection<TModel>>(manager.GetAllChildren(id));
            });
            //ResponseData<ICollection<TModel>> responseData = new ResponseData<ICollection<TModel>>();
            //try
            //{
            //    responseData.Data = mapper.Map<ICollection<TModel>>(manager.GetAllChildren(id));
            //}
            //catch (Exception exception)
            //{
            //    responseData.ErrorNumber = 1000;
            //    responseData.ErrorMessage = Properties.Resources.Error_1000;
            //    responseData.Exception = exception;
            //    logger.Error(exception);
            //}
            //return responseData;
        }
    }
}
