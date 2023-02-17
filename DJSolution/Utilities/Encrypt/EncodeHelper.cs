using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现DES对称加解密、AES RijndaelManaged加解密、Base64加密解密、MD5加密等操作。 
    /// </summary>
    public sealed class EncodeHelper
    {
        public const string DEFAULT_ENCRYPT_KEY = "12345678";
        private static readonly string string_0 = "@#kim123";
        private static byte[] byte_0 = new byte[]
		{
			65, 
			114, 
			101, 
			121, 
			111, 
			117, 
			109, 
			121, 
			83, 
			110, 
			111, 
			119, 
			109, 
			97, 
			110, 
			63
		};
        /// <summary>    
        /// 使用默认加密    
        /// </summary>    
        /// <param name="strText"></param>    
        /// <returns></returns>  
        public static string DesEncrypt(string strText)
        {
            string result;
            try { result = EncodeHelper.DesEncrypt(strText, "12345678"); }
            catch { result = ""; }
            return result;
        }
        /// <summary>    
        /// 使用默认解密    
        /// </summary>    
        /// <param name="strText">解密字符串</param>    
        /// <returns></returns> 
        public static string DesDecrypt(string strText)
        {
            string result;
            try { result = EncodeHelper.DesDecrypt(strText, "12345678"); }
            catch { result = ""; }
            return result;
        }
        /// <summary>     
        /// 加密字符串,注意strEncrKey的长度为8位    
        /// </summary>     
        /// <param name="strText">待加密字符串</param>     
        /// <param name="strEncrKey">加密键</param>     
        /// <returns></returns>   
        public static string DesEncrypt(string strText, string strEncrKey)
        {
            byte[] rgbIV = new byte[] { 18, 52, 86, 120, 144, 171, 205, 239 };
            byte[] bytes = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            byte[] bytes2 = Encoding.UTF8.GetBytes(strText);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(bytes, rgbIV), CryptoStreamMode.Write);
            cryptoStream.Write(bytes2, 0, bytes2.Length);
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(memoryStream.ToArray());
        }
        /// <summary>     
        /// 解密字符串,注意strEncrKey的长度为8位    
        /// </summary>     
        /// <param name="strText">待解密的字符串</param>     
        /// <param name="sDecrKey">解密键</param>     
        /// <returns>解密后的字符串</returns>   
        public static string DesDecrypt(string strText, string sDecrKey)
        {
            byte[] rgbIV = new byte[] {	18,	52,	86,	120, 144, 171, 205,	239 };
            byte[] array = new byte[strText.Length];
            byte[] bytes = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            array = Convert.FromBase64String(strText);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(bytes, rgbIV), CryptoStreamMode.Write);
            cryptoStream.Write(array, 0, array.Length);
            cryptoStream.FlushFinalBlock();
            Encoding encoding = new UTF8Encoding();
            return encoding.GetString(memoryStream.ToArray());
        }
        /// <summary>     
        /// 加密数据文件,注意strEncrKey的长度为8位    
        /// </summary>     
        /// <param name="m_InFilePath">待加密的文件路径</param>     
        /// <param name="m_OutFilePath">输出文件路径</param>     
        /// <param name="strEncrKey">加密键</param>  
        public static void DesEncrypt(string m_InFilePath, string m_OutFilePath, string strEncrKey)
        {
            byte[] rgbIV = new byte[] { 18, 52, 86, 120, 144, 171, 205, 239 };
            byte[] bytes = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            FileStream fileStream = new FileStream(m_InFilePath, FileMode.Open, FileAccess.Read);
            FileStream fileStream2 = new FileStream(m_OutFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream2.SetLength(0L);
            byte[] buffer = new byte[100];
            long num = 0L;
            long length = fileStream.Length;
            DES dES = new DESCryptoServiceProvider();
            CryptoStream cryptoStream = new CryptoStream(fileStream2, dES.CreateEncryptor(bytes, rgbIV), CryptoStreamMode.Write);
            while (num < length)
            {
                int num2 = fileStream.Read(buffer, 0, 100);
                cryptoStream.Write(buffer, 0, num2);
                num += (long)num2;
            }
            cryptoStream.Close();
            fileStream2.Close();
            fileStream.Close();
        }
        /// <summary>     
        /// 解密数据文件,注意strEncrKey的长度为8位    
        /// </summary>     
        /// <param name="m_InFilePath">待解密的文件路径</param>     
        /// <param name="m_OutFilePath">输出路径</param>     
        /// <param name="sDecrKey">解密键</param>  
        public static void DesDecrypt(string m_InFilePath, string m_OutFilePath, string sDecrKey)
        {
            byte[] rgbIV = new byte[]{ 18, 52, 86, 120, 144, 171, 205, 239 };
            byte[] bytes = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
            FileStream fileStream = new FileStream(m_InFilePath, FileMode.Open, FileAccess.Read);
            FileStream fileStream2 = new FileStream(m_OutFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream2.SetLength(0L);
            byte[] buffer = new byte[100];
            long num = 0L;
            long length = fileStream.Length;
            DES dES = new DESCryptoServiceProvider();
            CryptoStream cryptoStream = new CryptoStream(fileStream2, dES.CreateDecryptor(bytes, rgbIV), CryptoStreamMode.Write);
            while (num < length)
            {
                int num2 = fileStream.Read(buffer, 0, 100);
                cryptoStream.Write(buffer, 0, num2);
                num += (long)num2;
            }
            cryptoStream.Close();
            fileStream2.Close();
            fileStream.Close();
        }
        /// <summary>    
        /// 对称加密算法AES RijndaelManaged加密(RijndaelManaged（AES）算法是块式加密算法)    
        /// </summary>    
        /// <param name="encryptString">待加密字符串</param>    
        /// <returns>加密结果字符串</returns>  
        public static string AES_Encrypt(string encryptString)
        {
            return EncodeHelper.AES_Encrypt(encryptString, EncodeHelper.string_0);
        }
        /// <summary>    
        /// 对称加密算法AES RijndaelManaged加密(RijndaelManaged（AES）算法是块式加密算法)    
        /// </summary>    
        /// <param name="encryptString">待加密字符串</param>    
        /// <param name="encryptKey">加密密钥，须半角字符</param>    
        /// <returns>加密结果字符串</returns> 
        public static string AES_Encrypt(string encryptString, string encryptKey)
        {
            encryptKey = EncodeHelper.smethod_0(encryptKey, 32, "");
            encryptKey = encryptKey.PadRight(32, ' ');
            ICryptoTransform cryptoTransform = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32)),
                IV = EncodeHelper.byte_0
            }.CreateEncryptor();
            byte[] bytes = Encoding.UTF8.GetBytes(encryptString);
            byte[] inArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
            return Convert.ToBase64String(inArray);
        }
        /// <summary>    
        /// 对称加密算法AES RijndaelManaged解密字符串    
        /// </summary>    
        /// <param name="decryptString">待解密的字符串</param>    
        /// <returns>解密成功返回解密后的字符串,失败返源串</returns>  
        public static string AES_Decrypt(string decryptString)
        {
            return EncodeHelper.AES_Decrypt(decryptString, EncodeHelper.string_0);
        }
        /// <summary>    
        /// 对称加密算法AES RijndaelManaged解密字符串    
        /// </summary>    
        /// <param name="decryptString">待解密的字符串</param>    
        /// <param name="decryptKey">解密密钥,和加密密钥相同</param>    
        /// <returns>解密成功返回解密后的字符串,失败返回空</returns> 
        public static string AES_Decrypt(string decryptString, string decryptKey)
        {
            string result;
            try
            {
                decryptKey = EncodeHelper.smethod_0(decryptKey, 32, "");
                decryptKey = decryptKey.PadRight(32, ' ');
                ICryptoTransform cryptoTransform = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(decryptKey),
                    IV = EncodeHelper.byte_0
                }.CreateDecryptor();
                byte[] array = Convert.FromBase64String(decryptString);
                byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
                result = Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }
        private static string smethod_0(string string_1, int int_0, string string_2)
        {
            return EncodeHelper.smethod_1(string_1, 0, int_0, string_2);
        }
        private static string smethod_1(string string_1, int int_0, int int_1, string string_2)
        {
            string result;
            if (Regex.IsMatch(string_1, "[ࠀ-一]+") || Regex.IsMatch(string_1, "[가-힣]+"))
            {
                if (int_0 >= string_1.Length)
                {
                    result = string.Empty;
                }
                else
                {
                    result = string_1.Substring(int_0, (int_1 + int_0 > string_1.Length) ? (string_1.Length - int_0) : int_1);
                }
            }
            else
            {
                if (int_1 <= 0)
                {
                    result = string.Empty;
                }
                else
                {
                    byte[] bytes = Encoding.Default.GetBytes(string_1);
                    if (bytes.Length > int_0)
                    {
                        int num = bytes.Length;
                        if (bytes.Length > int_0 + int_1)
                        {
                            num = int_1 + int_0;
                        }
                        else
                        {
                            int_1 = bytes.Length - int_0;
                            string_2 = "";
                        }
                        int[] array = new int[int_1];
                        int num2 = 0;
                        for (int i = int_0; i < num; i++)
                        {
                            if (bytes[i] > 127)
                            {
                                num2++;
                                if (num2 == 3)
                                {
                                    num2 = 1;
                                }
                            }
                            else
                            {
                                num2 = 0;
                            }
                            array[i] = num2;
                        }
                        if (bytes[num - 1] > 127 && array[int_1 - 1] == 1)
                        {
                            int_1++;
                        }
                        byte[] array2 = new byte[int_1];
                        Array.Copy(bytes, int_0, array2, 0, int_1);
                        string text = Encoding.Default.GetString(array2);
                        text += string_2;
                        result = text;
                    }
                    else
                    {
                        result = string.Empty;
                    }
                }
            }
            return result;
        }
        /// <summary>    
        /// 加密文件流    
        /// </summary>    
        /// <param name="fs">文件流</param>    
        /// <returns></returns>    
        public static CryptoStream AES_EncryptStrream(FileStream fs, string decryptKey)
        {
            decryptKey = EncodeHelper.smethod_0(decryptKey, 32, "");
            decryptKey = decryptKey.PadRight(32, ' ');
            ICryptoTransform transform = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(decryptKey),
                IV = EncodeHelper.byte_0
            }.CreateEncryptor();
            return new CryptoStream(fs, transform, CryptoStreamMode.Write);
        }
        /// <summary>    
        /// 解密文件流    
        /// </summary>    
        /// <param name="fs">文件流</param>    
        /// <returns></returns>  
        public static CryptoStream AES_DecryptStream(FileStream fs, string decryptKey)
        {
            decryptKey = EncodeHelper.smethod_0(decryptKey, 32, "");
            decryptKey = decryptKey.PadRight(32, ' ');
            ICryptoTransform transform = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(decryptKey),
                IV = EncodeHelper.byte_0
            }.CreateDecryptor();
            return new CryptoStream(fs, transform, CryptoStreamMode.Read);
        }
        /// <summary>    
        /// 对指定文件加密    
        /// </summary>    
        /// <param name="InputFile">输入文件</param>    
        /// <param name="OutputFile">输出文件</param>    
        /// <returns></returns>   
        public static bool AES_EncryptFile(string InputFile, string OutputFile)
        {
            bool result;
            try
            {
                string decryptKey = "www.elite.com";
                FileStream fileStream = new FileStream(InputFile, FileMode.Open);
                FileStream fileStream2 = new FileStream(OutputFile, FileMode.Create);
                CryptoStream cryptoStream = EncodeHelper.AES_EncryptStrream(fileStream2, decryptKey);
                byte[] array = new byte[(int)((object)((IntPtr)fileStream.Length))];
                fileStream.Read(array, 0, array.Length);
                cryptoStream.Write(array, 0, array.Length);
                cryptoStream.Close();
                fileStream.Close();
                fileStream2.Close();
            }
            catch
            {
                result = false;
                return result;
            }
            result = true;
            return result;
        }
        /// <summary>    
        /// 对指定的文件解密   
        /// </summary>    
        /// <param name="InputFile">输入文件</param>    
        /// <param name="OutputFile">输出文件</param>    
        /// <returns></returns>    
        public static bool AES_DecryptFile(string InputFile, string OutputFile)
        {
            bool result;
            try
            {
                string decryptKey = "www.elite.com";
                FileStream fileStream = new FileStream(InputFile, FileMode.Open);
                FileStream fileStream2 = new FileStream(OutputFile, FileMode.Create);
                CryptoStream cryptoStream = EncodeHelper.AES_DecryptStream(fileStream, decryptKey);
                byte[] array = new byte[1024];
                int num;
                do
                {
                    num = cryptoStream.Read(array, 0, array.Length);
                    fileStream2.Write(array, 0, num);
                }
                while (num >= array.Length);
                cryptoStream.Close();
                fileStream.Close();
                fileStream2.Close();
            }
            catch
            {
                result = false;
                return result;
            }
            result = true;
            return result;
        }
        /// <summary>    
        /// Base64是一种使用64基的位置计数法。它使用2的最大次方来代表仅可列印的ASCII 字元。    
        /// 这使它可用来作为电子邮件的传输编码。在Base64中的变数使用字元A-Z、a-z和0-9 ，    
        /// 这样共有62个字元，用來作为开始的64个数字，最后两个用来作为数字的符号在不同的    
        /// 系統中而不同。    
        /// Base64加密    
        /// </summary>    
        /// <param name="str">Base64方式加密字符串</param>    
        /// <returns></returns>    
        public static string Base64Encrypt(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>    
        /// Base64解密字符串    
        /// </summary>    
        /// <param name="str">待解密的字符串</param>    
        /// <returns></returns>   
        public static string Base64Decrypt(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(bytes);
        }
        /// <summary>     
        /// 使用MD5加密字符串    
        /// </summary>     
        /// <param name="strText">待加密的字符串</param>     
        /// <returns>MD5加密后的字符串</returns>  
        public static string MD5Encrypt(string strText)
        {
            MD5 mD = new MD5CryptoServiceProvider();
            byte[] bytes = mD.ComputeHash(Encoding.Default.GetBytes(strText));
            return Encoding.Default.GetString(bytes);
        }
        /// <summary>    
        /// 使用MD5加密的Hash表    
        /// </summary>    
        /// <param name="input">待加密字符串</param>    
        /// <returns></returns> 
        public static string MD5EncryptHash(string input)
        {
            MD5 mD = new MD5CryptoServiceProvider();
            byte[] array = mD.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
            char[] array2 = new char[array.Length];
            Array.Copy(array, array2, array.Length);
            return new string(array2);
        }
        /// <summary>    
        /// 使用Md5加密为16进制字符串    
        /// </summary>    
        /// <param name="input">待加密字符串</param>    
        /// <returns></returns>    
        public static string MD5EncryptHashHex(string input)
        {
            MD5 mD = new MD5CryptoServiceProvider();
            byte[] array = mD.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
            string text = string.Empty;
            for (int i = 0; i < array.Length; i++)
            {
                text += Uri.HexEscape((char)array[i]);
            }
            text = text.Replace("%", "");
            text = text.ToLower();
            return text;
        }
        /// <summary>    
        /// MD5 三次加密算法.计算过程: (QQ使用)    
        /// 1. 验证码转为大写    
        /// 2. 将密码使用这个方法进行三次加密后,与验证码进行叠加    
        /// 3. 然后将叠加后的内容再次MD5一下,得到最终验证码的值    
        /// </summary>    
        /// <param name="s"></param>    
        /// <returns></returns>  
        public static string EncyptMD5_3_16(string s)
        {
            MD5 mD = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            byte[] buffer = mD.ComputeHash(bytes);
            byte[] buffer2 = mD.ComputeHash(buffer);
            byte[] array = mD.ComputeHash(buffer2);
            StringBuilder stringBuilder = new StringBuilder();
            byte[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                byte b = array2[i];
                stringBuilder.Append(b.ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString().ToUpper();
        }
        /// <summary>    
        /// SHA256函数    
        /// </summary>    
        /// <param name="str">原始字符串</param>    
        /// <returns>SHA256结果(返回长度为44字节的字符串)</returns> 
        public static string SHA256(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            SHA256Managed sHA256Managed = new SHA256Managed();
            byte[] inArray = sHA256Managed.ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
        /// <summary>    
        /// 加密字符串（使用MD5+Base64混合加密）    
        /// </summary>    
        /// <param name="input">待加密的字符串</param>    
        /// <returns></returns>   
        public static string EncryptString(string input)
        {
            return MD5Util.AddMD5Profix(Base64Util.Encrypt(MD5Util.AddMD5Profix(input)));
        }
        /// <summary>    
        /// 解密加过密的字符串（使用MD5+Base64混合加密）    
        /// </summary>    
        /// <param name="input">待解密的字符串</param>    
        /// <param name="throwException">解密失败是否抛异常</param>    
        /// <returns></returns>    
        public static string DecryptString(string input, bool throwException)
        {
            string result;
            try
            {
                if (!MD5Util.ValidateValue(input))
                {
                    throw new Exception("字符串无法转换成功！");
                }
                result = MD5Util.RemoveMD5Profix(Base64Util.Decrypt(MD5Util.RemoveMD5Profix(input)));
            }
            catch
            {
                if (throwException)
                {
                    throw;
                }
                result = "";
            }
            return result;
        }
    }
}
