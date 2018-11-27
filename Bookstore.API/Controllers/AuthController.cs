using System.Threading.Tasks;
using Bookstore.API.Data.Interfaces;
using Bookstore.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string login, string password)
        {
            login = login.ToLower();

            if(await _repo.UserExists(login))
                return BadRequest("Login already taken");
            
            var userToRegister = new User
            {
                Login = login,
                UserRole = "Client"
            };

            var registeredUser = await _repo.Register(userToRegister, password);

            return StatusCode(201);
        }
    }
}