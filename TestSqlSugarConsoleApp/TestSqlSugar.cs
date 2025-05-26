using Mapster;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SqlSugar;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestSqlSugarConsoleApp;
class TestSqlSugar
{
    public static object db { get; private set; }

    public static void Execute()
    {
        ExecuteSqlServer();
        //ExecuteSqlite();
        //ExecuteIoc();
        //ExecutePostgres();
    }
    private static void ExecuteSqlite()
    {
        SqlSugarClient Db = new SqlSugarClient(new ConnectionConfig()
        {
            ConnectionString = $"Data Source={AppContext.BaseDirectory}Application.db;Cache=Shared",
            DbType = SqlSugar.DbType.Sqlite,  //数据库类型
                                              //IsShardSameThread = true,  //共享数据库连接对象
            IsAutoCloseConnection = true  //自动关闭数据库连接 对数据库操作完自动关闭 Close
        },
        db =>
        {
            //5.1.3.24统一了语法和SqlSugarScope一样，老版本AOP可以写外面
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql);//输出sql,查看执行sql 性能无影响

                //获取原生SQL推荐 5.1.4.63  性能OK
                //UtilMethods.GetNativeSql(sql,pars)

                //获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
                //UtilMethods.GetSqlString(DbType.SqlServer,sql,pars)

            };
            //注意多租户 有几个设置几个
            //db.GetConnection(i).Aop            
        });
        //var allPersonList = Db.Queryable<Person>().ToList();
        //var first = allPersonList.First();
        //first.FirstName = "fff";
        //allPersonList.Remove(Person.CreatePerson());
        //allPersonList.Add(allPersonList[2]);
        //var x = Db.Storageable(allPersonList);

        Db.CodeFirst.InitTables(typeof(Person));//这样一个表就能成功创建了

        var person = Person.CreatePerson();
        Db.Insertable<Person>(person).ExecuteCommand();
        var personList = Db.Queryable<Person>().Where(p => p.Title.Equals("Mr. 7362"));//hit
        personList = Db.Queryable<Person>().Where(p => p.Title.Equals("mr. 7362", StringComparison.InvariantCultureIgnoreCase));//none
        personList = Db.Queryable<Person>().Where(p => p.Title.Contains("Mr. 736"));//hit
        personList = Db.Queryable<Person>().Where("Title like 'Mr. 736'");//none
        Console.WriteLine(personList.ToJson());

        var sql = "select * from person";
        var list = Db.Ado.SqlQuery<Person>(sql);
        list.ForEach(e => Console.WriteLine(e.ToString()));

        var dt = Db.Ado.GetDataTable(sql);
        var qr = Db.Ado.SqlQuery<Person>(sql);
        List<dynamic> dyt = Db.Ado.SqlQuery<dynamic>(sql);
        var er = Db.Ado.ExecuteCommand(sql);

        var dtp = Db.Ado.GetDataTable("select * from person where LastName=@lastname and Title like @title",
            new { lastname = "Pan", title = "%M%" });
        Console.WriteLine("Count:", dtp.Rows.Count);

        var list2 = Db.Queryable<dynamic>().AS("person").Select("Title,LastName").Where("Title like @title", new { title = "%M%" });
        Console.WriteLine(list2.ToJson());
        return;
        //Db.Ado.SqlQuery("", "");
    }

    private static void ExecuteSqlServer()
    {
        SqlSugarClient Db = new SqlSugarClient(new ConnectionConfig()
        {
            ConnectionString = Configuration.GetConfig().GetConnectionString("conn_db"),
            DbType = (DbType)Convert.ToInt32(Configuration.GetConfig().GetConnectionString("conn_db_type")),
            IsAutoCloseConnection = true
        },
       db =>
       {
           //5.1.3.24统一了语法和SqlSugarScope一样，老版本AOP可以写外面

           db.Aop.OnLogExecuting = (sql, pars) =>
           {
               Console.WriteLine(sql);//输出sql,查看执行sql 性能无影响


               //获取原生SQL推荐 5.1.4.63  性能OK
               //UtilMethods.GetNativeSql(sql,pars)

               //获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
               //UtilMethods.GetSqlString(DbType.SqlServer,sql,pars)


           };

           //注意多租户 有几个设置几个
           //db.GetConnection(i).Aop

       });
        Db.CodeFirst.InitTables(typeof(Student));
        var dtStudent = Db.Queryable<Student>().ToDataTable();
        //dtStudent.Columns.RemoveAt(0);
        dtStudent.Rows[0]["Id"] = 0;
        dtStudent.Rows[0]["StudentName"] = StringExtensions.GenerateRandomString(5); //"dd";
        //var updateView = new System.Data.DataView(dtStudent);
        //var result = Db.Fastest<System.Data.DataView>().AS("dbstudent").BulkUpdate(updateView.ToTable(), new[] { "SchoolId" });
        //var result = Db.Fastest<System.Data.DataView>().AS("dbstudent").BulkMerge(dtStudent, new[] { "SchoolId" }, new[] { "StudentName" }, false);
        var studentList = Db.Utilities.DataTableToList<Student>(dtStudent);

        var id = SnowFlakeSingle.Instance.NextId();//也可以在程序中直接获取ID

        var sql = @"
insert into u (name,age) values('c33',31);
insert into u (name,age) values('c33',32);
insert into u (name,age) values('c33',33);
insert into u (id,name,age) values(1,'c33',23);
";
        Db.Ado.BeginTran();
        try
        {
            Db.Ado.ExecuteCommand(sql);
            Db.Ado.CommitTran();
        }
        catch (Exception)
        {
            Db.Ado.RollbackTran();
            //Db.Ado.CommitTran();
            //throw;
        }


        var uTable = Db.Ado.GetDataTable("select * from u where Name like '%33'");
        if (uTable != null || uTable.Rows.Count > 0)
        {
            uTable.TableName = "u";
            var row = uTable.Rows[0];
            row[2] = 21;
            var newrow = uTable.NewRow();
            newrow[1] = "c55";
            newrow[2] = 22;
            uTable.Rows.Add(newrow);
            //Db.StorageableByObject(row);
            //Db.Storageable(new List<string> { }).TranLock();
            //你也可以拿到更新哪几条和插入哪几条
            //var insertList = x.InsertList.Select(z => z.Item).ToList();
            //var updateList = x.UpdateList.Select(z => z.Item).ToList();
            //Db.StorageableByObject(new object()).ExecuteCommand();
            var x = Db.Storageable(uTable).WhereColumns("ID").ToStorage();//.SplitInsert(it=>it.Any()).SplitUpdate(it=>it.Any());
            var updateList = x.DataTableGroups[0].DataTable;
            var insertList = x.DataTableGroups[1].DataTable;
            x.AsInsertable.IgnoreColumns(new[] { "ID" }).ExecuteCommand();//不存在插入
            x.AsUpdateable.ExecuteCommand();//存在更新

            var x2 = Db.Storageable(uTable).WhereColumns("ID").SplitInsert(item => !item.Any());
            //var s=result.ToSqlValue();
        }
        Db.DbMaintenance.GetDataBaseList().ForEach(data => Console.WriteLine(data));

        foreach (var col in Db.DbMaintenance.GetColumnInfosByTableName("configurationTableField"))
        {
            Db.MappingColumns.Add(col.DbColumnName /*类的属性大写*/, col.DbColumnName, "configurationTableField");
        }
        Db.DbFirst
            //.IsCreateAttribute() //生成带有SqlSugar特性的实体
            .FormatFileName(x => x.ToUpperInvariant())//格式化文件名
            .StringNullable() //.NET 7 字符串是否需要?设置
                              //.IsCreateDefaultValue() //生成实体带有默认值
            .CreateClassFile(Directory.GetCurrentDirectory(), "Models");
        var list = Db.Queryable<dynamic>().AS("configurationTableField").Select("id,tableName").Where("id=@id", new { id = 1 });
        //Console.WriteLine(JsonConvert.SerializeObject(list));
        Console.WriteLine(list.ToJson());

        //var nameP = new SugarParameter("@name", "张三", typeof(string), ParameterDirection.ReturnValue);
        var dt = Db.Ado.UseStoredProcedure().GetDataTable("P_Project_GetWcsTaskList", new { taskType = 2 });
        Console.WriteLine("dt.Rows.Count={0}", dt.Rows.Count);
        Console.WriteLine(JsonConvert.SerializeObject(dt));

        var taskType = new SugarParameter("@taskType", "2");
        var message = new SugarParameter("@message", null, true);//设置为output
        var ret = new SugarParameter("@ret", "", typeof(string), System.Data.ParameterDirection.ReturnValue);
        var dt2 = Db.Ado.UseStoredProcedure().GetDataTable("P_Project_GetWcsTaskList", taskType, message, ret);
        Console.WriteLine("message={0},ret={1}", message.Value, ret.Value);
        //Console.WriteLine(JsonConvert.SerializeObject(dt2));

        JsonClient jsonToSqlClient = new JsonClient();
        jsonToSqlClient.Context = Db;
        var json = """
        {
            "Table":"configurationTableField",
            Select: [ [{ SqlFunc_AggregateMin: ["id"]},"id"], [{ SqlFunc_GetDate: []},"Date"] ]
        }
        """;
        var jsonRet = jsonToSqlClient.Queryable(json).ToSql();


        var conModels = new List<IConditionalModel>();
        conModels.Add(new ConditionalModel { FieldName = "id", ConditionalType = ConditionalType.Equal, FieldValue = "1" });
        var student = Db.Queryable<dynamic>().AS("configurationTableField").Where(conModels).ToList();

        //像PgSql Oracle类型不匹配就会报错，高版本支持设置 CSharpTypeName  来指定参数类型
        // { "FieldName":"id","ConditionalType":"0","FieldValue":"1","CSharpTypeName":"int" } 

        json = """
             [
                { "FieldName":"id","ConditionalType":"3","FieldValue":"10"},
                { "FieldName":"control","ConditionalType":"0","FieldValue":"TextBox"}
             ]
             """;
        var conModels2 = Db.Utilities.JsonToConditionalModels(json);
        var student2 = Db.Queryable<dynamic>().AS("configurationTableField").Where(conModels2).ToList();

        var dtp = Db.Ado.GetDataTable("select * from configurationTableField where control=@control and id >= @id",
            new { control = "TextBox", id = 10 });
        Console.WriteLine("Count:", dtp.Rows.Count);
        var dtp2 = Db.Ado.GetDataTable("select * from configurationTableField");

        //var sqlObj =Db.Queryable<dynamic>().SqlBuilder.ConditionalModelToSql(conModels2, 0);
        var list5 = Db.SqlQueryable<dynamic>("select dictValue,dictLabel from sys_dict_type t join sys_dict_data d on t.dictType=d.dictType where dictId<5");//.Where("dictId<5");
        var list3 = Db.Queryable<dynamic>().AS("v_Base_inventoryList").Where("F_StoreCell=@id", new { id = 10001 }).ToList();//没实体一样用

        var name = new SugarParameter("@name", "haha", System.Data.DbType.AnsiString);
    }

    private static void ExecuteIoc()
    {
        SqlSugar.IOC.SugarIocServices.AddSqlSugar(new SqlSugar.IOC.IocConfig()
        {
            ConnectionString = Configuration.GetConfig().GetConnectionString("conn_db"),
            DbType = SqlSugar.IOC.IocDbType.SqlServer,
            IsAutoCloseConnection = true//自动释放
        });
        //注入后就能所有地方使用
        var list5 = SqlSugar.IOC.DbScoped.SugarScope.Queryable<dynamic>().AS("v_Base_inventoryList").Where("F_StoreCell=@id", new { id = 10001 }).ToList();
    }

    private static void ExecutePostgres()
    {
        SqlSugarClient Db = new SqlSugarClient(new ConnectionConfig()
        {
            ConnectionString = Configuration.GetConfig().GetConnectionString("conn_db_postgres"),
            DbType = (DbType)Convert.ToInt32(Configuration.GetConfig().GetConnectionString("conn_db_type_postgres")),
            IsAutoCloseConnection = true
        },
        db =>
        {
            //5.1.3.24统一了语法和SqlSugarScope一样，老版本AOP可以写外面

            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //Console.WriteLine(sql);//输出sql,查看执行sql 性能无影响


                //获取原生SQL推荐 5.1.4.63  性能OK
                //UtilMethods.GetNativeSql(sql,pars)

                //获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
                //UtilMethods.GetSqlString(DbType.SqlServer,sql,pars)


            };

            //注意多租户 有几个设置几个
            //db.GetConnection(i).Aop

        });
        Db.DbMaintenance.GetDataBaseList().ForEach(data => Console.WriteLine(data));
        Db.CodeFirst.InitTables(typeof(Person));//根据types创建表
        Db.Insertable(Person.CreatePerson()).ExecuteCommand();
        var persons = Db.Queryable<Person>().Where((p) => !string.IsNullOrEmpty(p.Title) && p.Title.StartsWith("Mr"));//case sensitive
        persons.ForEach(person => Console.WriteLine(person.Id));
        Console.WriteLine(persons.ToJson());
    }
}

public static class StringExtensions
{
    /// <summary>
    /// Generates a random string of the specified length.
    /// </summary>
    /// <param name="length">The length of the random string to generate.</param>
    /// <param name="allowedChars">Optional: A string containing the characters allowed in the random string.  Defaults to alphanumeric characters.</param>
    /// <returns>A random string of the specified length.</returns>
    public static string GenerateRandomString(int length, string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
    {
        if (length < 0)
        {
            throw new ArgumentException("Length must be non-negative.", nameof(length));
        }

        if (string.IsNullOrEmpty(allowedChars))
        {
            throw new ArgumentException("Allowed characters cannot be null or empty.", nameof(allowedChars));
        }

        Random random = new Random(); // Consider using a thread-safe Random for high-concurrency scenarios
        return new string(Enumerable.Repeat(allowedChars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}

