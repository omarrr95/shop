using eCommerce.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace eCommerce.Services
{
    public class eCommerceSignInManager : SignInManager<eCommerceUser>
    {
        public eCommerceSignInManager(
            UserManager<eCommerceUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<eCommerceUser> claimsFactory,
            ILogger<SignInManager<eCommerceUser>> logger)
            : base(userManager, contextAccessor, claimsFactory, null, null, null, logger)
        {
        }

        public override async Task<ClaimsPrincipal> CreateUserPrincipalAsync(eCommerceUser user)
        {
            return await ClaimsFactory.CreateAsync(user);
        }

        public static eCommerceSignInManager Create(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<eCommerceUser>>();
            var contextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var claimsFactory = serviceProvider.GetRequiredService<IUserClaimsPrincipalFactory<eCommerceUser>>();
            var logger = serviceProvider.GetRequiredService<ILogger<SignInManager<eCommerceUser>>>();

            return new eCommerceSignInManager(userManager, contextAccessor, claimsFactory, logger);
        }
    }
}
