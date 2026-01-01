using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IPageRepository<T> : IEntityWithImagesRepository<T, PageImage> where T : Page
{
    public Task<(IEnumerable<T> ListItems, int Count)?> GetPagesAsync(string? path, string? title,
        string? canonicalUrl, int? seoScore, PageInclude includes, bool trackChanges, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    
    public Task<T?> GetByPathAsync(string path, PageInclude include, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<T?> GetByIdAsync(int id, PageInclude include, bool trackChanges, CancellationToken cancellationToken);
}