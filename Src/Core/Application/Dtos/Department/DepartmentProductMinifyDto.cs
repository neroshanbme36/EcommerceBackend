using Application.Dtos.Product;

namespace Application.Dtos.Department
{
    public class DepartmentProductMinifyDto : BaseDepartmentDto
    {
        public IReadOnlyList<ProductMinifyDto> Products {get; set;} = new List<ProductMinifyDto>(); 
    }
}