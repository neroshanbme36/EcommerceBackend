using Application.Constants;
using Application.Contracts.Features;
using Application.Contracts.Persistence;
using Domain.Entities;

namespace Application.Features
{
    public class NumberSeriesService : INumberSeriesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NumberSeriesService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<NumberSeries?> GetNoSeries(string tableName, string deviceId)
        {
            return await _unitOfWork.NumberSeriesRepository.GetNoSeriesByTbNameAndDevId(tableName, deviceId);
        }

        public async Task<string> GenerateNewId(string tableName, NumberSeries noSeries)
        {
            if (noSeries == null) return string.Empty;
            string prefix = noSeries.Prefix ?? string.Empty;
            long newId = noSeries.LastNoUsed + 1;
            string newIdPfx = string.Empty;
            bool found = false;
            bool isIdExistInTb = false;
            while (!found)
            {
                newIdPfx = prefix + newId.ToString();
                if (tableName.Equals(EntityCodes.EposTransactionHeadersEcommOrderId))
                {
                    long id = Convert.ToInt64(newIdPfx);
                    isIdExistInTb = await _unitOfWork.EposTransactionHeaderRepository.AnyByEcommOrderId(id);
                    if (!isIdExistInTb)
                    {
                        isIdExistInTb = await _unitOfWork.PostedTransactionHeaderRepository.AnyByEcommOrderId(id);
                    }
                }

                if (isIdExistInTb)
                {
                    newId++;
                }
                else
                {
                    // newIdPfx = prefix + newIdPfx.ToString();
                    found = true;
                }
            }
            return newIdPfx;
        }

        public long GenerateLastNoUsed(string idPfx, string noSeriesPfx)
        {
            return long.Parse(idPfx.Substring(noSeriesPfx.Length, idPfx.Length - noSeriesPfx.Length));
        }
    }
}