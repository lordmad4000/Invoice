using System.ComponentModel.DataAnnotations;

namespace Users.API.Models.Request
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, ErrorMessage = "Username cannot be longer than 20 characters.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 10 characters.")]
        public string Password { get; set; }
    }
}