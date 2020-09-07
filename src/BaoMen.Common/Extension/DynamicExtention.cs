using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace BaoMen.Common.Extension
{
    /// <summary>
    /// dynamic扩展
    /// </summary>
    public static class DynamicExtention
    {
        /// <summary>
        /// 属性是否存在
        /// </summary>
        /// <param name="data">dynamic数据</param>
        /// <param name="propertyname">属性名称</param>
        /// <returns></returns>
        public static bool IsPropertyExist(dynamic data, string propertyname)
        {
            if (data is ExpandoObject)
                return ((IDictionary<string, object>)data).ContainsKey(propertyname);
            return data.GetType().GetProperty(propertyname) != null;
        }
    }
}
