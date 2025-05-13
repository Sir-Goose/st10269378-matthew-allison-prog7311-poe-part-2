using prog7311.Models;
using prog7311.Repository;

/*
 * This is the AuthenticationService class. it handles the authentication logic for the
 * web app.
 */

namespace prog7311.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppDbContext _dbContext;

        // Primary constructor
        public AuthenticationService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // This is the ValidateLogin method. It validates the user provided login credentials for both employees and 
        // farmers. It takes in an email and a password. 
        // It uses a tuple to indidicate success.  
        public (bool success, string role, string email) ValidateLogin(string email, string password)
        {
            // Check if the provided details match an employee in the database
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Email == email && e.Password == password);
            if (employee != null)
            {
                return (true, "Employee", employee.Email);
            }

            // Check if the provided details match a farmer in the database.
            var farmer = _dbContext.Farmers.FirstOrDefault(f => f.Email == email && f.Password == password);
            if (farmer != null)
            {
                return (true, "Farmer", farmer.Email);
            }

            // If neither for both cases return false.
            return (false, null, null);
        }

        // Logout stub.
        public void Logout()
        {
        }
    }
} 