using FluentValidation;
using ElegantGlamour.Core.Dtos;

namespace ElegantGlamour.Api.Validators
{
    public class AddCategoryDtoValidator : AbstractValidator<AddCategoryDto>
    {
        public AddCategoryDtoValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}