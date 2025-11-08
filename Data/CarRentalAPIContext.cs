using CarRentalAPI.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Data
{
    public class CarRentalAPIContext : IdentityDbContext<APIUser>
    {
        internal object Cars;

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

            // Seed Roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = APIRoles.Admin,
                    NormalizedName = APIRoles.Admin.ToUpper(),
                    Id = "4707afb7-ec07-42de-9641-ec7e5a0631dd"
                },
                new IdentityRole
                {
                    Name = APIRoles.User,
                    NormalizedName = APIRoles.User.ToUpper(),
                    Id = "6f51ac3a-e8b9-42b5-9a57-9d4901e12fcf"
                }
            );

            // Static password hashes generated using PasswordHasher
            var adminPasswordHash = "AQAAAAIAAYagAAAAELSY9W/d8BEJ6ToJ52C22STAFAflvh2Echlyi/Ag5MnyoZa+2yR5y4n2e2yrcWKHYQ==";
            var userPasswordHash = "AQAAAAIAAYagAAAAEOfXoQJhJZqFn3Vip/6ZSSNR65FotiWP9aF3kd1/rrY9beZ9s90dsbUYQG1MFxakNg==";

            // Seed Users with STATIC values only
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
                    SecurityStamp = "92C14015-1031-4A26-B151-38E48494C642", // Static GUID
                    EmailConfirmed = true,
                    ConcurrencyStamp = "D54C2414-8BBA-4A85-A718-29BB40424289" // Static GUID
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
                    SecurityStamp = "8F82577D-61DE-407B-B246-9AA8202970C0", // Static GUID
                    EmailConfirmed = true,
                    ConcurrencyStamp = "4B96F166-643E-4C21-A0B6-82D10CDA8025" // Static GUID
                }
            );

            // Seed User-Role relationships
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
