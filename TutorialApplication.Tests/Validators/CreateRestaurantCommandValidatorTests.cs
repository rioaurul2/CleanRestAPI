using FluentValidation.TestHelper;
using TutorialApplication.Services.Commands;
using Xunit;

namespace TutorialApplication.Validators.Tests;

public class CreateRestaurantCommandValidatorTests
{
    [Fact]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        //arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "name",
            Category = "Italian",
            ContactEmail = "email@emailtest.com",
            PostalCode = "12-234",
        };

        var validator = new CreateRestaurantCommandValidator();

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
    {
        //Arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "name",
            Category = "error",
            ContactEmail = "error",
            PostalCode = "12-234",
        };
        var validator = new CreateRestaurantCommandValidator();

        //Act
        var result = validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrors();
    }
}