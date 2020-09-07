using System;
using System.Collections.Generic;
using System.Text;

namespace BaoMen.Common.Model
{
    /// <summary>
    /// 文本/值
    /// </summary>
    public class TextValue<T>
    {
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; set; }
    }
}
