using System.Data;
using System.Threading.Channels;

namespace TestChannelConsoleApp
{
    internal class Program
    {
        private static Channel<int> channel = Channel.CreateBounded<int>(100);
        async static Task Main(string[] args)
        {
            TestChannelThread();
            //await TestChannel();
            //await SingleProducerSingleConsumer();

            Console.WriteLine("Hello, World!");
        }

        private static void TestChannelThread()
        {
            var t1 = Task.Run(() => TestChannelWrite("w1"));
            var t2 = Task.Run(() => TestChannelRead("r1"));
            var t3 = Task.Run(() => TestChannelRead("r2"));
            Task.WaitAll(t1, t2, t3);
        }
        private static async Task TestChannelRead(string name)
        {
            //var channel = Channel.CreateBounded<string>(100);
            await Console.Out.WriteLineAsync("TestChannelRead");
            while (await channel.Reader.WaitToReadAsync())
            {
                //await Console.Out.WriteLineAsync("WaitToReadAsync");
                while (channel.Reader.TryRead(out var i))
                {
                    await Console.Out.WriteLineAsync($"{DateTime.Now.ToString("o")} {Thread.CurrentThread.ManagedThreadId:3} {name} R:{i}");
                    await Task.Delay(new Random().Next(300, 1000));
                }
            }
        }
        private static async Task TestChannelWrite(string name)
        {
            //var channel = Channel.CreateBounded<string>(100);
            Console.WriteLine("TestChannelWrite");
            for (var i = 0; i < 10; i++)
            {
                await Console.Out.WriteLineAsync($"{DateTime.Now.ToString("o")} {Thread.CurrentThread.ManagedThreadId:3} {name} W:{i}");
                await channel.Writer.WriteAsync(i);
                await Task.Delay(3000);
            }
            channel.Writer.Complete();
        }

        private static async Task TestChannel()
        {
            // 创建有限容量的channel 
            var channel = Channel.CreateBounded<string>(100);

            // 创建无限容量的channel 
            //var channel = Channel.CreateUnbounded<string>();

            //Channel 还提供了 TryWrite() 方法，如果写入数据失败时会返回 false，
            //WaitToWriteAsync() 方法会做非阻塞的等待，直到 Channel 允许写入新的数据时返回 true，
            //同样的 Channel 关闭后会返回 false
            await channel.Writer.WriteAsync("hello");

            var item = await channel.Reader.ReadAsync();

            await Console.Out.WriteLineAsync(item);
        }

        public static async Task SingleProducerSingleConsumer()
        {
            var channel = Channel.CreateUnbounded<string>();
            var reader = channel.Reader;
            for (int i = 0; i < 10; i++)
            {
                var message = $"{DateTime.Now.ToString("o")} Item:{i}";
                await Console.Out.WriteLineAsync($"Send: {message}");
                await channel.Writer.WriteAsync(message);
                await Task.Delay(1000);
            }

            while (await reader.WaitToReadAsync())
            {
                if (reader.TryRead(out var number))
                {
                    Console.WriteLine(number);
                }
            }
        }
    }
}