using prog7311.Models;
using prog7311.Repository;

namespace prog7311.Services
{
    public class FarmerService : IFarmerService
    {
        private readonly AppDbContext _dbContext;

        public FarmerService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Farmer> GetAllFarmers()
        {
            return _dbContext.Farmers.ToList();
        }

        public Farmer GetFarmerByEmail(string email)
        {
            return _dbContext.Farmers.FirstOrDefault(f => f.Email == email);
        }

        public (bool success, string message) AddFarmer(Farmer farmer)
        {
            try
            {
                if (_dbContext.Farmers.Any(f => f.Email == farmer.Email))
                {
                    return (false, "A farmer with this email already exists.");
                }

                _dbContext.Farmers.Add(farmer);
                _dbContext.SaveChanges();
                return (true, "Farmer added successfully.");
            }
            catch
            {
                return (false, "An error occurred.");
            }
        }
    }
} 