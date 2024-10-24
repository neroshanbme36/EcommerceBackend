using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos.Store;
using Domain.Crm.Entities;

namespace Application.Contracts.Features
{
    public interface IStoreService
    {
        Task<CrmStoreDto?> GetStore(string id);
        Task<Store?> GetStoreByGuid(string guid);
    }
}