using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.ProductGroup;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.ProductGroup;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

public sealed class ProductGroupService : IProductGroupService
{
    private readonly IProductGroupRepository _repository;
    private readonly IProductGroupQueryRepository _queryRepository;
    private readonly IProductGroupPageRepository _pageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductGroupService(IProductGroupRepository repository, IProductGroupQueryRepository queryRepository,
        IProductGroupPageRepository pageRepository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _queryRepository = queryRepository;
        _pageRepository = pageRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<PagedResult<ProductGroupListItemDTO>> GetListItemsAsync(ProductGroupFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await _queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);
    
    public async Task AddAsync(AddProductGroupCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task EditAsync(EditProductGroupCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await _queryRepository.GetMaxSortOrderAsync(cancellationToken);
}