using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IConfigurationRepository : IGenericRepository<Configuration>
    {
        Task<IReadOnlyList<Configuration>> GetConfigurationsByDeviceIdAndCommon(string deviceId);
    }
}