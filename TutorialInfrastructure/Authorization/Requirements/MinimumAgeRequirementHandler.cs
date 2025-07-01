using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using TutorialApplication.User;

namespace TutorialInfrastructure.Authorization.Requirements;

public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    private readonly ILogger<MinimumAgeRequirementHandler> _logger;
    private readonly IUserContext _userContext;


    public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger,
        IUserContext userContext)
    {
        _logger = logger;
        _userContext = userContext;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var currentUser = _userContext.GetCurentUser();
        var dob = context.User.Claims.FirstOrDefault(c => c.Type == "");

        _logger.LogInformation("User {Email}, date of birth {DoB} - Handling MinimumRequirements",
            currentUser?.Email, currentUser?.DateOfBirth);

        if(currentUser?.DateOfBirth == null)
        {
            _logger.LogWarning("User date of birth is null");
            context.Fail();
            return Task.CompletedTask;

        }

        if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateTime.Today)
        {
            _logger.LogInformation("Auth succeded");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
