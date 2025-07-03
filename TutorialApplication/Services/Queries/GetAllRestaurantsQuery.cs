using MediatR;
using TutorialApplication.DTO;

namespace TutorialApplication.Services.Queries
{
    public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDto>>
    {
        public string? SearchPhrase { get; set; }
    }
}
