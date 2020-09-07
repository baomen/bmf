using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace BaoMen.Common.Model
{
    /// <summary>
    /// 返回的数据
    /// </summary>
    [Serializable]
    //[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    //[DataContract]
    public class ResponseData
    {
        /// <summary>
        /// 错误编号。0为无错误
        /// </summary>
        //[DataMember]
        public int ErrorNumber { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        //[DataMember]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        //[DataMember]
        public Exception Exception { get; set; }

    }

    /// <summary>
    /// 返回的数据
    /// </summary>
    [Serializable]
    //[DataContract]
    public class ResponseData<T> : ResponseData
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ResponseData()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="responseData">返回的数据实例</param>
        public ResponseData(ResponseData responseData)
        {
            ErrorMessage = responseData.ErrorMessage;
            ErrorNumber = responseData.ErrorNumber;
            Exception = responseData.Exception;
        }

        /// <summary>
        /// 获取或设置值
        /// </summary>
        //[DataMember]
        public T Data { get; set; }


        ///// <summary>
        ///// 提供将ResponseData转换为T的操作
        ///// </summary>
        ///// <param name="value">ResponseData实例</param>
        ///// <returns></returns>
        //public static implicit operator T(ResponseData<T> value)
        //{
        //    return value == null ? default(T) : value.Value;
        //}

        ///// <summary>
        ///// 提供将T转换为ResponseData的操作
        ///// </summary>
        ///// <param name="value">T的值</param>
        ///// <returns></returns>
        //public static implicit operator ResponseData<T>(T value)
        //{
        //    return new ResponseData<T>(value);
        //}
    }
}
