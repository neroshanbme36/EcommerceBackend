using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IPostedTransactionHeaderRepository : IGenericRepository<PostedTransactionHeader>
    {
        Task<PostedTransactionHeader?> GetHeaderIncLines(long id);
        Task<bool> AnyByEcommOrderId(long ecommOrderId);
    }
}