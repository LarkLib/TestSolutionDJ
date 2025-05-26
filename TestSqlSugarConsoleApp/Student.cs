using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSqlSugarConsoleApp
{
    [SugarTable("dbstudent")]//当和数据库名称不一样可以设置表别名 指定表明
    public class Student
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, IsOnlyIgnoreInsert = true)]//数据库是自增才配自增 
        public int Id { get; set; }
        public int? SchoolId { get; set; }
        [SugarColumn(ColumnName = "StudentName")]//数据库与实体不一样设置列名 
        public string Name { get; set; }
    }
}
