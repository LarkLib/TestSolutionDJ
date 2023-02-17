using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DJ.LMS.Utilities;
using DJ.LMS.WinForms.DianJin;

namespace DJ.LMS.WinForms
{
    static class EntryPoint
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool isOnlyOne;
            System.Threading.Mutex mtx = new System.Threading.Mutex(false, "OnlyOne", out isOnlyOne);
            if (isOnlyOne)
            {
                using (FrmLogin form = new FrmLogin())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        Application.Run(FrmMain.Instance);
                        //Application.Run(new FrmP300Main());
                        //Application.Run(new TestForm2());
                        //Application.Run(new TestFormTreeList());
                        //Application.Run(new TestFormGrid());
                    }
                }
                //Application.Run(new FrmUserEdit());
            }
            else
            {
                MessageUtil.ShowTips("应用程序已启动.");
            }
            //using (FrmLogin form = new FrmLogin())
            //{
            //    if (form.ShowDialog() == DialogResult.OK)
            //    {
            //        Application.Run(FrmMain.Instance);
            //    }
            //}
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string msg = "";
            Exception err = e.Exception as Exception;

            if (err != null)
                msg = string.Format("应用程序异常\n异常类型：{0}\n异常信息：{1}\n异常位置：{2}\n",
                     err.GetType().Name, err.Message, err.StackTrace);
            else
                msg = string.Format("应用程序执行序列错误:{0}", e);

            MessageUtil.ShowError(msg);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string msg = "";
            Exception error = e.ExceptionObject as Exception;
            if (error != null)
                msg = string.Format("Application UnhandledException:{0};\n非执行序列错误:{1}", error.Message, error.StackTrace);
            else
                msg = string.Format("Application UnhandledError:{0}", e);
            MessageUtil.ShowError(msg);
        }
    }
}
