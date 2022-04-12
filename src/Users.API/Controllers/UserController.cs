using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Interfaces;
using Users.Application.Services;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
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
        public async Task<ActionResult> GetUser(Guid id)
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
        public async Task<ActionResult> PostUser([FromBody] UserViewModel userVM)
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
        public async Task<ActionResult> DeleteUser(Guid id)
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
    }
}
