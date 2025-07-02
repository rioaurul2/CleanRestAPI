namespace TutorialInfrastructure.Authorization;

public static class PolicyNames
{
    public const string HasNationality = "HasNationality";
    public const string HasRoNationality = "HasRoNationality";
    public const string HasBirthDate = "HasBirthDate";
    public const string AtLeast20Years = "AtLeast20Years";
    public const string OwnerHasTwoRestaurants = "OwnerHasTwoRestaurants";
}

public static class AppClaimsTypes
{
    public const string Nationality = "Nationality";
    public const string DateOfBirth = "DateOfBirth";
}

public static class AllowedValues
{
    public static readonly IEnumerable<string> AllowedNationalities = ["German", "Polish"];
    public const string AllowedRo = "Romanian";
}
