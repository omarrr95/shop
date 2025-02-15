using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace eCommerce.Entities
{
    public class eCommerceUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public int? PictureID { get; set; }
        public virtual Picture Picture { get; set; }
        public DateTime? RegisteredOn { get; set; }
        public async Task<ClaimsPrincipal> GenerateUserIdentityAsync(UserManager<eCommerceUser> manager)
        {
            var userIdentity = new ClaimsPrincipal();
            var claimsIdentity = new ClaimsIdentity(await manager.GetClaimsAsync(this), IdentityConstants.ApplicationScheme);

            // Add custom user claims
            claimsIdentity.AddClaim(new Claim("Email", Email));
            claimsIdentity.AddClaim(new Claim("Picture", Picture != null ? Picture.URL : string.Empty));

            userIdentity.AddIdentity(claimsIdentity);
            return userIdentity;
        }
        public async Task<ClaimsPrincipal> GenerateUserIdentityAsync(UserManager<eCommerceUser> manager, string authenticationType)
        {
            var userIdentity = new ClaimsPrincipal();
            var claimsIdentity = new ClaimsIdentity(await manager.GetClaimsAsync(this), authenticationType);

            // Add custom user claims
            claimsIdentity.AddClaim(new Claim("Email", Email));
            claimsIdentity.AddClaim(new Claim("Picture", Picture != null ? Picture.URL : string.Empty));

            userIdentity.AddIdentity(claimsIdentity);
            return userIdentity;
        }


    }
}
