namespace Ecommerce3.Domain.Entities;

public sealed class ProductQnA : Entity, ICreatable, IUpdatable
{
    public int ProductId { get; private set; }
    public string Question { get; private set; }
    public int SortOrder { get; private set; }
    public string Answer { get; private set; }
    public int AnsweredBy { get; private set; }
    public DateTime AnsweredOn { get; private set; }
    public string AnswererIp { get; private set; }
    public int AnswerUpdator { get; private set; }
    public DateTime? AnswerUpdatedOn { get; private set; }
    public string? AnswerUpdatorIp { get; private set; }
    public int Approver { get; private set; }
    public DateTime ApprovedOn { get; private set; }
    public string ApproverIp { get; private set; }
    public int CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }

    private ProductQnA()
    {
    }

    public ProductQnA(string question, int createdBy, string createdByIp, int sortOrder)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(question, nameof(question));

        Question = question;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
        SortOrder = sortOrder;
    }
}