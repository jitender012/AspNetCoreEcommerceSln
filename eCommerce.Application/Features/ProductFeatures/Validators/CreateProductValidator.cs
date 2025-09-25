using eCommerce.Application.Features.ProductFeatures.Commands;
using FluentValidation;

namespace eCommerce.Application.Features.ProductFeatures.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x=>x.dto.Description)
                .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");
            RuleFor(x => x.dto.BrandId).NotEmpty().WithMessage("Brand is required.");
            RuleFor(x => x.dto.CategoryId).GreaterThan(0).WithMessage("Category is required.");
            
            
            RuleFor(x => x.dto.ProductVariant.VarientName)
                .NotNull()
                .MaximumLength(100)
                .WithMessage("Name is required.");
            RuleFor(x => x.dto.ProductVariant.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Product Quantity must be greater than or equal to 0.");
            RuleFor(x => x.dto.ProductVariant.SKU)
                .NotEmpty().WithMessage("Product Sku is required.")
                .MaximumLength(50).WithMessage("Product Sku must not exceed 50 characters.");
            RuleFor(x => x.dto.ProductVariant.Price).GreaterThan(0)
                .WithMessage("Price must be greater than 0.");

        }
    }
}

