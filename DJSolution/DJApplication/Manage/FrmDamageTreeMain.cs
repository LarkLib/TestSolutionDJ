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
    public partial class FrmDamageTreeMain : Form
    {
        public FrmDamageTreeMain()
        {
            InitializeComponent();
            GridControlUtil.SetGridViewColumns(gridList, "v_DamageTreeMain");
            gridList.CustomDrawRowIndicator += gridList_CustomDrawRowIndicator;
            gridList.RowCountChanged += gridList_RowCountChanged;
        }

        private void FrmDamageTreeMain_Load(object sender, EventArgs e)
        {
            this.LoadDamageTree();
        }

        private void gridList_RowCountChanged(object sender, EventArgs e)
        {
            gridList.IndicatorWidth = GridControlUtil.GetRowIndicatorWidth(gridList.RowCount);
        }

        private void gridList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void LoadDamageTree()
        {
            try
            {
                gridControl1.DataSource = CommonFunc.GetDamageTreeList();
                gridList.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
                gridControl1.DataSource = null;
            }
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (FrmDamageTreeEdit we = new FrmDamageTreeEdit((gridControl1.DataSource as DataTable).NewRow(), EditMode.Add))
            {
                we.ShowDialog();
                if (we.SubmitChanged)
                    this.LoadDamageTree();
            }
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow row = gridList.GetDataRow(gridList.FocusedRowHandle);
            if (row == null)
            {
                MessageUtil.ShowError("请在列表中选择要编辑的内容.");
                return;
            }
            using (FrmDamageTreeEdit we = new FrmDamageTreeEdit(row, EditMode.Edit))
            {
                we.ShowDialog();
                if (we.SubmitChanged)
                    this.LoadDamageTree();
            }
        }

        private void btnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnRef_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadDamageTree();
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}
