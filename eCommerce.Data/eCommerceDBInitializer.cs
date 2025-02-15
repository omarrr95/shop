using eCommerce.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Data
{
    public class eCommerceDBInitializer
    {
        private readonly eCommerceContext _context;
        private readonly UserManager<eCommerceUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public eCommerceDBInitializer(eCommerceContext context, UserManager<eCommerceUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            await _context.Database.MigrateAsync();
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedLanguagesAsync();
            await SeedCategoriesAsync();
            await SeedConfigurationsAsync();
        }

        private async Task SeedRolesAsync()
        {
            string[] roles = { "Administrator", "Moderator", "User" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private async Task SeedUsersAsync()
        {
            var adminEmail = "adm_use@domain.com";
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new eCommerceUser
                {
                    FullName = "Admin",
                    Email = adminEmail,
                    UserName = "adm_use",
                    PhoneNumber = "(312)555-0690",
                    Country = "Adminsburg",
                    City = "Adminstria",
                    Address = "404 Block D, Adm Street",
                    ZipCode = "123456",
                    RegisteredOn = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(adminUser, "adm_use123");

                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(adminUser, new[] { "Administrator", "Moderator", "User" });
                }
            }
        }

        private async Task SeedLanguagesAsync()
        {
            if (!await _context.Languages.AnyAsync())
            {
                var languages = new List<Language>
                {
                    new Language { Name = "English", ShortCode = "en", IsEnabled = true, IsDefault = true, IconCode = "GB.png" },
                    new Language { Name = "عربى", ShortCode = "ar", IsEnabled = true, IsDefault = false, IsRTL = true, IconCode = "SA.png" }
                };

                await _context.Languages.AddRangeAsync(languages);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedCategoriesAsync()
        {
            if (!await _context.Categories.AnyAsync())
            {
                var uncategorized = new Category
                {
                    SanitizedName = "uncategorized",
                    DisplaySeqNo = 0,
                    ModifiedOn = DateTime.UtcNow
                };

                await _context.Categories.AddAsync(uncategorized);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedConfigurationsAsync()
        {
            if (!await _context.Configurations.AnyAsync())
            {
                var configurations = new List<Configuration>
                {
                    new Configuration { Key = "ApplicationName", Value = "Your Store", Description = "This is the application name.", ConfigurationType = (int)ConfigurationTypes.Site, ModifiedOn = DateTime.UtcNow },
                    new Configuration { Key = "Email", Value = "contact@email.com", Description = "Company email.", ConfigurationType = (int)ConfigurationTypes.Site, ModifiedOn = DateTime.UtcNow }
                };

                await _context.Configurations.AddRangeAsync(configurations);
                await _context.SaveChangesAsync();
            }
        }
    }
}
