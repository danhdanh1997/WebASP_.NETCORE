using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ShopMartWebsite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopMartWebsite.Helpers
{
    public class CustomClaimsPrincipalFactory: UserClaimsPrincipalFactory<User,Role>
    {
        UserManager<User> _userManager;
        public CustomClaimsPrincipalFactory(UserManager<User> userManager, 
            RoleManager<Role> roleManager, IOptions<IdentityOptions> options):base(userManager, roleManager, options)
        {
            _userManager = userManager;
        }
        public async override Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
            {
                new Claim("Id", user.Id),
                new Claim("Email", user.Email),
                new Claim("displayName", user.displayname),
                new Claim("Role", string.Join(";",roles))
            });
            return principal;
        }
    }
}
