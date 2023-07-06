using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class UpdateUser
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string UserName { get; set; }
        [Required]  
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; } 
        public string Phone { get; set; }
    }
}
