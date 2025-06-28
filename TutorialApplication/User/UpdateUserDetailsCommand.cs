using MediatR;

namespace TutorialApplication.User;

public class UpdateUserDetailsCommand : IRequest
{
    public DateTime? DateOfBirth {  get; set; }
    public string? Nationality { get; set; }
}
