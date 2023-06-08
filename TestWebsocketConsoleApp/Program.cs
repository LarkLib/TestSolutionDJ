using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.WebSockets;

namespace TestWebsocketConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var builder = WebApplication.CreateBuilder();
            builder.WebHost.UseKestrel();
            builder.WebHost.UseUrls("http://localhost:5001");
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
            Console.ReadLine();
        }
        private static async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!receiveResult.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(
                    new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                    receiveResult.MessageType,
                    receiveResult.EndOfMessage,
                    CancellationToken.None);

                receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);
        }

    }
}