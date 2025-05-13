using System;
using System.Collections.Generic;
using System.Data.SQLite;
using prog7311.Models;
using Microsoft.EntityFrameworkCore;

// This is the product repository class. It handles all of the database CRUD operations for the product table.

namespace prog7311.Repository
{
    public class ProductRepository
    {
        // sqlite connection string
        private const string ConnectionString = "Data Source=app.db;Version=3;";

        // This is the add product method. It takes in a farmerId, name, category and production date as parameters.
        // It then creates a product object using that information and inserts it into the database.
        public void AddProduct(int farmerId, string name, string category, DateTime productionDate)
        {
            using (var db = new AppDbContext())
            {
                var product = new Product { FarmerId = farmerId, Name = name, Category = category, ProductionDate = productionDate };
                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        // This is the update product method. It takes in an ID, farmer ID, name, categroy and production date.
        // If iti finds a product with a matching ID it updates the product with the provided details.
        public void UpdateProduct(int id, int farmerId, string name, string category, DateTime productionDate)
        {
            using (var db = new AppDbContext())
            {
                var product = db.Products.Find(id);
                if (product != null)
                {
                    product.FarmerId = farmerId;
                    product.Name = name;
                    product.Category = category;
                    product.ProductionDate = productionDate;
                    db.SaveChanges();
                }
            }
        }

        // This is the delete product method. It takes in a product ID. If it finds a product with a matching
        // ID it removes the product from the database.
        public void DeleteProduct(int id)
        {
            using (var db = new AppDbContext())
            {
                var product = db.Products.Find(id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
            }
        }

        // This is the get all products method. it returns a list of all products in the database.
        public List<Product> GetAllProducts()
        {
            using (var db = new AppDbContext())
            {
                return db.Products.ToList();
            }
        }
    }
} 