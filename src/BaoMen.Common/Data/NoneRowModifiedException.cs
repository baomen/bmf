using System;

namespace BaoMen.Common.Data
{
    /// <summary>
    /// 更新或删除数据时未更改任何行异常类
    /// </summary>
    public class NoneRowModifiedException : Exception
    {
        /// <summary>
        /// 已重写。初始化一个<see cref="NoneRowModifiedException"/>新实例
        /// </summary>
        public NoneRowModifiedException()
            : base()
        {
        }

        /// <summary>
        /// 已重写。使用指定错误消息初始化<see cref="NoneRowModifiedException"/>新实例
        /// </summary>
        /// <param name="message">描述当前异常的消息</param>
        public NoneRowModifiedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 已重写。使用指定错误消息和对作为此异常原因的内部异常的引用来初始化<see cref="NoneRowModifiedException"/>新实例
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="innerException">导致当前异常的 Exception 实例</param>
        public NoneRowModifiedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
