using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Security.Claims;

namespace TutorialApplication.User;

public class UserContext(IHttpContextAccessor _httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurentUser()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null)
        {
            throw new InvalidOperationException("User contect is not present");
        }

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null;
        }

        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);
        var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
        var dateOfBirthString = user.FindFirst(c => c.Type == "DateOfBirth")?.Value;

        var dateOfBirth = dateOfBirthString == null ? (DateTime?)null : DateTime.ParseExact(dateOfBirthString, "yyy-MM--dd", CultureInfo.InvariantCulture);

        return new CurrentUser(userId, email, roles, nationality, dateOfBirth);

    }
}
