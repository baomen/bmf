using AutoMapper;
using BaoMen.Framework.System.BusinessLogic;
using BaoMen.Framework.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaoMen.Framework.Web.Converter
{
    /// <summary>
    /// ID 到 名称 转换器
    /// </summary>
    public class KeyToNameConverter<TSourceMember, TManager> : IValueConverter<TSourceMember, string>
        where TManager : IGetNameManager<TSourceMember>
    {
        private readonly TManager manager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="manager">业务逻辑</param>
        public KeyToNameConverter(TManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="sourceMember"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Convert(TSourceMember sourceMember, ResolutionContext context)
        {
            return manager.GetName(sourceMember);
        }
    }
}
