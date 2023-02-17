using System;
using System.Collections.Generic;
using System.Text;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现基于Base64的加密编码。
    /// Base64被定义为：Base64内容传送编码被设计
    /// 用来把任意序列的8位字节描述为一种不易被人直接识别的形式。
    /// </summary>
    public class Base64Util
    {
        protected static Base64Util s_b64 = new Base64Util();
        protected string m_codeTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZbacdefghijklmnopqrstu_wxyz0123456789*-";
        protected string m_pad = "v";
        protected Dictionary<int, char> m_t1 = new Dictionary<int, char>();
        protected Dictionary<char, int> m_t2 = new Dictionary<char, int>();
        public string CodeTable
        {
            get { return this.m_codeTable; }
            set
            {
                if (value == null)
                {
                    throw new Exception("密码表不能为null");
                }
                if (value.Length < 64)
                {
                    throw new Exception("密码表长度必须至少为64");
                }
                this.ValidateRepeat(value);
                this.ValidateEqualPad(value, this.m_pad);
                this.m_codeTable = value;
                this.InitDict();
            }
        }
        public string Pad
        {
            get { return this.m_pad; }
            set
            {
                if (value == null)
                {
                    throw new Exception("密码表的补码不能为null");
                }
                if (value.Length != 1)
                {
                    throw new Exception("密码表的补码长度必须为1");
                }
                this.ValidateEqualPad(this.m_codeTable, value);
                this.m_pad = value;
                this.InitDict();
            }
        }
        public Base64Util()
        {
            this.InitDict();
        }
        /// <summary>    
        /// 使用默认的密码表加密字符串    
        /// </summary>    
        /// <param name="input">待加密字符串</param>    
        /// <returns></returns> 
        public static string Encrypt(string input)
        {
            return Base64Util.s_b64.Encode(input);
        }
        /// <summary>    
        /// 使用默认的密码表解密字符串    
        /// </summary>    
        /// <param name="input">待解密字符串</param>    
        /// <returns></returns>  
        public static string Decrypt(string input)
        {
            return Base64Util.s_b64.Decode(input);
        }
        /// <summary>    
        /// 获取具有标准的Base64密码表的加密类    
        /// </summary>    
        /// <returns></returns>  
        public static Base64Util GetStandardBase64()
        {
            return new Base64Util
            {
                Pad = "=",
                CodeTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"
            };
        }
        public string Encode(string source)
        {
            string result;
            if (source == null || source == "")
            {
                result = "";
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                byte[] bytes = Encoding.UTF8.GetBytes(source);
                int num = bytes.Length % 3;
                int num2 = 3 - num;
                if (num != 0)
                {
                    Array.Resize<byte>(ref bytes, bytes.Length + num2);
                }
                int num3 = (int)Math.Ceiling((double)bytes.Length * 1.0 / 3.0);
                for (int i = 0; i < num3; i++)
                {
                    stringBuilder.Append(this.EncodeUnit(new byte[]
					{
						bytes[i * 3], 
						bytes[i * 3 + 1], 
						bytes[i * 3 + 2]
					}));
                }
                if (num != 0)
                {
                    stringBuilder.Remove(stringBuilder.Length - num2, num2);
                    for (int i = 0; i < num2; i++)
                    {
                        stringBuilder.Append(this.m_pad);
                    }
                }
                result = stringBuilder.ToString();
            }
            return result;
        }
        protected string EncodeUnit(params byte[] unit)
        {
            int[] array = new int[]
			{
				(unit[0] & 252) >> 2, 
				((int)(unit[0] & 3) << 4) + ((unit[1] & 240) >> 4), 
				((int)(unit[1] & 15) << 2) + ((unit[2] & 192) >> 6), 
				(int)(unit[2] & 63)
			};
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.Append(this.GetEC(array[i]));
            }
            return stringBuilder.ToString();
        }
        protected char GetEC(int code)
        {
            return this.m_t1[code];
        }
        public string Decode(string source)
        {
            string result;
            if (source == null || source == "")
            {
                result = "";
            }
            else
            {
                List<byte> list = new List<byte>();
                char[] array = source.ToCharArray();
                int num = array.Length % 4;
                if (num != 0)
                {
                    Array.Resize<char>(ref array, array.Length - num);
                }
                int num2 = source.IndexOf(this.m_pad);
                if (num2 != -1)
                {
                    num2 = source.Length - num2;
                }
                int num3 = array.Length / 4;
                for (int i = 0; i < num3; i++)
                {
                    this.DecodeUnit(list, new char[]
					{
						array[i * 4], 
						array[i * 4 + 1], 
						array[i * 4 + 2], 
						array[i * 4 + 3]
					});
                }
                for (int i = 0; i < num2; i++)
                {
                    list.RemoveAt(list.Count - 1);
                }
                result = Encoding.UTF8.GetString(list.ToArray());
            }
            return result;
        }
        protected void DecodeUnit(List<byte> byteArr, params char[] chArray)
        {
            int[] array = new int[3];
            byte[] array2 = new byte[chArray.Length];
            for (int i = 0; i < chArray.Length; i++)
            {
                array2[i] = this.FindChar(chArray[i]);
            }
            array[0] = ((int)array2[0] << 2) + ((array2[1] & 48) >> 4);
            array[1] = ((int)(array2[1] & 15) << 4) + ((array2[2] & 60) >> 2);
            array[2] = ((int)(array2[2] & 3) << 6) + (int)array2[3];
            for (int i = 0; i < array.Length; i++)
            {
                byteArr.Add((byte)array[i]);
            }
        }
        protected byte FindChar(char ch)
        {
            int num = this.m_t2[ch];
            return (byte)num;
        }
        protected void InitDict()
        {
            this.m_t1.Clear();
            this.m_t2.Clear();
            this.m_t2.Add(this.m_pad[0], -1);
            for (int i = 0; i < this.m_codeTable.Length; i++)
            {
                this.m_t1.Add(i, this.m_codeTable[i]);
                this.m_t2.Add(this.m_codeTable[i], i);
            }
        }
        protected void ValidateRepeat(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input.LastIndexOf(input[i]) > i)
                {
                    throw new Exception("密码表中含有重复字符：" + input[i]);
                }
            }
        }
        protected void ValidateEqualPad(string input, string pad)
        {
            if (input.IndexOf(pad) > -1)
            {
                throw new Exception("密码表中包含了补码字符：" + pad);
            }
        }
        protected void Test()
        {
            this.InitDict();
            string a = "abc ABC 你好！◎＃￥％……!@#$%^";
            string text = this.Encode("false");
            string b = this.Decode(text);
            Console.WriteLine(text);
            Console.WriteLine(a == b);
        }
    }
}
