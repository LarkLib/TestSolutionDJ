using System;
using System.Drawing;
using System.Windows.Forms;

namespace DJ.LMS.Utilities
{
    public class FullscreenHelper
    {
        private Form form_0 = null;
        private bool bool_0 = false;
        private Rectangle rectangle_0;
        private FormBorderStyle formBorderStyle_0;
        private FormWindowState formWindowState_0;
        /// <summary>    
        /// 设置窗体是否为全屏显示    
        /// </summary>
        public bool Fullscreen
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                if (this.bool_0 != value)
                {
                    this.bool_0 = value;
                    if (this.bool_0)
                    {
                        this.rectangle_0 = this.form_0.Bounds;
                        this.formBorderStyle_0 = this.form_0.FormBorderStyle;
                        this.formWindowState_0 = this.form_0.WindowState;
                        if (this.form_0.MainMenuStrip != null)
                        {
                            this.form_0.MainMenuStrip.Visible = false;
                        }
                        this.form_0.FormBorderStyle = FormBorderStyle.None;
                        this.form_0.Bounds = Screen.PrimaryScreen.Bounds;
                        this.form_0.WindowState = FormWindowState.Normal;
                    }
                    else
                    {
                        this.form_0.TopMost = false;
                        this.form_0.WindowState = this.formWindowState_0;
                        this.form_0.Bounds = this.rectangle_0;
                        this.form_0.FormBorderStyle = this.formBorderStyle_0;
                        if (this.form_0.MainMenuStrip != null)
                        {
                            this.form_0.MainMenuStrip.Visible = true;
                        }
                    }
                }
            }
        }
        /// <summary>    
        /// 构造函数，传入需要进行全屏操作的窗体    
        /// </summary>    
        /// <param name="form">需要进行全屏操作的窗体</param> 
        public FullscreenHelper(Form form)
        {
            this.form_0 = form;
        }
        /// <summary>    
        /// 全屏切换操作    
        /// </summary> 
        public void Toggle()
        {
            this.Fullscreen = !this.Fullscreen;
        }
    }
}
