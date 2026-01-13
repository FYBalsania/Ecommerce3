using System.Net;

namespace Ecommerce3.Domain.Entities;

public sealed class Customer : Entity
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
    public DateTime CreatedAt { get; private set; }
    public IPAddress CreatedByIp { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public IPAddress? UpdatedByIp { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public IPAddress? DeletedByIp { get; private set; }
    public IReadOnlyList<CustomerAddress> Addresses => _addresses;
    
    private readonly List<CustomerHistory> _history = [];
    public IReadOnlyList<CustomerHistory> History => _history.AsReadOnly();

    private Customer()
    {
    }

    public Customer(string firstName, string? lastName, string? companyName, string emailAddress, string? phoneNumber,
        string password, IPAddress createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName, nameof(firstName));
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName, nameof(lastName));
        ArgumentException.ThrowIfNullOrWhiteSpace(companyName, nameof(companyName));
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress, nameof(emailAddress));
        ArgumentException.ThrowIfNullOrWhiteSpace(password, nameof(password));

        FirstName = firstName;
        LastName = lastName;
        CompanyName = companyName;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Password = password;
        IsEmailVerified = false;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }
}