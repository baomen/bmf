/*
Author: WangXinBin
CreateTime: 2019/9/23 14:34:58
*/

using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using BaoMen.Framework.System.Entity;
using BaoMen.Common.Data;

namespace BaoMen.Framework.System.BusinessLogic
{
    #region interface IModuleManager (generated)
    /// <summary>
    /// 系统模块业务逻辑接口
    /// </summary>
    public interface IModuleManager : IHierarchicalBusinessLogic<string, Module, ModuleFilter>
    {

    }
    #endregion
}