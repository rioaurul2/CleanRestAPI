using AutoMapper;
using FluentAssertions;
using TutorialApplication.DTO;
using TutorialApplication.Services.Commands;
using TutorialDomain.Entities;
using Xunit;

namespace TutorialApplication.Profiles.Tests;

public class RestaurantProfileTests
{
    private IMapper _mapper;

    public RestaurantProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RestaurantProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
    {
        //Arrange


        var restaurant = new Restaurant()
        {
            Id = 1,
            Name = "Test",
            Category = "Test",
            HasDelivery = true,
            ContactEmail = "Test",
            ContactNumber = "Test",
            Address = new Address
            {
                City = "test",
                Street = "test",
                PostalCode = "12345"
            }
        };

        //Act
        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

        //Assert
        restaurantDto.Should().NotBeNull();
        restaurantDto.Name.Should().Be("Test");
        restaurantDto.Category.Should().Be("Test");
        restaurantDto.HasDelivery.Should().BeTrue();
        restaurantDto.Street.Should().NotBeNull();
    }

    [Fact]
    public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapsCorrectly()
    {
        //Arrange
        var restaurantCommand = new CreateRestaurantCommand()
        {
            Name = "Test",
            Category = "Test",
            HasDelivery = true,
            ContactEmail = "Test",
            ContactNumber = "Test",
            PostalCode = "12345",
            Street = "test",
            City = "test"
        };

        //Act
        var restaurant = _mapper.Map<Restaurant>(restaurantCommand);

        //Assert
        restaurant.Should().NotBeNull();
        restaurant.Name.Should().Be("Test");
        restaurant.Category.Should().Be("Test");
        restaurant.HasDelivery.Should().BeTrue();
        restaurant.Address?.Street.Should().NotBeNull();
    }
}