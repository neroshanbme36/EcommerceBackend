using Application.Dtos.CloudStoreEpos.Epos;
using Application.Dtos.Country;
using Application.Dtos.Department;
using Application.Dtos.Store;
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
      #region Country
      CreateMap<Country, CountryDto>();
      #endregion Country
      #region Department
      CreateMap<Department, DepartmentDto>();
      #endregion Department
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