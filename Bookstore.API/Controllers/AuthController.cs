using System.Threading.Tasks;
using Bookstore.API.Data.Interfaces;
using Bookstore.API.DTOs;
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
        public async Task<IActionResult> Register(UserToRegisterDto userToRegisterDto)
        {
            userToRegisterDto.Login = userToRegisterDto.Login.ToLower();

            if(await _repo.UserExists(userToRegisterDto.Login))
                return BadRequest("Login already taken");
            
            var userToRegister = new User
            {
                Login = userToRegisterDto.Login,
                UserRole = "Client"
            };

            var registeredUser = await _repo.Register(userToRegister, userToRegisterDto.Password);

            return StatusCode(201);
        }
    }
}