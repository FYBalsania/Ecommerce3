namespace Ecommerce3.Domain.DomainEvents.Category;

public record CategorySlugUpdatedDomainEvent(
    int Id,
    string OldSlug,
    string NewSlug,
    string OldPath,
    string NewPath)
    : IDomainEvent;