using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Interfaces;

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
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            ActionResult result = new OkResult();
            await Task.Run(() =>
            {
                result = Ok();
            });
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser()
        {
            ActionResult result = new OkResult();
            await Task.Run(() =>
            {
                result = Ok();
            });
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> PostUser()
        {
            ActionResult result = new OkResult();
            await Task.Run(() =>
            {
                result = Ok();
            });
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            ActionResult result = new OkResult();
            await Task.Run(() =>
            {
                result = Ok();
            });
            return result;
        }
    }
}
