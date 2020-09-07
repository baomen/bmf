using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaoMen.Framework.Web.Util
{
    /// <summary>
    /// 列表及数量
    /// </summary>
    public class TotalAndItem<T>
        where T : class
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public ICollection<T> Items { get; set; }
    }
}
