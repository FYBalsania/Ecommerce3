namespace Ecommerce3.Domain.DomainEvents.Common;

public record SlugUpdatedDomainEvent(string OldSlug, string NewSlug) : IDomainEvent;