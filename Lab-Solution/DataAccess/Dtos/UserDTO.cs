using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class LoginUserDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(15, ErrorMessage = "Your Password is limited to {2} to {1} characters", MinimumLength = 6)]
        public string Password { get; set; } = null!;
    }

    public class RegisterUserDTO : LoginUserDTO
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;
    }

    public class UserDTO : RegisterUserDTO
    {
        public ICollection<string> Roles { get; set; } = null!;
    }
}
