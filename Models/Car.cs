namespace CarRentalAPI.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; } = "";
        public string Model { get; set; } = "";
        public string Color { get; set; } = "";
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; } = true;

        public string ImageUrl { get; set; } = "";

    }
}
