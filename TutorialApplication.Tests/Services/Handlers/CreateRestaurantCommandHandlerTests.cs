using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using TutorialApplication.Services.Commands;
using TutorialApplication.User;
using TutorialDomain.Entities;
using TutorialDomain.Repositories;
using Xunit;

namespace TutorialApplication.Services.Handlers.Tests;

public class CreateRestaurantCommandHandlerTests
{
    [Fact]
    public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
    {
        //Arrange
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        var restaurantRepositoryMock = new Mock<IRestaurantRepository>();
        var userContextMock = new Mock<IUserContext>();
        var mapperMock = new Mock<IMapper>();

        restaurantRepositoryMock.Setup(repo => repo.AddRestaurantAsync(It.IsAny<Restaurant>())).ReturnsAsync(1);

        var currentUser = new CurrentUser("owner-id", "test@Email.com", [], null, null);
        userContextMock.Setup(u => u.GetCurentUser()).Returns(currentUser);

        var command = new CreateRestaurantCommand();
        var restaurant = new Restaurant();
        mapperMock.Setup(map => map.Map<Restaurant>(command)).Returns(restaurant);

        var commandHandler = new CreateRestaurantCommandHandler(restaurantRepositoryMock.Object, 
            loggerMock.Object,
            mapperMock.Object,
            userContextMock.Object);

        //Act

        var result = await commandHandler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().Be(1);
        restaurant.OwnerId.Should().Be("owner-id");
        restaurantRepositoryMock.Verify(r => r.AddRestaurantAsync(restaurant), Times.Once);
    }
}