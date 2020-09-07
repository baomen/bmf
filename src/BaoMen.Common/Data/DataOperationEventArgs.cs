using BaoMen.Common.Constant;
using System;

namespace BaoMen.Common.Data
{
    /// <summary>
    /// 数据操作的事件数据的基类
    /// </summary>
    public abstract class DataOperationEventArgs : EventArgs
    {
        private DataOperationType dataOperationType;
        /// <summary>
        /// 获取数据操作类型
        /// </summary>
        public DataOperationType DataOperationType
        {
            get { return this.dataOperationType; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataOperationType"><see cref="DataOperationType"/>实例</param>
        protected DataOperationEventArgs(DataOperationType dataOperationType)
        {
            this.dataOperationType = dataOperationType;
        }
    }

    /// <summary>
    /// 数据操作开始的事件数据
    /// </summary>
    public class DataOperatingEventArgs : DataOperationEventArgs
    {
        /// <summary>
        /// 获取或设置是否取消操作
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataOperationType"><see cref="DataOperationType"/>实例</param>
        public DataOperatingEventArgs(DataOperationType dataOperationType)
            : base(dataOperationType)
        {
        }
    }

    /// <summary>
    /// 数据操作成功的事件数据
    /// </summary>
    public class DataOperateSuccessEventArgs : DataOperationEventArgs
    {
        private object returnValue;

        /// <summary>
        /// 获取返回值
        /// </summary>
        public object ReturnValue { get { return this.returnValue; } }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="returnValue">返回值</param>
        /// <param name="dataOperationType"><see cref="DataOperationType"/>实例</param>
        public DataOperateSuccessEventArgs(DataOperationType dataOperationType, Object returnValue)
            : base(dataOperationType)
        {
            this.returnValue = returnValue;
        }
    }

    /// <summary>
    /// 数据操作失败的事件数据
    /// </summary>
    public class DataOperateErrorEventArgs : DataOperationEventArgs
    {
        /// <summary>
        /// 获取或设置是否处理异常
        /// </summary>
        public bool Handled { get; set; }

        private Exception exception;
        /// <summary>
        /// 获取异常
        /// </summary>
        public Exception Exception
        {
            get { return this.exception; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataOperationType"><see cref="DataOperationType"/>实例</param>
        /// <param name="exception">异常实例</param>
        public DataOperateErrorEventArgs(DataOperationType dataOperationType, Exception exception)
            : base(dataOperationType)
        {
            this.exception = exception;
        }
    }

    /// <summary>
    /// 数据操作完成的事件数据
    /// </summary>
    public class DataOperatedEventArgs : DataOperationEventArgs
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataOperationType"><see cref="DataOperationType"/>实例</param>
        public DataOperatedEventArgs(DataOperationType dataOperationType)
            : base(dataOperationType)
        {
        }
    }
}
