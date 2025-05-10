using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TutorialApplication.Interfaces;
using TutorialApplication.Services;
using TutorialApplication.Validators;

namespace TutorialApplication.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddValidatorsFromAssembly(typeof(CreateRestaurantDtoValidator).Assembly);
        }
    }
}
