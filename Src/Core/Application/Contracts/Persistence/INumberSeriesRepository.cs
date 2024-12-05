using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface INumberSeriesRepository : IGenericRepository<NumberSeries>
    {
        Task<NumberSeries?> GetNoSeriesByTbNameAndDevId(string tableName, string deviceId);
    }
}