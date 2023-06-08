using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Windows.Forms;
using TestSystemWinFormsApp;
using WebSocketSharp;

namespace TestSystemHotkeyWinFormsApp
{
    public partial class TestForm : Form
    {
        private TestWebsocketServer testWebsocketServer = new TestWebsocketServer();
        public TestForm()
        {
            InitializeComponent();
        }

        private const int WM_HOTKEY = 0x312; //窗口消息：热键
        private const int WM_CREATE = 0x1; //窗口消息：创建
        private const int WM_DESTROY = 0x2; //窗口消息：销毁

        private const int HotKeyID = 1; //热键ID（自定义）

        //protected override void WndProc(ref Message msg)
        //{
        //    base.WndProc(ref msg);
        //    switch (msg.Msg)
        //    {
        //        case WM_HOTKEY: //窗口消息：热键
        //            int tmpWParam = msg.WParam.ToInt32();
        //            if (tmpWParam == HotKeyID)
        //            {
        //                //System.Windows.Forms.SendKeys.Send("^v");
        //                //Key Code
        //                //SHIFT +
        //                //CTRL ^
        //                //ALT %
        //                //System.Windows.Forms.SendKeys.Send("+%");
        //                System.Windows.Forms.SendKeys.Send("{a 6}");
        //            }
        //            break;
        //        case WM_CREATE: //窗口消息：创建
        //            //SystemHotKey.RegHotKey(this.Handle, HotKeyID, SystemHotKey.KeyModifiers.None, Keys.F1);
        //            SystemHotKey.RegHotKey(this.Handle, HotKeyID, SystemHotKey.KeyModifiers.None, Keys.Enter);
        //            break;
        //        case WM_DESTROY: //窗口消息：销毁
        //            SystemHotKey.UnRegHotKey(this.Handle, HotKeyID); //销毁热键
        //            break;
        //        default:
        //            break;
        //    }
        //}

        private void CpuTemperatureButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = CpuTemperature.GetCpuTemperature().ToString();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = CpuTemperature.GetCpuTemperature().ToString();
        }

        private void wsServerButton_Click(object sender, EventArgs e)
        {
            testWebsocketServer.WebsocketServerMain();
            textBox1.Text = "WebsocketServerMain";
        }

        private void wsClientButton_Click(object sender, EventArgs e)
        {
            testWebsocketServer.WebsocketClineMain();
            textBox1.Text = "WebsocketClineMain";
        }

        private void wsServerStop_Click(object sender, EventArgs e)
        {
            testWebsocketServer.StopWebsocketServer();
            textBox1.Text = "StopWebsocketServer";
        }

        private void testOutputTodebugviewButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("这是Debugger.WriteLine输出的调试信息,Release模式下无效");//display in debugview,Release模式下无效
            Debugger.Log(0, null, "这是Debugger.Log输出的调试信息,Release模式下有效");
            Trace.WriteLine("这是Trace.WriteLine输出的调试信息,Release模式下有效");

            Console.WriteLine("haha456");//can't display in debugvie
        }

        private void testWebapplicationButton_Click(object sender, EventArgs e)
        {
        }
    }
}