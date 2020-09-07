using System;
using System.Collections.Generic;
using System.Text;

namespace BaoMen.Framework.Web.System.Models
{
    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// 访问令牌
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 访问令牌有效期（单位：秒）
        /// </summary>
        public int Expires { get; set; }
    }
}
