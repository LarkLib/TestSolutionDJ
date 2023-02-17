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
using System.Linq;
using System.Windows.Forms;

namespace DJ.LMS.WinForms
{
    public sealed partial class CommonFunc
    {
        #region 毁伤树
        public static DataTable GetDamageTreeList()
        {
            return smethod_0("select * from v_DamageTree");
        }

        public static DataTable GetDamageTreeType()
        {
            return smethod_0("select * from v_DamageTreeType");
        }

        public static DataTable GetDamageTreeStatus()
        {
            return smethod_0("select * from v_DamageTreeStatus");
        }

        public static DataTable GetDamageTreeItemList(long DamageTreeID)
        {
            return smethod_0($"select * from v_DamageTreeItem where DamageTreeID={DamageTreeID}");
        }

        public static DataTable GetDamageTreeItemDataSchema(long DamageTreeItemID)
        {
            return smethod_0($"select * from v_DamageTreeItemDataSchema where DamageTreeItemID={DamageTreeItemID}");
        }

        public static DataTable GetDamageTreeDataTableScheme()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("Name", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("Code", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("Type", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("Status", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("Remark", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("IsEffective", System.Type.GetType("System.Int32")));
            return table;
        }

        public static DataTable GetDamageTreeItemDataTableScheme()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("Name", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("Code", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("Remark", System.Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("DamageTreeID", System.Type.GetType("System.Int64")));
            table.Columns.Add(new DataColumn("PID", System.Type.GetType("System.Int64")));
            return table;
        }

        public static bool SubmitDamageTreeInfo(DataTable damageTreeInfo, EditMode editMode, ref string message)
        {
            string sql = null;
            if (damageTreeInfo != null)
            {
                var row = damageTreeInfo.Rows[0];
                var remark = Convert.IsDBNull(row["Remark"]) ? "" : ((string)row["Remark"]).Replace("'", "''");
                if (editMode == EditMode.Add)
                {
                    var existTable = smethod_0($"select id from DamageTreeMain where Code='{((string)row["Code"]).Replace("'", "''")}'");
                    if (existTable != null && existTable.Rows.Count > 0)
                    {
                        throw new Exception("已经存在相同编码!");
                    }
                    sql = $@"insert into DamageTreeMain (Name,Code,Type,Status,Remark,IsEffective) values(
                            '{((string)row["Name"]).Replace("'", "''")}',
                            '{((string)row["Code"]).Replace("'", "''")}',
                            '{((string)row["Type"]).Replace("'", "''")}',
                            '{((string)row["Status"]).Replace("'", "''")}',
                            '{remark}',
                            '{(int)row["IsEffective"]}'
                          )";
                }
                else if (editMode == EditMode.Edit)
                {
                    sql = $@"update DamageTreeMain set 
                            Name='{((string)row["Name"]).Replace("'", "''")}',
                            Code='{((string)row["Code"]).Replace("'", "''")}',
                            Type='{((string)row["Type"]).Replace("'", "''")}',
                            Status='{((string)row["Status"]).Replace("'", "''")}',
                            Remark='{remark}',
                            IsEffective={(int)row["IsEffective"]}
                           where ID={row["ID"]}";
                }
            }
            smethod_4(sql);
            return true;
        }

        public static bool SubmitDamageTreeItem(DataTable damageTreeInfo, EditMode editMode, ref string message)
        {
            string sql = null;
            if (damageTreeInfo != null)
            {
                var row = damageTreeInfo.Rows[0];
                var remark = Convert.IsDBNull(row["Remark"]) ? "" : ((string)row["Remark"]).Replace("'", "''");
                if (editMode == EditMode.Add)
                {
                    var existTable = smethod_0($"select id from DamageTreeItem where Code='{((string)row["Code"]).Replace("'", "''")}' and DamageTreeID={(long)row["DamageTreeID"]}");
                    if (existTable != null && existTable.Rows.Count > 0)
                    {
                        throw new Exception("已经存在相同编码!");
                    }
                    sql = $@"insert into DamageTreeItem (Name,Code,Remark,DamageTreeID,PID) values(
                            '{((string)row["Name"]).Replace("'", "''")}',
                            '{((string)row["Code"]).Replace("'", "''")}',
                            '{remark}',
                            '{(long)row["DamageTreeID"]}',
                            '{(long)row["PID"]}'
                          )";
                }
                else if (editMode == EditMode.Edit)
                {
                    sql = $@"update DamageTreeItem set 
                            Name='{((string)row["Name"]).Replace("'", "''")}',
                            Code='{((string)row["Code"]).Replace("'", "''")}',
                            Remark='{remark}',
                            DamageTreeID={(long)row["DamageTreeID"]},
                            PID={(long)row["PID"]}
                            where ID={row["ID"]}";
                }
                smethod_4(sql);
            }
            return true;
        }

        public static bool SubmitDamageTreeItemDataSchema(Dictionary<string, string> damageTreeSchemaInfo, long id)
        {
            string sql = null;
            if (damageTreeSchemaInfo != null && damageTreeSchemaInfo.Count > 0)
            {
                if (id < 0)
                {
                    var existTable = smethod_0($"select id from DamageTreeItemDataSchema where Name='{((string)damageTreeSchemaInfo["Name"]).Replace("'", "''")}' and DamageTreeItemID={damageTreeSchemaInfo["DamageTreeItemID"]}");
                    if (existTable != null && existTable.Rows.Count > 0) throw new Exception("已经存在相同编码!");

                    sql = "insert into DamageTreeItemDataSchema ({0}) values({1})";
                    var fields = new StringBuilder();
                    var values = new StringBuilder();
                    foreach (var item in damageTreeSchemaInfo)
                    {
                        fields.Append(item.Key).Append(",");
                        values.Append(item.Value).Append(",");
                    }
                    fields.Length = fields.Length - 1;
                    values.Length = values.Length - 1;
                    sql = string.Format(sql, fields.ToString(), values.ToString());
                }
                else // EditMode.Edit
                {
                    sql = "update DamageTreeItemDataSchema set {0} where ID={1}";
                    var fields = new StringBuilder();
                    foreach (var item in damageTreeSchemaInfo)
                    {
                        fields.AppendFormat("{0}={1},", item.Key, item.Value);
                    }
                    fields.Length = fields.Length - 1;
                    sql = string.Format(sql, fields.ToString(), id);
                }
                smethod_4(sql);
            }
            return true;
        }

        public static bool DeleteDamageTreeItem(long id, ref string message)
        {
            smethod_4($"delete DamageTreeItem where ID={id} or PID={id}");
            return true;
        }

        public static void BuildDataSchemaItem(DataTable dtUIConfig, Control parentControl)
        {
            if (dtUIConfig == null || dtUIConfig.Rows.Count < 1 || parentControl == null) return;

            //获取最长的标签
            int leftMargin = 20;
            int topMargin = 20;
            int totolwidth = parentControl.Width - 220 - leftMargin;

            Point currentLocation = new Point(leftMargin, topMargin);
            Point nextLocation = new Point(leftMargin, topMargin);
            int label_control_width = 2;
            int y = nextLocation.Y;

            int labelMaxLength = 20;
            int controlMaxLength = 160;

            int lastY = 0;
            //UI engine
            foreach (DataRow dr in dtUIConfig.Rows)
            {
                var labelText = $"{dr["title"].ToString()}({dr["DataTypeName"].ToString()})";
                //计量字符串长度
                SizeF maxSize = parentControl.CreateGraphics().MeasureString(labelText, parentControl.Font);
                if (labelMaxLength < maxSize.Width)
                {
                    labelMaxLength = int.Parse(maxSize.Width.ToString("0"));
                }
                if (controlMaxLength < int.Parse(dr["size"].ToString().Split(',')[0]))
                {
                    controlMaxLength = int.Parse(dr["size"].ToString().Split(',')[0]);
                }
            }

            //UI Builder
            foreach (DataRow dr in dtUIConfig.Rows)
            {
                if (dr["type"].ToString().ToLower() == "button")
                {
                    //Label label = new Label();
                    //label.Location = new Point(nextLocation.X, nextLocation.Y);
                    //label.Width = labelMaxLength;//max size
                    //label.Text = "";
                    ////-----------------------------------
                    //Button ctrlItem = new Button();
                    //ctrlItem.Location = new Point(label.Right + label_control_width, nextLocation.Y);
                    //ctrlItem.Width = int.Parse(dr["size"].ToString().Split(',')[0]);
                    //ctrlItem.Height = int.Parse(dr["size"].ToString().Split(',')[1]);
                    //ctrlItem.Name = dr["name"].ToString();
                    //ctrlItem.Text = dr["title"].ToString();
                    ////  ctrlItem.Font = parentControl.Font;
                    //ctrlItem.Click += new EventHandler(ctrlItem_Click);
                    ////-------------------------------------------------------------
                    //nextLocation.X = ctrlItem.Right + 8;
                    //lastY = ctrlItem.Bottom + 16;
                    //if (nextLocation.X >= totolwidth)
                    //{
                    //    nextLocation.Y = ctrlItem.Bottom + 16;
                    //    nextLocation.X = currentLocation.X;
                    //}
                    //parentControl.Controls.Add(label);
                    //parentControl.Controls.Add(ctrlItem);

                }

                //-------------------------------------------------
                if (dr["type"].ToString().ToLower() == "CustomComboBox".ToLower())
                {
                    //Label label = new Label();
                    //label.Location = new Point(nextLocation.X, nextLocation.Y);
                    //label.Width = labelMaxLength;//max size
                    //label.Text = dr["title"].ToString();
                    ////-----------------------------------


                    ////datagridview
                    //if ((dr["config"].ToString().ToLower() == "datagridview"))
                    //{
                    //    CustomComboBox ctrlItem = new CustomComboBox();
                    //    ctrlItem.Location = new Point(label.Right + label_control_width, nextLocation.Y);
                    //    ctrlItem.Width = int.Parse(dr["size"].ToString().Split(',')[0]);
                    //    ctrlItem.Height = int.Parse(dr["size"].ToString().Split(',')[1]);
                    //    ctrlItem.Name = dr["name"].ToString();
                    //    DataGridView gridView = new DataGridView();
                    //    gridView.Columns.Add("ID", "ID");
                    //    gridView.Columns.Add("Name", "Name");
                    //    gridView.Columns.Add("Level", "Level");
                    //    ctrlItem.DropDownControl = gridView;
                    //    gridView.Rows.Add(new object[] { "001", "jack", "9" });
                    //    gridView.Rows.Add(new object[] { "002", "wang", "9" });
                    //    gridView.Font = parentControl.Font;
                    //    ctrlItem.DropDownControlType = enumDropDownControlType.DataGridView;
                    //    ctrlItem.DisplayMember = "Name";
                    //    ctrlItem.ValueMember = "ID";
                    //    //-------------------------------------------------------------
                    //    nextLocation.X = ctrlItem.Right + 8;
                    //    lastY = ctrlItem.Bottom + 16;
                    //    if (nextLocation.X >= totolwidth)
                    //    {
                    //        nextLocation.Y = ctrlItem.Bottom + 16;
                    //        nextLocation.X = currentLocation.X;
                    //    }
                    //    parentControl.Controls.Add(label);
                    //    parentControl.Controls.Add(ctrlItem);
                    //}
                    //else if (dr["config"].ToString().ToLower() == "treeview")
                    //{
                    //    //CustomComboBox ctrlItem = new CustomComboBox();
                    //    //ctrlItem.Location = new Point(label.Right + label_control_width, nextLocation.Y);
                    //    //ctrlItem.Width = int.Parse(dr["size"].ToString().Split(',')[0]);
                    //    //ctrlItem.Height = int.Parse(dr["size"].ToString().Split(',')[1]);
                    //    //ctrlItem.Name = dr["name"].ToString();
                    //    ////静态变量 2个时候默认就是最后一个
                    //    //treeView1.Font = parentControl.Font;
                    //    //ctrlItem.DropDownControlType = enumDropDownControlType.TreeView;
                    //    //ctrlItem.DropDownControl = parentControl.treeView1;
                    //    ////not empty
                    //    //ctrlItem.DisplayMember = "Name";
                    //    //ctrlItem.ValueMember = "ID";
                    //    ////-------------------------------------------------------------
                    //    //nextLocation.X = ctrlItem.Right + 8;
                    //    //lastY = ctrlItem.Bottom + 16;
                    //    //if (nextLocation.X >= totolwidth)
                    //    //{
                    //    //    nextLocation.Y = ctrlItem.Bottom + 16;
                    //    //    nextLocation.X = currentLocation.X;
                    //    //}
                    //    //parentControl.Controls.Add(label);
                    //    //parentControl.Controls.Add(ctrlItem);

                    //}
                    //else
                    //{
                    //}
                }
                //---------------------------------------------------------------
                //强制换行
                if (dr["type"].ToString().ToLower() == "datagridview")
                {
                    ////Label label = new Label();
                    ////label.Location = new Point(nextLocation.X, nextLocation.Y);
                    ////label.Width = labelMaxLength;//max size
                    ////label.Text = dr["title"].ToString();
                    ////-----------------------------------
                    //DataGridView ctrlItem = new DataGridView();
                    ////强制换行
                    //ctrlItem.Location = new Point(currentLocation.X, lastY);
                    //ctrlItem.Width = int.Parse(dr["size"].ToString().Split(',')[0]);
                    //ctrlItem.Height = int.Parse(dr["size"].ToString().Split(',')[1]);
                    //ctrlItem.Name = dr["name"].ToString();

                    ////string connString = "server=.\\sql2008r2; database=GC管理; Trusted_Connection=True; ";
                    ////MkMisII.DAO.SqlHelper.DefaultConnectionString = connString;
                    ////DataTable dtC = MkMisII.DAO.SqlHelper.GetDataTableBySQL(dr["config"].ToString());
                    //var dtC = new DataTable();
                    ////实际从数据库加载
                    //dtC.Columns.Add("name");
                    //dtC.Columns.Add("title");
                    //dtC.Columns.Add("size");
                    //dtC.Columns.Add("location");
                    //dtC.Columns.Add("type");
                    //dtC.Columns.Add("config");

                    //dtC.Rows.Add(new object[] { "ID", "ID:", "160,30", "0,0", "textbox", "" });
                    //dtC.Rows.Add(new object[] { "name", "用户名:", "160,30", "0,0", "textbox", "" });
                    //dtC.Rows.Add(new object[] { "password", "密码:", "160,30", "0,0", "passwordtext", "" });
                    //dtC.Rows.Add(new object[] { "sex", "性别:", "160,30", "0,0", "combobox", "Man,Female" });
                    //dtC.Rows.Add(new object[] { "emp", "职员:", "160,30", "0,0", "CustomComboBox", "datagridview" });
                    //dtC.Rows.Add(new object[] { "dept", "部门:", "160,30", "0,0", "CustomComboBox", "treeview" });
                    //dtC.Rows.Add(new object[] { "details", "明细:", "440,200", "0,0", "datagridview", "select * from test" });
                    //dtC.Rows.Add(new object[] { "btnSave", "保存", "160,30", "0,0", "button", "" });

                    //if (dtC != null)
                    //{
                    //    ctrlItem.DataSource = dtC;
                    //}
                    ////-------------------------------------------------------------
                    ////nextLocation.X = ctrlItem.Right + 8;
                    ////lastY = ctrlItem.Bottom + 16;
                    ////if (nextLocation.X >= totolwidth)
                    ////{
                    //nextLocation.Y = ctrlItem.Bottom + 16;
                    //nextLocation.X = currentLocation.X;
                    ////}

                    //parentControl.Controls.Add(ctrlItem);
                }
                //-------------------------------------------------
                if (dr["type"].ToString().ToLower() == "textbox")
                {
                    Label label = new Label();
                    label.Location = new Point(nextLocation.X, nextLocation.Y);
                    label.Width = labelMaxLength;//max size
                    label.Text = $"{dr["title"].ToString()}({dr["DataTypeName"].ToString()})";
                    //-----------------------------------
                    TextBox ctrlItem = new TextBox();
                    ctrlItem.Location = new Point(label.Right + label_control_width, nextLocation.Y);
                    ctrlItem.Width = int.Parse(dr["size"].ToString().Split(',')[0]);
                    ctrlItem.Height = int.Parse(dr["size"].ToString().Split(',')[1]);
                    ctrlItem.Name = dr["name"].ToString();
                    ctrlItem.Text = dr["DefaultValue"].ToString();

                    //-------------------------------------------------------------
                    nextLocation.X = ctrlItem.Right + 8;
                    lastY = ctrlItem.Bottom + 16;
                    if (nextLocation.X >= totolwidth)
                    {
                        nextLocation.Y = ctrlItem.Bottom + 16;
                        nextLocation.X = currentLocation.X;
                    }
                    parentControl.Controls.Add(label);
                    parentControl.Controls.Add(ctrlItem);

                }
                //----------------------------------------------------------
                if (dr["type"].ToString().ToLower() == "combobox")
                {
                    Label label = new Label();
                    label.Location = new Point(nextLocation.X, nextLocation.Y);
                    label.Width = labelMaxLength;
                    label.Text = $"{dr["title"].ToString()}({dr["DataTypeName"].ToString()})";

                    //-----------------------------------
                    ComboBox ctrlItem = new ComboBox();
                    ctrlItem.Location = new Point(label.Right + label_control_width, nextLocation.Y);
                    ctrlItem.Width = int.Parse(dr["size"].ToString().Split(',')[0]);
                    ctrlItem.Height = int.Parse(dr["size"].ToString().Split(',')[1]);
                    ctrlItem.Name = dr["name"].ToString();
                    string[] items = dr["config"].ToString().Split(',');
                    foreach (string item in items)
                    {
                        ctrlItem.Items.Add(item);
                    }
                    //-------------------------------------------------------------
                    nextLocation.X = ctrlItem.Right + 8;
                    lastY = ctrlItem.Bottom + 16;
                    if (nextLocation.X >= totolwidth)
                    {
                        nextLocation.Y = ctrlItem.Bottom + 16;
                        nextLocation.X = currentLocation.X;
                    }

                    ctrlItem.Text = dr["DefaultValue"].ToString();

                    parentControl.Controls.Add(label);
                    parentControl.Controls.Add(ctrlItem);

                }

                if (dr["type"].ToString().ToLower() == "passwordtext")
                {
                    Label label = new Label();
                    label.Location = new Point(nextLocation.X, nextLocation.Y);
                    label.Width = labelMaxLength;
                    label.Text = dr["title"].ToString();

                    //-----------------------------------
                    TextBox ctrlItem = new TextBox();
                    ctrlItem.PasswordChar = '*';
                    ctrlItem.Location = new Point(label.Right + label_control_width, nextLocation.Y);
                    ctrlItem.Width = int.Parse(dr["size"].ToString().Split(',')[0]);
                    ctrlItem.Height = int.Parse(dr["size"].ToString().Split(',')[1]);
                    ctrlItem.Name = dr["name"].ToString();

                    //-------------------------------------------------------------
                    nextLocation.X = ctrlItem.Right + 8;
                    lastY = ctrlItem.Bottom + 16;
                    if (nextLocation.X >= totolwidth)
                    {
                        nextLocation.Y = ctrlItem.Bottom + 16;
                        nextLocation.X = currentLocation.X;
                    }
                    parentControl.Controls.Add(label);
                    parentControl.Controls.Add(ctrlItem);

                }

                if (dr["type"].ToString().ToLower() == "radiobutton")
                {
                    Label label = new Label();
                    label.Location = new Point(nextLocation.X, nextLocation.Y);
                    label.Width = labelMaxLength;
                    label.Text = $"{dr["title"].ToString()}({dr["DataTypeName"].ToString()})";

                    //-----------------------------------
                    var ctrlItem = new GroupBox();
                    ctrlItem.Location = new Point(label.Right + label_control_width, nextLocation.Y);
                    ctrlItem.Width = int.Parse(dr["size"].ToString().Split(',')[0]);
                    var height = int.Parse(dr["size"].ToString().Split(',')[1]);
                    //ctrlItem.Height = int.Parse(dr["size"].ToString().Split(',')[1]);
                    ctrlItem.Name = dr["name"].ToString();
                    ctrlItem.Text = $"{dr["title"].ToString()}({dr["DataTypeName"].ToString()})";
                    string[] items = dr["config"].ToString().Split(',');
                    var panel = new Panel();
                    panel.Dock = DockStyle.Fill;
                    ctrlItem.Controls.Add(panel);

                    var radHeight = 0;
                    foreach (string item in items)
                    {
                        RadioButton radItem = new RadioButton();
                        radItem.Name = item;
                        radItem.Text = item;
                        radItem.Location = new Point(0, radHeight);
                        radHeight += height;
                        if (item.Equals(dr["DefaultValue"]?.ToString())) radItem.Checked = true;
                        panel.Controls.Add(radItem);
                    }
                    ctrlItem.Height = height * items.Count() + height;
                    //-------------------------------------------------------------
                    nextLocation.X = ctrlItem.Right + 8;
                    lastY = ctrlItem.Bottom + 16;
                    if (nextLocation.X >= totolwidth)
                    {
                        nextLocation.Y = ctrlItem.Bottom + 16;
                        nextLocation.X = currentLocation.X;
                    }

                    //parentControl.Controls.Add(label);
                    parentControl.Controls.Add(ctrlItem);
                }
            }
        }
        #endregion
    }
}
