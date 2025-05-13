using prog7311.Models;
using prog7311.Repository;

// This is the farmer service class. It manages all farmer related operations.

namespace prog7311.Services
{
    public class FarmerService : IFarmerService
    {
        private readonly AppDbContext _dbContext;

        // Primary constructor
        public FarmerService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Returns a list containing all farmers in the database
        public List<Farmer> GetAllFarmers()
        {
            return _dbContext.Farmers.ToList();
        }

        // Searches the database for a farmer with a matching email.
        public Farmer GetFarmerByEmail(string email)
        {
            return _dbContext.Farmers.FirstOrDefault(f => f.Email == email);
        }

        // This is the add farmer method. It takes in a farmer object and returns a 
        // a true or false a long with a message dependign on whether it was
        // successful
        public (bool success, string message) AddFarmer(Farmer farmer)
        {
            try
            {
                if (_dbContext.Farmers.Any(f => f.Email == farmer.Email))
                {
                    // Duplicate email
                    return (false, "A farmer with this email already exists.");
                }

                _dbContext.Farmers.Add(farmer);
                _dbContext.SaveChanges();
                // farmer was added
                return (true, "Farmer added successfully.");
            }
            catch
            {
                // an unknown error took place
                return (false, "An error occurred.");
            }
        }
    }
} 