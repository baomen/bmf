/*
Author: WangXinBin
CreateTime: 2019/11/1 12:38:39
*/

using System.Collections.Generic;
using System.Linq;
using BaoMen.Framework.System.Entity;
using BaoMen.Common.Data;

namespace BaoMen.Framework.System.BusinessLogic
{
    #region interface IProvinceManager (generated)
    /// <summary>
    /// 省份信息业务逻辑接口
    /// </summary>
    public interface IProvinceManager : ICacheableBusinessLogic<string,Province,ProvinceFilter>, Util.IGetNameManager<string>, Util.IGetKeyManager<string>
    {
        
    }
    #endregion
}