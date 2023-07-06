using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos
{
    public class SignUpModel
    {

        [Required]
        public string FirstName { get; set; }
        [Required] 
        public string LastName { get; set; }
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; } 

    }
}
