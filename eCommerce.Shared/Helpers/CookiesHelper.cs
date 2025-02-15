using System;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Shared.Helpers
{
    public class CookiesHelper
    {
        private const string COOKIE_NAME = "ObjValue";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookiesHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public T GetObjectFromCookie<T>(string key)
        {
            string strValue = GetStringFromCookie(key);
            return !string.IsNullOrEmpty(strValue) ? DeserializeObject<T>(strValue) : default;
        }

        private string GetStringFromCookie(string key)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(COOKIE_NAME, out var cookieValue))
            {
                return cookieValue;
            }
            return string.Empty;
        }

        public void SetObjectInCookie<T>(string key, T value, int expireDays = 30)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(expireDays),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };

            string serializedValue = SerializeObject(value);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, serializedValue, options);
        }

        public void RemoveCookie(string key)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
        }

        private static string SerializeObject<T>(T toSerialize)
        {
            return JsonSerializer.Serialize(toSerialize);
        }

        private static T DeserializeObject<T>(string objValue)
        {
            return JsonSerializer.Deserialize<T>(objValue);
        }
    }
}
