using Ecommerce3.Application.Services.StoreFront.Interfaces;
using Ecommerce3.Contracts.DTO.StoreFront.Page;
using Ecommerce3.Contracts.QueryRepositories.StoreFront;

namespace Ecommerce3.Application.Services.StoreFront;

internal sealed class PageService(IPageQueryRepository pageQueryRepository) : IPageService
{
    public async Task<PageDTO?> GetByPathAsync(string path, CancellationToken cancellationToken)
    {
        return await pageQueryRepository.GetByPathAsync(path, cancellationToken);
    }
}