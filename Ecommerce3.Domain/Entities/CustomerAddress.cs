namespace Ecommerce3.Domain.Entities;

public sealed class CustomerAddress : Entity, ICreatable, IUpdatable, IDeletable
{
    public int CustomerId { get; private set; }
    public string? Type { get; private set; }
    public string AddressLine1 { get; private set; }
    public string? AddressLine2 { get; private set; }
    public string? Landmark { get; private set; }
    public string PostalCode { get; private set; }
    public string City { get; private set; }
    public int CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }

    private CustomerAddress()
    {
    }

    public CustomerAddress(int customerId, string? type, string addressLine1, string? addressLine2, string? landmark,
        string postalCode, string city, string createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine1, nameof(addressLine1));
        ArgumentException.ThrowIfNullOrWhiteSpace(postalCode, nameof(postalCode));
        ArgumentException.ThrowIfNullOrWhiteSpace(city, nameof(city));
        ArgumentException.ThrowIfNullOrWhiteSpace(createdByIp, nameof(createdByIp));

        CustomerId = customerId;
        Type = type;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        Landmark = landmark;
        PostalCode = postalCode;
        City = city;
        CreatedBy = customerId;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }
}