using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DJ.LMS.Utilities;

namespace DJ.LMS.WinForms
{
    public partial class FrmDamageTreeItemMain : Form
    {
        public FrmDamageTreeItemMain()
        {
            InitializeComponent();
            //GridControlUtil.SetGridViewColumns(gridList, "v_DamageTreeMain");
            //gridList.CustomDrawRowIndicator += gridList_CustomDrawRowIndicator;
            //gridList.RowCountChanged += gridList_RowCountChanged;
        }

        private void FrmDamageTreeItemMain_Load(object sender, EventArgs e)
        {
            this.LoadDamageTreeList();
        }

        private void gridList_RowCountChanged(object sender, EventArgs e)
        {
            //gridList.IndicatorWidth = GridControlUtil.GetRowIndicatorWidth(gridList.RowCount);
        }

        private void gridList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void LoadDamageTreeList()
        {
            try
            {
                damageTreeList.Properties.DataSource = CommonFunc.GetDamageTreeList();
                //damageTreeList.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
                damageTreeList.Properties.DataSource = null;
            }
        }

        private void LoadDamageTreeItemList()
        {
            try
            {
                if (!damageTreeList.EditValue.IsNotNull())
                {
                    damageTree.DataSource = null;
                    return;
                }

                damageTree.OptionsBehavior.Editable = false;
                #region 设置列头、节点指示器面板、表格线样式
                damageTree.OptionsView.ShowColumns = false;             //隐藏列标头
                damageTree.OptionsView.ShowIndicator = false;           //隐藏节点指示器面板
                damageTree.OptionsView.ShowHorzLines = false;           //隐藏水平表格线
                damageTree.OptionsView.ShowVertLines = false;           //隐藏垂直表格线
                damageTree.OptionsView.ShowIndentAsRowStyle = false;
                #endregion

                #region 初始禁用单元格选中，禁用整行选中

                damageTree.OptionsView.ShowFocusedFrame = true;                               //设置显示焦点框
                damageTree.OptionsSelection.EnableAppearanceFocusedCell = false;              //禁用单元格选中
                damageTree.OptionsSelection.EnableAppearanceFocusedRow = false;               //禁用正行选中

                #endregion

                #region 设置TreeList的展开折叠按钮样式和树线样式

                damageTree.OptionsView.ShowButtons = true;                  //显示展开折叠按钮
                damageTree.LookAndFeel.UseDefaultLookAndFeel = false;       //禁用默认外观与感觉
                damageTree.LookAndFeel.UseWindowsXPTheme = true;            //使用WindowsXP主题
                damageTree.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.Percent50;     //设置树线的样式
                damageTree.OptionsSelection.InvertSelection = true;

                #endregion

                damageTree.TreeViewFieldName = "Name";
                damageTree.KeyFieldName = "ID";
                damageTree.ParentFieldName = "PID";
                damageTree.DataSource = CommonFunc.GetDamageTreeItemList((long)damageTreeList.EditValue);
                damageTree.ExpandAll();
                damageTree.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
                damageTree.DataSource = null;
            }
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!damageTreeList.EditValue.IsNotNull()) return;
            if (damageTree.FocusedNode?.Level == 0)
            {
                MessageUtil.ShowError("已经存在根节点.");
                return;
            }
            var row = (damageTree.DataSource as DataTable).NewRow();
            DataRow focusedRow = damageTree.GetFocusedDataRow();
            if (focusedRow != null)
            {
                row["PID"] = focusedRow["PID"];
                row["DamageTreeID"] = focusedRow["DamageTreeID"];
            }
            else if (focusedRow == null && damageTree.Nodes.Count < 1)//无根结点
            {
                row["PID"] = -1;
                row["DamageTreeID"] = damageTreeList.EditValue;
            }
            else
            {
                MessageUtil.ShowError("请在列表中选择参考结点.");
                return;
            }
            using (FrmDamageTreeItemEdit we = new FrmDamageTreeItemEdit(row, EditMode.Add))
            {
                we.ShowDialog();
                if (we.SubmitChanged)
                    this.LoadDamageTreeItemList();
            }
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow row = damageTree.GetFocusedDataRow();
            if (row == null)
            {
                MessageUtil.ShowError("请在列表中选择要编辑的内容.");
                return;
            }
            using (FrmDamageTreeItemEdit we = new FrmDamageTreeItemEdit(row, EditMode.Edit))
            {
                we.ShowDialog();
                if (we.SubmitChanged)
                    this.LoadDamageTreeItemList();
            }
        }

        private void btnAddSub_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!damageTreeList.EditValue.IsNotNull()) return;

            var row = (damageTree.DataSource as DataTable).NewRow();
            DataRow focusedRow = damageTree.GetFocusedDataRow();
            if (focusedRow != null)
            {
                row["PID"] = focusedRow["ID"];
                row["DamageTreeID"] = focusedRow["DamageTreeID"];
            }
            else
            {
                MessageUtil.ShowError("请在列表中选择参考结点.");
                return;
            }
            using (FrmDamageTreeItemEdit we = new FrmDamageTreeItemEdit(row, EditMode.Add))
            {
                we.ShowDialog();
                if (we.SubmitChanged)
                    this.LoadDamageTreeItemList();
            }
        }

        private void btnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!MessageUtil.ConfirmYesNo("请确定是否删除节点及其所有子节点.")) return;
            string message = string.Empty;
            try
            {
                CommonFunc.DeleteDamageTreeItem(Convert.ToInt64(damageTree.FocusedNode.GetValue("ID")), ref message);
                LoadDamageTreeItemList();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        private void btnRef_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadDamageTreeItemList();
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void damageTreeList_EditValueChanged(object sender, EventArgs e)
        {
            LoadDamageTreeItemList();
        }

        private void btnEditData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DamageTreeItemDataSchema();
        }

        private void btnEditDataSchema_Click(object sender, EventArgs e)
        {
            if (damageTree.FocusedNode == null) return;
            using (var dialog = new FrmDamageTreeItemDataSchemaEdit((long)damageTree.FocusedNode.GetValue("ID"), damageTree.GetFocusedDisplayText()))
            {
                //if (dialog.ShowDialog() == DialogResult.OK)
                //{
                //    DamageTreeItemDataSchema();
                //}
                dialog.ShowDialog();
                DamageTreeItemDataSchema();
            }
        }

        private void DamageTreeItemDataSchema()
        {
            try
            {
                dataItemPanel.Controls.Clear();
                if (damageTree.FocusedNode == null) return;
                CommonFunc.BuildDataSchemaItem(CommonFunc.GetDamageTreeItemDataSchema((long)damageTree.FocusedNode.GetValue("ID")), dataItemPanel);
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }
    }
}
