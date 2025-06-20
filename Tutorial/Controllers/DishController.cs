using MediatR;
using Microsoft.AspNetCore.Mvc;
using TutorialApplication.Services.Commands;
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
        public async Task<IActionResult> CreateDish([FromRoute]int restaurantId, [FromBody] CreateDishCommand command )
        {
            command.RestaurantId = restaurantId;
            await _mediator.Send(command);
            return Created();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllDishes(int restarantId)
        //{
        //    return Ok();
        //}
    }
}
