using System.ComponentModel.DataAnnotations;

namespace Invoice.Api.Models.Request
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(40, ErrorMessage = "Email cannot be longer than 40 characters.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 10 characters.")]
        public string Password { get; set; }
    }
}