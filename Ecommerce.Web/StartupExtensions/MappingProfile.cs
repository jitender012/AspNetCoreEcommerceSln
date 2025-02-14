using AutoMapper;
using eCommerce.Application.DTO;
using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.DTO.VendorDTOs;
using eCommerce.Domain.Entities;
using eCommerce.Web.Areas.Admin.Models.Product;
using eCommerce.Web.Areas.Vendor.Models;
using eCommerce.Web.Models.ProductModels;

namespace eCommerce.Web.StartupExtensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Common Models

            CreateMap<ProductDTO, ProductViewModel>();
            CreateMap<ProductDTO, ProductViewModel>().ReverseMap();

            CreateMap<ProductVariantDTO, ProductVariantViewModel>();
            CreateMap<ProductVariantDTO, ProductVariantViewModel>().ReverseMap();

            CreateMap<ProductImagesDTO, ProductImageViewModel>().ReverseMap();
            CreateMap<ProductImagesDTO, ProductImageViewModel>();

            CreateMap<FeatureOptionDTO, FeatureOptionViewModel>();
            CreateMap<FeatureOptionDTO, FeatureOptionViewModel>().ReverseMap();

            CreateMap<FeatureCategoryDTO, FeatureCategoryViewModel>();
            CreateMap<FeatureCategoryDTO, FeatureCategoryViewModel>().ReverseMap();

            CreateMap<ProductFeatureDTO, ProductFeatureViewModel>();
            CreateMap<ProductFeatureDTO, ProductFeatureViewModel>().ReverseMap();
            #endregion

            #region For Admin Models

            #endregion

            #region For Seller Models
            CreateMap<SellerProductVariantViewModel, SellerProductVariantDTO>();
            CreateMap<SellerProductVariantViewModel, SellerProductVariantDTO>().ReverseMap();

            CreateMap<SellerProductViewModel, SellerProductDTO>();
            CreateMap<SellerProductViewModel, SellerProductDTO>().ReverseMap();            
            #endregion
        }
    }
}
