namespace Ecommerce3.Domain.Entities;

public sealed class Customer : Entity, ICreatable, IUpdatable, IDeletable
{
    private readonly List<CustomerAddress> _addresses = [];
    public string FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? CompanyName { get; private set; }
    public string EmailAddress { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string Password { get; private set; }
    public bool IsEmailVerified { get; private set; }
    public string? PasswordResetToken { get; private set; }
    public DateTime? PasswordResetTokenExpiry { get; private set; }
    public int CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }
    public IReadOnlyList<CustomerAddress> Addresses => _addresses;

    private Customer()
    {
    }

    public Customer(string firstName, string? lastName, string? companyName, string emailAddress, string? phoneNumber,
        string password, string createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName, nameof(firstName));
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName, nameof(lastName));
        ArgumentException.ThrowIfNullOrWhiteSpace(companyName, nameof(companyName));
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress, nameof(emailAddress));
        ArgumentException.ThrowIfNullOrWhiteSpace(password, nameof(password));
        ArgumentException.ThrowIfNullOrWhiteSpace(createdByIp, nameof(createdByIp));

        FirstName = firstName;
        LastName = lastName;
        CompanyName = companyName;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Password = password;
        IsEmailVerified = false;
        CreatedBy = this.Id;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }
}