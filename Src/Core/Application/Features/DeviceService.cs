using Application.Contracts.Features;
using Application.Contracts.Persistence;
using Application.Contracts.Persistence.Crm;
using Application.Exceptions;
using Domain.Crm.Entities;

namespace Application.Features
{
    public class DeviceService : IDeviceService
    {
        private readonly ICrmUnitOfWork _crmUnitOfWork;
        private readonly IUnitOfWork _unitOfWork;

        public DeviceService(ICrmUnitOfWork crmUnitOfWork, IUnitOfWork unitOfWork)
        {
            _crmUnitOfWork = crmUnitOfWork;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GetEcommerceDeviceId()
        {
            Device? device = await GetEcommerceDevice();
            return device != null ? device.Id : string.Empty;
        }

        public async Task<Device?> GetEcommerceDevice()
        {
            Domain.Entities.Store? store = await _unitOfWork.StoreRepository.GetTopStore();
            if (store == null) throw new BadRequestException("Store doesnt exist");

            Device? device = await _crmUnitOfWork.DeviceRepository.GetEcommerceDevice(store.Id);
            if (device == null) throw new BadRequestException("Device doesnt exist");

            return device;
        }
    }
}