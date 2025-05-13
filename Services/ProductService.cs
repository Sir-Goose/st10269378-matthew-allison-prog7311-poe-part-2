using prog7311.Models;
using prog7311.Repository;

// This is the Product Service class. It implements the product interface

namespace prog7311.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _dbContext;

        // Primary constructor
        public ProductService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Returns a list of all products connected to a provided farmer ID
        public List<Product> GetProductsByFarmer(int farmerId)
        {
            return _dbContext.Products.Where(p => p.FarmerId == farmerId).ToList();
        }

        // Search for products by name, category and an optional farmerID
        public List<Product> SearchProducts(string name, string category, int? farmerId)
        {
            // Get all the products
            var query = _dbContext.Products.AsQueryable();

            // Filter by name if name was provided
            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.Contains(name));
            // Filter by category if provided
            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category.Contains(category));
            // Filter by farmer ID if provided
            if (farmerId.HasValue)
                query = query.Where(p => p.FarmerId == farmerId.Value);

            return query.ToList();
        }

        // This is the add product method. It adds a new product to the database,
        // It returns a tuple with a true or false and a message
        public (bool success, string message) AddProduct(Product product)
        {
            try
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                return (true, "Product added successfully.");
            }
            catch
            {
                return (false, "An unexpected error occurred while adding the product.");
            }
        }
    }
} 