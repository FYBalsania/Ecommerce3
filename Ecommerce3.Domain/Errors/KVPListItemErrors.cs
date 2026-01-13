using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class KVPListItemErrors
    {
        public static readonly DomainError ParentEntityRequired =
            new($"{nameof(KVPListItem)}.ParentEntity", "Parent entity is required.");
        
        public static readonly DomainError InvalidId =
            new($"{nameof(KVPListItem)}.{nameof(KVPListItem.Id)}", "Id is invalid.");
        
        public static readonly DomainError KeyRequired =
            new DomainError($"{nameof(KVPListItem)}.{nameof(KVPListItem.Key)}", "Key is required.");
        
        public static readonly DomainError DuplicateKey =
            new($"{nameof(KVPListItem)}.{nameof(KVPListItem.Key)}", "Duplicate key.");
        
        public static readonly DomainError ValueRequired =
            new DomainError($"{nameof(KVPListItem)}.{nameof(KVPListItem.Value)}", "Value is required.");
        
        public static readonly DomainError DuplicateValue =
            new($"{nameof(KVPListItem)}.{nameof(KVPListItem.Value)}", "Duplicate value.");
        
        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(KVPListItem)}.{nameof(KVPListItem.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(KVPListItem)}.{nameof(KVPListItem.UpdatedBy)}", "Updated by is invalid.");
        
        public static readonly DomainError InvalidDeletedBy =
            new($"{nameof(KVPListItem)}.{nameof(KVPListItem.DeletedBy)}", "Deleted by is invalid.");
    }

    public static class CategoryKVPListItemErrors
    {
        public static readonly DomainError InvalidCategoryId =
            new($"{nameof(CategoryKVPListItem)}.{nameof(CategoryKVPListItem.CategoryId)}", "CategoryId is invalid.");
    }
    
    public static class ProductKVPListItemErrors
    {
        public static readonly DomainError InvalidProductId =
            new($"{nameof(ProductKVPListItem)}.{nameof(ProductKVPListItem.ProductId)}", "ProductId is invalid.");
    }
}