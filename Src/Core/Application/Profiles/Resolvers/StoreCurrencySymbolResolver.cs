using System.Globalization;
using Application.Dtos.Store;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles.Resolvers
{
    public class StoreCurrencySymbolResolver : IValueResolver<Store, StoreDto, string>
    {
        public StoreCurrencySymbolResolver()
        {
           
        }

        public string Resolve(Store source, StoreDto destination, string destMember, ResolutionContext context)
        {
            RegionInfo regionInfo = new RegionInfo(source.CountryId);
            return regionInfo.CurrencySymbol;
        }
    }
}