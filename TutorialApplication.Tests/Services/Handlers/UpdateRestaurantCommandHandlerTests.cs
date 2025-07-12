using Xunit;
using Moq;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using TutorialDomain.Repositories;
using AutoMapper;
using TutorialDomain.Interfaces;
using TutorialApplication.Services.Commands;
using TutorialDomain.Entities;
using TutorialDomain.Constants;

namespace TutorialApplication.Services.Handlers.Tests;

public class UpdateRestaurantCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
    private readonly Mock<IRestaurantRepository> _repositoryMock;
    private readonly Mock<IRestaurantAuthorizationService> _authorizationServiceMock;

    private readonly UpdateRestaurantCommandHandler _handler;

    public UpdateRestaurantCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        _repositoryMock = new Mock<IRestaurantRepository>();
        _authorizationServiceMock = new Mock<IRestaurantAuthorizationService>();

        _handler = new UpdateRestaurantCommandHandler
            (
                _repositoryMock.Object,
                _loggerMock.Object,
                _authorizationServiceMock.Object
            );
    }

    [Fact]
    public async Task Handle_withValidRequesr_ShouldUpdateRestaurant()
    {
        //Arrange
        var restaurantId = 1;
        var command = new UpdateRestaurantCommand(restaurantId)
            {
                Id = restaurantId,
                Name = "Test",
                HasDelivery = true,
            };

        var restaurant = new Restaurant()
        {
            Id = restaurantId,
            Name = "Test",
        };

        _repositoryMock.Setup(r => r.GetByIdAsync(restaurantId)).ReturnsAsync(restaurant);
        _authorizationServiceMock.Setup(a => a.Authorize(restaurant, ResourceOperation.Update)).Returns(true);

        //Act
        await _handler.Handle(command, CancellationToken.None);

        //Assert

        _repositoryMock.Verify(r => r.UpdateAsync(restaurant), Times.Once);
    }
}