using TutorialApplication.DTO;

namespace TutorialApplication.Interfaces;

public interface IRestaurantService
{
    public Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync();
    public Task<RestaurantDto?> GetRestaurantByIdAsync(int id);
    public Task<int> AddRestaurantAsync(CreateRestaurantDto createdRestaurant);
}
