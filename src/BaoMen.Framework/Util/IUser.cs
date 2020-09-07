using System;
using System.Collections.Generic;
using System.Text;

namespace BaoMen.Framework.Util
{
    /// <summary>
    /// 系统用户接口
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// ID
        /// </summary>
        string Id { get; set; }
    }

    /// <summary>
    /// 商户用户接口
    /// </summary>
    public interface IMerchantUser : IUser
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        string MerchantId { get; set; }
    }
}
