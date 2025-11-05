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

            var adminPasswordHash = "AQAAAAIAAYagAAAAEPNPzqnjA6IeLF1Pu8/XKp4wPoVXxrnhudWpPTrMYJoSbjwrCeKiAehZUYImRm0u3g==";
            var userPasswordHash = "AQAAAAIAAYagAAAAEOSpokKKxCZuIJ5/k4yFATOZAx6YzLMyHKd1D+LFXUUxOtAB3rEQO06UWsGrcLbcbQ==";
            
            // Static security stamps
            var adminSecurityStamp = "92c14015-1031-4a26-b151-38e48494c642";
            var userSecurityStamp = "8f82577d-61de-407b-b246-9aa8202970c0";

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
                    PasswordHash = adminPasswordHash,
                    SecurityStamp = adminSecurityStamp,
                    EmailConfirmed = true,
                    ConcurrencyStamp = "d54c2414-8bba-4a85-a718-29bb40424289"
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
                    PasswordHash = userPasswordHash,
                    SecurityStamp = userSecurityStamp,
                    EmailConfirmed = true,
                    ConcurrencyStamp = "4b96f166-643e-4c21-a0b6-82d10cda8025"
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
