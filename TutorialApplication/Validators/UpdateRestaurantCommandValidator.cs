using FluentValidation;
using TutorialApplication.Services.Commands;

namespace TutorialApplication.Validators
{
    public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name)
           .Length(3, 100)
           .NotEmpty();
        }

    }
}
