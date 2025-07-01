using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using TutorialDomain.Entities;

namespace TutorialInfrastructure.Authorization;

public class TutorialUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public TutorialUserClaimsPrincipalFactory(UserManager<User> userManager, 
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);

        if(user.Nationality != null)
        {
            id.AddClaim(new Claim(AppClaimsTypes.Nationality, user.Nationality));
        }

        if(user.DateOfBirth != null) 
        {
            id.AddClaim(new Claim(AppClaimsTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
        }

        return new ClaimsPrincipal(id);
    }
}
