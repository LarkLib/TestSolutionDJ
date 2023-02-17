using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
#pragma warning disable 0618

namespace DJ.LMS.Utilities
{
    public sealed class OracleDbHelper
    {
        private static string connString = new AppConfig().GetKeyValue("connectionString");
        public static bool Exists(string tableName, string sWhere)
        {
            try
            {
                if (tableName == null || tableName.Trim().Length == 0) return false;
                if (sWhere == null || sWhere.Trim().Length == 0)
                    return Convert.ToInt32(OracleHelper.ExecuteScalar(connString, CommandType.Text,
                        string.Format("SELECT COUNT(*) FROM DUAL WHERE EXISTS (SELECT 1 FROM {0})", tableName))) > 0;
                else
                    return Convert.ToInt32(OracleHelper.ExecuteScalar(connString, CommandType.Text,
                        string.Format("SELECT COUNT(*) FROM DUAL WHERE EXISTS (SELECT 1 FROM {0} WHERE {1})", tableName, sWhere))) > 0;
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public static DataSet GetDataSet(string commandText)
        {
            try
            {
                return (commandText == null || commandText.Trim().Length == 0) ? null : OracleHelper.ExecuteDataset(connString, CommandType.Text, commandText);
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public static DataSet GetDataSet(string tableName, string sField, string sWhere)
        {
            try
            {
                if (tableName == null || tableName.Trim().Length == 0) return null;
                string field = string.IsNullOrEmpty(sField.Trim()) ? "*" : sField;

                if (sWhere == null || sWhere.Trim().Length == 0)
                    return OracleHelper.ExecuteDataset(connString, CommandType.Text, string.Format("SELECT {0} FROM {1}", field, tableName));
                else
                    return OracleHelper.ExecuteDataset(connString, CommandType.Text, string.Format("SELECT {0} FROM {1} WHERE {2}", field, tableName, sWhere));
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public static DataSet GetDataSet(string storedProcedureName, OracleParameter[] parameters)
        {
            try
            {
                return (storedProcedureName == null || storedProcedureName.Trim().Length == 0) ? null :
                    OracleHelper.ExecuteDataset(connString, CommandType.StoredProcedure, storedProcedureName, parameters);
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public static int ExecuteNonQuery(string commandText)
        {
            try
            {
                if (commandText == null || commandText.Trim().Length == 0) return 0;
                return OracleHelper.ExecuteNonQuery(connString, CommandType.Text, commandText);
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public static int ExecuteNonQuery(string storedProcedureName, OracleParameter[] parameters)
        {
            try
            {
                return (storedProcedureName == null || storedProcedureName.Trim().Length == 0) ? 0 :
                    OracleHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, storedProcedureName, parameters);
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public static object GetFieldValue(string sField, string tableName, string sWhere)
        {
            try
            {
                if ((tableName == null || tableName.Trim().Length == 0) || (sField == null || sField.Trim().Length == 0)) return null;

                if (sWhere == null || sWhere.Trim().Length == 0)
                    return OracleHelper.ExecuteScalar(connString, CommandType.Text, string.Format("SELECT {0} FROM {1}", sField, tableName));
                else
                    return OracleHelper.ExecuteScalar(connString, CommandType.Text, string.Format("SELECT {0} FROM {1} WHERE {2}", sField, tableName, sWhere));
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public static DataTable GetFlagTable(string ITEM_CODE)
        {
            try
            {
                return OracleHelper.ExecuteDataset(connString, CommandType.Text,
                    string.Format("select list_code value,list_name name from v_item where upper(item_code)=upper('{0}') order by list_order", ITEM_CODE)).Tables[0];
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public static bool IsKey(string columnName, string tableName)
        {
            if ((columnName == null || columnName.Trim().Length == 0) || (tableName == null || tableName.Trim().Length == 0))
            {
                return false;
            }
            string result = GetTablePrimaryKey(tableName);
            return columnName.ToUpper().Trim().Equals(result.ToUpper().Trim()) ? true : false;
        }

        public static string GetTablePrimaryKey(string tableName)
        {
            try
            {
                if (tableName == null || tableName.Trim().Length == 0)
                    return string.Empty;
                string sql = string.Format("select col.column_name from user_constraints con," +
                    "user_cons_columns col where con.constraint_name=col.constraint_name and " +
                    "con.constraint_type='P' and upper(col.table_name) = upper('{0}')", tableName);
                return OracleHelper.ExecuteScalar(connString, CommandType.Text, sql).ToString();
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}
