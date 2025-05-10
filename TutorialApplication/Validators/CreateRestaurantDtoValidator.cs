using FluentValidation;
using TutorialApplication.DTO;

namespace TutorialApplication.Validators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    public CreateRestaurantDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Category)
            .NotEmpty()
            .WithMessage("Required");

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Please provide right mail");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide a valid postal code (XX-XXX)");

        
    }
}
