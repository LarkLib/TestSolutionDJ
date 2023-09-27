using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace TestConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            TestJson();
            Console.ReadLine();
        }

        private static void TestJson()
        {
            var json = @"
{
    'expires_in': 60,
    'refresh_expires_in': 1800,
    'token_type': 'Bearer',
    'not-before-policy': 0,
    'session_state': '6d4f61e5-d3df-4bab-a06d-e6742bed7a46',
    'scope': 'email profile',
	'task':{ 'id':100,'name':'task_001','statous':'disable'}
}
            ";
            var root = JsonConvert.DeserializeObject<Rootobject>(json);
            Console.WriteLine(root);
            Console.WriteLine("Hello, World!");
        }
    }

    public class Rootobject
    {
        public int expires_in { get; set; }
        public int refresh_expires_in { get; set; }
        public string token_type { get; set; }
        public int notbeforepolicy { get; set; }
        public string session_state { get; set; }
        public string scope { get; set; }
        public Task task { get; set; }
    }

    public class Task
    {
        public int id { get; set; }
        public string name { get; set; }
        public string statous { get; set; }
    }
}
