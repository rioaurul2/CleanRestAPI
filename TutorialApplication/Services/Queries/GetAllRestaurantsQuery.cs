using MediatR;
using TutorialApplication.Common;
using TutorialApplication.DTO;
using TutorialDomain.Constants;

namespace TutorialApplication.Services.Queries
{
    public class GetAllRestaurantsQuery : IRequest<PageResults<RestaurantDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
