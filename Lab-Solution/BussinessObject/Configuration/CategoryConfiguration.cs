using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Enim Etiam Corp"
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Facilisis Vitae Orci LLC"
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Fringilla Ornare Associates"
                },
                new Category
                {
                    CategoryId = 4,
                    CategoryName = "Primis In Faucibus Consulting"
                },
                new Category
                {
                    CategoryId = 5,
                    CategoryName = "Suspendisse Associates"
                });
        }
    }
}
