using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TutorialApplication.DTO;
using TutorialApplication.Services.Commands;
using TutorialApplication.Services.Queries;

namespace Tutorial.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RestaurantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
        {
            var restaurants = await _mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(id));

            if (restaurant is null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant(
            [FromBody] CreateRestaurantCommand createdRestaurant,
            [FromServices] IValidator<CreateRestaurantCommand> validator)
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

            var id = await _mediator.Send(createdRestaurant);

            var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(id));

            return CreatedAtAction(nameof(GetRestaurantById), new { id }, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {

            var isDeleted = await _mediator.Send(new DeleteRestaurantCommand(id));

            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok($"Object deleted");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRestaurant(
            [FromRoute] int id, 
            UpdateRestaurantCommand command,
            [FromServices] IValidator<UpdateRestaurantCommand> validator)
        {

            var result = await validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return BadRequest(ModelState);
            }

            command.Id = id;

            await _mediator.Send(command);

            return Ok($"Object Updated");
        }
    }
}
