using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorialApplication.DTO;
using TutorialApplication.Services.Queries;
using TutorialDomain.Exceptions;
using TutorialDomain.Repositories;

namespace TutorialApplication.Services.Handlers
{
    public class GetDishByIdForRestaurantQueryHandler : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        private readonly ILogger<GetDishByIdForRestaurantQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IDishesRepository _dishesRepository;

        public GetDishByIdForRestaurantQueryHandler(ILogger<GetDishByIdForRestaurantQueryHandler> logger,
                        IMapper mapper,
                        IRestaurantRepository restaurantRepository,
                        IDishesRepository dishesRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _restaurantRepository = restaurantRepository;
            _dishesRepository = dishesRepository;
        }

        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Process started");
            var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);

            if (restaurant == null)
            {
                throw new NotFoundException(nameof(request), request.RestaurantId.ToString());
            }

            var dish = await _dishesRepository.GetDishById(request.RestaurantId, request.DishId);

            if (dish == null)
            {
                throw new NotFoundException(nameof(request), request.DishId.ToString());
            }

            return _mapper.Map<DishDto>(dish);
        }
    }
}
