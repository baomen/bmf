using AutoMapper;
using BaoMen.Framework.System.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaoMen.Framework.Web.System.Mapper
{
    /// <summary>
    /// 系统参数ID转为名称
    /// </summary>
    public class ParameterIdToNameConverter : IValueConverter<string, string>
    {
        private readonly IParameterManager parameterManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parameterManager">系统参数业务逻辑</param>
        public ParameterIdToNameConverter(IParameterManager parameterManager)
        {
            this.parameterManager = parameterManager;
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="sourceMember"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Convert(string sourceMember, ResolutionContext context)
        {
            return parameterManager.Get(sourceMember)?.Name;
        }
    }

    /// <summary>
    /// 系统参数源数据
    /// </summary>
    public class ParameterSourceMember
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <param name="value">值</param>
        public ParameterSourceMember(string parentId, string value)
        {
            ParentId = parentId;
            Value = value;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <param name="value">值</param>
        public ParameterSourceMember(string parentId, int value)
        {
            ParentId = parentId;
            Value = value.ToString();
        }

        /// <summary>
        /// 父ID
        /// </summary>
        public string ParentId { get; private set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; private set; }
    }

    /// <summary>
    /// 系统参数值-->名称转换器
    /// </summary>
    public class ParameterValueToNameConverter : IValueConverter<ParameterSourceMember, string>
    {
        private readonly IParameterManager parameterManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parameterManager">系统参数业务逻辑</param>
        public ParameterValueToNameConverter(IParameterManager parameterManager)
        {
            this.parameterManager = parameterManager;
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="sourceMember"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Convert(ParameterSourceMember sourceMember, ResolutionContext context)
        {
            return parameterManager.Get(sourceMember.ParentId, sourceMember.Value)?.Name;
        }
    }

    ///// <summary>
    ///// 系统参数值-->名称转换器
    ///// </summary>
    //public abstract class ParameterValueToNameConverter : IValueConverter<string, string>
    //{
    //    private readonly IParameterManager parameterManager;

    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="parameterManager">系统参数业务逻辑</param>
    //    public ParameterValueToNameConverter(IParameterManager parameterManager)
    //    {
    //        this.parameterManager = parameterManager;
    //    }

    //    /// <summary>
    //    /// 父ID
    //    /// </summary>
    //    public abstract string ParentId { get; }

    //    /// <summary>
    //    /// 转换
    //    /// </summary>
    //    /// <param name="sourceMember"></param>
    //    /// <param name="context"></param>
    //    /// <returns></returns>
    //    public string Convert(string sourceMember, ResolutionContext context)
    //    {
    //        return parameterManager.Get(ParentId, sourceMember)?.Name;
    //    }
    //}

    ///// <summary>
    ///// 状态值-->名称转换器
    ///// </summary>
    //public class StatusValueToNameConverter : ParameterValueToNameConverter
    //{
    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="parameterManager">系统参数业务逻辑</param>
    //    public StatusValueToNameConverter(IParameterManager parameterManager) : base(parameterManager)
    //    {
    //    }

    //    /// <summary>
    //    /// 父ID
    //    /// </summary>
    //    public override string ParentId => "010101";
    //}
}
