using prog7311.Models;

namespace prog7311.Services
{
    public interface IFarmerService
    {
        List<Farmer> GetAllFarmers();
        Farmer GetFarmerByEmail(string email);
        (bool success, string message) AddFarmer(Farmer farmer);
    }
} 