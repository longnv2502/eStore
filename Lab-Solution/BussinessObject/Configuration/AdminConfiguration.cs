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
    public class AdminConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = ConstantConfiguration.Admin.ID,
                    FirstName = "Long",
                    LastName = "NV",
                    UserName = "Administrator",
                    NormalizedUserName = "Administrator",
                    Email = "admin@estore.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "admin@estore.com",
                    PasswordHash = hasher.HashPassword(null, "admin@@"),
                    SecurityStamp = string.Empty,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                }
            );
        }
    }
}
