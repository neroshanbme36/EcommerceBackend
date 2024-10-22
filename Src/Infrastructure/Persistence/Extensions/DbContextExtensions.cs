namespace Persistence.Extensions
{
  public static class DbContextExtensions
  {
    public static bool HasChanges(this MainDbContext context)
    {
      context.ChangeTracker.DetectChanges();
      return context.ChangeTracker.HasChanges();
    }

    public static bool HasChanges(this CrmDbContext context)
    {
      context.ChangeTracker.DetectChanges();
      return context.ChangeTracker.HasChanges();
    }
  }
}