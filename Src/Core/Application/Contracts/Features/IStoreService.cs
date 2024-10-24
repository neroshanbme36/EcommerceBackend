using Application.Dtos.Store;

namespace Application.Contracts.Features
{
    public interface IStoreService
    {
        Task<StoreDto> GetStore();
    }
}