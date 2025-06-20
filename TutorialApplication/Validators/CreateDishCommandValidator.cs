using FluentValidation;
using TutorialApplication.Services.Commands;

namespace TutorialApplication.Validators
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator()
        {
            RuleFor(dish => dish.Name).NotEmpty();
            RuleFor(dish => dish.Price).GreaterThanOrEqualTo(0).WithMessage("insert a bigger value"); ;
        }
    }
}
