namespace DJ.LMS.WinForms
{
    partial class FrmSystemInfoMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSystemInfoMain));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.tlItemMain = new DevExpress.XtraTreeList.TreeList();
            this.ItemID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ItemParentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ItemCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ItemName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ItemOrder = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.IsEffictive = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Remark = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.barDockControl6 = new DevExpress.XtraBars.BarDockControl();
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.btnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.btnDel = new DevExpress.XtraBars.BarButtonItem();
            this.btnRef = new DevExpress.XtraBars.BarButtonItem();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl5 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl7 = new DevExpress.XtraBars.BarDockControl();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.btnAddNew = new DevExpress.XtraBars.BarButtonItem();
            this.btnAddSub = new DevExpress.XtraBars.BarButtonItem();
            this.gridControlItemList = new DevExpress.XtraGrid.GridControl();
            this.gridList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnUp = new DevExpress.XtraBars.BarButtonItem();
            this.btnDown = new DevExpress.XtraBars.BarButtonItem();
            this.btnTop = new DevExpress.XtraBars.BarButtonItem();
            this.btnBottom = new DevExpress.XtraBars.BarButtonItem();
            this.btnRefItem = new DevExpress.XtraBars.BarButtonItem();
            this.btnSubDel = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDockingMenuItem1 = new DevExpress.XtraBars.BarDockingMenuItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlItemMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.tlItemMain);
            this.splitContainerControl1.Panel1.Controls.Add(this.barDockControl6);
            this.splitContainerControl1.Panel1.Controls.Add(this.barDockControl7);
            this.splitContainerControl1.Panel1.Controls.Add(this.barDockControl5);
            this.splitContainerControl1.Panel1.Controls.Add(this.barDockControl4);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControlItemList);
            this.splitContainerControl1.Panel2.Controls.Add(this.barDockControlLeft);
            this.splitContainerControl1.Panel2.Controls.Add(this.barDockControlRight);
            this.splitContainerControl1.Panel2.Controls.Add(this.barDockControlBottom);
            this.splitContainerControl1.Panel2.Controls.Add(this.barDockControlTop);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(825, 475);
            this.splitContainerControl1.SplitterPosition = 391;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // tlItemMain
            // 
            this.tlItemMain.AllowDrop = true;
            this.tlItemMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.ItemID,
            this.ItemParentID,
            this.ItemCode,
            this.ItemName,
            this.ItemOrder,
            this.IsEffictive,
            this.Remark});
            this.tlItemMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlItemMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlItemMain.KeyFieldName = "ItemID";
            this.tlItemMain.Location = new System.Drawing.Point(0, 31);
            this.tlItemMain.Name = "tlItemMain";
            this.tlItemMain.ParentFieldName = "ItemParentID";
            this.tlItemMain.RootValue = -1;
            this.tlItemMain.Size = new System.Drawing.Size(392, 444);
            this.tlItemMain.TabIndex = 0;
            // 
            // ItemID
            // 
            this.ItemID.Caption = "ItemID";
            this.ItemID.FieldName = "ItemID";
            this.ItemID.Name = "ItemID";
            this.ItemID.OptionsColumn.AllowEdit = false;
            // 
            // ItemParentID
            // 
            this.ItemParentID.Caption = "ItemParentID";
            this.ItemParentID.FieldName = "ItemParentID";
            this.ItemParentID.Name = "ItemParentID";
            this.ItemParentID.OptionsColumn.AllowEdit = false;
            // 
            // ItemCode
            // 
            this.ItemCode.Caption = "编码";
            this.ItemCode.FieldName = "ItemCode";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.OptionsColumn.AllowEdit = false;
            this.ItemCode.Visible = true;
            this.ItemCode.VisibleIndex = 0;
            this.ItemCode.Width = 87;
            // 
            // ItemName
            // 
            this.ItemName.Caption = "名称";
            this.ItemName.FieldName = "ItemName";
            this.ItemName.Name = "ItemName";
            this.ItemName.OptionsColumn.AllowEdit = false;
            this.ItemName.Visible = true;
            this.ItemName.VisibleIndex = 1;
            this.ItemName.Width = 87;
            // 
            // ItemOrder
            // 
            this.ItemOrder.Caption = "ItemOrder";
            this.ItemOrder.FieldName = "ItemOrder";
            this.ItemOrder.Name = "ItemOrder";
            this.ItemOrder.OptionsColumn.AllowEdit = false;
            // 
            // IsEffictive
            // 
            this.IsEffictive.Caption = "有效性";
            this.IsEffictive.FieldName = "IsEffictive";
            this.IsEffictive.Name = "IsEffictive";
            this.IsEffictive.OptionsColumn.AllowEdit = false;
            this.IsEffictive.Visible = true;
            this.IsEffictive.VisibleIndex = 2;
            this.IsEffictive.Width = 87;
            // 
            // Remark
            // 
            this.Remark.Caption = "备注信息";
            this.Remark.FieldName = "Remark";
            this.Remark.Name = "Remark";
            this.Remark.OptionsColumn.AllowEdit = false;
            this.Remark.Visible = true;
            this.Remark.VisibleIndex = 3;
            this.Remark.Width = 87;
            // 
            // barDockControl6
            // 
            this.barDockControl6.CausesValidation = false;
            this.barDockControl6.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl6.Location = new System.Drawing.Point(0, 31);
            this.barDockControl6.Manager = this.barManager2;
            this.barDockControl6.Size = new System.Drawing.Size(0, 444);
            // 
            // barManager2
            // 
            this.barManager2.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager2.DockControls.Add(this.barDockControl4);
            this.barManager2.DockControls.Add(this.barDockControl5);
            this.barManager2.DockControls.Add(this.barDockControl6);
            this.barManager2.DockControls.Add(this.barDockControl7);
            this.barManager2.Form = this.splitContainerControl1.Panel1;
            this.barManager2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnAdd,
            this.btnEdit,
            this.btnDel,
            this.btnRef,
            this.btnClose,
            this.barSubItem1,
            this.btnAddNew,
            this.btnAddSub});
            this.barManager2.MaxItemId = 8;
            // 
            // bar2
            // 
            this.bar2.BarName = "Tools";
            this.bar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(537, 353);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnDel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnRef, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Tools";
            // 
            // btnAdd
            // 
            this.btnAdd.Caption = "添加";
            this.btnAdd.Enabled = false;
            this.btnAdd.Id = 0;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.LargeImage")));
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAdd_ItemClick);
            // 
            // btnEdit
            // 
            this.btnEdit.Caption = "编辑";
            this.btnEdit.Id = 1;
            this.btnEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.Image")));
            this.btnEdit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.LargeImage")));
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEdit_ItemClick);
            // 
            // btnDel
            // 
            this.btnDel.Caption = "删除";
            this.btnDel.Enabled = false;
            this.btnDel.Id = 2;
            this.btnDel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.ImageOptions.Image")));
            this.btnDel.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDel.ImageOptions.LargeImage")));
            this.btnDel.Name = "btnDel";
            this.btnDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDel_ItemClick);
            // 
            // btnRef
            // 
            this.btnRef.Caption = "刷新";
            this.btnRef.Id = 3;
            this.btnRef.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRef.ImageOptions.Image")));
            this.btnRef.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRef.ImageOptions.LargeImage")));
            this.btnRef.Name = "btnRef";
            this.btnRef.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRef_ItemClick);
            // 
            // btnClose
            // 
            this.btnClose.Caption = "关闭";
            this.btnClose.Id = 4;
            this.btnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageOptions.Image")));
            this.btnClose.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageOptions.LargeImage")));
            this.btnClose.Name = "btnClose";
            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClose_ItemClick);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl4.Location = new System.Drawing.Point(0, 0);
            this.barDockControl4.Manager = this.barManager2;
            this.barDockControl4.Size = new System.Drawing.Size(392, 31);
            // 
            // barDockControl5
            // 
            this.barDockControl5.CausesValidation = false;
            this.barDockControl5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl5.Location = new System.Drawing.Point(0, 475);
            this.barDockControl5.Manager = this.barManager2;
            this.barDockControl5.Size = new System.Drawing.Size(392, 0);
            // 
            // barDockControl7
            // 
            this.barDockControl7.CausesValidation = false;
            this.barDockControl7.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl7.Location = new System.Drawing.Point(392, 31);
            this.barDockControl7.Manager = this.barManager2;
            this.barDockControl7.Size = new System.Drawing.Size(0, 444);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "barSubItem1";
            this.barSubItem1.Id = 5;
            this.barSubItem1.Name = "barSubItem1";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Caption = "添加同级节点";
            this.btnAddNew.Id = 6;
            this.btnAddNew.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNew.ImageOptions.Image")));
            this.btnAddNew.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAddNew.ImageOptions.LargeImage")));
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddNew_ItemClick);
            // 
            // btnAddSub
            // 
            this.btnAddSub.Caption = "添加子节点";
            this.btnAddSub.Id = 7;
            this.btnAddSub.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddSub.ImageOptions.Image")));
            this.btnAddSub.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAddSub.ImageOptions.LargeImage")));
            this.btnAddSub.Name = "btnAddSub";
            this.btnAddSub.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddSub_ItemClick);
            // 
            // gridControlItemList
            // 
            this.gridControlItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlItemList.Location = new System.Drawing.Point(0, 31);
            this.gridControlItemList.MainView = this.gridList;
            this.gridControlItemList.Name = "gridControlItemList";
            this.gridControlItemList.Size = new System.Drawing.Size(428, 444);
            this.gridControlItemList.TabIndex = 5;
            this.gridControlItemList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridList});
            // 
            // gridList
            // 
            this.gridList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gridList.GridControl = this.gridControlItemList;
            this.gridList.Name = "gridList";
            this.gridList.OptionsCustomization.AllowFilter = false;
            this.gridList.OptionsCustomization.AllowGroup = false;
            this.gridList.OptionsCustomization.AllowSort = false;
            this.gridList.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridList.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridList_InitNewRow);
            this.gridList.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridList_RowUpdated);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "sss";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 444);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this.splitContainerControl1.Panel2;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barDockingMenuItem1,
            this.barButtonItem1,
            this.btnUp,
            this.btnDown,
            this.btnTop,
            this.btnBottom,
            this.btnRefItem,
            this.btnSubDel});
            this.barManager1.MaxItemId = 8;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 1";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.FloatLocation = new System.Drawing.Point(560, 197);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnUp, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnDown, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnTop, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnBottom, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnRefItem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnSubDel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 1";
            // 
            // btnUp
            // 
            this.btnUp.Caption = "上移";
            this.btnUp.Id = 2;
            this.btnUp.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.ImageOptions.Image")));
            this.btnUp.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnUp.ImageOptions.LargeImage")));
            this.btnUp.Name = "btnUp";
            this.btnUp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUp_ItemClick);
            // 
            // btnDown
            // 
            this.btnDown.Caption = "下移";
            this.btnDown.Id = 3;
            this.btnDown.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.ImageOptions.Image")));
            this.btnDown.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDown.ImageOptions.LargeImage")));
            this.btnDown.Name = "btnDown";
            this.btnDown.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDown_ItemClick);
            // 
            // btnTop
            // 
            this.btnTop.Caption = "移到顶部";
            this.btnTop.Id = 4;
            this.btnTop.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTop.ImageOptions.Image")));
            this.btnTop.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnTop.ImageOptions.LargeImage")));
            this.btnTop.Name = "btnTop";
            this.btnTop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnTop_ItemClick);
            // 
            // btnBottom
            // 
            this.btnBottom.Caption = "移到底部";
            this.btnBottom.Id = 5;
            this.btnBottom.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBottom.ImageOptions.Image")));
            this.btnBottom.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnBottom.ImageOptions.LargeImage")));
            this.btnBottom.Name = "btnBottom";
            this.btnBottom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBottom_ItemClick);
            // 
            // btnRefItem
            // 
            this.btnRefItem.Caption = "刷新";
            this.btnRefItem.Id = 6;
            this.btnRefItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRefItem.ImageOptions.Image")));
            this.btnRefItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRefItem.ImageOptions.LargeImage")));
            this.btnRefItem.Name = "btnRefItem";
            this.btnRefItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefItem_ItemClick);
            // 
            // btnSubDel
            // 
            this.btnSubDel.Caption = "删除";
            this.btnSubDel.Id = 7;
            this.btnSubDel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSubDel.ImageOptions.Image")));
            this.btnSubDel.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSubDel.ImageOptions.LargeImage")));
            this.btnSubDel.Name = "btnSubDel";
            this.btnSubDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnSubDel_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(428, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 475);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(428, 0);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(428, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 444);
            // 
            // barDockingMenuItem1
            // 
            this.barDockingMenuItem1.Caption = "barDockingMenuItem1";
            this.barDockingMenuItem1.Id = 0;
            this.barDockingMenuItem1.Name = "barDockingMenuItem1";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "上移";
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Manager = null;
            this.barDockControl1.Size = new System.Drawing.Size(366, 29);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 475);
            this.barDockControl2.Manager = null;
            this.barDockControl2.Size = new System.Drawing.Size(366, 0);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 29);
            this.barDockControl3.Manager = null;
            this.barDockControl3.Size = new System.Drawing.Size(0, 446);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnAddNew, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnAddSub, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDel)});
            this.popupMenu1.Manager = this.barManager2;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // FrmSystemInfoMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 475);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FrmSystemInfoMain";
            this.Text = "FrmSystemInfoMain";
            this.Load += new System.EventHandler(this.FrmSystemInfoMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlItemMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList tlItemMain;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ItemCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ItemName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ItemOrder;
        private DevExpress.XtraTreeList.Columns.TreeListColumn IsEffictive;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Remark;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl6;
        private DevExpress.XtraBars.BarDockControl barDockControl7;
        private DevExpress.XtraBars.BarDockControl barDockControl5;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem btnAdd;
        private DevExpress.XtraBars.BarButtonItem btnEdit;
        private DevExpress.XtraBars.BarButtonItem btnDel;
        private DevExpress.XtraBars.BarButtonItem btnRef;
        private DevExpress.XtraBars.BarButtonItem btnClose;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ItemID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ItemParentID;
        private DevExpress.XtraGrid.GridControl gridControlItemList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem btnAddNew;
        private DevExpress.XtraBars.BarButtonItem btnAddSub;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockingMenuItem barDockingMenuItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem btnUp;
        private DevExpress.XtraBars.BarButtonItem btnDown;
        private DevExpress.XtraBars.BarButtonItem btnTop;
        private DevExpress.XtraBars.BarButtonItem btnBottom;
        private DevExpress.XtraBars.BarButtonItem btnRefItem;
        private DevExpress.XtraBars.BarButtonItem btnSubDel;
    }
}