using Microsoft.AspNetCore.Authorization;

namespace TutorialInfrastructure.Authorization.Requirements;

public class CreateMultipleRestaurantsRequrements(int restaurantsNo): IAuthorizationRequirement
{
    public int RestaurantsNo { get; } = restaurantsNo;
}
