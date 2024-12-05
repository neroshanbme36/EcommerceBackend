using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IEposTransactionHeaderRepository : IGenericRepository<EposTransactionHeader>
    {
        Task<EposTransactionHeader?> GetEposTransactionHeaderIncLinesByGuid(string guid);
        Task<EposTransactionHeader?> GetEposTransactionHeaderByGuid(string guid);
        Task<bool> AnyByEcommOrderId(long ecommOrderId);
    }
}