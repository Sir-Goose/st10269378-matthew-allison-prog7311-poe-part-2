using System.ComponentModel.DataAnnotations;

namespace prog7311.Models
{
    public class Farmer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        public ICollection<Product> Products { get; set; }
    }
} 