using Microsoft.Extensions.DependencyInjection;
using TutorialApplication.Interfaces;
using TutorialApplication.Services;

namespace TutorialApplication.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantService, RestaurantService>();
        }
    }
}
