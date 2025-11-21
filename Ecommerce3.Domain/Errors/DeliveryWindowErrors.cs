using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class DeliveryWindowErrors
    {
        public static readonly DomainError NameRequired =
            new($"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.Name)}", "Name is required.");

        public static readonly DomainError NameTooLong =
            new($"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.Name)}", "Name cannot exceed 256 characters.");
        
        public static readonly DomainError DuplicateName =
            new($"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.Name)}", "Duplicate name.");
        
        public static readonly DomainError UnitRequired =
            new($"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.Unit)}", "Unit is required.");
        
        public static readonly DomainError UnitTooLong =
            new($"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.Unit)}", "Unit cannot exceed 8 characters.");
        
        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError CreatedByIpRequired =
            new($"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.CreatedByIp)}", "Created by IP address is required.");

        public static readonly DomainError CreatedByIpTooLong =
            new($"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.CreatedByIp)}", "Created by IP address cannot exceed 128 characters.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.UpdatedBy)}", "Updated by is invalid.");

        public static readonly DomainError UpdatedByIpRequired =
            new($"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.UpdatedByIp)}", "Updated by IP address is required.");

        public static readonly DomainError UpdatedByIpTooLong =
            new($"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.UpdatedByIp)}", "Updated by IP address cannot exceed 128 characters.");
    }
}