using MediatR;
using Microsoft.AspNetCore.Mvc;
using TutorialApplication.DTO;
using TutorialApplication.Services.Commands;
using TutorialApplication.Services.Queries;
using TutorialDomain.Entities;

namespace Tutorial.Controllers
{
    [ApiController]
    [Route("api/restaurant/{restaurantId}/dishes")]
    public class DishController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DishController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, [FromBody] CreateDishCommand command )
        {
            command.RestaurantId = restaurantId;
            var dishId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetDishByIdForRestaurant), new {restaurantId, dishId}, null);
            //return Ok(dishId);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllDishes([FromRoute] int restaurantId)
        {
            var dishes = await _mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetDishByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = await _mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
            return Ok(dish);
        }

        [HttpDelete("{dishId}")]
        public async Task<ActionResult<bool>> DeleteDisheByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var deleted = await _mediator.Send(new DeleteDishByIdForRestaurantCommand(restaurantId, dishId));
            return Ok(deleted);
        }
    }
}
