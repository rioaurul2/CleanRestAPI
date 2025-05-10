using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TutorialApplication.DTO;
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

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant(
            [FromBody] CreateRestaurantDto createdRestaurant,
            [FromServices] IValidator<CreateRestaurantDto> validator)
        {
            var result = await validator.ValidateAsync(createdRestaurant);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return BadRequest(ModelState);
            }

            var id = await _restaurantService.AddRestaurantAsync(createdRestaurant);

            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);

            return CreatedAtAction(nameof(GetRestaurantById), new { id }, null);
        }

    }
}
