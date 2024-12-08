using Application.Dtos.Attribute;
using Application.Dtos.Banner;
using Application.Dtos.Cart;
using Application.Dtos.CloudStoreEpos.Epos;
using Application.Dtos.Country;
using Application.Dtos.CustomerAddress;
using Application.Dtos.Department;
using Application.Dtos.Product;
using Application.Dtos.Store;
using Application.Dtos.Tag;
using Application.Dtos.WishlistProduct;
using Application.Profiles.Resolvers;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      // ReverseMap() represents the reversal of the mapping
      // CreateMap<Source, Destination>();
      #region Store
      CreateMap<Domain.Entities.Store, StoreDto>()
        .ForMember(dest => dest.LogoImgUrl, opt => opt.MapFrom<StoreLogoImgUrlResolver>())
        .ForMember(dest => dest.CurrencySymbol, opt => opt.MapFrom<StoreCurrencySymbolResolver>());
      #endregion Store
      #region CUSTOMER
      CreateMap<CustomerAddress, CustomerAddressDto>();
      CreateMap<AddOrEditCustomerAddressDto, CustomerAddress>();
      CreateMap<CustomerAddress, BaseCustomerAddressDto>();
      #endregion CUSTOMER
      #region Country
      CreateMap<Country, CountryDto>();
      #endregion Country
      #region Department
      CreateMap<Department, DepartmentDto>();
      CreateMap<DepartmentDto, DepartmentProductMinifyDto>();
      CreateMap<Department, DepartmentMinifyDto>();
      #endregion Department
      #region PRODUCT
      CreateMap<Product, Dtos.Product.ProductDto>();
      CreateMap<Dtos.Product.ProductDto, ProductMinifyDto>();
      CreateMap<Product, ProductDetailDto>();
      #endregion PRODUCT
      #region WISHLIST PRODUCT
      CreateMap<AddWistlistProductDto, WishlistProduct>();
      CreateMap<WishlistProduct, WishlistProductDto>();
      #endregion WISHLIST PRODUCT
      #region TAG
      CreateMap<Tag, TagDto>();
      #endregion TAG
      #region MEDIA FILE
      CreateMap<MediaFile, HeroBannerDto>();
      #endregion MEDIA FILE
      #region ATTRIBUTE
      CreateMap<Domain.Entities.Attribute, AttributeDto>();
      #endregion ATTRIBUTE
      #region ATTRIBUTE VALUE
      CreateMap<AttributeValue, AttributeValueDto>();
      #endregion ATTRIBUTE VALUE
      #region EPOS
      #region POSTED TRANSACTION
      CreateMap<PostedTransactionHeader, OrderHeaderDto>();
      CreateMap<PostedTransactionLine, OrderLineDto>();
      #endregion POSTED TRANSACTION

      #region EPOS TRANSACTION
      CreateMap<EposTransactionHeader, OrderHeaderDto>();
      CreateMap<EposTransactionLine, OrderLineDto>();
      //CreateMap<RepairPaymentDto, PaymentDto>();
      CreateMap<CartHeaderInputDto, EposTransHeaderInputDto>()
        .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.CartId));
      CreateMap<CartLineInputDto, EposTransLineInputDto>()
        .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.CartId));
      #endregion EPOS TRANSACTION
      #endregion EPOS
    }
  }
}