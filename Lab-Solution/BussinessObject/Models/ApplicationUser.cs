using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }

        public string FirsName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
