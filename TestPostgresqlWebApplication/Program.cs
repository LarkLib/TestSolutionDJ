using Microsoft.EntityFrameworkCore;

namespace TestPostgresqlWebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

            var app = builder.Build();
            AddCustomerData(app);

            //app.MapGet("/", () => "Hello World!");
            app.MapGet("/", (HttpContext httpContext, AppDbContext context) => httpContext.Response.WriteAsync($"<b>Hello World</b></br>Customers:{context.Customers.Count()}"));

            app.Run();
        }

        static void AddCustomerData(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetService<AppDbContext>();

            if (db == null || db.Customers.Count() > 0) return;

            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
            //db.Database.Migrate();

            var customer1 = new Customer
            {
                CustomerID = "Customer ID 1",
                CustomerName = "Customer Name 1",
                CreateTime= DateTime.Now,
                
            };

            var customer2 = new Customer
            {
                CustomerID = "Customer ID 2",
                CustomerName = "Customer Name 2",
                CreateTime= DateTime.Now,
            };

            var customer3 = new Customer
            {
                CustomerID = "Customer ID 3",
                CustomerName = "Customer Name 3",
                CreateTime= DateTime.Now,
            };

            db.Customers.Add(customer1);
            db.Customers.Add(customer2);
            db.Customers.Add(customer3);

            db.SaveChanges();
        }

    }
}