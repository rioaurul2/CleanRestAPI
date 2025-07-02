using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorialApplication.Services.Commands;
using TutorialApplication.User;
using TutorialDomain.Entities;
using TutorialDomain.Repositories;

namespace TutorialApplication.Services.Handlers
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, 
            ILogger<CreateRestaurantCommandHandler> logger,
            IMapper mapper,
            IUserContext userContext)
        {
            _logger = logger;
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
            _userContext = userContext;
        }

        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurentUser();

            _logger.LogInformation("Start process: {@user} Creating restaurant {@request}", user!.Email, request);

            var restaurant = _mapper.Map<Restaurant>(request);

            restaurant.OwnerId = user.ID;

            var id = await _restaurantRepository.AddRestaurantAsync(restaurant);

            _logger.LogInformation($"End process successfully: Creating restaurant");

            return id;
        }
    }
}
