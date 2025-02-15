using eCommerce.Entities;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Shared.Helpers
{
    public class AppDataHelper
    {
        private const string IS_INITIALIZED = "IS_INITIALIZED";
        private const string CURRENT_LANGUAGE = "CURRENT_LANGUAGE";
        private const string IS_RTL = "IS_RTL";

        private readonly SharedURLHelper _sharedURLHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDataHelper(SharedURLHelper sharedURLHelper, IHttpContextAccessor httpContextAccessor)
        {
            _sharedURLHelper = sharedURLHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        public void Populate()
        {
            if (IsInitialized)
            {
                return;
            }

            // Get the requested Language OR set 'en' as default if nothing specified
            string languageShortName = _sharedURLHelper.GetLanguageUrlComponent();
            var language = LanguagesHelper.EnabledLanguages.FirstOrDefault(x => x.ShortCode == languageShortName);

            bool forceUpdate = false;

            if (language == null)
            {
                CurrentLanguage = LanguagesHelper.DefaultLanguage;
                forceUpdate = true;
            }
            else
            {
                CurrentLanguage = language;
            }

            if (CurrentLanguage == null)
            {
                throw new Exception("NO LANGUAGE DETECTED");
            }

            _sharedURLHelper.TryAddRouteKeyValue("lang", CurrentLanguage.ShortCode, forceUpdate);
            IsRTL = CurrentLanguage.IsRTL;

            IsInitialized = true;
        }

        public bool IsInitialized
        {
            get => _httpContextAccessor.HttpContext?.Items[IS_INITIALIZED] as bool? ?? false;
            set => _httpContextAccessor.HttpContext.Items[IS_INITIALIZED] = value;
        }

        public Language CurrentLanguage
        {
            get => _httpContextAccessor.HttpContext?.Items[CURRENT_LANGUAGE] as Language;
            set => _httpContextAccessor.HttpContext.Items[CURRENT_LANGUAGE] = value;
        }

        public bool IsRTL
        {
            get => _httpContextAccessor.HttpContext?.Items[IS_RTL] as bool? ?? false;
            set => _httpContextAccessor.HttpContext.Items[IS_RTL] = value;
        }

        public bool IsMobile
        {
            get
            {
                try
                {
                    var httpContext = _httpContextAccessor.HttpContext;
                    if (httpContext?.Request.Headers["User-Agent"].ToString().ToLower().Contains("mobi") == true && !IsTablet)
                    {
                        return true;
                    }
                }
                catch { }

                return false;
            }
        }

        public bool IsTablet
        {
            get
            {
                try
                {
                    var httpContext = _httpContextAccessor.HttpContext;
                    var userAgent = httpContext?.Request.Headers["User-Agent"].ToString().ToLower().Trim();
                    return !string.IsNullOrEmpty(userAgent) && userAgent.Contains("ipad");
                }
                catch { }

                return false;
            }
        }
    }
}
