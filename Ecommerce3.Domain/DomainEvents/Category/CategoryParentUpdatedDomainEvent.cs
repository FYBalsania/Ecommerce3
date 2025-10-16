namespace Ecommerce3.Domain.DomainEvents.Category;

public record CategoryParentIdUpdatedDomainEvent(int? OldParentId, int? NewParentId) : IDomainEvent;