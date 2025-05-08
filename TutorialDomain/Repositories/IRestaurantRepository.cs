using TutorialDomain.Entities;

namespace TutorialDomain.Repositories;

public interface IRestaurantRepository
{
    public Task<IEnumerable<Restaurant>> GetAllAsync();
    public Task<Restaurant> GetByIdAsync(int id);
}
