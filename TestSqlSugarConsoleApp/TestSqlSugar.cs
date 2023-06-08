using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SqlSugar;
using System.Runtime.InteropServices;

namespace TestSqlSugarConsoleApp;
class TestSqlSugar
{
    public static object db { get; private set; }

    public static void Execute()
    {
        ExecuteSqlServer();
        //ExecuteSqlite();
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

        Db.CodeFirst.InitTables(typeof(Person));//这样一个表就能成功创建了

        var person = Person.CreatePerson();
        Db.Insertable<Person>(person).ExecuteCommand();
        var personList = Db.Queryable<Person>();
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
}
