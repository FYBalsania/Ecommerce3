namespace Ecommerce3.Domain.Models;

public record CustomerAddressReference(
    int Id,
    string? Type,
    string? FullName,
    string? PhoneNumber,
    string? CompanyName,
    string AddressLine1,
    string? AddressLine2,
    string City,
    string StateOrProvince,
    string PostalCode,
    string? Landmark);