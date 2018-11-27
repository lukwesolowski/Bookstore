using System.ComponentModel.DataAnnotations;

namespace Bookstore.API.DTOs
{
    public class UserToRegisterDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, 
            ErrorMessage = "Please specify Your password between 4 to 8 chars")] 
        public string Password { get; set; }
    }
}