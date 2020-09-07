using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace BaoMen.Common.Extension
{
    /// <summary>
    /// 对象帮助
    /// </summary>
    public static class ObjectExtension
    {
        #region Clone
        /// <summary>
        /// 克隆(深拷贝)。对象必须是可序列化的类的实例。
        /// </summary>
        /// <param name="value">输入值。可以为null</param>
        /// <returns>新值</returns>
        private static object CloneObject(object value)
        {
            if (value == null)
                return null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, value);
                memoryStream.Position = 0;
                return formatter.Deserialize(memoryStream);
            }
        }

        /// <summary>
        /// 克隆(深拷贝)。对象必须是可序列化的类的实例
        /// </summary>
        /// <typeparam name="T">输入值的类型</typeparam>
        /// <param name="value">输入值。必须是可序列化的。</param>
        /// <returns>新值</returns>
        public static T Clone<T>(this T value)
        {
            return (T)CloneObject(value);
        }
        #endregion

        #region Serialize/Deserialize
        ///// <summary>
        ///// 将对象序列化为xml
        ///// </summary>
        ///// <typeparam name="T">对象类型</typeparam>
        ///// <param name="value">对象实例</param>
        ///// <returns></returns>
        //public static string SerializeXml<T>(T value)
        //    where T : class
        //{
        //    if (value == null) return null;
        //    Type serializerType = typeof(T);
        //    XmlSerializer xmlSerializer = new XmlSerializer(serializerType);
        //    using (StringWriter writer = new StringWriter())
        //    {
        //        xmlSerializer.Serialize(writer, value);
        //        return writer.ToString();
        //    }
        //}
        /// <summary>
        /// 将对象序列化为xml
        /// </summary>
        /// <param name="value">对象实例</param>
        /// <returns></returns>
        public static string SerializeXml(this object value)
        {
            if (value == null) return null;
            Type serializerType = value.GetType();
            XmlSerializer xmlSerializer = new XmlSerializer(serializerType);
            using (StringWriter writer = new StringWriter())
            {
                xmlSerializer.Serialize(writer, value);
                return writer.ToString();
            }
        }

        /// <summary>
        /// 将Xml反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="xml">xml</param>
        /// <returns></returns>
        public static T DeserializeXml<T>(string xml)
            where T : class
        {
            if (string.IsNullOrEmpty(xml)) return null;
            using (StringReader stringReader = new StringReader(xml))
            {
                //XmlReader xmlReader = XmlReader.Create(stringReader);
                Type serializerType = typeof(T);
                XmlSerializer xmlSerializer = new XmlSerializer(serializerType);
                return (T)xmlSerializer.Deserialize(stringReader);
            }
        }

        /// <summary>
        /// 将Xml反序列化为对象
        /// </summary>
        /// <param name="xml">xml</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static object DeserializeXml(string xml, Type type)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            using (StringReader stringReader = new StringReader(xml))
            {
                //XmlReader xmlReader = XmlReader.Create(stringReader);
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                return xmlSerializer.Deserialize(stringReader);
            }
        }
        /// <summary>
        /// 序列化对象。对象必须是可序列化的类。
        /// </summary>
        /// <param name="value">要序列化的对象实例。可以为null</param>
        /// <returns>序列化后的对象。如果输入为null则返回null</returns>
        public static byte[] Serialize(this object value)
        {
            if (value == null)
            {
                return null;
            }

            byte[] inMemoryBytes;
            using (MemoryStream inMemoryData = new MemoryStream())
            {
                new BinaryFormatter().Serialize(inMemoryData, value);
                inMemoryBytes = inMemoryData.ToArray();
            }

            return inMemoryBytes;

            //if (value == null)
            //    throw new ArgumentNullException("object");
            //Stream stream = new MemoryStream();
            //BinaryFormatter formatter = new BinaryFormatter();
            //formatter.Serialize(stream, value);
            //byte[] bytes = new byte[stream.Length];
            //stream.Position = 0;
            //stream.Read(bytes, 0, (int)stream.Length);
            //stream.Close();
            //return bytes;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="serializedObject">序列化后的对象。可以为null</param>
        /// <returns>反序列化后的对象。如果输入为null则返回null</returns>
        public static object Deserialize(byte[] serializedObject)
        {
            if (serializedObject == null)
            {
                return null;
            }

            using (MemoryStream dataInMemory = new MemoryStream(serializedObject))
            {
                return new BinaryFormatter().Deserialize(dataInMemory);
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="serializedObject">序列化后的对象</param>
        /// <returns></returns>
        public static T Deserialize<T>(byte[] serializedObject)
            where T : class
        {
            return (T)Deserialize(serializedObject);
        }

        /// <summary>
        /// 反序列化。如果出现异常，返回null
        /// </summary>
        /// <param name="serializedObject">序列化后的对象</param>
        /// <returns></returns>
        public static object TryDeserialize(byte[] serializedObject)
        {
            try
            {
                return Deserialize(serializedObject);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 反序列化。如果出现异常，返回null
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="serializedObject">序列化后的对象</param>
        /// <returns></returns>
        public static T TryDeserialize<T>(byte[] serializedObject)
            where T : class
        {
            try
            {
                return Deserialize<T>(serializedObject);
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
