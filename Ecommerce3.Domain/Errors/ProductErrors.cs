using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class ProductErrors
    {
        public static readonly DomainError InvalidId =
            new($"{nameof(Product)}.{nameof(Product.Id)}", "Id is invalid.");

        public static readonly DomainError SKURequired =
            new($"{nameof(Product)}.{nameof(Product.SKU)}", "SKU is required.");

        public static readonly DomainError SKUTooLong =
            new($"{nameof(Product)}.{nameof(Product.SKU)}", $"SKU cannot exceed {Product.SKUMaxLength} characters.");

        public static readonly DomainError DuplicateSKU =
            new($"{nameof(Product)}.{nameof(Product.SKU)}", "Duplicate SKU.");

        public static readonly DomainError GTINTooLong =
            new($"{nameof(Product)}.{nameof(Product.GTIN)}", $"GTIN cannot exceed {Product.GTINMaxLength} characters.");

        public static readonly DomainError MPNTooLong =
            new($"{nameof(Product)}.{nameof(Product.MPN)}", $"MPN cannot exceed {Product.MPNMaxLength} characters.");

        public static readonly DomainError MFCTooLong =
            new($"{nameof(Product)}.{nameof(Product.MFC)}", $"MFC cannot exceed {Product.MFCMaxLength} characters.");

        public static readonly DomainError EANTooLong =
            new($"{nameof(Product)}.{nameof(Product.EAN)}", $"EAN cannot exceed {Product.EANMaxLength} characters.");

        public static readonly DomainError UPCTooLong =
            new($"{nameof(Product)}.{nameof(Product.UPC)}", $"UPC cannot exceed {Product.UPCMaxLength} characters.");

        public static readonly DomainError NameRequired =
            new($"{nameof(Product)}.{nameof(Product.Name)}", "Name is required.");

        public static readonly DomainError NameTooLong =
            new($"{nameof(Product)}.{nameof(Product.Name)}", $"Name cannot exceed {Product.NameMaxLength} characters.");

        public static readonly DomainError DuplicateName =
            new($"{nameof(Product)}.{nameof(Product.Name)}", "Duplicate name.");

        public static readonly DomainError SlugRequired =
            new($"{nameof(Product)}.{nameof(Product.Slug)}", "Slug is required.");

        public static readonly DomainError DuplicateSlug =
            new($"{nameof(Product)}.{nameof(Product.Slug)}", "Duplicate slug.");

        public static readonly DomainError SlugTooLong =
            new($"{nameof(Product)}.{nameof(Product.Slug)}", $"Slug cannot exceed {Product.SlugMaxLength} characters.");

        public static readonly DomainError DisplayRequired =
            new($"{nameof(Product)}.{nameof(Product.Display)}", "Display is required.");

        public static readonly DomainError DisplayTooLong =
            new($"{nameof(Product)}.{nameof(Product.Display)}",
                $"Display cannot exceed {Product.DisplayMaxLength} characters.");

        public static readonly DomainError BreadcrumbRequired =
            new($"{nameof(Product)}.{nameof(Product.Breadcrumb)}", "Breadcrumb is required.");

        public static readonly DomainError BreadcrumbTooLong =
            new($"{nameof(Product)}.{nameof(Product.Breadcrumb)}",
                $"Breadcrumb cannot exceed {Product.BreadcrumbMaxLength} characters.");

        public static readonly DomainError AnchorTextRequired =
            new($"{nameof(Product)}.{nameof(Product.AnchorText)}", "Anchor text is required.");

        public static readonly DomainError AnchorTextTooLong =
            new($"{nameof(Product)}.{nameof(Product.AnchorText)}",
                $"Anchor text cannot exceed {Product.AnchorTextMaxLength} characters.");

        public static readonly DomainError AnchorTitleTooLong =
            new($"{nameof(Product)}.{nameof(Product.AnchorTitle)}",
                $"Anchor title cannot exceed {Product.AnchorTitleMaxLength} characters.");

        public static readonly DomainError InvalidBrandId =
            new($"{nameof(Product)}.{nameof(Product.BrandId)}", "BrandID is invalid.");

        public static readonly DomainError CategoryIdRequired =
            new DomainError($"{nameof(Product)}.{nameof(Product.Categories)}", "Category Id is required.");

        public static readonly DomainError InvalidCategoryId =
            new($"{nameof(Product)}.{nameof(Product.Categories)}", "Category Id is invalid.");

        public static readonly DomainError DuplicateCategoryId =
            new($"{nameof(Product)}.{nameof(Product.Categories)}", "Duplicate categories are not allowed.");

        public static readonly DomainError InvalidProductGroupId =
            new($"{nameof(Product)}.{nameof(Product.ProductGroupId)}", "ProductGroupID is invalid.");

        public static readonly DomainError ShortDescriptionTooLong =
            new($"{nameof(Product)}.{nameof(Product.ShortDescription)}",
                $"Short description cannot exceed {Product.ShortDescriptionMaxLength} characters.");

        public static readonly DomainError InvalidStock =
            new($"{nameof(Product)}.{nameof(Product.Stock)}", "Stock is invalid.");

        public static readonly DomainError InvalidMinStock =
            new($"{nameof(Product)}.{nameof(Product.MinStock)}", "Min. stock is invalid.");

        public static readonly DomainError InvalidAdditionalShippingCharge =
            new($"{nameof(Product)}.{nameof(Product.AdditionalShippingCharge)}",
                "Additional shipping charge is invalid.");

        public static readonly DomainError InvalidQuantityPerUnitOfMeasure =
            new($"{nameof(Product)}.{nameof(Product.QuantityPerUnitOfMeasure)}", "Qty. per unit is invalid.");

        public static readonly DomainError InvalidDeliveryWindowId =
            new($"{nameof(Product)}.{nameof(Product.DeliveryWindowId)}", "Delivery Window Id is invalid.");

        public static readonly DomainError InvalidUnitOfMeasureId =
            new($"{nameof(Product)}.{nameof(Product.UnitOfMeasureId)}", "Unit of Measure Id is invalid.");
        
        public static readonly DomainError InvalidCountryOfOriginId =
            new($"{nameof(Product)}.{nameof(Product.CountryOfOriginId)}", "Country of Origin Id is invalid.");

        public static readonly DomainError InvalidPrice =
            new($"{nameof(Product)}.{nameof(Product.Price)}", "Price is invalid.");

        public static readonly DomainError InvalidOldPrice =
            new($"{nameof(Product)}.{nameof(Product.OldPrice)}", "Old Price is invalid.");

        public static readonly DomainError InvalidCostPrice =
            new($"{nameof(Product)}.{nameof(Product.CostPrice)}", "Cost Price is invalid.");

        public static readonly DomainError MinOrderQuantityRequired =
            new($"{nameof(Product)}.{nameof(Product.MinOrderQuantity)}", "Min. order quantity is required.");

        public static readonly DomainError InvalidMinOrderQuantity =
            new($"{nameof(Product)}.{nameof(Product.MinOrderQuantity)}", "Min. order quantity is invalid.");

        public static readonly DomainError InvalidMaxOrderQuantity =
            new($"{nameof(Product)}.{nameof(Product.MaxOrderQuantity)}", "Max. order quantity is invalid.");

        public static readonly DomainError RedirectUrlRequired =
            new($"{nameof(Product)}.{nameof(Product.RedirectUrl)}", "Redirect URL is required.");

        public static readonly DomainError RedirectUrlNotRequired =
            new($"{nameof(Product)}.{nameof(Product.RedirectUrl)}", "Redirect URL is not required.");

        public static readonly DomainError RedirectUrlTooLong =
            new($"{nameof(Product)}.{nameof(Product.RedirectUrl)}",
                $"Redirect URL cannot exceed {Product.RedirectUrlMaxLength} characters.");

        public static readonly DomainError InvalidRedirectUrl =
            new($"{nameof(Product)}.{nameof(Product.RedirectUrl)}", "Redirect URL is invalid.");

        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(Product)}.{nameof(Product.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError CreatedByIpRequired =
            new($"{nameof(Product)}.{nameof(Product.CreatedByIp)}", "Created by IP address is required.");

        public static readonly DomainError CreatedByIpTooLong =
            new($"{nameof(Product)}.{nameof(Product.CreatedByIp)}",
                $"Created by IP address cannot exceed {ICreatable.CreatedByIpMaxLength} characters.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(Product)}.{nameof(Product.UpdatedBy)}", "Updated by is invalid.");

        public static readonly DomainError UpdatedByIpRequired =
            new($"{nameof(Product)}.{nameof(Product.UpdatedByIp)}", "Updated by IP address is required.");

        public static readonly DomainError UpdatedByIpTooLong =
            new($"{nameof(Product)}.{nameof(Product.UpdatedByIp)}",
                $"Updated by IP address cannot exceed {IUpdatable.UpdatedByIpMaxLength} characters.");

        public static readonly DomainError InvalidDeletedBy =
            new($"{nameof(Product)}.{nameof(Product.DeletedBy)}", "Deleted by is invalid.");

        public static readonly DomainError DeletedByIpRequired =
            new($"{nameof(Product)}.{nameof(Product.DeletedByIp)}", "Deleted by IP address is required.");

        public static readonly DomainError DeletedByIpTooLong =
            new($"{nameof(Product)}.{nameof(Product.DeletedByIp)}",
                $"Updated by IP address cannot exceed {IDeletable.DeletedByIpMaxLength} characters.");

        public static readonly DomainError InvalidAttributesCount =
            new($"{nameof(Product)}.{nameof(Product.Attributes)}",
                "Supplied attributes count does not match with Product Group's attribute count.");

        public static readonly DomainError InvalidAttributeId =
            new($"{nameof(Product)}.{nameof(Product.Attributes)}",
                "Supplied attribute id index does not match with Product Group's attribute id index.");

        public static readonly DomainError InvalidAttributeValueId =
            new($"{nameof(Product)}.{nameof(Product.Attributes)}",
                "Supplied attribute value id index does not match with Product Group's attribute value id index.");
    }
}