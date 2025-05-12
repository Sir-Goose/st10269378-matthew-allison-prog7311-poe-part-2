using System.ComponentModel.DataAnnotations;

/*
 * This is the farmer class. It is the data model for
 * farmers in the database.
 */

namespace prog7311.Models
{
    public class Farmer
    {
        // Primary key for farmers
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        // Farmer name
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        // Farmer email address
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        // Farmer password
        public string Password { get; set; }
        // A collection of products owned by the farmer
        public ICollection<Product> Products { get; set; }

        // Construtor for farmer to make sure that a 
        // product collection is instantiated
        public Farmer()
        {
            Products = new List<Product>(); 
        }
    }
}