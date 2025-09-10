namespace Ecommerce3.Domain.Entities;

public sealed class CustomerAddress : Entity
{
    public int CustomerId { get; private set; }
    public string AddressLine1 { get; private set; }
    public string? AddressLine2 { get; private set; }
    public string? Landmark { get; private set; }
    public string PostalCode { get; private set; }
    public string City { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public string? CreatedByIp { get; private set; }
    public DateTime? UpdatedOn { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public DateTime? DeletedOn { get; private set; }
    public string? DeletedByIp { get; private set; }

    private CustomerAddress()
    {
    }

    public CustomerAddress(int customerId, string addressLine1, string? addressLine2, string? landmark,
        string postalCode, string city, string createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine1, nameof(addressLine1));
        ArgumentException.ThrowIfNullOrWhiteSpace(postalCode, nameof(postalCode));
        ArgumentException.ThrowIfNullOrWhiteSpace(city, nameof(city));
        ArgumentException.ThrowIfNullOrWhiteSpace(createdByIp, nameof(createdByIp));
        
        CustomerId = customerId;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        Landmark = landmark;
        PostalCode = postalCode;
        City = city;
        CreatedByIp = createdByIp;
        CreatedOn = DateTime.Now;
    }
}