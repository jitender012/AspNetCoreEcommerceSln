using eCommerce.Application.Features.BrandFeature.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.BrandFeature.Validators
{
    public class UpdateBrandValidator : AbstractValidator<UpdateBrandCommand>
    {
        //public UpdateBrandValidator()
        //{
        //    RuleFor(x => x.BrandName)
        //        .NotEmpty()
        //        .WithMessage("Brand name is required.");
            
        //}
    }
}
