using MediatR;

namespace TutorialApplication.User;

public class RemoveUserRoleCommand : IRequest
{
    public string? UserEmail { get; set; }
    public string? RoleName { get; set; }
}
