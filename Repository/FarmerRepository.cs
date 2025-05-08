using System;
using System.Collections.Generic;
using System.Data.SQLite;
using prog7311.Models;
using Microsoft.EntityFrameworkCore;

namespace prog7311.Repository
{
    public class FarmerRepository
    {
        private const string ConnectionString = "Data Source=app.db;Version=3;";

        public void AddFarmer(string name, string email, string password)
        {
            using (var db = new AppDbContext())
            {
                var farmer = new Farmer { Name = name, Email = email, Password = password };
                db.Farmers.Add(farmer);
                db.SaveChanges();
            }
        }

        public void UpdateFarmer(int id, string name, string email, string password)
        {
            using (var db = new AppDbContext())
            {
                var farmer = db.Farmers.Find(id);
                if (farmer != null)
                {
                    farmer.Name = name;
                    farmer.Email = email;
                    farmer.Password = password;
                    db.SaveChanges();
                }
            }
        }

        public void DeleteFarmer(int id)
        {
            using (var db = new AppDbContext())
            {
                var farmer = db.Farmers.Find(id);
                if (farmer != null)
                {
                    db.Farmers.Remove(farmer);
                    db.SaveChanges();
                }
            }
        }

        public List<Farmer> GetAllFarmers()
        {
            using (var db = new AppDbContext())
            {
                return db.Farmers.ToList();
            }
        }
    }
} 