using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace eCommerce.Services
{
    public class eCommerceRoleManager : RoleManager<IdentityRole>
    {
        public eCommerceRoleManager(IRoleStore<IdentityRole> roleStore, ILogger<RoleManager<IdentityRole>> logger)
            : base(roleStore, null, null, null, logger)
        {
        }

        public static eCommerceRoleManager Create(IServiceProvider serviceProvider)
        {
            var roleStore = serviceProvider.GetRequiredService<IRoleStore<IdentityRole>>();
            var logger = serviceProvider.GetRequiredService<ILogger<RoleManager<IdentityRole>>>();

            return new eCommerceRoleManager(roleStore, logger);
        }
    }
}
