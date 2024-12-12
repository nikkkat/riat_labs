using AuthServiceMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

// ���������� ��������
builder.Services.AddControllersWithViews(); // ��������� MVC (������������ � �������������)
builder.Services.AddEndpointsApiExplorer(); // ��� API ������������
builder.Services.AddSwaggerGen(); // Swagger ��� API ������������

// ����������� ������� AuthService ����� ��������� IAuthService
builder.Services.AddScoped<IAuthService, AuthService>();

// ������������ HttpClient ��� ������ ������������ ��������������
builder.Services.AddHttpClient<AuthServiceClient>();

var app = builder.Build();

// ��������� �����
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ����� ��������� HTTP
app.UseHttpsRedirection();
app.UseStaticFiles(); // ��� ������������ ����������� ������ (CSS, JS, �����������)

app.UseRouting();

app.UseAuthentication(); // ���� ������������ �����������
app.UseAuthorization();

// ��������� ���������
app.UseEndpoints(endpoints =>
{
    // ������� ��� ������������
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

// ������ ����������
app.Run();
