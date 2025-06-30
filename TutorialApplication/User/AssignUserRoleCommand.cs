using MediatR;

namespace TutorialApplication.User;

public class AssignUserRoleCommand : IRequest
{
    public string? UserEmail {  get; set; }
    public string? RoleName { get; set; }
}
