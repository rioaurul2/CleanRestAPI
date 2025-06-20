using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TutorialApplication.Validators;

namespace TutorialApplication.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddValidatorsFromAssembly(typeof(CreateRestaurantCommandValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(CreateDishCommandValidator).Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        }
    }
}
