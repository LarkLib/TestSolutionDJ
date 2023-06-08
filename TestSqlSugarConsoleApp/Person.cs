using SqlSugar;
using System.Security.Principal;
using System.Text.RegularExpressions;

public class Person
{
    [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Address { get; set; }
    public static Person CreatePerson()
    {
        var random = new Random().Next(1,10000);
        return new Person()
        {
            Title = $"Mr. {random}",
            FirstName = $"Peter {random}",
            LastName = $"Pan {random}",
            DateOfBirth = DateTime.Now.AddDays(-random),
            Address = $"{random} Neverland 123N Funny Street 2 Neverwood"
        };
    }
}