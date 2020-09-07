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
    #region interface IParameterManager (generated)
    /// <summary>
    /// 系统参数业务逻辑接口
    /// </summary>
    public interface IParameterManager : IHierarchicalBusinessLogic<string, Parameter, ParameterFilter>
    {
        /// <summary>
        /// 获取系统参数
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        Parameter Get(string parentId, string value);

        /// <summary>
        /// 创建新的ID
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        string CreateId(string parentId);
    }
    #endregion
}