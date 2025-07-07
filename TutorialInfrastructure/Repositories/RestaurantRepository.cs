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

    public async Task<(IEnumerable<Restaurant>, int)> GetAllFilteredAsync(string? searcedPhrase, int pageNumber, int pageSize)
    {
        var searPhraseLower = searcedPhrase?.ToLower();

        var baseQuery = _dbContext.Restaurants
            .Include(r => r.Dishes)
            .Where(r => searPhraseLower == null || r.Name.ToLower().Contains(searPhraseLower));

        var totalCount = await baseQuery.CountAsync();

        var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (restaurants, totalCount);
    }

    public async Task<Restaurant> GetByIdAsync(int id)
    {
        var restaurant = await _dbContext.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(restaurant => restaurant.Id == id);

        return restaurant!;
    }

    public async Task<int> AddRestaurantAsync(Restaurant restaurant)
    {
        _dbContext.Restaurants.Add(restaurant);

        await _dbContext.SaveChangesAsync();

        return restaurant.Id;
    }

    public async Task DeleteAsync(Restaurant restaurant)
    {
        _dbContext.Restaurants.Remove(restaurant);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Restaurant restaurant)
    {
        _dbContext.Restaurants.Update(restaurant);

        await _dbContext.SaveChangesAsync();
    }
}
