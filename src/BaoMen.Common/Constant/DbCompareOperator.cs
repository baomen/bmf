
namespace BaoMen.Common.Constant
{
    /// <summary>
    /// 比较操作符枚举
    /// </summary>
    public enum DbCompareOperator
    {
        /// <summary>
        /// 相等比较
        /// </summary>
        Equal = 0,

        /// <summary>
        /// 不等比较
        /// </summary>
        NotEqual = 1,

        /// <summary>
        /// 大于比较
        /// </summary>
        GreaterThan = 2,

        /// <summary>
        /// 大于或等于比较
        /// </summary>
        GreaterThanOrEqual = 3,

        /// <summary>
        /// 小于比较
        /// </summary>
        LessThan = 4,

        /// <summary>
        /// 小于或等于比较
        /// </summary>
        LessThanOrEqual = 5,

        /// <summary>
        /// 为空
        /// </summary>
        IsNull = 6,

        /// <summary>
        /// 不为空
        /// </summary>
        IsNotNull = 7,

        /// <summary>
        /// 以xxx开始(like 'xxx%')
        /// </summary>
        StartWith = 8,

        /// <summary>
        /// 以xxx结束(like '%xxx')
        /// </summary>
        EndWith = 9,

        /// <summary>
        /// 包含(like '%xxx%')
        /// </summary>
        Contains = 10,

        /// <summary>
        /// 在xxx中(in ('xxx1','xxx2'))
        /// </summary>
        In = 11,

        /// <summary>
        /// 不在xxx中(not in ('xxx1','xxx2'))
        /// </summary>
        NotIn = 12,
    }
}
