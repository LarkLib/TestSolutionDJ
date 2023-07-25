using Serilog;
using System.Net.Sockets;

namespace TestSerilogConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestCase.Execute();
            Console.WriteLine("Hello, World!");
        }
    }

    class TestCase
    {
        static public void Execute()
        {
            using var log = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.SimpleSink()
                .WriteTo.Udp("192.168.89.130", 2345, AddressFamily.InterNetwork)
                .CreateLogger();
            log.Information("Hello, Serilog!");
            log.Debug("Debug info");

            var position = new { Latitude = 25, Longitude = 134 };
            var elapsedMs = 34;

            log.Information("Processed {@Position} in {Elapsed:000} ms.", position, elapsedMs);
            var unknown = new[] { 1, 2, 3 };
            Log.Information("Received {$Data}", unknown);
            log.Error(new ArgumentNullException("ServerName"), "Exceptionser:");
            log.Warning("Goodbye, Serilog.");
        }
    }
}
