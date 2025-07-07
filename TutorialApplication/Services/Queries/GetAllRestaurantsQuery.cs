using MediatR;
using TutorialApplication.Common;
using TutorialApplication.DTO;

namespace TutorialApplication.Services.Queries
{
    public class GetAllRestaurantsQuery : IRequest<PageResults<RestaurantDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
