using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TutorialDomain.Exceptions;

namespace TutorialApplication.User;

public class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand>
{
    private readonly ILogger<AssignUserRoleCommandHandler> _logger;
    private readonly IUserContext _userContext;
    private readonly UserManager<TutorialDomain.Entities.User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;


    public AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger,
        IUserContext userContext,
        UserManager<TutorialDomain.Entities.User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _userContext = userContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Assign user role {@Request}", request);

        var user = await _userManager.FindByEmailAsync(request.UserEmail!);

        if (user == null)
        {
            throw new NotFoundException(nameof(user), request!.UserEmail!);
        }

        var role = await _roleManager.FindByNameAsync(request!.RoleName!);

        if (role == null)
        {
            throw new NotFoundException(nameof(IdentityRole), request.RoleName!);
        }

        await _userManager.AddToRoleAsync(user, role.Name!);
    }
}
