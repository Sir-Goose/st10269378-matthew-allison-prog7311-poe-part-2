using System;
using System.Collections.Generic;
using System.Data.SQLite;
using prog7311.Models;
using Microsoft.EntityFrameworkCore;

namespace prog7311.Repository
{
    public class EmployeeRepository
    {
        private const string ConnectionString = "Data Source=app.db;Version=3;";

        public void AddEmployee(string name, string email, string password)
        {
            using (var db = new AppDbContext())
            {
                var employee = new Employee { Name = name, Email = email, Password = password };
                db.Employees.Add(employee);
                db.SaveChanges();
            }
        }

        public void UpdateEmployee(int id, string name, string email, string password)
        {
            using (var db = new AppDbContext())
            {
                var employee = db.Employees.Find(id);
                if (employee != null)
                {
                    employee.Name = name;
                    employee.Email = email;
                    employee.Password = password;
                    db.SaveChanges();
                }
            }
        }

        public void DeleteEmployee(int id)
        {
            using (var db = new AppDbContext())
            {
                var employee = db.Employees.Find(id);
                if (employee != null)
                {
                    db.Employees.Remove(employee);
                    db.SaveChanges();
                }
            }
        }

        public List<Employee> GetAllEmployees()
        {
            using (var db = new AppDbContext())
            {
                return db.Employees.ToList();
            }
        }
    }
} 