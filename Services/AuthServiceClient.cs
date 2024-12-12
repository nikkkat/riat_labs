using System.Text.Json;
using System.Text;

namespace AuthServiceMicroservice.Services
{
    public class AuthServiceClient
    {

        private readonly HttpClient _httpClient;

        public AuthServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7205/"); // URL микросервиса
        }

        public async Task<bool> Register(string username, string password)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(new { Username = username, Password = password }),
                Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/auth/register", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Login(string username, string password)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(new { Username = username, Password = password }),
                Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/auth/login", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> GetCurrentUser()
        {
            var response = await _httpClient.GetAsync("api/auth/current");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<CurrentUserResponse>(json);
                return result?.User;
            }
            return null;
        }

        public async Task Logout()
        {
            await _httpClient.PostAsync("api/auth/logout", null);
        }

        private class CurrentUserResponse
        {
            public string User { get; set; }
        }
    }
}
