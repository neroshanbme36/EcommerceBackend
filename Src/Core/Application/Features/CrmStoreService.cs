using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contracts.Features;
using Application.Contracts.Persistence.Crm;
using Application.Dtos.Store;
using Application.Exceptions;
using AutoMapper;
using Domain.Crm.Entities;

namespace Application.Features
{
    public class CrmStoreService : ICrmStoreService
    {
        private readonly ICrmUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CrmStoreService(ICrmUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CrmStoreDto?> GetStore(string id)
        {
            Store? store = await _unitOfWork.StoreRepository.Get(id);
            if (store == null) throw new NotFoundException("Store doesnt exist", id);
            return _mapper.Map<CrmStoreDto>(store);
        }

        public async Task<Store?> GetStoreByGuid(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid)) throw new BadRequestException("Store guid is required");

            Store? store = await _unitOfWork.StoreRepository.GetStoreByGuid(guid);
            if (store == null) throw new NotFoundException("Store doesnt exist", guid);
            if (!string.Equals(store.Guid, guid)) throw new BadRequestException("Store is invalid");
            return store;
        }
    }
}