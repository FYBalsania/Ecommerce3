namespace Ecommerce3.Domain.Entities;

public sealed class ProductReview : Entity, ICreatable, IUpdatable, IDeletable
{
    public int ProductId { get; private set; }
    public decimal Rating { get; private set; }
    public string? Review { get; private set; }
    public int SortOrder { get; private set; }
    public int? Approver { get; private set; }
    public DateTime? ApprovedOn { get; private set; }
    public string? ApproverIp { get; private set; }
    public int CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }

    private ProductReview()
    {
    }

    public ProductReview(decimal rating, string review, int sortOrder, int createdBy, string createdByIp)
    {
        if (rating is < 1 or > 5)
            throw new ArgumentException($"{nameof(Rating)} must be between 1 and 5.", nameof(rating));
        ArgumentException.ThrowIfNullOrWhiteSpace(review, nameof(review));
        ArgumentException.ThrowIfNullOrWhiteSpace(createdByIp, nameof(createdByIp));
        
        Rating = rating;
        Review = review;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }
}