﻿using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorialApplication.DTO;
using TutorialApplication.Services.Commands;
using TutorialApplication.Services.Queries;
using TutorialDomain.Constants;

namespace Tutorial.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] GetAllRestaurantsQuery query)
        {
            var restaurants = await _mediator.Send(query);
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "<id claim tyoe>")!.Value;
            var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(id));

            if (restaurant is null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Owner)]
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
