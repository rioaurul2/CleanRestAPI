using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TutorialDomain.Entities;
using TutorialDomain.Interfaces;
using TutorialDomain.Repositories;
using TutorialInfrastructure.Authorization;
using TutorialInfrastructure.Authorization.Requirements;
using TutorialInfrastructure.Authorization.Services;
using TutorialInfrastructure.Context;
using TutorialInfrastructure.Repositories;
using TutorialInfrastructure.Seeders;

namespace TutorialInfrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
    {
        var connectionString = configuration.GetConnectionString("RestaurantDb");
        services.AddDbContext<TutorialDbContext>(options => options
        .UseSqlServer(connectionString)
        .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<TutorialUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<TutorialDbContext>();

        services.AddScoped<IRestaurantSeeders, RestaurantSeeders>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();
        services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimsTypes.Nationality, AllowedValues.AllowedNationalities))
            .AddPolicy(PolicyNames.HasRoNationality, builder => builder.RequireClaim(AppClaimsTypes.Nationality, AllowedValues.AllowedRo))
            .AddPolicy(PolicyNames.HasBirthDate, builder => builder.RequireClaim(AppClaimsTypes.DateOfBirth))
            .AddPolicy(PolicyNames.AtLeast20Years, builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
            .AddPolicy(PolicyNames.OwnerHasTwoRestaurants, builder => builder.AddRequirements(new CreateMultipleRestaurantsRequrements(2)));


        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, CreateMultipleRestaurantsRequrementsHandler>();

    }
}
