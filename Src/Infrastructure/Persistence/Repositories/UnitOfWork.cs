using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.Extensions;
using System;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly MainDbContext _dbContext;
    private IStoreRepository? _storeRepository;
    private IDepartmentRepository? _departmentRepository;
    private IConfigurationRepository? _configurationRepository;
    private IMediaFileRepository? _mediaFileRepository;
    private IProductRepository? _productRepository;
    private IProductDepartmentRepository? _productDepartmentRepository;
    private IAttributeRepository? _attributeRepository;
    private IAttributeValueRepository? _attributeValueRepository;
    private IBrandRepository? _brandRepository;
    private ITagRepository? _tagRepository;
    private ICustomerAddressRepository? _customerAddressRepository;
    private ICountryRepository? _countryRepository;

    public UnitOfWork(MainDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool HasChanges()
    {
      return _dbContext.HasChanges();
    }

    public async Task<int> Save()
    {
      return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
      _dbContext.Dispose();
      GC.SuppressFinalize(this);
    }

    public IStoreRepository StoreRepository =>
        _storeRepository ??= new StoreRepository(_dbContext);

    public IDepartmentRepository DepartmentRepository =>
      _departmentRepository ??= new DepartmentRepository(_dbContext);

    public IConfigurationRepository ConfigurationRepository =>
      _configurationRepository ??= new ConfigurationRepository(_dbContext);

    public IMediaFileRepository MediaFileRepository =>
      _mediaFileRepository ??= new MediaFileRepository(_dbContext);

    public IProductRepository ProductRepository =>
      _productRepository ??= new ProductRepository(_dbContext);

    public IProductDepartmentRepository ProductDepartmentRepository =>
      _productDepartmentRepository ??= new ProductDepartmentRepository(_dbContext);

    public IAttributeRepository AttributeRepository =>
      _attributeRepository ??= new AttributeRepository(_dbContext);

    public IAttributeValueRepository AttributeValueRepository =>
      _attributeValueRepository ??= new AttributeValueRepository(_dbContext);

    public IBrandRepository BrandRepository =>
      _brandRepository ??= new BrandRepository(_dbContext);

    public ITagRepository TagRepository =>
      _tagRepository ??= new TagRepository(_dbContext);

    public ICustomerAddressRepository CustomerAddressRepository =>
      _customerAddressRepository ??= new CustomerAddressRepository(_dbContext);

    public ICountryRepository CountryRepository =>
      _countryRepository ??= new CountryRepository(_dbContext);
  }
}
