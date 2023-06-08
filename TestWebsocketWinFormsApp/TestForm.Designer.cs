namespace TestWebsocketWinFormsApp
{
    partial class TestForm
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
            startWebappButton = new Button();
            printerListButton = new Button();
            SuspendLayout();
            // 
            // startWebappButton
            // 
            startWebappButton.Location = new Point(26, 24);
            startWebappButton.Name = "startWebappButton";
            startWebappButton.Size = new Size(115, 23);
            startWebappButton.TabIndex = 0;
            startWebappButton.Text = "start wep app";
            startWebappButton.UseVisualStyleBackColor = true;
            startWebappButton.Click += startWebappButton_Click;
            // 
            // printerListButton
            // 
            printerListButton.Location = new Point(147, 25);
            printerListButton.Name = "printerListButton";
            printerListButton.Size = new Size(75, 23);
            printerListButton.TabIndex = 1;
            printerListButton.Text = "Printer list";
            printerListButton.UseVisualStyleBackColor = true;
            printerListButton.Click += printerListButton_Click;
            // 
            // TestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(printerListButton);
            Controls.Add(startWebappButton);
            Name = "TestForm";
            Text = "Form1";
            Load += TestForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button startWebappButton;
        private Button printerListButton;
    }
}