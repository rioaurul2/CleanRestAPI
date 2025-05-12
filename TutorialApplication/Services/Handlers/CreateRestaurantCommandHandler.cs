using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorialApplication.Services.Commands;
using TutorialDomain.Entities;
using TutorialDomain.Repositories;

namespace TutorialApplication.Services.Handlers
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, 
            ILogger<CreateRestaurantCommandHandler> logger,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start process: Creating restaurant");

            var restaurant = _mapper.Map<Restaurant>(request);

            var id = await _restaurantRepository.AddRestaurantAsync(restaurant);

            _logger.LogInformation($"End process successfully: Creating restaurant");

            return id;
        }
    }
}
