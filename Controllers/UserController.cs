using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using User_Project.Entities;
using User_Project.Services;

namespace User_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _content;

        public UserController(IUserRepository content)
        {

            _content = content ?? throw new ArgumentNullException(nameof(content));
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _content.GetUserAsync();
            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            var user = await _content.GetUserByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("User")]
        public async Task<ActionResult<User>> AddTodoItem(
            int id ,string username, string password, string email
            )
        {
            var user = new User(
                id,
                username,
                password,
                email
            );

            _content.AddUserAsync(user);
            await _content.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { username = user.Username }, user);
        }

        [HttpGet("User/Entity/{username}")]
        public async Task<User> GetUserEntity(string username)
        {
            var user = await _content.GetUserByNameAsync(username);
            if (user == null)
            {
                return null;
            }
            return user;
        }

    }
}
