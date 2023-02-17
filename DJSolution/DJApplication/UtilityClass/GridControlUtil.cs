using System;
using System.Collections.Generic;
using System.Xml;
using System.Data;
using System.IO;
using System.Reflection;
using DJ.LMS.Utilities;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraExport;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace DJ.LMS.WinForms
{
    internal static class GridControlUtil
    {
        #region ---GridView控件样式设置
        private static DataTable GetBindingFields(string tableName)
        {
            try
            {
                XmlParserContext context = new XmlParserContext(null, null, null, XmlSpace.None);
                XmlTextReader reader = new XmlTextReader(tableName, XmlNodeType.Document, context);
                DataSet ds = new DataSet();
                ds.ReadXml(reader);
                return (ds != null && ds.Tables.Count > 0) ? ds.Tables[1] : null;
            }
            catch { return null; }
        }

        private static void SetColumnProperties(DataRow row, GridColumn gridCol)
        {
            gridCol.FieldName = row["fieldName"].ToString();
            gridCol.Name = row["fieldName"].ToString();
            gridCol.Width = Convert.ToInt32(row["width"]);
            gridCol.Caption = string.IsNullOrEmpty(row["caption"].ToString().Trim()) ? row["fieldName"].ToString() : row["caption"].ToString();
            //gridCol.OptionsColumn.ReadOnly = row["editable"].ToString().ToLower().Equals("true") ? false : true;   
            gridCol.OptionsColumn.AllowEdit = row["editable"].ToString().ToLower().Equals("true") ? true : false;
            int index = 0;
            int.TryParse(row["index"].ToString(), out index);
            gridCol.VisibleIndex = index;
        }

        public static void SetGridViewColumns(GridView gridView, string tableName)
        {
            if (string.IsNullOrEmpty(tableName.Trim()))
                return;
            string fn = string.Format("{0}/{1}", AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "fieldConfig.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fn);
            XmlNode node = xmlDoc.SelectSingleNode(string.Format(@"configuration/Table[@name='{0}']", tableName));
            if (node == null) return;
            DataTable table = GetBindingFields(node.OuterXml);
            if (table == null)
                return;
            string controlType = string.Empty;
            //gridView.IndicatorWidth = 65;
            //gridView.OptionsBehavior.EditorShowMode = EditorShowMode.Click;
            gridView.Appearance.OddRow.BackColor = Color.White;
            gridView.OptionsView.EnableAppearanceOddRow = true;
            gridView.Appearance.EvenRow.BackColor = Color.WhiteSmoke;
            gridView.OptionsView.EnableAppearanceEvenRow = true;
            gridView.Appearance.FocusedRow.BackColor = Color.Aqua;
            foreach (DataRow row in table.Rows)
            {
                if (string.IsNullOrEmpty(row["caption"].ToString().Trim()))
                    continue;
                controlType = row["control"].ToString().ToLower();
                switch (controlType)
                {
                    case "lookupedit":
                        {
                            GridColumn lueCol = new GridColumn();
                            SetColumnProperties(row, lueCol);
                            RepositoryItemLookUpEdit rlue = new RepositoryItemLookUpEdit();
                            rlue.Name = string.Format("rlue_{0}", row["fieldName"]);
                            rlue.ShowHeader = false;
                            //rlue.ShowFooter = false;
                            rlue.AutoSearchColumnIndex = 2;
                            rlue.ImmediatePopup = true;
                            rlue.NullText = "";
                            rlue.SearchMode = SearchMode.OnlyInPopup;
                            rlue.DisplayMember = "NAME";
                            rlue.ValueMember = "VALUE";
                            if (row["comboSql"].ToString().Trim().ToLower().Contains("select"))
                                rlue.DataSource = SqlDbHelper.GetDataSet(row["comboSql"].ToString()).Tables[0];
                            else
                                rlue.DataSource = CommonFunc.GetState(row["comboSql"].ToString());
                            rlue.PopulateColumns();
                            rlue.Columns["VALUE"].Visible = false;
                            lueCol.ColumnEdit = rlue;
                            if (!gridView.Columns.Contains(lueCol))
                                gridView.Columns.Add(lueCol);
                            break;
                        }
                    case "checkedit":
                        {
                            GridColumn checkCol = new GridColumn();
                            SetColumnProperties(row, checkCol);
                            RepositoryItemCheckEdit rCheck = new RepositoryItemCheckEdit();
                            rCheck.AutoHeight = false;
                            rCheck.Name = string.Format("rCheck_{0}", row["fieldName"]);
                            checkCol.ColumnEdit = rCheck;
                            checkCol.OptionsColumn.AllowGroup = DefaultBoolean.False;
                            checkCol.OptionsColumn.AllowSize = false;
                            checkCol.OptionsColumn.AllowSort = DefaultBoolean.False;
                            if (!gridView.Columns.Contains(checkCol))
                                gridView.Columns.Add(checkCol);
                            break;
                        }
                    case "comboxedit":
                        {
                            break;
                        }
                    case "dateedit":
                        {
                            GridColumn dateCol = new GridColumn();
                            SetColumnProperties(row, dateCol);
                            RepositoryItemDateEdit riDate = new RepositoryItemDateEdit();
                            riDate.Name = string.Format("riDate_{0}", row["fieldName"]);
                            riDate.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
                            riDate.DisplayFormat.FormatType = FormatType.DateTime;
                            riDate.Mask.EditMask = "G";
                            dateCol.ColumnEdit = riDate;
                            if (!gridView.Columns.Contains(dateCol))
                                gridView.Columns.Add(dateCol);
                            break;
                        }
                    case "image":
                        {
                            GridColumn imgCol = new GridColumn();
                            SetColumnProperties(row, imgCol);
                            RepositoryItemPictureEdit riImg = new RepositoryItemPictureEdit();
                            riImg.Name = string.Format("riImg_{0}", row["fieldName"]);
                            imgCol.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                            imgCol.ColumnEdit = riImg;
                            if (!gridView.Columns.Contains(imgCol))
                                gridView.Columns.Add(imgCol);
                            break;
                        }
                    case "spinedit":
                        {
                            GridColumn spinEditCol = new GridColumn();
                            SetColumnProperties(row, spinEditCol);
                            RepositoryItemSpinEdit spinEdit = new RepositoryItemSpinEdit();
                            spinEdit.AutoHeight = false;
                            var valueString = row["comboSql"];
                            if (valueString != null && !string.IsNullOrWhiteSpace(valueString.ToString()))
                            {
                                var values = valueString.ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (values.Length == 2)
                                {
                                    var max = default(decimal);
                                    var min = default(decimal);
                                    if (decimal.TryParse(values[0], out max) && max != default(decimal))
                                    {
                                        spinEdit.MaxValue = max;
                                    }
                                    if (decimal.TryParse(values[1], out min) && min != default(decimal))
                                    {
                                        spinEdit.MaxValue = min;
                                    }
                                }
                            }
                            spinEdit.Name = string.Format("spinEdit_{0}", row["fieldName"]);
                            spinEditCol.ColumnEdit = spinEdit;
                            spinEditCol.OptionsColumn.AllowGroup = DefaultBoolean.False;
                            spinEditCol.OptionsColumn.AllowSize = false;
                            spinEditCol.OptionsColumn.AllowSort = DefaultBoolean.False;
                            if (!gridView.Columns.Contains(spinEditCol))
                                gridView.Columns.Add(spinEditCol);
                            break;
                        }
                    default:
                        {
                            GridColumn textCol = new GridColumn();
                            SetColumnProperties(row, textCol);
                            if (!gridView.Columns.Contains(textCol))
                                gridView.Columns.Add(textCol);
                            break;
                        }
                }
            }
        }
        public static void SaveColumnsSettings(GridView gridView, string tableName)
        {
            string fn = string.Format("{0}/{1}", AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "fieldConfig.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fn);
            XmlNode node;
            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                node = xmlDoc.SelectSingleNode(string.Format(@"configuration/Table[@name='{0}']/Field[@fieldName='{1}']", tableName, gridView.Columns[i].FieldName));
                node.Attributes["width"].Value = gridView.Columns[i].Width.ToString();
            }
            xmlDoc.Save(fn);
        }

        public static void SaveLayoutStyleToXml(GridView gridView, string xmlName)
        {
            try
            {
                gridView.SaveLayoutToXml(xmlName);
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }
        public static void RestoreLayoutStyleFromXml(GridView gridView, string xmlName)
        {
            try
            {
                gridView.RestoreLayoutFromXml(xmlName);
            }
            catch
            {
            }
        }
        #endregion

        #region ---GridView控件数据导出
        /// <summary>
        /// GridView数据导出至Excel
        /// </summary>
        /// <param name="grid">要导出的GridControl控件</param>
        /// <param name="caption">文件标题</param>
        public static void GridViewExportToExcel(GridControl grid, string caption)
        {
            if (grid.MainView.DataRowCount.Equals(0))
                return;
            string filePath = FileDialogHelper.SaveExcel(caption);
            if (filePath != string.Empty)
            {
                XlsExportOptions options = new XlsExportOptions(TextExportMode.Value);
                grid.ExportToXls(filePath, options);
            }
        }
        /// <summary>
        /// GridView数据导出至Xml
        /// </summary>
        /// <param name="grid">要导出的GridControl控件</param>
        /// <param name="caption">文件标题</param>
        public static void GridViewExportToXml(GridControl grid, string caption)
        {
            if (grid.MainView.DataRowCount.Equals(0))
                return;
            //string filePath = FileDialogHelper.SaveXml(caption);
            //if (filePath != string.Empty)
            //{
            //    IExportProvider provider = new ExportXmlProvider(filePath);
            //    BaseExportLink link = grid.MainView.CreateExportLink(provider);
            //    (link as GridViewExportLink).ExpandAll = false;
            //    link.ExportTo(true);
            //}
        }
        #endregion

        #region ---GridView常用操作
        /// <summary>
        /// 根据列选择GridView一行
        /// </summary>
        /// <param name="gridView">GridView控件</param>
        /// <param name="colName">列名</param>
        /// <param name="colValue">列值</param>
        public static void SelectGridViewRow(GridView gridView, string colName, object colValue)
        {
            gridView.ClearSelection();
            for (int rowHandle = 0; rowHandle < gridView.RowCount; rowHandle++)
            {
                object _cellValue = gridView.GetRowCellValue(rowHandle, colName);
                if (_cellValue != null)
                {
                    if (_cellValue.Equals(colValue))
                    {
                        gridView.SelectRow(rowHandle);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 根据rowIndex和visibleColumnsIndex来获取单元格可见值
        /// </summary>
        /// <param name="gridView">GridView</param>
        /// <param name="rowIndex">rowIndex</param>
        /// <param name="visibleColumnsIndex">visibleColumnsIndex</param>
        /// <returns>单元格可见值</returns>
        public static string GetRowCellDisplayText(GridView gridView, int rowIndex, int visibleColumnsIndex)
        {
            return gridView.GetRowCellDisplayText(rowIndex, gridView.VisibleColumns[visibleColumnsIndex]);
        }
        /// <summary>
        /// 根据rowIndex和visibleColumnsIndex来获取单元格值
        /// </summary>
        /// <param name="gridView">GridView</param>
        /// <param name="rowIndex">rowIndex</param>
        /// <param name="visibleColumnsIndex">visibleColumnsIndex</param>
        /// <returns>单元格值</returns>
        public static object GetRowCellValue(GridView gridView, int rowIndex, int visibleColumnsIndex)
        {
            return gridView.GetRowCellValue(rowIndex, gridView.VisibleColumns[visibleColumnsIndex]);
        }
        /// <summary>
        /// 删除全部行
        /// </summary>
        /// <param name="gridView">GridView</param>
        public static void ClearRows(this GridView gridView)
        {
            bool _mutilSelected = gridView.OptionsSelection.MultiSelect;//获取当前是否可以多选
            if (!_mutilSelected)
                gridView.OptionsSelection.MultiSelect = true;
            gridView.SelectAll();
            gridView.DeleteSelectedRows();
            gridView.OptionsSelection.MultiSelect = _mutilSelected;//还原之前是否可以多选状态
        }
        /// <summary>
        /// 为列头绘制CheckBox
        /// </summary>
        /// <param name="gridView">GridView</param>
        /// <param name="checkItem">RepositoryItemCheckEdit</param>
        /// <param name="fieldName">需要绘制Checkbox的列名</param>
        /// <param name="e">ColumnHeaderCustomDrawEventArgs</param>
        public static void DrawHeaderCheckBox(GridView gridView, RepositoryItemCheckEdit checkItem, string fieldName, ColumnHeaderCustomDrawEventArgs e)
        {
            /*说明：
             *参考：https://www.devexpress.com/Support/Center/Question/Details/Q354489
             *在CustomDrawColumnHeader中使用
             *eg：
             * private void gvCabChDetail_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
             * {
             * GridView _view = sender as GridView;
             * _view.DrawHeaderCheckBox(CheckItem, "Check", e);
             * }
             */
            if (e.Column != null && e.Column.FieldName.Equals(fieldName))
            {
                e.Info.InnerElements.Clear();
                e.Painter.DrawObject(e.Info);
                DrawCheckBox(checkItem, e.Graphics, e.Bounds, getCheckedCount(gridView, fieldName) == gridView.DataRowCount);
                e.Handled = true;
            }
        }
        private static void DrawCheckBox(RepositoryItemCheckEdit checkItem, Graphics g, Rectangle r, bool Checked)
        {
            DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo _info;
            DevExpress.XtraEditors.Drawing.CheckEditPainter _painter;
            DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs _args;
            _info = checkItem.CreateViewInfo() as DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo;
            _painter = checkItem.CreatePainter() as DevExpress.XtraEditors.Drawing.CheckEditPainter;
            _info.EditValue = Checked;

            _info.Bounds = r;
            _info.PaintAppearance.ForeColor = Color.Black;
            _info.CalcViewInfo(g);
            _args = new DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs(_info, new DevExpress.Utils.Drawing.GraphicsCache(g), r);
            _painter.Draw(_args);
            _args.Cache.Dispose();
        }
        private static int getCheckedCount(GridView view, string filedName)
        {
            int count = 0;
            for (int i = 0; i < view.DataRowCount; i++)
            {
                object _cellValue = view.GetRowCellValue(i, view.Columns[filedName]);
                if (_cellValue == null) continue;
                if (string.IsNullOrEmpty(_cellValue.ToString().Trim())) continue;
                bool _checkStatus = false;
                if (bool.TryParse(_cellValue.ToString(), out _checkStatus))
                {
                    if (_checkStatus)
                        count++;
                }
            }
            return count;
        }
        /// <summary>
        /// 自定义单元格验证
        /// </summary>
        /// <param name="view">GridView</param>
        /// <param name="e">BaseContainerValidateEditorEventArgs</param>
        /// <param name="fieldNameHandler">委托</param>
        /// <param name="errorHanlder">委托</param>
        /// <param name="errorText">当验证不通过对时候，错误提示信息文字</param>
        public static void CustomValidatingEditor(this GridView view, BaseContainerValidateEditorEventArgs e, Predicate<string> fieldNameHandler, Predicate<object> errorHanlder, string errorText)
        {
            /*说明
             *在ValidatingEditor事件使用
             *eg:
             *string[] workType = new string[4] { "-1", "关闭但不删除", "启用", "删除" };
             *void gvLampConfig_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
             *{
             * GridView _view = sender as GridView;
             * _view.CustomValidatingEditor(e, fieldName => fieldName.Equals("TLampWorkStatus"), value => !workType.Contains<string>(value.ToString()), "若想设置为不修改，请输入-1即可");
             * }
             */
            if (fieldNameHandler(view.FocusedColumn.FieldName))
            {
                if (errorHanlder(e.Value))
                {
                    e.Valid = false;
                    e.ErrorText = errorText;
                }
            }
        }
        /// <summary>
        /// 设置当没有数据行的提示信息『CustomDrawEmptyForeground』
        /// </summary>
        /// <param name="gridView">GridView</param>
        /// <param name="e">CustomDrawEventArgs</param>
        /// <param name="noRecordMsg">提示信息</param>
        /// 调用示例：
        /// private void gvLampTotal_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        /// {
        ///     gvLampTotal.DrawNoRowCountMessage(e, "暂无符合的数据！");
        /// }
        ///
        public static void DrawNoRowCountMessage(this GridView gridView, CustomDrawEventArgs e, string noRecordMsg)
        {
            if (gridView == null)
                throw new ArgumentNullException("gridView");
            if (gridView.RowCount == 0)
            {
                if (!string.IsNullOrEmpty(noRecordMsg))
                {
                    Font _font = new Font("宋体", 10, FontStyle.Bold);
                    Rectangle _r = new Rectangle(e.Bounds.Left + 5, e.Bounds.Top + 5, e.Bounds.Width - 5, e.Bounds.Height - 5);
                    e.Graphics.DrawString(noRecordMsg, _font, Brushes.Black, _r);
                }
            }
        }

        public static int GetRowIndicatorWidth(int rowCount)
        {
            if (rowCount < 1000)
                return 35;
            else if (rowCount < 10000)
                return 50;
            else if (rowCount < 100000)
                return 60;
            else if (rowCount < 1000000)
                return 70;
            else if (rowCount < 10000000)
                return 85;
            else
                return 100;
        }
        #endregion

        #region ---GridView展开明细
        /// <summary>
        /// 展开或关闭GridView主表明细信息
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="expanded">true:展开 false:关闭</param>
        public static void GridViewExpandedDetail(GridView gridView, bool expanded)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                gridView.SetMasterRowExpandedEx(i, -1, expanded);
            }
        }
        #endregion

        #region ---gridView打印
        public static void PrintGridView(GridControl gridControl)
        {
            DevExpress.XtraPrintingLinks.CompositeLink compositeLink = new DevExpress.XtraPrintingLinks.CompositeLink();
            DevExpress.XtraPrinting.PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();
            compositeLink.PrintingSystem = ps;
            compositeLink.Landscape = true;
            compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
            DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(ps);
            ps.PageSettings.Landscape = true;
            link.Component = gridControl;
            compositeLink.Links.Add(link);
            link.CreateDocument();  //建立文档
            ps.PreviewFormEx.Show();//进行预览   
        }
        #endregion
    }
}
