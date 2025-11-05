using CarRentalAPI.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Data
{
    public class CarRentalAPIContext : IdentityDbContext<APIUser>
    {
        public CarRentalAPIContext(DbContextOptions<CarRentalAPIContext> options)
            : base(options)
        {
        }
        public DbSet<CarRentalAPI.Models.Car> Car { get; set; } = default!;
        public DbSet<CarRentalAPI.Models.Customer> Customer { get; set; } = default!;
        public DbSet<CarRentalAPI.Models.Booking> Booking { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = APIRoles.Admin,
                    NormalizedName = APIRoles.Admin,
                    Id = "4707afb7-ec07-42de-9641-ec7e5a0631dd"
                },
                new IdentityRole
                {
                    Name = APIRoles.User,
                    NormalizedName = APIRoles.User,
                    Id = "6f51ac3a-e8b9-42b5-9a57-9d4901e12fcf"
                }
            );

            var hasher = new PasswordHasher<APIUser>();

            builder.Entity<APIUser>().HasData(
                new APIUser
                {
                    Id = "e8bf6b60-ff42-4de3-bddf-8678c327965b",
                    Email = "admin@carrentalapi.com",
                    NormalizedEmail = "ADMIN@CARRENTALAPI.COM",
                    UserName = "admin@carrentalapi.com",
                    NormalizedUserName = "ADMIN@CARRENTALAPI.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    PasswordHash = hasher.HashPassword(null, "Admin12345!"),
                    EmailConfirmed = true
                },
                new APIUser
                {
                    Id = "bb0deafa-dc7a-4098-926e-45913f8bc65e",
                    Email = "user@carrentalapi.com",
                    NormalizedEmail = "USER@CARRENTALAPI.COM",
                    UserName = "user@carrentalapi.com",
                    NormalizedUserName = "USER@CARRENTALAPI.COM",
                    FirstName = "System",
                    LastName = "User",
                    PasswordHash = hasher.HashPassword(null, "User12345!"),
                    EmailConfirmed = true
                }
            );
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "4707afb7-ec07-42de-9641-ec7e5a0631dd",
                    UserId = "e8bf6b60-ff42-4de3-bddf-8678c327965b"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "6f51ac3a-e8b9-42b5-9a57-9d4901e12fcf",
                    UserId = "bb0deafa-dc7a-4098-926e-45913f8bc65e"
                }
            );
        }
    }
}
