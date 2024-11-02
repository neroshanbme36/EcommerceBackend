using Application.Contracts.Persistence;
using Domain.Entities;

namespace Persistence.Repositories
{
  public class PostedTransactionLineRepository : GenericRepository<PostedTransactionLine>, IPostedTransactionLineRepository
  {
    private readonly MainDbContext _dbContext;
    public PostedTransactionLineRepository(MainDbContext dbContext) : base(dbContext)
    {
      _dbContext = dbContext;
    }
  }
}