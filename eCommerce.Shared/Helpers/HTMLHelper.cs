using eCommerce.Entities;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace eCommerce.Shared.Helpers
{
    public static class HTMLHelper
    {
        public static IHtmlContent MenuItemClass(this IHtmlHelper htmlHelper, string controllerName, string actionName = "")
        {
            var currentController = htmlHelper.ViewContext.RouteData.Values["controller"]?.ToString();

            if (string.Equals(controllerName, currentController, StringComparison.CurrentCultureIgnoreCase))
            {
                if (!string.IsNullOrEmpty(actionName))
                {
                    var currentAction = htmlHelper.ViewContext.RouteData.Values["action"]?.ToString();

                    return string.Equals(actionName, currentAction, StringComparison.CurrentCultureIgnoreCase)
                        ? new HtmlString("active")
                        : new HtmlString("");
                }
                return new HtmlString("active");
            }
            return new HtmlString("");
        }

        public static string GetCellBackgroundClassByOrderStatus(this IHtmlHelper htmlHelper, OrderStatus orderStatus)
        {
            return orderStatus switch
            {
                OrderStatus.Placed => "bg-primary text-white",
                OrderStatus.Processing or OrderStatus.WaitingForPayment => "bg-info text-white",
                OrderStatus.Delivered => "bg-success text-white",
                OrderStatus.Failed or OrderStatus.Cancelled => "bg-danger text-white",
                OrderStatus.OnHold or OrderStatus.Refunded => "bg-warning",
                _ => string.Empty
            };
        }

        public static string GetCellBackgroundClassByLanguageStatus(this IHtmlHelper htmlHelper, bool enabled)
        {
            return enabled ? "bg-success text-white" : "bg-danger text-white";
        }

        public static string GetFontAwesomeIconForSocialMediaProvider(this IHtmlHelper htmlHelper, string socialMediaProvider)
        {
            return socialMediaProvider switch
            {
                "Facebook" => "fab fa-facebook-f",
                "Twitter" => "fab fa-twitter",
                "Google" => "fab fa-google",
                "Microsoft" => "fab fa-microsoft",
                _ => string.Empty
            };
        }

        public static string GetButtonBackgroundClassForSocialMediaProvider(this IHtmlHelper htmlHelper, string socialMediaProvider)
        {
            return socialMediaProvider switch
            {
                "Facebook" => "bg-primary",
                "Twitter" => "bg-info",
                "Google" => "bg-danger",
                "Microsoft" => "bg-success",
                _ => string.Empty
            };
        }
    }
}
