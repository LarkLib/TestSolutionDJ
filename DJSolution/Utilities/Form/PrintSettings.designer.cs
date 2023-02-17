namespace DJ.LMS.Utilities
{
    partial class PrintSettings
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
            this.gbColumnsToPrint = new System.Windows.Forms.GroupBox();
            this.chklst = new System.Windows.Forms.CheckedListBox();
            this.gboxRowsToPrint = new System.Windows.Forms.GroupBox();
            this.rdoSelectedRows = new System.Windows.Forms.RadioButton();
            this.rdoAllRows = new System.Windows.Forms.RadioButton();
            this.chkFitToPageWidth = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbTitle = new System.Windows.Forms.GroupBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gbColumnsToPrint.SuspendLayout();
            this.gboxRowsToPrint.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbColumnsToPrint
            // 
            this.gbColumnsToPrint.Controls.Add(this.chklst);
            this.gbColumnsToPrint.Location = new System.Drawing.Point(12, 12);
            this.gbColumnsToPrint.Name = "gbColumnsToPrint";
            this.gbColumnsToPrint.Size = new System.Drawing.Size(200, 307);
            this.gbColumnsToPrint.TabIndex = 0;
            this.gbColumnsToPrint.TabStop = false;
            this.gbColumnsToPrint.Text = "待打印的列：";
            // 
            // chklst
            // 
            this.chklst.CheckOnClick = true;
            this.chklst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chklst.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklst.FormattingEnabled = true;
            this.chklst.Location = new System.Drawing.Point(3, 17);
            this.chklst.Name = "chklst";
            this.chklst.Size = new System.Drawing.Size(194, 287);
            this.chklst.TabIndex = 14;
            // 
            // gboxRowsToPrint
            // 
            this.gboxRowsToPrint.Controls.Add(this.rdoSelectedRows);
            this.gboxRowsToPrint.Controls.Add(this.rdoAllRows);
            this.gboxRowsToPrint.Font = new System.Drawing.Font("宋体", 9F);
            this.gboxRowsToPrint.Location = new System.Drawing.Point(218, 12);
            this.gboxRowsToPrint.Name = "gboxRowsToPrint";
            this.gboxRowsToPrint.Size = new System.Drawing.Size(186, 57);
            this.gboxRowsToPrint.TabIndex = 19;
            this.gboxRowsToPrint.TabStop = false;
            this.gboxRowsToPrint.Text = "打印方式：";
            // 
            // rdoSelectedRows
            // 
            this.rdoSelectedRows.AutoSize = true;
            this.rdoSelectedRows.Font = new System.Drawing.Font("宋体", 9F);
            this.rdoSelectedRows.Location = new System.Drawing.Point(104, 24);
            this.rdoSelectedRows.Name = "rdoSelectedRows";
            this.rdoSelectedRows.Size = new System.Drawing.Size(59, 16);
            this.rdoSelectedRows.TabIndex = 1;
            this.rdoSelectedRows.TabStop = true;
            this.rdoSelectedRows.Text = "选中行";
            this.rdoSelectedRows.UseVisualStyleBackColor = true;
            // 
            // rdoAllRows
            // 
            this.rdoAllRows.AutoSize = true;
            this.rdoAllRows.Font = new System.Drawing.Font("宋体", 9F);
            this.rdoAllRows.Location = new System.Drawing.Point(22, 24);
            this.rdoAllRows.Name = "rdoAllRows";
            this.rdoAllRows.Size = new System.Drawing.Size(59, 16);
            this.rdoAllRows.TabIndex = 0;
            this.rdoAllRows.TabStop = true;
            this.rdoAllRows.Text = "全部行";
            this.rdoAllRows.UseVisualStyleBackColor = true;
            // 
            // chkFitToPageWidth
            // 
            this.chkFitToPageWidth.AutoSize = true;
            this.chkFitToPageWidth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkFitToPageWidth.Font = new System.Drawing.Font("宋体", 9F);
            this.chkFitToPageWidth.Location = new System.Drawing.Point(22, 24);
            this.chkFitToPageWidth.Name = "chkFitToPageWidth";
            this.chkFitToPageWidth.Size = new System.Drawing.Size(102, 17);
            this.chkFitToPageWidth.TabIndex = 22;
            this.chkFitToPageWidth.Text = "适应页面宽度";
            this.chkFitToPageWidth.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkFitToPageWidth);
            this.groupBox1.Location = new System.Drawing.Point(218, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 57);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "页面设置：";
            // 
            // gbTitle
            // 
            this.gbTitle.Controls.Add(this.txtTitle);
            this.gbTitle.Location = new System.Drawing.Point(218, 138);
            this.gbTitle.Name = "gbTitle";
            this.gbTitle.Size = new System.Drawing.Size(186, 181);
            this.gbTitle.TabIndex = 24;
            this.gbTitle.TabStop = false;
            this.gbTitle.Text = "页面抬头：";
            // 
            // txtTitle
            // 
            this.txtTitle.AcceptsReturn = true;
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTitle.Font = new System.Drawing.Font("宋体", 9F);
            this.txtTitle.Location = new System.Drawing.Point(3, 17);
            this.txtTitle.Multiline = true;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTitle.Size = new System.Drawing.Size(180, 161);
            this.txtTitle.TabIndex = 20;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F);
            this.btnOK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.Location = new System.Drawing.Point(266, 337);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(63, 23);
            this.btnOK.TabIndex = 25;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 9F);
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancel.Location = new System.Drawing.Point(338, 337);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(63, 23);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(1, 329);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(413, 1);
            this.label1.TabIndex = 27;
            // 
            // PrintSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 366);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbTitle);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gboxRowsToPrint);
            this.Controls.Add(this.gbColumnsToPrint);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "打印设置";
            this.Load += new System.EventHandler(this.PrintSettings_Load);
            this.gbColumnsToPrint.ResumeLayout(false);
            this.gboxRowsToPrint.ResumeLayout(false);
            this.gboxRowsToPrint.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbTitle.ResumeLayout(false);
            this.gbTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox gbColumnsToPrint;
        internal System.Windows.Forms.CheckedListBox chklst;
        internal System.Windows.Forms.GroupBox gboxRowsToPrint;
        internal System.Windows.Forms.RadioButton rdoSelectedRows;
        internal System.Windows.Forms.RadioButton rdoAllRows;
        internal System.Windows.Forms.CheckBox chkFitToPageWidth;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.GroupBox gbTitle;
        internal System.Windows.Forms.TextBox txtTitle;
        protected System.Windows.Forms.Button btnOK;
        protected System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;

    }
}