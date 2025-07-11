using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using TutorialDomain.Constants;
using Xunit;
namespace TutorialApplication.User.Tests;

public class UserContextTests
{
    [Fact]
    public void GetCurentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        //arrange
        var httpContextMock = new Mock<IHttpContextAccessor>();
        var claims = new List<Claim>() 
        {
            new(ClaimTypes.NameIdentifier, "1"),
            new(ClaimTypes.Email, "test@test.com"),
            new(ClaimTypes.Role, UserRoles.Admin),
            new(ClaimTypes.Role, UserRoles.User),
            new("Nationality", "German"),
        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

        httpContextMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });

        var userContext = new UserContext(httpContextMock.Object);

        //act

        var currentUser = userContext.GetCurentUser();

        //assert
        currentUser.Should().NotBeNull();
        currentUser.ID.Should().Be("1");
        currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
    }

    [Fact]
    public void GetCurentUser_WithUserContextNotPresent_ShouldThrowError()
    {
        //areange
        var httpContextMock = new Mock<IHttpContextAccessor>();
        httpContextMock.Setup(x => x.HttpContext).Returns((HttpContext?)null);

        var userContext = new UserContext(httpContextMock.Object);

        //act
        Action action = () => userContext.GetCurentUser();

        //assert
        action.Should().Throw<InvalidOperationException>();
    }
}