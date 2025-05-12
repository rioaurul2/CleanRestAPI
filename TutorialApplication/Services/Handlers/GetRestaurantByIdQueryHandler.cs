using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorialApplication.DTO;
using TutorialApplication.Services.Queries;
using TutorialDomain.Repositories;

namespace TutorialApplication.Services.Handlers
{
    public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger<GetRestaurantByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetRestaurantByIdQueryHandler(
              IRestaurantRepository restaurantRepository,
              ILogger<GetRestaurantByIdQueryHandler> logger,
              IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start process: Getting restaurant by {request.Id}");

            var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);

            _logger.LogInformation($"End process successfully: Getting restaurant by {request.Id}");

            var restaurantDto = _mapper.Map<RestaurantDto?>(restaurant);

            return restaurantDto;
        }
    }
}
