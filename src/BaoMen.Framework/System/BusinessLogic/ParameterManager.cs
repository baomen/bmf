/*
Author: WangXinBin
CreateTime: 2019/9/23 14:34:58
*/

using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using BaoMen.Framework.System.Entity;
using BaoMen.Common.Data;
using BaoMen.Common.Constant;
using System;

namespace BaoMen.Framework.System.BusinessLogic
{
    #region class ParameterManager (generated)
    /// <summary>
    /// 系统参数业务逻辑
    /// </summary>
    public partial class ParameterManager : HierarchicalCacheableBusinessLogicBase<string, Parameter, ParameterFilter, DataAccess.Parameter>, IParameterManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置实例</param>
        /// <param name="operateHistoryManager">操作日志业务逻辑</param>
        public ParameterManager(IConfiguration configuration, IOperateHistoryManager operateHistoryManager) : base(configuration)
        {
            this.operateHistoryManager = operateHistoryManager;
        }

        private IOperateHistoryManager operateHistoryManager;

        /// <summary>
        /// 插入系统参数
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        protected override int DoInsert(Parameter item)
        {
            Parameter parent = DoGet(item.ParentId);
            if (parent != null && parent.IsNode == 0)
            {
                int affectRows = ProcessWithTransaction((transaction) =>
                {
                    int rows = dal.UpdateNode(parent.Id, 1, transaction);
                    rows += dal.Insert(item, transaction);
                    return rows;
                });
                if (affectRows == 2)
                {
                    operateHistoryManager.Insert(item.Id, item, DataOperationType.Insert);
                }
                return affectRows;
            }
            else
            {
                int rows = base.DoInsert(item);
                if (rows == 1)
                    operateHistoryManager.Insert(item.Id, item, DataOperationType.Insert);
                return rows;
            }
        }

        /// <summary>
        /// 更新系统参数
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        protected override int DoUpdate(Parameter item)
        {
            int affectRows = base.DoUpdate(item);
            if (affectRows == 1)
            {
                operateHistoryManager.Insert(item.Id, item, DataOperationType.Update);
            }
            return affectRows;
        }

        /// <summary>
        /// 删除系统参数
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        protected override int DoDelete(Parameter item)
        {
            Parameter parameter = DoGet(item.Id);
            if (parameter == null)
                throw new BusinessLogicException("参数不存在");
            ICollection<Parameter> children = DoGetChildren(item.Id);
            if (children.Count > 0)
                throw new BusinessLogicException("请先删除子节点");
            int affectRows;
            //如果不是根节点判断是否还有兄弟节点
            if (parameter.ParentId != "00")
            {
                ICollection<Parameter> brothers = DoGetChildren(parameter.ParentId).Where(p => p.Id != item.Id).ToList();
                if (brothers.Count == 0)
                {
                    Parameter parent = DoGet(parameter.ParentId);
                    affectRows = ProcessWithTransaction((transaction) =>
                    {
                        int rows = dal.UpdateNode(parent.Id, 0, transaction);
                        rows += dal.Delete(item, transaction);
                        return rows;
                    });
                    if (affectRows == 2)
                    {
                        operateHistoryManager.Insert(item.Id, item, DataOperationType.Delete);
                    }
                    return affectRows;
                }
            }
            affectRows = base.DoDelete(item);
            if (affectRows == 1)
                operateHistoryManager.Insert(item.Id, item, DataOperationType.Delete);
            return affectRows;
        }

        /// <summary>
        /// 获取系统参数
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public Parameter Get(string parentId, string value)
        {
            ICollection<Parameter> itemList = GetChildren(parentId);
            return itemList.SingleOrDefault(p => p.Value == value);
        }

        /// <summary>
        /// 创建新的ID
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public string CreateId(string parentId)
        {
            if (string.IsNullOrEmpty(parentId))
            {
                parentId = "00";
            }
            for (int i = 1; i < 100; i++)
            {
                string id = i.ToString("00");
                if (parentId != "00")
                    id = parentId + id;
                Parameter item = DoGet(id);
                if (item == null)
                {
                    return id;
                }
            }
            throw new ArgumentException("数据已达99条，无法生成新Id");
        }
    }
    #endregion
}