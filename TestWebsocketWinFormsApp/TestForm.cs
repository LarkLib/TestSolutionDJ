using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using System.Drawing.Printing;
using System.Collections.Specialized;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace TestWebsocketWinFormsApp
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
        }

        private void startWebappButton_Click(object sender, EventArgs e)
        {
            Task.Run(() => StartWebapplication());
        }

        private static void StartWebapplication()
        {
            var builder = WebApplication.CreateBuilder();
            builder.WebHost.UseKestrel();
            builder.WebHost.UseUrls("http://localhost:5000");
            builder.Services.AddControllers();
            var app = builder.Build();
            //app.UseRouting();

            var webSocketOptions = new WebSocketOptions
            {
                //KeepAliveInterval - How frequently to send "ping" frames to the client to ensure proxies keep the connection open. The default is two minutes.
                //AllowedOrigins - A list of allowed Origin header values for WebSocket requests. By default, all origins are allowed.For more information, see WebSocket origin restriction in this article.
                KeepAliveInterval = TimeSpan.FromMinutes(2)
            };
            //webSocketOptions.AllowedOrigins.Add("https://client.com");
            //webSocketOptions.AllowedOrigins.Add("https://www.client.com");

            app.UseWebSockets(webSocketOptions);
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        await Echo(webSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    }
                }
                else
                {
                    await next(context);
                }

            });
            app.MapGet("/", () => "Hello, world!");
            app.Run();
        }

        private static async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!receiveResult.CloseStatus.HasValue)
            {
                string message = Encoding.UTF8.GetString(buffer)?.Trim('\0');
                if (message.Equals("aaa"))
                {
                    buffer = Encoding.UTF8.GetBytes("bbb");
                    buffer=ObjectToByteArray(GetPrinters());
                }
                await webSocket.SendAsync(
                    //new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                    new ArraySegment<byte>(buffer, 0, buffer.Length),
                    receiveResult.MessageType,
                    receiveResult.EndOfMessage,
                    CancellationToken.None);

                buffer = new byte[1024 * 4];
                receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);
        }

        private void printerListButton_Click(object sender, EventArgs e)
        {
            foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
            {
                Console.WriteLine(sPrint);
            }
        }

        private static StringCollection GetPrinters()
        {
            var printerList = new StringCollection();
            foreach (var item in PrinterSettings.InstalledPrinters)
            {
                printerList.Add(item.ToString());
            }
            return printerList;
        }

        /// <summary>
        /// 将一个object对象序列化，返回一个byte[]
        /// </summary>
        /// <param name="obj">能序列化的对象</param>
        /// <returns></returns>
        public static byte[] ObjectToByteArray(Object obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }
    }
}