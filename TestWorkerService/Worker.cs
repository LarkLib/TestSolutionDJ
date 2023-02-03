using System.Net.NetworkInformation;

namespace TestWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private int Status = 0;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        public Worker(IHostApplicationLifetime hostApplicationLifetime, ILogger<Worker> logger)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (Status > 0) break;
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var httpClient = new HttpClient();
                var client = new swaggerClient("https://localhost:44378/", httpClient);
                var result = await client.TodoitemsAllAsync();
                _logger.LogInformation("result count: {count}", result.Count);
                await Task.Delay(5000, stoppingToken);
            }
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            // ע��Ӧ��ֹͣǰ��Ҫ��ɵĲ���
            _hostApplicationLifetime.ApplicationStopping.Register(() =>
            {
                GetOffWork();
            });

            _logger.LogInformation("StartAsync��output from StartAsync");
            return base.StartAsync(cancellationToken);
        }


        private void GetOffWork()
        {
            Status = 1;
            _logger.LogInformation("GetOffWork at: {time}", DateTimeOffset.Now);
            _logger.LogInformation("TimeSpan.FromSeconds(5)).Wait(): {time}", DateTimeOffset.Now);

            //�ȴ��Ĺ����У�ExecuteAsync�е�������Ȼ��ִ�С�
            Task.Delay(TimeSpan.FromSeconds(5)).Wait();

            _logger.LogInformation("Close at: {time}", DateTimeOffset.Now);
        }
    }
}