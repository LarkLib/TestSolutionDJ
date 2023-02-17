using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便动态设置托盘图标
    /// </summary>
    public class NotifyIconHelper
    {
        public enum Status
        {
            Offline,
            Online,
            TwinkleNotice
        }
        private NotifyIconHelper.Status status_0;
        private NotifyIcon notifyIcon_0;
        private Timer timer_0;
        private bool bool_0 = false;
        [CompilerGenerated]
        private Icon icon_0;
        [CompilerGenerated]
        private Icon icon_1;
        [CompilerGenerated]
        private string string_0;
        [CompilerGenerated]
        private string string_1;
        [CompilerGenerated]
        private Icon icon_2;
        [CompilerGenerated]
        private Icon icon_3;
        public Icon Icon_UnConntect
        {
            get;
            set;
        }
        public Icon Icon_Conntected
        {
            get;
            set;
        }
        public string Text_UnConntect
        {
            get;
            set;
        }
        public string Text_Conntected
        {
            get;
            set;
        }
        public Icon Icon_Shrink1
        {
            get;
            set;
        }
        public Icon Icon_Shrink2
        {
            get;
            set;
        }
        public NotifyIconHelper.Status NotifyStatus
        {
            get
            {
                return this.status_0;
            }
            set
            {
                if (value != this.status_0)
                {
                    if (this.status_0 == NotifyIconHelper.Status.TwinkleNotice && this.timer_0 != null)
                    {
                        this.timer_0.Stop();
                    }
                    this.status_0 = value;
                    this.Refresh();
                }
            }
        }
        public NotifyIconHelper(NotifyIcon notifyIcon)
        {
            this.notifyIcon_0 = notifyIcon;
            this.NotifyStatus = NotifyIconHelper.Status.Offline;
            this.timer_0 = new Timer();
            this.timer_0.Interval = 500;
            this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
        }
        public void Refresh()
        {
            switch (this.status_0)
            {
                case NotifyIconHelper.Status.Offline:
                    {
                        this.notifyIcon_0.Icon = this.Icon_UnConntect;
                        this.notifyIcon_0.Text = this.Text_UnConntect;
                        break;
                    }
                case NotifyIconHelper.Status.Online:
                    {
                        this.notifyIcon_0.Icon = this.Icon_Conntected;
                        this.notifyIcon_0.Text = this.Text_Conntected;
                        break;
                    }
                case NotifyIconHelper.Status.TwinkleNotice:
                    {
                        this.timer_0.Start();
                        break;
                    }
            }
        }
        private void timer_0_Tick(object sender, EventArgs e)
        {
            this.notifyIcon_0.Icon = (this.bool_0 ? this.Icon_Shrink1 : this.Icon_Shrink2);
            this.bool_0 = !this.bool_0;
        }
    }
}
