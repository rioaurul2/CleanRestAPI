using Microsoft.AspNetCore.Authorization;
using TutorialApplication.User;
using TutorialDomain.Repositories;

namespace TutorialInfrastructure.Authorization.Requirements;

public class CreateMultipleRestaurantsRequrementsHandler : AuthorizationHandler<CreateMultipleRestaurantsRequrements>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IUserContext _userContext;

    public CreateMultipleRestaurantsRequrementsHandler(IRestaurantRepository restaurantRepository, IUserContext userContext)
    {
        _restaurantRepository = restaurantRepository;
        _userContext = userContext;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateMultipleRestaurantsRequrements requirement)
    {
        var user = _userContext.GetCurentUser();

        var restaurants = await _restaurantRepository.GetAllAsync();

        var userRestaurants = restaurants.Count(r => r.OwnerId == user.ID);

        if(userRestaurants >= requirement.RestaurantsNo)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
