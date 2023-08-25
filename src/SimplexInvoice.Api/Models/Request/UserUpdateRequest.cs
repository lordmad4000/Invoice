using System;
using System.ComponentModel.DataAnnotations;

namespace SimplexInvoice.Api.Models.Request
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(40, ErrorMessage = "Email cannot be longer than 40 characters.")]
        public string Email { get; set; } = String.Empty;
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 10 characters.")]
        public string Password { get; set; } = String.Empty;
        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(20, ErrorMessage = "FirstName cannot be longer than 20 characters.")]
        public string FirstName { get; set; } = String.Empty;
        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(20, ErrorMessage = "LastName cannot be longer than 20 characters.")]
        public string LastName { get; set; } = String.Empty;
    }
}