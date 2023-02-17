using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DJ.LMS.Utilities;

namespace DJ.LMS.WinForms
{
    public partial class FrmP300Main : Form
    {
        public FrmP300Main()
        {
            InitializeComponent();
        }

        private void FrmManageQuery_Load(object sender, EventArgs e)
        {
            //splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
            LoadPLANT();
            LoadLimsStatus();
            this.LoadPlanListFactoryLotNoSearch();
            this.LoadPlanListrWorkLotNoSearch();
            LoadItemCode();
            LoadPrinter();
        }

        private void LoadPLANT()
        {
            try
            {
                var table = CommonFunc.GetState("LIMS_REPORT_DEPARTMENT");
                var row = table.NewRow();
                row["NAME"] = "<请选择>";
                row["VALUE"] = string.Empty;
                table.Rows.InsertAt(row, 0);
                table.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }
        private void LoadLimsStatus()
        {
            try
            {
                var table = CommonFunc.GetState("LIMS_REPORT_STATUS");
                var row = table.NewRow();
                row["NAME"] = "<请选择>";
                row["VALUE"] = string.Empty;
                table.Rows.InsertAt(row, 0);
                table.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }
        private void LoadPlanListFactoryLotNoSearch()
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }
        private void LoadPlanListrWorkLotNoSearch()
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }
        private void LoadItemCode()
        {
            //try
            //{
            //    var sql = "select distinct MATCODE ItemCode, PROGNAME ItemName from v_LimsReport";
            //    var table = SqlDbHelper.GetDataSet(sql).Tables[0];
            //}
            //catch (Exception ex)
            //{
            //    MessageUtil.ShowError(ex.Message);
            //}
        }
        private void LoadLimsReport()
        {
            //try
            //{
            //    this.splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
            //    var whereBuilder = new StringBuilder();
            //    whereBuilder.Append("where 1=1 ");
            //    if (!string.IsNullOrWhiteSpace(cmbPLANT.EditValue?.ToString()))
            //    {
            //        whereBuilder.AppendFormat("and PLANT= '{0}'", cmbPLANT.Text);
            //    }
            //    if (!string.IsNullOrWhiteSpace(cmbLimsStatus.EditValue?.ToString()))
            //    {
            //        whereBuilder.AppendFormat("and Status='{0}'", cmbLimsStatus.EditValue);
            //    }
            //    if (!string.IsNullOrWhiteSpace(cmbFactoryLotNo.EditValue?.ToString()))
            //    {
            //        whereBuilder.AppendFormat("and CATARRIVENO = '{0}'", cmbFactoryLotNo.EditValue);
            //    }
            //    if (dateBegin.EditValue != null)
            //    {
            //        whereBuilder.AppendFormat("and cast(CreateTime as datetime) >= '{0}'", dateBegin.DateTime.ToString("yyyy-MM-dd"));
            //    }
            //    if (dateEnd.EditValue != null)
            //    {
            //        whereBuilder.AppendFormat("and cast(CreateTime as datetime) <= '{0}'", dateEnd.DateTime.AddDays(1).ToString("yyyy-MM-dd"));
            //    }
            //    gridControl1.DataSource = CommonFunc.DataTableAddCheckColumn(CommonFunc.GetLimsReportQuery(whereBuilder.ToString()));
            //    gridManage.BestFitColumns();
            //}
            //catch (Exception ex)
            //{
            //    MessageUtil.ShowError(ex.Message);
            //}
        }
        private void LoadPlanListDetailSearch()
        {
            //try
            //{
            //    searchGridList.Columns.Clear();
            //    cmbLocalPlanCode.EditValue = txtFWId.EditValue = null;
            //    GridControlUtil.SetGridViewColumns(searchGridList, "v_PlanListDetailSearch");
            //    cmbLocalPlanCode.Properties.DataSource = CommonFunc.GetPlanListDetailForLimsSearch();
            //    searchGridList.BestFitColumns();
            //}
            //catch (Exception ex)
            //{
            //    MessageUtil.ShowError(ex.Message);
            //    cmbLocalPlanCode.Properties.DataSource = null;
            //}
        }
        private void LoadPrinter()
        {
            try
            {
                cmbPrinter.Properties.Items.Clear();
                cmbPrinter.Properties.Items.AddRange(PrinterSettings.InstalledPrinters);
                cmbPrinter.Properties.Items.Insert(0, "默认打印机");
                cmbPrinter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        private void EditLimsReport()
        {
            //string requestId = row == null || row["REQUESTID"] == null || string.IsNullOrWhiteSpace(row["REQUESTID"].ToString()) ? null : row["REQUESTID"].ToString();
            //if (string.IsNullOrWhiteSpace(requestId))
            //{
            //    MessageUtil.ShowTips("请选择需要编辑的送捡报告!");
            //    return;
            //}
            //long id;
            //if (long.TryParse(row["ID"].ToString(), out id))
            //{
            //    var frm = new FrmLimsReport(id, requestId);
            //    frm.ShowDialog();
            //    LoadLimsReport();
            //}
        }

        void gridManage_MouseDown(object sender, MouseEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridManage.CalcHitInfo(new Point(e.X, e.Y));
            //if (hInfo.InRow && e.Button == MouseButtons.Left && e.Clicks == 2)
            //{
            //    EditLimsReport();
            //}
            //if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //{
            //    if (hInfo.InRow)
            //    {
            //        LoadManageDetail();
            //        biSendTo.Enabled = true;
            //        biComplete.Enabled = true;
            //        biCancel.Enabled = true;
            //    }
            //    else
            //    {
            //        biSendTo.Enabled = false;
            //        biComplete.Enabled = false;
            //        biCancel.Enabled = false;
            //    }
            //    this.popupMenu1.ShowPopup(Control.MousePosition);
            //}
        }

        void gridDetail_MouseDown(object sender, MouseEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridManage.CalcHitInfo(new Point(e.X, e.Y));
            //if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //{
            //    //if (hInfo.InRow)
            //    //{
            //    //    biComplete.Enabled = true;
            //    //    biCancel.Enabled = true;
            //    //}
            //    //else
            //    //{
            //    //    biComplete.Enabled = false;
            //    //    biCancel.Enabled = false;
            //    //}
            //    this.popupMenu1.ShowPopup(Control.MousePosition);
            //}
        }

        void gridDetail_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        void gridManage_RowCountChanged(object sender, EventArgs e)
        {
           // gridManage.IndicatorWidth = GridControlUtil.GetRowIndicatorWidth(gridManage.RowCount);
        }

        void gridManage_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void FrmManageQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnExecute_ItemClick(null, null);
            }
        }

        private void gridManage_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //LoadManageDetail();
        }

        private void btnExecute_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //DataRow row = gridManage.GetDataRow(gridManage.FocusedRowHandle);
            //if (row == null)
            //{
            //    MessageUtil.ShowError("请在任务列表中选择要操作的记录.");
            //    return;
            //}
            //if (MessageUtil.ShowYesNoAndTips("请确认是否执行命令？") == System.Windows.Forms.DialogResult.No)
            //    return;
            //try
            //{
            //    string message = string.Empty;
            //    //if (CommonFunc.WmsTaskManualProcess(row["WmsTaskID"], "SendTo", ref message))
            //    //    LoadWmsTask();
            //    //else
            //    //    MessageUtil.ShowError(message);
            //}
            //catch (Exception ex)
            //{
            //    MessageUtil.ShowError(ex.Message);
            //}
        }

        private void btnAllExecute_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (gridManage.RowCount == 0)
            //    return;
            //if (MessageUtil.ShowYesNoAndTips("请确认是否执行命令？") == System.Windows.Forms.DialogResult.No)
            //    return;
            //try
            //{
            //    string message = string.Empty;
            //    //if (CommonFunc.WmsTaskBatchSend(1, ref message))
            //    //    LoadWmsTask();
            //    //else
            //    //    MessageUtil.ShowError(message);
            //}
            //catch (Exception ex)
            //{
            //    MessageUtil.ShowError(ex.Message);
            //}
        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //DataRow row = gridManage.GetDataRow(gridManage.FocusedRowHandle);
            //if (row == null)
            //{
            //    MessageUtil.ShowError("请在任务列表中选择要操作的记录.");
            //    return;
            //}
            //if (MessageUtil.ShowYesNoAndTips("请确认是否执行命令？") == System.Windows.Forms.DialogResult.No)
            //    return;
            //try
            //{
            //    string message = string.Empty;
            //    //if (CommonFunc.WmsTaskManualProcess(row["WmsTaskID"], "Cancel", ref message))
            //    //    LoadWmsTask();
            //    //else
            //    //    MessageUtil.ShowError(message);
            //}
            //catch (Exception ex)
            //{
            //    MessageUtil.ShowError(ex.Message);
            //}
        }

        private void btnComplete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //DataRow row = gridManage.GetDataRow(gridManage.FocusedRowHandle);
            //if (row == null)
            //{
            //    MessageUtil.ShowError("请在任务列表中选择要操作的记录.");
            //    return;
            //}
            //if (MessageUtil.ShowYesNoAndTips("请确认是否执行命令？") == System.Windows.Forms.DialogResult.No)
            //    return;
            //try
            //{
            //    string message = string.Empty;
            //    //if (CommonFunc.WmsTaskManualProcess(row["WmsTaskID"], "Complete", ref message))
            //    //    LoadWmsTask();
            //    //else
            //    //    MessageUtil.ShowError(message);
            //}
            //catch (Exception ex)
            //{
            //    MessageUtil.ShowError(ex.Message);
            //}
        }

        private void btnRef_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadLimsReport();
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadLimsReport();
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //LoadPlanListDetailSearch();
            //splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
        }

        private void btnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (gridManage.RowCount == 0)
            //    return;
            //gridManage.CloseEditor();
            //gridManage.UpdateCurrentRow();
            //DataRow[] selectedRows = (gridControl1.DataSource as DataTable).Select("Check=true");
            //if (selectedRows.Length == 0)
            //{
            //    MessageUtil.ShowError("请在列表中选择需要删除的送检报告.");
            //    return;
            //}

            //if (MessageUtil.ShowYesNoAndTips("请确认是否删除送检报告.") == System.Windows.Forms.DialogResult.No)
            //    return;
            //var sqlBuilder = new StringBuilder();
            //foreach (DataRow row in selectedRows)
            //{
            //    string requestId = row == null || row["REQUESTID"] == null || string.IsNullOrWhiteSpace(row["REQUESTID"].ToString()) ? null : row["REQUESTID"].ToString();
            //    if (!string.IsNullOrWhiteSpace(requestId))
            //    {
            //        sqlBuilder.AppendFormat("delete LimsReport where REQUESTID='{0}';", requestId);
            //    }
            //}
            //SqlDbHelper.ExecuteNonQuery(sqlBuilder.ToString());
            //LoadLimsReport();
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditLimsReport();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //long id;
            //string idString = null;
            //if (!cmbLocalPlanCode.EditValue.IsNotNull() && !txtFWId.EditValue.IsNotNull())
            //{
            //    MessageUtil.ShowTips(string.Format("请选择送检单据!"));
            //    return;
            //}
            //if (!cmbLocalPlanCode.Text.Equals(txtFWId.EditValue))
            //{
            //    idString = SqlDbHelper.GetFieldValue("v_PlanList", "ID", string.Format("CONCAT(FactoryLotNo,WorkLotNo)='{0}'", txtFWId.EditValue)).ToString();
            //}
            //else
            //{
            //    idString = cmbLocalPlanCode.EditValue.IsNotNull() ? cmbLocalPlanCode.EditValue.ToString() : null;
            //}
            //if (!long.TryParse(idString, out id))
            //{
            //    MessageUtil.ShowTips(string.Format("无效送检单据<{0}>", txtFWId.EditValue));
            //    return;
            //}
            //var frm = new FrmLimsReport(id, null);
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    LoadLimsReport();
            //}
            //splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
        }

        private void cmbLocalPlanCode_EditValueChanged(object sender, EventArgs e)
        {
            //txtFWId.Text = cmbLocalPlanCode.Text;
        }

        private void btnPrintReview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (gridManage.RowCount == 0)
            //    return;
            //var row = gridManage.GetDataRow(gridManage.FocusedRowHandle);
            //if (row == null)
            //{
            //    MessageUtil.ShowTips("请选择预览的送检报告.");
            //    return;
            //}
            //var report = new XtraReportLims(row["REQUESTID"].ToString());
            //DevExpress.XtraReports.UI.ReportPrintTool printTool = new DevExpress.XtraReports.UI.ReportPrintTool(report);
            //printTool.PreviewForm.PrintBarManager.HideToolBarsPopup();
            //printTool.PreviewForm.PrintBarManager.MainMenu.Visible = false;
            //printTool.PreviewForm.PrintBarManager.AllowCustomization = false;

            //printTool.PreviewRibbonForm.RibbonControl.Visible = false;
            //var bars = printTool.PreviewForm.PrintBarManager.Bars;
            //bars["Toolbar"].Visible = false;
            //printTool.PreviewForm.Width = 600;
            //printTool.ShowPreview();
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (gridManage.RowCount == 0)
            //    return;
            //gridManage.CloseEditor();
            //gridManage.UpdateCurrentRow();
            //DataRow[] selectedRows = (gridControl1.DataSource as DataTable).Select("Check=true");
            //if (selectedRows.Length == 0)
            //{
            //    MessageUtil.ShowError("请在列表中选择需要打印的送检报告.");
            //    return;
            //}

            //if (MessageUtil.ShowYesNoAndTips("请确认是否打印送检报告.") == System.Windows.Forms.DialogResult.No)
            //    return;
            //var sqlBuilder = new StringBuilder();
            //foreach (DataRow row in selectedRows)
            //{
            //    string requestId = row == null || row["REQUESTID"] == null || string.IsNullOrWhiteSpace(row["REQUESTID"].ToString()) ? null : row["REQUESTID"].ToString();
            //    if (!string.IsNullOrWhiteSpace(requestId))
            //    {
            //        var report = new XtraReportLims(requestId);
            //        report.ShowPrintStatusDialog = false;
            //        DevExpress.XtraReports.UI.ReportPrintTool printTool = new DevExpress.XtraReports.UI.ReportPrintTool(report);
            //        printTool.PrintingSystem.ShowPrintStatusDialog = false;
            //        printTool.PrintingSystem.ShowMarginsWarning = false;
            //        printTool.AutoShowParametersPanel = false;
            //        if (cmbPrinter.SelectedIndex > 0)
            //        {
            //            printTool.PrinterSettings.PrinterName = cmbPrinter.SelectedItem.ToString();
            //        }
            //        printTool.Print();
            //    }
            //}
        }

        private void btnReturnQuery_Click(object sender, EventArgs e)
        {
            //splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
        }
    }
}
