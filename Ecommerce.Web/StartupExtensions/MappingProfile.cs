using AutoMapper;
using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.DTO.VendorDTOs;
using eCommerce.Application.Features.BrandFeature.Commands;
using eCommerce.Application.Features.BrandFeature.Dtos;
using eCommerce.Application.Features.ProductCategoryFeatures.Dtos;
using eCommerce.Application.Features.ProductFeatures.Dtos;
using eCommerce.Domain.Entities;
using eCommerce.Web.Areas.Admin.Models.Brand;
using eCommerce.Web.Areas.Admin.Models.Product;
using eCommerce.Web.Areas.Vendor.Models;
using eCommerce.Web.Models.ProductModels;

namespace eCommerce.Web.StartupExtensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<ProductCategory, ProductCategoryDto>()
                .ForMember(dest => dest.ParentCategoryName,
                           opt => opt.MapFrom(src => src.ParentCategory != null
                                   ? src.ParentCategory.CategoryName
                                   : string.Empty));

            CreateMap<ProductCategory, ProductCategoryDetailsDto>();

            CreateMap<Product, ProductDto>();
            CreateMap<UpdateBrandViewModel, UpdateBrandCommand>();
            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, BrandViewModel>();
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
