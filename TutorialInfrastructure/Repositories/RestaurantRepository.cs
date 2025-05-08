using Microsoft.EntityFrameworkCore;
using TutorialDomain.Entities;
using TutorialDomain.Repositories;
using TutorialInfrastructure.Context;

namespace TutorialInfrastructure.Repositories;

internal class RestaurantRepository : IRestaurantRepository
{
    private readonly TutorialDbContext _dbContext;

    public RestaurantRepository(TutorialDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await _dbContext.Restaurants
            .Include(r => r.Dishes)
            .ToListAsync();

        return restaurants;
    }

    public async Task<Restaurant> GetByIdAsync(int id)
    {
        var restaurant = await _dbContext.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(restaurant => restaurant.Id == id);

        return restaurant!;
    }
}
