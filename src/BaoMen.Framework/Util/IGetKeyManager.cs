using System;
using System.Collections.Generic;
using System.Text;

namespace BaoMen.Framework.Util
{
    /// <summary>
    /// 根据名称取Key接口
    /// </summary>
    /// <typeparam name="TKey">Key的类型</typeparam>
    public interface IGetKeyManager<TKey>
    {
        /// <summary>
        /// 根据名称取Key
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        TKey GetKey(string name);
    }
}
