using System;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现转换人民币大小金额操作
    /// </summary>
    public class RMBUtil
    {
        /// <summary>     
        /// 转换人民币大小金额     
        /// </summary>     
        /// <param name="number">金额</param>     
        /// <returns>返回大写形式</returns>
        public static string ToRMB(decimal number)
        {
            string text = "零壹贰叁肆伍陆柒捌玖";
            string text2 = "万仟佰拾亿仟佰拾万仟佰拾元角分";
            string text3 = "";
            string str = "";
            string str2 = "";
            int num = 0;
            number = Math.Round(Math.Abs(number), 2);
            string text4 = ((long)(number * 100m)).ToString();
            int length = text4.Length;
            string result;
            if (length > 15)
            {
                result = "溢出";
            }
            else
            {
                text2 = text2.Substring(15 - length);
                for (int i = 0; i < length; i++)
                {
                    string text5 = text4.Substring(i, 1);
                    int startIndex = Convert.ToInt32(text5);
                    if (i != length - 3 && i != length - 7 && i != length - 11 && i != length - 15)
                    {
                        if (text5 == "0")
                        {
                            str = "";
                            str2 = "";
                            num++;
                        }
                        else
                        {
                            if (text5 != "0" && num != 0)
                            {
                                str = "零" + text.Substring(startIndex, 1);
                                str2 = text2.Substring(i, 1);
                                num = 0;
                            }
                            else
                            {
                                str = text.Substring(startIndex, 1);
                                str2 = text2.Substring(i, 1);
                                num = 0;
                            }
                        }
                    }
                    else
                    {
                        if (text5 != "0" && num != 0)
                        {
                            str = "零" + text.Substring(startIndex, 1);
                            str2 = text2.Substring(i, 1);
                            num = 0;
                        }
                        else
                        {
                            if (text5 != "0" && num == 0)
                            {
                                str = text.Substring(startIndex, 1);
                                str2 = text2.Substring(i, 1);
                                num = 0;
                            }
                            else
                            {
                                if (text5 == "0" && num >= 3)
                                {
                                    str = "";
                                    str2 = "";
                                    num++;
                                }
                                else
                                {
                                    if (length >= 11)
                                    {
                                        str = "";
                                        num++;
                                    }
                                    else
                                    {
                                        str = "";
                                        str2 = text2.Substring(i, 1);
                                        num++;
                                    }
                                }
                            }
                        }
                    }
                    if (i == length - 11 || i == length - 3)
                    {
                        str2 = text2.Substring(i, 1);
                    }
                    text3 = text3 + str + str2;
                    if (i == length - 1 && text5 == "0")
                    {
                        text3 += '整';
                    }
                }
                if (number == 0m)
                {
                    text3 = "零元整";
                }
                result = text3;
            }
            return result;
        }
        /// <summary>     
        /// 将字符串格式的数字转换人民币大小金额     
        /// </summary>     
        /// <param name="numberString">字符串格式的数字</param>     
        /// <returns></returns>
        public static string ToRMB(string numberString)
        {
            string result;
            try
            {
                decimal number = Convert.ToDecimal(numberString);
                result = RMBUtil.ToRMB(number);
            }
            catch
            {
                result = "非数字形式！";
            }
            return result;
        }
    }
}
