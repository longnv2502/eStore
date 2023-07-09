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
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    ProductId = 1,
                    CategoryId = 4,
                    ProductName = "Hendrerit Donec",
                    UnitPrice = 0.62,
                    UnitsInStock = 20,
                    Weight = 0.5
                },
                new Product
                {
                    ProductId = 2,
                    CategoryId = 2,
                    ProductName = "Euismod In Dolor Foundation",
                    UnitPrice = 0.39,
                    UnitsInStock = 12,
                    Weight = 0.5
                },
                new Product
                {
                    ProductId = 3,
                    CategoryId = 4,
                    ProductName = "Faucibus Morbi Vehicula Industries",
                    UnitPrice = 12.43,
                    UnitsInStock = 8,
                    Weight = 0.5
                },
                new Product
                {
                    ProductId = 4,
                    CategoryId = 3,
                    ProductName = "Magnis LLC",
                    UnitPrice = 9.80,
                    UnitsInStock = 18,
                    Weight = 0.5
                },
                new Product
                {
                    ProductId = 5,
                    CategoryId = 1,
                    ProductName = "Ante Inc.",
                    UnitPrice = 15.90,
                    UnitsInStock = 5,
                    Weight = 0.5
                },
                new Product
                {
                    ProductId = 6,
                    CategoryId = 1,
                    ProductName = "Curabitur Dictum Associates",
                    UnitPrice = 19.17,
                    UnitsInStock = 5,
                    Weight = 0.5
                },
                new Product
                {
                    ProductId = 7,
                    CategoryId = 2,
                    ProductName = "Adipiscing Non Luctus Industries",
                    UnitPrice = 9.98,
                    UnitsInStock = 17,
                    Weight = 0.5
                },
                new Product
                {
                    ProductId = 8,
                    CategoryId = 1,
                    ProductName = "Magna Nec Quam PC",
                    UnitPrice = 16.31,
                    UnitsInStock = 19,
                    Weight = 0.5
                },
                new Product
                {
                    ProductId = 9,
                    CategoryId = 1,
                    ProductName = "Est Nunc Institute",
                    UnitPrice = 18.10,
                    UnitsInStock = 13,
                    Weight = 0.5
                },
                new Product
                {
                    ProductId = 10,
                    CategoryId = 3,
                    ProductName = "Egestas Sed Pharetra Foundation",
                    UnitPrice = 12.29,
                    UnitsInStock = 1,
                    Weight = 0.5
                },
                new Product
                {
                    ProductId = 11,
                    CategoryId = 3,
                    ProductName = "Donec Est Institute",
                    UnitPrice = 19.83,
                    UnitsInStock = 11,
                    Weight = 0.5
                },
                new Product
                {
                    ProductId = 12,
                    CategoryId = 5,
                    ProductName = "Nunc Limited",
                    UnitPrice = 10.77,
                    UnitsInStock = 15,
                    Weight = 0.5
                },
                new Product
                {
                    ProductId = 13,
                    CategoryId = 3,
                    ProductName = "Tincidunt Corp.",
                    UnitPrice = 15.22,
                    UnitsInStock = 5,
                    Weight = 0.5
                },
                new Product
                {
                    ProductId = 14,
                    CategoryId = 3,
                    ProductName = "Nec Mauris Inc.",
                    UnitPrice = 12.54,
                    UnitsInStock = 8,
                    Weight = 0.5
                },
                new Product
                {
                    ProductId = 15,
                    CategoryId = 3,
                    ProductName = "Risus A Industries",
                    UnitPrice = 11.21,
                    UnitsInStock = 6,
                    Weight = 0.5
                });
        }
    }
}
