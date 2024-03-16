using System;
using System.Net.Sockets;
using System.Text;

namespace TestModbusWinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var address = ushort.Parse(txtAddress.Text);
            var commandBuffer = GetReadCommand(address, 1, 3, 1);
            var commandBuilder = new StringBuilder();
            foreach (var command in commandBuffer)
            {
                commandBuilder.Append($"{command:X2}");
            }
            txtReadCommand.Text = commandBuilder.ToString();

            var buffer = SendCommand(commandBuffer);
            int length = buffer[4] * 256 + buffer[5] - 2;//减2是因为这个长度数据包括了单元标识符和功能码，占两个字节
            if (length <= 0) return;
            byte[] buffer3 = new byte[length - 1];
            //5.3  过滤第一个字节（第一个字节代表数据的字节个数）
            Array.Copy(buffer, 9, buffer3, 0, buffer3.Length);
            var buffer3Reverse = buffer3.Reverse().ToArray();
            var value = BitConverter.ToInt16(buffer3Reverse, 0);
            txtReadResult.Text = value.ToString(); ;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            short value = short.Parse(txtValue.Text);
            var values = BitConverter.GetBytes(value).Reverse().ToArray();
            var address = ushort.Parse(txtAddress.Text);
            var commandBuffer = GetWriteCommand(address, values, 1, 0x10);
            var commandBuilder = new StringBuilder();
            foreach (var command in commandBuffer)
            {
                commandBuilder.Append($"{command:X2}");
            }
            txtWriteCommand.Text = commandBuilder.ToString();
            SendCommand(commandBuffer);
        }
        private void btnWriteOneCommand_Click(object sender, EventArgs e)
        {
            short value = short.Parse(txtValue.Text);
            var address = ushort.Parse(txtAddress.Text);
            var commandBuffer = GetWriteOneCommand(address, value, 1);
            var commandBuilder = new StringBuilder();
            foreach (var command in commandBuffer)
            {
                commandBuilder.Append($"{command:X2}");
            }
            txtWriteCommand.Text = commandBuilder.ToString();
            SendCommand(commandBuffer);
        }

        private static byte[] SendCommand(byte[] commandBuffer)
        {
            TcpClient? client = null;
            NetworkStream? stream = null;
            var buffer = new byte[512];
            try
            {
                client = new TcpClient();
                client.ReceiveTimeout = 500;
                client.SendTimeout = 500;
                client.ConnectAsync("200.200.200.101", 502).Wait(500);
                stream = client.GetStream();
                stream.Write(commandBuffer);
                var ret = stream.Read(buffer);
                return buffer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (client != null) client.Close();
            }
            return buffer;
        }

        /// <summary>
        /// 获取读取命令（此方法传入参数后就可以得到类似19 B2 00 00 00 06 02 03 00 04 00 01这样的请求报文）
        /// </summary>
        /// <param name="address">寄存器起始地址</param>
        /// <param name="stationNumber">站号</param>
        /// <param name="functionCode">功能码</param>
        /// <param name="length">读取长度</param>
        /// <returns></returns>
        public static byte[] GetReadCommand(ushort address, byte stationNumber, byte functionCode, ushort length)
        {
            byte[] buffer = new byte[12];
            buffer[0] = 0x19;
            buffer[1] = 0xB2;//Client发出的检验信息
            buffer[2] = 0x00;
            buffer[3] = 0x00;//表示tcp/ip 的协议的modbus的协议
            buffer[4] = 0x00;
            buffer[5] = 0x06;//表示的是该字节以后的字节长度

            buffer[6] = stationNumber;  //站号
            buffer[7] = functionCode;   //功能码
            buffer[8] = BitConverter.GetBytes(address)[1];
            buffer[9] = BitConverter.GetBytes(address)[0];//寄存器地址
            buffer[10] = BitConverter.GetBytes(length)[1];
            buffer[11] = BitConverter.GetBytes(length)[0];//表示request 寄存器的长度(寄存器个数)
            return buffer;
        }

        /// <summary>
        /// 获取写入命令
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <param name="values"></param>
        /// <param name="stationNumber">站号</param>
        /// <param name="functionCode">功能码</param>
        /// <returns></returns>
        public static byte[] GetWriteCommand(ushort address, byte[] values, byte stationNumber, byte functionCode)
        {
            byte[] buffer = new byte[13 + values.Length];
            buffer[0] = 0x19;
            buffer[1] = 0xB2;//检验信息，用来验证response是否串数据了           
            buffer[4] = BitConverter.GetBytes(7 + values.Length)[1];
            buffer[5] = BitConverter.GetBytes(7 + values.Length)[0];//表示的是header handle后面还有多长的字节

            buffer[6] = stationNumber; //站号
            buffer[7] = functionCode;  //功能码
            buffer[8] = BitConverter.GetBytes(address)[1];
            buffer[9] = BitConverter.GetBytes(address)[0];//寄存器地址
            buffer[10] = (byte)(values.Length / 2 / 256);
            buffer[11] = (byte)(values.Length / 2 % 256);//写寄存器数量(除2是一个寄存器两个字节，寄存器16位。除以256是byte最大存储255。)              
            buffer[12] = (byte)(values.Length);          //写字节的个数
            values.CopyTo(buffer, 13);                   //把目标值附加到数组后面
            return buffer;
        }

        /// <summary>
        /// 获取写入命令
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <param name="values"></param>
        /// <param name="stationNumber">站号</param>
        /// <param name="functionCode">功能码</param>
        /// <returns></returns>
        public static byte[] GetWriteOneCommand(ushort address, short value, byte stationNumber, byte functionCode = 0x06)
        {
            var values = BitConverter.GetBytes(value).Reverse().ToArray();
            byte[] buffer = new byte[12];
            buffer[0] = 0x19;
            buffer[1] = 0xB2;//检验信息，用来验证response是否串数据了           

            buffer[4] = 0x00; //长度
            buffer[5] = 0x06; //长度
            buffer[6] = stationNumber; //站号
            buffer[7] = functionCode;  //功能码
            buffer[8] = BitConverter.GetBytes(address)[1];
            buffer[9] = BitConverter.GetBytes(address)[0];//寄存器地址
            buffer[10] = values[0];
            buffer[11] = values[1];             
            return buffer;
        }
    }
}
