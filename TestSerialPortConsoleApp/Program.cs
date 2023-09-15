using System.IO.Ports;
using System.Management;
using System.Text;

namespace TestSerialPortConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //商品open后才开始接收数据

            //ReadSerialPort();
            //return;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PnPEntity where Name like '%(COM%'");
            foreach (ManagementObject mo in searcher.Get())
            {
                Console.WriteLine($"mo:{mo.ToString()}");
                var hardInfos = searcher.Get();
                foreach (var hardInfo in hardInfos)
                {
                    Console.WriteLine($"hardInfo:{hardInfo}");
                    //当 COM口 有效时，Status 为 OK
                    //当 COM口 被禁用，Status 为 Error
                    Console.WriteLine($"Status:{hardInfo.Properties["Status"].Value}");
                    foreach (var p in hardInfo.Properties)
                    {
                        Console.WriteLine($"property:{p.Name}={p.Value}");
                    }
                    if (hardInfo.Properties["Name"].Value != null)
                    {
                        string deviceName = hardInfo.Properties["Name"].Value.ToString();
                        if (string.IsNullOrEmpty(deviceName))
                            continue;
                        Console.WriteLine($"deviceName:{deviceName}");
                    }

                }
            }

            var list = SerialPort.GetPortNames();
            SerialPort mySerialPort = new SerialPort("COM3");

            mySerialPort.BaudRate = 115200;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = true;
            mySerialPort.Encoding = Encoding.UTF8;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            mySerialPort.Open();

            Console.WriteLine("Press any key to continue...");
            Console.WriteLine();
            Console.ReadKey();
            mySerialPort.Close();
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Console.WriteLine("Data Received:");
            Console.Write(indata);
        }

        private static string ReadSerialPort()
        {
            int intReadCount = 0;
            Byte[] charBuffer = new Byte[2000];
            string cartNo = "";
            SerialPort _serialPort = new SerialPort
            {
                PortName = "COM3",
                BaudRate = 115200,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One
            };
            try
            {
                if (!_serialPort.IsOpen)
                    _serialPort.Open();

                if (_serialPort.BytesToRead > 0)
                {
                    Thread.Sleep(200);
                    // 读取串口数据
                    intReadCount = _serialPort.Read(charBuffer, 0, _serialPort.BytesToRead);
                    cartNo = Encoding.UTF8.GetString(charBuffer).Trim().Substring(0, intReadCount);
                }
            }
            catch
            {
                cartNo = "";
            }
            if (_serialPort.IsOpen)
                _serialPort.DiscardInBuffer();

            //关闭串口设备
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
            }
            return cartNo;
        }

    }
}