namespace Ecommerce3.Domain.Entities;

public sealed class Customer : Entity
{
    private readonly List<CustomerAddress> _addresses = [];

    public string FullName { get; private set; }
    public string EmailAddress { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string Password { get; private set; }
    public bool IsEmailVerified { get; private set; }
    public string? PasswordResetToken { get; private set; }
    public DateTime? PasswordResetTokenExpiry { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public string? CreatedByIp { get; private set; }
    public DateTime? UpdatedOn { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public DateTime? DeletedOn { get; private set; }
    public string? DeletedByIp { get; private set; }
    public IReadOnlyList<CustomerAddress> Addresses => _addresses;

    private Customer()
    {
    }

    public Customer(string fullName, string emailAddress, string? phoneNumber, string password, string createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fullName, nameof(fullName));
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress, nameof(emailAddress));
        ArgumentException.ThrowIfNullOrWhiteSpace(password, nameof(password));
        ArgumentException.ThrowIfNullOrWhiteSpace(createdByIp, nameof(createdByIp));
        
        FullName = fullName;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Password = password;
        IsEmailVerified = false;
        CreatedOn = DateTime.Now;
        CreatedByIp = createdByIp;
    }
}