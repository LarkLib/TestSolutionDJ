using Microsoft.EntityFrameworkCore;

namespace TestPostgresqlWebApplication
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext>
    options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<PersonInfo> PersonInfos { get; set; }
        public DbSet<SmartScale> SmartScales { get; set; }
    }
}