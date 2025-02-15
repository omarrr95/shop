using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace eCommerce.Shared.Attributes
{
    public class CustomAuthorize : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user?.Identity == null || !user.Identity.IsAuthenticated)
            {
                // User is not logged in, redirect to login page
                context.Result = new UnauthorizedResult();
            }
            else
            {
                // User is authenticated but does not have access, redirect to custom unauthorized page
                context.Result = new RedirectToActionResult("UnAuthorized", "Home", null);
            }
        }
    }
}
