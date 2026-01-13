using System.Net;
using Ecommerce3.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce3.Infrastructure.Entities;

public class AppUser : IdentityUser<int>, IAppUser
{
    public required string FirstName { get; init; }
    public string? LastName { get; init; }
    public string FullName { get; init; }
    public int CreatedBy { get; }
    public DateTime CreatedAt { get; }
    public IPAddress CreatedByIp { get; }
    public int? UpdatedBy { get; }
    public DateTime? UpdatedAt { get; }
    public IPAddress? UpdatedByIp { get; }
}