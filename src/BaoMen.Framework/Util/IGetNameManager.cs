using System;
using System.Collections.Generic;
using System.Text;

namespace BaoMen.Framework.Util
{
    /// <summary>
    /// 获取名称的业务逻辑接口
    /// </summary>
    public interface IGetNameManager<TKey>
    {
        /// <summary>
        /// 根据关键字获取名称
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetName(TKey key);
    }
}
