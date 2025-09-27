using AutoMapper;
using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.DTO.VendorDTOs;
using eCommerce.Application.Features.BrandFeature.Commands;
using eCommerce.Application.Features.BrandFeature.Dtos;
using eCommerce.Application.Features.ProductCategoryFeatures.Dtos;
using eCommerce.Application.Features.ProductFeatures.Dtos;
using eCommerce.Application.Features.ProductVariantFeatures.Dtos;
using eCommerce.Domain.Entities;
using eCommerce.Web.Areas.Admin.Models.Brand;
using eCommerce.Web.Areas.Admin.Models.Product;
using eCommerce.Web.Areas.Seller.Models;
using eCommerce.Web.Areas.Vendor.Models;
using eCommerce.Web.ViewModels.ProductFeatureVMs;
using eCommerce.Web.ViewModels.ProductVariantVMs;
using eCommerce.Web.ViewModels.ProductVMs;

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


            CreateMap<FeatureCategoryDTO, FeatureCategoryViewModel>();
            CreateMap<FeatureCategoryDTO, FeatureCategoryViewModel>().ReverseMap();

            CreateMap<ProductFeatureDTO, ProductFeatureViewModel>();
            CreateMap<ProductFeatureDTO, ProductFeatureViewModel>().ReverseMap();


            //Domain to DTO and DTO to Domain
            CreateMap<Product, ProductListDto>()
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category!.CategoryName))
                .ForMember(dest => dest.VariantCount,
                    opt => opt.MapFrom(src => src.ProductVariants.Count));

            CreateMap<Product, ProductDetailsDto>();

            CreateMap<ProductVariant, ProductVariantDto>()
                .ForMember(dest => dest.ImageUrls,
                    opt => opt.MapFrom(src => src.ProductImages.Select(pi => pi.ImageUrl)));
            
            
            
            CreateMap<ProductVariantSaveDTO, ProductVariant>();



            //DTO to VM and VM to DTO
            CreateMap<ProductListDto, ProductListVM>();

            CreateMap<ProductDetailsDto, ProductDetailsVM>();

            CreateMap<ProductSaveVM, ProductSaveDTO>();

            CreateMap<ProductVariantSaveVM, ProductVariantSaveDTO>();

            CreateMap<FeaturesVM, FeaturesDto>();            
            

            #endregion

            #region For Admin Models

            #endregion

            #region For Seller Models
            // Domain to DTO and DTO to Domain
            CreateMap<ProductSaveDTO, Product>();
            CreateMap<ProductVariant, ProductVariantDto>();

            //DTO to VM and VM to DTO
            CreateMap<ProductVariantSaveVM, SellerProductVariantDTO>();
            CreateMap<ProductVariantSaveVM, SellerProductVariantDTO>().ReverseMap();

            CreateMap<SellerProductViewModel, SellerProductDTO>();
            CreateMap<SellerProductViewModel, SellerProductDTO>().ReverseMap();

            #endregion

           
        }
    }
}
