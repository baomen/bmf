using System;

namespace BaoMen.Common.Converter
{
    /// <summary>
    /// 时间日期转换器
    /// </summary>
    public class DateTimeConverter
    {
        /// <summary>
        /// 将unix的时间戳转为时间日期
        /// </summary>
        /// <param name="unixTimeStamp">unix的时间戳,单位为毫秒</param>
        /// <returns>时间日期，如果转换失败则返回空</returns>
        public static DateTime FromUnixTimeStampMillisecond(long unixTimeStamp)
        { 
            //long timeStamp = unixTimeStamp * 10000000;
            long timeStamp = unixTimeStamp * 10000;
            //DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime startTime = System.TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return startTime.Add(new TimeSpan(timeStamp));
        }

        /// <summary>
        /// 将unix的时间戳转为时间日期
        /// </summary>
        /// <param name="unixTimeStamp">unix的时间戳，单位为秒</param>
        /// <returns>时间日期，如果转换失败则返回空</returns>
        public static DateTime FromUnixTimeStampSecond(long unixTimeStamp)
        {
            long timeStamp = unixTimeStamp * 10000000;
            //DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime startTime = System.TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return startTime.Add(new TimeSpan(timeStamp));
        }

        /// <summary>
        /// 时间日期转换为unix时间戳
        /// </summary>
        /// <param name="dateTime">需要转换的时间日期</param>
        /// <returns></returns>
        public static long ToUnixTimeStamp(System.DateTime dateTime)
        {
            //System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            DateTime startTime = System.TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return (long)(dateTime - startTime).TotalSeconds;
        }

        /// <summary>
        /// 时间日期格式转换为yyyyMMddHHmmss的字符串
        /// </summary>
        /// <param name="dateTime">需要转换的时间日期</param>
        /// <returns></returns>
        public static string ToCharacter14(System.DateTime dateTime)
        {
            return dateTime.ToString(Constant.DateTimeFormat.Character14);
        }

        /// <summary>
        /// 时间日期格式转换为yyyyMMddHHmmssfff的字符串
        /// </summary>
        /// <param name="dateTime">需要转换的时间日期</param>
        /// <returns></returns>
        public static string ToCharacter17(System.DateTime dateTime)
        {
            return dateTime.ToString(Constant.DateTimeFormat.Character17);
        }

        /// <summary>
        /// 将格式为yyyyMMddHHmmss的字符串转换为时间日期，如果转换失败返回null
        /// </summary>
        /// <param name="value">需要转换的时间日期字符串</param>
        /// <returns></returns>
        public static DateTime? FromCharacter14(string value)
        {
            DateTime datetime;
            if (DateTime.TryParseExact(value,
                Constant.DateTimeFormat.Character14,
                null,
                System.Globalization.DateTimeStyles.None,
                out datetime))
                return datetime;
            return null;
        }

        /// <summary>
        /// 将格式为yyyyMMddHHmmssfff的字符串转换为时间日期，如果转换失败返回null
        /// </summary>
        /// <param name="value">需要转换的时间日期字符串</param>
        /// <returns></returns>
        public static DateTime? FromCharacter17(string value)
        {
            DateTime datetime;
            if (DateTime.TryParseExact(value,
                Constant.DateTimeFormat.Character17,
                null,
                System.Globalization.DateTimeStyles.None,
                out datetime))
                return datetime;
            return null;
        }

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="value">需要转换的时间日期字符串</param>
        /// <returns></returns>
        public static DateTime? FromLongYearToSecond(string value)
        {
            DateTime datetime;
            if (DateTime.TryParseExact(value,
                Constant.DateTimeFormat.LongYearToSecond,
                null,
                System.Globalization.DateTimeStyles.None,
                out datetime))
                return datetime;
            return null;
        }
    }
}
