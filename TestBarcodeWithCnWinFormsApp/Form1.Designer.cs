namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnPrint = new Button();
            pictureBox1 = new PictureBox();
            txtWidth = new TextBox();
            txtHeight = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtBarCodeLength = new TextBox();
            radChar = new RadioButton();
            radUnicode = new RadioButton();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(12, 12);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(75, 23);
            btnPrint.TabIndex = 0;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Location = new Point(366, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(564, 337);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // txtWidth
            // 
            txtWidth.Location = new Point(50, 49);
            txtWidth.Name = "txtWidth";
            txtWidth.Size = new Size(100, 23);
            txtWidth.TabIndex = 2;
            txtWidth.Text = "300";
            // 
            // txtHeight
            // 
            txtHeight.Location = new Point(50, 90);
            txtHeight.Name = "txtHeight";
            txtHeight.Size = new Size(100, 23);
            txtHeight.TabIndex = 3;
            txtHeight.Text = "300";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 52);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 4;
            label1.Text = "宽度";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 93);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 5;
            label2.Text = "高度";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 135);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 7;
            label3.Text = "字数";
            // 
            // txtBarCodeLength
            // 
            txtBarCodeLength.Location = new Point(50, 132);
            txtBarCodeLength.Name = "txtBarCodeLength";
            txtBarCodeLength.Size = new Size(100, 23);
            txtBarCodeLength.TabIndex = 6;
            txtBarCodeLength.Text = "20";
            // 
            // radChar
            // 
            radChar.AutoSize = true;
            radChar.Location = new Point(20, 181);
            radChar.Name = "radChar";
            radChar.Size = new Size(51, 19);
            radChar.TabIndex = 9;
            radChar.TabStop = true;
            radChar.Text = "字母";
            radChar.UseVisualStyleBackColor = true;
            // 
            // radUnicode
            // 
            radUnicode.AutoSize = true;
            radUnicode.Checked = true;
            radUnicode.Location = new Point(77, 181);
            radUnicode.Name = "radUnicode";
            radUnicode.Size = new Size(51, 19);
            radUnicode.TabIndex = 10;
            radUnicode.TabStop = true;
            radUnicode.Text = "汉字";
            radUnicode.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 215);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(283, 286);
            textBox1.TabIndex = 11;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(932, 513);
            Controls.Add(textBox1);
            Controls.Add(radUnicode);
            Controls.Add(radChar);
            Controls.Add(label3);
            Controls.Add(txtBarCodeLength);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtHeight);
            Controls.Add(txtWidth);
            Controls.Add(btnPrint);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnPrint;
        private PictureBox pictureBox1;
        private TextBox txtWidth;
        private TextBox txtHeight;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtBarCodeLength;
        private RadioButton radChar;
        private RadioButton radUnicode;
        private TextBox textBox1;
    }
}