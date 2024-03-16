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
            int length = buffer[4] * 256 + buffer[5] - 2;//��2����Ϊ����������ݰ����˵�Ԫ��ʶ���͹����룬ռ�����ֽ�
            if (length <= 0) return;
            byte[] buffer3 = new byte[length - 1];
            //5.3  ���˵�һ���ֽڣ���һ���ֽڴ������ݵ��ֽڸ�����
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
        /// ��ȡ��ȡ����˷������������Ϳ��Եõ�����19 B2 00 00 00 06 02 03 00 04 00 01�����������ģ�
        /// </summary>
        /// <param name="address">�Ĵ�����ʼ��ַ</param>
        /// <param name="stationNumber">վ��</param>
        /// <param name="functionCode">������</param>
        /// <param name="length">��ȡ����</param>
        /// <returns></returns>
        public static byte[] GetReadCommand(ushort address, byte stationNumber, byte functionCode, ushort length)
        {
            byte[] buffer = new byte[12];
            buffer[0] = 0x19;
            buffer[1] = 0xB2;//Client�����ļ�����Ϣ
            buffer[2] = 0x00;
            buffer[3] = 0x00;//��ʾtcp/ip ��Э���modbus��Э��
            buffer[4] = 0x00;
            buffer[5] = 0x06;//��ʾ���Ǹ��ֽ��Ժ���ֽڳ���

            buffer[6] = stationNumber;  //վ��
            buffer[7] = functionCode;   //������
            buffer[8] = BitConverter.GetBytes(address)[1];
            buffer[9] = BitConverter.GetBytes(address)[0];//�Ĵ�����ַ
            buffer[10] = BitConverter.GetBytes(length)[1];
            buffer[11] = BitConverter.GetBytes(length)[0];//��ʾrequest �Ĵ����ĳ���(�Ĵ�������)
            return buffer;
        }

        /// <summary>
        /// ��ȡд������
        /// </summary>
        /// <param name="address">�Ĵ�����ַ</param>
        /// <param name="values"></param>
        /// <param name="stationNumber">վ��</param>
        /// <param name="functionCode">������</param>
        /// <returns></returns>
        public static byte[] GetWriteCommand(ushort address, byte[] values, byte stationNumber, byte functionCode)
        {
            byte[] buffer = new byte[13 + values.Length];
            buffer[0] = 0x19;
            buffer[1] = 0xB2;//������Ϣ��������֤response�Ƿ�������           
            buffer[4] = BitConverter.GetBytes(7 + values.Length)[1];
            buffer[5] = BitConverter.GetBytes(7 + values.Length)[0];//��ʾ����header handle���滹�ж೤���ֽ�

            buffer[6] = stationNumber; //վ��
            buffer[7] = functionCode;  //������
            buffer[8] = BitConverter.GetBytes(address)[1];
            buffer[9] = BitConverter.GetBytes(address)[0];//�Ĵ�����ַ
            buffer[10] = (byte)(values.Length / 2 / 256);
            buffer[11] = (byte)(values.Length / 2 % 256);//д�Ĵ�������(��2��һ���Ĵ��������ֽڣ��Ĵ���16λ������256��byte���洢255��)              
            buffer[12] = (byte)(values.Length);          //д�ֽڵĸ���
            values.CopyTo(buffer, 13);                   //��Ŀ��ֵ���ӵ��������
            return buffer;
        }

        /// <summary>
        /// ��ȡд������
        /// </summary>
        /// <param name="address">�Ĵ�����ַ</param>
        /// <param name="values"></param>
        /// <param name="stationNumber">վ��</param>
        /// <param name="functionCode">������</param>
        /// <returns></returns>
        public static byte[] GetWriteOneCommand(ushort address, short value, byte stationNumber, byte functionCode = 0x06)
        {
            var values = BitConverter.GetBytes(value).Reverse().ToArray();
            byte[] buffer = new byte[12];
            buffer[0] = 0x19;
            buffer[1] = 0xB2;//������Ϣ��������֤response�Ƿ�������           

            buffer[4] = 0x00; //����
            buffer[5] = 0x06; //����
            buffer[6] = stationNumber; //վ��
            buffer[7] = functionCode;  //������
            buffer[8] = BitConverter.GetBytes(address)[1];
            buffer[9] = BitConverter.GetBytes(address)[0];//�Ĵ�����ַ
            buffer[10] = values[0];
            buffer[11] = values[1];             
            return buffer;
        }
    }
}
