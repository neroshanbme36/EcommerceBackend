using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos.Store;
using Domain.Crm.Entities;

namespace Application.Contracts.Features
{
    public interface ICrmStoreService
    {
        Task<CrmStoreDto?> GetStore(string id);
        Task<Store?> GetStoreByGuid(string guid);
    }
}