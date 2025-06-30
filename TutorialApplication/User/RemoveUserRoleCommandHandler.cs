using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TutorialDomain.Exceptions;

namespace TutorialApplication.User;

public class RemoveUserRoleCommandHandler: IRequestHandler<RemoveUserRoleCommand>
{
    private readonly ILogger<RemoveUserRoleCommandHandler> _logger;
    private readonly IUserContext _userContext;
    private readonly UserManager<TutorialDomain.Entities.User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;


    public RemoveUserRoleCommandHandler(ILogger<RemoveUserRoleCommandHandler> logger,
        IUserContext userContext,
        UserManager<TutorialDomain.Entities.User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _userContext = userContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Remove user role {@Request}", request);

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

        await _userManager.RemoveFromRoleAsync(user, role.Name!);
    }
}
