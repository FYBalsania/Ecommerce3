using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.Page;
using Ecommerce3.Contracts.QueryRepositories;

namespace Ecommerce3.Application.Services;

internal sealed class PageService : IPageService
{
    private readonly IPageQueryRepository _pageQueryRepository;

    public PageService(IPageQueryRepository pageQueryRepository)
    {
        _pageQueryRepository = pageQueryRepository;
    }
    
    public async Task<(IReadOnlyList<PageListItemDTO>, int)> GetPageListItemsAsync(string? name,
        int pageNumber, int pageSize, CancellationToken cancellationToken)
        => await _pageQueryRepository.GetPageListItemsAsync(name, pageNumber, pageSize, cancellationToken);
}