namespace DJ.LMS.WinForms
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.mainMenu = new DevExpress.XtraBars.Bar();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.menuModifyPwd = new DevExpress.XtraBars.BarButtonItem();
            this.menuExit = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.menuAbout = new DevExpress.XtraBars.BarButtonItem();
            this.mainStatusBar = new DevExpress.XtraBars.Bar();
            this.statusLabelUser = new DevExpress.XtraBars.BarStaticItem();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnAllScreen = new DevExpress.XtraBars.BarButtonItem();
            this.btnStandard = new DevExpress.XtraBars.BarButtonItem();
            this.btnCloseCurrentWindow = new DevExpress.XtraBars.BarButtonItem();
            this.btnCloseAllWindow = new DevExpress.XtraBars.BarButtonItem();
            this.btnNotepade = new DevExpress.XtraBars.BarButtonItem();
            this.btnCalculator = new DevExpress.XtraBars.BarButtonItem();
            this.btnExit = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.imageTrvList = new System.Windows.Forms.ImageList(this.components);
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.trvList = new System.Windows.Forms.TreeView();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.mainMenu,
            this.mainStatusBar,
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSubItem1,
            this.menuModifyPwd,
            this.menuExit,
            this.barSubItem2,
            this.menuAbout,
            this.btnAllScreen,
            this.btnStandard,
            this.btnCloseCurrentWindow,
            this.btnCloseAllWindow,
            this.btnNotepade,
            this.btnCalculator,
            this.btnExit,
            this.statusLabelUser});
            this.barManager1.MainMenu = this.mainMenu;
            this.barManager1.MaxItemId = 14;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1});
            this.barManager1.StatusBar = this.mainStatusBar;
            // 
            // mainMenu
            // 
            this.mainMenu.BarName = "Main menu";
            this.mainMenu.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.mainMenu.DockCol = 0;
            this.mainMenu.DockRow = 0;
            this.mainMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.mainMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem2)});
            this.mainMenu.OptionsBar.AllowQuickCustomization = false;
            this.mainMenu.OptionsBar.MultiLine = true;
            this.mainMenu.OptionsBar.UseWholeRow = true;
            this.mainMenu.Text = "Main menu";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "系统(&S)";
            this.barSubItem1.Id = 0;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModifyPwd),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuExit)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // menuModifyPwd
            // 
            this.menuModifyPwd.Caption = "修改密码";
            this.menuModifyPwd.Id = 1;
            this.menuModifyPwd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("menuModifyPwd.ImageOptions.Image")));
            this.menuModifyPwd.Name = "menuModifyPwd";
            this.menuModifyPwd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModifyPwd_ItemClick);
            // 
            // menuExit
            // 
            this.menuExit.Caption = "退出系统";
            this.menuExit.Id = 2;
            this.menuExit.ImageOptions.Image = global::DJ.LMS.WinForms.Properties.Resources.logout;
            this.menuExit.Name = "menuExit";
            this.menuExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuExit_ItemClick);
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "帮助(&H)";
            this.barSubItem2.Id = 3;
            this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuAbout)});
            this.barSubItem2.Name = "barSubItem2";
            // 
            // menuAbout
            // 
            this.menuAbout.Caption = "关于(&A)";
            this.menuAbout.Id = 4;
            this.menuAbout.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("menuAbout.ImageOptions.Image")));
            this.menuAbout.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("menuAbout.ImageOptions.LargeImage")));
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuAbout_ItemClick);
            // 
            // mainStatusBar
            // 
            this.mainStatusBar.BarName = "Status bar";
            this.mainStatusBar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.mainStatusBar.DockCol = 0;
            this.mainStatusBar.DockRow = 0;
            this.mainStatusBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.mainStatusBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.statusLabelUser)});
            this.mainStatusBar.OptionsBar.AllowQuickCustomization = false;
            this.mainStatusBar.OptionsBar.DrawDragBorder = false;
            this.mainStatusBar.OptionsBar.UseWholeRow = true;
            this.mainStatusBar.Text = "Status bar";
            // 
            // statusLabelUser
            // 
            this.statusLabelUser.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.statusLabelUser.Id = 13;
            this.statusLabelUser.Name = "statusLabelUser";
            this.statusLabelUser.Size = new System.Drawing.Size(200, 0);
            this.statusLabelUser.Width = 200;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 4";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnAllScreen, "", false, true, false, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnStandard, "", false, true, false, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnCloseCurrentWindow, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnCloseAllWindow, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnNotepade, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnCalculator, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnExit, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 4";
            // 
            // btnAllScreen
            // 
            this.btnAllScreen.Caption = "全屏模式";
            this.btnAllScreen.Id = 6;
            this.btnAllScreen.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAllScreen.ImageOptions.Image")));
            this.btnAllScreen.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAllScreen.ImageOptions.LargeImage")));
            this.btnAllScreen.Name = "btnAllScreen";
            this.btnAllScreen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAllScreen_ItemClick);
            // 
            // btnStandard
            // 
            this.btnStandard.Caption = "标准模式";
            this.btnStandard.Id = 7;
            this.btnStandard.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnStandard.ImageOptions.Image")));
            this.btnStandard.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnStandard.ImageOptions.LargeImage")));
            this.btnStandard.Name = "btnStandard";
            this.btnStandard.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStandard_ItemClick);
            // 
            // btnCloseCurrentWindow
            // 
            this.btnCloseCurrentWindow.Caption = "关闭当前";
            this.btnCloseCurrentWindow.Id = 8;
            this.btnCloseCurrentWindow.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseCurrentWindow.ImageOptions.Image")));
            this.btnCloseCurrentWindow.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnCloseCurrentWindow.ImageOptions.LargeImage")));
            this.btnCloseCurrentWindow.Name = "btnCloseCurrentWindow";
            this.btnCloseCurrentWindow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCloseCurrentWindow_ItemClick);
            // 
            // btnCloseAllWindow
            // 
            this.btnCloseAllWindow.Caption = "关闭全部";
            this.btnCloseAllWindow.Id = 9;
            this.btnCloseAllWindow.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseAllWindow.ImageOptions.Image")));
            this.btnCloseAllWindow.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnCloseAllWindow.ImageOptions.LargeImage")));
            this.btnCloseAllWindow.Name = "btnCloseAllWindow";
            this.btnCloseAllWindow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCloseAllWindow_ItemClick);
            // 
            // btnNotepade
            // 
            this.btnNotepade.Caption = "记事本";
            this.btnNotepade.Id = 10;
            this.btnNotepade.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNotepade.ImageOptions.Image")));
            this.btnNotepade.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnNotepade.ImageOptions.LargeImage")));
            this.btnNotepade.Name = "btnNotepade";
            this.btnNotepade.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNotepade_ItemClick);
            // 
            // btnCalculator
            // 
            this.btnCalculator.Caption = "计算器";
            this.btnCalculator.Id = 11;
            this.btnCalculator.ImageOptions.Image = global::DJ.LMS.WinForms.Properties.Resources.calc;
            this.btnCalculator.Name = "btnCalculator";
            this.btnCalculator.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCalculator_ItemClick);
            // 
            // btnExit
            // 
            this.btnExit.Caption = "退出系统";
            this.btnExit.Id = 12;
            this.btnExit.ImageOptions.Image = global::DJ.LMS.WinForms.Properties.Resources.logout;
            this.btnExit.Name = "btnExit";
            this.btnExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuExit_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlTop.Size = new System.Drawing.Size(846, 74);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 449);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlBottom.Size = new System.Drawing.Size(846, 37);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 74);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 375);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(846, 74);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 375);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.MenuManager = this.barManager1;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // imageTrvList
            // 
            this.imageTrvList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageTrvList.ImageStream")));
            this.imageTrvList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageTrvList.Images.SetKeyName(0, "folder.gif");
            this.imageTrvList.Images.SetKeyName(1, "folder_open.png");
            this.imageTrvList.Images.SetKeyName(2, "list_comments.gif");
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
            this.navBarControl1.Location = new System.Drawing.Point(0, 74);
            this.navBarControl1.Margin = new System.Windows.Forms.Padding(4);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.NavigationPaneMaxVisibleGroups = 0;
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 447;
            this.navBarControl1.OptionsNavPane.ShowGroupImageInHeader = true;
            this.navBarControl1.OptionsNavPane.ShowOverflowPanel = false;
            this.navBarControl1.OptionsNavPane.ShowSplitter = false;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.Size = new System.Drawing.Size(447, 375);
            this.navBarControl1.TabIndex = 10;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.View = new DevExpress.XtraNavBar.ViewInfo.SkinNavigationPaneViewInfoRegistrator();
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "功能菜单";
            this.navBarGroup1.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Small;
            this.navBarGroup1.GroupClientHeight = 332;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup1.ImageOptions.SmallImage")));
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.trvList);
            this.navBarGroupControlContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(447, 334);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // trvList
            // 
            this.trvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvList.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.trvList.ImageIndex = 0;
            this.trvList.ImageList = this.imageTrvList;
            this.trvList.ItemHeight = 20;
            this.trvList.Location = new System.Drawing.Point(0, 0);
            this.trvList.Margin = new System.Windows.Forms.Padding(4);
            this.trvList.Name = "trvList";
            this.trvList.SelectedImageIndex = 1;
            this.trvList.Size = new System.Drawing.Size(447, 334);
            this.trvList.TabIndex = 9;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(447, 74);
            this.splitterControl1.Margin = new System.Windows.Forms.Padding(4);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(9, 375);
            this.splitterControl1.TabIndex = 11;
            this.splitterControl1.TabStop = false;
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.MdiParent = this;
            this.xtraTabbedMdiManager1.ShowFloatingDropHint = DevExpress.Utils.DefaultBoolean.True;
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 486);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.navBarControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "毁伤效能评估系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar mainMenu;
        private DevExpress.XtraBars.Bar mainStatusBar;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem menuModifyPwd;
        private DevExpress.XtraBars.BarButtonItem menuExit;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarButtonItem menuAbout;
        private System.Windows.Forms.ImageList imageTrvList;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        internal DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnAllScreen;
        private DevExpress.XtraBars.BarButtonItem btnStandard;
        private DevExpress.XtraBars.BarButtonItem btnCloseCurrentWindow;
        private DevExpress.XtraBars.BarButtonItem btnCloseAllWindow;
        private DevExpress.XtraBars.BarButtonItem btnNotepade;
        private DevExpress.XtraBars.BarButtonItem btnCalculator;
        private DevExpress.XtraBars.BarButtonItem btnExit;
        private DevExpress.XtraBars.BarStaticItem statusLabelUser;
        private System.Windows.Forms.TreeView trvList;
    }
}