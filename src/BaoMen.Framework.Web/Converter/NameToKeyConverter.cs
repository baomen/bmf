using AutoMapper;
using BaoMen.Framework.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaoMen.Framework.Web.Converter
{
    /// <summary>
    /// 名称 到 ID 转换器
    /// </summary>
    public class NameToKeyConverter<TKey, TManager> : IValueConverter<string, TKey>
        where TManager : IGetKeyManager<TKey>
    {
        private readonly TManager manager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="manager">业务逻辑</param>
        public NameToKeyConverter(TManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="sourceMember"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public TKey Convert(string sourceMember, ResolutionContext context)
        {
            return manager.GetKey(sourceMember);
        }
    }
}
