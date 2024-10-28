using Application.Contracts.Persistence;

namespace Persistence.Repositories
{
    public class AttributeRepository : GenericRepository<Domain.Entities.Attribute>, IAttributeRepository
    {
        private readonly MainDbContext _dbContext;

        public AttributeRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}