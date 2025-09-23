using Ecommerce3.Application.Commands;
using Ecommerce3.Application.DTOs;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IBrandService
{
    Task<(IEnumerable<BrandListItemDTO> ListItems, int Count)> GetBrandListItemsAsync(string? name, int pageNumber,
        int pageSize, CancellationToken cancellationToken);
    Task AddBrandAsync(AddBrandCommand command, CancellationToken cancellationToken);
    Task<BrandDTO?> GetBrandAsync(int id, CancellationToken cancellationToken);
    Task UpdateBrandAsync(UpdateBrandCommand command, CancellationToken cancellationToken);
    Task DeleteBrandAsync(int id, CancellationToken cancellationToken);
}