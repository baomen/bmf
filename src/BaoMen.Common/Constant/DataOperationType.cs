using System;

namespace BaoMen.Common.Constant
{
    /// <summary>
    /// 数据操作
    /// </summary>
    [Flags]
    public enum DataOperationType
    {
        /// <summary>
        /// 无操作
        /// </summary>
        None = 0,

        /// <summary>
        /// 查询
        /// </summary>
        Select = 1,

        /// <summary>
        /// 插入
        /// </summary>
        Insert = 2,

        /// <summary>
        /// 更新
        /// </summary>
        Update = 4,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 8,
    }
}
