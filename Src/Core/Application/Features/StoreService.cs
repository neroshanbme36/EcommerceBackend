using Application.Contracts.Features;
using Application.Contracts.Persistence;
using Application.Dtos.Store;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;

namespace Application.Features
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StoreDto> GetStore()
        {
            Store? store = await _unitOfWork.StoreRepository.GetTopStore();
            if (store == null) throw new NotFoundException("Store doesnt exist", string.Empty);
            return _mapper.Map<StoreDto>(store);
        }
    }
}