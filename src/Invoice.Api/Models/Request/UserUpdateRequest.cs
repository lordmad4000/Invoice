using System;
using System.ComponentModel.DataAnnotations;

namespace Invoice.Api.Models.Request
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(40, ErrorMessage = "Email cannot be longer than 40 characters.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(20, ErrorMessage = "FirstName cannot be longer than 20 characters.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(20, ErrorMessage = "LastName cannot be longer than 20 characters.")]
        public string LastName { get; set; }
    }
}