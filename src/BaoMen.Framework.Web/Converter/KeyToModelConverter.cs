using AutoMapper;
using BaoMen.Framework.System.BusinessLogic;
using BaoMen.Framework.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaoMen.Framework.Web.Converter
{
    /// <summary>
    /// ID 到 模型 转换器
    /// </summary>
    public class KeyToModelConverter<TSourceMember, TDestinationMember, TManager,TEntity> : IValueConverter<TSourceMember, TDestinationMember>
        where TManager : Common.Data.IBusinessLogic<TSourceMember, TEntity>
        where TEntity : class, new()
    {
        private readonly TManager manager;
        private readonly IMapper mapper;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="manager">业务逻辑</param>
        /// <param name="mapper">autoMapper实例</param>
        public KeyToModelConverter(TManager manager, IMapper mapper)
        {
            this.manager = manager;
            this.mapper = mapper;
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="sourceMember"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public TDestinationMember Convert(TSourceMember sourceMember, ResolutionContext context)
        {
            TEntity entity =  manager.Get(sourceMember);
            if (entity == null)
            {
                return default;
            }
            return mapper.Map<TDestinationMember>(entity);
        }
    }
}
