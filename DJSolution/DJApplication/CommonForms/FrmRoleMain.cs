using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DJ.LMS.Utilities;

namespace DJ.LMS.WinForms
{
    public partial class FrmRoleMain : Form
    {
        public FrmRoleMain()
        {
            InitializeComponent();
            GridControlUtil.SetGridViewColumns(gridList, "RoleMain");
            gridList.CustomDrawRowIndicator += gridList_CustomDrawRowIndicator;
            gridList.RowCountChanged += gridList_RowCountChanged;
            gridList.RowCellClick += gridList_RowCellClick;
        }

        private void FrmRoleMain_Load(object sender, EventArgs e)
        {
            this.LoadRole();
        }

        private void gridList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            this.LoadRelationMenu();
        }

        void gridList_RowCountChanged(object sender, EventArgs e)
        {
            gridList.IndicatorWidth = GridControlUtil.GetRowIndicatorWidth(gridList.RowCount);
        }

        void gridList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void LoadRole()
        {
            try
            {
                this.gridControl1.DataSource = CommonFunc.GetRoleList();
                this.gridList.BestFitColumns();
                this.LoadRelationMenu();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
                gridControl1.DataSource = null;
            }
        }

        private void LoadRelationMenu()
        {
            try
            {
                this.lbcRelationMenu.Items.Clear();
                DataRow row = gridList.GetDataRow(gridList.FocusedRowHandle);
                if (row == null)
                {
                    return;
                }
                DataTable table = CommonFunc.GetEnableMenuByRole(row["ID"]);
                foreach (DataRow dr in table.Rows)
                {
                    this.lbcRelationMenu.Items.Add(dr["MenuName"]);
                }
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
                lbcRelationMenu.DataSource = null;
            }
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (FrmRoleEdit ue = new FrmRoleEdit((gridControl1.DataSource as DataTable).NewRow(), EditMode.Add))
            {
                ue.ShowDialog();
                if (ue.SubmitChanged)
                    this.LoadRole();
            }
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow row = gridList.GetDataRow(gridList.FocusedRowHandle);
            if (row == null)
            {
                MessageUtil.ShowError("请在列表中选择要编辑的角色.");
                return;
            }
            using (var ue = new FrmRoleEdit(row, EditMode.Edit))
            {
                ue.ShowDialog();
                if (ue.SubmitChanged)
                    this.LoadRole();
            }
        }

        private void btnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow row = gridList.GetDataRow(gridList.FocusedRowHandle);
            if (row == null)
            {
                MessageUtil.ShowError("请在列表中选择要删除的记录.");
                return;
            }
            if (Convert.ToInt64(row["ID"]).Equals(0))
            {
                MessageUtil.ShowError("系统不允许删除超级管理员.");
                return;
            }
            if (MessageUtil.ShowYesNoAndWarning("请确认是否删除选定的记录？") == DialogResult.No)
                return;
            try
            {
                string message = "";
                if (CommonFunc.DeleteRole(row["ID"], ref message))
                {
                    LoadRole();
                }
                else
                {
                    MessageUtil.ShowError(message);
                }
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        private void btnRef_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadRole();
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}
