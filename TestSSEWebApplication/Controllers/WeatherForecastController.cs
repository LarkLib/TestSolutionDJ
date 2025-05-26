using Microsoft.AspNetCore.Mvc;

namespace TestSSEWebApplication.Controllers
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

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("stream")]
        public async Task StreamEventsAsync()
        {
            Response.Headers.Append("Content-Type", "text/event-stream");
            Response.Headers.Append("Cache-Control", "no-cache");
            Response.Headers.Append("Connection", "keep-alive");

            CancellationToken cancellationToken = HttpContext.RequestAborted;

            try
            {
                int messageCount = 0;
                while (!cancellationToken.IsCancellationRequested)
                {
                    await HttpContext.Response.WriteAsync($"data: Message {messageCount++}\n\n", cancellationToken);
                    await HttpContext.Response.Body.FlushAsync(cancellationToken);

                    // Wait for a short interval before sending the next message
                    await Task.Delay(1000, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                // Ignore cancellation exceptions
            }
        }
    }
}
