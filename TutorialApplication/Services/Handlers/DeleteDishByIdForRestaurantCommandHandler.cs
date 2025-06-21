using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorialApplication.DTO;
using TutorialApplication.Services.Commands;
using TutorialDomain.Exceptions;
using TutorialDomain.Repositories;

namespace TutorialApplication.Services.Handlers
{
    public class DeleteDishByIdForRestaurantCommandHandler : IRequestHandler<DeleteDishByIdForRestaurantCommand, bool>
    {
        private readonly ILogger<DeleteDishByIdForRestaurantCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IDishesRepository _dishesRepository;

        public DeleteDishByIdForRestaurantCommandHandler(ILogger<DeleteDishByIdForRestaurantCommandHandler> logger,
                        IMapper mapper,
                        IRestaurantRepository restaurantRepository,
                        IDishesRepository dishesRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _restaurantRepository = restaurantRepository;
            _dishesRepository = dishesRepository;
        }

        public async Task<bool> Handle(DeleteDishByIdForRestaurantCommand request, CancellationToken cancellationToken)
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

            await _dishesRepository.DeleteDish(dish);

            return true;
        }
    }
}
