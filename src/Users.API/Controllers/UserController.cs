using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Interfaces;
using Users.Application.Services;

namespace Users.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsers();
                return (Ok(users));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var userVM = await _userService.GetById(id);
                return (Ok(userVM));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutUser([FromBody] UserViewModel userVM)
        {
            try
            {
                await _userService.PutUser(userVM);
                return (Ok(userVM));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserViewModel userVM)
        {
            try
            {
                userVM = await _userService.PostUser(userVM);
                return (Ok(userVM));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var result = await _userService.DeleteUser(id);
                return (Ok(result));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("ActivateUser")]
        public async Task<IActionResult> ActivateUser(string activationCode)
        {
            try
            {
                var result = await _userService.ActivateUser(activationCode);
                return (Ok(result));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
