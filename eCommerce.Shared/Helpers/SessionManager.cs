using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;

namespace eCommerce.Shared.Helpers
{
    public static class SessionManager
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private static ISession Session => _httpContextAccessor?.HttpContext?.Session;

        public static T Get<T>(string key)
        {
            try
            {
                if (Session == null || !Session.TryGetValue(key, out var data))
                    return default;

                var jsonString = System.Text.Encoding.UTF8.GetString(data);
                return JsonSerializer.Deserialize<T>(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Session Get Error: {ex.Message}");
                return default;
            }
        }

        public static void Set<T>(string key, T value)
        {
            if (Session == null) return;

            var jsonString = JsonSerializer.Serialize(value);
            var data = System.Text.Encoding.UTF8.GetBytes(jsonString);
            Session.Set(key, data);
        }

        public static void ClearKey(string key)
        {
            Session?.Remove(key);
        }
    }
}
