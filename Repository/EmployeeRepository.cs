using System;
using System.Collections.Generic;
using System.Data.SQLite;
using prog7311.Models;
using Microsoft.EntityFrameworkCore;

/*
 * This is the Employee repository. It handles all of the CRUD operations
 * for the employees.
 */

namespace prog7311.Repository
{
    public class EmployeeRepository
    {
        // connection string for the sqlite database
        private const string ConnectionString = "Data Source=app.db;Version=3;";

        // This is the AddEmployee method. It takes in a name, email and password
        // parameter, creates a new employee and then adds it to the database.
        public void AddEmployee(string name, string email, string password)
        {
            using (var db = new AppDbContext())
            {
                var employee = new Employee { Name = name, Email = email, Password = password };
                db.Employees.Add(employee);
                db.SaveChanges();
            }
        }

        // This is the UpdateEmployee method. It takes in the unique id, name, email
        // and password. If an employee is found with the provided ID it updates the 
        // employee details
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

        // This is the DeleteEmployee method. It takes in an employee ID and
        // if the employee is found, it deletes the Employee from the database.
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

        // This is the GetAllEmployees method. It returns a list of all of the 
        // employees in the database.
        public List<Employee> GetAllEmployees()
        {
            using (var db = new AppDbContext())
            {
                return db.Employees.ToList();
            }
        }
    }
} 