using Application.Contracts.Features;
using Application.Contracts.Persistence;
using Application.Dtos.Banner;
using AutoMapper;
using Domain.Entities;

namespace Application.Features
{
    public class BannerService : IBannerService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BannerService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<HeroBannerDto>> GetHeroBanners()
        {
            IReadOnlyList<MediaFile> mediaFiles = await _uow.MediaFileRepository.GetMediaFilesByEntityTypeAndType("Banners", "Hero");
            return _mapper.Map<IReadOnlyList<HeroBannerDto>>(mediaFiles);
        }
    }
}