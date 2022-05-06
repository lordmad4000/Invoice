using System;
using System.ComponentModel.DataAnnotations;

namespace Users.API.Models.Response
{
    public class UserLoginResponse
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
    }
}