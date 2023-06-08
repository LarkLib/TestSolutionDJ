using WebSocketSharp;
using WebSocketSharp.Server;

namespace TestSystemWinFormsApp
{
    internal class TestWebsocketServer
    {
        private WebSocketServer wssv = new WebSocketServer("ws://localhost:8030");
        private WebSocket ws = new WebSocket("ws://localhost:8030/Laputa");
        public void WebsocketServerMain()
        {

            wssv.AddWebSocketService<Laputa>("/Laputa");
            wssv.Start();
            //Console.ReadKey(true);
            //Thread.Sleep(60000);
            //wssv.Stop();
        }

        public void WebsocketClineMain()
        {
            //using (var ws = new WebSocket("ws://localhost:8030/Laputa"))
           //{
                ws.OnMessage += (sender, e) =>
                                  Console.WriteLine("Laputa says: " + e.Data);

                ws.Connect();
                ws.Send("BALUS");
                //Console.ReadKey(true);
            //}
        }

        public void StopWebsocketServer()
        {
            wssv.Stop();
        }
    }

    internal class Laputa : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = e.Data == "BALUS"
                      ? "Are you kidding?"
                      : "I'm not available now.";

            Send(msg);
        }
    }
}


