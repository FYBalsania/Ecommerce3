using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.ProductAttribute;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.ProductAttribute;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

public sealed class ProductAttributeService : IProductAttributeService
{
    private readonly IProductAttributeRepository _repository;
    private readonly IProductAttributeQueryRepository _queryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductAttributeService(IProductAttributeRepository repository,
        IProductAttributeQueryRepository queryRepository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _queryRepository = queryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<ProductAttributeListItemDTO>> GetListItemsAsync(ProductAttributeFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await _queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task AddAsync(AddProductAttributeCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task EditAsync(EditProductAttributeCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}