namespace Ecommerce3.Domain.DomainEvents.Category;

public record CategorySlugUpdatedDomainEvent(string OldSlug, string NewSlug) : IDomainEvent;