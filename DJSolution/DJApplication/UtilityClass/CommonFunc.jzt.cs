using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DJ.LMS.Utilities;
using System.Xml;
using System.Reflection;
using System.Drawing;
using ZXing;
using ZXing.Common;
using ThoughtWorks.QRCode.Codec;
using DevExpress.XtraGrid.Views.Grid;

namespace DJ.LMS.WinForms
{
    public sealed partial class CommonFunc
    {
        //Get item main info
        public static DataTable GetItemMainItems()
        {
            return SqlDbHelper.GetDataSet("select * from ItemMain where IsAllowCustomerEdit=1 order by ItemOrder").Tables[0];
        }

        //Get Item List
        public static DataTable GetItemListItems(int itemID)
        {
            return SqlDbHelper.GetDataSet(string.Format("select * from ItemList where ItemID={0} order by ItemListOrder", itemID)).Tables[0];
        }

        //update item main
        public static bool ModifyItemMain(int itemID, string itemCode, string itemName, bool isEffictive, string remark, int itemParentID = 0)
        {
            string sql = null;
            if (itemID > 0)
                sql = string.Format("update ItemMain set ItemCode='{0}',ItemName='{1}',IsEffictive={2},Remark='{3}',itemParentID={4} where ItemID={5}", itemCode.Replace("'", "''"), itemName.Replace("'", "''"), isEffictive ? 1 : 0, remark?.Replace("'", "''"), itemParentID, itemID);
            else
                sql = string.Format("insert ItemMain (ItemCode,ItemName,IsEffictive,Remark,ItemOrder,itemParentID) values('{0}','{1}',{2},'{3}',0,{4}) ", itemCode.Replace("'", "''"), itemName.Replace("'", "''"), isEffictive ? 1 : 0, remark?.Replace("'", "''"), itemParentID);

            return SqlDbHelper.ExecuteNonQuery(sql) > 0 ? true : false;
        }

        //update item main
        public static bool ModifyItemList(int itemListID, int itemID, string itemCode, string itemName, bool isEffictive, int itemOrder, string remark)
        {
            string sql = null;
            if (itemListID > 0)
                sql = string.Format("update ItemList set ItemListCode='{0}',ItemListName='{1}',IsEffective={2},ItemListOrder={5},Remark='{3}' where ItemListID={4}", itemCode.Replace("'", "''"), itemName.Replace("'", "''"), isEffictive ? 1 : 0, remark.Replace("'", "''"), itemListID, itemOrder);
            else
                sql = string.Format("insert ItemList (ItemListCode,ItemListName,IsEffective,Remark,ItemListOrder,ItemID) values('{0}','{1}',{2},'{3}',{5},{4}) ", itemCode.Replace("'", "''"), itemName.Replace("'", "''"), isEffictive ? 1 : 0, remark.Replace("'", "''"), itemID, itemOrder);

            return SqlDbHelper.ExecuteNonQuery(sql) > 0 ? true : false;
        }

        //Get last ItemListOrder by Item ID
        public static int GetLastOrderByItemID(int id)
        {
            string sql = string.Format("select coalesce(max(ItemListOrder),0)+1  ItemListOrder from ItemList where ItemID={0}", id);
            var table = SqlDbHelper.GetDataSet(sql).Tables[0];
            return (int)table.Rows[0][0];
        }

        //Del Item Main
        public static bool DelItemMain(int id)
        {
            string sql = string.Format("delete ItemList where ItemID={0};delete ItemMain where ItemID={0}", id);
            return SqlDbHelper.ExecuteNonQuery(sql) > 0 ? true : false;
        }
        //Del Item List
        public static bool DelItemList(int id)
        {
            string sql = string.Format("delete ItemList where ItemListID={0}", id);
            return SqlDbHelper.ExecuteNonQuery(sql) > 0 ? true : false;

        }

        //update wms base data
        public static bool ModifyWmsBaseData(DataRow row)
        {
            string sql = null;
            var tableName = row.Table.TableName;
            var id = (int)row["ID"];
            if (id > 0)
            {
                sql = "update {0} set {1} where ID={2}";
                StringBuilder subSql = new StringBuilder();
                foreach (DataColumn item in row.Table.Columns)
                {
                    if (item.ColumnName.Equals("ID")) continue;
                    subSql.AppendFormat("{0}='{1}',", item.ColumnName, row[item.ColumnName] == null ? string.Empty : row[item.ColumnName].ToString().Replace("'", "''"));
                }
                sql = string.Format(sql, tableName, subSql.ToString().TrimEnd(new char[] { ',' }), id);
            }
            else
            {
                sql = "insert {0} ({1}) values({2})";
                StringBuilder fields = new StringBuilder();
                StringBuilder values = new StringBuilder();

                foreach (DataColumn item in row.Table.Columns)
                {
                    if (item.ColumnName.Equals("ID")) continue;
                    fields.AppendFormat("{0},", item.ColumnName);
                    values.AppendFormat("'{0}',", row[item.ColumnName] == null ? string.Empty : row[item.ColumnName].ToString().Replace("'", "''"));
                }
                sql = string.Format(sql, tableName, fields.ToString().TrimEnd(new char[] { ',' }), values.ToString().TrimEnd(new char[] { ',' }));
            }
            return SqlDbHelper.ExecuteNonQuery(sql) > 0 ? true : false;
        }

        //Del  wms base data
        public static bool DelWmsBaseData(string tableName, int id)
        {
            string sql = string.Format("delete {1} where ID={0};", id, tableName);
            return SqlDbHelper.ExecuteNonQuery(sql) > 0 ? true : false;
        }

        /// <summary>
        /// 获取业务类型
        /// </summary>
        /// <param name="operType">操作类型：1-入库;2-出库;3-调拨;0-全部</param>
        /// <returns></returns>
        public static DataTable GetBizType(int operType)
        {
            string sql = string.Format("select BizType,BizTypeName from BusinessType where (OperType={0} or {0}=0) and IsEffective=1", operType);
            return smethod_0(sql);
        }

        internal static bool SubmitBizChooseOutbound(DataTable dataTable, DataTable chooseList, ref string message)
        {
            throw new NotImplementedException();
        }

        internal static DataTable GetAuditResultDataTable()
        {
            throw new NotImplementedException();
        }

        internal static DataTable AuditPlan(string code, object p1, object p2, string p3)
        {
            throw new NotImplementedException();
        }

        internal static DataTable GetBizTypeInfo(object p)
        {
            throw new NotImplementedException();
        }

        internal static bool AuditSuccessModifyStorage(string code, ref string message)
        {
            throw new NotImplementedException();
        }

        internal static DataTable GetChooseOutboundListDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("StorageListID", System.Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("CellID", System.Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("CellCode", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("Code", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("LineId", System.Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("ItemCode", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("ItemName", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("Quantity", System.Type.GetType("System.Decimal")));
            return table;
        }

        internal static DataTable GetItemStoreQuantity(long _warehouse, DataTable table)
        {
            throw new NotImplementedException();
        }

        internal static DataTable GetRemotePlanByCode(object bizType, DataTable planCode)
        {
            throw new NotImplementedException();
        }

        public static DataTable GetRemotePlanByCode(object bizType, string planCode)
        {
            try
            {
                DataTable localBizCondition = smethod_0(string.Format("select BizTypeName from BusinessType where BizType='{0}'", bizType));
                if (localBizCondition == null || localBizCondition.Rows.Count == 0)
                    throw new Exception("要查找的业务类型不存在");
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("select * from v_PlanList where BizType='{0}' and RemotePlanCode='{1}'", bizType, planCode);
                DataTable remotePlan = SqlDbHelper.GetDataSet(sb.ToString()).Tables[0];
                //DataTable remotePlan = smethod_0(sb.ToString());
                if (remotePlan == null || remotePlan.Rows.Count == 0)
                    throw new Exception(string.Format("U9数据库中不存在业务单{0}的信息.", planCode));
                return remotePlan;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal static object GetLocalPlanByCode(object planCode)
        {
            throw new NotImplementedException();
        }

        internal static DataTable GetPlanDataTable()
        {
            throw new NotImplementedException();
        }

        internal static DataTable GetSystemChooseScheme(DataTable table)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取所有的物料类别
        /// </summary>
        /// <returns></returns>
        public static DataTable GetItemCategory(bool includeMaterial = true)
        {
            try
            {
                return smethod_0(string.Format(@"
with t as
(
SELECT sysid, name AS Name, number AS ID, RIGHT(longnumber, CHARINDEX('!', REVERSE(longnumber) + '!') - 1) AS ParentID FROM dbo.MaterialGroup
{0}
)
select Name,ID,ParentID from t where sysid in (select min(sysid) from t group by ID) order by id
",
!includeMaterial ? string.Empty : "union select ID + 1000000, material_name, number, materialGroup_number from Material m where m.ID in (select min(ID) from Material group by number)"));
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取单据信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPlanMainQuery(string whereString)
        {
            try
            {
                return smethod_0(string.Format(@"select * from v_PlanMain vpm {0} order by CreateTime desc", whereString));
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取养护信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMaintainPlanQuery(string whereString)
        {
            try
            {
                return smethod_0(string.Format(@"select * from v_MaintainPlan m {0} order by CreateTime desc", whereString));
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取养护信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetLimsReportQuery(string whereString)
        {
            try
            {
                return smethod_0(string.Format(@"select * from v_LimsReport {0} order by CreateTime desc", whereString));
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        //获取打印条码
        public static DataTable GetPlanListDetailForPrint(string localPlanCode)
        {
            return smethod_0(string.Format("select * from v_PlanListDetail where PlanListID='{0}' order by LocalPlanCode desc,ID", localPlanCode));
        }

        //打印条码查询条件
        public static DataTable GetPlanListDetailForSearch()
        {
            return smethod_0("select distinct CONCAT(FactoryLotNo,WorkLotNo) FWId, p.InspStatusName, p.ID, p.BizTypeName,FactoryLotNo,LocalPlanCode,RemotePlanCode,gmpnumber,gmpname,UnitName,Quantity,SupplierName,SuppLotNo from v_PlanList p join BusinessType b on p.BizType=b.BizType where b.OperType=1 order by LocalPlanCode desc");
        }

        //Lims送检查询条件
        public static DataTable GetPlanListDetailForLimsSearch()
        {
            return smethod_0("select distinct  CONCAT(FactoryLotNo,WorkLotNo) FWId, p.InspStatusName, p.ID, p.BizTypeName,FactoryLotNo,LocalPlanCode,RemotePlanCode,gmpnumber,gmpname,UnitName,Quantity,SupplierName,SuppLotNo from v_PlanList p join BusinessType b on p.BizType=b.BizType where p.InspStatus='Waiting' and b.OperType=1 order by LocalPlanCode desc");
        }
        //获取未完成任务信息
        public static DataTable GetPlanLisToFinish(string where)
        {
            return smethod_0(string.Format("select *,convert(nvarchar(20),CreateTime,20) CreateTimeString from v_PlanList {0} order by LocalPlanCode desc,ID", where));
        }

        //未完成任务查询条件
        public static DataTable GetPlanListToFinishForFactoryLotNoSearch()
        {
            return smethod_0("select distinct FactoryLotNo,gmpnumber,gmpname,UnitName from v_PlanList where PlanStatus<>'Complete' and coalesce(FactoryLotNo,'')<>''");
        }

        //未完成任务查询条件
        public static DataTable GetPlanListToFinishForWorkLotNoSearch()
        {
            return smethod_0("select distinct WorkLotNo,gmpnumber,gmpname,UnitName from v_PlanList where PlanStatus<>'Complete' and coalesce(WorkLotNo,'')<>''");
        }

        //完成未完成任务查询条件
        public static bool SetPlanListToFinish(DataTable ids, ref string message)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ids", SqlDbType.Structured),
                new SqlParameter("@message",SqlDbType.NVarChar,500),
                new SqlParameter("@return", SqlDbType.Int)
            };

            parameters[0].Value = ids;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Direction = ParameterDirection.ReturnValue;

            return smethod_2("up_FinishPlanList", ref message, parameters);

            //var sql = string.Format("update planlist set PlanStatus='Complete',CompleteTime=convert(nvarchar(20),getdate(),20) where ID={0} and PlanStatus<>'Complete'", id);
            //return smethod_4(sql);
        }

        //Del Item List
        public static bool DelLimsReportReason(int id)
        {
            string sql = string.Format("delete LimsReportReason where ID={0}", id);
            return SqlDbHelper.ExecuteNonQuery(sql) > 0 ? true : false;
        }

        //物料大类
        public static Dictionary<int, string> MaterialCategory = new Dictionary<int, string>()
        {
            {-1,"<--请选择-->" },
            {1,"原辅包材" },
            {2,"中间品与自加工饮片" },
            {3,"产成品" }
        };

        //根据物料编码获取物料大类
        public static int GetMaterialCategoryByItemCode(string itemCode)
        {
            var result = -1;

            if (string.IsNullOrWhiteSpace(itemCode))
            {
                result = -1;
            }
            else if (itemCode.StartsWith("17") || itemCode.StartsWith("0302") || itemCode.StartsWith("0305"))
            {
                result = 2; //中间品与自加工饮片
            }
            else if (itemCode.StartsWith("01"))
            {
                result = 3; //产成品
            }
            else
            {
                result = 1; //原辅包材
            }

            return result;
        }

        //检查plan list item是否需要提交检测报告
        public static int CheckPlanListForLimsReport(int id, ref string message)
        {
            var result = 0;
            var table = SqlDbHelper.GetDataSet(string.Format("select * from v_PlanList where id={0}", id)).Tables[0];
            if (table != null && table.Rows.Count > 0)
            {
                //todo: need update
                var row = table.Rows[0];
                var inspStatus = row["InspStatus"].ToString();
                if (!string.IsNullOrWhiteSpace(inspStatus) && inspStatus.Equals("Waiting"))
                {
                    result = 1;//等检状态, 
                }
                else
                {
                    message = "该批次物料已经检验完成.";
                }
            }
            return result;
        }

        /// <summary>
        /// 获取初检记录信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetInitialInspectionRecordQuery(string tableName, string whereString)
        {
            try
            {
                return smethod_0(string.Format(@"select * from {0} {1} order by CheckDate desc", tableName, whereString));
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 验证导入库存信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DataTable ImportStorageVerify(ref string message)
        {

            SqlParameter[] parameters = {
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),    new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Direction = ParameterDirection.ReturnValue;
            return smethod_1("up_ImportStorageVerify", parameters);
        }

        /// <summary>
        /// 导入库存信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool ImportStorage(ref string message)
        {

            SqlParameter[] parameters = {
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),    new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_ImportStorage", ref message, parameters);
        }

        ///生成盘点计划
        public static bool CreateStorageCheckPlan(DateTime date, string type, string owner, string creator, DataTable storageListIDs, ref string message)
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(long));

            SqlParameter[] parameters =
            {
                new SqlParameter("@validDays", SqlDbType.Int),              //0
                new SqlParameter("@planType", SqlDbType.NVarChar,50),       //1
                new SqlParameter("@ownerNumber", SqlDbType.NVarChar,50),    //2
                new SqlParameter("@creatorNumber", SqlDbType.NVarChar,50),  //3
                new SqlParameter("@beginCheckDate", SqlDbType.NVarChar,50), //4
                new SqlParameter("@endCheckDate", SqlDbType.NVarChar,50),   //5
                new SqlParameter("@source", SqlDbType.NVarChar,50),         //6
                new SqlParameter("@storageListIDs", SqlDbType.Structured),  //7
                new SqlParameter("@message",SqlDbType.NVarChar,500),        //8
                new SqlParameter("@return", SqlDbType.Int)                  //9
            };

            parameters[0].Value = 0;
            parameters[1].Value = type;
            parameters[2].Value = owner;
            parameters[3].Value = creator;
            parameters[4].Value = date.ToString("yyyy-MM-dd");
            parameters[5].Value = null;
            parameters[6].Value = "人工创建";
            parameters[7].Value = storageListIDs ?? table;
            parameters[8].Direction = ParameterDirection.Output;
            parameters[9].Direction = ParameterDirection.ReturnValue;

            return smethod_2("up_CreateStorageCheckPlan", ref message, parameters);
        }

        ///盘点计划-上报盘盈盘亏
        public static bool UploadStorageCheckPlan(string creatorNumber, DataTable checkPlanIDs, ref string message)
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(long));

            SqlParameter[] parameters =
            {
                new SqlParameter("@creatorNumber", SqlDbType.NVarChar,50),
                new SqlParameter("@checkPlanIDs", SqlDbType.Structured),
                new SqlParameter("@message",SqlDbType.NVarChar,500),
                new SqlParameter("@return", SqlDbType.Int)
            };

            parameters[0].Value = creatorNumber;
            parameters[1].Value = checkPlanIDs ?? table;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3].Direction = ParameterDirection.ReturnValue;

            return smethod_2("up_UploadStorageCheckPlan", ref message, parameters);
        }
        /// <summary>
        /// 批量更改库存状态
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool UpdateMaterialStatusByFwid(string fwid, string status, decimal contenQty, ref string message)
        {

            SqlParameter[] parameters = {
                new SqlParameter("@fwid",SqlDbType.NVarChar,255),
                new SqlParameter("@status",SqlDbType.NVarChar,255),
                new SqlParameter("@contentQty",SqlDbType.NVarChar,255),
                new SqlParameter("@message",SqlDbType.NVarChar,500),
                new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = fwid;
            parameters[1].Value = status;
            parameters[2].Value = contenQty;
            parameters[3].Direction = ParameterDirection.Output;
            parameters[4].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_UpdateMaterialStatusByFwid", ref message, parameters);
        }

    }
    public static class Extensions
    {
        public static bool IsNotNull(this object obj)
        {
            return obj != null && !string.IsNullOrWhiteSpace(obj.ToString());
        }
        public static void AllowCopyCellData(this GridView grid)
        {
            grid.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            grid.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            grid.OptionsSelection.MultiSelect = true;
        }
    }
}
