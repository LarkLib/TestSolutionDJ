using System;
using System.IO;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现目录操作的相关功能，包括目录可写与空间计算、获取指定目录中的文件列表、
    /// 获取指定目录中的子目录列表、创建目录、生成目录、检测目录等目录操作功能。
    /// </summary>
    public class DirectoryUtil
    {
        /// <summary>    
        ///检查目录是否可写，如果可以，返回True，否则False    
        /// </summary>    
        /// <param name="path"></param>    
        /// <returns></returns>
        public static bool IsWriteable(string path)
        {
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    return false;
                }
            }
            try
            {
                string path2 = ".test." + Guid.NewGuid().ToString().Substring(0, 5);
                string path3 = Path.Combine(path, path2);
                File.WriteAllLines(path3, new string[] { "test" });
                File.Delete(path3);
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>    
        /// 检查磁盘是否有足够的可用空间    
        /// </summary>    
        /// <param name="path"></param>    
        /// <param name="requiredSpace"></param>    
        /// <returns></returns>  
        public static bool IsDiskSpaceEnough(string path, ulong requiredSpace)
        {
            string pathRoot = Path.GetPathRoot(path);
            ulong freeSpace = DirectoryUtil.GetFreeSpace(pathRoot);
            return requiredSpace <= freeSpace;
        }
        /// <summary>    
        /// 获取驱动盘符的可用空间大小    
        /// </summary>    
        /// <param name="driveName">Direve name</param>    
        /// <returns>free space (byte)</returns>
        public static ulong GetFreeSpace(string driveName)
        {
            ulong result = 0uL;
            try
            {
                DriveInfo driveInfo = new DriveInfo(driveName);
                result = (ulong)driveInfo.AvailableFreeSpace;
            }
            catch
            {
            }
            return result;
        }
        public static ulong ConvertByteCountToKByteCount(ulong byteCount)
        {
            return byteCount / 1024uL;
        }
        public static float ConvertKByteCountToMByteCount(ulong kByteCount)
        {
            return kByteCount / 1024uL;
        }
        public static float ConvertMByteCountToGByteCount(float kByteCount)
        {
            return kByteCount / 1024f;
        }
        /// <summary>    
        /// 获取指定目录中所有文件列表    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param>
        public static string[] GetFileNames(string directoryPath)
        {
            if (!DirectoryUtil.IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }
            return Directory.GetFiles(directoryPath);
        }
        /// <summary>    
        /// 获取指定目录及子目录中所有文件列表    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param>    
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。    
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>    
        /// <param name="isSearchChild">是否搜索子目录</param> 
        public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            if (!DirectoryUtil.IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }
            string[] files;
            try
            {
                files = isSearchChild ? 
                    Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories) :
                    Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
            }
            catch (IOException ex)
            {
                throw ex;
            }
            return files;
        }
        /// <summary>    
        /// 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法.    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        public static string[] GetDirectories(string directoryPath)
        {
            string[] directories;
            try
            {
                directories = Directory.GetDirectories(directoryPath);
            }
            catch (IOException ex)
            {
                throw ex;
            }
            return directories;
        }
        /// <summary>    
        /// 获取指定目录及子目录中所有子目录列表    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param>    
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。    
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>    
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
        {
            string[] directories;
            try
            {
                directories = isSearchChild ? 
                    Directory.GetDirectories(directoryPath, searchPattern, SearchOption.AllDirectories) :
                    Directory.GetDirectories(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
            }
            catch (IOException ex)
            {
                throw ex;
            }
            return directories;
        }
        /// <summary>    
        /// 生成日期 文件夹    格式：yyyy\mm\dd    
        /// </summary>    
        /// <remarks>    
        /// 生成时间目录   返回 例如： c:\directory\2009\03\01    
        /// </remarks>    
        /// <param name="rootPath">绝对路径   [在此目录下建日期目录]</param>    
        /// <returns>返回完整路径  </returns> 
        public static string CreateDirectoryByDate(string rootPath)
        {
            return DirectoryUtil.CreateDirectoryByDate(rootPath, "yyyy-MM-dd");
        }
        /// <summary>    
        /// 相应格式生成日期目录    
        /// </summary>    
        /// <remarks>    
        /// formatString:    
        ///              yyyy-MM-dd        :2009\03\01    
        ///              yyyy-MM-dd-HH     :2009\03\01\01    
        /// </remarks>    
        /// <param name="rootPath">绝对路径   [在此目录下建日期目录]</param>    
        /// <param name="formatString">格式</param>    
        /// <returns>返回完整路径 </returns>
        public static string CreateDirectoryByDate(string rootPath, string formatString)
        {
            if (!DirectoryUtil.IsExistDirectory(rootPath))
            {
                throw new DirectoryNotFoundException("the rootPath is not found");
            }
            bool flag = false;
            if (formatString != null)
            {
                if (formatString == "yyyy-MM-dd")
                {
                    flag = false;
                    goto IL_3E;
                }
                if (formatString == "yyyy-MM-dd-HH")
                {
                    flag = true;
                    goto IL_3E;
                }
            }
            flag = false;
        IL_3E:
            string arg_59_1 = "\\";
            DateTime now = DateTime.Now;
            int num = now.Year;
            string text = rootPath + arg_59_1 + num.ToString();
            DirectoryUtil.CreateDirectory(text);
            string arg_88_0 = text;
            string arg_88_1 = "\\";
            now = DateTime.Now;
            num = now.Month;
            text = arg_88_0 + arg_88_1 + num.ToString("00");
            DirectoryUtil.CreateDirectory(text);
            string arg_B7_0 = text;
            string arg_B7_1 = "\\";
            now = DateTime.Now;
            num = now.Day;
            text = arg_B7_0 + arg_B7_1 + num.ToString("00");
            DirectoryUtil.CreateDirectory(text);
            if (flag)
            {
                string arg_EC_0 = text;
                string arg_EC_1 = "\\";
                now = DateTime.Now;
                num = now.Hour;
                text = arg_EC_0 + arg_EC_1 + num.ToString("00");
                DirectoryUtil.CreateDirectory(text);
            }
            return text;
        }
        /// <summary>    
        /// 确保文件夹被创建    
        /// </summary>    
        /// <param name="filePath">文件夹全名（含路径）</param>
        public static void AssertDirExist(string filePath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }
        /// <summary>    
        /// 检测指定目录是否存在    
        /// </summary>    
        /// <param name="directoryPath">目录的绝对路径</param>
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
        /// <summary>    
        /// 检测指定目录是否为空    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        public static bool IsEmptyDirectory(string directoryPath)
        {
            bool result;
            try
            {
                string[] fileNames = DirectoryUtil.GetFileNames(directoryPath);
                if (fileNames.Length > 0)
                {
                    result = false;
                }
                else
                {
                    string[] directories = DirectoryUtil.GetDirectories(directoryPath);
                    result = directories.Length > 0 ? false : true;
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>    
        /// 检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法.    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param>    
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。    
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        public static bool ContainFile(string directoryPath, string searchPattern)
        {
            bool result;
            try
            {
                string[] fileNames = DirectoryUtil.GetFileNames(directoryPath, searchPattern, false);
                result = fileNames.Length == 0 ? false : true;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>    
        /// 检测指定目录中是否存在指定的文件    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param>    
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。    
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>    
        /// <param name="isSearchChild">是否搜索子目录</param>  
        public static bool ContainFile(string directoryPath, string searchPattern, bool isSearchChild)
        {
            bool result;
            try
            {
                string[] fileNames = DirectoryUtil.GetFileNames(directoryPath, searchPattern, true);
                result = fileNames.Length == 0 ? false : true;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>    
        /// 创建一个目录    
        /// </summary>    
        /// <param name="directoryPath">目录的绝对路径</param>
        public static void CreateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
        /// <summary>    
        /// 清空指定目录下所有文件及子目录,但该目录依然保存.    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param>
        public static void ClearDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                string[] fileNames = DirectoryUtil.GetFileNames(directoryPath);
                for (int i = 0; i < fileNames.Length; i++)
                {
                    FileUtil.DeleteFile(fileNames[i]);
                }
                string[] directories = DirectoryUtil.GetDirectories(directoryPath);
                for (int i = 0; i < directories.Length; i++)
                {
                    DirectoryUtil.DeleteDirectory(directories[i]);
                }
            }
        }
        /// <summary>    
        /// 删除指定目录及其所有子目录    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param>
        public static void DeleteDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }
        /// <summary>    
        /// 取系统目录    
        /// </summary>    
        /// <returns></returns>
        public static string GetSystemDirectory()
        {
            return Environment.SystemDirectory;
        }
        /// <summary>    
        /// 取系统的特别目录    
        /// </summary>    
        /// <param name="folderType"></param>    
        /// <returns></returns> 
        public static string GetSpeicalFolder(Environment.SpecialFolder folderType)
        {
            return Environment.GetFolderPath(folderType);
        }
        /// <summary>    
        /// 返回当前系统的临时目录    
        /// </summary>    
        /// <returns></returns>  
        public static string GetTempPath()
        {
            return Path.GetTempPath();
        }
        /// <summary>
        /// 获取应用程序的当前工作目录
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }
        /// <summary>
        /// 将应用程序的当前工作目录设置为指定的目录
        /// </summary>
        /// <param name="path"></param>
        public static void SetCurrentDirectory(string path)
        {
            Directory.SetCurrentDirectory(path);
        }
        /// <summary>
        /// 获取包含不允许在路径名中使用的字符的数组
        /// </summary>
        /// <returns></returns>
        public static char[] GetInvalidPathChars()
        {
            return Path.GetInvalidPathChars();
        }
        /// <summary>    
        /// 取系统所有的逻辑驱动器    
        /// </summary>    
        /// <returns></returns>
        public static DriveInfo[] GetAllDrives()
        {
            return DriveInfo.GetDrives();
        }
    }
}
