using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DJ.LMS.Utilities;

namespace DJ.LMS.WinForms
{
    public partial class FrmUserMain : Form
    {
        public FrmUserMain()
        {
            InitializeComponent();
            GridControlUtil.SetGridViewColumns(gridList, "UserMain");
            gridList.CustomDrawRowIndicator += gridList_CustomDrawRowIndicator;
            gridList.RowCountChanged += gridList_RowCountChanged;
            gridList.RowCellClick += gridList_RowCellClick;
        }

        private void FrmUserMain_Load(object sender, EventArgs e)
        {
            this.LoadUser();
        }

        private void gridList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

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

        private void LoadUser()
        {
            try
            {
                this.gridControl1.DataSource = CommonFunc.GetUserList();
                this.gridList.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
                gridControl1.DataSource = null;
            }
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (FrmUserEdit ue = new FrmUserEdit((gridControl1.DataSource as DataTable).NewRow(), EditMode.Add))
            {
                ue.ShowDialog();
                if (ue.SubmitChanged)
                    this.LoadUser();
            }
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow row = gridList.GetDataRow(gridList.FocusedRowHandle);
            if (row == null)
            {
                MessageUtil.ShowError("请在列表中选择要编辑的用户.");
                return;
            }
            using (var ue = new FrmUserEdit(row, EditMode.Edit))
            {
                ue.ShowDialog();
                if (ue.SubmitChanged)
                    this.LoadUser();
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
                MessageUtil.ShowError("系统不允许删除超级用户.");
                return;
            }
            if (MessageUtil.ShowYesNoAndWarning("请确认是否删除选定的记录？") == DialogResult.No)
                return;
            try
            {
                string message = "";
                if (CommonFunc.DeleteUser(row["ID"], ref message))
                {
                    LoadUser();
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
            this.LoadUser();
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}
