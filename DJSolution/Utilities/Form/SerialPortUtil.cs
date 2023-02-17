using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来对串口进行读写操作.
    /// </summary>
    public class SerialPortUtil
    {
        public bool ReceiveEventFlag = false;
        public byte EndByte = 35;
        private DataReceivedEventHandler dataReceivedEventHandler_0;
        private SerialErrorReceivedEventHandler serialErrorReceivedEventHandler_0;
        private string string_0 = "COM1";
        private SerialPortBaudRates serialPortBaudRates_0 = SerialPortBaudRates.BaudRate_57600;
        private Parity parity_0 = Parity.None;
        private StopBits stopBits_0 = StopBits.One;
        private SerialPortDatabits serialPortDatabits_0 = SerialPortDatabits.EightBits;
        private SerialPort serialPort_0 = new SerialPort();
        /// <summary>
        /// 串口数据接收事件
        /// </summary>
        public event DataReceivedEventHandler DataReceived
        {
            add
            {
                DataReceivedEventHandler dataReceivedEventHandler = this.dataReceivedEventHandler_0;
                DataReceivedEventHandler dataReceivedEventHandler2;
                do
                {
                    dataReceivedEventHandler2 = dataReceivedEventHandler;
                    DataReceivedEventHandler value2 = (DataReceivedEventHandler)Delegate.Combine(dataReceivedEventHandler2, value);
                    dataReceivedEventHandler = Interlocked.CompareExchange<DataReceivedEventHandler>(ref this.dataReceivedEventHandler_0, value2, dataReceivedEventHandler2);
                }
                while (dataReceivedEventHandler != dataReceivedEventHandler2);
            }
            remove
            {
                DataReceivedEventHandler dataReceivedEventHandler = this.dataReceivedEventHandler_0;
                DataReceivedEventHandler dataReceivedEventHandler2;
                do
                {
                    dataReceivedEventHandler2 = dataReceivedEventHandler;
                    DataReceivedEventHandler value2 = (DataReceivedEventHandler)Delegate.Remove(dataReceivedEventHandler2, value);
                    dataReceivedEventHandler = Interlocked.CompareExchange<DataReceivedEventHandler>(ref this.dataReceivedEventHandler_0, value2, dataReceivedEventHandler2);
                }
                while (dataReceivedEventHandler != dataReceivedEventHandler2);
            }
        }
        public event SerialErrorReceivedEventHandler Error
        {
            add
            {
                SerialErrorReceivedEventHandler serialErrorReceivedEventHandler = this.serialErrorReceivedEventHandler_0;
                SerialErrorReceivedEventHandler serialErrorReceivedEventHandler2;
                do
                {
                    serialErrorReceivedEventHandler2 = serialErrorReceivedEventHandler;
                    SerialErrorReceivedEventHandler value2 = (SerialErrorReceivedEventHandler)Delegate.Combine(serialErrorReceivedEventHandler2, value);
                    serialErrorReceivedEventHandler = Interlocked.CompareExchange<SerialErrorReceivedEventHandler>(ref this.serialErrorReceivedEventHandler_0, value2, serialErrorReceivedEventHandler2);
                }
                while (serialErrorReceivedEventHandler != serialErrorReceivedEventHandler2);
            }
            remove
            {
                SerialErrorReceivedEventHandler serialErrorReceivedEventHandler = this.serialErrorReceivedEventHandler_0;
                SerialErrorReceivedEventHandler serialErrorReceivedEventHandler2;
                do
                {
                    serialErrorReceivedEventHandler2 = serialErrorReceivedEventHandler;
                    SerialErrorReceivedEventHandler value2 = (SerialErrorReceivedEventHandler)Delegate.Remove(serialErrorReceivedEventHandler2, value);
                    serialErrorReceivedEventHandler = Interlocked.CompareExchange<SerialErrorReceivedEventHandler>(ref this.serialErrorReceivedEventHandler_0, value2, serialErrorReceivedEventHandler2);
                }
                while (serialErrorReceivedEventHandler != serialErrorReceivedEventHandler2);
            }
        }
        /// <summary>
        /// 获取或设置串口号
        /// </summary>
        public string PortName
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
        /// <summary>
        /// 获取或设置串口波特率
        /// </summary>
        public SerialPortBaudRates BaudRate
        {
            get
            {
                return this.serialPortBaudRates_0;
            }
            set
            {
                this.serialPortBaudRates_0 = value;
            }
        }
        /// <summary>
        /// 获取或设置串口的奇偶校验
        /// </summary>
        public Parity Parity
        {
            get
            {
                return this.parity_0;
            }
            set
            {
                this.parity_0 = value;
            }
        }
        /// <summary>
        /// 获取或设置串口的数据位
        /// </summary>
        public SerialPortDatabits DataBits
        {
            get
            {
                return this.serialPortDatabits_0;
            }
            set
            {
                this.serialPortDatabits_0 = value;
            }
        }
        /// <summary>
        /// 获取或设置串口的停止位
        /// </summary>
        public StopBits StopBits
        {
            get
            {
                return this.stopBits_0;
            }
            set
            {
                this.stopBits_0 = value;
            }
        }
        /// <summary>
        /// 获取串口是否打开
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return this.serialPort_0.IsOpen;
            }
        }
        /// <summary>    
        /// 参数构造函数（使用枚举参数构造）    
        /// </summary>    
        /// <param name="baud">波特率</param>    
        /// <param name="par">奇偶校验位</param>    
        /// <param name="sBits">停止位</param>    
        /// <param name="dBits">数据位</param>    
        /// <param name="name">串口号</param> 
        public SerialPortUtil(string name, SerialPortBaudRates baud, Parity par, SerialPortDatabits dBits, StopBits sBits)
        {
            this.string_0 = name;
            this.serialPortBaudRates_0 = baud;
            this.parity_0 = par;
            this.serialPortDatabits_0 = dBits;
            this.stopBits_0 = sBits;
            this.serialPort_0.DataReceived += new SerialDataReceivedEventHandler(this.serialPort_0_DataReceived);
            this.serialPort_0.ErrorReceived += new SerialErrorReceivedEventHandler(this.serialPort_0_ErrorReceived);
        }
        /// <summary>    
        /// 参数构造函数（使用字符串参数构造）    
        /// </summary>    
        /// <param name="baud">波特率</param>    
        /// <param name="par">奇偶校验位</param>    
        /// <param name="sBits">停止位</param>    
        /// <param name="dBits">数据位</param>    
        /// <param name="name">串口号</param>
        public SerialPortUtil(string name, string baud, string par, string dBits, string sBits)
        {
            this.string_0 = name;
            this.serialPortBaudRates_0 = (SerialPortBaudRates)Enum.Parse(typeof(SerialPortBaudRates), baud);
            this.parity_0 = (Parity)Enum.Parse(typeof(Parity), par);
            this.serialPortDatabits_0 = (SerialPortDatabits)Enum.Parse(typeof(SerialPortDatabits), dBits);
            this.stopBits_0 = (StopBits)Enum.Parse(typeof(StopBits), sBits);
            this.serialPort_0.DataReceived += new SerialDataReceivedEventHandler(this.serialPort_0_DataReceived);
            this.serialPort_0.ErrorReceived += new SerialErrorReceivedEventHandler(this.serialPort_0_ErrorReceived);
        }
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SerialPortUtil()
        {
            this.string_0 = "COM1";
            this.serialPortBaudRates_0 = SerialPortBaudRates.BaudRate_9600;
            this.parity_0 = Parity.None;
            this.serialPortDatabits_0 = SerialPortDatabits.EightBits;
            this.stopBits_0 = StopBits.One;
            this.serialPort_0.DataReceived += new SerialDataReceivedEventHandler(this.serialPort_0_DataReceived);
            this.serialPort_0.ErrorReceived += new SerialErrorReceivedEventHandler(this.serialPort_0_ErrorReceived);
        }
        /// <summary>
        /// 打开串口
        /// </summary>
        public void OpenPort()
        {
            if (this.serialPort_0.IsOpen)
            {
                this.serialPort_0.Close();
            }
            this.serialPort_0.PortName = this.string_0;
            this.serialPort_0.BaudRate = (int)this.serialPortBaudRates_0;
            this.serialPort_0.Parity = this.parity_0;
            this.serialPort_0.DataBits = (int)this.serialPortDatabits_0;
            this.serialPort_0.StopBits = this.stopBits_0;
            this.serialPort_0.Open();
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void ClosePort()
        {
            if (this.serialPort_0.IsOpen)
            {
                this.serialPort_0.Close();
            }
        }
        /// <summary>
        /// 丢弃串口接收缓冲区和传输缓冲区中的数据
        /// </summary>
        public void DiscardBuffer()
        {
            this.serialPort_0.DiscardInBuffer();
            this.serialPort_0.DiscardOutBuffer();
        }
        private void serialPort_0_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!this.ReceiveEventFlag)
            {
                List<byte> list = new List<byte>();
                bool flag = false;
                while (this.serialPort_0.BytesToRead > 0 || !flag)
                {
                    byte[] array = new byte[this.serialPort_0.ReadBufferSize + 1];
                    int num = this.serialPort_0.Read(array, 0, this.serialPort_0.ReadBufferSize);
                    for (int i = 0; i < num; i++)
                    {
                        list.Add(array[i]);
                        if (array[i] == this.EndByte)
                        {
                            flag = true;
                        }
                    }
                }
                string @string = Encoding.Default.GetString(list.ToArray(), 0, list.Count);
                if (this.dataReceivedEventHandler_0 != null)
                {
                    this.dataReceivedEventHandler_0(new DataReceivedEventArgs(@string));
                }
            }
        }
        private void serialPort_0_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (this.serialErrorReceivedEventHandler_0 != null)
            {
                this.serialErrorReceivedEventHandler_0(sender, e);
            }
        }
        /// <summary>
        /// 将指定的字符串写入串口
        /// </summary>
        /// <param name="msg"></param>
        public void WriteData(string msg)
        {
            if (!this.serialPort_0.IsOpen)
            {
                this.serialPort_0.Open();
            }
            this.serialPort_0.Write(msg);
        }
        /// <summary>
        /// 使用缓冲区数据将指定数量的字节写入串口
        /// </summary>
        /// <param name="msg">要写入串口的字节数组</param>
        public void WriteData(byte[] msg)
        {
            if (!this.serialPort_0.IsOpen)
            {
                this.serialPort_0.Open();
            }
            this.serialPort_0.Write(msg, 0, msg.Length);
        }
        /// <summary>
        /// 使用缓冲区数据将指定数量的字节写入串口
        /// </summary>
        /// <param name="msg">要写入端口的数据的字节数组</param>
        /// <param name="offset">从零开始的字节偏量，从此处开始将字节复制到端口</param>
        /// <param name="count">要写入的字节数</param>
        public void WriteData(byte[] msg, int offset, int count)
        {
            if (!this.serialPort_0.IsOpen)
            {
                this.serialPort_0.Open();
            }
            this.serialPort_0.Write(msg, offset, count);
        }
        public int SendCommand(byte[] SendData, ref byte[] ReceiveData, int Overtime)
        {
            if (!this.serialPort_0.IsOpen)
            {
                this.serialPort_0.Open();
            }
            this.ReceiveEventFlag = true;
            this.serialPort_0.DiscardInBuffer();
            this.serialPort_0.Write(SendData, 0, SendData.Length);
            int num = 0;
            int result = 0;
            while (num++ < Overtime && this.serialPort_0.BytesToRead < ReceiveData.Length)
            {
                Thread.Sleep(1);
            }
            if (this.serialPort_0.BytesToRead >= ReceiveData.Length)
            {
                result = this.serialPort_0.Read(ReceiveData, 0, ReceiveData.Length);
            }
            this.ReceiveEventFlag = false;
            return result;
        }
        /// <summary>
        /// 获取所有端口号
        /// </summary>
        /// <returns></returns>
        public static string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }
        /// <summary>    
        /// 设置串口号    
        /// </summary>    
        /// <param name="obj"></param>  
        public static void SetPortNameValues(ComboBox obj)
        {
            obj.Items.Clear();
            string[] portNames = SerialPort.GetPortNames();
            for (int i = 0; i < portNames.Length; i++)
            {
                string item = portNames[i];
                obj.Items.Add(item);
            }
        }
        /// <summary>    
        /// 设置波特率    
        /// </summary>  
        public static void SetBauRateValues(ComboBox obj)
        {
            obj.Items.Clear();
            foreach (SerialPortBaudRates serialPortBaudRates in Enum.GetValues(typeof(SerialPortBaudRates)))
            {
                ComboBox.ObjectCollection arg_3F_0 = obj.Items;
                int num = (int)serialPortBaudRates;
                arg_3F_0.Add(num.ToString());
            }
        }
        /// <summary>    
        /// 设置数据位    
        /// </summary>
        public static void SetDataBitsValues(ComboBox obj)
        {
            obj.Items.Clear();
            foreach (SerialPortDatabits serialPortDatabits in Enum.GetValues(typeof(SerialPortDatabits)))
            {
                ComboBox.ObjectCollection arg_3F_0 = obj.Items;
                int num = (int)serialPortDatabits;
                arg_3F_0.Add(num.ToString());
            }
        }
        /// <summary>    
        /// 设置校验位列表    
        /// </summary> 
        public static void SetParityValues(ComboBox obj)
        {
            obj.Items.Clear();
            string[] names = Enum.GetNames(typeof(Parity));
            for (int i = 0; i < names.Length; i++)
            {
                string item = names[i];
                obj.Items.Add(item);
            }
        }
        /// <summary>    
        /// 设置停止位    
        /// </summary>
        public static void SetStopBitValues(ComboBox obj)
        {
            obj.Items.Clear();
            string[] names = Enum.GetNames(typeof(StopBits));
            for (int i = 0; i < names.Length; i++)
            {
                string item = names[i];
                obj.Items.Add(item);
            }
        }
        public static byte[] HexToByte(string msg)
        {
            msg = msg.Replace(" ", "");
            byte[] array = new byte[msg.Length / 2];
            for (int i = 0; i < msg.Length; i += 2)
            {
                array[i / 2] = Convert.ToByte(msg.Substring(i, 2), 16);
            }
            return array;
        }
        public static string ByteToHex(byte[] comByte)
        {
            StringBuilder stringBuilder = new StringBuilder(comByte.Length * 3);
            for (int i = 0; i < comByte.Length; i++)
            {
                byte value = comByte[i];
                stringBuilder.Append(Convert.ToString(value, 16).PadLeft(2, '0').PadRight(3, ' '));
            }
            return stringBuilder.ToString().ToUpper();
        }
        public static bool Exists(string port_name)
        {
            string[] portNames = SerialPort.GetPortNames();
            bool result;
            for (int i = 0; i < portNames.Length; i++)
            {
                string a = portNames[i];
                if (a == port_name)
                {
                    result = true;
                    return result;
                }
            }
            result = false;
            return result;
        }
        public static string Format(SerialPort port)
        {
            return string.Format("{0} ({1},{2},{3},{4},{5})", new object[]
			{
				port.PortName, 
				port.BaudRate, 
				port.DataBits, 
				port.StopBits, 
				port.Parity, 
				port.Handshake
			});
        }
    }
    /// <summary>    
    /// 串口波特率列表
    /// </summary> 
    public enum SerialPortBaudRates
    {
        BaudRate_75 = 75,
        BaudRate_110 = 110,
        BaudRate_150 = 150,
        BaudRate_300 = 300,
        BaudRate_600 = 600,
        BaudRate_1200 = 1200,
        BaudRate_2400 = 2400,
        BaudRate_4800 = 4800,
        BaudRate_9600 = 9600,
        BaudRate_14400 = 14400,
        BaudRate_19200 = 19200,
        BaudRate_28800 = 28800,
        BaudRate_38400 = 38400,
        BaudRate_56000 = 56000,
        BaudRate_57600 = 57600,
        BaudRate_115200 = 115200,
        BaudRate_128000 = 128000,
        BaudRate_230400 = 230400,
        BaudRate_256000 = 256000
    }

    /// <summary>    
    /// 串口数据位列表（5,6,7,8）    
    /// </summary>  
    public enum SerialPortDatabits
    {
        FiveBits = 5,
        SixBits,
        SeventBits,
        EightBits
    }
}
