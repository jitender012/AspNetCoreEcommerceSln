using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.ServiceContracts;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Services.ProductServices
{
    public class ProductVariantService(IProductVariantRepository productVariantRepository, IUserContextService userContextService) : IProductVariantService
    {
        private readonly IProductVariantRepository _productVariantRepository = productVariantRepository;
        private readonly IUserContextService _userContextService = userContextService;

        public async Task<Guid> AddProductVariantAsync(ProductVariantDTO productVariant)
        {
           
            ProductVariant pv = new()
            {
                ProductIvarientId = Guid.NewGuid(),
                VarientName = productVariant.VarientName,
            };

            List<ProductImage> productImage = [];

            if (productVariant.ProductImagesDTO != null)
            {
                int order = 1;
                productImage = productVariant.ProductImagesDTO.Select(img => new ProductImage
                {
                    ProductVariantId = pv.ProductIvarientId,
                    ImageUrl = img.ImageUrl,
                    CreatedAt = DateTime.Now,
                    IsPrimary = img.IsPrimary,
                    Order = order++
                }).ToList();
            }
            
            List<FeatureOption> featureOption = [];
            if (productVariant.FeatureOptions != null)
            {
                featureOption = productVariant.FeatureOptions.Select(fo => new FeatureOption
                {
                   CreatedBy = _userContextService.GetUserId().ToString(),
                   Value = fo.Value
                }).ToList();
            }
           
            await _productVariantRepository.InsertProductVariantAsync(pv, productImage, featureOption);
            return pv.ProductIvarientId;
        }

        public Task<bool> DeleteProductVariantAsync(Guid productVariantId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductVariant>> GetProductVariantsAsync()
        {
           //_productVariantRepository.
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProductVariantAsync(ProductVariantDTO productVariant)
        {
            throw new NotImplementedException();
        }
    }
}
