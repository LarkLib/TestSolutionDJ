﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DJ.LMS.Utilities
{
    public sealed class SqlDbHelper
    {

        private static bool enableCryptConnectionString;
        private static bool EnableCryptConnectionString = bool.TryParse(new AppConfig().GetKeyValue("EnableCryptConnectionString"), out enableCryptConnectionString) ? enableCryptConnectionString : false;
        private static string connString = EnableCryptConnectionString ? EncodeHelper.AES_Decrypt(new AppConfig().GetKeyValue("connectionString")) : new AppConfig().GetKeyValue("connectionString");
        private static string testConnString = EnableCryptConnectionString ? EncodeHelper.AES_Decrypt(new AppConfig().GetKeyValue("testConnString")) : new AppConfig().GetKeyValue("testConnString");
        public static bool Exists(string tableName, string sWhere)
        {
            try
            {
                if (tableName == null || tableName.Trim().Length == 0) return false;
                return (sWhere == null || sWhere.Trim().Length == 0) ?
                    Convert.ToInt32(SqlHelper.ExecuteScalar(connString, CommandType.Text, string.Format("SELECT COUNT(*) FROM {0}", tableName))) > 0 :
                    Convert.ToInt32(SqlHelper.ExecuteScalar(connString, CommandType.Text, string.Format("SELECT COUNT(*) FROM {0} WHERE {1}", tableName, sWhere))) > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataSet GetDataSet(string commandText)
        {
            try
            {
                return (commandText == null || commandText.Trim().Length == 0) ? null : SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataSet GetDataSetFromTestDataBase(string commandText)
        {
            try
            {
                return (commandText == null || commandText.Trim().Length == 0) ? null : SqlHelper.ExecuteDataset(testConnString, CommandType.Text, commandText);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataSet GetDataSet(string tableName, string strWhere)
        {
            if (tableName == null || tableName.Trim().Length == 0) return null;
            try
            {
                return (strWhere == null || strWhere.Trim().Length == 0) ?
                    SqlHelper.ExecuteDataset(connString, CommandType.Text, string.Format("SELECT * FROM {0}", tableName)) :
                    SqlHelper.ExecuteDataset(connString, CommandType.Text, string.Format("SELECT * FROM {0} WHERE {1}", tableName, strWhere));
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataSet GetDataSet(string tableName, string strFields, string strWhere)
        {
            if (tableName == null || tableName.Trim().Length == 0) return null;
            string field = string.IsNullOrEmpty(strFields.Trim()) ? "*" : strFields;
            try
            {
                return (strWhere == null || strWhere.Trim().Length == 0) ?
                    GetDataSet(string.Format("SELECT {0} FROM {1}", field, tableName, strWhere)) :
                    GetDataSet(string.Format("SELECT {0} FROM {1} WHERE {2}", field, tableName, strWhere));
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataSet GetDataSet(string storedProcedureName, SqlParameter[] parameters)
        {
            try
            {
                return (storedProcedureName == null || storedProcedureName.Trim().Length == 0) ? null :
                    SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, storedProcedureName, parameters);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static object GetFieldValue(string tableName, string strFieldName, string strWhere)
        {
            if ((tableName == null || tableName.Trim().Length == 0) || (strFieldName == null || strFieldName.Trim().Length == 0)) return null;
            try
            {
                return (strWhere == null || strWhere.Trim().Length == 0) ?
                    SqlHelper.ExecuteScalar(connString, CommandType.Text, string.Format("SELECT {0} FROM {1}", strFieldName, tableName, strWhere)) :
                    SqlHelper.ExecuteScalar(connString, CommandType.Text, string.Format("SELECT {0} FROM {1} WHERE {2}", strFieldName, tableName, strWhere));
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static int ExecuteNonQuery(string commandText)
        {
            if (commandText == null || commandText.Trim().Length == 0) return 0;
            try
            {
                return SqlHelper.ExecuteNonQuery(connString, CommandType.Text, commandText);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static int ExecuteNonQuery(string storedProcedureName, SqlParameter[] parameters)
        {
            try
            {
                return (storedProcedureName == null || storedProcedureName.Trim().Length == 0) ? 0 :
                    SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, storedProcedureName, parameters);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static object ExecuteScalarByStoredProcedure(string storedProcedureName, SqlParameter[] parameters)
        {
            try
            {
                return (storedProcedureName == null || storedProcedureName.Trim().Length == 0) ? 0 :
                    SqlHelper.ExecuteScalar(connString, CommandType.StoredProcedure, storedProcedureName, parameters);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void ExecuteNoQueryByTransaction(ArrayList commandText)
        {
            if (commandText == null) return;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < commandText.Count; n++)
                    {
                        string strsql = commandText[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static int ExecuteNoQueryByTransaction(List<String> commandText)
        {
            if (commandText == null) return 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < commandText.Count; n++)
                    {
                        string strsql = commandText[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static DataTable GetFlagTable(string ITEM_CODE)
        {
            try
            {
                return SqlHelper.ExecuteDataset(connString, CommandType.Text,
                    string.Format("select ITEM_LIST_CODE as VALUE, ITEM_LIST_NAME as NAME from v_Item where ITEM_CODE = '{0}' order by ITEM_LIST_ORDER", ITEM_CODE)).Tables[0];
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void BulkCopyData(DataTable dataTable)
        {
            using (var connection = new SqlConnection(connString))
            {
                var bulkCopy = new SqlBulkCopy(connection);
                foreach (DataColumn item in dataTable.Columns)
                {
                    bulkCopy.ColumnMappings.Add(item.ColumnName, item.ColumnName);
                }
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                bulkCopy.DestinationTableName = dataTable.TableName;
                bulkCopy.WriteToServer(dataTable);
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }
    }
}
