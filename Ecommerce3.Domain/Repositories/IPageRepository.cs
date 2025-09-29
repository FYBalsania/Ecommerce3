using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Repositories;

public interface IPageRepository : IRepository<Page>
{
    public Task<(IEnumerable<Page> ListItems, int Count)?> GetPagesAsync(string? path, string? title,
        string? canonicalUrl, int? seoScore, PageInclude[] includes, bool trackChanges, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    public Task<bool> ExistsByPathAsync(string path, int? excludeId, CancellationToken cancellationToken);

    public Task<Page?> GetByPathAsync(string path, PageInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<Page?> GetByIdAsync(int id, PageInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);
}