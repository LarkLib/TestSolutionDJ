using Mapster;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SqlSugar;
using System.Text.Json;
namespace TestSqlSugarConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestSqlSugar.Execute();
            //TestMapster.Execute();
            //var entry = System.Reflection.Assembly.GetEntryAssembly()?.EntryPoint?.DeclaringType?.Namespace ?? "(null)";
            //Console.WriteLine(System.Reflection.Assembly.GetTypes());
            //Console.WriteLine($"Application.ExecutablePath={Application.ExecutablePath}");
            Console.WriteLine("Namespace: {0}", System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Namespace ?? string.Empty);
            Console.WriteLine("Class: {0}", System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name ?? string.Empty);
            Console.WriteLine("Method: {0}", System.Reflection.MethodBase.GetCurrentMethod()?.Name ?? string.Empty);
            Console.WriteLine($"AppContext.BaseDirectory={AppContext.BaseDirectory}");

        }        
    }
}