using System;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
  public interface IUnitOfWork : IDisposable
  {
    Task<int> Save();
    bool HasChanges();
    IStoreRepository StoreRepository { get; }
    IDepartmentRepository DepartmentRepository {get;}
    IConfigurationRepository ConfigurationRepository {get;}
    IMediaFileRepository MediaFileRepository {get;}
    IProductRepository ProductRepository {get;}
    IProductDepartmentRepository ProductDepartmentRepository {get;}
    IAttributeRepository AttributeRepository {get;}
    IAttributeValueRepository AttributeValueRepository {get;}
    IBrandRepository BrandRepository {get;}
    ITagRepository TagRepository {get;}
    ICustomerAddressRepository CustomerAddressRepository {get;}
    ICountryRepository CountryRepository {get;}
    IWishlistProductRepository WishlistProductRepository {get;}
    IPostedTransactionHeaderRepository PostedTransactionHeaderRepository {get;}
    IPostedTransactionLineRepository PostedTransactionLineRepository {get;}
    IAppAccessTokenRepository AppAccessTokenRepository {get;}
    IEposTransactionHeaderRepository EposTransactionHeaderRepository {get;}
    IShippingZoneRepository ShippingZoneRepository {get;}
    INumberSeriesRepository NumberSeriesRepository {get;}
  }
}
