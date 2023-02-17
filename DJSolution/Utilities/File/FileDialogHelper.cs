using System;
using System.Drawing;
using System.Windows.Forms;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现打开、保存文件对话框的操作，如常用的图片文件、Excel文件、
    /// Access文件、文本文件、压缩文件、颜色等对话框的操作。 
    /// </summary>
    public class FileDialogHelper
    {
        private const string string_0 = "配置文件(*.cfg)|*.cfg|All File(*.*)|*.*";
        private static string string_1 = "Excel(*.xls;*.xlsx)|*.xls;*.xlsx|All File(*.*)|*.*";
        private static string string_2 = "Image Files(*.BMP;*.bmp;*.JPG;*.jpg;*.GIF;*.gif;*.png)|(*.BMP;*.bmp;*.JPG;*.jpg;*.GIF;*.gif;*.png)|All File(*.*)|*.*";
        private static string string_3 = "HTML files (*.html;*.htm)|*.html;*.htm|All files (*.*)|*.*";
        private static string string_4 = "Access(*.mdb)|*.mdb|All File(*.*)|*.*";
        private static string string_5 = "Zip(*.zip)|*.zip|All files (*.*)|*.*";
        private static string string_6 = "(*.txt)|*.txt|All files (*.*)|*.*";
        private static string string_7 = "XML文件(*.xml)|*.xml|All File(*.*)|*.*";
        private static string string_8 = "Excel(*.xls;*.xlsx)|*.xls;*.xlsx";
        private FileDialogHelper()
        {
        }
        /// <summary>    
        /// 打开Txt对话框    
        /// </summary>    
        /// <returns></returns> 
        public static string OpenText()
        {
            return FileDialogHelper.Open("文本文件选择", FileDialogHelper.string_6);
        }
        /// <summary>    
        /// 保存Txt对话框,并返回保存全路径    
        /// </summary>    
        /// <returns></returns> 
        public static string SaveText()
        {
            return FileDialogHelper.SaveText(string.Empty);
        }
        /// <summary>    
        /// 保存Txt对话框,并返回保存全路径    
        /// </summary>    
        /// <returns></returns>
        public static string SaveText(string filename)
        {
            return FileDialogHelper.Save("保存文本文件", FileDialogHelper.string_6, filename);
        }
        /// <summary>    
        /// 保存Txt对话框,并返回保存全路径    
        /// </summary>    
        /// <param name="filename">要保存的文件名</param>
        /// <param name="initialDirectory">初始路径</param>
        /// <returns></returns>
        public static string SaveText(string filename, string initialDirectory)
        {
            return FileDialogHelper.Save("保存文本文件", FileDialogHelper.string_6, filename, initialDirectory);
        }
        /// <summary>
        /// 打开Excel对话框
        /// </summary>
        /// <returns></returns>
        public static string OpenExcel()
        {
            return FileDialogHelper.Open("Excel选择", FileDialogHelper.string_1);
        }
        /// <summary>
        /// 打开Excel对话框
        /// </summary>
        /// <param name="showTitle">要显示的标题</param>
        /// <returns></returns>
        public static string OpenExcel(string showTitle)
        {
            string title = showTitle.Trim().Length == 0 ? "Excel文件选择" : showTitle;
            return FileDialogHelper.Open(title, FileDialogHelper.string_8);
        }
        /// <summary>
        /// 保存Excel对话框,并返回保存全路径 
        /// </summary>
        /// <returns></returns>
        public static string SaveExcel()
        {
            return FileDialogHelper.SaveExcel(string.Empty);
        }
        /// <summary>
        /// 保存Excel对话框,并返回保存全路径 
        /// </summary>
        /// <param name="filename">Excel文件名</param>
        /// <returns></returns>
        public static string SaveExcel(string filename)
        {
            return FileDialogHelper.Save("保存Excel", FileDialogHelper.string_1, filename);
        }
        /// <summary>
        /// 保存Excel对话框,并返回保存全路径 
        /// </summary>
        /// <param name="filename">Excel文件名</param>
        /// <param name="initialDirectory">初始路径</param>
        /// <returns></returns>
        public static string SaveExcel(string filename, string initialDirectory)
        {
            return FileDialogHelper.Save("保存Excel", FileDialogHelper.string_1, filename, initialDirectory);
        }
        /// <summary>
        /// 打开Xml对话框
        /// </summary>
        /// <returns></returns>
        public static string OpenXml()
        {
            return FileDialogHelper.Open("Xml文件选择", FileDialogHelper.string_7);
        }
        /// <summary>
        /// 保存Xml对话框,并返回保存全路径 
        /// </summary>
        /// <returns></returns>
        public static string SaveXml()
        {
            return FileDialogHelper.SaveXml(string.Empty);
        }
        /// <summary>
        /// 保存Xml对话框,并返回保存全路径 
        /// </summary>
        /// <param name="filename">Excel文件名</param>
        /// <returns></returns>
        public static string SaveXml(string filename)
        {
            return FileDialogHelper.Save("保存Xml", FileDialogHelper.string_7, filename);
        }
        /// <summary>
        /// 保存Xml对话框,并返回保存全路径 
        /// </summary>
        /// <param name="filename">Xml文件名</param>
        /// <param name="initialDirectory">初始路径</param>
        /// <returns></returns>
        public static string SaveXml(string filename, string initialDirectory)
        {
            return FileDialogHelper.Save("保存Xml", FileDialogHelper.string_7, filename, initialDirectory);
        }
        /// <summary>
        /// 保存Html对话框
        /// </summary>
        /// <returns></returns>
        public static string OpenHtml()
        {
            return FileDialogHelper.Open("Html选择", FileDialogHelper.string_3);
        }
        /// <summary>
        /// 保存Html对话框,并返回保存全路径 
        /// </summary>
        /// <returns></returns>
        public static string SaveHtml()
        {
            return FileDialogHelper.SaveHtml(string.Empty);
        }
        /// <summary>
        /// 保存Html对话框,并返回保存全路径 
        /// </summary>
        /// <param name="filename">Html文件名</param>
        /// <returns></returns>
        public static string SaveHtml(string filename)
        {
            return FileDialogHelper.Save("保存Html", FileDialogHelper.string_3, filename);
        }
        /// <summary>
        /// 保存Html对话框,并返回保存全路径 
        /// </summary>
        /// <param name="filename">Html文件名</param>
        /// <param name="initialDirectory">初始路径</param>
        /// <returns></returns>
        public static string SaveHtml(string filename, string initialDirectory)
        {
            return FileDialogHelper.Save("保存Html", FileDialogHelper.string_3, filename, initialDirectory);
        }
        /// <summary>
        /// 打开Zip对话框
        /// </summary>
        /// <returns></returns>
        public static string OpenZip()
        {
            return FileDialogHelper.Open("压缩文件选择", FileDialogHelper.string_5);
        }
        /// <summary>
        /// 打开Zip对话框
        /// </summary>
        /// <param name="filename">Zip文件名</param>
        /// <returns></returns>
        public static string OpenZip(string filename)
        {
            return FileDialogHelper.Open("压缩文件选择", FileDialogHelper.string_5, filename);
        }
        /// <summary>
        /// 保存Zip对话框,并返回保存全路径 
        /// </summary>
        /// <returns></returns>
        public static string SaveZip()
        {
            return FileDialogHelper.SaveZip(string.Empty);
        }
        /// <summary>
        /// 保存Zip对话框,并返回保存全路径 
        /// </summary>
        /// <param name="filename">Zip文件名</param>
        /// <returns></returns>
        public static string SaveZip(string filename)
        {
            return FileDialogHelper.Save("压缩文件保存", FileDialogHelper.string_5, filename);
        }
        /// <summary>
        /// 保存Zip对话框,并返回保存全路径 
        /// </summary>
        /// <param name="filename">Zip文件名</param>
        /// <param name="initialDirectory">初始路径</param>
        /// <returns></returns>
        public static string SaveZip(string filename, string initialDirectory)
        {
            return FileDialogHelper.Save("压缩文件保存", FileDialogHelper.string_5, filename, initialDirectory);
        }
        /// <summary>
        /// 打开Image对话框
        /// </summary>
        /// <returns></returns>
        public static string OpenImage()
        {
            return FileDialogHelper.Open("图片选择", FileDialogHelper.string_2);
        }
        /// <summary>
        /// 保存Image对话框,并返回保存全路径 
        /// </summary>
        /// <returns></returns>
        public static string SaveImage()
        {
            return FileDialogHelper.SaveImage(string.Empty);
        }
        /// <summary>
        /// 保存Image对话框,并返回保存全路径 
        /// </summary>
        /// <param name="filename">Image文件名</param>
        /// <returns></returns>
        public static string SaveImage(string filename)
        {
            return FileDialogHelper.Save("保存图片", FileDialogHelper.string_2, filename);
        }
        /// <summary>
        /// 保存Image对话框,并返回保存全路径 
        /// </summary>
        /// <param name="filename">Image文件名</param>
        /// <param name="initialDirectory">初始路径</param>
        /// <returns></returns>
        public static string SaveImage(string filename, string initialDirectory)
        {
            return FileDialogHelper.Save("保存图片", FileDialogHelper.string_2, filename, initialDirectory);
        }
        public static string SaveAccessDb()
        {
            return FileDialogHelper.Save("数据库备份", FileDialogHelper.string_4);
        }
        public static string SaveBakDb()
        {
            return FileDialogHelper.Save("数据库备份", "Access(*.bak)|*.bak");
        }
        public static string OpenBakDb(string file)
        {
            return FileDialogHelper.Open("数据库还原", "Access(*.bak)|*.bak", file);
        }
        public static string OpenAccessDb()
        {
            return FileDialogHelper.Open("数据库还原", FileDialogHelper.string_4);
        }
        public static string SaveConfig()
        {
            return FileDialogHelper.Save("配置文件备份", "配置文件(*.cfg)|*.cfg|All File(*.*)|*.*");
        }
        public static string OpenConfig()
        {
            return FileDialogHelper.Open("配置文件还原", "配置文件(*.cfg)|*.cfg|All File(*.*)|*.*");
        }
        public static string OpenDir()
        {
            return FileDialogHelper.OpenDir(string.Empty);
        }
        public static string OpenDir(string selectedPath)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择路径";
            folderBrowserDialog.SelectedPath = selectedPath;
            string result;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                result = folderBrowserDialog.SelectedPath;
            }
            else
            {
                result = string.Empty;
            }
            return result;
        }
        public static string Open(string title, string filter, string filename)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = filter;
            openFileDialog.Title = title;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FileName = filename;
            string result;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                result = openFileDialog.FileName;
            }
            else
            {
                result = string.Empty;
            }
            return result;
        }
        public static string Open(string title, string filter)
        {
            return FileDialogHelper.Open(title, filter, string.Empty);
        }
        public static string Save(string title, string filter, string filename)
        {
            return FileDialogHelper.Save(title, filter, filename, "");
        }
        public static string Save(string title, string filter, string filename, string initialDirectory)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = filter;
            saveFileDialog.Title = title;
            saveFileDialog.FileName = filename;
            saveFileDialog.RestoreDirectory = true;
            if (!string.IsNullOrEmpty(initialDirectory))
            {
                saveFileDialog.InitialDirectory = initialDirectory;
            }
            string result;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                result = saveFileDialog.FileName;
            }
            else
            {
                result = string.Empty;
            }
            return result;
        }
        public static string Save(string title, string filter)
        {
            return FileDialogHelper.Save(title, filter, string.Empty);
        }
        public static Color PickColor()
        {
            Color result = SystemColors.Control;
            ColorDialog colorDialog = new ColorDialog();
            if (DialogResult.OK == colorDialog.ShowDialog())
            {
                result = colorDialog.Color;
            }
            return result;
        }
        public static Color PickColor(Color color)
        {
            Color result = SystemColors.Control;
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = color;
            if (DialogResult.OK == colorDialog.ShowDialog())
            {
                result = colorDialog.Color;
            }
            return result;
        }
    }
}
