using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestClassLibrary;

namespace TestConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var testLibrary = new TestClass();
            var msg = testLibrary.ReciveData();
        }
    }
}
