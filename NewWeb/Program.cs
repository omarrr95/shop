using System.Web.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using eCommerce.Data;
using eCommerce.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AreaRegistrationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// Configure ASP.NET Core Identity
//builder.Services.AddIdentity<eCommerceUser, IdentityRole>()
//    .AddEntityFrameworkStores<eCommerceContext>()
//    .AddDefaultTokenProviders();

// Configure Authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/access-denied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});

// Add authentication services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie()
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
                app.MapControllerRoute(
                name: "Register",
                pattern: "register",
                defaults: new { area = "", controller = "Users", action = "Register" }
                
            );
app.MapControllerRoute(
    name: "LanguageBased_Register",
    pattern: "{lang}/register",
    defaults: new { area = "", controller = "Users", action = "Register" }
    
);

app.MapControllerRoute(
    name: "Login",
    pattern: "login",
    defaults: new { area = "", controller = "Users", action = "Login" }
    
);
app.MapControllerRoute(
    name: "LanguageBased_Login",
    pattern: "{lang}/login",
    defaults: new { area = "", controller = "Users", action = "Login" }
    
);

app.MapControllerRoute(
    name: "SocialLogin",
    pattern: "social-login",
    defaults: new { area = "", controller = "Users", action = "SocialLogin" }
    
);
app.MapControllerRoute(
    name: "LanguageBased_SocialLogin",
    pattern: "{lang}/social-login",
    defaults: new { area = "", controller = "Users", action = "SocialLogin" }
    
);

app.MapControllerRoute(
    name: "SocialLoginCallback",
    pattern: "social-login-callback",
    defaults: new { area = "", controller = "Users", action = "SocialLoginCallback" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_SocialLoginCallback",
    pattern: "{lang}/social-login-callback",
    defaults: new { area = "", controller = "Users", action = "SocialLoginCallback" }
    
);

app.MapControllerRoute(
    name: "ForgotPassword",
    pattern: "forgot-password",
    defaults: new { area = "", controller = "Users", action = "ForgotPassword" }
    
);
app.MapControllerRoute(
    name: "LanguageBased_ForgotPassword",
    pattern: "{lang}/forgot-password",
    defaults: new { area = "", controller = "Users", action = "ForgotPassword" }
    
);

app.MapControllerRoute(
    name: "ResetPassword",
    pattern: "reset-password",
    defaults: new { area = "", controller = "Users", action = "ResetPassword" }
    
);
app.MapControllerRoute(
    name: "LanguageBased_ResetPassword",
    pattern: "{lang}/reset-password",
    defaults: new { area = "", controller = "Users", action = "ResetPassword" }
    
);

app.MapControllerRoute(
    name: "Logoff",
    pattern: "logoff",
    defaults: new { area = "", controller = "Users", action = "LogOff" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_Logoff",
    pattern: "{lang}/logoff",
    defaults: new { area = "", controller = "Users", action = "LogOff" }
    
);

app.MapControllerRoute(
    name: "AboutUs",
    pattern: "about-us",
    defaults: new { area = "", controller = "Contents", action = "AboutUs" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_AboutUs",
    pattern: "{lang}/about-us",
    defaults: new { area = "", controller = "Contents", action = "AboutUs" }
    
);

app.MapControllerRoute(
    name: "ContactUs",
    pattern: "contact-us",
    defaults: new { area = "", controller = "Contents", action = "ContactUs" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_ContactUs",
    pattern: "{lang}/contact-us",
    defaults: new { area = "", controller = "Contents", action = "ContactUs" }
    
);

app.MapControllerRoute(
    name: "Blog",
    pattern: "blog",
    defaults: new { area = "", controller = "Contents", action = "Blog" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_Blog",
    pattern: "{lang}/blog",
    defaults: new { area = "", controller = "Contents", action = "Blog" }
    
);

app.MapControllerRoute(
    name: "PrivacyPolicy",
    pattern: "privacy-policy",
    defaults: new { area = "", controller = "Contents", action = "PrivacyPolicy" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_PrivacyPolicy",
    pattern: "{lang}/privacy-policy",
    defaults: new { area = "", controller = "Contents", action = "PrivacyPolicy" }
    
);

app.MapControllerRoute(
    name: "RefundPolicy",
    pattern: "refund-policy",
    defaults: new { area = "", controller = "Contents", action = "RefundPolicy" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_RefundPolicy",
    pattern: "{lang}/refund-policy",
    defaults: new { area = "", controller = "Contents", action = "RefundPolicy" }
    
);

app.MapControllerRoute(
    name: "TermsConditions",
    pattern: "terms-conditions",
    defaults: new { area = "", controller = "Contents", action = "TermsConditions" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_TermsConditions",
    pattern: "{lang}/terms-conditions",
    defaults: new { area = "", controller = "Contents", action = "TermsConditions" }
    
);

app.MapControllerRoute(
    name: "SearchProducts",
    pattern: "search/{category}",
    defaults: new { area = "", controller = "Home", action = "Search", category = UrlParameter.Optional }
    
);

app.MapControllerRoute(
    name: "LanguageBased_SearchProducts",
    pattern: "{lang}/search/{category}",
    defaults: new { area = "", controller = "Home", action = "Search", category = UrlParameter.Optional }
    
);

app.MapControllerRoute(
    name: "ProductDetails",
    pattern: "{category}/product/{ID}/{sanitizedtitle}",
    defaults: new { area = "", controller = "Products", action = "Details", sanitizedtitle = UrlParameter.Optional }
    
);

app.MapControllerRoute(
    name: "LanguageBased_ProductDetails",
    pattern: "{lang}/{category}/product/{ID}/{sanitizedtitle}",
    defaults: new { area = "", controller = "Products", action = "Details", sanitizedtitle = UrlParameter.Optional }
    
);

app.MapControllerRoute(
    name: "UserProfile",
    pattern: "user/profile",
    defaults: new { area = "", controller = "Users", action = "UserProfile" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_UserProfile",
    pattern: "{lang}/user/profile",
    defaults: new { area = "", controller = "Users", action = "UserProfile" }
    
);

app.MapControllerRoute(
    name: "UpdateProfile",
    pattern: "user/update-profile",
    defaults: new { area = "", controller = "Users", action = "UpdateProfile" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_UpdateProfile",
    pattern: "{lang}/user/update-profile",
    defaults: new { area = "", controller = "Users", action = "UpdateProfile" }
    
);

app.MapControllerRoute(
    name: "ChangePassword",
    pattern: "user/change-password",
    defaults: new { area = "", controller = "Users", action = "ChangePassword" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_ChangePassword",
    pattern: "{lang}/user/change-password",
    defaults: new { area = "", controller = "Users", action = "ChangePassword" }
    
);

app.MapControllerRoute(
    name: "UpdatePassword",
    pattern: "user/update-password",
    defaults: new { area = "", controller = "Users", action = "UpdatePassword" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_UpdatePassword",
    pattern: "{lang}/user/update-password",
    defaults: new { area = "", controller = "Users", action = "UpdatePassword" }
    
);

app.MapControllerRoute(
    name: "ChangeAvatar",
    pattern: "user/change-avatar",
    defaults: new { area = "", controller = "Users", action = "ChangeAvatar" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_ChangeAvatar",
    pattern: "{lang}/user/change-avatar",
    defaults: new { area = "", controller = "Users", action = "ChangeAvatar" }
    
);

app.MapControllerRoute(
    name: "UpdateAvatar",
    pattern: "user/update-avatar",
    defaults: new { area = "", controller = "Users", action = "UpdateAvatar" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_UpdateAvatar",
    pattern: "{lang}/user/update-avatar",
    defaults: new { area = "", controller = "Users", action = "UpdateAvatar" }
    
);

app.MapControllerRoute(
    name: "UserOrders",
    pattern: "user/orders",
    defaults: new { controller = "Orders", action = "UserOrders" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_UserOrders",
    pattern: "{lang}/user/orders",
    defaults: new { controller = "Orders", action = "UserOrders" }
    
);

app.MapControllerRoute(
    name: "Cart",
    pattern: "cart",
    defaults: new { area = "", controller = "Cart", action = "Cart" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_Cart",
    pattern: "{lang}/cart",
    defaults: new { area = "", controller = "Cart", action = "Cart" }
    
);

app.MapControllerRoute(
    name: "UpdateCart",
    pattern: "update-cart",
    defaults: new { area = "", controller = "Cart", action = "UpdateCart" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_UpdateCart",
    pattern: "{lang}/update-cart",
    defaults: new { area = "", controller = "Cart", action = "UpdateCart" }
    
);

app.MapControllerRoute(
    name: "AddItemToCart",
    pattern: "add-to-cart",
    defaults: new { area = "", controller = "Cart", action = "AddItemToCart" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_AddItemToCart",
    pattern: "{lang}/add-to-cart",
    defaults: new { area = "", controller = "Cart", action = "AddItemToCart" }
    
);

app.MapControllerRoute(
    name: "GetCartItems",
    pattern: "get-cart-items",
    defaults: new { area = "", controller = "Cart", action = "GetCartItems" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_GetCartItems",
    pattern: "{lang}/get-cart-items",
    defaults: new { area = "", controller = "Cart", action = "GetCartItems" }
    
);

app.MapControllerRoute(
    name: "Checkout",
    pattern: "checkout",
    defaults: new { area = "", controller = "Cart", action = "Checkout" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_Checkout",
    pattern: "{lang}/checkout",
    defaults: new { area = "", controller = "Cart", action = "Checkout" }
    
);

app.MapControllerRoute(
    name: "DeliveryInfo",
    pattern: "delivery-info",
    defaults: new { area = "", controller = "Cart", action = "DeliveryInfo" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_DeliveryInfo",
    pattern: "{lang}/delivery-info",
    defaults: new { area = "", controller = "Cart", action = "DeliveryInfo" }
    
);

app.MapControllerRoute(
    name: "ConfirmOrder",
    pattern: "confirm-order",
    defaults: new { area = "", controller = "Cart", action = "ConfirmOrder" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_ConfirmOrder",
    pattern: "{lang}/confirm-order",
    defaults: new { area = "", controller = "Cart", action = "ConfirmOrder" }
    
);

app.MapControllerRoute(
    name: "PlaceOrder",
    pattern: "place-order",
    defaults: new { area = "", controller = "Cart", action = "PlaceOrder" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_PlaceOrder",
    pattern: "{lang}/place-order",
    defaults: new { area = "", controller = "Orders", action = "PlaceOrder" }
    
);

app.MapControllerRoute(
    name: "PlaceOrderViaCashOnDelivery",
    pattern: "place-order-cod",
    defaults: new { area = "", controller = "Orders", action = "PlaceOrderViaCashOnDelivery" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_PlaceOrderViaCashOnDelivery",
    pattern: "{lang}/place-order-cod",
    defaults: new { area = "", controller = "Orders", action = "PlaceOrderViaCashOnDelivery" }
    
);

app.MapControllerRoute(
    name: "PlaceOrderViaPayPal",
    pattern: "place-order-paypal",
    defaults: new { area = "", controller = "Orders", action = "PlaceOrderViaPayPal" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_PlaceOrderViaPayPal",
    pattern: "{lang}/place-order-paypal",
    defaults: new { area = "", controller = "Orders", action = "PlaceOrderViaPayPal" }
    
);

app.MapControllerRoute(
    name: "OrderTrack",
    pattern: "tracking",
    defaults: new { area = "", controller = "Orders", action = "Tracking" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_OrderTrack",
    pattern: "{lang}/tracking",
    defaults: new { area = "", controller = "Orders", action = "Tracking" }
    
);

app.MapControllerRoute(
    name: "PrintInvoice",
    pattern: "print-ivoice",
    defaults: new { area = "", controller = "Orders", action = "PrintInvoice" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_PrintInvoice",
    pattern: "{lang}/print-ivoice",
    defaults: new { area = "", controller = "Orders", action = "PrintInvoice" }
    
);

app.MapControllerRoute(
    name: "FeaturedProducts",
    pattern: "featured-products",
    defaults: new { area = "", controller = "Products", action = "FeaturedProducts" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_FeaturedProducts",
    pattern: "{lang}/featured-products",
    defaults: new { area = "", controller = "Products", action = "FeaturedProducts" }
    
);

app.MapControllerRoute(
    name: "RecentProducts",
    pattern: "recent-products",
    defaults: new { area = "", controller = "Products", action = "RecentProducts" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_RecentProducts",
    pattern: "{lang}/recent-products",
    defaults: new { area = "", controller = "Products", action = "RecentProducts" }
    
);

app.MapControllerRoute(
    name: "RelatedProducts",
    pattern: "related-products",
    defaults: new { area = "", controller = "Products", action = "RelatedProducts" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_RelatedProducts",
    pattern: "{lang}/related-products",
    defaults: new { area = "", controller = "Products", action = "RelatedProducts" }
    
);

app.MapControllerRoute(
    name: "CategoriesMenu",
    pattern: "categories-menu",
    defaults: new { area = "", controller = "Categories", action = "CategoriesMenu" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_CategoriesMenu",
    pattern: "{lang}/categories-menu",
    defaults: new { area = "", controller = "Categories", action = "CategoriesMenu" }
    
);

app.MapControllerRoute(
    name: "UploadPictures",
    pattern: "pictures/upload",
    defaults: new { area = "", controller = "Shared", action = "UploadPictures" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_UploadPictures",
    pattern: "{lang}/pictures/upload",
    defaults: new { area = "", controller = "Shared", action = "UploadPictures" }
    
);


app.MapControllerRoute(
    name: "UploadPicturesWithoutDatabase",
    pattern: "pictures/upload-nodb/{subFolder}",
    defaults: new { area = "", controller = "Shared", action = "UploadPicturesWithoutDatabase", subFolder = UrlParameter.Optional }
    
);

app.MapControllerRoute(
    name: "LanguageBased_UploadPicturesWithoutDatabase",
    pattern: "{lang}/pictures/upload-nodb/{subFolder}",
    defaults: new { area = "", controller = "Shared", action = "UploadPicturesWithoutDatabase", subFolder = UrlParameter.Optional }
    
);


app.MapControllerRoute(
    name: "LeaveComment",
    pattern: "add-comment",
    defaults: new { area = "", controller = "Comments", action = "LeaveComment" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_LeaveComment",
    pattern: "{lang}/add-comment",
    defaults: new { area = "", controller = "Comments", action = "LeaveComment" }
    
);

app.MapControllerRoute(
    name: "DeleteComment",
    pattern: "delete-comment",
    defaults: new { area = "", controller = "Comments", action = "DeleteComment" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_DeleteComment",
    pattern: "{lang}/delete-comment",
    defaults: new { area = "", controller = "Comments", action = "DeleteComment" }
    
);

app.MapControllerRoute(
    name: "UserComments",
    pattern: "user/comments",
    defaults: new { area = "", controller = "Comments", action = "UserComments" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_UserComments",
    pattern: "{lang}/user/comments",
    defaults: new { area = "", controller = "Comments", action = "UserComments" }
    
);

app.MapControllerRoute(
    name: "SubscribeNewsLetter",
    pattern: "newsletter-subscription",
    defaults: new { area = "", controller = "Home", action = "SubscribeNewsLetter" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_SubscribeNewsLetter",
    pattern: "{lang}/newsletter-subscription",
    defaults: new { area = "", controller = "Home", action = "SubscribeNewsLetter" }
    
);

app.MapControllerRoute(
    name: "SubmitContactForm",
    pattern: "contact-form-submit",
    defaults: new { area = "", controller = "Home", action = "SubmitContactForm" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_SubmitContactForm",
    pattern: "{lang}/contact-form-submit",
    defaults: new { area = "", controller = "Home", action = "SubmitContactForm" }
    
);

app.MapControllerRoute(
    name: "ChangeMode",
    pattern: "change-mode",
    defaults: new { area = "", controller = "Shared", action = "ChangeMode" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_ChangeMode",
    pattern: "{lang}/change-mode",
    defaults: new { area = "", controller = "Shared", action = "ChangeMode" }
    
);

app.MapControllerRoute(
    name: "ExternalSocialScripts",
    pattern: "external-social-scripts",
    defaults: new { area = "", controller = "Shared", action = "ExternalSocialScripts" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_ExternalSocialScripts",
    pattern: "{lang}/external-social-scripts",
    defaults: new { area = "", controller = "Shared", action = "ExternalSocialScripts" }
    
);

app.MapControllerRoute(
    name: "Home",
    pattern: "",
    defaults: new { controller = "Home", action = "Index" }
    
);

app.MapControllerRoute(
    name: "LanguageBased_Home",
    pattern: "{lang}",
    defaults: new { controller = "Home", action = "Index" }
    
);

app.MapControllerRoute(
    name: "Default",
    pattern: "{controller}/{action}/{id}",
    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
    
);
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var contextMigrate = serviceScope.ServiceProvider.GetService<AreaRegistrationContext>();
  //  contextMigrate?.Database.Migrate();
}
app.Run();
