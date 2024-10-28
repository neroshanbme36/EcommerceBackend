using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IProductDepartmentRepository : IGenericRepository<ProductDepartment>
    {
        Task<IReadOnlyList<ProductDepartment>> GetProductDepartmentsByDeptIds(IReadOnlyList<string> departmentIds);
    }
}