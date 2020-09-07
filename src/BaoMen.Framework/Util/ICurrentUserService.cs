using System;
using System.Collections.Generic;
using System.Text;

namespace BaoMen.Framework.Util
{
    /// <summary>
    /// 当前商户用户接口
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        IUser GetCurrentUser();
    }
}
