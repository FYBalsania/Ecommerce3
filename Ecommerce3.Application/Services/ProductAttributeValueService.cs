using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs;
using Ecommerce3.Contracts.QueryRepositories;

namespace Ecommerce3.Application.Services;

public sealed class ProductAttributeValueService : IProductAttributeValueService
{
    private readonly IProductAttributeValueQueryRepository _productAttributeValueQueryRepository;

    public ProductAttributeValueService(IProductAttributeValueQueryRepository productAttributeValueQueryRepository)
    {
        _productAttributeValueQueryRepository = productAttributeValueQueryRepository;
    }
    
    public async Task<ProductAttributeValueDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _productAttributeValueQueryRepository.GetByIdAsync(id, cancellationToken);
    }
}