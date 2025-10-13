using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IPageImageRepository : IImageRepository<PageImage>
{
    Task<PageImage?> GetByPageIdAsync(int pageId, PageImageInclude include, bool trackChanges,
        CancellationToken cancellationToken);
}