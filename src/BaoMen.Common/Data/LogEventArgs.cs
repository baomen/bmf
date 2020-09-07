using NLog;
using System;

namespace BaoMen.Common.Data
{
    /// <summary>
    /// 日志事件参数
    /// </summary>
    [Serializable]
    public class LogEventArgs : EventArgs
    {
        /// <summary>
        /// LogEventInfo
        /// </summary>
        public LogEventInfo LogEventInfo { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logEventInfo">LogEventInfo实例</param>
        public LogEventArgs(LogEventInfo logEventInfo)
        {
            LogEventInfo = logEventInfo;
        }
    }
}
