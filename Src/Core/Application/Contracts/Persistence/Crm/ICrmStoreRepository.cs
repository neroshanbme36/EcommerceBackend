using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence.Crm
{
    public interface ICrmStoreRepository : ICrmGenericRepository<Domain.Crm.Entities.Store>
    {
        Task<IReadOnlyList<Domain.Crm.Entities.Store>> GetStoresByCustomerId(string customerId);
        Task<IReadOnlyList<Domain.Crm.Entities.Store>> GetStoresByAppCompanyId(string appCompanyId);
        Task<Domain.Crm.Entities.Store?> GetStoreByGuid(string guid);
    }
}