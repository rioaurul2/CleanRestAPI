using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorialApplication.Services.Commands;
using TutorialDomain.Constants;
using TutorialDomain.Entities;
using TutorialDomain.Exceptions;
using TutorialDomain.Interfaces;
using TutorialDomain.Repositories;

namespace TutorialApplication.Services.Handlers
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, bool>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
        private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

        public DeleteRestaurantCommandHandler(
              IRestaurantRepository restaurantRepository,
              ILogger<DeleteRestaurantCommandHandler> logger,
              IRestaurantAuthorizationService restaurantAuthorizationService)
        {
            _restaurantRepository = restaurantRepository;
            _logger = logger;
            _restaurantAuthorizationService = restaurantAuthorizationService;
        }

        async Task<bool> IRequestHandler<DeleteRestaurantCommand, bool>.Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleting restaurant with id : {request.Id}");

            var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);

            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            }

            if(!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            {
                throw new ForbidException();
            }

            await _restaurantRepository.DeleteAsync(restaurant);
            return true;
        }
    }
}
