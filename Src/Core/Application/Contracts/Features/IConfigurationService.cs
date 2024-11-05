using Application.Dtos.Configuration;

namespace Application.Contracts.Features
{
    public interface IConfigurationService
    {
        Task<ConfigAttributeValueDto> GetConfigAttributeValue();
    }
}