using Domain.Common;

namespace Domain.Entities
{
    public class ProductDepartment : BaseEntity
    {
        public string ItemNo { get; set; }  = string.Empty;
        public Product? Product {get; set;}

        public string DepartmentId {get; set;} = string.Empty;
        public Department? Department {get; set;} 
    }
}