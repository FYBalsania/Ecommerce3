using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.PostCode;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.PostCode;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

internal sealed class PostCodeService(
    IPostCodeRepository repository,
    IPostCodeQueryRepository queryRepository,
    IUnitOfWork unitOfWork) : IPostCodeService
{
    public async Task<PagedResult<PostCodeListItemDTO>> GetListItemsAsync(PostCodeFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task AddAsync(AddPostCodeCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByCodeAsync(command.Code, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.PostCodeErrors.DuplicateCode);

        var postCode = new PostCode(command.Code, command.IsActive, command.CreatedBy, command.CreatedByIp);
        
        await repository.AddAsync(postCode, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<PostCodeDTO?> GetByPostCodeIdAsync(int id, CancellationToken cancellationToken)
        => await queryRepository.GetByIdAsync(id, cancellationToken);

    public async Task EditAsync(EditPostCodeCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByCodeAsync(command.Code, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.PostCodeErrors.DuplicateCode);

        var postCode = await repository.GetByIdAsync(command.Id, PostCodeInclude.None, true, cancellationToken);
        if (postCode is null) throw new DomainException(DomainErrors.PostCodeErrors.InvalidId);

        postCode.Update(command.Code, command.IsActive, command.UpdatedBy, command.UpdatedByIp);
        
        repository.Update(postCode);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}