using TutorialDomain.Entities;

namespace TutorialDomain.Repositories
{
    public interface IDishesRepository
    {
        Task<int> Create(Dish entity);
        Task<IEnumerable<Dish>> GetAllDishes(int restaurantId);
        Task<Dish> GetDishById(int restaurantId, int dishId);
        Task DeleteDish(Dish dish);

    }
}
