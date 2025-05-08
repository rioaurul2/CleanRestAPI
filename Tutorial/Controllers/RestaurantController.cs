using Microsoft.AspNetCore.Mvc;
using TutorialApplication.Interfaces;

namespace Tutorial.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var restaurants = await _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantById(int id) 
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);

            if(restaurant is null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

    }
}
