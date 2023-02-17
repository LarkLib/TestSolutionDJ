namespace DJ.LMS.WinForms
{
    partial class FrmUserEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserEdit));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ckAllSelect = new DevExpress.XtraEditors.CheckEdit();
            this.btnSubmit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmbRole = new DevExpress.XtraEditors.LookUpEdit();
            this.ckDefaultPwd = new DevExpress.XtraEditors.CheckEdit();
            this.ckIsEffective = new DevExpress.XtraEditors.CheckEdit();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.txtRealName = new DevExpress.XtraEditors.TextEdit();
            this.txtLoginName = new DevExpress.XtraEditors.TextEdit();
            this.txtUserID = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.clbWarehouse = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckAllSelect.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckDefaultPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckIsEffective.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clbWarehouse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.ckAllSelect);
            this.panelControl1.Controls.Add(this.btnSubmit);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.xtraTabControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(918, 655);
            this.panelControl1.TabIndex = 0;
            // 
            // ckAllSelect
            // 
            this.ckAllSelect.Location = new System.Drawing.Point(38, 608);
            this.ckAllSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckAllSelect.Name = "ckAllSelect";
            this.ckAllSelect.Properties.Caption = "全选";
            this.ckAllSelect.Size = new System.Drawing.Size(112, 26);
            this.ckAllSelect.TabIndex = 3;
            this.ckAllSelect.Visible = false;
            this.ckAllSelect.CheckedChanged += new System.EventHandler(this.ckAllSelect_CheckedChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSubmit.ImageOptions.Image")));
            this.btnSubmit.Location = new System.Drawing.Point(634, 604);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(112, 35);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "保存";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnClose
            // 
            this.btnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageOptions.Image")));
            this.btnClose.Location = new System.Drawing.Point(756, 604);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 35);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(18, 18);
            this.xtraTabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(882, 575);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage3});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.layoutControl1);
            this.xtraTabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(874, 533);
            this.xtraTabPage1.Text = "基础信息设置";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmbRole);
            this.layoutControl1.Controls.Add(this.ckDefaultPwd);
            this.layoutControl1.Controls.Add(this.ckIsEffective);
            this.layoutControl1.Controls.Add(this.txtRemark);
            this.layoutControl1.Controls.Add(this.txtRealName);
            this.layoutControl1.Controls.Add(this.txtLoginName);
            this.layoutControl1.Controls.Add(this.txtUserID);
            this.layoutControl1.Location = new System.Drawing.Point(30, 29);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(810, 480);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmbRole
            // 
            this.cmbRole.Location = new System.Drawing.Point(114, 119);
            this.cmbRole.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbRole.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name1", 30, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name2", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.cmbRole.Properties.DisplayMember = "Name";
            this.cmbRole.Properties.NullText = "";
            this.cmbRole.Properties.ShowFooter = false;
            this.cmbRole.Properties.ShowHeader = false;
            this.cmbRole.Properties.ValueMember = "ID";
            this.cmbRole.Size = new System.Drawing.Size(678, 28);
            this.cmbRole.StyleController = this.layoutControl1;
            this.cmbRole.TabIndex = 2;
            // 
            // ckDefaultPwd
            // 
            this.ckDefaultPwd.Location = new System.Drawing.Point(18, 437);
            this.ckDefaultPwd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckDefaultPwd.Name = "ckDefaultPwd";
            this.ckDefaultPwd.Properties.Caption = "初始登录密码（默认：123456）";
            this.ckDefaultPwd.Size = new System.Drawing.Size(774, 26);
            this.ckDefaultPwd.StyleController = this.layoutControl1;
            this.ckDefaultPwd.TabIndex = 5;
            // 
            // ckIsEffective
            // 
            this.ckIsEffective.Location = new System.Drawing.Point(18, 405);
            this.ckIsEffective.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckIsEffective.Name = "ckIsEffective";
            this.ckIsEffective.Properties.Caption = "用户有效性";
            this.ckIsEffective.Size = new System.Drawing.Size(774, 26);
            this.ckIsEffective.StyleController = this.layoutControl1;
            this.ckIsEffective.TabIndex = 4;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(114, 153);
            this.txtRemark.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Properties.MaxLength = 200;
            this.txtRemark.Size = new System.Drawing.Size(678, 246);
            this.txtRemark.StyleController = this.layoutControl1;
            this.txtRemark.TabIndex = 3;
            // 
            // txtRealName
            // 
            this.txtRealName.Location = new System.Drawing.Point(114, 85);
            this.txtRealName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRealName.Name = "txtRealName";
            this.txtRealName.Properties.MaxLength = 50;
            this.txtRealName.Size = new System.Drawing.Size(678, 28);
            this.txtRealName.StyleController = this.layoutControl1;
            this.txtRealName.TabIndex = 1;
            // 
            // txtLoginName
            // 
            this.txtLoginName.Location = new System.Drawing.Point(114, 51);
            this.txtLoginName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Properties.MaxLength = 50;
            this.txtLoginName.Size = new System.Drawing.Size(678, 28);
            this.txtLoginName.StyleController = this.layoutControl1;
            this.txtLoginName.TabIndex = 0;
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(114, 17);
            this.txtUserID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Properties.ReadOnly = true;
            this.txtUserID.Size = new System.Drawing.Size(678, 28);
            this.txtUserID.StyleController = this.layoutControl1;
            this.txtUserID.TabIndex = 6;
            this.txtUserID.Visible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(810, 480);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtUserID;
            this.layoutControlItem1.CustomizationFormText = "用户索引：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(780, 34);
            this.layoutControlItem1.Text = "用户索引：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(90, 22);
            this.layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtLoginName;
            this.layoutControlItem2.CustomizationFormText = "登录名称：";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(780, 34);
            this.layoutControlItem2.Text = "登录名称：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(90, 22);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtRealName;
            this.layoutControlItem3.CustomizationFormText = "真实姓名：";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 68);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(780, 34);
            this.layoutControlItem3.Text = "真实姓名：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(90, 22);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtRemark;
            this.layoutControlItem4.CustomizationFormText = "备注信息：";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 136);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(780, 252);
            this.layoutControlItem4.Text = "备注信息：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(90, 22);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ckIsEffective;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 388);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(780, 32);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.ckDefaultPwd;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 420);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(780, 32);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.cmbRole;
            this.layoutControlItem7.CustomizationFormText = "用户角色：";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 102);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(780, 34);
            this.layoutControlItem7.Text = "用户角色：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(90, 22);
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.clbWarehouse);
            this.xtraTabPage3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.PageVisible = false;
            this.xtraTabPage3.Size = new System.Drawing.Size(874, 533);
            this.xtraTabPage3.Text = "关联仓库设置";
            // 
            // clbWarehouse
            // 
            this.clbWarehouse.CheckOnClick = true;
            this.clbWarehouse.Location = new System.Drawing.Point(21, 21);
            this.clbWarehouse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clbWarehouse.Name = "clbWarehouse";
            this.clbWarehouse.Size = new System.Drawing.Size(828, 487);
            this.clbWarehouse.TabIndex = 0;
            // 
            // FrmUserEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 655);
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUserEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户信息编辑";
            this.Load += new System.EventHandler(this.FrmUserEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckAllSelect.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckDefaultPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckIsEffective.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clbWarehouse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.SimpleButton btnSubmit;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.CheckEdit ckDefaultPwd;
        private DevExpress.XtraEditors.CheckEdit ckIsEffective;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.TextEdit txtRealName;
        private DevExpress.XtraEditors.TextEdit txtLoginName;
        private DevExpress.XtraEditors.TextEdit txtUserID;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.CheckedListBoxControl clbWarehouse;
        private DevExpress.XtraEditors.LookUpEdit cmbRole;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraEditors.CheckEdit ckAllSelect;
    }
}