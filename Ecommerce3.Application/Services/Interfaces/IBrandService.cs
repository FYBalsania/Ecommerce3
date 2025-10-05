using Ecommerce3.Application.Commands;
using Ecommerce3.Application.Commands.Brand;
using Ecommerce3.Contracts.DTOs.Brand;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IBrandService
{
    Task<(IReadOnlyList<BrandListItemDTO>, int)> GetBrandListItemsAsync(string? name,
        int pageNumber, int pageSize, CancellationToken cancellationToken);

    Task AddAsync(AddBrandCommand command, CancellationToken cancellationToken);
    Task<BrandDTO?> GetByBrandIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateBrandCommand command, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}