using Microsoft.EntityFrameworkCore;
using TutorialDomain.Entities;
using TutorialDomain.Repositories;
using TutorialInfrastructure.Context;

namespace TutorialInfrastructure.Repositories
{
    internal class DishesRepository : IDishesRepository
    {
        private readonly TutorialDbContext _context;

        public DishesRepository(TutorialDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Dish entity)
        {
            _context.Dishes.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteDish(Dish dish)
        {
            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Dish>> GetAllDishes(int restaurantId)
        {
            var dishes = await _context.Dishes
                        .Where(dish => dish.RestaurantId == restaurantId)
                        .ToListAsync();

            return dishes;
        }

        public async Task<Dish> GetDishById(int restaurantId, int dishId)
        {
            var dish = await _context.Dishes.Where(dish => dish.RestaurantId == restaurantId && dish.Id == dishId)
                        .FirstOrDefaultAsync();
            return dish;
        }
    }
}
