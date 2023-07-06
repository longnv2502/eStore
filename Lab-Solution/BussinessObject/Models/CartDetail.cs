using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BussinessObject.Models
{
    public partial class CartDetail
    {
        [Key]
        public int CartDetailId { get; set; }
        public int CartId { get; set; } 
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }

        public virtual Cart Cart { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
