using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaoMen.Framework.Web.System.Models
{
    /// <summary>
    /// 登录请求参数
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
