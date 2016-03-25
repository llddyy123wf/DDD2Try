using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Library.Security
{
    public static class SecurityHelper
    {
        //AES密钥向量
        private static readonly byte[] aeskeys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// SHA1哈希
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>哈希后的字符串</returns>
        public static string HashBySHA1(this string source)
        {
            if (string.IsNullOrEmpty(source)) return source;

            HashAlgorithm sha1Provider = new SHA1CryptoServiceProvider();
            byte[] reses = sha1Provider.ComputeHash(Encoding.UTF8.GetBytes(source));

            StringBuilder sbResult = new StringBuilder();
            foreach (var res in reses)
            {
                sbResult.Append(res.ToString("x2"));
            }

            return sbResult.ToString();
        }

        /// <summary>
        /// SHA256哈希
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>哈希后的字符串</returns>
        public static string HashBySHA256(this string source)
        {
            if (string.IsNullOrEmpty(source)) return source;

            HashAlgorithm sha1Provider = new SHA256CryptoServiceProvider();
            byte[] reses = sha1Provider.ComputeHash(Encoding.UTF8.GetBytes(source));

            StringBuilder sbResult = new StringBuilder();
            foreach (var res in reses)
            {
                sbResult.Append(res.ToString("x2"));
            }

            return sbResult.ToString();
        }

        /// <summary>
        /// MD5哈希
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>哈希后的字符串</returns>
        public static string HashByMD5(this string source)
        {
            if (string.IsNullOrEmpty(source)) return source;

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] reses = md5.ComputeHash(Encoding.UTF8.GetBytes(source));

            StringBuilder sbResult = new StringBuilder();
            foreach (var res in reses)
            {
                sbResult.Append(res.ToString("x").PadLeft(2, '0'));
            }

            return sbResult.ToString();

        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptSource">加密字符串</param>
        /// <param name="encryptKey">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string AESEncrypt(string encryptSource, string encryptKey)
        {
            if (string.IsNullOrWhiteSpace(encryptSource)) return string.Empty;
            encryptKey = (encryptKey.Length >= 32 ? encryptKey.Substring(0, 32) : encryptKey.PadLeft(32, ' '));

            // 分组加密算法
            SymmetricAlgorithm des = Rijndael.Create();
            // 得到需要加密的字节数组
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptSource);
            // 设置密钥及密钥向量
            des.Key = Encoding.UTF8.GetBytes(encryptKey);
            des.IV = aeskeys;
            byte[] cipherBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cipherBytes = ms.ToArray();
                    cs.Close();
                    ms.Close();
                }
            }
            return Convert.ToBase64String(cipherBytes);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="decryptSource">解密字符串</param>
        /// <param name="decryptKey">密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string AESDecrypt(string decryptSource, string decryptKey)
        {
            if (string.IsNullOrWhiteSpace(decryptSource)) return string.Empty;
            decryptKey = (decryptKey.Length >= 32 ? decryptKey.Substring(0, 32) : decryptKey.PadLeft(32, ' '));

            byte[] cipherText = Convert.FromBase64String(decryptSource);
            SymmetricAlgorithm des = Rijndael.Create();
            des.Key = Encoding.UTF8.GetBytes(decryptKey);
            des.IV = aeskeys;
            byte[] decryptBytes = new byte[cipherText.Length];
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(decryptBytes, 0, decryptBytes.Length);
                    cs.Close();
                    ms.Close();
                }
            }
            return Encoding.UTF8.GetString(decryptBytes).TrimEnd('\0');
        }
    }
}
