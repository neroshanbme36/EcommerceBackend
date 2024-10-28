using Application.Dtos.Department;

namespace Application.Contracts.Features
{
    public interface IDepartmentService
    {
        Task<IReadOnlyList<DepartmentDto>> GetDepartments();
        Task<IReadOnlyList<DepartmentDto>> GetHomePageDepartments();
    }
}