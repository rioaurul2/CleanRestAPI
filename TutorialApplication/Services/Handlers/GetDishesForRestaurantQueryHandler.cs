using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorialApplication.DTO;
using TutorialApplication.Services.Queries;
using TutorialDomain.Exceptions;
using TutorialDomain.Repositories;

namespace TutorialApplication.Services.Handlers
{
    public class GetDishesForRestaurantQueryHandler : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
    {
        private readonly ILogger<GetDishesForRestaurantQueryHandler> _logger;
        private readonly IDishesRepository _dishesRepository;
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;

        public GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler> logger,
                    IDishesRepository dishesRepository,
                    IMapper mapper,
                    IRestaurantRepository restaurantRepository)
        {
            _logger = logger;
            _dishesRepository = dishesRepository;
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;

        }

        public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Process started");
            var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);

            if (restaurant == null)
            {
                throw new NotFoundException(nameof(request), request.RestaurantId.ToString());
            }

            var dishes = await _dishesRepository.GetAllDishes(request.RestaurantId);
            return _mapper.Map<IEnumerable<DishDto>>(dishes);
        }
    }
}
