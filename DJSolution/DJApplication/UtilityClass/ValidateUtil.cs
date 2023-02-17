using System;
using System.IO;
using System.Text.RegularExpressions;

namespace DJ.LMS.WinForms
{
	/// <summary>
	/// RegexValid ������ʽƥ����
	/// </summary>
	public class ValidateUtil
    {
        #region ------ƥ������ַ���
        /// <summary>
        /// ��֤Email��ʽ
        /// </summary>
        static public string Email = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        
        /// <summary>
        /// ��֤��������
        /// </summary>
        static public string Brith = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
       
        /// <summary>
        /// ��֤URL
        /// </summary>
        static public string URL = @"^\s*(\d{4})-(\d{2})-(\d{2})\s*$";

        /// <summary>
        /// ��֤�û�����
        /// </summary>
        static public string Pwd = @"^[a-zA-Z]\w{5,17}$";

        /// <summary>
        /// ֻ�����뺺��
        /// </summary>
        static public string Chinese = @"^[\u4e00-\u9fa5]{0,}$";
        
        /// <summary>
        /// ��֤�Ƿ�Ϊ����
        /// </summary>
        static public string Integer = @"^[0-9]*$";

        /// <summary>
        /// ֻ����������������
        /// </summary>
        static public string Positive = @"^\+?[1-9][0-9]*$";

        /// <summary>
        /// ֻ�������� 26 ��Ӣ����ĸ��ɵ��ַ���
        /// </summary>
        static public string LetterStr = @"^[A-Za-z]+$";

        /// <summary>
        /// ֻ�����������ֺ� 26 ��Ӣ����ĸ��ɵ��ַ���
        /// </summary>
        static public string NumLetterStr = @"^[A-Za-z0-9]+$";
        /// <summary>
        /// �Ǹ�������
        /// </summary>
        static public string NonnegativeFloatingPoint = @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$";

        #endregion

        #region ------����Ӧ��

		/// <summary>
		/// ��֤��������
		/// </summary>
        /// <param name="strBirth">��֤�ַ���</param>
		/// <returns>�Ϸ�����true,���򷵻�false</returns>
		public static bool IsValidBirth(string strBirth)
		{
            return Regex.IsMatch(strBirth, Brith);
		}

        /// <summary>
        /// ����ʼ��Ƿ�η�
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
                // �Ȱ�����Ϊ��
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
		/// ��֤URL
		/// </summary>
        /// <param name="strURL">��֤�ַ���</param>
		/// <returns>�Ϸ�����true,���򷵻�false</returns>
		public static bool IsValidURL(string strURL)
		{
            return Regex.IsMatch(strURL,URL);
		}

        /// <summary>
        /// ��֤�û�����,����ĸ��ͷ��������6��18֮�䣬ֻ�ܰ����ַ������ֺ��»���
        /// </summary>
        /// <param name="str1">��֤�ַ���</param>
        /// <returns>�Ϸ�����true,���򷵻�false</returns>
        public static bool IsValidPassword(string str1)
        {
            return Regex.IsMatch(str1, Pwd);
        }

        /// <summary>
        /// ֻ�����뺺��
        /// </summary>
        /// <param name="str1">��֤�ַ���</param>
        /// <returns>�Ϸ�����true,���򷵻�false</returns>
        public static bool IsValidChinese(string str1)
        {
            return Regex.IsMatch(str1, Chinese);
        }

        #endregion

        #region ------������֤
        /// <summary>
        /// ��֤�Ƿ�Ϊ����
        /// </summary>
        /// <param name="str1">��֤�ַ���</param>
        /// <returns>�Ϸ�����true,���򷵻�false</returns>
        public static bool IsValidInteger(string str1)
        {
            return Regex.IsMatch(str1, Integer );
        }

        /// <summary>�Ƿ�����</summary>
        /// <param name="strInput">�����ַ���</param>
        /// <returns>true/false</returns>
        public static bool IsNumeric(string strInput)
        {
            if (string.IsNullOrEmpty(strInput))
                return false;
            var reg = new Regex(@"^[-]?\d+[.]?\d*$");
            return reg.IsMatch(strInput);
        }

        /// <summary>
        /// ֻ����������������
        /// </summary>
        /// <param name="str1">��֤�ַ���</param>
        /// <returns>�Ϸ�����true,���򷵻�false</returns>
        public static bool IsValidPositive(string str1)
        {
            return Regex.IsMatch(str1, Positive);
        }
        /// <summary>
        /// ����������֤(������0)
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static bool IsNonnegativeFloatingPoint(string strInput)
        {
            return Regex.IsMatch(strInput, NonnegativeFloatingPoint);
        }

        #endregion

        #region ------�ַ�����֤

        /// <summary>
        /// ֻ�������� 26 ��Ӣ����ĸ��ɵ��ַ���
        /// </summary>
        /// <param name="str1">��֤�ַ���</param>
        /// <returns>�Ϸ�����true,���򷵻�false</returns>
        public static bool IsValidLetterStr(string str1)
        {
            return Regex.IsMatch(str1, LetterStr);
        }

        /// <summary>
        /// ֻ�����������ֺ� 26 ��Ӣ����ĸ��ɵ��ַ���
        /// </summary>
        /// <param name="str1">��֤�ַ���</param>
        /// <returns>�Ϸ�����true,���򷵻�false</returns>
        public static bool IsValidNumLetterStr(string str1)
        {
            return Regex.IsMatch(str1, NumLetterStr);
        }

        /// <summary>
        /// �ǲ���ʱ����������
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

        /// <summary>�Ƿ��</summary>
        /// <param name="strInput">�����ַ���</param>
        /// <returns>true/false</returns>
        public static bool IsBlank(string strInput)
        {
            return string.IsNullOrEmpty(strInput);
        }

        /// <summary>
        /// ��֤�ļ����Ƿ�Ϸ�
        /// </summary>
        /// <param name="fileName">�ļ���</param>
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
        /// ��֤�ļ����Ƿ�Ϸ�
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
