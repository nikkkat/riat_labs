using Microsoft.AspNetCore.Mvc;
using AuthServiceMicroservice.Services;

namespace AuthServiceMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserCredentials credentials)
        {
            if (_authService.Register(credentials.Username, credentials.Password))
                return Ok("Registration successful");
            return BadRequest("User already exists");
        }
        /*
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            if (_authService.Login(username, password))
            {
                // После успешного входа, перенаправляем на страницу Drawing/Index
                return RedirectToAction("Index", "Drawing");
            }

            // Если вход не удался, возвращаем на текущую страницу с ошибкой
            ModelState.AddModelError("", "Неверное имя пользователя или пароль");
            return RedirectToAction("Login", "Accoun");
        }*/

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials credentials)
        {
            if (_authService.Login(credentials.Username, credentials.Password))
            {
                // После успешного входа, перенаправляем на страницу Drawing/Index
                return Ok("Login successful");
            }

            return Unauthorized("Invalid username or password");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _authService.Logout();
            return Ok("Logged out");
        }

        [HttpGet("current")]
        public IActionResult GetCurrentUser()
        {
            var user = _authService.GetCurrentUser();
            if (user == null) return Unauthorized("No user is logged in");
            return Ok(new { User = user });
        }
    }

    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
