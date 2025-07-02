using TutorialApplication.User;
using TutorialDomain.Constants;
using TutorialDomain.Entities;
using TutorialDomain.Interfaces;

namespace TutorialInfrastructure.Authorization.Services;

public class RestaurantAuthorizationService : IRestaurantAuthorizationService
{
    private readonly ILogger<RestaurantAuthorizationService> _logger;
    private readonly IUserContext _userContext;

    public RestaurantAuthorizationService(IUserContext userContext, ILogger<RestaurantAuthorizationService> logger)
    {
        _userContext = userContext;
        _logger = logger;

    }
    public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
    {
        var user = _userContext.GetCurentUser();

        _logger.LogInformation($"Authorize Operation {resourceOperation} on {restaurant.Name} by user {user.Email}");

        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            _logger.LogInformation($"Authorized on Create/Read");
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            _logger.LogInformation($"Authorized on Delete");
            return true;
        }

        if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Create) && user.ID == restaurant.OwnerId)
        {
            _logger.LogInformation($"Authorized on Delete by owner");
            return true;
        }

        return false;
    }
}
