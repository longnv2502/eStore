using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace BussinessObject.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartDetails = new HashSet<CartDetail>();
        }

        public int CartId { get; set; }
        public double? CartTotle { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string MemberId { get; set; } = null!;

        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
