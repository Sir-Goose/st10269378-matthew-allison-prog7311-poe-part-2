using System;
using System.Collections.Generic;
using System.Data.SQLite;
using prog7311.Models;
using Microsoft.EntityFrameworkCore;

namespace prog7311.Repository
{
    public class ProductRepository
    {
        private const string ConnectionString = "Data Source=app.db;Version=3;";

        public void AddProduct(int farmerId, string name, string category, DateTime productionDate)
        {
            using (var db = new AppDbContext())
            {
                var product = new Product { FarmerId = farmerId, Name = name, Category = category, ProductionDate = productionDate };
                db.Products.Add(product);
                db.SaveChanges();
            }
        }

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

        public List<Product> GetAllProducts()
        {
            using (var db = new AppDbContext())
            {
                return db.Products.ToList();
            }
        }
    }
} 