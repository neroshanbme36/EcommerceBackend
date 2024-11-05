using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Crm.Entities;

namespace Application.Contracts.Persistence.Crm
{
    public interface IDeviceRepository : ICrmGenericRepository<Device>
    {
        Task<Device?> GetEcommerceDevice(string storeId);
    }
}