using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Helpers;

namespace Ecommerce3.Domain.Extensions;

public static class TypeExtensions
{
    public static bool IsEntityWithImages(this Type? type) => EntityWithImagesHelper.IsEntityWithImages(type);
    public static bool IsImage(this Type? type) => type is not null && typeof(Image).IsAssignableFrom(type);
}