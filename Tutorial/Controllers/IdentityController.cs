using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorialApplication.User;
using TutorialDomain.Constants;

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
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateUserDetailsCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("userRole")]
    [Authorize(Roles = UserRoles.Admin)]

    public async Task<IActionResult> AddUserRole([FromBody] AssignUserRoleCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("userRole")]
    [Authorize(Roles = UserRoles.Admin)]

    public async Task<IActionResult> RemoveUserRole([FromBody] RemoveUserRoleCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
