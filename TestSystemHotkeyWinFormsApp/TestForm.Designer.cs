namespace TestSystemHotkeyWinFormsApp
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
            textBox1 = new TextBox();
            GetCpuTemperatureButton = new Button();
            wsServerButton = new Button();
            wsClientButton = new Button();
            wsServerStop = new Button();
            testOutputTodebugviewButton = new Button();
            testWebapplicationButton = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(51, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(436, 23);
            textBox1.TabIndex = 0;
            // 
            // GetCpuTemperatureButton
            // 
            GetCpuTemperatureButton.Location = new Point(509, 12);
            GetCpuTemperatureButton.Name = "GetCpuTemperatureButton";
            GetCpuTemperatureButton.Size = new Size(142, 23);
            GetCpuTemperatureButton.TabIndex = 1;
            GetCpuTemperatureButton.Text = "GetCpuTemperature";
            GetCpuTemperatureButton.UseVisualStyleBackColor = true;
            GetCpuTemperatureButton.Click += CpuTemperatureButton_Click;
            // 
            // wsServerButton
            // 
            wsServerButton.Location = new Point(51, 57);
            wsServerButton.Name = "wsServerButton";
            wsServerButton.Size = new Size(75, 23);
            wsServerButton.TabIndex = 2;
            wsServerButton.Text = "ws start";
            wsServerButton.UseVisualStyleBackColor = true;
            wsServerButton.Click += wsServerButton_Click;
            // 
            // wsClientButton
            // 
            wsClientButton.Location = new Point(246, 57);
            wsClientButton.Name = "wsClientButton";
            wsClientButton.Size = new Size(75, 23);
            wsClientButton.TabIndex = 3;
            wsClientButton.Text = "ws client";
            wsClientButton.UseVisualStyleBackColor = true;
            wsClientButton.Click += wsClientButton_Click;
            // 
            // wsServerStop
            // 
            wsServerStop.Location = new Point(146, 57);
            wsServerStop.Name = "wsServerStop";
            wsServerStop.Size = new Size(75, 23);
            wsServerStop.TabIndex = 4;
            wsServerStop.Text = "ws stop";
            wsServerStop.UseVisualStyleBackColor = true;
            wsServerStop.Click += wsServerStop_Click;
            // 
            // testOutputTodebugviewButton
            // 
            testOutputTodebugviewButton.Location = new Point(341, 57);
            testOutputTodebugviewButton.Name = "testOutputTodebugviewButton";
            testOutputTodebugviewButton.Size = new Size(146, 23);
            testOutputTodebugviewButton.TabIndex = 5;
            testOutputTodebugviewButton.Text = "output to debugview";
            testOutputTodebugviewButton.UseVisualStyleBackColor = true;
            testOutputTodebugviewButton.Click += testOutputTodebugviewButton_Click;
            // 
            // testWebapplicationButton
            // 
            testWebapplicationButton.Location = new Point(509, 57);
            testWebapplicationButton.Name = "testWebapplicationButton";
            testWebapplicationButton.Size = new Size(142, 23);
            testWebapplicationButton.TabIndex = 6;
            testWebapplicationButton.Text = "test web appliation";
            testWebapplicationButton.UseVisualStyleBackColor = true;
            testWebapplicationButton.Click += testWebapplicationButton_Click;
            // 
            // TestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(testWebapplicationButton);
            Controls.Add(testOutputTodebugviewButton);
            Controls.Add(wsServerStop);
            Controls.Add(wsClientButton);
            Controls.Add(wsServerButton);
            Controls.Add(GetCpuTemperatureButton);
            Controls.Add(textBox1);
            Name = "TestForm";
            Text = "Form1";
            Load += TestForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button GetCpuTemperatureButton;
        private Button wsServerButton;
        private Button wsClientButton;
        private Button wsServerStop;
        private Button testOutputTodebugviewButton;
        private Button testWebapplicationButton;
    }
}