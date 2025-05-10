using prog7311.Models;

namespace prog7311.Services
{
    public interface IProductService
    {
        List<Product> GetProductsByFarmer(int farmerId);
        List<Product> SearchProducts(string name, string category, int? farmerId);
        (bool success, string message) AddProduct(Product product);
    }
} 