using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现各种输入格式验证操作
    /// 格式验证包括各种数字格式、中文检测、身份证、邮件、邮政编码、
    /// 固定电话、手机、URL地址、IP地址、日期格式、GUID、Base64编码等格式验证
    /// </summary>
    public class ValidateUtil
    {
        /// <summary>    
        /// 电子邮件正则表达式    
        /// </summary> 
        public static readonly string EmailRegex = "^([a-z0-9_\\.-]+)@([\\da-z\\.-]+)\\.([a-z\\.]{2,6})$";
        /// <summary>    
        /// 检测是否有中文字符正则表达式    
        /// </summary>    
        public static readonly string CHZNRegex = "[\u4e00-\u9fa5]"; 
        /// <summary>    
        /// 检测用户名格式是否有效(只能是汉字、字母、下划线、数字)    
        /// </summary> 
        public static readonly string UserNameRegex = "^([\\u4e00-\\u9fa5A-Za-z_0-9]{0,})$";
        /// <summary>    
        /// 密码有效性正则表达式(仅包含字符数字下划线）6~16位    
        /// </summary> 
        public static readonly string PasswordCharNumberRegex = "^[A-Za-z_0-9]{6,16}$";
        /// <summary>    
        /// 密码有效性正则表达式（纯数字或者纯字母，不通过） 6~16位    
        /// </summary>
        public static readonly string PasswordRegex = "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s).{6,16}$";
        /// <summary>    
        /// INT类型数字正则表达式    
        /// </summary> 
        public static readonly string ValidIntRegex = "^[1-9]\\d*\\.?[0]*$";
        /// <summary>    
        /// 是否数字正则表达式    
        /// </summary>
        public static readonly string NumericRegex = "^[-]?\\d+[.]?\\d*$";
        /// <summary>    
        /// 是否整数字正则表达式    
        /// </summary>
        public static readonly string NumberRegex = "^[0-9]+$";
        /// <summary>    
        /// 是否整数正则表达式（可带带正负号）    
        /// </summary>
        public static readonly string NumberSignRegex = "^[+-]?[0-9]+$";
        /// <summary>    
        /// 是否是浮点数正则表达式    
        /// </summary>
        public static readonly string DecimalRegex = "^[0-9]+[.]?[0-9]+$";
        /// <summary>    
        /// 是否是浮点数正则表达式(可带正负号)    
        /// </summary>
        public static readonly string DecimalSignRegex = "^[+-]?[0-9]+[.]?[0-9]+$";
        /// <summary>    
        /// 固定电话正则表达式    
        /// </summary>
        public static readonly string PhoneRegex = "^(\\(\\d{3,4}\\)|\\d{3,4}-)?\\d{7,8}$";
        /// <summary>    
        /// 移动电话正则表达式    
        /// </summary> 
        public static readonly string MobileRegex = "^(13|15|18)\\d{9}$";
        /// <summary>    
        /// 固定电话、移动电话正则表达式    
        /// </summary> 
        public static readonly string PhoneMobileRegex = "^(\\(\\d{3,4}\\)|\\d{3,4}-)?\\d{7,8}$|^(13|15|18)\\d{9}$";
        /// <summary>    
        /// 身份证15位正则表达式    
        /// </summary>
        public static readonly string ID15Regex = "^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$";
        /// <summary>    
        /// 身份证18位正则表达式    
        /// </summary>
        public static readonly string ID18Regex = "^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])((\\d{4})|\\d{3}[A-Z])$";
        /// <summary>    
        /// URL正则表达式    
        /// </summary> 
        public static readonly string UrlRegex = "\\b(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]*[-A-Za-z0-9+&@#/%=~_|]";
        /// <summary>    
        /// IP正则表达式    
        /// </summary>
        public static readonly string IPRegex = "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$";
        /// <summary>    
        /// Base64编码正则表达式。    
        /// 大小写字母各26个，加上10个数字，和加号“+”，斜杠“/”，一共64个字符，等号“=”用来作为后缀用途    
        /// </summary>
        public static readonly string Base64Regex = "[A-Za-z0-9\\+\\/\\=]";
        /// <summary>    
        /// 是否为纯字符的正则表达式    
        /// </summary> 
        public static readonly string LetterRegex = "^[A-Za-z]+$";
        /// <summary>    
        /// GUID正则表达式    
        /// </summary>
        public static readonly string GuidRegex = "[A-F0-9]{8}(-[A-F0-9]{4}){3}-[A-F0-9]{12}|[A-F0-9]{32}";
        /// <summary>    
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true    
        /// </summary>    
        /// <param name="input">输入字符串</param>    
        /// <param name="pattern">模式字符串</param>
        public static bool IsMatch(string input, string pattern)
        {
            return ValidateUtil.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }
        /// <summary>    
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true    
        /// </summary>    
        /// <param name="input">输入的字符串</param>    
        /// <param name="pattern">模式字符串</param>    
        /// <param name="options">筛选条件</param> 
        public static bool IsMatch(string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }
        /// <summary>    
        /// 返回字符串真实长度, 1个汉字长度为2    
        /// </summary>    
        /// <returns>字符长度</returns> 
        public static int GetStringLength(string stringValue)
        {
            return Encoding.Default.GetBytes(stringValue).Length;
        }
        /// <summary>    
        /// 检测用户名格式是否有效    
        /// </summary>    
        /// <param name="userName">用户名</param>    
        /// <returns></returns>
        public static bool IsValidUserName(string userName)
        {
            int stringLength = ValidateUtil.GetStringLength(userName);
            return stringLength >= 4 && stringLength <= 20 && Regex.IsMatch(userName, ValidateUtil.UserNameRegex);
        }
        /// <summary>    
        /// 密码有效性（纯数字或者纯字母，不通过）    
        /// </summary>    
        /// <param name="password">密码字符串</param>    
        /// <returns></returns>
        public static bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, ValidateUtil.PasswordRegex);
        }
        /// <summary>    
        /// int有效性    
        /// </summary> 
        public static bool IsValidInt(string val)
        {
            return Regex.IsMatch(val, ValidateUtil.ValidIntRegex);
        }
        /// <summary>    
        /// 是否数字字符串    
        /// </summary>    
        /// <param name="inputData">输入字符串</param>    
        /// <returns></returns> 
        public static bool IsNumeric(string inputData)
        {
            Regex regex = new Regex(ValidateUtil.NumericRegex);
            Match match = regex.Match(inputData);
            return match.Success;
        }
        /// <summary>    
        /// 是否整数字符串    
        /// </summary>    
        /// <param name="inputData">输入字符串</param>    
        /// <returns></returns> 
        public static bool IsNumber(string inputData)
        {
            Regex regex = new Regex(ValidateUtil.NumberRegex);
            Match match = regex.Match(inputData);
            return match.Success;
        }
        /// <summary>    
        /// 是否整数字符串（带正负号）    
        /// </summary>    
        /// <param name="inputData">输入字符串</param>    
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            Regex regex = new Regex(ValidateUtil.NumberSignRegex);
            Match match = regex.Match(inputData);
            return match.Success;
        }
        /// <summary>    
        /// 是否是浮点数    
        /// </summary>    
        /// <param name="inputData">输入字符串</param>    
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            Regex regex = new Regex(ValidateUtil.DecimalRegex);
            Match match = regex.Match(inputData);
            return match.Success;
        }
        /// <summary>    
        /// 是否是浮点数 可带正负号    
        /// </summary>    
        /// <param name="inputData">输入字符串</param>    
        /// <returns></returns>
        public static bool IsDecimalSign(string inputData)
        {
            Regex regex = new Regex(ValidateUtil.DecimalSignRegex);
            Match match = regex.Match(inputData);
            return match.Success;
        }
        /// <summary>    
        /// 检测是否有中文字符    
        /// </summary>  
        public static bool IsHasCHZN(string inputData) 
        {
            Regex regex = new Regex(ValidateUtil.CHZNRegex);
            Match match = regex.Match(inputData);
            return match.Success;
        }
        /// <summary>     
        /// 检测含有中文字符串的实际长度     
        /// </summary>     
        /// <param name="inputData">字符串</param> 
        public static int GetCHZNLength(string inputData)
        {
            ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
            byte[] bytes = aSCIIEncoding.GetBytes(inputData);
            int num = 0;
            for (int num2 = 0; num2 <= bytes.Length - 1; num2++)
            {
                if (bytes[num2] == 63)
                {
                    num++;
                }
                num++;
            }
            return num;
        }
        /// <summary>    
        /// 验证输入字母  "^[A-Za-z]+$"    
        /// </summary>    
        /// <param name="inputData">输入字符串</param>    
        /// <returns></returns>
        public bool IsLetter(string inputData)
        {
            return Regex.IsMatch(inputData, ValidateUtil.LetterRegex);
        }
        /// <summary>    
        /// 验证身份证是否合法  15 和  18位两种    
        /// </summary>    
        /// <param name="idCard">要验证的身份证</param>
        public static bool IsIdCard(string idCard)
        {
            bool result;
            if (string.IsNullOrEmpty(idCard))
            {
                result = false;
            }
            else
            {
                if (idCard.Length == 15)
                {
                    result = Regex.IsMatch(idCard, ValidateUtil.ID15Regex);
                }
                else
                {
                    result = (idCard.Length == 18 && Regex.IsMatch(idCard, ValidateUtil.ID18Regex, RegexOptions.IgnoreCase));
                }
            }
            return result;
        }
        /// <summary>    
        /// 是否是邮件地址    
        /// </summary>    
        /// <param name="inputData">输入字符串</param>    
        /// <returns></returns> 
        public static bool IsEmail(string inputData)
        {
            Regex regex = new Regex(ValidateUtil.EmailRegex);
            Match match = regex.Match(inputData);
            return match.Success;
        }
        /// <summary>    
        /// 邮编有效性    
        /// </summary> 
        public static bool IsValidZip(string zip)
        {
            Regex regex = new Regex("^\\d{6}$", RegexOptions.None);
            Match match = regex.Match(zip);
            return match.Success;
        }
        /// <summary>    
        /// 固定电话有效性    
        /// </summary> 
        public static bool IsValidPhone(string phone)
        {
            Regex regex = new Regex(ValidateUtil.PhoneRegex, RegexOptions.None);
            Match match = regex.Match(phone);
            return match.Success;
        }
        /// <summary>    
        /// 手机有效性    
        /// </summary>
        public static bool IsValidMobile(string mobile)
        {
            Regex regex = new Regex(ValidateUtil.MobileRegex, RegexOptions.None);
            Match match = regex.Match(mobile);
            return match.Success;
        }
        /// <summary>    
        /// 电话有效性（固话和手机 ）    
        /// </summary>
        public static bool IsValidPhoneAndMobile(string number)
        {
            Regex regex = new Regex(ValidateUtil.PhoneMobileRegex, RegexOptions.None);
            Match match = regex.Match(number);
            return match.Success;
        }
        /// <summary>    
        /// Url有效性    
        /// </summary>    
        public static bool IsValidURL(string url)
        {
            return Regex.IsMatch(url, ValidateUtil.UrlRegex);
        }
        /// <summary>    
        /// IP有效性    
        /// </summary>
        public static bool IsValidIP(string ip)
        {
            return Regex.IsMatch(ip, ValidateUtil.IPRegex);
        }
        /// <summary>    
        /// domain 有效性    
        /// </summary>    
        /// <param name="host">域名</param>    
        /// <returns></returns> 
        public static bool IsValidDomain(string host)
        {
            Regex regex = new Regex("^\\d+$");
            return host.IndexOf(".") != -1 && !regex.IsMatch(host.Replace(".", string.Empty));
        }
        /// <summary>    
        /// 判断是否为base64字符串    
        /// </summary> 
        public static bool IsBase64String(string str)
        {
            return Regex.IsMatch(str, ValidateUtil.Base64Regex);
        }
        /// <summary>    
        /// 验证字符串是否是GUID    
        /// </summary>    
        /// <param name="guid">字符串</param>    
        /// <returns></returns>
        public static bool IsGuid(string guid)
        {
            return !string.IsNullOrEmpty(guid) && Regex.IsMatch(guid, ValidateUtil.GuidRegex, RegexOptions.IgnoreCase);
        }
        /// <summary>    
        /// 判断输入的字符是否为日期    
        /// </summary> 
        public static bool IsDate(string strValue)
        {
            return Regex.IsMatch(strValue, "^((\\d{2}(([02468][048])|([13579][26]))[\\-\\/\\s]?((((0?[13578])|(1[02]))[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])))))|(\\d{2}(([02468][1235679])|([13579][01345789]))[\\-\\/\\s]?((((0?[13578])|(1[02]))[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\\-\\/\\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))");
        }
        /// <summary>    
        /// 判断输入的字符是否为日期,如2004-07-12 14:25|||1900-01-01 00:00|||9999-12-31 23:59    
        /// </summary>  
        public static bool IsDateHourMinute(string strValue)
        {
            return Regex.IsMatch(strValue, "^(19[0-9]{2}|[2-9][0-9]{3})-((0(1|3|5|7|8)|10|12)-(0[1-9]|1[0-9]|2[0-9]|3[0-1])|(0(4|6|9)|11)-(0[1-9]|1[0-9]|2[0-9]|30)|(02)-(0[1-9]|1[0-9]|2[0-9]))\\x20(0[0-9]|1[0-9]|2[0-3])(:[0-5][0-9]){1}$");
        }
        /// <summary>    
        /// 检查字符串最大长度，返回指定长度的串    
        /// </summary>    
        /// <param name="inputData">输入字符串</param>    
        /// <param name="maxLength">最大长度</param>    
        /// <returns></returns> 
        public static string CheckMathLength(string inputData, int maxLength)
        {
            if (inputData != null && inputData != string.Empty)
            {
                inputData = inputData.Trim();
                if (inputData.Length > maxLength)
                {
                    inputData = inputData.Substring(0, maxLength);
                }
            }
            return inputData;
        }
        /// <summary>    
        /// 转换成 HTML code    
        /// </summary> 
        public static string Encode(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }
        /// <summary>    
        ///解析html成 普通文本    
        /// </summary>
        public static string Decode(string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }
        /// <summary>
        /// 验证文件名是否合法
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static bool CheckFileName(string fileName)
        {
            bool returnValue = true;
            if (fileName.Trim().Length == 0)
            {
                returnValue = false;
            }
            else
            {
                if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }
        /// <summary>
        /// 验证文件夹是否合法
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static bool CheckFolderName(string folderName)
        {
            bool returnValue = true;
            if (folderName.Trim().Length == 0)
            {
                returnValue = false;
            }
            else
            {
                if ((folderName.IndexOfAny(Path.GetInvalidPathChars()) >= 0) || (folderName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0))
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }
    }
}
