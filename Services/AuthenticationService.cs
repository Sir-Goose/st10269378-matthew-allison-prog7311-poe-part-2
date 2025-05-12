using prog7311.Models;
using prog7311.Repository;

namespace prog7311.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppDbContext _dbContext;

        public AuthenticationService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public (bool success, string role, string email) ValidateLogin(string email, string password)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Email == email && e.Password == password);
            if (employee != null)
            {
                return (true, "Employee", employee.Email);
            }

            var farmer = _dbContext.Farmers.FirstOrDefault(f => f.Email == email && f.Password == password);
            if (farmer != null)
            {
                return (true, "Farmer", farmer.Email);
            }

            return (false, null, null);
        }

        public void Logout()
        {
        }
    }
} 