using prog7311.Models;

// This is the interface for the farmer service

namespace prog7311.Services
{
    public interface IFarmerService
    {
        // Retrieve a list of all farmers
        List<Farmer> GetAllFarmers();
        // Get a specific farmer using an email address
        Farmer GetFarmerByEmail(string email);
        // Add a new farmer
        (bool success, string message) AddFarmer(Farmer farmer);
    }
} 