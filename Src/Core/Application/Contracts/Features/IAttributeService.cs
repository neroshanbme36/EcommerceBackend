using Application.Dtos.Attribute;

namespace Application.Contracts.Features
{
    public interface IAttributeService
    {
        Task<IReadOnlyList<AttributeDto>> GetAttributeValues();
    }
}