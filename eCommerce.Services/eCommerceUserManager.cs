using eCommerce.Data;
using eCommerce.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace eCommerce.Services
{
    public class eCommerceUserManager : UserManager<eCommerceUser>
    {
        public eCommerceUserManager(
            IUserStore<eCommerceUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<eCommerceUser> passwordHasher,
            IEnumerable<IUserValidator<eCommerceUser>> userValidators,
            IEnumerable<IPasswordValidator<eCommerceUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<eCommerceUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public static eCommerceUserManager Create(IServiceProvider serviceProvider)
        {
            var userStore = serviceProvider.GetRequiredService<IUserStore<eCommerceUser>>();
            var options = serviceProvider.GetRequiredService<IOptions<IdentityOptions>>();
            var passwordHasher = serviceProvider.GetRequiredService<IPasswordHasher<eCommerceUser>>();
            var userValidators = serviceProvider.GetRequiredService<IEnumerable<IUserValidator<eCommerceUser>>>();
            var passwordValidators = serviceProvider.GetRequiredService<IEnumerable<IPasswordValidator<eCommerceUser>>>();
            var keyNormalizer = serviceProvider.GetRequiredService<ILookupNormalizer>();
            var errorDescriber = serviceProvider.GetRequiredService<IdentityErrorDescriber>();
            var logger = serviceProvider.GetRequiredService<ILogger<UserManager<eCommerceUser>>>();

            var manager = new eCommerceUserManager(
                userStore,
                options,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errorDescriber,
                serviceProvider,
                logger);

            // Configure user validation
            manager.Options.User.RequireUniqueEmail = true;
            manager.Options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

            // Configure password validation
            manager.Options.Password.RequireDigit = false;
            manager.Options.Password.RequiredLength = 4;
            manager.Options.Password.RequireNonAlphanumeric = false;
            manager.Options.Password.RequireUppercase = false;
            manager.Options.Password.RequireLowercase = false;

            // Configure account lockout
            manager.Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.Options.Lockout.MaxFailedAccessAttempts = 5;
            manager.Options.Lockout.AllowedForNewUsers = true;

            return manager;
        }
    }
}
