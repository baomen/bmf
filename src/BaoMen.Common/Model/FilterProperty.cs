using BaoMen.Common.Constant;
using System;
using System.Runtime.Serialization;

namespace BaoMen.Common.Model
{
    /// <summary>
    /// 数据库过滤类属性类型
    /// 当属性为null时，不作为检索条件
    /// </summary>
    [Serializable]
    [DataContract]
    public class FilterProperty<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FilterProperty()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">值</param>
        public FilterProperty(T value)
        {
            this.value = value;
        }

        private T value;
        /// <summary>
        /// 值
        /// </summary>
        [DataMember]
        public T Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private DbLogicOperator logicOperator;
        /// <summary>
        /// 逻辑操作符
        /// </summary>
        [DataMember]
        public DbLogicOperator LogicOperator
        {
            get { return this.logicOperator; }
            set { this.logicOperator = value; }
        }

        private DbCompareOperator compareOperator;
        /// <summary>
        /// 比较操作符
        /// </summary>
        [DataMember]
        public DbCompareOperator CompareOperator
        {
            get { return this.compareOperator; }
            set { this.compareOperator = value; }
        }

        /// <summary>
        /// 提供将FilterProperty转换为T的操作
        /// </summary>
        /// <param name="value">FilterProperty实例</param>
        /// <returns></returns>
        public static implicit operator T(FilterProperty<T> value)
        {
            return value == null ? default(T) : value.Value;
        }

        /// <summary>
        /// 提供将T转换为FilterProperty的操作
        /// </summary>
        /// <param name="value">T的值</param>
        /// <returns></returns>
        public static implicit operator FilterProperty<T>(T value)
        {
            return new FilterProperty<T>(value);
        }

        /// <summary>
        /// 重写的ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return value == null ? "null" : value.ToString();
        }
    }
}
