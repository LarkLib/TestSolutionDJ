using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSqlSugarConsoleApp
{
    internal class Configuration
    {
        internal static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            return builder.Build();
        }
    }
}
