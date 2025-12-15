using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Domain.Repositories;

public interface ICategoryRepository : IEntityWithImagesRepository<Category, CategoryImage> //IRepository<Category>
{
    Task<Category?> GetByIdAsync(int id, CategoryInclude includes, bool trackChanges,
        CancellationToken cancellationToken);

    Task<Category?> GetBySlugAsync(string slug, CategoryInclude includes, bool trackChanges,
        CancellationToken cancellationToken);

    Task<Category?> GetByNameAsync(string name, CategoryInclude includes, bool trackChanges,
        CancellationToken cancellationToken);

    Task<List<Category>> GetDescendantsAsync(int parentId, CategoryInclude include, bool trackChanges,
        CancellationToken cancellationToken);

    Task UpdateDescendantPathsAsync(int categoryId, string oldPath, string newPath, CancellationToken cancellationToken);
}