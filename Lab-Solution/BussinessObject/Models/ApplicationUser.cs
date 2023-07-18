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
            Orders = new HashSet<Order>();
        }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }

        public ApplicationUser SetValue(ApplicationUser updateValue)
        {
            FirstName = updateValue.FirstName;
            LastName = updateValue.LastName;
            PhoneNumber = updateValue.PhoneNumber;
            return this;
        }
    }
}
