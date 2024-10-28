using Application.Dtos.Banner;

namespace Application.Contracts.Features
{
    public interface IBannerService
    {
        Task<IReadOnlyList<HeroBannerDto>> GetHeroBanners();
    }
}