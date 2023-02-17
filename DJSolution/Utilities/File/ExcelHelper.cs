using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现对Excel的相关操作，不需要调用Office的VBA相关类。 该导出操作是基于XML文件和OleDB格式的，
    /// 因此导出Excel文件不需要客户端安装Excel软件，因此非常方便易用
    /// 可以列出Excel的所有表、列出指定表的所有列、从Excel转换为DataSet对象集合、把DataSet转换保存为Excel文件等操作
    /// </summary>
    public class ExcelHelper
    {
        public enum ExcelType
		{
			const_0,
			const_1
		}
		public enum IMEXType
		{
			ExportMode,
			ImportMode,
			LinkedMode
		}
        /// <summary>    
        /// 返回Excel 连接字符串   [IMEX=1]    
        /// </summary>    
        /// <param name="excelPath">Excel文件 绝对路径</param>    
        /// <param name="header">是否把第一行作为列名</param>    
        /// <param name="eType">Excel 版本 </param>    
        /// <returns></returns> 
		public static string GetExcelConnectstring(string excelPath, bool header, ExcelHelper.ExcelType eType)
		{
			return ExcelHelper.GetExcelConnectstring(excelPath, header, eType, ExcelHelper.IMEXType.ImportMode);
		}
        /// <summary>    
        /// 返回Excel 连接字符串    
        /// </summary>    
        /// <param name="excelPath">Excel文件 绝对路径</param>    
        /// <param name="header">是否把第一行作为列名</param>    
        /// <param name="eType">Excel 版本 </param>    
        /// <param name="imex">IMEX模式</param>    
        /// <returns></returns>
		public static string GetExcelConnectstring(string excelPath, bool header, ExcelHelper.ExcelType eType, ExcelHelper.IMEXType imex)
		{
			if (!FileUtil.IsExistFile(excelPath))
			{
				throw new FileNotFoundException("Excel路径不存在!");
			}
			string result = string.Empty;
			string text = "NO";
			if (header)
			{
				text = "YES";
			}
			if (eType == ExcelHelper.ExcelType.const_0)
			{
				result = string.Concat(new object[]
				{
					"Provider=Microsoft.ACE.OLEDB.12.0; data source=", //"Provider=Microsoft.Jet.OleDb.4.0; data source=", 
					excelPath, 
					";Extended Properties='Excel 12.0; HDR=", //";Extended Properties='Excel 8.0; HDR=", 
					text, 
					"; IMEX=", 
					imex.GetHashCode(), 
					"'"
				});
			}
			else
			{
				result = string.Concat(new object[]
				{
					"Provider=Microsoft.ACE.OLEDB.12.0; data source=", 
					excelPath, 
					";Extended Properties='Excel 12.0 Xml; HDR=", 
					text, 
					"; IMEX=", 
					imex.GetHashCode(), 
					"'"
				});
			}
			return result;
		}
        /// <summary>    
        /// 返回Excel工作表名    
        /// </summary>    
        /// <param name="excelPath">Excel文件 绝对路径</param>    
        /// <param name="eType">Excel 版本 </param>    
        /// <returns></returns>
		public static List<string> GetExcelTablesName(string excelPath, ExcelHelper.ExcelType eType)
		{
			string excelConnectstring = ExcelHelper.GetExcelConnectstring(excelPath, true, eType);
			return ExcelHelper.GetExcelTablesName(excelConnectstring);
		}
        /// <summary>    
        /// 返回Excel工作表名    
        /// </summary>    
        /// <param name="connectstring">excel连接字符串</param>    
        /// <returns></returns> 
		public static List<string> GetExcelTablesName(string connectstring)
		{
			List<string> excelTablesName;
			using (OleDbConnection oleDbConnection = new OleDbConnection(connectstring))
			{
				excelTablesName = ExcelHelper.GetExcelTablesName(oleDbConnection);
			}
			return excelTablesName;
		}
        /// <summary>    
        /// 返回Excel工作表名    
        /// </summary>    
        /// <param name="connection">excel连接</param>    
        /// <returns></returns>
		public static List<string> GetExcelTablesName(OleDbConnection connection)
		{
			List<string> list = new List<string>();
			if (connection.State == ConnectionState.Closed)
			{
				connection.Open();
			}
			DataTable oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
			if (oleDbSchemaTable != null && oleDbSchemaTable.Rows.Count > 0)
			{
				for (int i = 0; i < oleDbSchemaTable.Rows.Count; i++)
				{
					list.Add(ConvertHelper.ConvertTo<string>(oleDbSchemaTable.Rows[i][2]));
				}
			}
			return list;
		}
        /// <summary>    
        /// 返回Excel第一个工作表表名    
        /// </summary>    
        /// <param name="excelPath">Excel文件 绝对路径</param>    
        /// <param name="eType">Excel 版本 </param>    
        /// <returns></returns>
		public static string GetExcelFirstTableName(string excelPath, ExcelHelper.ExcelType eType)
		{
			string excelConnectstring = ExcelHelper.GetExcelConnectstring(excelPath, true, eType);
			return ExcelHelper.GetExcelFirstTableName(excelConnectstring);
		}
        /// <summary>    
        /// 返回Excel第一个工作表表名    
        /// </summary>    
        /// <param name="connectstring">excel连接字符串</param>    
        /// <returns></returns>
		public static string GetExcelFirstTableName(string connectstring)
		{
			string excelFirstTableName;
			using (OleDbConnection oleDbConnection = new OleDbConnection(connectstring))
			{
				excelFirstTableName = ExcelHelper.GetExcelFirstTableName(oleDbConnection);
			}
			return excelFirstTableName;
		}
        /// <summary>    
        /// 返回Excel第一个工作表表名    
        /// </summary>    
        /// <param name="connection">excel连接</param>    
        /// <returns></returns>
		public static string GetExcelFirstTableName(OleDbConnection connection)
		{
			string result = string.Empty;
			if (connection.State == ConnectionState.Closed)
			{
				connection.Open();
			}
			DataTable oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
			if (oleDbSchemaTable != null && oleDbSchemaTable.Rows.Count > 0)
			{
				result = ConvertHelper.ConvertTo<string>(oleDbSchemaTable.Rows[0][2]);
			}
			return result;
		}
        /// <summary>    
        /// 获取Excel文件中指定工作表的列    
        /// </summary>    
        /// <param name="excelPath">Excel文件 绝对路径</param>    
        /// <param name="table">名称 excel table  例如：Sheet1$</param>    
        /// <returns></returns>
		public static List<string> GetColumnsList(string excelPath, ExcelHelper.ExcelType eType, string table)
		{
			List<string> list = new List<string>();
			DataTable dataTable = null;
			string excelConnectstring = ExcelHelper.GetExcelConnectstring(excelPath, true, eType);
			using (OleDbConnection oleDbConnection = new OleDbConnection(excelConnectstring))
			{
				oleDbConnection.Open();
				dataTable = ExcelHelper.smethod_0(table, oleDbConnection);
			}
			foreach (DataRow dataRow in dataTable.Rows)
			{
				string item = dataRow["ColumnName"].ToString();
				((OleDbType)dataRow["ProviderType"]).ToString();
				dataRow["DataType"].ToString();
				list.Add(item);
			}
			return list;
		}
		private static DataTable smethod_0(object object_0, IDbConnection idbConnection_0)
		{
			DataTable result = null;
			using (IDataReader dataReader = ((IDbCommand)new OleDbCommand
			{
				CommandText = string.Format("select * from [{0}]", object_0), 
				Connection = (OleDbConnection)idbConnection_0
			}).ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
			{
				result = dataReader.GetSchemaTable();
			}
			return result;
		}
        /// <summary>    
        /// EXCEL导入DataSet    
        /// </summary>    
        /// <param name="excelPath">Excel文件 绝对路径</param>    
        /// <param name="table">名称 excel table  例如：Sheet1$ </param>    
        /// <param name="header">是否把第一行作为列名</param>    
        /// <param name="eType">Excel 版本 </param>    
        /// <returns>返回Excel相应工作表中的数据 DataSet   [table不存在时返回空的DataSet]</returns> 
		public static DataSet ExcelToDataSet(string excelPath, string table, bool header, ExcelHelper.ExcelType eType)
		{
			string excelConnectstring = ExcelHelper.GetExcelConnectstring(excelPath, header, eType);
			return ExcelHelper.ExcelToDataSet(excelConnectstring, table);
		}
		private static bool smethod_1(OleDbConnection oleDbConnection_0, string string_0)
		{
			List<string> excelTablesName = ExcelHelper.GetExcelTablesName(oleDbConnection_0);
			bool result;
			foreach (string current in excelTablesName)
			{
				if (current.ToLower() == string_0.ToLower())
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
        /// <summary>    
        /// EXCEL导入DataSet    
        /// </summary>    
        /// <param name="connectstring">excel连接字符串</param>    
        /// <param name="table">名称 excel table  例如：Sheet1$ </param>    
        /// <returns>返回Excel相应工作表中的数据 DataSet   [table不存在时返回空的DataSet]</returns>
		public static DataSet ExcelToDataSet(string connectstring, string table)
		{
			DataSet result;
			using (OleDbConnection oleDbConnection = new OleDbConnection(connectstring))
			{
				DataSet dataSet = new DataSet();
				if (ExcelHelper.smethod_1(oleDbConnection, table))
				{
					OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("SELECT * FROM [" + table + "]", oleDbConnection);
					oleDbDataAdapter.Fill(dataSet, table);
				}
				result = dataSet;
			}
			return result;
		}
        /// <summary>    
        /// EXCEL所有工作表导入DataSet    
        /// </summary>    
        /// <param name="excelPath">Excel文件 绝对路径</param>    
        /// <param name="header">是否把第一行作为列名</param>    
        /// <param name="eType">Excel 版本 </param>    
        /// <returns>返回Excel第一个工作表中的数据 DataSet </returns>
		public static DataSet ExcelToDataSet(string excelPath, bool header, ExcelHelper.ExcelType eType)
		{
			string excelConnectstring = ExcelHelper.GetExcelConnectstring(excelPath, header, eType);
			return ExcelHelper.ExcelToDataSet(excelConnectstring);
		}
        /// <summary>    
        /// EXCEL所有工作表导入DataSet    
        /// </summary>    
        /// <param name="connectstring">excel连接字符串</param>    
        /// <returns>返回Excel第一个工作表中的数据 DataSet </returns>
		public static DataSet ExcelToDataSet(string connectstring)
		{
			DataSet result;
			using (OleDbConnection oleDbConnection = new OleDbConnection(connectstring))
			{
				DataSet dataSet = new DataSet();
				List<string> excelTablesName = ExcelHelper.GetExcelTablesName(oleDbConnection);
				foreach (string current in excelTablesName)
				{
					OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("SELECT * FROM [" + current + "]", oleDbConnection);
					oleDbDataAdapter.Fill(dataSet, current);
				}
				result = dataSet;
			}
			return result;
		}        
        /// <summary>    
        /// 将DataTable到处为Excel(OleDb 方式操作）    
        /// </summary>    
        /// <param name="dataTable">表</param>    
        /// <param name="fileName">导出默认文件名</param>
		public static void DataSetToExcel(DataTable dataTable, string fileName)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "xls files (*.xls)|*.xls";
			saveFileDialog.FileName = fileName;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				fileName = saveFileDialog.FileName;
				if (File.Exists(fileName))
				{
					try
					{
						File.Delete(fileName);
					}
					catch
					{
						MessageBox.Show("该文件正在使用中,关闭文件或重新命名导出文件再试!");
						return;
					}
				}
				OleDbConnection oleDbConnection = new OleDbConnection();
				OleDbCommand oleDbCommand = new OleDbCommand();
				string text = "";
				try
				{
					oleDbConnection.ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + fileName + ";Extended ProPerties=\"Excel 8.0;HDR=Yes;\"";
					oleDbConnection.Open();
					oleDbCommand.CommandType = CommandType.Text;
					oleDbCommand.Connection = oleDbConnection;
					text = "CREATE TABLE sheet1 (";
					for (int i = 0; i < dataTable.Columns.Count; i++)
					{
						if (i < dataTable.Columns.Count - 1)
						{
							text = text + "[" + dataTable.Columns[i].Caption + "] TEXT(100) ,";
						}
						else
						{
							text = text + "[" + dataTable.Columns[i].Caption + "] TEXT(200) )";
						}
					}
					oleDbCommand.CommandText = text;
					oleDbCommand.ExecuteNonQuery();
					for (int j = 0; j < dataTable.Rows.Count; j++)
					{
						text = "INSERT INTO sheet1 VALUES('";
						for (int i = 0; i < dataTable.Columns.Count; i++)
						{
							if (i < dataTable.Columns.Count - 1)
							{
								text = text + dataTable.Rows[j][i].ToString() + " ','";
							}
							else
							{
								text = text + dataTable.Rows[j][i].ToString() + " ')";
							}
						}
						oleDbCommand.CommandText = text;
						oleDbCommand.ExecuteNonQuery();
					}
					MessageBox.Show("导出EXCEL成功");
				}
				catch (Exception ex)
				{
					MessageBox.Show("导出EXCEL失败:" + ex.Message);
				}
				finally
				{
					oleDbCommand.Dispose();
					oleDbConnection.Close();
					oleDbConnection.Dispose();
				}
			}
		}
    }
}
