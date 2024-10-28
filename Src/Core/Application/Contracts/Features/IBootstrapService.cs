using Application.Dtos.Bootstrap;

namespace Application.Contracts.Features
{
    public interface IBootstrapService
    {
        Task<PrimeBaseResponseDto> GetPrimeBase(string deviceId, string userEmail, PrimeBaseRequestDto request);
        Task<HomePageResourceDto> GetHomePageResource();
        Task<BrowseCategoryPageResourceDto> GetBrowseCategoryPageResource();
    }
}