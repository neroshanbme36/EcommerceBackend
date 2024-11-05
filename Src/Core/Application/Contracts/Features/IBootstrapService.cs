using Application.Dtos.Bootstrap;

namespace Application.Contracts.Features
{
    public interface IBootstrapService
    {
        Task<PrimeBaseResponseDto> GetPrimeBase(string userEmail, PrimeBaseRequestDto request);
        Task<HomePageResourceDto> GetHomePageResource();
        Task<BrowseCategoryPageResourceDto> GetBrowseCategoryPageResource();
    }
}