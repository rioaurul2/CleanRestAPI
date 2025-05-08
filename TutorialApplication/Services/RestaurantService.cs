using Microsoft.Extensions.Logging;
using TutorialApplication.Interfaces;
using TutorialDomain.Entities;
using TutorialDomain.Repositories;

namespace TutorialApplication.Services
{
    internal class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(IRestaurantRepository restaurantRepository, ILogger<RestaurantService> logger)
        {
            _restaurantRepository = restaurantRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            _logger.LogInformation("Start process: Getting all restaurants");

            var restaurants = await _restaurantRepository.GetAllAsync();

            _logger.LogInformation("End process successfully: Getting all restaurants");

            return restaurants;
        }

        public async Task<Restaurant> GetRestaurantByIdAsync(int id)
        {
            _logger.LogInformation($"Start process: Getting restaurant by {id}");

            var restaurant = await _restaurantRepository.GetByIdAsync(id);

            _logger.LogInformation($"End process successfully: Getting restaurant by {id}");

            return restaurant;
        }
    }
}
