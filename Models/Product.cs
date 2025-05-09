using System.ComponentModel.DataAnnotations;

namespace prog7311.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public int FarmerId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Category { get; set; }
        [Required]
        public DateTime ProductionDate { get; set; }
        public Farmer Farmer { get; set; }
    }
} 