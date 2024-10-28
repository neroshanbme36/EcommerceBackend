namespace Application.Dtos.Department
{
    public class DepartmentDto : BaseDepartmentDto
    {
        public string? ParentId { get; set; }
        public string? LongDescription { get; set; }
        public int NoOfItems {get; set;}
    }
}