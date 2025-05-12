using MediatR;
using TutorialApplication.DTO;

namespace TutorialApplication.Services.Queries
{
    public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantDto?>
    {
        public int Id { get; set; } = id;
    }
}
