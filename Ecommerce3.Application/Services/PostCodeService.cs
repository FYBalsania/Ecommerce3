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

internal sealed class PostCodeService : IPostCodeService
{
    private readonly IPostCodeRepository _repository;
    private readonly IPostCodeQueryRepository _queryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PostCodeService(IPostCodeRepository repository, IPostCodeQueryRepository queryRepository, IUnitOfWork unitOfWork)
    {
        _queryRepository = queryRepository;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<PostCodeListItemDTO>> GetListItemsAsync(PostCodeFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await _queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task AddAsync(AddPostCodeCommand command, CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByCodeAsync(command.Code, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.PostCodeErrors.DuplicateCode);

        var postCode = new PostCode(command.Code, command.IsActive, command.CreatedBy, command.CreatedByIp);
        
        await _repository.AddAsync(postCode, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<PostCodeDTO?> GetByPostCodeIdAsync(int id, CancellationToken cancellationToken)
    {
        var postCode = await _queryRepository.GetByIdAsync(id, cancellationToken);

        return new PostCodeDTO
        {
            Id = postCode.Id,
            Code = postCode.Code,
            IsActive = postCode.IsActive,
        };
    }

    public async Task EditAsync(EditPostCodeCommand command, CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByCodeAsync(command.Code, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.PostCodeErrors.DuplicateCode);

        var postCode = await _repository.GetByIdAsync(command.Id, PostCodeInclude.None, true, cancellationToken);
        if (postCode is null) throw new ArgumentNullException(nameof(command.Id), "PostCode not found.");

        var postCodeUpdated = postCode.Update(command.Code, command.IsActive, command.UpdatedBy, command.UpdatedByIp);

        if (postCodeUpdated)
        {
            _repository.Update(postCode);
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}