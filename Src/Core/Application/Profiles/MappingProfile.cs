using Application.Dtos.Attribute;
using Application.Dtos.Banner;
using Application.Dtos.CloudStoreEpos.Epos;
using Application.Dtos.Country;
using Application.Dtos.CustomerAddress;
using Application.Dtos.Department;
using Application.Dtos.Product;
using Application.Dtos.Store;
using Application.Dtos.Tag;
using Application.Profiles.Resolvers;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.CloudStore;

namespace Application.Profiles
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      // ReverseMap() represents the reversal of the mapping
      // CreateMap<Source, Destination>();
      #region CrmStore
      CreateMap<Domain.Crm.Entities.Store, CrmStoreDto>()
        .ForMember(dest => dest.LogoImgUrl, opt => opt.MapFrom<CrmStoreLogoImgUrlResolver>());
      #endregion CrmStore
      #region Store
      CreateMap<Domain.Entities.Store, StoreDto>()
        .ForMember(dest => dest.LogoImgUrl, opt => opt.MapFrom<StoreLogoImgUrlResolver>());
      #endregion Store
      #region CUSTOMER
      CreateMap<CustomerAddress, CustomerAddressDto>();
      CreateMap<AddOrEditCustomerAddressDto, CustomerAddress>();
      #endregion CUSTOMER
      #region Country
      CreateMap<Country, CountryDto>();
      #endregion Country
      #region Department
      CreateMap<Department, DepartmentDto>();
      CreateMap<DepartmentDto, DepartmentProductMinifyDto>();
      #endregion Department
      #region PRODUCT
      CreateMap<Product, Dtos.Product.ProductDto>();
      CreateMap<Dtos.Product.ProductDto, ProductMinifyDto>();
      CreateMap<Product, ProductDetailDto>();
      #endregion PRODUCT
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
      #endregion EPOS TRANSACTION
      #endregion EPOS
    }
  }
}