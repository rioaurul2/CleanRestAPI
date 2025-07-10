using FluentAssertions;
using TutorialDomain.Constants;
using Xunit;

namespace TutorialApplication.User.Tests;

public class CurrentUserTests
{
    //TestMethod_Scenariu_Rezultat
    [Fact]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue()
    {
        //Arrange
        var currentUser = new CurrentUser("1", "test@email.com", [UserRoles.Admin, UserRoles.User], null, null);

        //Act
        var isInRole = currentUser.IsInRole(UserRoles.Admin);

        //Assert
        isInRole.Should().BeTrue();
    }

    [Fact]
    public void IsInRole_WithoutMatchingRole_ShouldReturnFalse()
    {
        //Arrange
        var currentUser = new CurrentUser("1", "test@email.com", [UserRoles.Admin, UserRoles.User], null, null);

        //Act
        var isInRole = currentUser.IsInRole(UserRoles.Owner);

        //Assert
        isInRole.Should().BeFalse();

    }

    [Fact]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        //Arrange
        var currentUser = new CurrentUser("1", "test@email.com", [UserRoles.Admin, UserRoles.User], null, null);

        //Act
        var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

        //Assert
        isInRole.Should().BeFalse();

    }
}