using Application.Contracts.Features;
using Application.Contracts.Persistence;
using Application.Dtos.CustomerAddress;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;

namespace Application.Features
{
    public class CustomerAddressService : ICustomerAddressService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CustomerAddressService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CustomerAddressDto>> GetCustomerAddressesByEcommUserId(string ecommUserId)
        {
            IReadOnlyList<CustomerAddress> customerAddresses = await _uow.CustomerAddressRepository.GetCustomerAddressesByEcommUserId(ecommUserId);
            return _mapper.Map<IReadOnlyList<CustomerAddressDto>>(customerAddresses);
        }

        public async Task<AddOrEditCustomerAddressDto> AddOrEditCustomerAddress(string ecommUserId, AddOrEditCustomerAddressDto request)
        {
            bool isCountryExist = await _uow.CountryRepository.IsAny(request.CountryId);
            if (!isCountryExist) throw new BadRequestException("Country doesnt exist");

            

            CustomerAddress? defaultCustomerAddress = await _uow.CustomerAddressRepository.GetDefaultCustomerAddress(ecommUserId, request.Category);
            if (defaultCustomerAddress != null)
            {
                if (request.IsDefault && request.Id != defaultCustomerAddress.Id)
                {
                    defaultCustomerAddress.IsDefault = false;
                    await _uow.CustomerAddressRepository.Update(defaultCustomerAddress);
                }
            }
            else
            {
                request.IsDefault = true;
            }

            CustomerAddress customerAddress = _mapper.Map<CustomerAddress>(request);
            customerAddress.EcommUserId = ecommUserId;

            if (customerAddress.Id == 0)
            {
                await _uow.CustomerAddressRepository.Add(customerAddress);
            }
            else
            {
                CustomerAddress? customerAddressRepo = await _uow.CustomerAddressRepository.Get(customerAddress.Id);
                if (customerAddressRepo == null) throw new NotFoundException("Address doesnt exist", customerAddress.Id);

                if (!string.Equals(customerAddress.EcommUserId, customerAddressRepo.EcommUserId))
                    throw new BadRequestException("UserId mismatch");

                customerAddress.CustomerId = customerAddressRepo.CustomerId;
                await _uow.CustomerAddressRepository.Update(customerAddress);
            }

            bool isSaved = await _uow.Save() > 0;
            if (!isSaved) throw new InternalServerErrorException("Address save failed");

            request.Id = customerAddress.Id;
            return request;
        }

        public async Task DeleteCustomerAddress(string ecommUserId, long id)
        {
            CustomerAddress? customerAddress = await _uow.CustomerAddressRepository.Get(id);
            if (customerAddress == null) throw new NotFoundException("Address doesnt exist", id);

            if (!string.Equals(customerAddress.EcommUserId, ecommUserId))
                throw new BadRequestException("UserId mismatch");

            if (customerAddress.IsDefault)
                throw new BadRequestException($"Please change another {customerAddress.Category} address to default");

            await _uow.CustomerAddressRepository.Delete(customerAddress);
            bool isSaved = await _uow.Save() > 0;
            if (!isSaved) throw new InternalServerErrorException("Address delete failed");
        }
    }
}