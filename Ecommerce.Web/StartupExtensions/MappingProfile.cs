using AutoMapper;
using eCommerce.Application.Common.Dtos;
using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.DTO.VendorDTOs;
using eCommerce.Application.Features.BrandFeature.Dtos;
using eCommerce.Application.Features.FeatureCategoryFeatures.Dtos;
using eCommerce.Application.Features.MeasurementUnitFeature.Dtos;
using eCommerce.Application.Features.ProductCategoryFeatures.Dtos;
using eCommerce.Application.Features.ProductConfigurationFeature.DTOs;
using eCommerce.Application.Features.ProductFeatures.Dtos;
using eCommerce.Application.Features.ProductVariantFeatures.Dtos;
using eCommerce.Application.Features.WarehouseFeature.Dtos;
using eCommerce.Application.Features.WishlistFeature.Dtos;
using eCommerce.Domain.Entities;
using eCommerce.Web.Areas.Admin.Models.Brand;
using eCommerce.Web.Areas.Admin.Models.FeatureCategory;
using eCommerce.Web.Areas.Admin.Models.Product;
using eCommerce.Web.Areas.Admin.Models.ProductCategory;
using eCommerce.Web.Areas.Admin.Models.ProductFeature;
using eCommerce.Web.Areas.Vendor.Models;
using eCommerce.Web.Models;
using eCommerce.Web.ViewModels.ProductVariantVMs;
using eCommerce.Web.ViewModels.ProductVMs;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

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

            #region Common Models

            CreateMap(typeof(IdNameDto<>), typeof(IdNameVm<>)).ReverseMap();

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
            // Domain to DTO and DTO to Domain
            CreateMap<Brand, BrandListDTO>()
                .ForMember(x => x.ProductCount, opt => opt.MapFrom(src => src.Products.Count));
            CreateMap<Brand, BrandDetailsDto>()
                .ForMember(dest => dest.TotalProducts, opt => opt.MapFrom(src => src.Products.Count))
                .ForMember(dest => dest.ProductNames, opt => opt.MapFrom(src => src.Products.Select(p => p.ProductName).ToList()));
            CreateMap<Brand, BrandSaveDto>();
            CreateMap<BrandSaveDto, Brand>();

            CreateMap<ProductFeature, FeatureListDTO>()
                .ForMember(dest => dest.FeatureCategoryName,
                           opt => opt.MapFrom(src => src.FeatureCategory != null
                                   ? src.FeatureCategory.Name
                                   : string.Empty))
                .ForMember(dest => dest.MeasurementUnit,
                           opt => opt.MapFrom(src => src.MeasurementUnit != null
                                   ? src.MeasurementUnit.UnitName
                                   : string.Empty));

            CreateMap<ProductFeature, FeatureDetailsDTO>()
                .ForMember(dest => dest.FeatureCategoryName,
                           opt => opt.MapFrom(src => src.FeatureCategory != null
                                   ? src.FeatureCategory.Name
                                   : string.Empty))
                .ForMember(dest => dest.MeasurementUnit,
                           opt => opt.MapFrom(src => src.MeasurementUnit != null
                                   ? src.MeasurementUnit.UnitName
                                   : string.Empty))
                .ForMember(dest => dest.FeatureOptions,
                           opt => opt.MapFrom(src => src.FeatureOptions.Select(fo => fo.Value).ToList()));
            CreateMap<FeatureSaveDTO, ProductFeature>()
                .ForMember(des => des.FeatureOptions,
                opt => opt.Ignore());

            CreateMap<FeatureOptionDTO, FeatureOption>();

            CreateMap<FeatureCategory, FeatureCategoryListDTO>();

            CreateMap<MeasurementUnit, MeasurementUnitDTO>();

            CreateMap<FeatureCategory, FeatureCategoryListDTO>();
            //CreateMap<FeatureCategory, FeatureCategoryDetailsDTO>()
            //    .ForMember(dest => dest.ProductFeatures,
            //        opt => opt.MapFrom(src => src.ProductFeatures
            //        .Select(f => new IdNameDto<int> { Id = f.ProductFeaturesId, Name = f.Name }).ToList()
            //        ))
            //    .ForMember(dest => dest.ProductCategories,
            //        opt => opt.MapFrom(src => src.ProductCategoryFeatures
            //        .Select(x => new IdNameDto<int>
            //            {
            //                Id = x.ProductCategory.ProductCategoryId,
            //                Name = x.ProductCategory.CategoryName
            //            })));
            CreateMap<FeatureCategorySaveDto, FeatureCategory>();

            CreateMap<ProductCategory, ProductCategoryListDto>()
                .ForMember(dest => dest.ParentCategoryName,
                           opt => opt.MapFrom(src => src.ParentCategory != null
                                   ? src.ParentCategory.CategoryName
                                   : null));


            //----DTO to VM and VM to DTO----

            CreateMap<BrandListDTO, BrandListVM>();
            CreateMap<BrandDetailsDto, BrandVM>();
            CreateMap<BrandSaveDto, BrandSaveVM>();            
            CreateMap<BrandSaveVM, BrandSaveDto>();            

            CreateMap<FeatureListDTO, FeatureListVM>();
            CreateMap<FeatureDetailsDTO, FeatureDetailsVM>();
            CreateMap<FeatureSaveVM, FeatureSaveDTO>();

            CreateMap<FeatureOptionSaveDTO, FeatureOptionDTO>();

            CreateMap<FeatureCategoryListDTO, FeatureCategoryListVm>();
            CreateMap<FeatureCategoryDetailsDTO, FeatureCategoryDetailsVm>();
            CreateMap<FeatureCategorySaveVm, FeatureCategorySaveDto>();

            CreateMap<ProductCategoryListDto, ProductCategoryListVm>();
            #endregion

            #region For Seller Models
            // Domain to DTO and DTO to Domain
            CreateMap<ProductSaveDTO, Product>();
            CreateMap<ProductVariant, ProductVariantDto>();

            CreateMap<WarehouseSaveDto, Warehouse>();
            CreateMap<Warehouse, WarehouseDto>();
            CreateMap<Warehouse, WarehouseListDto>();

            //DTO to VM and VM to DTO
            CreateMap<ProductVariantSaveVM, SellerProductVariantDTO>();
            CreateMap<ProductVariantSaveVM, SellerProductVariantDTO>().ReverseMap();

            CreateMap<SellerProductViewModel, SellerProductDTO>();
            CreateMap<SellerProductViewModel, SellerProductDTO>().ReverseMap();

            #endregion

            #region For Customer Models
            // Domain to DTO and DTO to Domain
            CreateMap<WishlistSaveDto, Wishlist>();
            CreateMap<Wishlist, WishlistItemDto>()
                .ForMember(dest => dest.ProductVariantId,
                           opt => opt.MapFrom(src => src.ProductVariant.ProductVariantId))
                .ForMember(dest => dest.VariantName,
                           opt => opt.MapFrom(src => src.ProductVariant.VarientName))
                .ForMember(dest => dest.ProductPrice,
                           opt => opt.MapFrom(src => src.ProductVariant.Price))
                .ForMember(dest => dest.ProductImageUrl,
                           opt => opt.MapFrom(src => src.ProductVariant.ProductImages
                               .FirstOrDefault() != null
                               ? src.ProductVariant.ProductImages.FirstOrDefault()!.ImageUrl
                               : string.Empty));

            // DTO to VM and VM to DTO
            #endregion

        }
    }
}
