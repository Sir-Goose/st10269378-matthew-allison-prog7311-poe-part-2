using prog7311.Models;

// This is the product interface

namespace prog7311.Services
{
    public interface IProductService
    {
        // Get all products belonging to a particular farmer
        List<Product> GetProductsByFarmer(int farmerId);
        // Search for a product by name. category and optinally a farmer id
        List<Product> SearchProducts(string name, string category, int? farmerId);
        // Add a new product
        (bool success, string message) AddProduct(Product product);
    }
} 