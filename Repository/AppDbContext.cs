using Microsoft.EntityFrameworkCore;
using prog7311.Models;
using System;
using System.Linq;

/*
 * This is the AppDbContext class. It is responsible for the initial creation
 * and seeding of data into the database. It uses entity framework.
 */
namespace prog7311.Repository
{
    public class AppDbContext : DbContext
    {
        // farmers table
        public DbSet<Farmer> Farmers { get; set; }
        // employee table
        public DbSet<Employee> Employees { get; set; }
        // products table
        public DbSet<Product> Products { get; set; }

        // Configure the database connection to the local sqlite database called app.db
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=app.db");

        // Primary constructor
        public AppDbContext()
        {
            // Make sure the database and tables exist
            Database.EnsureCreated();
            // Add in the default data into the database
            SeedDatabase();
        }

        // This is the seed database method. It creates and adds employees farmers and products 
        // as well as assigning products to farmers
        private void SeedDatabase()
        {
            // Only run if database is not empty 
            if (!Employees.Any() && !Farmers.Any() && !Products.Any())
            {
                // Seed Employees
                Employees.AddRange(new[]
                {
                    new Employee { Name = "Clive Frankland", Email = "clive.frankland@example.com", Password = "password1" },
                    new Employee { Name = "Matthew Allison", Email = "matthew.allison@example.com", Password = "password2" }
                });
                SaveChanges();

                // Seed Farmers
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
                
                // Randomly distribute products amongst the farmers. 5 each
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