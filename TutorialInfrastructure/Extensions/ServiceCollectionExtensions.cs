using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TutorialInfrastructure.Context;
using TutorialInfrastructure.Seeders;

namespace TutorialInfrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
    {
        var connectionString = configuration.GetConnectionString("RestaurantDb");
        services.AddDbContext<TutorialDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IRestaurantSeeders, RestaurantSeeders>();
    }
}
