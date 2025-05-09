using AutoMapper;
using Microsoft.Extensions.Logging;
using TutorialApplication.DTO;
using TutorialApplication.Interfaces;
using TutorialDomain.Entities;
using TutorialDomain.Repositories;

namespace TutorialApplication.Services
{
    internal class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IMapper _mapper;

        public RestaurantService(
            IRestaurantRepository restaurantRepository, 
            ILogger<RestaurantService> logger,
            IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
        {
            _logger.LogInformation("Start process: Getting all restaurants");

            var restaurants = await _restaurantRepository.GetAllAsync();

            _logger.LogInformation("End process successfully: Getting all restaurants");

            //var restaurantsDto = restaurants.Select(r => RestaurantDto.FromEntity(r));
            var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);


            return restaurantsDto;
        }

        public async Task<RestaurantDto?> GetRestaurantByIdAsync(int id)
        {
            _logger.LogInformation($"Start process: Getting restaurant by {id}");

            var restaurant = await _restaurantRepository.GetByIdAsync(id);

            _logger.LogInformation($"End process successfully: Getting restaurant by {id}");

            //var restaurantDto = RestaurantDto.FromEntity(restaurant);

            var restaurantDto = _mapper.Map<RestaurantDto?>(restaurant);

            return restaurantDto;
        }

        public async Task<int> AddRestaurantAsync(CreateRetaurantDto createdRestaurant)
        {
            _logger.LogInformation($"Start process: Creating restaurant");

            var restaurant = _mapper.Map<Restaurant>(createdRestaurant);

            var id = await _restaurantRepository.AddRestaurantAsync(restaurant);

            _logger.LogInformation($"End process successfully: Creating restaurant");

            return id;
        }
    }
}
