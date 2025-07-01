namespace TutorialApplication.User;

public record CurrentUser(string ID, string Email,
    IEnumerable<string> Roles,
    string? Nationality,
    DateTime? DateOfBirth)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
