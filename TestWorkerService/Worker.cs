namespace TestWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var httpClient = new HttpClient();
                var client = new swaggerClient("https://localhost:44378/", httpClient);
                var result = await client.TodoitemsAllAsync();
                _logger.LogInformation("result count: {count}", result.Count);
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}