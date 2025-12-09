using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public sealed class CategoryKVPListItem : KVPListItem
{
    public int CategoryId { get; private set; }
    public Category? Category { get; private set; }

    private CategoryKVPListItem() : base()
    {
    }

    public CategoryKVPListItem(KVPListItemType type, string key, string value, decimal sortOrder, int categoryId,
        int createdBy, DateTime createdAt, string createdByIp)
        : base(type, key, value, sortOrder, createdBy, createdAt, createdByIp)
    {
        ValidateCategoryId(categoryId);
        
        CategoryId = categoryId;
    }
    

    private static void ValidateCategoryId(int categoryId)
    {
        if (categoryId <= 0) throw new DomainException(DomainErrors.CategoryKVPListItemErrors.InvalidCategoryId);
    }
}