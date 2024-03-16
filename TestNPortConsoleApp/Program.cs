using System.Diagnostics.Contracts;
using System.Net.Sockets;
using System.Text;

namespace TestNPortConsoleApp
{
    internal class Program
    {
        private static int successful = 0;
        private static int fail = 0;

        static void Main(string[] args)
        {
            //GetBarCode("127.0.0.1", 4001);

            for (int i = 0; i < 100; i++)
            {
                Console.Write(i);
                Console.Write(": ");
                GetBarCode("10.160.10.105", 4001);
                Console.Write(i);
                Console.Write(": ");
                GetBarCode("10.160.10.105", 4002);

            }
            Console.WriteLine("successful:{0},fail:{1}", successful, fail);
            Console.ReadLine();
        }

        static void GetBarCode(string ip, int port)
        {
            TcpClient? client = null;
            NetworkStream? stream = null;
            try
            {
                var buffer = new byte[512];
                client = new TcpClient();
                client.ReceiveTimeout = 500;
                client.SendTimeout = 500;
                client.ConnectAsync(ip, port).Wait(500);
                stream = client.GetStream();
                stream.Write(new byte[] { 0x16, 0x54, 0x0d });
                var ret = stream.Read(buffer);
                var barcode = Encoding.Default.GetString(buffer, 0, ret);
                Console.WriteLine(Encoding.Default.GetString(buffer));
                successful++;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                fail++;
            }
            finally
            {
                if (stream != null) stream.Write(new byte[] { 0x16, 0x55, 0x0d }); ;
                if (client != null) client.Close();
            }
            return;
        }
    }
}
