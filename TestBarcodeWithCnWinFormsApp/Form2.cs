using DevExpress.CodeParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        private SerialPort _serialPort;             //串口设备
        private Int32 _intReadCount = 12;
        private Byte[] _charBuffer = new Byte[2000];

        public Form2()
        {
            InitializeComponent();

            timer1.Tick += Timer1_Tick;

            FormClosing += (s, e) =>
            {

                this.timer1.Stop();

                //关闭串口设备
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    _serialPort.Close();
                }

                this.Dispose();
                this.Close();
            };
        }

        #region ---定时处理条码扫描信息
        private void Timer1_Tick(object sender, EventArgs e)
        {
            var messageBuilder=new StringBuilder();
            messageBuilder.Append($"Timer1_Tick:{DateTime.Now.ToString("o")}");
            this.timer1.Stop();
            try
            {
                string barcode = ReadSerialPort();
                messageBuilder.Append($",    barcode:{barcode}");
                if (barcode.Length > 0)
                {
                    textBox1.Text = barcode;
                }
                listBox1.Items.Add( messageBuilder.ToString() );
            }
            catch (Exception ex)
            {
                //DevMessageUtil.ShowError(ex.Message);
            }
            finally
            {
                if (_serialPort.IsOpen)
                    _serialPort.DiscardInBuffer();

                this.timer1.Start();
            }
        }

        #endregion
        #region ---初始化串口设备
        private void InitSerialPort()
        {
            string port = "";

            _serialPort = new SerialPort
            {
                PortName = port.Length == 0 ? "COM3" : port,
                BaudRate = 115200,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One
            };
            this.timer1.Start();
        }
        #endregion
        #region ---读取串口数据
        /// <summary>
        /// 读取串口数据
        /// </summary>
        /// <returns></returns>
        private string ReadSerialPort()
        {
            string cartNo = "";

            try
            {
                if (!_serialPort.IsOpen)
                    _serialPort.Open();

                if (_serialPort.BytesToRead > 0)
                {
                    Thread.Sleep(200);
                    // 读取串口数据
                    this._intReadCount = this._serialPort.Read(this._charBuffer, 0, _serialPort.BytesToRead);
                    cartNo = Encoding.UTF8.GetString(this._charBuffer).Trim().Substring(0, _intReadCount);
                }
            }
            catch
            {
                cartNo = "";
            }

            return cartNo;
        }
        #endregion

        private void Form2_Load(object sender, EventArgs e)
        {
            InitSerialPort();
        }
    }
}
