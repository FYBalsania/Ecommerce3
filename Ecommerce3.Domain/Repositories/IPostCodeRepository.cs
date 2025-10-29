using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IPostCodeRepository : IRepository<PostCode>
{
    public Task<PostCode?> GetByIdAsync(int id, PostCodeInclude includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<PostCode?> GetByCodeAsync(string code, PostCodeInclude includes, bool trackChanges,
        CancellationToken cancellationToken);
}