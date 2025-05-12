using Microsoft.EntityFrameworkCore;
using prog7311.Models;
using System;
using System.Linq;

namespace prog7311.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=app.db");

        public AppDbContext()
        {
            Database.EnsureCreated();
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            if (!Employees.Any() && !Farmers.Any() && !Products.Any())
            {
                // Employees
                Employees.AddRange(new[]
                {
                    new Employee { Name = "Clive Frankland", Email = "clive.frankland@example.com", Password = "password1" },
                    new Employee { Name = "Matthew Allison", Email = "matthew.alison@example.com", Password = "password2" }
                });
                SaveChanges();

                // Farmers
                var farmers = new[]
                {
                    new Farmer { Name = "Joshua Shields", Email = "joshua.shields@example.com", Password = "boer123" },
                    new Farmer { Name = "Kashvir Sewpersad", Email = "kashvir.sewpersad@example.com", Password = "plaas456" },
                    new Farmer { Name = "Erin Steenveld", Email = "erin.steenveld@example.com", Password = "landbou789" },
                    new Farmer { Name = "Luke Carolus", Email = "luke.carolus@example.com", Password = "groente321" },
                    new Farmer { Name = "Rudolf Holzhausen", Email = "rudolf.holzhausen@example.com", Password = "vars654" }
                };
                Farmers.AddRange(farmers);
                SaveChanges();

                // Products for each farmer
                var productNames = new[]
                {
                    ("Boerewors", "Meat"),
                    ("Biltong", "Meat"),
                    ("Rooibos Tea", "Beverage"),
                    ("Naartjies", "Fruit"),
                    ("Koeksisters", "Baked Goods"),
                    ("Melktert", "Baked Goods"),
                    ("Gem Squash", "Vegetable"),
                    ("Droëwors", "Meat"),
                    ("Granadillas", "Fruit"),
                    ("Butternut", "Vegetable")
                };
                var rand = new Random();
                foreach (var farmer in Farmers)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        var (prodName, prodCat) = productNames[rand.Next(productNames.Length)];
                        Products.Add(new Product
                        {
                            FarmerId = farmer.Id,
                            Name = prodName,
                            Category = prodCat,
                            ProductionDate = DateTime.Now.AddDays(-rand.Next(30))
                        });
                    }
                }
                SaveChanges();
            }
        }
    }
} 