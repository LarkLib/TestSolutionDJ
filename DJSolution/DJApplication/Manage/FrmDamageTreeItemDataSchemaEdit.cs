using DevExpress.CodeParser;
using DevExpress.Data.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DJ.LMS.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DJ.LMS.WinForms
{
    public partial class FrmDamageTreeItemDataSchemaEdit : Form
    {
        private long LamageTreeItemID { get; set; }
        public FrmDamageTreeItemDataSchemaEdit(long damageTreeItemID, string nodeName)
        {
            InitializeComponent();
            LamageTreeItemID = damageTreeItemID;
            groupControl1.Text = $"{groupControl1.Text}(当前节点：{nodeName})";
            GridControlUtil.SetGridViewColumns(gridList, "v_DamageTreeItemDataSchema");
        }

        public FrmDamageTreeItemDataSchemaEdit()
        {
            InitializeComponent();
        }

        private void FrmDamageTreeItemDataSchemaEdit_Load(object sender, EventArgs e)
        {
            BindGridData();
            LoadTreeItemType();
            LoadTreeItemDataType();
        }


        private void BindGridData()
        {
            var dt = CommonFunc.GetDamageTreeItemDataSchema(LamageTreeItemID);
            gridControl1.DataSource = dt;
            gridList.BestFitColumns();

            this.textEditID.DataBindings.Clear();
            this.textEditID.DataBindings.Clear();
            this.textEditName.DataBindings.Clear();
            this.textEditTitle.DataBindings.Clear();
            this.textEditSize.DataBindings.Clear();
            this.textEditLocation.DataBindings.Clear();
            this.textEditConfig.DataBindings.Clear();
            this.textEditDefaultValue.DataBindings.Clear();
            this.checkEditIsEffective.DataBindings.Clear();
            this.lookUpEditDataType.DataBindings.Clear();
            this.lookUpEditType.DataBindings.Clear();
            //this.textEditDamageTreeItemID.DataBindings.Clear();

            this.textEditID.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", dt, "ID", true, DataSourceUpdateMode.Never));
            this.textEditName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", dt, "Name", true, DataSourceUpdateMode.Never));
            this.textEditTitle.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", dt, "Title", true, DataSourceUpdateMode.Never));
            this.textEditSize.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", dt, "Size", true, DataSourceUpdateMode.Never));
            this.textEditLocation.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", dt, "Location", true, DataSourceUpdateMode.Never));
            this.textEditConfig.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", dt, "Config", true, DataSourceUpdateMode.Never));
            this.textEditDefaultValue.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", dt, "DefaultValue", true, DataSourceUpdateMode.Never));
            this.checkEditIsEffective.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", dt, "IsEffective", true, DataSourceUpdateMode.Never));
            this.lookUpEditDataType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", dt, "DataType", true, DataSourceUpdateMode.Never));
            this.lookUpEditType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", dt, "Type", true, DataSourceUpdateMode.Never));
            //this.textEditDamageTreeItemID.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", dt, "DamageTreeItemID", true, DataSourceUpdateMode.Never));
            this.textEditDamageTreeItemID.EditValue = LamageTreeItemID;
            this.textEditID.EditValue = this.textEditID.EditValue.IsNotNull() ? this.textEditID.EditValue : -1;
        }

        private void LoadTreeItemType()
        {
            var table = CommonFunc.GetState("DAMAGE_TREE_DATASCHEMA_TYPE");
            lookUpEditType.Properties.DataSource = table;
        }

        private void LoadTreeItemDataType()
        {
            var table = CommonFunc.GetState("DAMAGE_TREE_DATASCHEMA_DATA_TYPE");
            lookUpEditDataType.Properties.DataSource = table;

        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.textEditName.EditValue = null;
            this.textEditTitle.EditValue = null;
            this.textEditSize.EditValue = null;
            this.textEditLocation.EditValue = null;
            this.textEditConfig.EditValue = null;
            this.textEditDefaultValue.EditValue = null;
            this.checkEditIsEffective.EditValue = true;
            this.lookUpEditDataType.EditValue = null;
            this.lookUpEditType.EditValue = null;
            this.textEditID.EditValue = -1;
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var dic = new Dictionary<string, string>();
                var controls = layoutControlDataDetail.Controls;
                long id = -1L;
                foreach (Control control in controls)
                {
                    if (control.Name.Contains("Edit"))
                    {
                        var names = control.Name.Split(new string[] { "Edit" }, StringSplitOptions.RemoveEmptyEntries);
                        if (names.Length < 2) continue;
                        if (names[1] == "ID")
                        {
                            id = Convert.ToInt64(control.Text);
                            continue;
                        }
                        else if (control is CheckEdit)
                            dic.Add(names[1], ((CheckEdit)control).Checked ? $"1" : "0");
                        else if (control is LookUpEdit)
                        {
                            var value = ((LookUpEdit)control).EditValue;
                            dic.Add(names[1], value.IsNotNull() ? $"'{value.ToString().Replace("'", "''")}'" : "null");
                        }
                        else
                            dic.Add(names[1], string.IsNullOrWhiteSpace(control.Text) ? "null" : $"'{control.Text.Replace("'", "''")}'");
                    }
                }
                if (dic.Count > 0)
                {
                    CommonFunc.SubmitDamageTreeItemDataSchema(dic, id);
                    var selectIndex = gridList.FocusedRowHandle;
                    BindGridData();
                    gridList.FocusedRowHandle = selectIndex;
                    gridList.SelectRow(selectIndex);
                }
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }

        }

        private void btnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var row = this.gridList.GetDataRow(this.gridList.FocusedRowHandle);
                if (row == null)
                {
                    MessageUtil.ShowError("请选择一条数据.");
                    return;
                }
                if (MessageUtil.ConfirmYesNo("你确认是否删除选中数据行."))
                {
                    var table = SqlDbHelper.ExecuteNonQuery(string.Format(@"delete DamageTreeItemDataSchema where ID={0};", row["ID"]));
                    BindGridData();
                }
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        private void btnRef_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BindGridData();
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}

