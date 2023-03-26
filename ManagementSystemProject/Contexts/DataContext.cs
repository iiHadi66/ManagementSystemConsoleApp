using ManagementSystemProject.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace ManagementSystemProject.Contexts;

public class DataContext : DbContext
    {
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<MatterEntity> Matters { get; set; }


        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hadia\source\repos\ManagementSystem\ManagementSystemProject\Contexts\EFDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        }

    }