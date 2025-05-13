using prog7311.Models;

// This is the interface for the authentication service

namespace prog7311.Services
{
    public interface IAuthenticationService
    {
        // Validate provided login credentials
        (bool success, string role, string email) ValidateLogin(string email, string password);
        // Handle logouts
        void Logout();
    }
} 