namespace prog7311.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int FarmerId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime ProductionDate { get; set; }
        public Farmer Farmer { get; set; }
    }
} 