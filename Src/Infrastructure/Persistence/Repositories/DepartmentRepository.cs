using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly MainDbContext _dbContext;

        public DepartmentRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Department>> GetDepartments()
        {
            return await _dbContext.Departments
                .Where(c => c.ShowInEcommerce)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Department>> GetHomePageDepartments()
        {
            return await _dbContext.Departments
                .Where(c => c.ShowInEcommerce && c.ShowEcommHome
                 && string.IsNullOrWhiteSpace(c.ParentId))
                .ToListAsync();
        }
    }
}