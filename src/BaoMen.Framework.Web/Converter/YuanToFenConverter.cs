using AutoMapper;
using System;

namespace BaoMen.Framework.Web.Converter
{
    /// <summary>
    /// 元 --> 分
    /// </summary>
    public class YuanToFenConverter : IValueConverter<decimal, int>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="sourceMember">源</param>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public int Convert(decimal sourceMember, ResolutionContext context)
        {
            return (int)(sourceMember * 100);
        }
    }
}
