using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace TutorialApplication.User
{
    public interface IUserContext
    {
        CurrentUser? GetCurentUser();
    }
}