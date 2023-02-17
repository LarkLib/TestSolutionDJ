using DevExpress.XtraGrid.Views.Grid;
using DJ.LMS.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DJ.LMS.WinForms
{
    public partial class FrmSystemInfoMain : Form
    {
        public FrmSystemInfoMain()
        {
            InitializeComponent();
        }

        private void FrmSystemInfoMain_Load(object sender, EventArgs e)
        {
            GridControlUtil.SetGridViewColumns(gridList, "ItemList");
            tlItemMain.DoubleClick += tlItemMain_DoubleClick;
            //tlItemMain.Click += tlItemMain_Click;
            tlItemMain.FocusedNodeChanged += tlItemMain_FocusedNodeChanged;
            tlItemMain.MouseDown += tlItemMain_MouseDown;
            LoadItemMain();
        }

        void tlItemMain_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            LoadItemList();
        }

        private void LoadItemList()
        {
            gridList.ClearSorting();
            if (tlItemMain.FocusedNode != null && tlItemMain.GetDataRecordByNode(tlItemMain.FocusedNode) != null)
            {
                var row = (DataRowView)tlItemMain.GetDataRecordByNode(tlItemMain.FocusedNode);
                var itemID = (int)row["ItemID"];
                var table = CommonFunc.GetItemListItems(itemID);
                gridControlItemList.DataSource = table;
                gridList.BestFitColumns();
            }
        }

        void tlItemMain_GotFocus(object sender, EventArgs e)
        {
            if (tlItemMain.FocusedNode != null && tlItemMain.GetDataRecordByNode(tlItemMain.FocusedNode) != null)
            {
                var row = (DataRowView)tlItemMain.GetDataRecordByNode(tlItemMain.FocusedNode);
                var itemID = (int)row["ItemID"];
                var table = CommonFunc.GetItemListItems(itemID);
                gridControlItemList.DataSource = table;
                gridList.BestFitColumns();
            }
        }

        void tlItemMain_Click(object sender, EventArgs e)
        {
            if (tlItemMain.FocusedNode != null && tlItemMain.GetDataRecordByNode(tlItemMain.FocusedNode) != null)
            {
                var row = (DataRowView)tlItemMain.GetDataRecordByNode(tlItemMain.FocusedNode);

            }
        }

        private void LoadItemMain()
        {
            tlItemMain.DataSource = CommonFunc.GetItemMainItems();
            tlItemMain.BestFitColumns();
        }

        void tlItemMain_DoubleClick(object sender, EventArgs e)
        {
            EditRow(true, false);
        }

        private void EditRow(bool isEdit, bool isSubNode)
        {
            try
            {
                DataRowView row = null;
                var id = -1;
                if (tlItemMain.FocusedNode != null && tlItemMain.GetDataRecordByNode(tlItemMain.FocusedNode) != null)
                {
                    row = (DataRowView)tlItemMain.GetDataRecordByNode(tlItemMain.FocusedNode);
                    id = (int)row["ItemID"];
                }
                if (row == null && isEdit)
                {
                    return;
                }
                var form = new FrmItemMainEdit(row, isEdit, isSubNode);
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    LoadItemMain();
                    if (id > 0) tlItemMain.SetFocusedNode(tlItemMain.FindNodeByKeyID(id));
                }
            }
            catch (Exception ex)
            {

                MessageUtil.ShowError(ex.Message);
            }
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditRow(false, false);
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditRow(true, false);
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnRef_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadItemMain();
        }

        private void btnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (tlItemMain.FocusedNode == null || tlItemMain.GetDataRecordByNode(tlItemMain.FocusedNode) == null)
                {
                    return;
                }
                var row = (DataRowView)tlItemMain.GetDataRecordByNode(tlItemMain.FocusedNode);
                var id = (int)row["ItemID"];
                if (!CommonFunc.DelItemMain(id))
                    MessageUtil.ShowError("删除失败!");
                LoadItemMain();

            }
            catch (Exception ex)
            {

                MessageUtil.ShowError(ex.Message);
            }
        }

        private void btnRefItemList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadItemList();
        }

        private void gridList_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                var row = (DataRowView)e.Row;
                if (!CommonFunc.ModifyItemList((int)row["ItemListID"], (int)row["ItemID"], row["ItemListCode"].ToString(), row["ItemListName"].ToString(), (bool)row["IsEffective"], (int)row["ItemListOrder"], row["Remark"].ToString()))
                    MessageUtil.ShowError("更新失败!");
                else
                    LoadItemList();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        private void gridList_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            var itemID = (int)((DataRowView)tlItemMain.GetDataRecordByNode(tlItemMain.FocusedNode))["ItemID"];
            DataRow row = gridList.GetDataRow(e.RowHandle);
            row["IsEffective"] = true;
            row["ItemListID"] = -1;
            row["ItemID"] = itemID;
            row["ItemListOrder"] = CommonFunc.GetLastOrderByItemID(itemID);
        }

        void tlItemMain_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridList.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void btnAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditRow(false, false);
        }

        private void btnAddSub_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditRow(false, true);
        }

        private void btnUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridView view = gridList;
            view.GridControl.Focus();
            int index = view.FocusedRowHandle;
            if (index <= 0) return;

            DataRow row1 = view.GetDataRow(index);
            DataRow row2 = view.GetDataRow(index - 1);
            object value1 = row1["ItemListOrder"];
            object value2 = row2["ItemListOrder"];
            var id1 = row1["ItemListID"];
            var id2 = row2["ItemListID"];
            var cmdList = new List<string>();
            cmdList.Add(string.Format("update ItemList set ItemListOrder={0} where ItemListID={1}", value2, id1));
            cmdList.Add(string.Format("update ItemList set ItemListOrder={0} where ItemListID={1}", value1, id2));
            SqlDbHelper.ExecuteNoQueryByTransaction(cmdList);

            LoadItemList();
            view.FocusedRowHandle = index - 1;
        }


        private void btnDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridView view = gridList;
            view.GridControl.Focus();
            int index = view.FocusedRowHandle;
            if (index >= view.DataRowCount - 1) return;

            DataRow row1 = view.GetDataRow(index);
            DataRow row2 = view.GetDataRow(index + 1);
            object value1 = row1["ItemListOrder"];
            object value2 = row2["ItemListOrder"];
            var id1 = row1["ItemListID"];
            var id2 = row2["ItemListID"];
            var cmdList = new List<string>();
            cmdList.Add(string.Format("update ItemList set ItemListOrder={0} where ItemListID={1}", value2, id1));
            cmdList.Add(string.Format("update ItemList set ItemListOrder={0} where ItemListID={1}", value1, id2));
            SqlDbHelper.ExecuteNoQueryByTransaction(cmdList);
            LoadItemList();

            view.FocusedRowHandle = index + 1;
        }

        private void btnTop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridView view = gridList;
            view.GridControl.Focus();
            int index = view.FocusedRowHandle;
            if (index <= 0) return;

            DataRow row = view.GetDataRow(index);
            object value = row["ItemListOrder"];
            var id = row["ItemID"];
            var target = view.GetDataRow(0)["ItemListOrder"];

            var sql = string.Format(@"
update  il 
set ItemListOrder=o.LeadOrder
from ItemList il
join 
    (
    SELECT       ItemListID, 
    LAG (ItemListOrder,1,{2})   OVER (order by ItemListOrder) LagOrder,
    lead(ItemListOrder,1,{2})   OVER (order by ItemListOrder) LeadOrder
    FROM            ItemList
    WHERE        (ItemID = {0}) and ItemListOrder<={1}) o
on il.ItemListID=o.ItemListID
", id, value, target);
            SqlDbHelper.ExecuteNonQuery(sql);

            LoadItemList();
            view.FocusedRowHandle = 0;
        }

        private void btnBottom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridView view = gridList;
            view.GridControl.Focus();
            int index = view.FocusedRowHandle;
            if (index >= view.DataRowCount - 1) return;

            DataRow row = view.GetDataRow(index);
            object value = row["ItemListOrder"];
            var id = row["ItemID"];
            var target = view.GetDataRow(view.DataRowCount - 1)["ItemListOrder"];

            var sql = string.Format(@"
update  il 
set ItemListOrder=o.LagOrder
from ItemList il
join 
    (
    SELECT       ItemListID, 
    LAG (ItemListOrder,1,{2})   OVER (order by ItemListOrder) LagOrder,
    lead(ItemListOrder,1,{2})   OVER (order by ItemListOrder) LeadOrder
    FROM            ItemList
    WHERE        (ItemID = {0}) and ItemListOrder>={1}) o
on il.ItemListID=o.ItemListID
", id, value, target);
            SqlDbHelper.ExecuteNonQuery(sql);

            LoadItemList();
            view.FocusedRowHandle = view.DataRowCount - 1;
        }

        private void btnRefItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadItemList();
        }

        private void BtnSubDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DataRow row = gridList.GetDataRow(gridList.FocusedRowHandle);
                if (row == null)
                {
                    return;
                }
                var id = (int)row["ItemListID"];
                if (!CommonFunc.DelItemList(id))
                    MessageUtil.ShowError("删除失败!");
                LoadItemList();

            }
            catch (Exception ex)
            {

                MessageUtil.ShowError(ex.Message);
            }
        }
    }
}
