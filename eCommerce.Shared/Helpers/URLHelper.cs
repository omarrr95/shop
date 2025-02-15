using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eCommerce.Shared.Helpers
{
    public static class URLHelper
    {
        public static string Home(this IUrlHelper helper, ConfigurationsHelper configurationsHelper)
        {
            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.RouteUrl("LanguageBased_Home")
                : helper.RouteUrl("Home");

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string StaticPage(this IUrlHelper helper, string pageId, ConfigurationsHelper configurationsHelper)
        {
            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.RouteUrl($"LanguageBased_{pageId}")
                : helper.RouteUrl(pageId);

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string SubscribeNewsLetter(this IUrlHelper helper, ConfigurationsHelper configurationsHelper)
        {
            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.RouteUrl("LanguageBased_SubscribeNewsLetter")
                : helper.RouteUrl("SubscribeNewsLetter");

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string SubmitContactForm(this IUrlHelper helper, ConfigurationsHelper configurationsHelper)
        {
            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.RouteUrl("LanguageBased_SubmitContactForm")
                : helper.RouteUrl("SubmitContactForm");

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string Register(this IUrlHelper helper, ConfigurationsHelper configurationsHelper)
        {
            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.RouteUrl("LanguageBased_Register")
                : helper.RouteUrl("Register");

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string Login(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, string returnUrl = "")
        {
            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.RouteUrl("LanguageBased_Login", new { returnUrl })
                : helper.RouteUrl("Login", new { returnUrl });

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string ForgotPassword(this IUrlHelper helper, ConfigurationsHelper configurationsHelper)
        {
            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.RouteUrl("LanguageBased_ForgotPassword")
                : helper.RouteUrl("ForgotPassword");

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string ResetPassword(this IUrlHelper helper, ConfigurationsHelper configurationsHelper)
        {
            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.RouteUrl("LanguageBased_ResetPassword")
                : helper.RouteUrl("ResetPassword");

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string SearchProducts(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, string category = "", string q = "", decimal? from = null, decimal? to = null, string sortby = "", int? pageNo = 1, int? recordSize = null)
        {
            var routeValues = new
            {
                category,
                q,
                from = from > 0 ? from : null,
                to = to > 0 ? to : null,
                sortby,
                pageNo = pageNo > 1 ? pageNo : null,
                recordSize = recordSize > 1 ? recordSize : null
            };

            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.RouteUrl("LanguageBased_SearchProducts", routeValues)
                : helper.RouteUrl("SearchProducts", routeValues);

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string ProductDetails(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, string category, int id, string sanitizedTitle = "")
        {
            var routeValues = new { category, id, sanitizedTitle };

            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.RouteUrl("LanguageBased_ProductDetails", routeValues)
                : helper.RouteUrl("ProductDetails", routeValues);

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string UserProfile(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, string tab = "")
        {
            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.RouteUrl("LanguageBased_UserProfile", new { tab })
                : helper.RouteUrl("UserProfile", new { tab });

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string Checkout(this IUrlHelper helper, ConfigurationsHelper configurationsHelper)
        {
            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.RouteUrl("LanguageBased_Checkout")
                : helper.RouteUrl("Checkout");

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string PlaceOrder(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, bool isCashOnDelivery = false, bool isPayPal = false)
        {
            string routeKey = isCashOnDelivery ? "PlaceOrderViaCashOnDelivery" :
                              isPayPal ? "PlaceOrderViaPayPal" :
                              "PlaceOrder";

            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.RouteUrl($"LanguageBased_{routeKey}")
                : helper.RouteUrl(routeKey);

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string OrderTrack(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, string orderId = "", bool orderPlaced = false)
        {
            var routeValues = new { orderId, orderPlaced };

            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.RouteUrl("LanguageBased_OrderTrack", routeValues)
                : helper.RouteUrl("OrderTrack", routeValues);

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string PrintInvoice(this IUrlHelper helper, int orderId, ConfigurationsHelper configurationsHelper)
        {
            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.RouteUrl("LanguageBased_PrintInvoice", new { orderId })
                : helper.RouteUrl("PrintInvoice", new { orderId });

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }
    }
}
