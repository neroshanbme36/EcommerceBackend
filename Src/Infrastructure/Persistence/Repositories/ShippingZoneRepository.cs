using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ShippingZoneRepository : GenericRepository<ShippingZone>, IShippingZoneRepository
    {
        private readonly MainDbContext _dbContext;

        public ShippingZoneRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShippingZone?> GetShippingZone(string countryId, string postcode)
        {
            return await _dbContext.ShippingZones
                .Include(z => z.ShippingZonePostcodes)
                .FirstOrDefaultAsync(z => z.CountryId == countryId && z.ShippingZonePostcodes.Any(p =>
                    p.Postcode == postcode ||
                    (!string.IsNullOrEmpty(p.PostcodePattern) && EF.Functions.Like(postcode, p.PostcodePattern.Replace("*", "%"))) ||
                    (p.PostcodeRangeStart != null && p.PostcodeRangeEnd != null &&
                     string.Compare(postcode, p.PostcodeRangeStart) >= 0 &&
                     string.Compare(postcode, p.PostcodeRangeEnd) <= 0)));
        }
    }
}