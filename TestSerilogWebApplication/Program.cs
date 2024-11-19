using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System.Diagnostics;
using Wms.Web.Api.Service;
using Wms.Web.Api.Service.Filters;

namespace TestSerilogWebApplication
{
    public class Program
    {
        static long fileRolling = DateTimeOffset.Now.ToUnixTimeSeconds();
        public static void Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()//serilog step1
                                                      //.ReadFrom.Configuration(builder.Configuration)
                    .Enrich.FromLogContext()//The FromLogContext method in Serilog is used to enrich log events with properties from the ambient execution context. This can be useful for adding contextual information such as the HTTP request path, user identity, and any other contextual information you have added to the log context. It¡¯s very useful for tracing and understanding the flow of operations, especially when diagnosing issues.
                    .Enrich.With<LogFilePathEnricher>()//LogContext.PushProperty(LogFilePathEnricher.LogFilePathPropertyName, "Logs\\wms.txt");
                                                       //.Enrich.With<LocalTimeEnricher>()
                    //.Enrich.WithClientIp()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Override Microsoft logs
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning) // Override ASP.NET Core logs
                    .MinimumLevel.Debug()
                    //.WriteTo.Console(new RenderedCompactJsonFormatter())
                    .WriteTo.Console()
                    //.WriteTo.Async(a => a.File($"Logs/{fileRolling}-logs.txt", rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 1024 * 5))
                    //.WriteTo.File($"Logs/{fileRolling}-logs.txt", rollingInterval: RollingInterval.Day, fileSizeLimitBytes:10)
                    .WriteTo.Async(//The Serilog.Sinks.Async package can be used to wrap the file sink and perform all disk access on a background worker thread.
                    a => a.Map(
                        LogFilePathEnricher.LogFilePathPropertyName,
                        (logFilePath, wt) =>
                        wt.File(
                            //https://github.com/serilog/serilog-sinks-file
                            formatter: new CustomCompactJsonFormatter(),//To write events to the file in an alternative format such as JSON, pass an ITextFormatter as the first argument
                            path: $"{logFilePath}",//log file path, In XML and JSON configuration formats, environment variables can be used in setting values. This means, for instance, that the log file path can be based on TMP or APPDATA
                            shared: false,//Buffered writes are not available when file sharing is enabledTo enable multi-process shared log files, set shared to true
                            buffered: true,//Buffered writes are not available when file sharing is enabled By default, the file sink will flush each event written through it to disk. To improve write performance, specifying buffered: true will permit the underlying stream to buffer writes.
                            rollingInterval: RollingInterval.Day,//o create a log file per day or other time period
                            fileSizeLimitBytes: 1024 * 1024,//To avoid bringing down apps with runaway disk usage the file sink limits file size to 1GB by default. Once the limit is reached, no further events will be written until the next roll poin.The limit can be changed or removed using the fileSizeLimitBytes parameter. fileSizeLimitBytes: null
                            rollOnFileSizeLimit: true,//To roll when the file reaches fileSizeLimitBytes, specify rollOnFileSizeLimit
                            retainedFileCountLimit: 5,//For the same reason, only the most recent 31 files are retained by default (i.e. one long month). To change or remove this limit, pass the retainedFileCountLimit parameter.retainedFileCountLimit: null
                            flushToDiskInterval: TimeSpan.FromSeconds(1)//,// If you don¡¯t specify a value for flushToDiskInterval, Serilog will rely on the operating system¡¯s page cache to write the logs to disk2. This means that the logs are written to disk when the operating system decides it¡¯s appropriate, which can sometimes result in a delay2.
                            //outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                            ),
                        sinkMapCountLimit: 1))
                    .CreateLogger();
                /*
                The properties you¡¯re seeing in the JSON log entry are part of the Serilog event format. Here¡¯s what each of them means:
                @t: This represents the timestamp of the log event. It indicates when the event was logged.
                @mt: This stands for the message template. It¡¯s the template used to format the log message.
                @tr: This is the transaction ID or trace ID. It helps in tracking the flow of a request through different components of the system.
                @sp: This represents the span ID. It¡¯s used in distributed tracing to identify a specific span within a trace.
                These properties are useful for understanding the context and details of each log event. If you have any more questions or need further clarification, feel free to ask!

                Extensibility
                FileLifecycleHooks provide an extensibility point that allows hooking into different parts of the life cycle of a log file.
                You can create a hook by extending from FileLifecycleHooks and overriding the OnFileOpened and/or OnFileDeleting methods.
                OnFileOpened provides access to the underlying stream that log events are written to, before Serilog begins writing events. You can use this to write your own data to the stream (for example, to write a header row), or to wrap the stream in another stream (for example, to add buffering, compression or encryption)
                OnFileDeleting provides a means to work with obsolete rolling log files, before they are deleted by Serilog's retention mechanism - for example, to archive log files to another location
                Available hooks:
                serilog-sinks-file-header: writes a header to the start of each log file
                serilog-sinks-file-gzip: compresses logs as they are written, using streaming GZIP compression
                serilog-sinks-file-archive: archives obsolete rolling log files before they are deleted by Serilog's retention mechanism
                */

                var builder = WebApplication.CreateBuilder(args);
                builder.Host.UseSerilog();//serilog step2
                //builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());

                // Add services to the container.
                builder.Services.AddHealthChecks();

                builder.Services.AddControllers(options =>
                {
                    options.Filters.Add(typeof(ApiLogFilter));
                });

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                //app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                //app.UseSerilogRequestLogging();//serilog step3
                //app.UseSerilogRequestLogging(options =>
                //{
                //    //options.MessageTemplate = "Handled {RequestPath}";
                //    options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Fatal;
                //});

                app.UseMiddleware<ExceptionMiddleware>();
                app.MapHealthChecks("/health");

                app.Run();
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
    }

    internal class LogFilePathEnricher : ILogEventEnricher
    {
        private string _cachedLogFilePath;
        private LogEventProperty _cachedLogFilePathProperty;

        public const string LogFilePathPropertyName = "LogFilePath";

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var logFilePath = "Logs\\log.txt"; // Read path from your appsettings.json
                                               // Check for null, etc...

            LogEventProperty logFilePathProperty;

            if (logFilePath.Equals(_cachedLogFilePath))
            {
                // Path hasn't changed, so let's use the cached property
                logFilePathProperty = _cachedLogFilePathProperty;
            }
            else
            {
                // We've got a new path for the log. Let's create a new property
                // and cache it for future log events to use
                _cachedLogFilePath = logFilePath;

                _cachedLogFilePathProperty = logFilePathProperty =
                    propertyFactory.CreateProperty(LogFilePathPropertyName, logFilePath);
            }

            logEvent.AddPropertyIfAbsent(logFilePathProperty);
        }
    }

    //public class CustomCompactJsonFormatter : Serilog.Formatting.ITextFormatter//CompactJsonFormatter
    //{
    //    public void Format(LogEvent logEvent, TextWriter output)
    //    {
    //        output.Write("{\"@t\":\"");
    //        output.Write(logEvent.Timestamp.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"));
    //        output.Write("\",\"@mt\":\"");
    //        output.Write(logEvent.MessageTemplate.Text);
    //        output.Write("\"");

    //        foreach (var property in logEvent.Properties)
    //        {
    //            output.Write(",\"");
    //            output.Write(property.Key);
    //            output.Write("\":\"");
    //            output.Write(property.Value);
    //            output.Write("\"");
    //        }

    //        output.Write("}");
    //    }

    //    //protected override void WriteProperties(
    //    //    IReadOnlyDictionary<string, LogEventPropertyValue> properties,
    //    //    TextWriter output)
    //    //{
    //    //    // Customize the properties output here
    //    //    base.WriteProperties(properties, output);
    //    //}

    //    //protected override void WriteTimestamp(DateTimeOffset timestamp, TextWriter output)
    //    //{
    //    //    // Customize the timestamp format here
    //    //    output.Write("{\"@t\":\"");
    //    //    output.Write(timestamp.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"));
    //    //    output.Write("\"");
    //    //}
    //}

    //internal class LocalTimeEnricher : ILogEventEnricher
    //{
    //    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    //    {
    //        var localTimestamp = logEvent.Timestamp.ToLocalTime();
    //        logEvent.AddOrUpdateProperty(new LogEventProperty("@t", new ScalarValue(localTimestamp)));
    //    }
    //}
}
