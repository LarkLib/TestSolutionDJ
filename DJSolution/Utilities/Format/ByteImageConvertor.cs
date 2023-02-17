using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现提供字节数组和图片之间的转换操作
    /// </summary>
    public sealed class ByteImageConvertor
    {
        private ByteImageConvertor()
        {
        }
        /// <summary>    
        /// 把Image对象转换为Byte数组    
        /// </summary>    
        /// <param name="image">图片Image对象</param>    
        /// <returns>字节集合</returns> 
        public static byte[] ImageToBytes(Image image)
        {
            byte[] result = null;
            if (image != null)
            {
                Monitor.Enter(image);
                try
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        image.Save(memoryStream, ImageFormat.Png);
                        result = memoryStream.GetBuffer();
                    }
                }
                finally
                {
                    Monitor.Exit(image);
                }
            }
            return result;
        }
        /// <summary>    
        /// 把Image对象转换为Byte数组    
        /// </summary>    
        /// <param name="image">image对象</param>    
        /// <param name="imageFormat">图片格式（后缀名）</param>    
        /// <returns></returns>
        public static byte[] ImageToBytes(Image image, ImageFormat imageFormat)
        {
            byte[] result;
            if (image == null)
            {
                result = null;
            }
            else
            {
                byte[] array = null;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (Bitmap bitmap = new Bitmap(image))
                    {
                        bitmap.Save(memoryStream, imageFormat);
                        memoryStream.Position = 0L;
                        array = new byte[(int)((object)((IntPtr)memoryStream.Length))];
                        memoryStream.Read(array, 0, Convert.ToInt32(memoryStream.Length));
                        memoryStream.Flush();
                    }
                }
                result = array;
            }
            return result;
        }
        /// <summary>    
        /// 转换Byte数组到Image对象    
        /// </summary>    
        /// <param name="bytes">字节数组</param>    
        /// <returns>Image图片</returns>
        public static Image ImageFromBytes(byte[] bytes)
        {
            Image result = null;
            try
            {
                if (bytes != null)
                {
                    MemoryStream memoryStream = new MemoryStream(bytes, false);
                    using (memoryStream)
                    {
                        result = ByteImageConvertor.smethod_0(memoryStream);
                    }
                }
            }
            catch
            {
            }
            return result;
        }
        /// <summary>    
        /// 转换地址（文件路径或者URL地址）到Image对象    
        /// </summary>    
        /// <param name="url">图片地址（文件路径或者URL地址）</param>    
        /// <returns>Image对象</returns>
        public static Image ImageFromUrl(string url)
        {
            Image result = null;
            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    Uri uri = new Uri(url);
                    if (uri.IsFile)
                    {
                        FileStream fileStream = new FileStream(uri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        using (fileStream)
                        {
                            result = ByteImageConvertor.smethod_0(fileStream);
                            goto IL_93;
                        }
                    }
                    WebClient webClient = new WebClient();
                    using (webClient)
                    {
                        byte[] buffer = webClient.DownloadData(uri);
                        MemoryStream memoryStream = new MemoryStream(buffer, false);
                        using (memoryStream)
                        {
                            result = ByteImageConvertor.smethod_0(memoryStream);
                        }
                    }
                }
            IL_93: ;
            }
            catch
            {
            }
            return result;
        }
        private static Image smethod_0(Stream stream_0)
        {
            Image result = null;
            try
            {
                stream_0.Position = 0L;
                Image image = Image.FromStream(stream_0);
                using (image)
                {
                    result = new Bitmap(image);
                }
            }
            catch
            {
                try
                {
                    stream_0.Position = 0L;
                    Icon icon = new Icon(stream_0);
                    if (icon != null)
                    {
                        result = icon.ToBitmap();
                    }
                }
                catch
                {
                }
            }
            return result;
        }
        /// <summary>    
        /// byte[]数组转换为Bitmap    
        /// </summary>    
        /// <param name="bytes">byte[]数组</param>    
        /// <returns></returns>
        public static Bitmap BitmapFromBytes(byte[] bytes)
        {
            Bitmap result = null;
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(bytes))
                {
                    result = new Bitmap(new Bitmap(memoryStream));
                }
            }
            catch
            {
            }
            return result;
        }
        /// <summary>    
        /// Bitmap对象转换为byte 数组    
        /// </summary>    
        /// <param name="bitmap">Bitmap对象</param>    
        /// <returns></returns> 
        public static byte[] BitmapToBytes(Bitmap bitmap)
        {
            byte[] result = null;
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    bitmap.Save(memoryStream, bitmap.RawFormat);
                    result = new byte[(int)((object)((IntPtr)memoryStream.Length))];
                    result = memoryStream.ToArray();
                }
            }
            catch
            {
            }
            return result;
        }
    }
}
