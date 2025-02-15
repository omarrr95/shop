using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eCommerce.Shared.Helpers
{
    public static class DashboardURLsHelper
    {
        public static string Dashboard(this IUrlHelper helper, ConfigurationsHelper configurationsHelper)
        {
            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.Action("Index", "Dashboard", new { area = "Admin" })
                : helper.Action("Index", "Dashboard");

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string ListAction(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, string controller, string searchTerm = "", int? pageNo = 0)
        {
            var routeValues = new
            {
                searchTerm,
                pageNo = pageNo > 1 ? pageNo : null
            };

            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.Action("List", controller, routeValues)
                : helper.Action("List", controller, routeValues);

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string Categories(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, string searchTerm = "", int? pageNo = 0, int? parentCategoryID = 0)
        {
            var routeValues = new
            {
                searchTerm,
                parentCategoryID = parentCategoryID > 0 ? parentCategoryID : null,
                pageNo = pageNo > 1 ? pageNo : null
            };

            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.Action("Categories", "Category", routeValues)
                : helper.Action("Categories", "Category", routeValues);

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string Products(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, string searchTerm = "", int? pageNo = 0, int? categoryID = 0, bool? showOnlyLowStock = false)
        {
            var routeValues = new
            {
                searchTerm,
                categoryID = categoryID > 0 ? categoryID : null,
                showOnlyLowStock = showOnlyLowStock == true ? showOnlyLowStock : null,
                pageNo = pageNo > 1 ? pageNo : null
            };

            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.Action("Products", "Product", routeValues)
                : helper.Action("Products", "Product", routeValues);

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string Orders(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, string userID = "", int? orderID = 0, int? orderStatus = 0, int? pageNo = 0)
        {
            var routeValues = new
            {
                userID,
                orderID = orderID > 0 ? orderID : null,
                orderStatus = orderStatus > 0 ? orderStatus : null,
                pageNo = pageNo > 1 ? pageNo : null
            };

            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.Action("Orders", "Order", routeValues)
                : helper.Action("Orders", "Order", routeValues);

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string UpdateOrderStatus(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, int orderID)
        {
            var routeValues = new { orderID };

            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.Action("UpdateOrderStatus", "Order", routeValues)
                : helper.Action("UpdateOrderStatus", "Order", routeValues);

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string Users(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, string searchTerm = "", string roleID = "", int? pageNo = 0)
        {
            var routeValues = new
            {
                searchTerm,
                roleID,
                pageNo = pageNo > 1 ? pageNo : null
            };

            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.Action("Users", "User", routeValues)
                : helper.Action("Users", "User", routeValues);

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string UserDetails(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, string userID)
        {
            var routeValues = new { userID };

            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.Action("Details", "User", routeValues)
                : helper.Action("Details", "User", routeValues);

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string RoleDetails(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, string roleID)
        {
            var routeValues = new { roleID };

            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.Action("RoleDetails", "Role", routeValues)
                : helper.Action("RoleDetails", "Role", routeValues);

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string RoleUsers(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, string roleID, int? pageNo = 0)
        {
            var routeValues = new
            {
                roleID,
                pageNo = pageNo > 1 ? pageNo : null
            };

            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.Action("RoleUsers", "Role", routeValues)
                : helper.Action("RoleUsers", "Role", routeValues);

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string AssignUserRole(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, string userID)
        {
            var routeValues = new { userID };

            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.Action("AssignUserRole", "User", routeValues)
                : helper.Action("AssignUserRole", "User", routeValues);

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }

        public static string RemoveUserRole(this IUrlHelper helper, ConfigurationsHelper configurationsHelper, string userID)
        {
            var routeValues = new { userID };

            string routeURL = configurationsHelper.EnableMultilingual
                ? helper.Action("RemoveUserRole", "User", routeValues)
                : helper.Action("RemoveUserRole", "User", routeValues);

            return WebUtility.UrlDecode(routeURL)?.ToLower();
        }
    }
}
