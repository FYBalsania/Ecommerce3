using Ecommerce3.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce3.Infrastructure.Entities;

public class AppUser : IdentityUser<int>, IUser
{
    public string FirstName { get; }
    public string LastName { get; }
    public int CreatedBy { get; }
    public DateTime CreatedAt { get; }
    public string CreatedByIp { get; }
    public int? UpdatedBy { get; }
    public DateTime? UpdatedAt { get; }
    public string? UpdatedByIp { get; }
}