using Ecommerce3.Contracts.DTOs;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class ProductAttributeValueQueryRepository : IProductAttributeValueQueryRepository
{
    private readonly AppDbContext _dbContext;

    public ProductAttributeValueQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductAttributeValueDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.ProductAttributeValues
            .Where(x => x.Id == id)
            .Include(x => x.CreatedByUser)
            .Select(x => MapToValueDTO(x))
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    internal static ProductAttributeValueDTO MapToValueDTO(ProductAttributeValue value)
    {
        return value switch
        {
            ProductAttributeBooleanValue bv => new ProductAttributeBooleanValueDTO(
                bv.Id, bv.Value, bv.Slug, bv.Display, bv.Breadcrumb, bv.SortOrder,
                bv.CreatedByUser!.FullName, bv.CreatedAt, bv.BooleanValue),

            ProductAttributeColourValue cv => new ProductAttributeColourValueDTO(
                cv.Id, cv.Value, cv.Slug, cv.Display, cv.Breadcrumb, cv.SortOrder,
                cv.CreatedByUser!.FullName, cv.CreatedAt, cv.HexCode,
                cv.ColourFamily, cv.ColourFamilyHexCode),

            ProductAttributeDecimalValue dv => new ProductAttributeDecimalValueDTO(
                dv.Id, dv.Value, dv.Slug, dv.Display, dv.Breadcrumb, dv.SortOrder,
                dv.CreatedByUser!.FullName, dv.CreatedAt, dv.DecimalValue),

            ProductAttributeDateOnlyValue dv => new ProductAttributeDateOnlyValueDTO(
                dv.Id, dv.Value, dv.Slug, dv.Display, dv.Breadcrumb, dv.SortOrder,
                dv.CreatedByUser!.FullName, dv.CreatedAt, dv.DateOnlyValue),

            not null => new ProductAttributeValueDTO(value.Id, value.Value, value.Slug, value.Display, value.Breadcrumb,
                value.SortOrder, value.CreatedByUser!.FullName, value.CreatedAt),

            _ => throw new NotSupportedException(
                $"Product attribute value type '{value!.GetType().Name}' is not supported.")
        };
    }
}