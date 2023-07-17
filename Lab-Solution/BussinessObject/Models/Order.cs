using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    [Table("Order")]
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public float? Feight { get; set; }

        [ForeignKey("ApplicationUser")]
        public string MemberId { get; set; } = null!;

        public virtual ApplicationUser Member { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public Order SetValue(Order value)
        {
            _ = value.OrderDate != null ? OrderDate = value.OrderDate : null;
            _ = value.RequiredDate != null ? RequiredDate = value.RequiredDate : null;
            _ = value.ShippedDate != null ? ShippedDate = value.ShippedDate : null;
            _ = value.Feight != null ? Feight = value.Feight : null;
            MemberId = value.MemberId;
            return this;
        }
    }
}
