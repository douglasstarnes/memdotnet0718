using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.ViewModels;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;

        }

        [HttpPost("register")]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (userRepository.Register(registerViewModel))
                return Created("pending", null);
            else
                return BadRequest(new { Error = "User already exists" });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var canLogin = userRepository.Login(loginViewModel);
            if (!canLogin)
                return Unauthorized();
            return Ok(new { Success = "Logged in"});
        }

        [HttpPost("token")]
        public IActionResult Token(LoginViewModel loginViewModel)
        {
            var canLogin = userRepository.Login(loginViewModel);
            if (!canLogin)
                return Unauthorized();
            var token = userRepository.GenerateToken(loginViewModel);
            return Ok(new { Token = token });
        }
    }
}