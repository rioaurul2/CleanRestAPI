using MediatR;
using TutorialApplication.DTO;

namespace TutorialApplication.Services.Queries
{
    public class GetDishesForRestaurantQuery(int restaurantId) : IRequest<IEnumerable<DishDto>>
    {

        public int RestaurantId { get; } = restaurantId;
    }
}
