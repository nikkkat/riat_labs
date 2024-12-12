using AuthServiceMicroservice.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthServiceMicroservice.Controllers
{
    public class DrawingController : Controller
    {
        private readonly IAuthService _authService;

        public DrawingController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("Draw")]
        public IActionResult Draw()
        {
            // Проверяем авторизован ли пользователь
            if (_authService.GetCurrentUser() == null)
            {
                return RedirectToAction("Login", "Accoun");
            }
            return View(); // Отображаем страницу рисования
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View(); // Отображаем страницу рисования
        }
    }

}
