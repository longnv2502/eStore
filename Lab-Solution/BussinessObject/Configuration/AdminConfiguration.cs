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
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var adminConfig = config.GetSection("Admin");
            PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = ConstantConfiguration.Admin.ID,
                    FirsName = "Long",
                    LastName = "NV",
                    UserName = "Administrator",
                    NormalizedUserName = "Administrator",
                    Email = adminConfig.GetSection("Email").Value,
                    EmailConfirmed = true,
                    NormalizedEmail = adminConfig.GetSection("Email").Value,
                    PasswordHash = hasher.HashPassword(null, adminConfig.GetSection("Password").Value),
                    SecurityStamp = string.Empty,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                }
            );
        }
    }
}
