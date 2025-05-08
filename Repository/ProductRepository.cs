using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace prog7311.Repository
{
    public class ProductRepository
    {
        private const string ConnectionString = "Data Source=app.db;Version=3;";

        public void AddProduct(int farmerId, string name, string category, DateTime productionDate)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("INSERT INTO Product (FarmerId, Name, Category, ProductionDate) VALUES (@FarmerId, @Name, @Category, @ProductionDate)", connection);
                command.Parameters.AddWithValue("@FarmerId", farmerId);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Category", category);
                command.Parameters.AddWithValue("@ProductionDate", productionDate.ToString("yyyy-MM-dd"));
                command.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(int id, int farmerId, string name, string category, DateTime productionDate)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("UPDATE Product SET FarmerId = @FarmerId, Name = @Name, Category = @Category, ProductionDate = @ProductionDate WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@FarmerId", farmerId);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Category", category);
                command.Parameters.AddWithValue("@ProductionDate", productionDate.ToString("yyyy-MM-dd"));
                command.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("DELETE FROM Product WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        public List<(int Id, int FarmerId, string Name, string Category, DateTime ProductionDate)> GetAllProducts()
        {
            var products = new List<(int, int, string, string, DateTime)>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT Id, FarmerId, Name, Category, ProductionDate FROM Product", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add((
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            DateTime.Parse(reader.GetString(4))
                        ));
                    }
                }
            }
            return products;
        }
    }
} 