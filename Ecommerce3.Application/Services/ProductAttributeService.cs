using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.ProductAttribute;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.ProductAttribute;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Exceptions;
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
        var exists = await _queryRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Name} already exists.", nameof(ProductAttribute.Name));

        exists = await _queryRepository.ExistsBySlugAsync(command.Slug, null, cancellationToken);
        if (exists) throw new DuplicateException($"{nameof(ProductAttribute.Slug)} already exists.", nameof(ProductAttribute.Slug));

        var productAttribute = new ProductAttribute(command.Name, command.Slug, command.Display, command.Breadcrumb, command.DataType, 
            command.SortOrder, command.CreatedBy, command.CreatedByIp);
        
        await _repository.AddAsync(productAttribute, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditAsync(EditProductAttributeCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await _queryRepository.GetMaxSortOrderAsync(cancellationToken);
}