using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BaoMen.Common.Extension
{
    /// <summary>
    /// 字符串帮助类
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 将字符串转为32位的MD5编码
        /// </summary>
        /// <param name="str">输入的字符串</param>
        /// <returns>32位编码的字符串</returns>
        public static string To32MD5(this string str)
        {
            //if (string.IsNullOrEmpty(str)) return str;
            //StringBuilder sb = new StringBuilder();
            ////MD5 md5 = MD5.Create();
            ////byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            //MD5 md5 = new MD5CryptoServiceProvider();
            //byte[] s = md5.ComputeHash(Encoding.GetEncoding(0).GetBytes(str));
            //md5.Clear();
            //for (int i = 0; i < s.Length; i++)
            //{
            //    sb.Append(s[i].ToString("x2"));
            //}
            //return sb.ToString();
            return To32MD5(str, Encoding.GetEncoding(0));
        }

        /// <summary>
        /// 将字符串转为32位的MD5编码
        /// </summary>
        /// <param name="str">输入的字符串</param>
        /// <param name="encoding">字符编码</param>
        /// <returns>32位编码的字符串</returns>
        public static string To32MD5(this string str, Encoding encoding)
        {
            if (string.IsNullOrEmpty(str)) return str;
            if (encoding == null) encoding = Encoding.Default;
            StringBuilder sb = new StringBuilder();
            //MD5 md5 = MD5.Create();
            //byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] s = md5.ComputeHash(encoding.GetBytes(str));
            md5.Clear();
            for (int i = 0; i < s.Length; i++)
            {
                sb.Append(s[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 计算字符串的SHA1值
        /// </summary>
        /// <param name="str">输入的字符串</param>
        /// <returns>字符串的SHA1值</returns>
        public static string ToSHA1(this string str)
        {
            //if (string.IsNullOrEmpty(str)) return str;
            //StringBuilder sb = new StringBuilder();
            //SHA1 sha = new SHA1CryptoServiceProvider();
            //// This is one implementation of the abstract class SHA1.
            //byte[] result = sha.ComputeHash(Encoding.Default.GetBytes(str));
            //sha.Clear();
            //for (int i = 0; i < result.Length; i++)
            //{
            //    sb.Append(result[i].ToString("x2"));
            //}
            //return sb.ToString();
            return ToSHA1(str, Encoding.Default);
        }

        /// <summary>
        /// 计算字符串的SHA1值
        /// </summary>
        /// <param name="str">输入的字符串</param>
        /// <param name="encoding">字符编码</param>
        /// <returns>字符串的SHA1值</returns>
        public static string ToSHA1(this string str, Encoding encoding)
        {
            if (string.IsNullOrEmpty(str)) return str;
            if (encoding == null) encoding = Encoding.Default;
            SHA1 sha = new SHA1CryptoServiceProvider();
            //将mystr转换成byte[]
            //byte[] dataToHash = Encoding.ASCII.GetBytes(temp);
            byte[] dataToHash = encoding.GetBytes(str);
            //Hash运算
            byte[] dataHashed = sha.ComputeHash(dataToHash);
            //将运算结果转换成string
            return BitConverter.ToString(dataHashed);
        }

        ///// <summary>
        ///// 加密
        ///// </summary>
        ///// <param name="str">要加密的字符串</param>
        ///// <param name="key">密钥</param>
        ///// <param name="iv">向量</param>
        ///// <returns></returns>
        //public static string Encrypt(this string str, string key, string iv = null)
        //{
        //    if (string.IsNullOrEmpty(str))
        //        throw new ArgumentNullException();
        //    if (string.IsNullOrEmpty(key))
        //        throw new ArgumentNullException("key");
        //    byte[] encrypted;
        //    // Create an Rijndael object
        //    // with the specified key and IV.
        //    using (Rijndael rijAlg = Rijndael.Create())
        //    {
        //        rijAlg.Key = Encoding.UTF8.GetBytes(key);
        //        if (!string.IsNullOrEmpty(iv))
        //            rijAlg.IV = Encoding.UTF8.GetBytes(iv);

        //        // Create an encryptor to perform the stream transform.
        //        ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

        //        // Create the streams used for encryption.
        //        using (MemoryStream msEncrypt = new MemoryStream())
        //        {
        //            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //                {

        //                    //Write all data to the stream.
        //                    swEncrypt.Write(str);
        //                }
        //                encrypted = msEncrypt.ToArray();
        //            }
        //        }
        //    }

        //    // Return the encrypted bytes from the memory stream.
        //    return Convert.ToBase64String(encrypted);
        //}

        ///// <summary>
        ///// 解密
        ///// </summary>
        ///// <param name="str">要解密的字符串</param>
        ///// <param name="key">密钥</param>
        ///// <param name="iv">向量</param>
        ///// <returns></returns>
        //public static string Decrypt(this string str, string key, string iv = null)
        //{
        //    // Check arguments.
        //    if (string.IsNullOrEmpty(str))
        //        throw new ArgumentNullException();
        //    if (string.IsNullOrEmpty(key))
        //        throw new ArgumentNullException("key");

        //    // Declare the string used to hold
        //    // the decrypted text.
        //    string plaintext = null;

        //    // Create an Rijndael object
        //    // with the specified key and IV.
        //    using (Rijndael rijAlg = Rijndael.Create())
        //    {
        //        rijAlg.Key = Encoding.UTF8.GetBytes(key);
        //        if (!string.IsNullOrEmpty(iv))
        //            rijAlg.IV = Encoding.UTF8.GetBytes(iv);

        //        // Create a decryptor to perform the stream transform.
        //        ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

        //        // Create the streams used for decryption.
        //        using (MemoryStream msDecrypt = new MemoryStream(Encoding.UTF8.GetBytes(str)))
        //        {
        //            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        //            {
        //                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
        //                {

        //                    // Read the decrypted bytes from the decrypting stream
        //                    // and place them in a string.
        //                    plaintext = srDecrypt.ReadToEnd();
        //                }
        //            }
        //        }

        //    }

        //    return plaintext;

        //}

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="str">输入的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string EncryptAES(this string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                //Key = Convert.FromBase64String(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="str">输入的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string DecryptAES(this string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
        /// 将旧编码的字符串转化成新编码的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="oldEncodingName">旧编码名称</param>
        /// <param name="newEncodingName">新编码名称</param>
        /// <returns></returns>
        public static string TransferCode(this string str, string oldEncodingName, string newEncodingName)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(oldEncodingName) || string.IsNullOrEmpty(newEncodingName) || oldEncodingName == newEncodingName)
                return str;
            byte[] bytes = Encoding.GetEncoding(oldEncodingName).GetBytes(str);
            return Encoding.GetEncoding(newEncodingName).GetString(bytes);
        }

        /// <summary>
        /// 将旧编码的字符串转化成新编码的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="oldCodePage">旧编码</param>
        /// <param name="newCodePage">新编码</param>
        /// <returns></returns>
        public static string TransferCode(this string str, int oldCodePage, int newCodePage)
        {
            if (string.IsNullOrEmpty(str) || oldCodePage < 0 || newCodePage < 0 || oldCodePage == newCodePage)
                return str;
            byte[] bytes = Encoding.GetEncoding(oldCodePage).GetBytes(str);
            return Encoding.GetEncoding(newCodePage).GetString(bytes);
        }

        /// <summary>
        /// 将旧编码的字符串转化成新编码的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="oldEncoding">旧编码</param>
        /// <param name="newEncoding">新编码</param>
        /// <returns></returns>
        public static string TransferCode(this string str, Encoding oldEncoding, Encoding newEncoding)
        {
            return TransferCode(str, oldEncoding, newEncoding, true);
        }

        /// <summary>
        /// 将旧编码的字符串转化成新编码的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="oldEncoding">旧编码</param>
        /// <param name="newEncoding">新编码</param>
        /// <param name="transfer">是否需要转换</param>
        /// <returns></returns>
        public static string TransferCode(this string str, Encoding oldEncoding, Encoding newEncoding, bool transfer)
        {
            if (!transfer || oldEncoding == null || newEncoding == null || oldEncoding == newEncoding || string.IsNullOrEmpty(str))
                return str;
            byte[] bytes = oldEncoding.GetBytes(str);
            return newEncoding.GetString(bytes);
        }

        /// <summary>
        /// 转为Camel命名规范（首字母小写）
        /// </summary>
        /// <param name="word">单词</param>
        /// <returns></returns>
        public static string ToCamelCase(this string word)
        {
            if (string.IsNullOrEmpty(word)) return word;
            if (char.IsUpper(word[0]))
            {
                char[] chars = word.ToCharArray();
                chars[0] = char.ToLower(chars[0]);
                //for (int i = 1, j = 2; i < chars.Length; i++, j++)
                //{
                //    if (char.IsUpper(chars[i]) && ((j < chars.Length && char.IsUpper(chars[j])) || j == chars.Length))
                //        chars[i] = char.ToLower(chars[i]);
                //    else
                //        break;
                //}
                return new string(chars);
            }
            else
                return word;
            //string camelCaseString = char.ToLower(str[0]).ToString();
            //if (str.Length > 1)
            //    camelCaseString += str.Substring(1);
            //return camelCaseString;
        }

        /// <summary>
        /// 转为Camel命名规范（首字母小写）
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string ToCamelCase(this string str, char separator)
        {
            if (string.IsNullOrEmpty(str)) return str;
            string[] strs = str.ToLower().Split(separator);
            StringBuilder sb = new StringBuilder();
            sb.Append(strs[0]);
            for (int i = 1; i < strs.Length; i++)
            {
                sb.Append(strs[i].ToPascalCase());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 转为Pascal命名规范（首字母大写）
        /// </summary>
        /// <returns></returns>
        public static string ToPascalCase(this string word)
        {
            if (string.IsNullOrEmpty(word)) return word;
            if (char.IsLower(word[0]))
            {
                char[] chars = word.ToCharArray();
                chars[0] = char.ToUpper(chars[0]);
                return new string(chars);
            }
            else
                return word;
        }

        /// <summary>
        /// 转为Pascal命名规范（首字母大写）
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string ToPascalCase(this string str, char separator)
        {
            if (string.IsNullOrEmpty(str)) return str;
            string[] strs = str.ToLower().Split(separator);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strs.Length; i++)
            {
                sb.Append(strs[i].ToPascalCase());
            }
            return sb.ToString();
        }
    }
}
