using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.ProductAttribute;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs;
using Ecommerce3.Contracts.DTOs.ProductAttribute;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

internal sealed class ProductAttributeService : IProductAttributeService
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

    public async Task<PagedResult<ProductAttributeListItemDTO>> GetListItemsAsync(ProductAttributeFilter filter,
        int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await _queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task AddAsync(AddProductAttributeCommand command, CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Name} already exists.", nameof(ProductAttribute.Name));

        exists = await _queryRepository.ExistsBySlugAsync(command.Slug, null, cancellationToken);
        if (exists)
            throw new DuplicateException($"{nameof(ProductAttribute.Slug)} already exists.",
                nameof(ProductAttribute.Slug));

        var productAttribute = new ProductAttribute(command.Name, command.Slug, command.Display, command.Breadcrumb,
            command.DataType, command.SortOrder, command.CreatedBy, command.CreatedAt, command.CreatedByIp);

        await _repository.AddAsync(productAttribute, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditAsync(EditProductAttributeCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await _queryRepository.GetMaxSortOrderAsync(cancellationToken);

    public async Task<ProductAttributeDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _queryRepository.GetByIdAsync(id, cancellationToken);
    }

    #region ProductAttributeValue

    public async Task AddValueAsync(AddProductAttributeValueCommand command, CancellationToken cancellationToken)
    {
        var productAttribute = await TryGetByIdAsync(command.ProductAttributeId, ProductAttributeInclude.Values,
            true, cancellationToken);
        var productAttributeValue = new ProductAttributeValue(command.Value, command.Slug, command.Display,
            command.Breadcrumb, command.SortOrder, command.CreatedBy, DateTime.Now, command.CreatedByIp);

        productAttribute.AddValue(productAttributeValue);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditValueAsync(EditProductAttributeValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttribute = await TryGetByIdAsync(command.ProductAttributeId, ProductAttributeInclude.Values,
            true, cancellationToken);
        var updated = productAttribute.UpdateValue(command.Id, command.Value, command.Slug, command.Display,
            command.Breadcrumb, command.SortOrder, command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        if (updated) await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task DeleteValueAsync(DeleteProductAttributeValueCommand command, CancellationToken cancellationToken)
    {
        var productAttribute = await TryGetByIdAsync(command.ProductAttributeId, ProductAttributeInclude.Values,
            true, cancellationToken);
        productAttribute.DeleteValue(command.Id, command.DeletedBy, command.DeletedAt, command.DeletedByIp);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    #endregion

    #region ProductAttributeDecimalValue

    public async Task AddDecimalValueAsync(AddProductAttributeDecimalValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttribute = await TryGetByIdAsync(command.ProductAttributeId, ProductAttributeInclude.Values,
            true, cancellationToken);
        var decimalValue = new ProductAttributeDecimalValue(command.DecimalValue, command.Slug, command.Display,
            command.Breadcrumb, command.SortOrder, command.CreatedBy, command.CreatedAt, command.CreatedByIp);

        productAttribute.AddValue(decimalValue);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditDecimalValueAsync(EditProductAttributeDecimalValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttribute = await TryGetByIdAsync(command.ProductAttributeId, ProductAttributeInclude.Values,
            true, cancellationToken);
        var updated = productAttribute.UpdateValue(command.Id, command.DecimalValue, command.Slug, command.Display,
            command.Breadcrumb, command.SortOrder, command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        if (updated) await _unitOfWork.CompleteAsync(cancellationToken);
    }

    #endregion

    #region ProductAttributeDateOnlyValue

    public async Task AddDateOnlyValueAsync(AddProductAttributeDateOnlyValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttribute = await TryGetByIdAsync(command.ProductAttributeId, ProductAttributeInclude.Values,
            true, cancellationToken);
        var dateonlyValue = new ProductAttributeDateOnlyValue(command.DateOnlyValue, command.Slug, command.Display,
            command.Breadcrumb, command.SortOrder, command.CreatedBy, DateTime.Now, command.CreatedByIp);

        productAttribute.AddValue(dateonlyValue);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditDateOnlyValueAsync(EditProductAttributeDateOnlyValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttribute = await TryGetByIdAsync(command.ProductAttributeId, ProductAttributeInclude.Values,
            true, cancellationToken);
        var updated = productAttribute.UpdateValue(command.Id, command.DateOnlyValue, command.Slug, command.Display,
            command.Breadcrumb, command.SortOrder, command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        if (updated) await _unitOfWork.CompleteAsync(cancellationToken);
    }

    #endregion

    #region ProductAttributeColourValue

    public async Task AddColourValueAsync(AddProductAttributeColourValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttribute = await TryGetByIdAsync(command.ProductAttributeId, ProductAttributeInclude.Values, true,
            cancellationToken);
        var colourValue = new ProductAttributeColourValue(command.Value, command.Slug, command.Display,
            command.Breadcrumb, command.HexCode, command.ColourFamily, command.ColourFamilyHexCode, command.SortOrder,
            command.CreatedBy, DateTime.Now, command.CreatedByIp);

        productAttribute.AddValue(colourValue);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditColourValueAsync(EditProductAttributeColourValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttribute = await TryGetByIdAsync(command.ProductAttributeId, ProductAttributeInclude.Values, true,
            cancellationToken);
        var updated = productAttribute.UpdateValue(command.Id, command.Value, command.Slug, command.Display,
            command.Breadcrumb, command.HexCode, command.ColourFamily, command.ColourFamilyHexCode, command.SortOrder,
            command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        if (updated) await _unitOfWork.CompleteAsync(cancellationToken);
    }

    #endregion

    #region ProductAttributeBooleanValue

    public async Task EditBooleanValueAsync(EditProductAttributeBooleanValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttribute = await TryGetByIdAsync(command.ProductAttributeId, ProductAttributeInclude.Values, true,
            cancellationToken);
        var updated = productAttribute.UpdateValue(command.Id, command.Slug, command.Display, command.Breadcrumb,
            command.SortOrder, command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        if (updated) await _unitOfWork.CompleteAsync(cancellationToken);
    }

    #endregion

    private async Task<ProductAttribute> TryGetByIdAsync(int id, ProductAttributeInclude includes,
        bool trackChanges, CancellationToken cancellationToken)
        => await _repository.GetByIdAsync(id, includes, trackChanges, cancellationToken) ??
           throw new DomainException(DomainErrors.ProductAttributeErrors.InvalidProductAttributeId);

    public async Task<IReadOnlyList<ProductAttributeValueDTO>> GetValuesByIdAsync(int id,
        CancellationToken cancellationToken)
        => await _queryRepository.GetValuesByIdAsync(id, cancellationToken);
}