using MediatR;
using TutorialApplication.DTO;

namespace TutorialApplication.Services.Queries
{
    public class GetDishByIdForRestaurantQuery(int restaurantId, int dishId) : IRequest<DishDto>
    {
        public int RestaurantId {  get; } = restaurantId;
        public int DishId { get; } = dishId;
    }
}
