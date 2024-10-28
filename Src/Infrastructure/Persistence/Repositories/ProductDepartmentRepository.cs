using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProductDepartmentRepository : GenericRepository<ProductDepartment>, IProductDepartmentRepository
    {
        private readonly MainDbContext _dbContext;

        public ProductDepartmentRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<ProductDepartment>> GetProductDepartmentsByDeptIds(IReadOnlyList<string> departmentIds)
        {
            return await _dbContext.ProductDepartments
                .Where(c => departmentIds.Contains(c.DepartmentId))
                .ToListAsync();
        }
    }
}