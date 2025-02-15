using eCommerce.Shared.Enums;
using Microsoft.Extensions.Configuration;

namespace eCommerce.Shared.Helpers
{
    public class ConfigurationsHelper
    {
        private readonly IConfiguration _configuration;

        public ConfigurationsHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T GetConfigValue<T>(string key)
        {
            try
            {
                var value = _configuration[key];

                if (!string.IsNullOrEmpty(value))
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
                else
                {
                    return default;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Cannot convert configuration value for '{key}' to {typeof(T)}", e);
            }
        }

        public string ApplicationName => GetConfigValue<string>("ApplicationName");
        public string ApplicationIntro => GetConfigValue<string>("ApplicationIntro");
        public string AddressLine1 => GetConfigValue<string>("AddressLine1");
        public string AddressLine2 => GetConfigValue<string>("AddressLine2");
        public string PhoneNumber => GetConfigValue<string>("PhoneNumber");
        public string MobileNumber => GetConfigValue<string>("MobileNumber");
        public string Email => GetConfigValue<string>("Email");
        public string AdminEmailAddress => GetConfigValue<string>("AdminEmailAddress");
        public string FacebookURL => GetConfigValue<string>("FacebookURL");
        public string TwitterUsername => GetConfigValue<string>("TwitterUsername");
        public string TwitterURL => GetConfigValue<string>("TwitterURL");
        public string WhatsAppNumber => GetConfigValue<string>("WhatsAppNumber");
        public string InstagramURL => GetConfigValue<string>("InstagramURL");
        public string YouTubeURL => GetConfigValue<string>("YouTubeURL");
        public string LinkedInURL => GetConfigValue<string>("LinkedInURL");
        public string CurrencySymbol => GetConfigValue<string>("CurrencySymbol");
        public string PriceCurrencyPosition => GetConfigValue<string>("PriceCurrencyPosition");
        public bool EnableCreditCardPayment => GetConfigValue<bool>("EnableCreditCardPayment");
        public bool EnableCashOnDeliveryMethod => GetConfigValue<bool>("EnableCashOnDeliveryMethod");
        public bool EnablePayPalPaymentMethod => GetConfigValue<bool>("EnablePayPalPaymentMethod");
        public string PayPalClientID => GetConfigValue<string>("PayPalClientID");
        public decimal FlatDeliveryCharges => GetConfigValue<decimal>("FlatDeliveryCharges");
        public int DigitsAfterDecimalPoint => GetConfigValue<int>("DigitsAfterDecimalPoint");
        public int DefaultRating => 5;
        public bool EnableMultilingual => GetConfigValue<bool>("EnableMultilingual");
        public string DefaultLanguage => GetConfigValue<string>("DefaultLanguage");
        public string AuthorizeNet_ApiLoginID => GetConfigValue<string>("AuthorizeNet_ApiLoginID");
        public string AuthorizeNet_ApiTransactionKey => GetConfigValue<string>("AuthorizeNet_ApiTransactionKey");
        public bool AuthorizeNet_SandBox => GetConfigValue<bool>("AuthorizeNet_SandBox");
        public string SendGrid_APIKey => GetConfigValue<string>("SendGrid_APIKey");
        public string SendGrid_FromEmailAddress => GetConfigValue<string>("SendGrid_FromEmailAddress");
        public string SendGrid_FromEmailAddressName => GetConfigValue<string>("SendGrid_FromEmailAddressName");
        public string GoogleAnalyticsScript => GetConfigValue<string>("GoogleAnalyticsScript");
        public string FacebookMessengerScript => GetConfigValue<string>("FacebookMessengerScript");

        public Environments Environment
        {
            get
            {
                var env = _configuration["Environment"];

                return env switch
                {
                    "QA" => Environments.LIVE,
                    "STAGING" => Environments.STAGING,
                    "DEMO" => Environments.DEMO,
                    _ => Environments.LIVE,
                };
            }
        }
    }
}
