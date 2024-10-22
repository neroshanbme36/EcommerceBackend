using Application.Dtos.Store;
using Application.Profiles.Resolvers;
using AutoMapper;
using Domain.Crm.Entities;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ReverseMap() represents the reversal of the mapping
            // CreateMap<Source, Destination>();
            #region Store
            CreateMap<Store, StoreDto>()
              .ForMember(dest => dest.LogoImgUrl, opt => opt.MapFrom<StoreLogoImgUrlResolver>());
            #endregion Store
        }
    }
}