using MediatR;

namespace TutorialApplication.Services.Commands
{
    public class DeleteDishByIdForRestaurantCommand(int restaurantId, int dishId) : IRequest<bool>
    {
        public int RestaurantId { get; } = restaurantId;
        public int DishId { get; } = dishId;
    }
}
