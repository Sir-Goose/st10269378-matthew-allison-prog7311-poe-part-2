using System.ComponentModel.DataAnnotations;

/*
 * This is the product class. It represents a
 * product in the database
 */

namespace prog7311.Models
{
    public class Product
    {
        // primary key for products
        public int Id { get; set; }
        [Required]
        // foreign key to reference the farmer who owns the product
        public int FarmerId { get; set; }
        [Required]
        [StringLength(100)]
        // nmae of the product
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        // product category
        public string Category { get; set; }
        [Required]
        // Product date
        public DateTime ProductionDate { get; set; }
        // Attached farmer
        public Farmer Farmer { get; set; }
    }
} 