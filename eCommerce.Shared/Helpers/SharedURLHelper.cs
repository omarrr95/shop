using eCommerce.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace eCommerce.Shared.Helpers
{
    public class SharedURLHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUrlHelper _urlHelper;

        public SharedURLHelper(IHttpContextAccessor contextAccessor, IUrlHelper urlHelper)
        {
            _contextAccessor = contextAccessor;
            _urlHelper = urlHelper;
        }

        public string GetLanguageUrlComponent()
        {
            var languageShortName = GetUrlValueByKey("lang");
            return languageShortName;
        }

        public string GetUrlComponentByKey(string key)
        {
            return GetUrlValueByKey(key);
        }

        private string GetUrlValueByKey(string key)
        {
            var routeValues = _contextAccessor.HttpContext?.GetRouteData()?.Values;

            if (routeValues != null && routeValues.TryGetValue(key, out var value))
            {
                return value?.ToString() ?? string.Empty;
            }

            return string.Empty;
        }

        public bool TryAddRouteKeyValue(string key, string value, bool forceUpdate = false)
        {
            var routeValues = _contextAccessor.HttpContext?.GetRouteData()?.Values;

            if (routeValues == null) return false;

            if (!routeValues.ContainsKey(key))
            {
                routeValues[key] = value;
                return true;
            }
            else if (forceUpdate)
            {
                routeValues[key] = value;
                return true;
            }

            return false;
        }

        public string LanguageBasedURL(string langShortCode)
        {
            var httpContext = _contextAccessor.HttpContext;
            if (httpContext == null) return string.Empty;

            var routeValues = httpContext.GetRouteData()?.Values;
            string queryString = httpContext.Request.QueryString.HasValue ? httpContext.Request.QueryString.Value : string.Empty;

            string routeUrl = httpContext.Request.Path.Value?.ToLower().Trim() ?? string.Empty;

            if (routeValues != null)
            {
                foreach (var routeValue in routeValues)
                {
                    string key = $"{{{routeValue.Key.ToLower().Trim()}}}";

                    if (routeValue.Key.Equals("lang", StringComparison.OrdinalIgnoreCase))
                    {
                        routeUrl = routeUrl.Replace(key, langShortCode);
                    }
                    else
                    {
                        routeUrl = routeUrl.Replace(key, routeValue.Value.ToString().Trim());
                    }
                }
            }

            routeUrl = !string.IsNullOrEmpty(routeUrl) ? routeUrl.ReplaceUnpassedRouteValues() : string.Empty;

            return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{routeUrl}{queryString}".ToLower();
        }
    }
}
