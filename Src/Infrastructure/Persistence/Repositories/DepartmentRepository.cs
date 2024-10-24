using Application.Contracts.Persistence;
using Domain.Entities;

namespace Persistence.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly MainDbContext _dbContext;

        public DepartmentRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}