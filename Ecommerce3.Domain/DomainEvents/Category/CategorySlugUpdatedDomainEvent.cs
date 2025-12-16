using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Domain.DomainEvents.Category;

public record CategorySlugUpdatedDomainEvent(
    int Id,
    string OldSlug,
    string NewSlug,
    LTree OldPath,
    LTree NewPath)
    : IDomainEvent;