using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorialApplication.User;

namespace Tutorial.Controllers;

[ApiController]
[Route("ape/identity")]
[Authorize]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator) 
    {
        _mediator = mediator;
    }


    [HttpPatch("user")]
    public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateUserDetailsCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
