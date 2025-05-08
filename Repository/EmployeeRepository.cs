using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace prog7311.Repository
{
    public class EmployeeRepository
    {
        private const string ConnectionString = "Data Source=app.db;Version=3;";

        public void AddEmployee(string name, string email, string password)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("INSERT INTO Employee (Name, Email, Password) VALUES (@Name, @Email, @Password)", connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateEmployee(int id, string name, string email, string password)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("UPDATE Employee SET Name = @Name, Email = @Email, Password = @Password WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("DELETE FROM Employee WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        public List<(int Id, string Name, string Email)> GetAllEmployees()
        {
            var employees = new List<(int, string, string)>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT Id, Name, Email FROM Employee", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add((reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                    }
                }
            }
            return employees;
        }
    }
} 