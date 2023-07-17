using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class ProductDto
    {
        public string ProductName { get; set; } = null!;
        public double? Weight { get; set; }
        public double? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public int CategoryId { get; set; }
    }
}
