using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<IReadOnlyList<Department>> GetDepartments();
        Task<IReadOnlyList<Department>> GetHomePageDepartments();
    }
}