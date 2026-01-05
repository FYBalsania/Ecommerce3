using Ecommerce3.Contracts.DTO.StoreFront.Page;

namespace Ecommerce3.Contracts.QueryRepositories.StoreFront;

public interface IPageQueryRepository
{
    Task<PageDTO?> GetByPathAsync(string path, CancellationToken cancellationToken);
    Task<PageDTO?> GetByCategoryIdAsync(int id, CancellationToken cancellationToken);
}