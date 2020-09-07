using BaoMen.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaoMen.Framework.Util
{
    /// <summary>
    /// 商户数据接口
    /// </summary>
    public interface IMerchantData
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        string MerchantId { get; set; }
    }

    /// <summary>
    /// 商户数据过滤器接口
    /// </summary>
    public interface IMerchantFilter
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        FilterProperty<string> MerchantId { get; set; }
    }

    /// <summary>
    /// 商户服务
    /// </summary>
    public interface IMerchantService
    {
        ///// <summary>
        ///// 获取商户ID
        ///// </summary>
        ///// <returns></returns>
        //string GetMerchantId();

        /// <summary>
        /// 商户ID
        /// </summary>
        public string MerchantId { get; set; }
    }
}
