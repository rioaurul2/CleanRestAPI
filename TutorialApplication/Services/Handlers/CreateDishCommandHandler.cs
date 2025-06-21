using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorialApplication.Services.Commands;
using TutorialDomain.Entities;
using TutorialDomain.Exceptions;
using TutorialDomain.Repositories;

namespace TutorialApplication.Services.Handlers
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, int>
    {
        private readonly ILogger<CreateDishCommandHandler> _logger;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IDishesRepository _dishesRepository;
        private readonly IMapper _mapper;

        public CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger, 
            IRestaurantRepository restaurantRepository,
            IDishesRepository dishesRepository,
            IMapper mapper)
        {
            _logger = logger;
            _restaurantRepository = restaurantRepository;
            _dishesRepository = dishesRepository;
            _mapper = mapper;

        }

        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start the proces of creating a dish {@DishRequest}", request);

            var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);

            if(restaurant == null)
            {
                throw new NotFoundException(nameof(request), request.RestaurantId.ToString());
            }

            var dish = _mapper.Map<Dish>(request);

            var result = await _dishesRepository.Create(dish);

            return result;
        }
    }
}
