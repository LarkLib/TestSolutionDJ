using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using System.Drawing;
using System.Drawing.Printing;

namespace TestSerilogWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var userID = 100;
            var itemCount = 200;
            LogContext.PushProperty(LogFilePathEnricher.LogFilePathPropertyName, "Logs\\wms.txt");
            _logger.LogInformation("Wms User {UserID} checked out {ItemCount} items", userID, itemCount);

            LogContext.PushProperty(LogFilePathEnricher.LogFilePathPropertyName, "Logs\\wcs.txt");
            _logger.LogInformation("Wcs User {UserID} checked out {ItemCount} items", userID, itemCount);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
             .ToArray();
        }

        private Font printFont;
        [HttpGet("TestPrint")]
        public bool TestPrint()
        {
            printFont = new Font("Arial", 10);
            var printerList = PrinterSettings.InstalledPrinters;
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = "NPI64DFF8 (HP LaserJet MFP M437-M443)";
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            pd.Print();
            return true;
        }
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            ev.Graphics.DrawString("line", printFont, Brushes.Black, 10, 10, new StringFormat());
        }
    }
}
