using prog7311.Models;
using prog7311.Repository;

namespace prog7311.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _dbContext;

        public ProductService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetProductsByFarmer(int farmerId)
        {
            return _dbContext.Products.Where(p => p.FarmerId == farmerId).ToList();
        }

        public List<Product> SearchProducts(string name, string category, int? farmerId)
        {
            var query = _dbContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.Contains(name));
            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category.Contains(category));
            if (farmerId.HasValue)
                query = query.Where(p => p.FarmerId == farmerId.Value);

            return query.ToList();
        }

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