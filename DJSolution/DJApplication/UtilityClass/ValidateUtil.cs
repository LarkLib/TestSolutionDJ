using System;
using System.IO;
using System.Text.RegularExpressions;

namespace DJ.LMS.WinForms
{
	/// <summary>
	/// RegexValid 正则表达式匹配类
	/// </summary>
	public class ValidateUtil
    {
        #region ------匹配规则字符串
        /// <summary>
        /// 验证Email格式
        /// </summary>
        static public string Email = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        
        /// <summary>
        /// 验证出生日期
        /// </summary>
        static public string Brith = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
       
        /// <summary>
        /// 验证URL
        /// </summary>
        static public string URL = @"^\s*(\d{4})-(\d{2})-(\d{2})\s*$";

        /// <summary>
        /// 验证用户密码
        /// </summary>
        static public string Pwd = @"^[a-zA-Z]\w{5,17}$";

        /// <summary>
        /// 只能输入汉字
        /// </summary>
        static public string Chinese = @"^[\u4e00-\u9fa5]{0,}$";
        
        /// <summary>
        /// 验证是否为整数
        /// </summary>
        static public string Integer = @"^[0-9]*$";

        /// <summary>
        /// 只能输入非零的正整数
        /// </summary>
        static public string Positive = @"^\+?[1-9][0-9]*$";

        /// <summary>
        /// 只能输入由 26 个英文字母组成的字符串
        /// </summary>
        static public string LetterStr = @"^[A-Za-z]+$";

        /// <summary>
        /// 只能输入由数字和 26 个英文字母组成的字符串
        /// </summary>
        static public string NumLetterStr = @"^[A-Za-z0-9]+$";
        /// <summary>
        /// 非负浮点数
        /// </summary>
        static public string NonnegativeFloatingPoint = @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$";

        #endregion

        #region ------网络应用

		/// <summary>
		/// 验证出生日期
		/// </summary>
        /// <param name="strBirth">验证字符串</param>
		/// <returns>合法返回true,否则返回false</returns>
		public static bool IsValidBirth(string strBirth)
		{
            return Regex.IsMatch(strBirth, Brith);
		}

        /// <summary>
        /// 检查邮件是否何法
        /// </summary>
        /// <param name="inputEmail"></param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {
            email = email.Trim();
            const string regexString = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var regex = new Regex(regexString);
            return regex.IsMatch(email);
        }

        public static bool CheckEmail(string email)
        {
            bool returnValue = true;
            if (email.Trim().Length == 0)
            {
                // 先按可以为空
                returnValue = true;
            }
            else
            {
                Regex Regex = new Regex("[\\w-]+@([\\w-]+\\.)+[\\w-]+");
                if (!Regex.IsMatch(email))
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }

		/// <summary>
		/// 验证URL
		/// </summary>
        /// <param name="strURL">验证字符串</param>
		/// <returns>合法返回true,否则返回false</returns>
		public static bool IsValidURL(string strURL)
		{
            return Regex.IsMatch(strURL,URL);
		}

        /// <summary>
        /// 验证用户密码,以字母开头，长度在6～18之间，只能包含字符、数字和下划线
        /// </summary>
        /// <param name="str1">验证字符串</param>
        /// <returns>合法返回true,否则返回false</returns>
        public static bool IsValidPassword(string str1)
        {
            return Regex.IsMatch(str1, Pwd);
        }

        /// <summary>
        /// 只能输入汉字
        /// </summary>
        /// <param name="str1">验证字符串</param>
        /// <returns>合法返回true,否则返回false</returns>
        public static bool IsValidChinese(string str1)
        {
            return Regex.IsMatch(str1, Chinese);
        }

        #endregion

        #region ------数字验证
        /// <summary>
        /// 验证是否为整数
        /// </summary>
        /// <param name="str1">验证字符串</param>
        /// <returns>合法返回true,否则返回false</returns>
        public static bool IsValidInteger(string str1)
        {
            return Regex.IsMatch(str1, Integer );
        }

        /// <summary>是否数字</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool IsNumeric(string strInput)
        {
            if (string.IsNullOrEmpty(strInput))
                return false;
            var reg = new Regex(@"^[-]?\d+[.]?\d*$");
            return reg.IsMatch(strInput);
        }

        /// <summary>
        /// 只能输入非零的正整数
        /// </summary>
        /// <param name="str1">验证字符串</param>
        /// <returns>合法返回true,否则返回false</returns>
        public static bool IsValidPositive(string str1)
        {
            return Regex.IsMatch(str1, Positive);
        }
        /// <summary>
        /// 正浮点数验证(不包括0)
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static bool IsNonnegativeFloatingPoint(string strInput)
        {
            return Regex.IsMatch(strInput, NonnegativeFloatingPoint);
        }

        #endregion

        #region ------字符串验证

        /// <summary>
        /// 只能输入由 26 个英文字母组成的字符串
        /// </summary>
        /// <param name="str1">验证字符串</param>
        /// <returns>合法返回true,否则返回false</returns>
        public static bool IsValidLetterStr(string str1)
        {
            return Regex.IsMatch(str1, LetterStr);
        }

        /// <summary>
        /// 只能输入由数字和 26 个英文字母组成的字符串
        /// </summary>
        /// <param name="str1">验证字符串</param>
        /// <returns>合法返回true,否则返回false</returns>
        public static bool IsValidNumLetterStr(string str1)
        {
            return Regex.IsMatch(str1, NumLetterStr);
        }

        /// <summary>
        /// 是不是时间类型数据
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static bool IsDateTime(string strDate)
        {
            var reg =
                new Regex(
                    @"(((^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(10|12|0?[13578])([-\/\._])(3[01]|[12][0-9]|0?[1-9]))|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(11|0?[469])([-\/\._])(30|[12][0-9]|0?[1-9]))|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(0?2)([-\/\._])(2[0-8]|1[0-9]|0?[1-9]))|(^([2468][048]00)([-\/\._])(0?2)([-\/\._])(29))|(^([3579][26]00)([-\/\._])(0?2)([-\/\._])(29))|(^([1][89][0][48])([-\/\._])(0?2)([-\/\._])(29))|(^([2-9][0-9][0][48])([-\/\._])(0?2)([-\/\._])(29))|(^([1][89][2468][048])([-\/\._])(0?2)([-\/\._])(29))|(^([2-9][0-9][2468][048])([-\/\._])(0?2)([-\/\._])(29))|(^([1][89][13579][26])([-\/\._])(0?2)([-\/\._])(29))|(^([2-9][0-9][13579][26])([-\/\._])(0?2)([-\/\._])(29)))((\s+(0?[1-9]|1[012])(:[0-5]\d){0,2}(\s[AP]M))?$|(\s+([01]\d|2[0-3])(:[0-5]\d){0,2})?$))");
            return reg.IsMatch(strDate);
        }
        
        #endregion

        /// <summary>是否空</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool IsBlank(string strInput)
        {
            return string.IsNullOrEmpty(strInput);
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
