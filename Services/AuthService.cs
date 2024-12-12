using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AuthServiceMicroservice.Services
{
    // Простая модель пользователя
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }

    public interface IAuthService
    {
        bool Login(string username, string password);
        bool Register(string username, string password);
        User GetCurrentUser();
        void Logout();
    }

    public class AuthService : IAuthService
    {
        private static List<User> _users = new List<User>();
        private static User _currentUser;

        // Метод для генерации соль и хэширования пароля
        private string GenerateHash(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hash);
            }
        }

        // Логика для входа пользователя
        public bool Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return false;

            // Проверяем, совпадает ли хэш пароля
            var hash = GenerateHash(password, user.Salt);
            if (user.PasswordHash == hash)
            {
                _currentUser = user; // Устанавливаем текущего пользователя
                return true;
            }

            return false;
        }

        // Логика для регистрации пользователя
        public bool Register(string username, string password)
        {
            if (_users.Any(u => u.Username == username))
                return false; // Пользователь с таким именем уже существует

            // Генерируем соль и хэшируем пароль
            string salt = Guid.NewGuid().ToString();
            var passwordHash = GenerateHash(password, salt);

            var user = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                Salt = salt
            };

            _users.Add(user);
            return true;
        }

        // Получение текущего пользователя
        public User GetCurrentUser()
        {
            return _currentUser;
        }

        // Логика выхода из системы
        public void Logout()
        {
            _currentUser = null; // Очищаем текущего пользователя
        }
    }
}
