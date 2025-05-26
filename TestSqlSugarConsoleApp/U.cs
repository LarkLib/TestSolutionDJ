using SqlSugar;
using System.Security.Principal;
using System.Text.RegularExpressions;

public class U
{
    [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
    public int ID { get; set; }
    [SugarColumn(ColumnName ="Name")]
    public string? NameA{ get; set; }
    public short Age { get; set; }
    //public short Class { get; set; }
}