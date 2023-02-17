using System;
using System.Text;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便处理数据类型转换，数制转换、编码转换相关的操作。
    /// 可以实现Int类型、布尔类型、字符串类型、Decimal类型、Double类型、Float类型、Byte转换、枚举类型、半角全角转换等操作。
    /// </summary>
    public sealed class ConvertHelper
    {
        /// <summary>    
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。    
        /// </summary>    
        /// <param name="value">要转换的值,即原值</param>    
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>    
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param> 
        public static string ConvertBase(string value, int from, int to)
        {
            if (!ConvertHelper.smethod_0(from))
            {
                throw new ArgumentException("参数from只能是2,8,10,16四个值。");
            }
            if (!ConvertHelper.smethod_0(to))
            {
                throw new ArgumentException("参数to只能是2,8,10,16四个值。");
            }
            int value2 = Convert.ToInt32(value, from);
            string text = Convert.ToString(value2, to);
            if (to == 2)
            {
                switch (text.Length)
                {
                    case 3:
                        {
                            text = "00000" + text;
                            break;
                        }
                    case 4:
                        {
                            text = "0000" + text;
                            break;
                        }
                    case 5:
                        {
                            text = "000" + text;
                            break;
                        }
                    case 6:
                        {
                            text = "00" + text;
                            break;
                        }
                    case 7:
                        {
                            text = "0" + text;
                            break;
                        }
                }
            }
            return text;
        }
        private static bool smethod_0(int int_0)
        {
            return int_0 == 2 || int_0 == 8 || int_0 == 10 || int_0 == 16;
        }
        /// <summary>    
        /// 将string转换成byte[]    
        /// </summary>    
        /// <param name="text">要转换的字符串</param>
        public static byte[] StringToBytes(string text)
        {
            return Encoding.Default.GetBytes(text);
        }
        /// <summary>    
        /// 使用指定字符集将string转换成byte[]    
        /// </summary>    
        /// <param name="text">要转换的字符串</param>    
        /// <param name="encoding">字符编码</param>
        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }
        /// <summary>    
        /// 将byte[]转换成string    
        /// </summary>    
        /// <param name="bytes">要转换的字节数组</param>
        public static string BytesToString(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }
        /// <summary>    
        /// 使用指定字符集将byte[]转换成string    
        /// </summary>    
        /// <param name="bytes">要转换的字节数组</param>    
        /// <param name="encoding">字符编码</param>
        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }
        /// <summary>    
        /// 将byte[]转换成int    
        /// </summary>    
        /// <param name="data">需要转换成整数的byte数组</param>
        public static int BytesToInt32(byte[] data)
        {
            int result;
            if (data.Length < 4)
            {
                result = 0;
            }
            else
            {
                int num = 0;
                if (data.Length >= 4)
                {
                    byte[] array = new byte[4];
                    Buffer.BlockCopy(data, 0, array, 0, 4);
                    num = BitConverter.ToInt32(array, 0);
                }
                result = num;
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为整型   转换失败返回默认值    
        /// </summary>    
        /// <typeparam name="T">数据类型</typeparam>    
        /// <param name="data">数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns>
        public static int ToInt32<T>(T data, int defValue)
        {
            int result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToInt32(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为整型   转换失败返回默认值    
        /// </summary>    
        /// <param name="data">数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns> 
        public static int ToInt32(string data, int defValue)
        {
            int result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                int num = 0;
                if (int.TryParse(data, out num))
                {
                    result = num;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为整型  转换失败返回默认值    
        /// </summary>    
        /// <param name="data">数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns>
        public static int ToInt32(object data, int defValue)
        {
            int result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToInt32(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为布尔类型  转换失败返回默认值    
        /// </summary>    
        /// <typeparam name="T">数据类型</typeparam>    
        /// <param name="data">数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns> 
        public static bool ToBoolean<T>(T data, bool defValue)
        {
            bool result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToBoolean(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为布尔类型  转换失败返回 默认值    
        /// </summary>    
        /// <param name="data">数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns>
        public static bool ToBoolean(string data, bool defValue)
        {
            bool result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                bool flag = false;
                if (bool.TryParse(data, out flag))
                {
                    result = flag;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为布尔类型  转换失败返回 默认值    
        /// </summary>    
        /// <param name="data">数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns> 
        public static bool ToBoolean(object data, bool defValue)
        {
            bool result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToBoolean(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为单精度浮点型  转换失败 返回默认值    
        /// </summary>    
        /// <typeparam name="T">数据类型</typeparam>    
        /// <param name="data">数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns> 
        public static float ToFloat<T>(T data, float defValue)
        {
            float result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToSingle(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为单精度浮点型   转换失败返回默认值    
        /// </summary>    
        /// <param name="data">数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns>
        public static float ToFloat(object data, float defValue)
        {
            float result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToSingle(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为单精度浮点型   转换失败返回默认值    
        /// </summary>    
        /// <param name="data">数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns> 
        public static float ToFloat(string data, float defValue)
        {
            float result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                float num = 0f;
                if (float.TryParse(data, out num))
                {
                    result = num;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为双精度浮点型   转换失败返回默认值    
        /// </summary>    
        /// <typeparam name="T">数据的类型</typeparam>    
        /// <param name="data">要转换的数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns>
        public static double ToDouble<T>(T data, double defValue)
        {
            double result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToDouble(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为双精度浮点型,并设置小数位   转换失败返回默认值    
        /// </summary>    
        /// <typeparam name="T">数据的类型</typeparam>    
        /// <param name="data">要转换的数据</param>    
        /// <param name="decimals">小数的位数</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns> 
        public static double ToDouble<T>(T data, int decimals, double defValue)
        {
            double result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Math.Round(Convert.ToDouble(data), decimals);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为双精度浮点型  转换失败返回默认值    
        /// </summary>    
        /// <param name="data">要转换的数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns> 
        public static double ToDouble(object data, double defValue)
        {
            double result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToDouble(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为双精度浮点型  转换失败返回默认值    
        /// </summary>    
        /// <param name="data">要转换的数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns>
        public static double ToDouble(string data, double defValue)
        {
            double result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                double num = 0.0;
                if (double.TryParse(data, out num))
                {
                    result = num;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为双精度浮点型,并设置小数位  转换失败返回默认值    
        /// </summary>    
        /// <param name="data">要转换的数据</param>    
        /// <param name="decimals">小数的位数</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns> 
        public static double ToDouble(object data, int decimals, double defValue)
        {
            double result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Math.Round(Convert.ToDouble(data), decimals);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为双精度浮点型,并设置小数位  转换失败返回默认值    
        /// </summary>    
        /// <param name="data">要转换的数据</param>    
        /// <param name="decimals">小数的位数</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns> 
        public static double ToDouble(string data, int decimals, double defValue)
        {
            double result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                double value = 0.0;
                if (double.TryParse(data, out value))
                {
                    result = Math.Round(value, decimals);
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为指定类型    
        /// </summary>    
        /// <param name="data">转换的数据</param>    
        /// <param name="targetType">转换的目标类型</param>
        public static object ConvertTo(object data, Type targetType)
        {
            object result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = null;
            }
            else
            {
                Type type = data.GetType();
                if (targetType == type)
                {
                    result = data;
                }
                else
                {
                    bool arg_5E_0;
                    if (targetType != typeof(Guid))
                    {
                        if (targetType != typeof(Guid?))
                        {
                            arg_5E_0 = true;
                            goto IL_5E;
                        }
                    }
                    arg_5E_0 = (type != typeof(string));
                IL_5E:
                    if (!arg_5E_0)
                    {
                        if (string.IsNullOrEmpty(data.ToString()))
                        {
                            result = null;
                        }
                        else
                        {
                            result = new Guid(data.ToString());
                        }
                    }
                    else
                    {
                        if (targetType.IsEnum)
                        {
                            try
                            {
                                result = Enum.Parse(targetType, data.ToString(), true);
                                return result;
                            }
                            catch
                            {
                                result = Enum.ToObject(targetType, data);
                                return result;
                            }
                        }
                        if (targetType.IsGenericType)
                        {
                            targetType = targetType.GetGenericArguments()[0];
                        }
                        result = Convert.ChangeType(data, targetType);
                    }
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为指定类型    
        /// </summary>    
        /// <typeparam name="T">转换的目标类型</typeparam>    
        /// <param name="data">转换的数据</param> 
        public static T ConvertTo<T>(object data)
        {
            T result;
            if (data == null || Convert.IsDBNull(data))
            {
                T t = default(T);
                result = t;
            }
            else
            {
                object obj = ConvertHelper.ConvertTo(data, typeof(T));
                if (obj == null)
                {
                    T t = default(T);
                    result = t;
                }
                else
                {
                    result = (T)obj;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为Decimal  转换失败返回默认值    
        /// </summary>    
        /// <typeparam name="T">数据类型</typeparam>    
        /// <param name="data">数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns> 
        public static decimal ToDecimal<T>(T data, decimal defValue)
        {
            decimal result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToDecimal(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为Decimal  转换失败返回 默认值    
        /// </summary>    
        /// <param name="data">数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns> 
        public static decimal ToDecimal(object data, decimal defValue)
        {
            decimal result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToDecimal(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为Decimal  转换失败返回 默认值    
        /// </summary>    
        /// <param name="data">数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns>
        public static decimal ToDecimal(string data, decimal defValue)
        {
            decimal result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                decimal num = 0m;
                if (decimal.TryParse(data, out num))
                {
                    result = num;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为DateTime  转换失败返回默认值    
        /// </summary>    
        /// <typeparam name="T">数据类型</typeparam>    
        /// <param name="data">数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns>
        public static DateTime ToDateTime<T>(T data, DateTime defValue)
        {
            DateTime result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToDateTime(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为DateTime  转换失败返回 默认值    
        /// </summary>    
        /// <param name="data">数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns> 
        public static DateTime ToDateTime(object data, DateTime defValue)
        {
            DateTime result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToDateTime(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 将数据转换为DateTime  转换失败返回 默认值    
        /// </summary>    
        /// <param name="data">数据</param>    
        /// <param name="defValue">默认值</param>    
        /// <returns></returns>
        public static DateTime ToDateTime(string data, DateTime defValue)
        {
            DateTime result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                DateTime now = DateTime.Now;
                if (DateTime.TryParse(data, out now))
                {
                    result = now;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }
        /// <summary>    
        /// 转全角的函数(SBC case)    
        /// </summary>    
        /// <param name="input">任意字符串</param>    
        /// <returns>全角字符串</returns>    
        ///<remarks>    
        ///全角空格为12288，半角空格为32    
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248    
        ///</remarks> 
        public static string ConvertToSBC(string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == ' ')
                {
                    array[i] = '\u3000';
                }
                else
                {
                    if (array[i] < '\u007f')
                    {
                        array[i] += 'ﻠ';
                    }
                }
            }
            return new string(array);
        }
        /// <summary> 转半角的函数(DBC case) </summary>    
        /// <param name="input">任意字符串</param>    
        /// <returns>半角字符串</returns>    
        ///<remarks>    
        ///全角空格为12288，半角空格为32    
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248    
        ///</remarks>
        public static string ConvertToDBC(string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == '\u3000')
                {
                    array[i] = ' ';
                }
                else
                {
                    if (array[i] > '＀' && array[i] < '｟')
                    {
                        array[i] -= 'ﻠ';
                    }
                }
            }
            return new string(array);
        }
    }
}
