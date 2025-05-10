using prog7311.Models;

namespace prog7311.Services
{
    public interface IAuthenticationService
    {
        (bool success, string role, string email) ValidateLogin(string email, string password);
        void Logout();
    }
} 