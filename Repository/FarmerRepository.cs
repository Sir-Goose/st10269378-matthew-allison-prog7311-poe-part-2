using System;
using System.Collections.Generic;
using System.Data.SQLite;
using prog7311.Models;
using Microsoft.EntityFrameworkCore;

/*
 * This is the farmer repository class. It handles all database CRUD actions related to the
 * farmer table.
 */

namespace prog7311.Repository
{
    public class FarmerRepository
    {
        // Connection string for the sqlite database
        private const string ConnectionString = "Data Source=app.db;Version=3;";

        // This is the add farmer method. It takes in a name, email and password as parameters
        // and creates a farmer object and inserts it into the database.
        public void AddFarmer(string name, string email, string password)
        {
            using (var db = new AppDbContext())
            {
                var farmer = new Farmer { Name = name, Email = email, Password = password };
                db.Farmers.Add(farmer);
                db.SaveChanges();
            }
        }

        // This is the update farmer method. It takes an a farmer ID, name, email and password.
        // If it finds an existing farmer with that ID it updates the details of that farmer.
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

        // This is the delete farmer method. It takes in a farmer ID as a parameter. If it finds a
        // farmer with a matching ID it removes the farmer from the database.
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

        // This is the get all farmers method. It returns a list of all of the farmers in the database.
        public List<Farmer> GetAllFarmers()
        {
            using (var db = new AppDbContext())
            {
                return db.Farmers.ToList();
            }
        }
    }
} 