using eCommerce.Application.Features.BrandFeature.Commands;
using FluentValidation;

namespace eCommerce.Application.Features.BrandFeature.Validators
{
    public class CreateBrandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandValidator()
        {
            RuleFor(x => x.Dto.BrandName)
                .NotEmpty()
                .WithMessage("Brand name is required.");
        }
    }    
}
