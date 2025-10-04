using eCommerce.Application.Features.BrandFeature.Commands;
using eCommerce.Application.Features.BrandFeature.Dtos;
using eCommerce.Domain.RepositoryContracts;
using FluentValidation;

namespace eCommerce.Application.Features.BrandFeature.Validators
{
    public class BrandSaveValidator : AbstractValidator<BrandSaveDTO>
    {
        private readonly IBrandRepository _brandRepository;
        public BrandSaveValidator(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;

            RuleFor(x => x.BrandName)
                 .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Brand name is required.")
                .MustAsync(BeUniqueName)
                .WithMessage("Brand name already exists.");


        }
        private async Task<bool> BeUniqueName(string brandName, CancellationToken cancellationToken)
        {
            return !await _brandRepository.ExistsByNameAsync(brandName);
        }
    }
}
