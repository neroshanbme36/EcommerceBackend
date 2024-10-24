using Application.Constants;
using Application.Contracts.Features;
using Application.Contracts.Persistence;
using Application.Dtos.Configuration;
using Application.Extensions;
using Domain.Entities;

namespace Application.Features
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConfigurationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ConfigAttributeValueDto> GetConfigAttributeValue(string deviceId)
        {
            ConfigAttributeValueDto dto = new ConfigAttributeValueDto();
            var configurations = await _unitOfWork.ConfigurationRepository.GetConfigurationsByDeviceIdAndCommon(deviceId);
            foreach (Configuration config in configurations)
            {
                switch (config.Code)
                {
                    case ConfigurationCodes.OnlineCardPayment:
                        dto.OnlineCardPayment = config.Value.ToBoolean();
                        break;
                    case ConfigurationCodes.CashOnDelivery:
                        dto.CashOnDelivery = config.Value.ToBoolean();
                        break;
                    case ConfigurationCodes.PlayStore:
                        dto.PlayStore = config.Value;
                        break;
                    case ConfigurationCodes.AppStore:
                        dto.AppStore = config.Value;
                        break;
                    case ConfigurationCodes.FaceBook:
                        dto.FaceBook = config.Value;
                        break;
                    case ConfigurationCodes.Instagram:
                        dto.Instagram = config.Value;
                        break;
                    case ConfigurationCodes.Twitter:
                        dto.Twitter = config.Value;
                        break;
                    case ConfigurationCodes.PrimaryColor:
                        dto.PrimaryColor = config.Value;
                        break;
                    case ConfigurationCodes.HoverColor:
                        dto.HoverColor = config.Value;
                        break;
                    case ConfigurationCodes.FooterColor:
                        dto.FooterColor = config.Value;
                        break;
                    case ConfigurationCodes.SideMenuColor:
                        dto.SideMenuColor = config.Value;
                        break;
                    // case ConfigurationCodes.MaxAmount:
                    //     dto.MaxAmount = config.Value.ToDecimal();
                    //     break;
                    // case ConfigurationCodes.MaxQuantity:
                    //     dto.MaxQuantity = config.Value.ToInt();
                    //     break;
                    default:
                        break;
                }
            }
            return dto;
        }
    }
}