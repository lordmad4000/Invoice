using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController()
        {
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            ActionResult result = new OkResult();
            await Task.Run(() =>
            {
                result = Ok();
            });
            return result;
        }

        // GET: api/Users/5
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

        // PUT: api/Users/5
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

        // POST: api/Users
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

        // DELETE: api/Users/5
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
