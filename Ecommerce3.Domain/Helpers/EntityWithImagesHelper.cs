using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Helpers;

internal static class EntityWithImagesHelper
{
    /// <summary>
    /// Returns true if <paramref name="type"/> derives from EntityWithImages&lt;TImage&gt; for some TImage.
    /// </summary>
    public static bool IsEntityWithImages(Type? type)
    {
        if (type is null) return false;

        var current = type;
        while (current != null && current != typeof(object))
        {
            if (current.IsGenericType &&
                current.GetGenericTypeDefinition() == typeof(EntityWithImages<>))
            {
                return true;
            }

            current = current.BaseType;
        }

        return false;
    }
}