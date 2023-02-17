namespace DJ.LMS.WinForms.DianJin
{
    partial class TestForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("自动装弹机");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("炮管");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("炮膛");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("火炮系统", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("火控系统");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("炮塔");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("火力系统", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode4,
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("行走系统");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("动力系统");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("M1A1", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("云爆车");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("故障树", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11});
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.panelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1053, 706);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "groupControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.textBox1);
            this.panelControl1.Controls.Add(this.treeView1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(2, 34);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1049, 670);
            this.panelControl1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(386, 32);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(461, 517);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "\r\n\r\n撞击速度（标准值）：800 米/秒\r\n撞击速度（测量值）：\r\n温度（标准值）： 200 度\r\n温度（测量值）：\r\n炮管炸裂（测量值）：\r\n\r\n";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(51, 32);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node10";
            treeNode1.Text = "自动装弹机";
            treeNode2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            treeNode2.Name = "Node13";
            treeNode2.Text = "炮管";
            treeNode3.Name = "Node14";
            treeNode3.Text = "炮膛";
            treeNode4.Name = "Node11";
            treeNode4.Text = "火炮系统";
            treeNode5.Name = "Node12";
            treeNode5.Text = "火控系统";
            treeNode6.Name = "Node15";
            treeNode6.Text = "炮塔";
            treeNode7.Name = "Node7";
            treeNode7.Text = "火力系统";
            treeNode8.Name = "Node8";
            treeNode8.Text = "行走系统";
            treeNode9.Name = "Node9";
            treeNode9.Text = "动力系统";
            treeNode10.Name = "Node3";
            treeNode10.Text = "M1A1";
            treeNode11.Name = "Node6";
            treeNode11.Text = "云爆车";
            treeNode12.Name = "Node0";
            treeNode12.Text = "故障树";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode12});
            this.treeView1.Size = new System.Drawing.Size(261, 517);
            this.treeView1.TabIndex = 1;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 706);
            this.Controls.Add(this.groupControl1);
            this.Name = "TestForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TreeView treeView1;
    }
}