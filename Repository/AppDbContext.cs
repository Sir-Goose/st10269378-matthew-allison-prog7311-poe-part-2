using Microsoft.EntityFrameworkCore;
using prog7311.Models;

namespace prog7311.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=app.db");
    }
} 