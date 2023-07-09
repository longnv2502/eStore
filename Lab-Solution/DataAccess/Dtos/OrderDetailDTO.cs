using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class OrderDetailDTO
    {
        [Required]
        public int ProductId { get; set; }
        public double? UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        public double? Discount { get; set; }
    }
}
