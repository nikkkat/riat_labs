using AuthServiceMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов
builder.Services.AddControllersWithViews(); // Поддержка MVC (контроллеров и представлений)
builder.Services.AddEndpointsApiExplorer(); // Для API документации
builder.Services.AddSwaggerGen(); // Swagger для API тестирования

// Регистрация сервиса AuthService через интерфейс IAuthService
builder.Services.AddScoped<IAuthService, AuthService>();

// Регистрируем HttpClient для вызова микросервиса аутентификации
builder.Services.AddHttpClient<AuthServiceClient>();

var app = builder.Build();

// Настройка среды
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Общая настройка HTTP
app.UseHttpsRedirection();
app.UseStaticFiles(); // Для обслуживания статических файлов (CSS, JS, изображения)

app.UseRouting();

app.UseAuthentication(); // Если используется авторизация
app.UseAuthorization();

// Настройка маршрутов
app.UseEndpoints(endpoints =>
{
    // Роутинг для контроллеров
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Accoun}/{action=Login}/{id?}");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accoun}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "drawing",
    pattern: "{controller=Accoun}/{action=Draw}/{id?}");

// Запуск приложения
app.Run();
