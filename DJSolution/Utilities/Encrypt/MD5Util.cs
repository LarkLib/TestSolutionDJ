using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现MD5各种长度加密字符、验证MD5等操作。
    /// MD5即Message-Digest Algorithm 5（信息-摘要算法 5），用于确保信息传输完整一致。是计算机广泛使用的散列算法之一（又译摘要算法、哈希算法）。
    /// MD5已经广泛使用在为文件传输提供一定的可靠性方面。例如，服务器预先提供一个MD5校验和，用户下载完文件以后，用MD5算法计算下载文件的MD5校验和，
    /// 然后通过检查这两个校验和是否一致，就能判断下载的文件是否出错。 
    /// </summary>
    public class MD5Util
    {
        /// <summary>    
        /// 获得32位的MD5加密    
        /// </summary>  
        public static string GetMD5_32(string input)
        {
            MD5 mD = MD5.Create();
            byte[] array = mD.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.Append(array[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }
        /// <summary>    
        /// 获得16位的MD5加密    
        /// </summary>  
        public static string GetMD5_16(string input)
        {
            return MD5Util.GetMD5_32(input).Substring(8, 16);
        }
        /// <summary>    
        /// 获得8位的MD5加密    
        /// </summary> 
        public static string GetMD5_8(string input)
        {
            return MD5Util.GetMD5_32(input).Substring(8, 8);
        }
        /// <summary>    
        /// 获得4位的MD5加密    
        /// </summary> 
        public static string GetMD5_4(string input)
        {
            return MD5Util.GetMD5_32(input).Substring(8, 4);
        }
        /// <summary>    
        /// 添加MD5的前缀，便于检查有无篡改    
        /// </summary>
        public static string AddMD5Profix(string input)
        {
            return MD5Util.GetMD5_4(input) + input;
        }
        /// <summary>    
        /// 移除MD5的前缀    
        /// </summary>
        public static string RemoveMD5Profix(string input)
        {
            return input.Substring(4);
        }
        /// <summary>    
        /// 验证MD5前缀处理的字符串有无被篡改    
        /// </summary>  
        public static bool ValidateValue(string input)
        {
            bool result = false;
            if (input.Length >= 4)
            {
                string input2 = input.Substring(4);
                if (input.Substring(0, 4) == MD5Util.GetMD5_4(input2))
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>    
        /// 对给定文件路径的文件加上标签    
        /// </summary>    
        /// <param name="path">要加密的文件的路径</param>    
        /// <returns>标签的值</returns> 
        public static bool AddMD5(string path)
        {
            bool flag = true;
            if (MD5Util.CheckMD5(path))
            {
                flag = false;
            }
            bool result;
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] array = new byte[(int)((object)((IntPtr)fileStream.Length))];
                fileStream.Read(array, 0, (int)fileStream.Length);
                fileStream.Close();
                if (flag)
                {
                    string s = MD5Util.smethod_0(array, 0, array.Length);
                    byte[] bytes = Encoding.ASCII.GetBytes(s);
                    FileStream fileStream2 = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                    fileStream2.Write(array, 0, array.Length);
                    fileStream2.Write(bytes, 0, bytes.Length);
                    fileStream2.Close();
                }
                else
                {
                    FileStream fileStream2 = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                    fileStream2.Write(array, 0, array.Length);
                    fileStream2.Close();
                }
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
        /// 对给定路径的文件进行验证，如果一致返回True，否则返回False    
        /// </summary>    
        /// <param name="path"></param>    
        /// <returns>是否加了标签或是否标签值与内容值一致</returns> 
        public static bool CheckMD5(string path)
        {
            bool result;
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] array = new byte[(int)((object)((IntPtr)fileStream.Length))];
                fileStream.Read(array, 0, (int)fileStream.Length);
                fileStream.Close();
                string a = MD5Util.smethod_0(array, 0, array.Length - 32);
                string @string = Encoding.ASCII.GetString(array, array.Length - 32, 32);
                result = (a == @string);
            }
            catch
            {
                result = false;
            }
            return result;
        }
        private static string smethod_0(byte[] byte_0, int int_0, int int_1)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] value = mD5CryptoServiceProvider.ComputeHash(byte_0, int_0, int_1);
            string text = BitConverter.ToString(value);
            return text.Replace("-", "");
        }
        private void method_0()
        {
            string text = "i love u";
            text = MD5Util.AddMD5Profix(text);
            Console.WriteLine(text);
            Console.WriteLine(MD5Util.ValidateValue(text));
            text = MD5Util.RemoveMD5Profix(text);
            Console.WriteLine(text);
        }
    }
}
