

using FluentValidation;
using TutorialApplication.DTO;
using TutorialApplication.Services.Queries;

namespace TutorialApplication.Validators;

public class GeyAllRestauransQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private int[] allowPageSizes = [5, 10, 15, 30];
    private string[] allowedSortByColumnNames = [nameof(RestaurantDto.Name)
        , nameof(RestaurantDto.Category)];

    public GeyAllRestauransQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by must be in [{string.Join(",", allowedSortByColumnNames)}] otherwise is optional");

    }
}
