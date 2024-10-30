using Application.Dtos.Country;

namespace Application.Contracts.Features
{
    public interface ICountryService
    {
        Task<IReadOnlyList<CountryDto>> GetCountries();
    }
}