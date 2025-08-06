using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductCategoryFeatures.Commands
{
    public record DeleteCategoryCommand(int id) : IRequest<bool>
    {
    }
}
