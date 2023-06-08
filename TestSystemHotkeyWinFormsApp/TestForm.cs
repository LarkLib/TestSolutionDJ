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

        private const int WM_HOTKEY = 0x312; //������Ϣ���ȼ�
        private const int WM_CREATE = 0x1; //������Ϣ������
        private const int WM_DESTROY = 0x2; //������Ϣ������

        private const int HotKeyID = 1; //�ȼ�ID���Զ��壩

        //protected override void WndProc(ref Message msg)
        //{
        //    base.WndProc(ref msg);
        //    switch (msg.Msg)
        //    {
        //        case WM_HOTKEY: //������Ϣ���ȼ�
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
        //        case WM_CREATE: //������Ϣ������
        //            //SystemHotKey.RegHotKey(this.Handle, HotKeyID, SystemHotKey.KeyModifiers.None, Keys.F1);
        //            SystemHotKey.RegHotKey(this.Handle, HotKeyID, SystemHotKey.KeyModifiers.None, Keys.Enter);
        //            break;
        //        case WM_DESTROY: //������Ϣ������
        //            SystemHotKey.UnRegHotKey(this.Handle, HotKeyID); //�����ȼ�
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
            Debug.WriteLine("����Debugger.WriteLine����ĵ�����Ϣ,Releaseģʽ����Ч");//display in debugview,Releaseģʽ����Ч
            Debugger.Log(0, null, "����Debugger.Log����ĵ�����Ϣ,Releaseģʽ����Ч");
            Trace.WriteLine("����Trace.WriteLine����ĵ�����Ϣ,Releaseģʽ����Ч");

            Console.WriteLine("haha456");//can't display in debugvie
        }

        private void testWebapplicationButton_Click(object sender, EventArgs e)
        {
        }
    }
}