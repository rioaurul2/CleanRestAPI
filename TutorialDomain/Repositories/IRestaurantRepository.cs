﻿using TutorialDomain.Entities;

namespace TutorialDomain.Repositories;

public interface IRestaurantRepository
{
    public Task<IEnumerable<Restaurant>> GetAllAsync();
    public Task<IEnumerable<Restaurant>> GetAllFilteredAsync(string searcedPhrase);
    public Task<Restaurant> GetByIdAsync(int id);
    public Task<int> AddRestaurantAsync(Restaurant restaurant);
    public Task DeleteAsync(Restaurant restaurant);
    public Task UpdateAsync(Restaurant restaurant);
}
