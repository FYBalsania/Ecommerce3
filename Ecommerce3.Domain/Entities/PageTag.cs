namespace Ecommerce3.Domain.Entities;

public sealed class PageTag  : Entity, ICreatable, IUpdatable, IDeletable
{
    private readonly List<Page> _pages = [];
    public string Tag { get; set; }
    public int CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }
    public IReadOnlyCollection<Page> Pages => _pages;
}