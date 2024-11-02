using Application.Specifications.Common;
using Domain.Entities;

namespace Application.Specifications
{
    public class PostedTransactionHeaderSpecification : BaseSpecification<PostedTransactionHeader>
    {
        public PostedTransactionHeaderSpecification(IReadOnlyList<long> ids, bool incLines, string? deviceId,
        bool asNoTracking, int? pageSize, int? pageNumber, string? sort, string? ecommCustomerId)
            : base(x => (ids.Count == 0 || ids.Contains(x.Id))
            && (string.IsNullOrWhiteSpace(deviceId) || x.DeviceId.Contains(deviceId))
            && (string.IsNullOrWhiteSpace(ecommCustomerId) || x.EcommCustomerId == ecommCustomerId))
        {
            ApplyAsNoTracking(asNoTracking);
            if (incLines) AddInclude(x => x.PostedTransactionLines);
            ApplyPaging(pageSize * (pageNumber - 1), pageSize);

            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort)
                {
                    case "idAsc":
                        AddOrderBy(x => x.Id);
                        break;
                    case "idDesc":
                        AddOrderByDescending(x => x.Id);
                        break;
                    case "createdOnAsc":
                        AddOrderBy(x => x.CreatedOn);
                        break;
                    case "createdOnDesc":
                        AddOrderByDescending(x => x.CreatedOn);
                        break;
                    default:
                        AddOrderBy(x => x.Id);
                        break;
                }
            }
        }
    }
}