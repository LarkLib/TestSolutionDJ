using System;
using System.Text;
using System.Runtime.InteropServices;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便快捷获取或设置INI文件的内容。
    /// 包括下面功能：写INI文件、读取INI文件、删除ini文件下所有段落、删除ini文件下指定段落下的所有键等功能。
    /// </summary>
    public class IniFileUtil
    {
        public string path;
        public IniFileUtil(string iniFilePath)
		{
			this.path = iniFilePath;
		}
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string string_0, string string_1, string string_2, string string_3);
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string string_0, string string_1, string string_2, StringBuilder stringBuilder_0, int int_0, string string_3);
		[DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
		private static extern int GetPrivateProfileString_1(string string_0, string string_1, string string_2, byte[] byte_0, int int_0, string string_3);

        /// <summary>    
        /// 写INI文件    
        /// </summary>    
        /// <param name="Section">分组节点</param>    
        /// <param name="Key">关键字</param>    
        /// <param name="Value">值</param> 
		public void IniWriteValue(string Section, string Key, string Value)
		{
            IniFileUtil.WritePrivateProfileString(Section, Key, Value, this.path);
		}
        /// <summary>    
        /// 读取INI文件    
        /// </summary>    
        /// <param name="Section">分组节点</param>    
        /// <param name="Key">关键字</param>    
        /// <returns></returns> 
		public string IniReadValue(string Section, string Key)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
            IniFileUtil.GetPrivateProfileString(Section, Key, "", stringBuilder, 255, this.path);
			return stringBuilder.ToString();
		}
        /// <summary>    
        /// 读取INI文件    
        /// </summary>    
        /// <param name="Section">分组节点</param>    
        /// <param name="Key">关键字</param>    
        /// <returns></returns> 
		public byte[] IniReadValues(string section, string key)
		{
			byte[] array = new byte[255];
            IniFileUtil.GetPrivateProfileString_1(section, key, "", array, 255, this.path);
			return array;
		}
        /// <summary>    
        /// 删除ini文件下所有段落    
        /// </summary> 
		public void ClearAllSection()
		{
			this.IniWriteValue(null, null, null);
		}
        /// <summary>    
        /// 删除ini文件下指定段落下的所有键    
        /// </summary>    
        /// <param name="Section"></param> 
		public void ClearSection(string Section)
		{
			this.IniWriteValue(Section, null, null);
		}
    }
}
