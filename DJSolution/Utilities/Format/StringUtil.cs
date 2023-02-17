using System;
using System.Collections;
using System.Text;
using System.Web;
using Microsoft.VisualBasic;
using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 字符串操作类
    /// </summary>
    public class StringUtil
    {
        private static Regex RegexBr = new Regex(@"(\r\n)", RegexOptions.IgnoreCase);
        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensitive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensitive)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensitive)
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                        return i;
                    else
                        if (strSearch == stringArray[i])
                            return i;
            }
            return -1;
        }
        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置, 字符串不区分大小写
        /// </summary>
        /// <param name="strSearch">要查找的字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>		
        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            return GetInArrayID(strSearch, stringArray, true);
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="strSearch">要查找的字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensitive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensitive)
        {
            return GetInArrayID(strSearch, stringArray, caseInsensitive) >= 0;
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string[] stringArray)
        {
            return InArray(str, stringArray, false);
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringArray">内部以逗号分割单词的字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringArray)
        {
            return InArray(str, SplitString(stringArray, ","), false);
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringArray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringArray, string strsplit)
        {
            return InArray(str, SplitString(stringArray, strsplit), false);
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringArray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <param name="caseInsensitive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringArray, string strsplit, bool caseInsensitive)
        {
            return InArray(str, SplitString(stringArray, strsplit), caseInsensitive);
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (strContent.IndexOf(strSplit) < 0)
            {
                string[] tmp = { strContent };
                return tmp;
            }
            return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, int p_3)
        {
            string[] result = new string[p_3];
            string[] splited = SplitString(strContent, strSplit);
            for (int i = 0; i < p_3; i++)
            {
                result[i] = i < splited.Length ? splited[i] : string.Empty;
            }
            return result;
        }
        /// <summary>
        /// 删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RTrim(string str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].Equals(" ") || str[i].Equals("\r") || str[i].Equals("\n"))
                {
                    str.Remove(i, 1);
                }
            }
            return str;
        }
        /// <summary>
        /// 清除给定字符串中的回车及换行符
        /// </summary>
        /// <param name="str">要清除的字符串</param>
        /// <returns>清除后返回的字符串</returns>
        public static string ClearBR(string str)
        {
            Match m = null;
            for (m = RegexBr.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "");
            }
            return str;
        }
        /// <summary>
        /// 从字符串的指定位置截取指定长度的子字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <param name="length">子字符串的长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length = length * -1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        startIndex = startIndex - length;
                    }
                }
                if (startIndex > str.Length)
                {
                    return "";
                }
            }
            else
            {
                if (length < 0)
                {
                    return "";
                }
                else
                {
                    if (length + startIndex > 0)
                    {
                        length = length + startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            if (str.Length - startIndex < length)
            {
                length = str.Length - startIndex;
            }
            return str.Substring(startIndex, length);
        }
        /// <summary>
        /// 从字符串的指定位置开始截取到字符串结尾的了符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex)
        {
            return CutString(str, startIndex, str.Length);
        }
        /// <summary>
        /// 字符串如果超过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            return GetSubString(p_SrcString, 0, p_Length, p_TailString);
        }
        /// <summary>
        /// 取指定长度的字符串
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_StartIndex">起始位置</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string myResult = p_SrcString;
            //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
            if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") ||
                System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
            {
                //当截取的起始位置超出字段串长度时
                if (p_StartIndex >= p_SrcString.Length)
                {
                    return "";
                }
                else
                {
                    return p_SrcString.Substring(p_StartIndex, ((p_Length + p_StartIndex) > p_SrcString.Length) ? 
                        (p_SrcString.Length - p_StartIndex) : p_Length);
                }
            }
            if (p_Length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(p_SrcString);
                //当字符串长度大于起始位置
                if (bsSrcString.Length > p_StartIndex)
                {
                    int p_EndIndex = bsSrcString.Length;
                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (p_StartIndex + p_Length))
                    {
                        p_EndIndex = p_Length + p_StartIndex;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾
                        p_Length = bsSrcString.Length - p_StartIndex;
                        p_TailString = "";
                    }
                    int nRealLength = p_Length;
                    int[] anResultFlag = new int[p_Length];
                    byte[] bsResult = null;
                    int nFlag = 0;
                    for (int i = p_StartIndex; i < p_EndIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }
                        anResultFlag[i] = nFlag;
                    }
                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[p_Length - 1] == 1))
                    {
                        nRealLength = p_Length + 1;
                    }
                    bsResult = new byte[nRealLength];
                    Array.Copy(bsSrcString, p_StartIndex, bsResult, 0, nRealLength);
                    myResult = Encoding.Default.GetString(bsResult);
                    myResult = myResult + p_TailString;
                }
            }
            return myResult;
        }
        /// <summary>
        /// 自定义的替换字符串函数
        /// </summary>
        public static string ReplaceString(string SourceString, string SearchString, string ReplaceString, bool IsCaseInsensetive)
        {
            return Regex.Replace(SourceString, Regex.Escape(SearchString), ReplaceString, IsCaseInsensetive ? RegexOptions.IgnoreCase : RegexOptions.None);
        }
        /// <summary>
        /// 清理字符串
        /// </summary>
        public static string CleanInput(string strIn)
        {
            return Regex.Replace(strIn.Trim(), @"[^\w\.@-]", "");
        }
        /// <summary>    
        /// 转换为Camel字符串格式，去掉字符之间的空格以及起始"_"符号    
        /// </summary>    
        /// <param name="name">待转换字符串</param>    
        /// <returns></returns>
        public static string ToCamel(string name)
        {
            string text = name.TrimStart(new char[] { '_' });
            text = StringUtil.RemoveSpaces(StringUtil.ToProperCase(text));
            return string.Format("{0}{1}", char.ToLower(text[0]), text.Substring(1, text.Length - 1));
        }
        /// <summary>    
        /// 转换为Capital格式显示，去掉字符之间的空格以及起始"_"符号    
        /// </summary>    
        /// <param name="name">待转换字符串</param>    
        /// <returns></returns>
        public static string ToCapit(string name)
        {
            string s = name.TrimStart(new char[] { '_' });
            return StringUtil.RemoveSpaces(StringUtil.ToProperCase(s));
        }
        /// <summary>    
        /// 移除字符串最后的一个字符    
        /// </summary>    
        /// <param name="s"></param>    
        /// <returns></returns>  
        public static string RemoveFinalChar(string s)
        {
            if (s.Length > 1)
            {
                s = s.Substring(0, s.Length - 1);
            }
            return s;
        }
        /// <summary>    
        /// 移除字符串中最后的一个逗号    
        /// </summary>    
        /// <param name="s">操作的字符串</param>    
        /// <returns></returns>
        public static string RemoveFinalComma(string s)
        {
            if (s.Trim().Length > 0)
            {
                int num = s.LastIndexOf(",");
                if (num > 0)
                {
                    s = s.Substring(0, s.Length - (s.Length - num));
                }
            }
            return s;
        }
        /// <summary>    
        /// 移除字符间的空格    
        /// </summary>    
        /// <param name="s">操作的字符串</param>    
        /// <returns></returns>
        public static string RemoveSpaces(string s)
        {
            s = s.Trim();
            s = s.Replace(" ", "");
            return s;
        }
        /// <summary>    
        /// 将字符串转换为合适的大小写    
        /// </summary>    
        /// <param name="s">操作的字符串</param>    
        /// <returns></returns>
        public static string ToProperCase(string s)
        {
            string result = "";
            if (s.Length > 0)
            {
                if (s.IndexOf(" ") > 0)
                {
                    result = Strings.StrConv(s, VbStrConv.ProperCase, 1033);
                }
                else
                {
                    string str = s.Substring(0, 1).ToUpper(new CultureInfo("en-US"));
                    result = str + s.Substring(1, s.Length - 1);
                }
            }
            return result;
        }
        /// <summary>    
        /// 清除字符间的空格，并转换为合适的大小写    
        /// </summary>    
        /// <param name="s"></param>    
        /// <returns></returns>
        public static string ToTrimmedProperCase(string s)
        {
            return StringUtil.RemoveSpaces(StringUtil.ToProperCase(s));
        }
        /// <summary>    
        /// 转换对象为字符串表示    
        /// </summary>    
        /// <param name="o">操作对象</param>    
        /// <returns></returns>
        public static string ToString(object o)
        {
            Type type = o.GetType();
            PropertyInfo[] properties = type.GetProperties();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Properties for: " + o.GetType().Name + Environment.NewLine);
            PropertyInfo[] array = properties;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo propertyInfo = array[i];
                try
                {
                    stringBuilder.Append(string.Concat(new string[] { "\t", propertyInfo.Name, "(", propertyInfo.PropertyType.ToString(), "): " }));
                    if (null != propertyInfo.GetValue(o, null))
                    {
                        stringBuilder.Append(propertyInfo.GetValue(o, null).ToString());
                    }
                }
                catch
                {
                }
                stringBuilder.Append(Environment.NewLine);
            }
            FieldInfo[] fields = type.GetFields();
            FieldInfo[] array2 = fields;
            for (int i = 0; i < array2.Length; i++)
            {
                FieldInfo fieldInfo = array2[i];
                try
                {
                    stringBuilder.Append(string.Concat(new string[] { "\t", fieldInfo.Name, "(", fieldInfo.FieldType.ToString(), "): " }));
                    if (null != fieldInfo.GetValue(o))
                    {
                        stringBuilder.Append(fieldInfo.GetValue(o).ToString());
                    }
                }
                catch
                {
                }
                stringBuilder.Append(Environment.NewLine);
            }
            return stringBuilder.ToString();
        }
        /// <summary>    
        /// 在字符串中，指定开始字符和结束字符，提取中间的内容    
        /// </summary>    
        /// <param name="content">待操作字符串</param>    
        /// <param name="start">开始字符</param>    
        /// <param name="end">结束字符</param>    
        /// <returns></returns>
        public static ArrayList ExtractInnerContent(string content, string start, string end)
        {
            int num = -1;
            int num2 = -1;
            ArrayList arrayList = new ArrayList();
            num = content.IndexOf(start);
            int num3 = num + start.Length;
            num2 = content.IndexOf(end, num3);
            int length = num2 - num3;
            if (num >= 0 && num2 > num)
            {
                arrayList.Add(content.Substring(num3, length));
            }
            while (num >= 0 && num2 > 0)
            {
                num = content.IndexOf(start, num2);
                if (num > 0)
                {
                    num2 = content.IndexOf(end, num);
                    num3 = num + start.Length;
                    length = num2 - num3;
                    if (num3 > 0 && num2 > 0)
                    {
                        arrayList.Add(content.Substring(num3, length));
                    }
                }
            }
            return arrayList;
        }
        /// <summary>    
        /// 在字符串中，指定开始字符和结束字符，提取非中间的数据    
        /// </summary>    
        /// <param name="content">待操作的字符</param>    
        /// <param name="start">开始字符</param>    
        /// <param name="end">结束字符</param>    
        /// <returns></returns> 
        public static ArrayList ExtractOuterContent(string content, string start, string end)
        {
            int num = -1;
            int num2 = -1;
            ArrayList arrayList = new ArrayList();
            num = content.IndexOf(start);
            num2 = content.IndexOf(end);
            if (num >= 0 && num2 > num)
            {
                arrayList.Add(content.Substring(num, num2 + end.Length - num));
            }
            while (num >= 0 && num2 > 0)
            {
                num = content.IndexOf(start, num2);
                if (num > 0)
                {
                    num2 = content.IndexOf(end, num);
                    if (num > 0 && num2 > 0)
                    {
                        arrayList.Add(content.Substring(num, num2 + end.Length - num));
                    }
                }
            }
            return arrayList;
        }
        /// <summary>
        /// 使用一个分隔符合并字符串数组
        /// </summary>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="separator">分隔符</param>
        /// <returns>返回合并后的字符串</returns>
        public static string Concat(string[] stringArray, string separator)
        {
            string retval = string.Empty;
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (!string.IsNullOrEmpty(retval))
                    retval += separator;
                retval += stringArray[i];
            }
            return retval;
        }
        /// <summary>
        /// 使用一个分隔符合并字符串数组
        /// </summary>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="separator">分隔符</param>
        /// <returns>返回合并后的字符串</returns>
        public static string Concat(string[] stringArray, char separator)
        {
            string retval = string.Empty;
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (!string.IsNullOrEmpty(retval))
                    retval += separator;
                retval += stringArray[i];
            }
            return retval;
        }
    }
}
