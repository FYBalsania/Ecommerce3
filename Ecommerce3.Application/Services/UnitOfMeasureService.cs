using Ecommerce3.Application.Commands.UnitOfMeasure;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

internal class UnitOfMeasureService(
    IUnitOfMeasureRepository repository,
    IUnitOfMeasureQueryRepository queryRepository,
    IUnitOfWork unitOfWork)
    : IUnitOfMeasureService
{
    public async Task AddAsync(AddUnitOfMeasureCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByCodeAsync(command.Code, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.UnitOfMeasureErrors.DuplicateCode);

        exists = await queryRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.UnitOfMeasureErrors.DuplicateName);

        var uom = new UnitOfMeasure(command.Code, command.Name, command.Type, command.BaseId, command.ConversionFactor,
            command.IsActive, command.CreatedBy, command.CreatedAt, command.CreatedByIp);
        
        await repository.AddAsync(uom, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditAsync(EditUnitOfMeasureCommand command, CancellationToken cancellationToken)
    {
        var uom = await repository.GetByIdAsync(command.Id, UnitOfMeasureInclude.None, true, cancellationToken);
        if (uom is null) throw new DomainException(DomainErrors.UnitOfMeasureErrors.InvalidUnitOfMeasureId);

        var exists = await queryRepository.ExistsByCodeAsync(command.Code, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.UnitOfMeasureErrors.DuplicateCode);

        exists = await queryRepository.ExistsByNameAsync(command.Name, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.UnitOfMeasureErrors.DuplicateName);

        var update = uom.Update(command.Code, command.Name, command.Type, command.BaseId, command.ConversionFactor,
            command.IsActive, command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        if (update) await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeleteUnitOfMeasureCommand command, CancellationToken cancellationToken)
    {
        var uom = await repository.GetByIdAsync(command.Id, UnitOfMeasureInclude.None, true, cancellationToken);
        if (uom is null) throw new DomainException(DomainErrors.UnitOfMeasureErrors.InvalidUnitOfMeasureId);
        
        uom.Delete(command.DeletedBy, command.DeletedAt, command.DeletedByIp);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<IDictionary<int, string>> GetIdAndNameDictionaryAsync(CancellationToken cancellationToken)
    {
        return await queryRepository.GetIdAndNameDictionaryAsync(cancellationToken);
    }
}