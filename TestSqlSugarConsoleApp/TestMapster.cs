using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSqlSugarConsoleApp
{
    internal class TestMapster
    {
        public static void Execute()
        {
            Console.WriteLine("Hello, World!");
            var person = Person.CreatePerson();
            PersonDto personDto = person.Adapt<PersonDto>();
            Console.Write(personDto);
        }
    }
}
