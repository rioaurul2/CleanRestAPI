using TutorialDomain.Entities;

namespace TutorialApplication.Interfaces;

public interface IRestaurantService
{
    public Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
    public Task<Restaurant> GetRestaurantByIdAsync(int id);
}
