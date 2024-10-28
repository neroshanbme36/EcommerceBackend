using Application.Contracts.Persistence;
using Domain.Entities;

namespace Persistence.Repositories
{
    public class AttributeValueRepository : GenericRepository<AttributeValue>, IAttributeValueRepository
    {
        private readonly MainDbContext _dbContext;

        public AttributeValueRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}