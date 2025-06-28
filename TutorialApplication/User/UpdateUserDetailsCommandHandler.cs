using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TutorialDomain.Exceptions;

namespace TutorialApplication.User;

public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand>
{
    private readonly ILogger<UpdateUserDetailsCommandHandler> _logger;
    private readonly IUserContext _userContext;
    private readonly IUserStore<TutorialDomain.Entities.User> _userStore;

    public UpdateUserDetailsCommandHandler( ILogger<UpdateUserDetailsCommandHandler> logger,
        IUserContext userContext,
        IUserStore<TutorialDomain.Entities.User> userStore)
    {
        _logger = logger;
        _userContext = userContext;
        _userStore = userStore;
    }

    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurentUser();

        _logger.LogInformation("Update user details");

        var dbUser = await _userStore.FindByIdAsync(user!.ID, cancellationToken);

        if(dbUser == null )
        {
            throw new NotFoundException(nameof(user), user!.ID);
        }

        dbUser.Nationality = request.Nationality;
        dbUser.DateOfBirth = request.DateOfBirth;

        await _userStore.UpdateAsync(dbUser, cancellationToken);
    }
}
