using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorialApplication.Common;
using TutorialApplication.DTO;
using TutorialApplication.Services.Queries;
using TutorialDomain.Repositories;

namespace TutorialApplication.Services.Handlers
{
    public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, PageResults<RestaurantDto>>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger<GetAllRestaurantsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllRestaurantsQueryHandler(
            IRestaurantRepository restaurantRepository,
            ILogger<GetAllRestaurantsQueryHandler> logger,
            IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PageResults<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start process: Getting all restaurants");

            var (restaurants, totalCount) = await _restaurantRepository.GetAllFilteredAsync(request.SearchPhrase!, request.PageNumber, request.PageSize);

            _logger.LogInformation("End process successfully: Getting all restaurants");

            var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            var result = new PageResults<RestaurantDto>(restaurantsDto, totalCount, request.PageSize, request.PageNumber);

            return result;
        }
    }
}
