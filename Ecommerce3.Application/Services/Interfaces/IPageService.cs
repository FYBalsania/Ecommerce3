using Ecommerce3.Contracts.DTOs.Page;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IPageService
{
    Task<(IReadOnlyList<PageListItemDTO>, int)> GetPageListItemsAsync(string? name,
        int pageNumber, int pageSize, CancellationToken cancellationToken);

    Task<PageDTO?> GetByPathAsync(string path, CancellationToken cancellationToken);
}