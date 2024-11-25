using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IEposTransactionHeaderRepository : IGenericRepository<EposTransactionHeader>
    {
        Task<EposTransactionHeader?> GetEposTransactionHeaderByGuid(string guid);
    }
}