using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Data
{
    public class CarRentalAPIContext : IdentityDbContext
    {
        public CarRentalAPIContext(DbContextOptions<CarRentalAPIContext> options)
            : base(options)
        {
        }
        public DbSet<CarRentalAPI.Models.Car> Car { get; set; } = default!;
        public DbSet<CarRentalAPI.Models.Customer> Customer { get; set; } = default!;
        public DbSet<CarRentalAPI.Models.Booking> Booking { get; set; } = default!;
    }
}
