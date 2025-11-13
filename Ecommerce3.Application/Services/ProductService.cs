using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Product;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.Product;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

internal sealed class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IProductQueryRepository _queryRepository;
    private readonly IProductPageRepository _pageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IProductRepository repository, IProductQueryRepository queryRepository,
        IProductPageRepository pageRepository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _queryRepository = queryRepository;
        _pageRepository = pageRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<PagedResult<ProductListItemDTO>> GetListItemsAsync(ProductFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await _queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);
    
    public async Task AddAsync(AddProductCommand command, CancellationToken cancellationToken)
    {
    }

    public async Task EditAsync(EditProductCommand command, CancellationToken cancellationToken)
    {
    }
    
    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await _queryRepository.GetMaxSortOrderAsync(cancellationToken);
}