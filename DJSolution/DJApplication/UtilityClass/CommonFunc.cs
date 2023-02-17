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
using DevExpress.XtraRichEdit.Import.OpenXml;
using System.Windows.Forms;
using DJ.LMS.WinForms.DianJin;
using System.Linq;

namespace DJ.LMS.WinForms
{
    public sealed partial class CommonFunc
    {
        private static string remoteConnString = new AppConfig().GetKeyValue("remoteConnString");
        private static string mesConnString = new AppConfig().GetKeyValue("mesConnString");

        #region ---导入文件模板字段操作
        public static string[] importChoose = { "料号", "料品名称", "实发数量" };
        public static string[] importStorage = { "供应商编码", "供应商", "供应商批号", "仓库编码", "货位条码", "进厂编号", "生产批号", "物料条码", "物料名称", "物料编码(EAS)", "数量", "件数", "包装规格", "检验状态", "生产日期", "有效期", "入库日期", "储存期", "复检期", "含量数据", "检验日期", "版本号(包材必须)", "备注" };
        public static string[] importStorageField = { "SupplierCode", "SupplierName", "SuppLotNo", "WarehouseID", "StoreCell", "FactoryLotNo", "WorkLotNo", "Qrcode", "ItemName", "ItemCode", "Quantity", "PieceCount", "PackingSpecification", "InspStatus", "ProductDate", "ValidDate", "InDate", "StoreLifeDate", "ReinspectionDate", "ContentQty", "InspDate", "ItemVersion", "Remark" };
        public static string[] importAssebmly = { "托盘", "料号", "料品名称", "实到数量" };
        private static bool FindField(ImportTemplateType templateType, string fieldName)
        {
            bool result = false;
            switch (templateType)
            {
                case ImportTemplateType.ChooseImport:
                    result = FindField(importChoose, fieldName);
                    break;
                case ImportTemplateType.AssemblyImport:
                    result = FindField(importAssebmly, fieldName);
                    break;
                case ImportTemplateType.StorageImport:
                    result = FindField(importStorage, fieldName);
                    break;
            }
            return result;
        }

        private static bool FindField(string[] source, string fieldName)
        {
            bool result = false;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i].Equals(fieldName))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 处理导入的数据；删除多余列和空行
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static DataTable ProcessImportTable(ImportTemplateType templateType, DataSet ds, bool isDeleteEmptyRows = true)
        {
            DataTable table = ds.Tables[0];
            for (int i = table.Columns.Count - 1; i >= 0; i--)
            {
                if (!CommonFunc.FindField(templateType, table.Columns[i].ColumnName))
                {
                    table.Columns.Remove(table.Columns[i].ColumnName);
                }
            }
            return isDeleteEmptyRows ? CommonFunc.DeleteEmptyRows(table) : table;
        }
        #endregion

        #region ---通用信息管理
        /// <summary>
        /// 获取系统用状态信息
        /// </summary>
        /// <param name="itemCode">状态分类码</param>
        /// <returns></returns>
        public static DataTable GetState(string itemCode, bool withBlank = false)
        {
            return smethod_0(string.Format("{1} select VALUE,NAME from (Select top 1000 ItemListCode as VALUE, ItemListName as NAME from v_Item where ItemCode='{0}' order by ItemListOrder) t", itemCode, withBlank ? "select null VALUE,'<-请选择->' NAME union " : string.Empty));
        }
        /// <summary>
        /// 通用ID表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetIDsDataTableScheme()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int64")));
            return table;
        }
        /// <summary>
        /// 获取用户表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUserDataTableScheme()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("LoginName", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("RealName", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("LoginPwd", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("RelationRole", System.Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("IsEffective", System.Type.GetType("System.Int32")));
            table.Columns.Add(new DataColumn("Remark", System.Type.GetType("System.String")));
            DataColumn[] clos = new DataColumn[1];
            clos[0] = table.Columns["ID"];
            table.PrimaryKey = clos;
            return table;
        }
        /// <summary>
        /// 获取角色表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRoleDataTableScheme()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("Name", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("IsEffective", System.Type.GetType("System.Int32")));
            table.Columns.Add(new DataColumn("Remark", System.Type.GetType("System.String")));
            DataColumn[] clos = new DataColumn[1];
            clos[0] = table.Columns["ID"];
            table.PrimaryKey = clos;
            return table;
        }
        /// <summary>
        /// 获取仓库表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetWarehouseDataTableScheme()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("Name", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("ErpCode", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("ErpName", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("CategoryID", System.Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("ProcessBase", System.Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("StoreArea", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("IsEffective", System.Type.GetType("System.Int32")));
            return table;
        }
        /// <summary>
        /// 获取料品信息表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetItemMasterDataTableScheme()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Code", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("Name", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("StoreUOM", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("IsEffective", System.Type.GetType("System.Int32")));
            table.Columns.Add(new DataColumn("Remark", System.Type.GetType("System.String")));
            return table;
        }

        /// <summary>
        /// 获取入库类型业务单表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPlanInboundDataTableScheme()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Code", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("LineId", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("BizType", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("BizTypeName", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("PurchaseOrder", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("PurchaseOrderLineId", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("SupplierCode", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("SupplierName", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("SupplierBatchNo", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("ItemCode", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("ItemName", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("Quantity", System.Type.GetType("System.Decimal")));
            table.Columns.Add(new DataColumn("SingleWeight", System.Type.GetType("System.Decimal")));
            table.Columns.Add(new DataColumn("StoreUOM", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("FactoryCode", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("FactoryName", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("WarehouseCode", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("WarehouseName", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("ProductionDate", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("ArrivalDate", System.Type.GetType("System.String")));
            return table;
        }
        /// <summary>
        /// 获取出库类型业务单表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPlanOutboundDataTableScheme()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Code", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("LineId", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("BizType", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("BizTypeName", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("ProductionNo", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("ItemCode", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("ItemName", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("Quantity", System.Type.GetType("System.Decimal")));
            table.Columns.Add(new DataColumn("StoreUOM", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("FactoryCode", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("FactoryName", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("WarehouseCode", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("WarehouseName", System.Type.GetType("System.String")));
            return table;
        }
        /// <summary>
        /// 获取货位设置表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetStoreCellDataTableScheme()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Code", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("Name", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("CellZ", System.Type.GetType("System.Int32")));
            table.Columns.Add(new DataColumn("CellX", System.Type.GetType("System.Int32")));
            table.Columns.Add(new DataColumn("CellY", System.Type.GetType("System.Int32")));
            return table;
        }
        /// <summary>
        /// 产成品出库表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProductOutboundDataTableScheme()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("StorageListID", System.Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("CellName", System.Type.GetType("System.String")));
            return table;
        }
        /// <summary>
        /// 原料调拨表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMaterialTransferDataTableScheme()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("StorageListID", System.Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("StorageID", System.Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("CellName", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("ItemCode", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("TotalWeight", System.Type.GetType("System.Decimal")));
            return table;
        }

        public static DataTable GetMaterialManualReturnDataTableScheme()
        {
            return new DataTable("tableMaterialManualReturn")
            {
                Columns =
                {
                    new DataColumn("BizType", System.Type.GetType("System.String")),
                    new DataColumn("ItemCode", System.Type.GetType("System.String")),
                    new DataColumn("OriginPlace", System.Type.GetType("System.String")),
                    new DataColumn("SupplierCode", System.Type.GetType("System.String")),
                    new DataColumn("SuppLotNo", System.Type.GetType("System.String")),
                    new DataColumn("FactoryLotNo", System.Type.GetType("System.String")),
                    new DataColumn("WorkLotNo", System.Type.GetType("System.String")),
                    new DataColumn("ContentQty", System.Type.GetType("System.Decimal")),
                    new DataColumn("ItemVersion", System.Type.GetType("System.String")),
                    new DataColumn("AdminOrgUnitNumber", System.Type.GetType("System.String")),
                    new DataColumn("ReturnReason", System.Type.GetType("System.String")),
                    new DataColumn("ProductDate", System.Type.GetType("System.String")),
                    new DataColumn("ValidDate", System.Type.GetType("System.String")),
                    new DataColumn("StoreLifeDate", System.Type.GetType("System.String")),
                    new DataColumn("Bzgg", System.Type.GetType("System.Decimal")),
                    new DataColumn("Quantity", System.Type.GetType("System.Decimal")),
                    new DataColumn("InspStatus", System.Type.GetType("System.String")),
                    new DataColumn("LineId", System.Type.GetType("System.Int64")),
                    new DataColumn("StoreCell", System.Type.GetType("System.Int64")),
                    new DataColumn("WarehouseID", System.Type.GetType("System.Int64"))
                }
            };
        }

        #region ---初验类表结构
        /// <summary>
        /// 获取原辅包材初验记录表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMaterialInitialInspectionRecordDataTableScheme()
        {
            return new DataTable("MaterialInitInspectionRecord")
            {
                Columns =
                {
                    new DataColumn("RemotePlanCode", System.Type.GetType("System.String")),
                    new DataColumn("BizType", System.Type.GetType("System.String")),
                    new DataColumn("LineId", System.Type.GetType("System.Int64")),
                    new DataColumn("ItemCode", System.Type.GetType("System.String")),
                    new DataColumn("SupplierCode", System.Type.GetType("System.String")),
                    new DataColumn("SupplierLotNo", System.Type.GetType("System.String")),
                    new DataColumn("FactoryLotNo", System.Type.GetType("System.String")),
                    new DataColumn("AdminOrgUnitNumber", System.Type.GetType("System.String")),
                    new DataColumn("Description", System.Type.GetType("System.String")),
                    new DataColumn("Conclusion", System.Type.GetType("System.String")),
                    new DataColumn("Measures", System.Type.GetType("System.String")),
                    new DataColumn("CheckItem", System.Type.GetType("System.String")),
                    new DataColumn("CarNo", System.Type.GetType("System.String")),
                    new DataColumn("DriverNo", System.Type.GetType("System.String")),
                    new DataColumn("SupercargoNo", System.Type.GetType("System.String")),
                    new DataColumn("Remark", System.Type.GetType("System.String"))
                }
            };
        }
        /// <summary>
        /// 获取中间品、自加工饮片和产成品初验记录表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProductInitialInspectionRecordDataTableScheme()
        {
            return new DataTable("ProductInitInspectionRecord")
            {
                Columns =
                {
                    new DataColumn("RemotePlanCode", System.Type.GetType("System.String")),
                    new DataColumn("BizType", System.Type.GetType("System.String")),
                    new DataColumn("LineId", System.Type.GetType("System.Int64")),
                    new DataColumn("ItemCode", System.Type.GetType("System.String")),
                    new DataColumn("AdminOrgUnitNumber", System.Type.GetType("System.String")),
                    new DataColumn("WorkLotNo", System.Type.GetType("System.String")),
                    new DataColumn("Description", System.Type.GetType("System.String")),
                    new DataColumn("Conclusion", System.Type.GetType("System.String")),
                    new DataColumn("Measures", System.Type.GetType("System.String")),
                    new DataColumn("CheckItem", System.Type.GetType("System.String")),
                    new DataColumn("Remark", System.Type.GetType("System.String"))
                }
            };
        }
        #endregion

        #endregion

        #region ---用户管理
        /// <summary>
        /// 获取所有的用户列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUserList()
        {
            return smethod_0("select * from UserMain where ID<>0");
        }
        /// <summary>
        /// 获取除超级用户和当前用户之外所有激活的用户
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUserListOutsideAdmin()
        {
            return smethod_0(string.Format("select * from UserMain where ID>0 and ID<>{0} and IsEffective=1", LoginUser.Instance.UserId));
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="loginName">登录用户名</param>
        /// <returns></returns>
        public static DataTable GetUser(string loginName)
        {
            string sql = string.Format("select * from v_User where LoginName='{0}'", loginName);
            return smethod_0(sql);
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userID">当前用户ID</param>
        /// <param name="pwd">新的登录密码</param>
        /// <returns></returns>
        public static bool ModifyPassword(Int64 userID, string pwd)
        {
            string sql = string.Format("update UserMain set LoginPwd='{0}' where ID={1}", pwd, userID);
            return SqlDbHelper.ExecuteNonQuery(sql) > 0 ? true : false;
        }
        /// <summary>
        /// 提交编辑的用户信息.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="relationWarehouse"></param>
        /// <param name="editMode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool SubmitUserInfo(DataTable userInfo, DataTable relationWarehouse, EditMode editMode, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@userInfo",SqlDbType.Structured),
                                            new SqlParameter("@relationWarehouse",SqlDbType.Structured),
                                            new SqlParameter("@editMode",SqlDbType.Int),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),
                                            new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = userInfo;
            parameters[1].Value = relationWarehouse;
            parameters[2].Value = editMode == EditMode.Add ? 1 : 2;
            parameters[3].Direction = ParameterDirection.Output;
            parameters[4].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_SubmitUserInfo", ref message, parameters);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="userId">要删除的用户索引</param>
        /// <param name="message">删除失败返回的异常信息</param>
        /// <returns></returns>
        public static bool DeleteUser(object userId, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@ID",SqlDbType.BigInt),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),
                                            new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = userId;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_DeleteUser", ref message, parameters);
        }
        #endregion

        #region ---角色管理
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRoleList()
        {
            return smethod_0("select * from RoleMain where ID > 0");
        }
        /// <summary>
        /// 获取可用的全部角色信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDisEnableRole()
        {
            return smethod_0("select * from RoleMain where ID>0 and IsEffective=1");
        }
        /// <summary>
        /// 获取给定用户的已选角色
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetEnableRole(object userID)
        {
            string sql = string.Format("select * from RoleMain where ID>0 and ID in (select RoleID from R_UserAndRole where UserID={0})", userID);
            return smethod_0(sql);
        }
        /// <summary>
        /// 获取给定用户的可选角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DataTable GetDisEnableRole(object userID)
        {
            string sql = string.Format("select * from RoleMain where ID>0 and IsEffective=1 and ID not in (select RoleID from R_UserAndRole where UserID={0})", userID);
            return smethod_0(sql);
        }
        /// <summary>
        /// 获取登录用户的角色列表
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static DataTable GetRelationRolesByUser(string loginName)
        {
            return loginName.Trim().ToLower().Equals("admin") ?
                smethod_0("select ID,Name from RoleMain where IsEffective=1") :
                smethod_0(string.Format("select a.RelationRole as ID,b.Name from UserMain as a left outer join RoleMain as b on a.RelationRole=b.ID where a.LoginName='{0}'", loginName));
        }
        /// <summary>
        /// 提交编辑的角色信息.
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <param name="relationMenus"></param>
        /// <param name="editMode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool SubmitRoleInfo(DataTable roleInfo, DataTable relationMenus, EditMode editMode, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@role",SqlDbType.Structured),
                                            new SqlParameter("@selectedMenus",SqlDbType.Structured),
                                            new SqlParameter("@editMode",SqlDbType.Int),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),
                                            new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = roleInfo;
            parameters[1].Value = relationMenus;
            parameters[2].Value = editMode == EditMode.Add ? 1 : 2;
            parameters[3].Direction = ParameterDirection.Output;
            parameters[4].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_SubmitRoleInfo", ref message, parameters);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleID">要删除的角色索引</param>
        /// <param name="message">删除失败返回的异常信息</param>
        /// <returns></returns>
        public static bool DeleteRole(object roleID, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@ID",SqlDbType.BigInt),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),
                                            new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = roleID;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_DeleteRole", ref message, parameters);
        }
        #endregion

        #region ---物料信息管理
        /// <summary>
        /// 获取物料信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMaterialItemMaster()
        {
            return smethod_0("select * from MaterialItemMaster");
        }
        /// <summary>
        /// 获取物料分组信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMaterialGroup()
        {
            return smethod_0("select * from MaterialGroup");
        }
        /// <summary>
        /// 获取托盘编号
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPalletCode()
        {
            return smethod_0("select * from PalletCodeMain");
        }
        /// <summary>
        /// 添加托盘条码
        /// </summary>
        /// <param name="palletCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool AddPalletCode(object palletCode, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@palletCode",SqlDbType.NVarChar,50),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),
                                            new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = palletCode;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_AddPalletCode", ref message, parameters);
        }
        /// <summary>
        /// 添加料品信息
        /// </summary>
        /// <param name="tableItem">料品信息表</param>
        /// <param name="mode">编辑模式；1：添加    2：编辑</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool SubmitItemMasterInfo(DataTable tableItem, int mode, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@item",SqlDbType.Structured),
                                            new SqlParameter("@mode",SqlDbType.Int),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),
                                            new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = tableItem;
            parameters[1].Value = mode;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_SubmitItemMasterInfo", ref message, parameters);
        }
        /// <summary>
        /// 删除指定的料品信息
        /// </summary>
        /// <param name="itemCode">料品编号</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool DelItem(object itemCode, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@itemCode",SqlDbType.NVarChar,50),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),
                                            new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = itemCode;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_DelItem", ref message, parameters);
        }
        #endregion

        #region ---仓库信息管理
        /// <summary>
        /// 获取仓库信息列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetWarehouseList()
        {
            return smethod_0("select * from v_Warehouse");
        }
        public static DataTable GetWarehouseUsedForSelect()
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("Name", "<-----请选择----->");
            hashtable.Add("ID", -1);
            return CommonFunc.MargeDataTableAndHashtable(smethod_0("select * from WarehouseMain"), hashtable);
        }
        /// <summary>
        /// 获取仓库类别信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetWarehouseCategory()
        {
            return smethod_0("select * from WarehouseCategory");
        }
        /// <summary>
        /// 获取仓库所属的生产基地
        /// </summary>
        /// <returns></returns>
        public static DataTable GetWarehouseProcessBase()
        {
            return smethod_0("select * from ProcessBaseMain");
        }
        public static DataTable GetWarehouseUsedForSelect(Int64 userId, object ModuleId)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("Name", "<-----请选择----->");
            hashtable.Add("ID", -1);
            string sql = userId == 0 ? string.Format("select * from WarehouseMain where IsEffective=1 and RelationModule={0}", ModuleId) :
                string.Format("select * from WarehouseMain where ID in (select WarehouseID from R_UserAndWarehouse where UserID={0}) and RelationModule={1} and IsEffective=1", userId, ModuleId);

            return CommonFunc.MargeDataTableAndHashtable(smethod_0(sql), hashtable);
        }
        /// <summary>
        /// 获取全部待分配的仓库信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDisEnableWarehouse()
        {
            return smethod_0("select * from WarehouseMain where IsEffective=1");
        }
        /// <summary>
        /// 获取给定用户的已选仓库信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetEnableWarehouse(object userID)
        {
            string sql = string.Format("select ID from WarehouseMain where ID in (select WarehouseID from R_UserAndWarehouse where UserID={0})", userID);
            return smethod_0(sql);
        }
        /// <summary>
        /// 获取用户关联的仓库
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetUserRelationWarehouse(object userID)
        {
            string sql = string.Format("select Name from WarehouseMain where ID in (select WarehouseID from R_UserAndWarehouse where UserID={0})", userID);
            return smethod_0(sql);
        }
        /// <summary>
        /// 提交编辑的仓库信息
        /// </summary>
        /// <param name="warehouseInfo"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool SubmitWarehouseInfo(DataTable warehouseInfo, EditMode editMode, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@warehouse",SqlDbType.Structured),
                                            new SqlParameter("@editMode",SqlDbType.Int),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),
                                            new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = warehouseInfo;
            parameters[1].Value = editMode == EditMode.Add ? 1 : 2;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_SubmitWarehouseInfo", ref message, parameters);
        }
        #endregion

        #region ---角色功能管理
        /// <summary>
        /// 获取全部权限
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMenuList()
        {
            return smethod_0("select * from MenuMain order by ParentID, MenuOrder");
        }
        /// <summary>
        /// 获取角色已选的功能菜单
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static DataTable GetEnableMenuByRole(object roleID)
        {
            string sql = string.Format("select * from MenuMain where SelectedFlag=1 and ChildNodeFlag=1 and MenuID in (select MenuID from R_RoleAndMenu where RoleID={0}) order by MenuID", roleID);
            return smethod_0(sql);
        }
        public static DataTable GetDisEnableMenu()
        {
            return smethod_0("select * from MenuMain where ChildNodeFlag=1 and SelectedFlag=1 order by MenuID");
        }
        /// <summary>
        /// 获取角色可选功能菜单
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static DataTable GetDisEnableMenuByRole(object roleID)
        {
            string sql = string.Format("select * from MenuMain where SelectedFlag=1 and ChildNodeFlag=1 and MenuID not in (select MenuID from R_RoleAndMenu where RoleID={0}) order by MenuID", roleID);
            return smethod_0(sql);
        }
        /// <summary>
        /// 获取角色关联的权限
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static DataTable GetMenusByRole(Int64 roleID)
        {
            SqlParameter[] parameters = { new SqlParameter("@roleID", SqlDbType.BigInt) };
            parameters[0].Value = roleID;
            return smethod_1("up_GetMenusByRole", parameters);
        }
        #endregion

        #region ---货位信息管理

        /// <summary>
        /// 按排进行货位的存储和运行状态统计
        /// </summary>
        /// <param name="warehouseId">仓库索引</param>
        /// <param name="line">排索引</param>
        /// <returns></returns>
        public static DataTable CellStatisticalByLine(Int64 warehouseId, int line)
        {
            string sqlText = string.Format("select (select count(*) from cellMain where StoreStatus='Full' and CellType='Cell' and WarehouseID={0} and CellZ={1}) as full_count," +
                                           "(select count(*) from cellMain where StoreStatus='Empty' and CellType='Cell' and WarehouseID={0} and CellZ={1}) as empty_count," +
                                           "(select count(*) from cellMain where StoreStatus='Exception' and CellType='Cell' and WarehouseID={0} and CellZ={1}) as exception_count," +
                                           "(select count(*) from cellMain where StoreStatus='Pallet' and CellType='Cell' and WarehouseID={0} and CellZ={1}) as pallet_count", warehouseId, line);
            return smethod_0(sqlText);
        }
        public static DataTable CellStatisticalByLine(Int64 warehouseId)
        {
            SqlParameter[] parameters = { new SqlParameter("@warehouse", SqlDbType.BigInt) };
            parameters[0].Value = warehouseId;
            return smethod_1("up_CellStatisticsForAutoWarehouse", parameters);
        }
        /// <summary>
        /// 按仓库进行货位的存储和运行状态统计
        /// </summary>
        /// <param name="warehouseId">仓库索引</param>
        /// <returns></returns>
        public static DataTable CellStatisticalByWarehouse(Int64 warehouseId)
        {
            string sqlText = string.Format("select (select count(*) from cellMain where StoreStatus='Full' and CellType='Cell' and WarehouseID={0}) as full_count," +
                                           "(select count(*) from cellMain where StoreStatus='Empty' and CellType='Cell' and WarehouseID={0}) as empty_count," +
                                           "(select count(*) from cellMain where StoreStatus='Exception' and CellType='Cell' and WarehouseID={0}) as exception_count," +
                                           "(select count(*) from cellMain where StoreStatus='Pallet' and CellType='Cell' and WarehouseID={0}) as pallet_count", warehouseId);
            return smethod_0(sqlText);
        }
        /// <summary>
        /// 获取给定仓库的所有排
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public static DataTable GetCellLine(Int64 warehouseId)
        {
            string sqlText = string.Format("select distinct top(100) percent CellZ, \'第\' + convert(varchar(2),CellZ) + \'排\' as cell_z_name from cellMain where CellType='Cell' and WarehouseID={0} order by CellZ", warehouseId);
            return smethod_0(sqlText);
        }
        /// <summary>
        /// 获取成品库所有排
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public static DataTable GetProductCellLine(Int64 warehouseId)
        {
            string sqlText = string.Format("select distinct top(100) percent InZ, \'第\' + convert(varchar(2),InZ) + \'排\' as cell_z_name from cellMain where CellType='Cell' and WarehouseID={0} order by InZ", warehouseId);
            return SqlHelper.ExecuteDataset(remoteConnString, CommandType.Text, sqlText).Tables[0];
        }
        /// <summary>
        /// 获取指定排的所有货位信息
        /// </summary>
        /// <param name="warehouseId">仓库索引值</param>
        /// <param name="line">排索引值</param>
        /// <returns></returns>
        public static DataTable GetInfoByLine(Int64 warehouseId, int line)
        {
            string sql = string.Format("select * from v_cell where WarehouseID={0} and CellZ={1} and CellType='Cell' order by CellX, CellY desc", warehouseId, line);
            return smethod_0(sql);
        }
        /// <summary>
        /// 获取成品库指定排的所有货位信息
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static DataTable GetProductCellInfoByLine(Int64 warehouseId, int line)
        {
            string sql = string.Format("select * from v_StorageChannel where WarehouseID={0} and InZ={1} and CellType='Cell' order by InX, InY desc", warehouseId, line);
            return SqlHelper.ExecuteDataset(remoteConnString, CommandType.Text, sql).Tables[0];
        }
        /// <summary>
        /// 获取货位尺寸信息
        /// </summary>
        /// <param name="warehouseId">仓库索引值</param>
        /// <param name="line">排索引</param>
        /// <returns></returns>
        public static DataTable GetCellSize(Int64 warehouseId, int line)
        {
            string sqlText = string.Format("select max(a.CellX) as max_x,max(a.CellY) as max_y,min(a.CellX) as min_x,min(a.CellY) as min_y" +
                                           " from cellMain as a where " +
                                           "a.WarehouseID={0} and CellZ={1} and a.CellType='Cell'", warehouseId, line);
            return smethod_0(sqlText);
        }
        /// <summary>
        /// 获取成品库货位尺寸信息
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static DataTable GetProductCellSize(Int64 warehouseId, int line)
        {
            string sqlText = string.Format("select max(a.InX) as max_x,max(a.InY) as max_y,min(a.InX) as min_x,min(a.InY) as min_y" +
                                           " from cellMain as a where " +
                                           "a.WarehouseID={0} and InZ={1} and a.CellType='Cell'", warehouseId, line);
            return SqlHelper.ExecuteDataset(remoteConnString, CommandType.Text, sqlText).Tables[0];
        }
        /// <summary>
        /// 更新货位状态（存储、运行）
        /// </summary>
        /// <param name="cellId">货位索引</param>
        /// <param name="storeStatus">存储状态</param>
        /// <param name="message">更新失败返回的异常信息</param>
        /// <returns></returns>
        public static bool UpdateCellState(Int64 cellId, string storeStatus, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@cell",SqlDbType.Int),                new SqlParameter("@storeStatus",SqlDbType.NVarChar,50),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),    new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = cellId;
            parameters[1].Value = storeStatus;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_ModifyCellStatus", ref message, parameters);
        }
        /// <summary>
        /// 生成存储货位
        /// </summary>
        /// <param name="warehouseID">仓库索引</param>
        /// <param name="storeCells">存储货位</param>
        /// <param name="message">失败返回的异常信息</param>
        /// <returns></returns>
        public static bool GenerateStoreCell(object warehouseID, DataTable storeCells, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@warehouseID",SqlDbType.BigInt),      new SqlParameter("@cells",SqlDbType.Structured),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),    new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = warehouseID;
            parameters[1].Value = storeCells;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_AddStoreCell", ref message, parameters);
        }

        public static DataTable GetCellForPrint(string warehouseId, bool onlyEnabledCell)
        {

            return smethod_0(string.Format("select ID,WarehouseName,Code,Name,StoreStatusName,WarehouseID from v_Cell where CellType='Cell' {0} {1} order by ID",
                string.IsNullOrWhiteSpace(warehouseId) ? string.Empty : string.Format(" and WarehouseID='{0}'", warehouseId),
                onlyEnabledCell ? " and ID in (select CellID from CellMainStatus where (Status&1)=1)" : string.Empty));
        }
        #endregion

        #region ---库存信息管理
        /// <summary>
        /// 查找指定货位的库存信息
        /// </summary>
        /// <param name="storeCell">货位索引</param>
        /// <returns></returns>
        public static DataTable SearchStorage(object storeCell)
        {
            return smethod_0(string.Format("select * from v_StorageList where StoreCell={0}", storeCell));
        }
        /// <summary>
        /// 获取料品的存储状态；用于选择
        /// </summary>
        /// <returns></returns>
        public static DataTable GetItemStorageStatus()
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("NAME", "<-----请选择----->");
            hashtable.Add("VALUE", "UNKNOWN");
            return CommonFunc.MargeDataTableAndHashtable(GetState("QUALIFED_STATUS"), hashtable);
        }
        /// <summary>
        /// 获取产成品库存统计标准
        /// </summary>
        /// <returns></returns>
        public static DataTable GetStatisticalNormForProduct()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int32")));
            table.Columns.Add(new DataColumn("Name", System.Type.GetType("System.String")));
            table.Rows.Add(1, "WMS库存");
            table.Rows.Add(2, "英克库存");
            return table;
        }
        /// <summary>
        /// 获取非产成品库存统计标准
        /// </summary>
        /// <returns></returns>
        public static DataTable GetStatisticalNormForMaterial()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int32")));
            table.Columns.Add(new DataColumn("Name", System.Type.GetType("System.String")));
            table.Rows.Add(1, "WMS库存");
            table.Rows.Add(2, "EAS库存");
            return table;
        }
        public static DataTable GetStorageDifferenceCondition()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int32")));
            table.Columns.Add(new DataColumn("Name", System.Type.GetType("System.String")));
            table.Rows.Add(1, ">0");
            table.Rows.Add(2, "=0");
            table.Rows.Add(3, "<0");
            table.Rows.Add(4, "≠0");
            Hashtable hashtable = new Hashtable();
            hashtable.Add("Name", "<-----请选择----->");
            hashtable.Add("ID", -1);
            return CommonFunc.MargeDataTableAndHashtable(table, hashtable);
        }
        /// <summary>
        /// 获取wms产成品库存统计信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetWmsProductStorageSummary()
        {
            return smethod_1("up_GetWmsProductStorageSummary", null);
        }
        /// <summary>
        /// 获取eas库存统计信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetEasMaterialStorageSummary()
        {
            return smethod_1("up_GetEasStorageSummary", null);
        }

        /// <summary>
        /// 获取非产成品无计划退货物料条码
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="lotNo"></param>
        /// <param name="lotNoType"></param>
        /// <returns></returns>
        public static string GetMaterialNoPlanReturnQrcode(string itemCode, string lotNo, int lotNoType)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@itemcode",SqlDbType.NVarChar,50),
                new SqlParameter("@lotno",SqlDbType.NVarChar,50),
                new SqlParameter("@lottype",SqlDbType.Int),
                new SqlParameter("@qrcode",SqlDbType.NVarChar,50)
            };
            parameters[0].Value = itemCode;
            parameters[1].Value = lotNo;
            parameters[2].Value = lotNoType;
            parameters[3].Direction = ParameterDirection.Output;
            return smethod_3("up_GetMaterialNoPlanReturnQrcode", parameters).ToString();
        }
        #endregion

        #region ---Mes信息
        /// <summary>
        /// 获取产成品信息
        /// </summary>
        /// <param name="orderCode"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public static DataTable GetProductInfoFromMes(object orderCode, object batchNo)
        {
            string sql = string.Format("select top 1 * from View_MES_IO_WMSBatchInfo where OrderCode='{0}' and BatchNo='{1}'", orderCode, batchNo);
            return SqlHelper.ExecuteDataset(mesConnString, CommandType.Text, sql).Tables[0];
        }

        #endregion

        #region ---任务管理
        /// <summary>
        /// 获取WMS生成的业务单号
        /// </summary>
        /// <returns></returns>
        public static string GetLocalOrderNo()
        {
            SqlParameter[] parameters = { new SqlParameter("@returnString", SqlDbType.NVarChar, 50) };
            parameters[0].Direction = ParameterDirection.ReturnValue;
            return smethod_3("up_GetLocalOrderNo", parameters).ToString();
        }
        public static DataTable GetRecordType()
        {
            var table = smethod_0("select * from RecordType");
            var row = table.NewRow();
            row[0] = -1;
            row[1] = "<-请选择->";
            table.Rows.InsertAt(row, 0);
            return table;
        }
        /// <summary>
        /// 获取原料库送料托盘信息
        /// </summary>
        /// <param name="pallet"></param>
        /// <returns></returns>
        public static DataTable GetPalletStorageFromRemote(object pallet)
        {
            string sql = string.Format("select * from PlanOutboundReported where cPalletCode='{0}' and bRead=0", pallet);
            return SqlHelper.ExecuteDataset(remoteConnString, CommandType.Text, sql).Tables[0];
        }
        /// <summary>
        /// 原材料入库
        /// </summary>
        /// <param name="pallet"></param>
        /// <param name="storeCell"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool MaterialInbound(object pallet, object storeCell, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@userID",SqlDbType.BigInt),       new SqlParameter("@palletCode",SqlDbType.NVarChar,50),
                                            new SqlParameter("@storeCell",SqlDbType.BigInt),    new SqlParameter("@message",SqlDbType.NVarChar,500),
                                            new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = LoginUser.Instance.UserId;
            parameters[1].Value = pallet;
            parameters[2].Value = storeCell;
            parameters[3].Direction = ParameterDirection.Output;
            parameters[4].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_MaterialInbound", ref message, parameters);
        }
        /// <summary>
        /// 原材料出库
        /// </summary>
        /// <param name="storeCell"></param>
        /// <param name="itemCode"></param>
        /// <param name="quantity"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool MaterialOutbound(object storeCell, object itemCode, object quantity, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@userID",SqlDbType.BigInt),
                                            new SqlParameter("@storeCell",SqlDbType.BigInt),
                                            new SqlParameter("@itemCode",SqlDbType.NVarChar,50),
                                            new SqlParameter("@quantity",SqlDbType.Decimal),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),
                                            new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = LoginUser.Instance.UserId;
            parameters[1].Value = storeCell;
            parameters[2].Value = itemCode;
            parameters[3].Value = quantity;
            parameters[4].Direction = ParameterDirection.Output;
            parameters[5].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_MaterialOutbound", ref message, parameters);
        }
        /// <summary>
        /// 原材料调拨；线边仓->零散库
        /// </summary>
        /// <param name="transferList"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool MaterialTransferToScatter(DataTable transferList, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@userID",SqlDbType.BigInt),
                                            new SqlParameter("@transfer",SqlDbType.Structured),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),
                                            new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = LoginUser.Instance.UserId;
            parameters[1].Value = transferList;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_MaterialTransferToScatter", ref message, parameters);
        }
        /// <summary>
        /// 原材料调拨；零散库->线边仓
        /// </summary>
        /// <param name="storageListID"></param>
        /// <param name="storeCell"></param>
        /// <param name="quantity"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool MaterialTransferToLineSide(object storageListID, object storeCell, object quantity, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@userID",SqlDbType.BigInt),
                                            new SqlParameter("@storageListID",SqlDbType.BigInt),
                                            new SqlParameter("@tarStoreCell",SqlDbType.BigInt),
                                            new SqlParameter("@quantity",SqlDbType.Decimal),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),
                                            new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = LoginUser.Instance.UserId;
            parameters[1].Value = storageListID;
            parameters[2].Value = storeCell;
            parameters[3].Value = quantity;
            parameters[4].Direction = ParameterDirection.Output;
            parameters[5].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_MaterialTransferToLineSide", ref message, parameters);
        }
        /// <summary>
        /// 产成品入库
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        /// <param name="storeCell"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool ProductInbound(DataRow product, object quantity, object storeCell, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@userID",SqlDbType.BigInt),           new SqlParameter("@orderCode",SqlDbType.NVarChar,50),
                                            new SqlParameter("@batchNo",SqlDbType.NVarChar,50),     new SqlParameter("@itemCode",SqlDbType.NVarChar,50),
                                            new SqlParameter("@quantity",SqlDbType.Decimal),        new SqlParameter("@storeCell",SqlDbType.BigInt),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),    new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = LoginUser.Instance.UserId;
            parameters[1].Value = product["OrderCode"];
            parameters[2].Value = product["BatchNo"];
            parameters[3].Value = product["ProductName"];
            parameters[4].Value = quantity;
            parameters[5].Value = storeCell;
            parameters[6].Direction = ParameterDirection.Output;
            parameters[7].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_ProductInbound", ref message, parameters);
        }
        /// <summary>
        /// 产成品入库-零散品
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool ProductInboundToScatter(DataRow product, object quantity, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@userID",SqlDbType.BigInt),           new SqlParameter("@orderCode",SqlDbType.NVarChar,50),
                                            new SqlParameter("@batchNo",SqlDbType.NVarChar,50),     new SqlParameter("@itemCode",SqlDbType.NVarChar,50),
                                            new SqlParameter("@quantity",SqlDbType.Decimal),        new SqlParameter("@message",SqlDbType.NVarChar,500),
                                            new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = LoginUser.Instance.UserId;
            parameters[1].Value = product["OrderCode"];
            parameters[2].Value = product["BatchNo"];
            parameters[3].Value = product["ProductName"];
            parameters[4].Value = quantity;
            parameters[5].Direction = ParameterDirection.Output;
            parameters[6].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_ProductInboundToScatter", ref message, parameters);
        }
        /// <summary>
        /// 合格的产成品出库
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool ProductQualifiedOutbound(DataTable detail, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@userid",SqlDbType.BigInt),           new SqlParameter("@detail",SqlDbType.Structured),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),    new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = LoginUser.Instance.UserId;
            parameters[1].Value = detail;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_ProductQualifiedOutbound", ref message, parameters);
        }
        /// <summary>
        /// 不合格的产成品出库
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool ProductUnqualifiedOutbound(DataTable detail, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@userid",SqlDbType.BigInt),           new SqlParameter("@detail",SqlDbType.Structured),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),    new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = LoginUser.Instance.UserId;
            parameters[1].Value = detail;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_ProductUnqualifiedOutbound", ref message, parameters);
        }
        /// <summary>
        /// 不合格的产成品调拨
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool ProductUnqualifiedTransfer(DataTable detail, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@userid",SqlDbType.BigInt),           new SqlParameter("@detail",SqlDbType.Structured),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),    new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = LoginUser.Instance.UserId;
            parameters[1].Value = detail;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_ProductUnqualifiedTansfer", ref message, parameters);
        }
        /// <summary>
        /// 产成品质检
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="batchNo"></param>
        /// <param name="itemCode"></param>
        /// <param name="status"></param>
        /// <param name="checkDate"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool ProductInspection(object orderNo, object batchNo, string status, string checkDate, ref string message)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@orderNo",SqlDbType.NVarChar,50),     new SqlParameter("@batchNo",SqlDbType.NVarChar,50),
                                            new SqlParameter("@status",SqlDbType.NVarChar,50),      new SqlParameter("@date",SqlDbType.VarChar,20),
                                            new SqlParameter("@message",SqlDbType.NVarChar,500),    new SqlParameter("@return",SqlDbType.Int)
                                        };
            parameters[0].Value = orderNo;
            parameters[1].Value = batchNo;
            parameters[2].Value = status;
            parameters[3].Value = checkDate;
            parameters[4].Direction = ParameterDirection.Output;
            parameters[5].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_ProductInspection", ref message, parameters);
        }
        /// <summary>
        /// 原辅包材、劳保五金、自加工饮片和中间品手工无计划退货
        /// </summary>
        /// <param name="tableReceivedOrder"></param>
        /// <param name="tableMaterialCheckRecord"></param>
        /// <param name="tableProductCheckRecord"></param>
        /// <param name="materialCategory"></param>
        /// <param name="qrcode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool SubmitMaterialManualReturnReceivedOrder(DataTable tableReceivedOrder, DataTable tableMaterialCheckRecord, DataTable tableProductCheckRecord, int materialCategory, string qrcode, ref string message)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@receivedorder", SqlDbType.Structured),
                new SqlParameter("@materialcheckrecord", SqlDbType.Structured),
                new SqlParameter("@productcheckrecord", SqlDbType.Structured),
                new SqlParameter("@category", SqlDbType.Int),
                new SqlParameter("@qrcode", SqlDbType.NVarChar,50),
                new SqlParameter("@opername", SqlDbType.NVarChar,50),
                new SqlParameter("@message",SqlDbType.NVarChar,500),
                new SqlParameter("@return", SqlDbType.Int)
            };
            parameters[0].Value = tableReceivedOrder;
            parameters[1].Value = tableMaterialCheckRecord;
            parameters[2].Value = tableProductCheckRecord;
            parameters[3].Value = materialCategory;
            parameters[4].Value = qrcode;
            parameters[5].Value = LoginUser.Instance.Code;
            parameters[6].Direction = ParameterDirection.Output;
            parameters[7].Direction = ParameterDirection.ReturnValue;
            return smethod_2("up_MaterialManualReturnReceivedOrder", ref message, parameters);
        }
        #endregion

        #region ---条形码生成

        /// <summary>
        /// 生成一维条形码
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public static Bitmap CreateBarcode(string text, int width, int height)
        {
            BarcodeWriter writer = new BarcodeWriter();
            //使用ITF 格式，不能被现在常用的支付宝、微信扫出来
            //如果想生成可识别的可以使用 CODE_128 格式
            //writer.Format = BarcodeFormat.ITF;
            writer.Format = BarcodeFormat.CODE_128;
            EncodingOptions options = new EncodingOptions()
            {
                Width = width,
                Height = height,
                PureBarcode = true,
                Margin = 4
            };
            writer.Options = options;
            Bitmap map = writer.Write(text);
            return map;
        }

        /// <summary>
        /// 生成物料二维码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static System.Drawing.Bitmap CreateQRcode(string code)
        {
            QRCodeEncoder encoder = new QRCodeEncoder();
            encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            encoder.QRCodeScale = 2;
            encoder.QRCodeVersion = 0;
            encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            return encoder.Encode(code, Encoding.GetEncoding("UTF-8"));
        }

        #endregion

        #region ---数据分页
        /// <summary>
        /// 获取分页数据；采用存储过程分页方式
        /// </summary>
        /// <param name="tableName">要查询的数据表名称</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="recordCount">总的记录数量</param>
        /// <returns></returns>
        public static DataTable GetPaginationData(string tableName, string strWhere, string orderBy, int pageIndex, int pageSize, out int recordCount)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                recordCount = 0;
                return null;
            }
            string sqlText = string.Format("select * from {0}", tableName);
            if (!string.IsNullOrEmpty(strWhere))
                sqlText = sqlText + string.Format(" where {0}", strWhere);
            if (!string.IsNullOrEmpty(orderBy))
                sqlText = sqlText + string.Format(" order by {0}", orderBy);
            SqlParameter[] parameters = {
                                            new SqlParameter("@sql",SqlDbType.NVarChar,2000),   new SqlParameter("@currentpage",SqlDbType.Int),
                                            new SqlParameter("@pagesize",SqlDbType.Int),        new SqlParameter("@recordCount",SqlDbType.Int)
                                        };
            parameters[0].Value = sqlText;
            parameters[1].Value = pageIndex;
            parameters[2].Value = pageSize;
            parameters[3].Direction = ParameterDirection.Output;
            try
            {
                DataSet ds = SqlDbHelper.GetDataSet("up_RecordsPagination", parameters);
                recordCount = (ds == null || ds.Tables.Count == 0) ? 0 : Convert.ToInt32(parameters[3].Value);
                return (ds == null || ds.Tables.Count == 0) ? null : ds.Tables[2];
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 获取分页数据；采用rownumber()分页方式
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="tableName">要查询的数据表名称</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="recordCount">总的记录数量</param>
        /// <returns></returns>
        public static DataTable GetPaginationData(string connString, string tableName, string strWhere, string orderBy, int pageIndex, int pageSize, out int recordCount)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                recordCount = 0;
                return null;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("select * from (select top({0}*{1}) row_number() over(order by {4}) as rownum,* from {2} where 1=1 {3}) as temp where temp.rownum>({0}*({1}-1)) order by {4}", pageSize, pageIndex, tableName, strWhere, orderBy));
            string sqlText = string.Format("select count(*) from {0} where 1=1 {1}", tableName, strWhere);
            try
            {
                recordCount = Convert.ToInt32(SqlHelper.ExecuteScalar(connString, CommandType.Text, sqlText));
                return SqlHelper.ExecuteDataset(connString, CommandType.Text, sb.ToString()).Tables[0];
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region ---辅助方法定义
        /// <summary>
        /// 执行sql语句，返回数据集
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        private static DataTable smethod_0(string commandText)
        {
            try
            {
                return SqlDbHelper.GetDataSet(commandText).Tables[0];
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 执行存储过程，返回数据集
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        private static DataTable smethod_1(string procedureName, params SqlParameter[] commandParameters)
        {
            try
            {
                return SqlDbHelper.GetDataSet(procedureName, commandParameters).Tables[0];
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <param name="message">异常信息</param>
        /// <param name="commandParameters">存储过程参数数组</param>
        /// <returns></returns>
        private static bool smethod_2(string procedureName, ref string message, params SqlParameter[] commandParameters)
        {
            try
            {
                SqlDbHelper.ExecuteNonQuery(procedureName, commandParameters);
                message = commandParameters[commandParameters.Length - 2].Value.ToString();
                return Convert.ToInt32(commandParameters[commandParameters.Length - 1].Value).Equals(0) ? true : false;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private static object smethod_3(string procedureName, params SqlParameter[] commandParameters)
        {
            try
            {
                SqlDbHelper.ExecuteScalarByStoredProcedure(procedureName, commandParameters);
                return commandParameters[commandParameters.Length - 1].Value;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 执sql
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <param name="message">异常信息</param>
        /// <param name="commandParameters">存储过程参数数组</param>
        /// <returns></returns>
        private static bool smethod_4(string commandText)
        {
            try
            {
                return Convert.ToInt32(SqlDbHelper.ExecuteNonQuery(commandText)).Equals(0) ? true : false;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 合并给定的DataTable和Hashtable
        /// </summary>
        /// <param name="dataTable">给定的DataTable</param>
        /// <param name="hashtable">给定的Hashtable</param>
        /// <returns>返回合并后的DataTable</returns>
        public static DataTable MargeDataTableAndHashtable(DataTable dataTable, System.Collections.Hashtable hashtable)
        {
            if (hashtable == null || hashtable.Count == 0)
                return dataTable;
            DataTable table = dataTable;
            if (table == null || table.Columns.Count == 0)
            {
                table = new DataTable();
                foreach (System.Collections.DictionaryEntry entry in hashtable)
                {
                    table.Columns.Add(entry.Key.ToString());
                }
            }
            DataRow row = table.NewRow();
            foreach (System.Collections.DictionaryEntry entry in hashtable)
            {
                row[entry.Key.ToString()] = entry.Value;
            }
            table.Rows.InsertAt(row, 0);
            return table;
        }
        /// <summary>
        /// 从给定的DataTable中按照筛选条件获取子集
        /// </summary>
        /// <param name="table">给定的DataTable</param>
        /// <param name="filter">筛选条件</param>
        /// <returns>返回筛选后的DataTable子集</returns>
        public static DataTable SubsetFromDataTable(DataTable table, string filter)
        {
            if (string.IsNullOrEmpty(filter) || table == null || table.Rows.Count == 0)
                return table;
            DataTable dt = table.Clone();
            foreach (DataRow row in table.Select(filter))
            {
                dt.ImportRow(row);
            }
            return dt;
        }
        /// <summary>
        /// 将给定的DataTable按照指定的字段进行排序
        /// </summary>
        /// <param name="table">给定的DataTable</param>
        /// <param name="field">排序字段</param>
        /// <returns>返回排序后的DataTable</returns>
        public static DataTable SortDataTable(DataTable table, string field)
        {
            DataTable dt = table.Copy();
            DataView dv = dt.DefaultView;
            dv.Sort = field;
            dt = dv.ToTable();
            return dt;
        }
        /// <summary>
        /// 删除DataTable中的空行
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataTable DeleteEmptyRows(DataTable table)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                bool rowdataisnull = false;
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (table.Rows[i][j] == DBNull.Value || table.Rows[i][j].ToString().Trim().Length == 0)
                    {
                        rowdataisnull = true;
                        break;
                    }
                }
                if (rowdataisnull)
                {
                    removelist.Add(table.Rows[i]);
                }
            }
            for (int i = 0; i < removelist.Count; i++)
            {
                table.Rows.Remove(removelist[i]);
            }
            return table;
        }
        /// <summary>
        /// DataTable添加选择列
        /// </summary>
        /// <param name="table">给定的DataTable</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DataTable DataTableAddCheckColumn(DataTable table, bool defaultValue = false)
        {
            int rowCount = table.Rows.Count;
            table.Columns.Add("Check", System.Type.GetType("System.Boolean"));
            for (int i = 0; i < rowCount; i++)
            {
                table.Rows[i]["Check"] = defaultValue;
            }
            return table;
        }
        #endregion
    }
}
