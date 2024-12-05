using Domain.Entities;

namespace Application.Contracts.Features
{
    public interface INumberSeriesService
    {
        Task<NumberSeries?> GetNoSeries(string tableName, string deviceId);
        Task<string> GenerateNewId(string tableName, NumberSeries noSeries);
        long GenerateLastNoUsed(string idPfx, string noSeriesPfx);
    }
}