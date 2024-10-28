using Application.Dtos.Attribute;

namespace Application.Dtos.Bootstrap
{
    public class BrowseCategoryPageResourceDto
    {
        public IReadOnlyList<AttributeDto> AttributeValues {get; set;} = new List<AttributeDto>();
    }
}