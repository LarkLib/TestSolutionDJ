using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现CSV文件和DataTable对象的相互转换。
    /// 逗号分隔型取值格式（英文全称为Comma Separated Values，简称CSV），是一种纯文本格式，用来存储数据。
    /// 在CSV中，数据的字段由逗号分开，程序通过读取文件重新创建正确的字段，方法是每次遇到逗号时开始新一段数据。
    /// CSV除了可以用记事本等文本工具打开外，还可以用Excel打开，其效果和Excel很类似，因此二维表格数据一般也可以导出成CSV格式的文件。 
    /// 由于CSV文件可以使用Excel打开并操作，但导出CSV文件不需要客户端安装Excel软件，因此非常方便易用。
    /// </summary>
    public class CSVHelper
    {
        /// <summary>    
        /// CSV转换成DataTable（OleDb数据库访问方式）    
        /// </summary>    
        /// <param name="csvPath">csv文件路径</param>    
        /// <returns></returns>
        public static DataTable CSVToDataTableByOledb(string csvPath)
        {
            DataTable dataTable = new DataTable("csv");
            if (!File.Exists(csvPath))
            {
                throw new FileNotFoundException("csv文件路径不存在!");
            }
            FileInfo fileInfo = new FileInfo(csvPath);
            using (OleDbConnection oleDbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileInfo.DirectoryName + ";Extended Properties='Text;'"))
            {
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("SELECT * FROM [" + fileInfo.Name + "]", oleDbConnection);
                oleDbDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }
        /// <summary>    
        /// CSV转换成DataTable（文件流方式）    
        /// </summary>    
        /// <param name="csvPath">csv文件路径</param>    
        /// <returns></returns>
        public static DataTable CSVToDataTableByStreamReader(string csvPath)
        {
            DataTable dataTable = new DataTable("csv");
            int num = 0;
            bool flag = true;
            using (StreamReader streamReader = new StreamReader(csvPath, FileUtil.GetEncoding(csvPath)))
            {
                string text;
                while (!string.IsNullOrEmpty(text = streamReader.ReadLine()))
                {
                    string[] array = text.Split(new char[] { ',' });
                    if (flag)
                    {
                        flag = false;
                        num = array.Length;
                        for (int i = 0; i < array.Length; i++)
                        {
                            DataColumn column = new DataColumn(array[i]);
                            dataTable.Columns.Add(column);
                        }
                    }
                    else
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i < num; i++)
                        {
                            dataRow[i] = array[i];
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }
            }
            return dataTable;
        }
        /// <summary>    
        /// DataTable 生成 CSV    
        /// </summary>    
        /// <param name="dt">DataTable</param>    
        /// <param name="csvPath">csv文件路径</param>
        public static void DataTableToCSV(DataTable dt, string csvPath)
        {
            if (null != dt)
            {
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder stringBuilder2 = new StringBuilder();
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    stringBuilder2.Append(",");
                    stringBuilder2.Append(dataColumn.ColumnName);
                }
                stringBuilder.AppendLine(stringBuilder2.ToString().Substring(1));
                foreach (DataRow dataRow in dt.Rows)
                {
                    stringBuilder2 = new StringBuilder();
                    foreach (DataColumn dataColumn in dt.Columns)
                    {
                        stringBuilder2.Append(",");
                        stringBuilder2.Append(dataRow[dataColumn.ColumnName].ToString().Replace(',', ' '));
                    }
                    stringBuilder.AppendLine(stringBuilder2.ToString().Substring(1));
                }
                File.WriteAllText(csvPath, stringBuilder.ToString(), Encoding.Default);
            }
        }
    }
}
