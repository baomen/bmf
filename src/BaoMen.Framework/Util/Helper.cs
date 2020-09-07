using BaoMen.Common.Model;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BaoMen.Framework.Util
{
    /// <summary>
    /// 帮助类
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// 将JObject转换为具体的类型
        /// </summary>
        /// <typeparam name="T">具体的类型</typeparam>
        /// <param name="value">objec包装过的JObject对象</param>
        /// <returns></returns>
        public static T Parse<T>(object value)
            where T : class
        {
            if (value == null) return null;
            JObject jObject = value as JObject;
            if (jObject == null)
            {
                return null;
            }
            return jObject.ToObject<T>();
        }

        /// <summary>
        /// 从百分比的值转换为decimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal FromPercent(decimal value)
        {
            return Math.Round(value / 100, 4);
        }

        /// <summary>
        /// 四舍六入五成双算法计算费用
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetFee(decimal value)
        {
            return (int)Math.Round(value, 0);
        }

        /// <summary>
        /// 计算不含税费用
        /// </summary>
        /// <param name="fee"></param>
        /// <param name="taxRate"></param>
        /// <returns></returns>
        public static int GetFeeWithoutTax(int fee, decimal taxRate)
        {
            if (taxRate == 0) return fee;
            decimal tax = Util.Helper.FromPercent(taxRate);
            return GetFee(fee / (1 + tax));
        }

        /// <summary>
        /// http下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="file">文件存放地址，包含文件名</param>
        /// <returns></returns>
        public static ResponseData HttpDownload(string url, string file)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            LogEventInfo log = new LogEventInfo
            {
                LoggerName = logger.Name
            };
            log.Properties["url"] = url;
            log.Properties["file"] = file;
            ResponseData responseData = new ResponseData();
            try
            {
                log.Level = LogLevel.Debug;
                string folder = Path.GetDirectoryName(file);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 300000; //300秒
                                          //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                //Stream stream = new FileStream(tempFile, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    //stream.Write(bArr, 0, size);
                    fs.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                //stream.Close();
                fs.Close();
                responseStream.Close();
            }
            catch (Exception e)
            {
                responseData.ErrorNumber = 1;
                responseData.ErrorMessage = "下载文件失败";
                responseData.Exception = e;
                log.Level = LogLevel.Warn;
                log.Exception = e;
            }
            logger.Log(log);
            return responseData;
        }
    }
}
