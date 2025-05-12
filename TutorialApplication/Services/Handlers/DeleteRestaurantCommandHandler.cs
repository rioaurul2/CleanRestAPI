using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorialApplication.Services.Commands;
using TutorialDomain.Repositories;

namespace TutorialApplication.Services.Handlers
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, bool>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger<DeleteRestaurantCommandHandler> _logger;

        public DeleteRestaurantCommandHandler(
              IRestaurantRepository restaurantRepository,
              ILogger<DeleteRestaurantCommandHandler> logger)
        {
            _restaurantRepository = restaurantRepository;
            _logger = logger;
        }

        async Task<bool> IRequestHandler<DeleteRestaurantCommand, bool>.Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleting restaurant with id : {request.Id}");

            var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);

            if (restaurant == null)
            {
                return false;
            }

            await _restaurantRepository.DeleteAsync(restaurant);

            return true;
        }
    }
}
