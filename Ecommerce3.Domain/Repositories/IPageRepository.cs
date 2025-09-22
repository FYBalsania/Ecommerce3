using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Repositories;

public interface IPageRepository : IRepository<Page>
{
    public Task<(IEnumerable<Page> ListItems, int Count)?> GetPagesAsync(string? path, string? title,
        int pageNumber, string? canonicalUrl, int? seoScore, int pageSize, CancellationToken cancellationToken);
    public Task<bool> ExistsByPathAsync(string path, CancellationToken cancellationToken);
    public Task<Page?> GetByPathAsync(string path, CancellationToken cancellationToken);
}