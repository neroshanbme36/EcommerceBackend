using Application.Contracts.Features;
using Application.Contracts.Persistence;
using Application.Dtos.Country;
using AutoMapper;
using Domain.Entities;

namespace Application.Features
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork uow,  IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CountryDto>> GetCountries()
        {
            IReadOnlyList<Country> countries = await _uow.CountryRepository.GetAll();
            return _mapper.Map<IReadOnlyList<CountryDto>>(countries);
        }
    }
}