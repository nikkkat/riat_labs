using AuthServiceMicroservice.Models;
using AuthServiceMicroservice.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class AccounController : Controller
{
    private readonly IAuthService _authService;

    public AccounController(IAuthService authService)
    {
        _authService = authService;
    }

    // Страница логина
    [HttpGet]
    public IActionResult Login()
    {
        return View(); // Возвращает Login.cshtml
    }

    // Обработка логина
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        if (_authService.Login(username, password))
        {
            // Перенаправляем на страницу рисования после успешного входа
            return RedirectToAction("Index", "Drawing"); // Переход на контроллер Drawing, метод Index
        }
        else
        {
            ModelState.AddModelError("", "Неверный логин или пароль.");
            return View();
        }
    }

    // Страница регистрации
    [HttpGet]
    public IActionResult Register()
    {
        return View(); // Возвращает Register.cshtml
    }

    // Обработка регистрации
    [HttpPost]
    public IActionResult Register(string username, string password)
    {
        if (_authService.Register(username, password))
        {
            // Перенаправляем на страницу рисования после успешной регистрации
            return RedirectToAction("Index", "Drawing"); // Переход на контроллер Drawing, метод Index
        }
        else
        {
            ModelState.AddModelError("", "Ошибка регистрации. Пользователь уже существует.");
            return View();
        }
    }

    // Страница для рисования
    public IActionResult Draw()
    {
        // Если пользователь не авторизован, перенаправляем на страницу логина
        var user = _authService.GetCurrentUser();
        if (user == null)
        {
            return RedirectToAction("Login");
        }
        return RedirectToAction("Index", "Drawing"); // Переход на метод Index контроллера Drawing
    }

    // Страница выхода
    public IActionResult Logout()
    {
        _authService.Logout();
        return RedirectToAction("Login");
    }
}
