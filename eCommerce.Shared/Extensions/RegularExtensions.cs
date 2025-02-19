using eCommerce.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace eCommerce.Shared.Extensions
{
    public static class RegularExtensions
    {
        private static readonly string sanitizingPattern = @"[^a-zA-Z0-9-]";
        private static readonly string multipleHyphenCharacterReplacePattern = @"-{2,}";
        private static readonly string emailPattern = @"(?<=^[A-Za-z0-9]{3}).*?(?=@)";
        private static readonly Regex upperCamelCaseRegex = new(@"(?<!^)((?<!\d)\d|(?(?<=[A-Z])[A-Z](?=[a-z])|[A-Z]))", RegexOptions.Compiled);
        private static readonly Regex curlyStringContainers = new(@"{([^}]*)}", RegexOptions.Compiled);

        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static string SanitizeString(this string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;

            var sanitizedString = Regex.Replace(str.Trim(), sanitizingPattern, "-");
            return Regex.Replace(sanitizedString, multipleHyphenCharacterReplacePattern, "-");
        }

        public static string SanitizeLowerString(this string str) => str.SanitizeString().ToLower();

        public static string SanitizeLowerString(this string str, int characterLimit)
        {
            var sanitized = str.SanitizeString().ToLower();
            return sanitized.Length >= characterLimit ? sanitized[..characterLimit] : sanitized;
        }

        public static string ToSiteURL(this string pageURL)
        {
            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null) return pageURL;

            var request = httpContext.Request;
            return $"{request.Scheme}://{request.Host}{pageURL}";
        }

        public static string ToAuthorizeNetProductName(this string productName) =>
            string.IsNullOrEmpty(productName) ? string.Empty :
            productName.Length > 31 ? productName.Substring(0, 30) : productName;

        public static string WithCurrency(this decimal price, ConfigurationsHelper configurationsHelper)
        {
            return configurationsHelper.PriceCurrencyPosition
                .Replace("{price}", price.ToDecimalWithPoints(configurationsHelper.DigitsAfterDecimalPoint))
                .Replace("{currency}", configurationsHelper.CurrencySymbol);
        }

        public static string ToDecimalWithPoints(this decimal price, int digitsAfterDecimalPoints) =>
            price.ToString($"0.{new string('0', digitsAfterDecimalPoints)}");

        public static string GetSubstringText(this string str, string start, string end)
        {
            try
            {
                var startIndex = !string.IsNullOrEmpty(start) ? str.IndexOf(start, StringComparison.Ordinal) + 1 : 0;
                var endIndex = !string.IsNullOrEmpty(end) ? str.IndexOf(end, StringComparison.Ordinal) : str.Length;
                return str.Substring(startIndex, endIndex - startIndex).Trim();
            }
            catch
            {
                return null;
            }
        }

        public static string IfNullOrEmptyShowAlternative(this string str, string alternativeStr) =>
            string.IsNullOrWhiteSpace(str) ? alternativeStr : str;

        public static string SafeTrim(this string str) => string.IsNullOrWhiteSpace(str) ? str : str.Trim();

        public static string SafeSubstring(this string str, int length, string appendString = "") =>
            string.IsNullOrWhiteSpace(str) || str.Length <= length ? str : str.Substring(0, length) + appendString;

        public static string UpperCaseFirstLetter(this string str) =>
            string.IsNullOrEmpty(str) ? str : char.ToUpper(str[0]) + str.Substring(1);

        public static string MakeWord(this string str) => upperCamelCaseRegex.Replace(str, " $1");

        public static string ReplaceUnpassedRouteValues(this string str) =>
            curlyStringContainers.Replace(str, string.Empty);

        public static string HideEmail(this string email, string replaceWith = "*")
        {
            if (string.IsNullOrEmpty(email)) return string.Empty;

            var match = Regex.Match(email, emailPattern);
            return match.Success && !string.IsNullOrEmpty(match.Value)
                ? email.Replace(match.Value, new string('*', match.Length))
                : email;
        }
    }
}
