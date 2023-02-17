using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现文件相关的操作，包括Stream、byte[] 和 文件之间的转换、获取文件编码、获取文件长度、创建文件、删除文件、移动文件、读取文件、读取文件属性、设置文件属性等功能
    /// </summary>
    public class FileUtil
    {
        /// <summary>    
        /// 将流读取到缓冲区中    
        /// </summary>    
        /// <param name="stream">原始流</param>
        public static byte[] StreamToBytes(Stream stream)
		{
			byte[] result;
			try
			{
				byte[] array = new byte[(int)((object)((IntPtr)stream.Length))];
				stream.Read(array, 0, Convert.ToInt32(stream.Length));
				result = array;
			}
			catch (IOException ex)
			{
				throw ex;
			}
			finally
			{
				stream.Close();
			}
			return result;
		}
        /// <summary>    
        /// 将 byte[] 转成 Stream    
        /// </summary>
		public static Stream BytesToStream(byte[] bytes)
		{
			return new MemoryStream(bytes);
		}
        /// <summary>    
        /// 将 Stream 写入文件    
        /// </summary>
		public static void StreamToFile(Stream stream, string fileName)
		{
			byte[] array = new byte[(int)((object)((IntPtr)stream.Length))];
			stream.Read(array, 0, array.Length);
			stream.Seek(0L, SeekOrigin.Begin);
			FileStream fileStream = new FileStream(fileName, FileMode.Create);
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			binaryWriter.Write(array);
			binaryWriter.Close();
			fileStream.Close();
		}
        /// <summary>    
        /// 从文件读取 Stream    
        /// </summary>
		public static Stream FileToStream(string fileName)
		{
			FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			byte[] array = new byte[(int)((object)((IntPtr)fileStream.Length))];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			return new MemoryStream(array);
		}
        /// <summary>    
        /// 将文件读取到缓冲区中    
        /// </summary>    
        /// <param name="filePath">文件的绝对路径</param>
		public static byte[] FileToBytes(string filePath)
		{
			int fileSize = FileUtil.GetFileSize(filePath);
			byte[] array = new byte[fileSize];
			FileInfo fileInfo = new FileInfo(filePath);
			FileStream fileStream = fileInfo.Open(FileMode.Open);
			byte[] result;
			try
			{
				fileStream.Read(array, 0, fileSize);
				result = array;
			}
			catch (IOException ex)
			{
				throw ex;
			}
			finally
			{
				fileStream.Close();
			}
			return result;
		}
        /// <summary>    
        /// 将文件读取到字符串中    
        /// </summary>    
        /// <param name="filePath">文件的绝对路径</param>
		public static string FileToString(string filePath)
		{
			return FileUtil.FileToString(filePath, Encoding.Default);
		}
        /// <summary>    
        /// 将文件读取到字符串中    
        /// </summary>    
        /// <param name="filePath">文件的绝对路径</param>    
        /// <param name="encoding">字符编码</param>
		public static string FileToString(string filePath, Encoding encoding)
		{
			string result;
			try
			{
				using (StreamReader streamReader = new StreamReader(filePath, encoding))
				{
					result = streamReader.ReadToEnd();
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
			return result;
		}
        /// <summary>    
        /// 从嵌入资源中读取文件内容(e.g: xml).    
        /// </summary>    
        /// <param name="fileWholeName">嵌入资源文件名，包括项目的命名空间.</param>    
        /// <returns>资源中的文件内容.</returns>
		public static string ReadFileFromEmbedded(string fileWholeName)
		{
			string result = string.Empty;
			using (TextReader textReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(fileWholeName)))
			{
				result = textReader.ReadToEnd();
			}
			return result;
		}
        /// <summary>    
        /// 获取文件编码    
        /// </summary>    
        /// <param name="filePath">文件绝对路径</param>    
        /// <returns></returns>
		public static Encoding GetEncoding(string filePath)
		{
			return FileUtil.GetEncoding(filePath, Encoding.Default);
		}
        /// <summary>    
        /// 获取文件编码    
        /// </summary>    
        /// <param name="filePath">文件绝对路径</param>    
        /// <param name="defaultEncoding">找不到则返回这个默认编码</param>    
        /// <returns></returns>
		public static Encoding GetEncoding(string filePath, Encoding defaultEncoding)
		{
			Encoding result = defaultEncoding;
			using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4))
			{
				if (fileStream != null && fileStream.Length >= 2L)
				{
					long position = fileStream.Position;
					fileStream.Position = 0L;
					int[] array = new int[]
					{
						fileStream.ReadByte(), 
						fileStream.ReadByte(), 
						fileStream.ReadByte(), 
						fileStream.ReadByte()
					};
					fileStream.Position = position;
					if (array[0] == 254 && array[1] == 255)
					{
						result = Encoding.BigEndianUnicode;
					}
					if (array[0] == 255 && array[1] == 254)
					{
						result = Encoding.Unicode;
					}
					if (array[0] == 239 && array[1] == 187 && array[2] == 191)
					{
						result = Encoding.UTF8;
					}
				}
			}
			return result;
		}
        /// <summary>    
        /// 获取一个文件的长度,单位为Byte    
        /// </summary>    
        /// <param name="filePath">文件的绝对路径</param>
		public static int GetFileSize(string filePath)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			return (int)fileInfo.Length;
		}
        /// <summary>    
        /// 获取一个文件的长度,单位为KB    
        /// </summary>    
        /// <param name="filePath">文件的路径</param>
		public static double GetFileSizeKB(string filePath)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			return ConvertHelper.ToDouble<double>(Convert.ToDouble(fileInfo.Length) / 1024.0, 1.0);
		}
        /// <summary>    
        /// 获取一个文件的长度,单位为MB    
        /// </summary>    
        /// <param name="filePath">文件的路径</param>
		public static double GetFileSizeMB(string filePath)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			return ConvertHelper.ToDouble<double>(Convert.ToDouble(fileInfo.Length) / 1024.0 / 1024.0, 1.0);
		}
        /// <summary>    
        /// 向文本文件中写入内容    
        /// </summary>    
        /// <param name="filePath">文件的绝对路径</param>    
        /// <param name="content">写入的内容</param>
		public static void WriteText(string filePath, string content)
		{
			File.WriteAllText(filePath, content, Encoding.Default);
		}
        /// <summary>    
        /// 向文本文件的尾部追加内容    
        /// </summary>    
        /// <param name="filePath">文件的绝对路径</param>    
        /// <param name="content">写入的内容</param>
		public static void AppendText(string filePath, string content)
		{
			File.AppendAllText(filePath, content, Encoding.Default);
		}
        /// <summary>    
        /// 将源文件的内容复制到目标文件中    
        /// </summary>    
        /// <param name="sourceFilePath">源文件的绝对路径</param>    
        /// <param name="destFilePath">目标文件的绝对路径</param>
		public static void Copy(string sourceFilePath, string destFilePath)
		{
			File.Copy(sourceFilePath, destFilePath, true);
		}
        /// <summary>    
        /// 将文件移动到指定目录    
        /// </summary>    
        /// <param name="sourceFilePath">需要移动的源文件的绝对路径</param>    
        /// <param name="descDirectoryPath">移动到的目录的绝对路径</param>
		public static void Move(string sourceFilePath, string descDirectoryPath)
		{
			string fileName = FileUtil.GetFileName(sourceFilePath);
			if (Directory.Exists(descDirectoryPath))
			{
				if (FileUtil.IsExistFile(descDirectoryPath + "\\" + fileName))
				{
					FileUtil.DeleteFile(descDirectoryPath + "\\" + fileName);
				}
				File.Move(sourceFilePath, descDirectoryPath + "\\" + fileName);
			}
		}
        /// <summary>    
        /// 检测指定文件是否存在,如果存在则返回true。    
        /// </summary>    
        /// <param name="filePath">文件的绝对路径</param>
		public static bool IsExistFile(string filePath)
		{
			return File.Exists(filePath);
		}
        /// <summary>    
        /// 创建一个文件。    
        /// </summary>    
        /// <param name="filePath">文件的绝对路径</param>
		public static void CreateFile(string filePath)
		{
			try
			{
				if (!File.Exists(filePath))
				{
					File.Create(filePath);
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}
        /// <summary>    
        /// 创建一个文件,并将字节流写入文件。    
        /// </summary>    
        /// <param name="filePath">文件的绝对路径</param>    
        /// <param name="buffer">二进制流数据</param>
		public static void CreateFile(string filePath, byte[] buffer)
		{
			try
			{
				if (!File.Exists(filePath))
				{
					using (FileStream fileStream = File.Create(filePath))
					{
						fileStream.Write(buffer, 0, buffer.Length);
					}
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}
        /// <summary>    
        /// 获取文本文件的行数    
        /// </summary>    
        /// <param name="filePath">文件的绝对路径</param>
		public static int GetLineCount(string filePath)
		{
			string[] array = File.ReadAllLines(filePath);
			return array.Length;
		}
        /// <summary>    
        /// 从文件的绝对路径中获取文件名( 包含扩展名 )    
        /// </summary>    
        /// <param name="filePath">文件的绝对路径</param>
		public static string GetFileName(string filePath)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			return fileInfo.Name;
		}
        /// <summary>    
        /// 从文件的绝对路径中获取文件名( 不包含扩展名 )    
        /// </summary>    
        /// <param name="filePath">文件的绝对路径</param>
		public static string GetFileNameNoExtension(string filePath)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			return fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf('.'));
		}
        /// <summary>    
        /// 从文件的绝对路径中获取扩展名    
        /// </summary>    
        /// <param name="filePath">文件的绝对路径</param>
		public static string GetExtension(string filePath)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			return fileInfo.Extension;
		}
        /// <summary>    
        /// 清空文件内容    
        /// </summary>    
        /// <param name="filePath">文件的绝对路径</param>
		public static void ClearFile(string filePath)
		{
			File.Delete(filePath);
			FileUtil.CreateFile(filePath);
		}
        /// <summary>    
        /// 删除指定文件    
        /// </summary>    
        /// <param name="filePath">文件的绝对路径</param>
		public static void DeleteFile(string filePath)
		{
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
		}
        /// <summary>    
        /// 文件是否存在或无权访问    
        /// </summary>    
        /// <param name="path">相对路径或绝对路径</param>    
        /// <returns>如果是目录也返回false</returns>
		public static bool FileIsExist(string path)
		{
			return File.Exists(path);
		}
        /// <summary>    
        /// 文件是否只读    
        /// </summary>    
        /// <param name="fullpath"></param>    
        /// <returns></returns>
		public static bool FileIsReadOnly(string fullpath)
		{
			FileInfo fileInfo = new FileInfo(fullpath);
			return (fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
		}
        /// <summary>    
        /// 设置文件是否只读    
        /// </summary>    
        /// <param name="fullpath"></param>    
        /// <param name="flag">true表示只读，反之</param> 
		public static void SetFileReadonly(string fullpath, bool flag)
		{
			FileInfo fileInfo = new FileInfo(fullpath);
			if (flag)
			{
				fileInfo.Attributes |= FileAttributes.ReadOnly;
			}
			else
			{
				fileInfo.Attributes &= ~FileAttributes.ReadOnly;
			}
		}
        /// <summary>    
        /// 取文件名    
        /// </summary>    
        /// <param name="fullpath"></param>    
        /// <returns></returns>
		public static string GetFileName(string fullpath, bool removeExt)
		{
			FileInfo fileInfo = new FileInfo(fullpath);
			string text = fileInfo.Name;
			if (removeExt)
			{
				text = text.Remove(text.IndexOf('.'));
			}
			return text;
		}
        /// <summary>    
        /// 取文件创建时间    
        /// </summary>    
        /// <param name="fullpath"></param>    
        /// <returns></returns>
		public static DateTime GetFileCreateTime(string fullpath)
		{
			FileInfo fileInfo = new FileInfo(fullpath);
			return fileInfo.CreationTime;
		}
        /// <summary>    
        /// 取文件最后存储时间    
        /// </summary>    
        /// <param name="fullpath"></param>    
        /// <returns></returns>
		public static DateTime GetLastWriteTime(string fullpath)
		{
			FileInfo fileInfo = new FileInfo(fullpath);
			return fileInfo.LastWriteTime;
		}
        /// <summary>    
        /// 创建一个零字节临时文件    
        /// </summary>    
        /// <returns></returns>
		public static string CreateTempZeroByteFile()
		{
			return Path.GetTempFileName();
		}
        /// <summary>    
        /// 创建一个随机文件名，不创建文件本身    
        /// </summary>    
        /// <returns></returns>
		public static string GetRandomFileName()
		{
			return Path.GetRandomFileName();
		}
        /// <summary>    
        /// 判断两个文件的哈希值是否一致    
        /// </summary>    
        /// <param name="fileName1"></param>    
        /// <param name="fileName2"></param>    
        /// <returns></returns>
		public static bool CompareFilesHash(string fileName1, string fileName2)
		{
			bool result;
			using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create())
			{
				using (FileStream fileStream = new FileStream(fileName1, FileMode.Open))
				{
					using (FileStream fileStream2 = new FileStream(fileName2, FileMode.Open))
					{
						byte[] value = hashAlgorithm.ComputeHash(fileStream);
						byte[] value2 = hashAlgorithm.ComputeHash(fileStream2);
						result = (BitConverter.ToString(value) == BitConverter.ToString(value2));
					}
				}
			}
			return result;
		}
        /// <summary>    
        /// 从XML文件转换为Object对象类型.    
        /// </summary>    
        /// <param name="path">XML文件路径</param>    
        /// <param name="type">Object对象类型</param>    
        /// <returns></returns>
		public static object LoadObjectFromXml(string path, Type type)
		{
			object result = null;
			using (StreamReader streamReader = new StreamReader(path))
			{
				string xml = streamReader.ReadToEnd();
				result = XmlConvertor.XmlToObject(xml, type);
			}
			return result;
		}
        /// <summary>    
        /// 保存对象到特定格式的XML文件    
        /// </summary>    
        /// <param name="path">XML文件路径.</param>    
        /// <param name="obj">待保存的对象</param>
		public static void SaveObjectToXml(string path, object obj)
		{
			string value = XmlConvertor.ObjectToXml(obj, true);
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				streamWriter.Write(value);
			}
		}
    }
}
