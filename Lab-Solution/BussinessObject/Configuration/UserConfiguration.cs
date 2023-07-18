using BussinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BussinessObject.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = ConstantConfiguration.Admin.ID,
                    FirstName = "Admin",
                    LastName = "",
                    UserName = "Administrator",
                    NormalizedUserName = "Administrator",
                    Email = "admin@estore.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "admin@estore.com",
                    PasswordHash = hasher.HashPassword(null, "admin@@"),
                    SecurityStamp = string.Empty,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new ApplicationUser
                {
                    Id = ConstantConfiguration.User.ID,
                    FirstName = "Long",
                    LastName = "NV",
                    UserName = "dclong2502",
                    NormalizedUserName = "dclong2502",
                    Email = "dclong2502@gmail.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "dclong2502@gmail.com",
                    PhoneNumber = "0969975700",
                    PhoneNumberConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "admin@@"),
                    SecurityStamp = string.Empty,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                }
            );
        }
    }
}
