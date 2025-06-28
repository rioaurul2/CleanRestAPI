using MediatR;
using Microsoft.Extensions.Logging;
using TutorialApplication.Services.Commands;
using TutorialDomain.Entities;
using TutorialDomain.Exceptions;
using TutorialDomain.Repositories;

namespace TutorialApplication.Services.Handlers
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger<UpdateRestaurantCommandHandler> _logger;

        public UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepository,
            ILogger<UpdateRestaurantCommandHandler> logger)
        {
            _logger = logger;
            _restaurantRepository = restaurantRepository;
        }

        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Update restaurant with id : {request.Id} with new values {request}");

            var  restaurant = await _restaurantRepository.GetByIdAsync(request.Id);

            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            }

            restaurant.HasDelivery = request.HasDelivery;
            restaurant.Name = request.Name;

            await _restaurantRepository.UpdateAsync(restaurant);
        }
    }
}
