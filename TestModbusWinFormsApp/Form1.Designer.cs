namespace TestModbusWinFormsApp
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
            button1 = new Button();
            txtReadCommand = new TextBox();
            button2 = new Button();
            txtWriteCommand = new TextBox();
            txtAddress = new TextBox();
            txtValue = new TextBox();
            txtReadResult = new TextBox();
            btnWriteOneCommand = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(102, 23);
            button1.TabIndex = 0;
            button1.Text = "ReadCommand";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtReadCommand
            // 
            txtReadCommand.Location = new Point(234, 12);
            txtReadCommand.Name = "txtReadCommand";
            txtReadCommand.Size = new Size(260, 23);
            txtReadCommand.TabIndex = 1;
            // 
            // button2
            // 
            button2.Location = new Point(12, 41);
            button2.Name = "button2";
            button2.Size = new Size(102, 23);
            button2.TabIndex = 2;
            button2.Text = "WriteCommand";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // txtWriteCommand
            // 
            txtWriteCommand.Location = new Point(234, 41);
            txtWriteCommand.Name = "txtWriteCommand";
            txtWriteCommand.Size = new Size(260, 23);
            txtWriteCommand.TabIndex = 3;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(120, 13);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(100, 23);
            txtAddress.TabIndex = 4;
            txtAddress.Text = "40001";
            // 
            // txtValue
            // 
            txtValue.Location = new Point(120, 42);
            txtValue.Name = "txtValue";
            txtValue.Size = new Size(100, 23);
            txtValue.TabIndex = 5;
            txtValue.Text = "12";
            // 
            // txtReadResult
            // 
            txtReadResult.Location = new Point(500, 13);
            txtReadResult.Name = "txtReadResult";
            txtReadResult.Size = new Size(100, 23);
            txtReadResult.TabIndex = 6;
            // 
            // btnWriteOneCommand
            // 
            btnWriteOneCommand.Location = new Point(12, 66);
            btnWriteOneCommand.Name = "btnWriteOneCommand";
            btnWriteOneCommand.Size = new Size(102, 23);
            btnWriteOneCommand.TabIndex = 7;
            btnWriteOneCommand.Text = "WriteOneCommand";
            btnWriteOneCommand.UseVisualStyleBackColor = true;
            btnWriteOneCommand.Click += btnWriteOneCommand_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnWriteOneCommand);
            Controls.Add(txtReadResult);
            Controls.Add(txtValue);
            Controls.Add(txtAddress);
            Controls.Add(txtWriteCommand);
            Controls.Add(button2);
            Controls.Add(txtReadCommand);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox txtReadCommand;
        private Button button2;
        private TextBox txtWriteCommand;
        private TextBox txtAddress;
        private TextBox txtValue;
        private TextBox txtReadResult;
        private Button btnWriteOneCommand;
    }
}
