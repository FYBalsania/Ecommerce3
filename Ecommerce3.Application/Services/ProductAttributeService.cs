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
    private readonly IProductAttributeColourValueRepository _productAttributeColourValueRepository;
    private readonly IProductAttributeDecimalValueRepository _productAttributeDecimalValueRepository;
    private readonly IProductAttributeDateOnlyValueRepository _productAttributeDateOnlyValueRepository;
    private readonly IProductAttributeBooleanValueRepository _productAttributeBooleanValueRepository;

    public ProductAttributeService(IProductAttributeRepository repository,
        IProductAttributeQueryRepository queryRepository, IUnitOfWork unitOfWork,
        IProductAttributeColourValueRepository productAttributeColourValueRepository,
        IProductAttributeDecimalValueRepository productAttributeDecimalValueRepository,
        IProductAttributeDateOnlyValueRepository productAttributeDateOnlyValueRepository,
        IProductAttributeBooleanValueRepository productAttributeBooleanValueRepository)
    {
        _repository = repository;
        _queryRepository = queryRepository;
        _unitOfWork = unitOfWork;
        _productAttributeColourValueRepository = productAttributeColourValueRepository;
        _productAttributeDecimalValueRepository = productAttributeDecimalValueRepository;
        _productAttributeDateOnlyValueRepository = productAttributeDateOnlyValueRepository;
        _productAttributeBooleanValueRepository = productAttributeBooleanValueRepository;
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

    public async Task AddProductAttributeColourValueAsync(AddProductAttributeColourValueCommand command,
        CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByValueNameAsync(command.Value, null, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Value} already exists.", nameof(ProductAttribute.Name));

        exists = await _queryRepository.ExistsByValueSlugAsync(command.Slug, null, cancellationToken);
        if (exists)
            throw new DuplicateException($"{nameof(ProductAttribute.Slug)} already exists.",
                nameof(ProductAttribute.Slug));

        var productAttribute = await _repository.GetByIdAsync(command.ProductAttributeId,
            ProductAttributeInclude.Values, true, cancellationToken);
        if (productAttribute is null)
            throw new ArgumentNullException(nameof(command.ProductAttributeId), "Product attribute not found.");

        var productAttributeValue = new ProductAttributeColourValue(command.Value, command.Slug, command.Display,
            command.Breadcrumb,
            command.HexCode, command.ColourFamily, command.ColourFamilyHexCode, command.SortOrder, command.CreatedBy,
            DateTime.Now, command.CreatedByIp);
        productAttribute.AddValue(productAttributeValue);

        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task AddProductAttributeDecimalValueAsync(AddProductAttributeDecimalValueCommand command,
        CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByValueNameAsync(command.Value, null, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Value} already exists.", nameof(ProductAttribute.Name));

        exists = await _queryRepository.ExistsByValueSlugAsync(command.Slug, null, cancellationToken);
        if (exists)
            throw new DuplicateException($"{nameof(ProductAttribute.Slug)} already exists.",
                nameof(ProductAttribute.Slug));

        var productAttribute = await _repository.GetByIdAsync(command.ProductAttributeId, ProductAttributeInclude.None,
            true, cancellationToken);
        if (productAttribute is null)
            throw new ArgumentNullException(nameof(command.ProductAttributeId), "Product attribute not found.");

        var productAttributeValue = new ProductAttributeDecimalValue(command.Value, command.Slug, command.Display,
            command.Breadcrumb,
            command.DecimalValue, command.SortOrder, command.CreatedBy, DateTime.Now, command.CreatedByIp);
        productAttribute.AddValue(productAttributeValue);

        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task AddProductAttributeDateOnlyValueAsync(AddProductAttributeDateOnlyValueCommand command,
        CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByValueNameAsync(command.Value, null, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Value} already exists.", nameof(ProductAttribute.Name));

        exists = await _queryRepository.ExistsByValueSlugAsync(command.Slug, null, cancellationToken);
        if (exists)
            throw new DuplicateException($"{nameof(ProductAttribute.Slug)} already exists.",
                nameof(ProductAttribute.Slug));

        var productAttribute = await _repository.GetByIdAsync(command.ProductAttributeId, ProductAttributeInclude.None,
            true, cancellationToken);
        if (productAttribute is null)
            throw new ArgumentNullException(nameof(command.ProductAttributeId), "Product attribute not found.");

        var productAttributeValue = new ProductAttributeDateOnlyValue(command.Value, command.Slug, command.Display,
            command.Breadcrumb,
            command.DateOnlyValue, command.SortOrder, command.CreatedBy, DateTime.Now, command.CreatedByIp);
        productAttribute.AddValue(productAttributeValue);

        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task AddProductAttributeBooleanValueAsync(AddProductAttributeBooleanValueCommand command,
        CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByValueNameAsync(command.Value, null, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Value} already exists.", nameof(ProductAttribute.Name));

        exists = await _queryRepository.ExistsByValueSlugAsync(command.Slug, null, cancellationToken);
        if (exists)
            throw new DuplicateException($"{nameof(ProductAttribute.Slug)} already exists.",
                nameof(ProductAttribute.Slug));

        var productAttribute = await _repository.GetByIdAsync(command.ProductAttributeId, ProductAttributeInclude.None,
            true, cancellationToken);
        if (productAttribute is null)
            throw new ArgumentNullException(nameof(command.ProductAttributeId), "Product attribute not found.");

        var productAttributeValue = new ProductAttributeBooleanValue(command.Value, command.Slug, command.Display,
            command.Breadcrumb,
            command.BooleanValue, command.SortOrder, command.CreatedBy, DateTime.Now, command.CreatedByIp);
        productAttribute.AddValue(productAttributeValue);

        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task AddValueAsync(AddProductAttributeValueCommand command, CancellationToken cancellationToken)
    {
        var productAttribute = await _repository.GetByIdAsync(command.ProductAttributeId,
            ProductAttributeInclude.Values, true, cancellationToken);
        if (productAttribute is null)
            throw new DomainException(DomainErrors.ProductAttributeErrors.InvalidProductAttributeId);

        var productAttributeValue = new ProductAttributeValue(command.Value, command.Slug, command.Display,
            command.Breadcrumb, command.SortOrder, command.CreatedBy, DateTime.Now, command.CreatedByIp);
        productAttribute.AddValue(productAttributeValue);

        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditProductAttributeColourValueAsync(EditProductAttributeColourValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttributeValue =
            await _productAttributeColourValueRepository.GetColourValueByIdAsync(command.Id, true, cancellationToken);
        if (productAttributeValue is null)
            throw new ArgumentNullException(nameof(command.Id), "Product attribute value not found.");

        // productAttributeValue.Update(command.Value, command.Slug, command.Display, command.Breadcrumb,
        //     command.SortOrder, command.HexCode, command.ColourFamily, command.ColourFamilyHexCode,
        //     command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        _productAttributeColourValueRepository.Update(productAttributeValue);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditProductAttributeDecimalValueAsync(EditProductAttributeDecimalValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttributeValue =
            await _productAttributeDecimalValueRepository.GetDecimalValueByIdAsync(command.Id, true, cancellationToken);
        if (productAttributeValue is null)
            throw new ArgumentNullException(nameof(command.Id), "Product attribute value not found.");

        // productAttributeValue.Update(command.Value, command.Slug, command.Display, command.Breadcrumb,
        //     command.SortOrder, command.DecimalValue, command.UpdatedBy, command.UpdatedByIp);

        _productAttributeDecimalValueRepository.Update(productAttributeValue);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditProductAttributeDateOnlyValueAsync(EditProductAttributeDateOnlyValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttributeValue =
            await _productAttributeDateOnlyValueRepository.GetDateOnlyValueByIdAsync(command.Id, true,
                cancellationToken);
        if (productAttributeValue is null)
            throw new ArgumentNullException(nameof(command.Id), "Product attribute value not found.");

        // productAttributeValue.Update(command.Value, command.Slug, command.Display, command.Breadcrumb,
        //     command.SortOrder, command.DateOnlyValue, command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        _productAttributeDateOnlyValueRepository.Update(productAttributeValue);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditProductAttributeBooleanValueAsync(EditProductAttributeBooleanValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttributeValue =
            await _productAttributeBooleanValueRepository.GetBooleanValueByIdAsync(command.Id, true, cancellationToken);
        if (productAttributeValue is null)
            throw new ArgumentNullException(nameof(command.Id), "Product attribute value not found.");

        // productAttributeValue.Update(command.Value, command.Slug, command.Display, command.Breadcrumb,
        //     command.SortOrder, command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        _productAttributeBooleanValueRepository.Update(productAttributeValue);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditProductAttributeValueAsync(EditProductAttributeValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttribute = await _repository.GetByIdAsync(command.ProductAttributeId,
            ProductAttributeInclude.Values, true, cancellationToken);
        if (productAttribute is null)
            throw new DomainException(DomainErrors.ProductAttributeErrors.InvalidProductAttributeId);

        var updated = productAttribute.UpdateValue(command.Id, command.Value, command.Slug, command.Display,
            command.Breadcrumb, command.SortOrder, command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        if (updated) await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProductAttributeValueDTO?>> GetValuesByProductAttributeIdAsync(int id,
        CancellationToken cancellationToken)
    {
        return await _queryRepository.GetValuesByProductAttributeIdAsync(id, cancellationToken);
    }

    public async Task<ProductAttributeValueDTO?> GetByProductAttributeValueIdAsync(int id,
        CancellationToken cancellationToken)
    {
        return await _queryRepository.GetValueByProductAttributeValueIdAsync(id, cancellationToken);
    }

    public async Task DeleteProductAttributeColourValueAsync(DeleteProductAttributeValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttributeValue =
            await _productAttributeColourValueRepository.GetColourValueByIdAsync(command.Id, true, cancellationToken);
        if (productAttributeValue is null)
            throw new ArgumentNullException(nameof(command.Id), "Product attribute value not found.");

        _productAttributeColourValueRepository.Remove(productAttributeValue);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task DeleteProductAttributeDecimalValueAsync(DeleteProductAttributeValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttributeValue =
            await _productAttributeDecimalValueRepository.GetDecimalValueByIdAsync(command.Id, true, cancellationToken);
        if (productAttributeValue is null)
            throw new ArgumentNullException(nameof(command.Id), "Product attribute value not found.");

        _productAttributeDecimalValueRepository.Remove(productAttributeValue);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task DeleteProductAttributeDateOnlyValueAsync(DeleteProductAttributeValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttributeValue =
            await _productAttributeDateOnlyValueRepository.GetDateOnlyValueByIdAsync(command.Id, true,
                cancellationToken);
        if (productAttributeValue is null)
            throw new ArgumentNullException(nameof(command.Id), "Product attribute value not found.");

        _productAttributeDateOnlyValueRepository.Remove(productAttributeValue);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task DeleteProductAttributeValueAsync(DeleteProductAttributeValueCommand command,
        CancellationToken cancellationToken)
    {
        var productAttribute = await _repository.GetByIdAsync(command.ProductAttributeId,
            ProductAttributeInclude.Values, true, cancellationToken);
        if (productAttribute is null)
            throw new DomainException(DomainErrors.ProductAttributeErrors.InvalidProductAttributeId);

        productAttribute.DeleteValue(command.Id, command.DeletedBy, command.DeletedAt, command.DeletedByIp);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}