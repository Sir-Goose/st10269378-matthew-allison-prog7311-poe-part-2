using System;
using System.Data.SQLite;

namespace prog7311.Repository
{
    public class Database
    {
        private const string ConnectionString = "Data Source=app.db;Version=3;";

        public Database()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                CreateTables(connection);
            }
        }

        private void CreateTables(SQLiteConnection connection)
        {
            string createFarmerTable = @"
                CREATE TABLE IF NOT EXISTS Farmer (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Email TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL
                );";

            string createEmployeeTable = @"
                CREATE TABLE IF NOT EXISTS Employee (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Email TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL
                );";

            string createProductTable = @"
                CREATE TABLE IF NOT EXISTS Product (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FarmerId INTEGER NOT NULL,
                    Name TEXT NOT NULL,
                    Category TEXT NOT NULL,
                    ProductionDate TEXT NOT NULL,
                    FOREIGN KEY(FarmerId) REFERENCES Farmer(Id)
                );";

            using (var command = new SQLiteCommand(createFarmerTable, connection))
            {
                command.ExecuteNonQuery();
            }
            using (var command = new SQLiteCommand(createEmployeeTable, connection))
            {
                command.ExecuteNonQuery();
            }
            using (var command = new SQLiteCommand(createProductTable, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
