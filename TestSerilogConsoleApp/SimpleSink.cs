using Serilog.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace TestSerilogConsoleApp;

public class SimpleSink : ILogEventSink
{
    private readonly IFormatProvider _formatProvider;

    public SimpleSink(IFormatProvider formatProvider)
    {
        _formatProvider = formatProvider;
    }

    public void Emit(LogEvent logEvent)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(logEvent.RenderMessage(_formatProvider));
        Console.ResetColor();

        //var message = logEvent.RenderMessage(_formatProvider);
        //Console.WriteLine(DateTimeOffset.Now.ToString() + " " + message);
    }
}
public static class SimpleSinkExtensions
{
    public static LoggerConfiguration SimpleSink(
              this LoggerSinkConfiguration loggerConfiguration,
              IFormatProvider formatProvider = null)
    {
        return loggerConfiguration.Sink(new SimpleSink(formatProvider));
    }
}