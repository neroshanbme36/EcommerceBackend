namespace Application.Dtos.Department
{
    public class DepartmentDto
    {
        public string Id { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ParentId { get; set; }
        public string? LongDescription { get; set; }
    }
}