using Application.Contracts.Features;
using Application.Contracts.Persistence;
using Application.Dtos.Attribute;
using Application.Dtos.Store;
using Domain.Entities;

namespace Application.Features
{
    public class AttributeService : IAttributeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IStoreService _storeService;

        public AttributeService(IUnitOfWork uow, IStoreService storeService)
        {
            _uow = uow;
            _storeService = storeService;
        }

        public async Task<IReadOnlyList<AttributeDto>> GetAttributeValues()
        {
            List<AttributeDto> attributeDtos = new List<AttributeDto>();
            StoreDto store = await _storeService.GetStore();
            // IReadOnlyList<Department> departments = await _uow.DepartmentRepository.GetDepartments();
            // if (departments.Count > 0)
            // {
            //     IReadOnlyList<AttributeValueDto> departmentAttributeValues = departments.Select(c => new AttributeValueDto() { Value = c.Id }).ToList();
            //     AttributeDto categoryAttributeDto = new AttributeDto
            //     {
            //         Name = "Category",
            //         PluralName = "Categories",
            //         FilterType = "Include",
            //         Style = "Checkbox",
            //         Priority = 1 + attributeDtos.Count,
            //         IsSearchable = true,
            //         AttributeValues = departmentAttributeValues
            //     };
            //     attributeDtos.Add(categoryAttributeDto);
            // }

            IReadOnlyList<Brand> brands = await _uow.BrandRepository.GetBrands();
            if (brands.Count > 0)
            {
                IReadOnlyList<AttributeValueDto> brandAttributeValues = brands.Select(c => new AttributeValueDto() { Value = c.Name }).ToList();
                AttributeDto brandAttributeDto = new AttributeDto
                {
                    Name = "Brand",
                    PluralName = "Brands",
                    FilterType = "Include",
                    Style = "MultiCheckbox",
                    Priority = 1 + attributeDtos.Count,
                    IsSearchBar = true,
                    IsSearchable = true,
                    AttributeValues = brandAttributeValues
                };
                attributeDtos.Add(brandAttributeDto);
            }

            AttributeDto priceAttributeDto = new AttributeDto
            {
                Name = "Price",
                PluralName = "Price",
                PrefixValueText = store.CurrencySymbol,
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

            AttributeDto availabiltyAttributeDto = new AttributeDto
            {
                Name = "Availability",
                PluralName = "Availability",
                FilterType = "Include",
                Style = "Checkbox",
                Priority = 1 + attributeDtos.Count,
                IsSearchable = true,
                AttributeValues = new List<AttributeValueDto>
                {
                    new AttributeValueDto {Value = "All", IsDefault = true},
                    new AttributeValueDto {Value = "In Stock"},
                    new AttributeValueDto {Value = "Out Of Stock"}
                }
            };
            attributeDtos.Add(availabiltyAttributeDto);

            return attributeDtos;
        }
    }
}