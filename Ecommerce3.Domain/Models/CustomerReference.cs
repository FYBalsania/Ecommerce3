namespace Ecommerce3.Domain.Models;

public record CustomerReference(
    int Id,
    string FirstName,
    string? LastName,
    string? CompanyName,
    string? PhoneNumber,
    bool IsEmailVerified);