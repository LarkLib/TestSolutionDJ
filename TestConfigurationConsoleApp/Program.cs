using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Runtime;

namespace TestConfigurationConsoleApp
{
    internal class Program
    {
        public static IConfiguration Configuration;

        static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection() //将配置文件的数据加载到内存中
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

            var mySettings = Configuration.GetSection("gen").Get<GenEntity>();//Entity类名与配置文件里的字段名匹配，不区分大小写
            var conn = Configuration.GetConnectionString("conn_db");

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //https://www.cnblogs.com/harrychinese/p/winform_DependencyInjection.html
            //在 appsettings.json 中存一些自定义的信息, 如何方便读取呢? 微软推荐的 Options 模式, 下面详细介绍.
            // 首先安装库:
            //Microsoft.Extensions.Options.ConfigurationExtensions 库, 为DI容器增加了从配置文件中实例化对象的能力, 即  serviceCollection.Configure<TOptions>(IConfiguration)
            //Microsoft.Extensions.Options 库, 提供以强类型的方式读取configuration文件, 这是.Net中首选的读取configuration文件方式.
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICounterAppService, CounterAppService>()
                .AddSingleton<IConfiguration>(Configuration)
                .AddOptions()
                .Configure<GenEntity>(Configuration.GetSection("gen"))
                .BuildServiceProvider();

            //using (var scope = serviceProvider.CreateScope())
            //{
            //    var foo = scope.ServiceProvider.GetRequiredService<CounterAppService>();
            //}

            var counter = serviceProvider.GetService<ICounterAppService>();
            var cont = counter?.Double(10);
            counter?.DisplayInjectionInfo();


            Console.WriteLine("Hello, World!");
        }
    }

    public class GenEntity
    {
        public string conn { get; set; }
        public int dbType { get; set; }
        public bool autoPre { get; set; }
        public string author { get; set; }
        public string tablePrefix { get; set; }
        public string vuePath { get; set; }
        public string oracle_db { get; set; }
        public string[] allowList { get; set; }
    }

    public interface ICounterAppService
    {
        int Double(int value);
        void DisplayInjectionInfo();
    }

    public class CounterAppService : ICounterAppService
    {
        private readonly IConfiguration _configuration;
        private GenEntity _appServiceOptions;
        public CounterAppService(IConfiguration configuration, IOptions<GenEntity> appServiceOptionsWrapper)
        {
            _configuration = configuration;
            _appServiceOptions = appServiceOptionsWrapper.Value;
        }

        public void DisplayInjectionInfo()
        {
            Console.WriteLine(_configuration.ToString());
            Console.WriteLine(_appServiceOptions.conn);
        }
        public int Double(int value)
        {
            return value * 2;
        }
    }

}