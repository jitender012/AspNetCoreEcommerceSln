using eCommerce.Application.Features.ProductCategoryFeatures.Commands;
using FluentValidation;

namespace eCommerce.Application.Features.ProductCategoryFeatures.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.dto.CategoryName)
                .NotEmpty()
                .WithMessage("Category name is required.");
        }
    }
}
