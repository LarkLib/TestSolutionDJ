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
    public partial class FrmEventLogQuery : Form
    {
        public FrmEventLogQuery()
        {
            InitializeComponent();
            GridControlUtil.SetGridViewColumns(grid, "EventLog");
            this.Activated += FrmControlQuery_Activated;
            grid.CustomDrawRowIndicator += grid_CustomDrawRowIndicator;
            grid.RowCountChanged += grid_RowCountChanged;
            grid.MouseDown += grid_MouseDown;
        }

        void grid_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = grid.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                biComplete.Enabled = hInfo.InRow ? true : false;
                biDel.Enabled = hInfo.InRow ? true : false;
                biCancel.Enabled = hInfo.InRow ? true : false;
                this.popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        void grid_RowCountChanged(object sender, EventArgs e)
        {
            grid.IndicatorWidth = GridControlUtil.GetRowIndicatorWidth(grid.RowCount);
        }

        void grid_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        void FrmControlQuery_Activated(object sender, EventArgs e)
        {
            LoadEventLog();
        }

        private void FrmControlQuery_Load(object sender, EventArgs e)
        {
            LoadEventLog();
        }

        private void LoadEventLog(int type = 0)
        {
            try
            {
                gridControl1.DataSource = SqlDbHelper.GetDataSet("SELECT * FROM EventLog where ExpireDate>getdate() order by CreateTime desc").Tables[0];
                grid.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
                gridControl1.DataSource = null;
            }
        }

        private void btnComplete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow row = grid.GetDataRow(grid.FocusedRowHandle);
            if (row == null)
            {
                MessageUtil.ShowError("请在列表中选择要执行的任务记录.");
                return;
            }
            if (MessageUtil.ShowYesNoAndTips("请确认是否执行命令？") == System.Windows.Forms.DialogResult.No)
                return;
            try
            {
                int id = Convert.ToInt32(row["ID"]);
                string message = string.Empty;
                SqlDbHelper.ExecuteNonQuery(string.Format("Update EventLog set status='处理完毕' where ID={0}", id));
                LoadEventLog();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        private void btnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow row = grid.GetDataRow(grid.FocusedRowHandle);
            if (row == null)
            {
                MessageUtil.ShowError("请在列表中选择要执行的任务记录.");
                return;
            }
            if (MessageUtil.ShowYesNoAndTips("请确认是否执行命令？") == System.Windows.Forms.DialogResult.No)
                return;
            try
            {
                int id = Convert.ToInt32(row["ID"]);
                SqlDbHelper.ExecuteNonQuery(string.Format("Delete EventLog where ID={0}", id));
                LoadEventLog();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadEventLog(500);
        }

        private void btnRef_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadEventLog();
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnException_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //using (FrmCellException form = new FrmCellException())
            //{
            //    form.ShowDialog();
            //}
        }
    }
}
