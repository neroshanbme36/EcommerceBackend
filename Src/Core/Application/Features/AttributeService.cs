using Application.Contracts.Features;
using Application.Contracts.Persistence;
using Application.Dtos.Attribute;
using AutoMapper;
using Domain.Entities;

namespace Application.Features
{
    public class AttributeService : IAttributeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AttributeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<AttributeDto>> GetAttributeValues()
        {
            List<AttributeDto> attributeDtos = new List<AttributeDto>();

            IReadOnlyList<Department> departments = await _uow.DepartmentRepository.GetDepartments();
            if (departments.Count > 0)
            {
                IReadOnlyList<AttributeValueDto> departmentAttributeValues = departments.Select(c => new AttributeValueDto() { Value = c.Id }).ToList();
                AttributeDto categoryAttributeDto = new AttributeDto
                {
                    Name = "Category",
                    PluralName = "Categories",
                    FilterType = "Include",
                    Style = "Checkbox",
                    Priority = 1 + attributeDtos.Count,
                    IsSearchable = true,
                    AttributeValues = departmentAttributeValues
                };
                attributeDtos.Add(categoryAttributeDto);
            }

            AttributeDto brandAttributeDto = new AttributeDto
            {
                Name = "Brand",
                PluralName = "Brands",
                FilterType = "Include",
                Style = "MultiCheckbox",
                Priority = 1 + attributeDtos.Count,
                IsSearchBar = true,
                IsSearchable = true,
                // AttributeValues = departmentAttributeValues
            };

            AttributeDto priceAttributeDto = new AttributeDto
            {
                Name = "Price",
                PluralName = "Price",
                PrefixValueText = "GBP",
                FilterType = "Range",
                Style = "Slider",
                Priority = 1 + attributeDtos.Count,
                IsSearchable = true,
                AttributeValueRangeDtos = new List<AttributeValueRangeDto>{
                    new AttributeValueRangeDto
                    {
                        MinValue = 0.00m,
                        MaxValue = 10000.00m // can be taken from products max price
                    }
                }
            };
            attributeDtos.Add(priceAttributeDto);

            AttributeDto ratingAttributeDto = new AttributeDto
            {
                Name = "Rating",
                PluralName = "Rating",
                FilterType = "Range",
                Style = "Rating",
                Priority = 1 + attributeDtos.Count,
                IsSearchable = true,
            };
            attributeDtos.Add(ratingAttributeDto);

            return attributeDtos;
        }
    }
}