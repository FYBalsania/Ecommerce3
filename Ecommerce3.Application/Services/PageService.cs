using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.Page;
using Ecommerce3.Contracts.QueryRepositories;

namespace Ecommerce3.Application.Services;

internal sealed class PageService(IPageQueryRepository pageQueryRepository) : IPageService
{
    public async Task<(IReadOnlyList<PageListItemDTO>, int)> GetPageListItemsAsync(string? name,
        int pageNumber, int pageSize, CancellationToken cancellationToken)
        => await pageQueryRepository.GetPageListItemsAsync(name, pageNumber, pageSize, cancellationToken);

    public async Task<PageDTO?> GetByPathAsync(string path, CancellationToken cancellationToken)
        => await pageQueryRepository.GetByPathAsync(path, cancellationToken);
}