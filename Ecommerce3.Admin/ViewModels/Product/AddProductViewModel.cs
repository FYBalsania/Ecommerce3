using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.Admin.Product;
using Ecommerce3.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.ViewModels.Product;

public class AddProductViewModel
{
    public string PageTitle { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "SKU is required.")]
    [StringLength(16, MinimumLength = 1, ErrorMessage = "SKU must be between 1 and 16 characters.")]
    [Display(Name = "SKU (Stock Keeping Unit)")]
    public string SKU { get; set; }

    [StringLength(16, MinimumLength = 1, ErrorMessage = "GTIN must be between 1 and 16 characters.")]
    [Display(Name = "GTIN (Global Trade Item Number)")]
    public string? GTIN { get; set; }

    [StringLength(16, MinimumLength = 1, ErrorMessage = "MPN must be between 1 and 16 characters.")]
    [Display(Name = "MPN (Manufacturer Part Number)")]
    public string? MPN { get; set; }

    [StringLength(16, MinimumLength = 1, ErrorMessage = "MFC must be between 1 and 16 characters.")]
    [Display(Name = "MFC")]
    public string? MFC { get; set; }

    [StringLength(16, MinimumLength = 1, ErrorMessage = "EAN must be between 1 and 16 characters.")]
    [Display(Name = "EAN (European Article Number)")]
    public string? EAN { get; set; }

    [StringLength(16, MinimumLength = 1, ErrorMessage = "UPC must be between 1 and 16 characters.")]
    [Display(Name = "UPC (Universal Product Code)")]
    public string? UPC { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 256 characters.")]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Slug is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Slug must be between 1 and 256 characters.")]
    [Display(Name = "Slug")]
    public string Slug { get; set; }

    [Required(ErrorMessage = "Display is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Display must be between 1 and 256 characters.")]
    [Display(Name = "Display")]
    public string Display { get; set; }

    [Required(ErrorMessage = "Breadcrumb is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Breadcrumb must be between 1 and 256 characters.")]
    [Display(Name = "Breadcrumb")]
    public string Breadcrumb { get; set; }

    [Required(ErrorMessage = "Anchor text is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Anchor text must be between 1 and 256 characters.")]
    [Display(Name = "Anchor Text")]
    public string AnchorText { get; set; }

    [StringLength(256, MinimumLength = 1, ErrorMessage = "Anchor title must be between 1 and 256 characters.")]
    [Display(Name = "Anchor Title")]
    public string? AnchorTitle { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Brand is required.")]
    [Display(Name = "Brand")]
    public int BrandId { get; set; }
    public SelectList Brands { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Category(s) is required.")]
    [Display(Name = "Category")]
    public int[] CategoryIds { get; set; }
    public SelectList Categories { get; set; }

    [Display(Name = "Product Group")] 
    public int? ProductGroupId { get; set; }
    public SelectList ProductGroups { get; set; }

    [Display(Name = "Short Description")] 
    public string? ShortDescription { get; set; }

    [Display(Name = "Full Description")] 
    public string? FullDescription { get; set; }

    [Required(ErrorMessage = "Allow reviews is required.")]
    [Display(Name = "Allow Reviews?")]
    public bool? AllowReviews { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Price is required.")]
    [Display(Name = "Price")]
    [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public decimal? Price { get; set; }

    [Display(Name = "Old Price")]
    [Range(1, int.MaxValue, ErrorMessage = "Old price must be greater than 0.")]
    public decimal? OldPrice { get; set; }

    [Display(Name = "Cost Price")]
    [Range(1, int.MaxValue, ErrorMessage = "Cost price must be greater than 0.")]
    public decimal? CostPrice { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Stock is required.")]
    [Display(Name = "Stock")]
    [Range(0, int.MaxValue, ErrorMessage = "Stock must be greater than or equal to 0.")]
    public decimal? Stock { get; set; }

    [Display(Name = "Minimum Stock")]
    [Range(1, int.MaxValue, ErrorMessage = "Minimum stock must be greater than or equal to 1.")]
    public decimal? MinStock { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Show Availability is required.")]
    [Display(Name = "Show Availability?")]
    public bool? ShowAvailability { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Free Shipping is required.")]
    [Display(Name = "Free Shipping?")]
    public bool? FreeShipping { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Additional Shipping Charge is required.")]
    [Display(Name = "Additional Shipping Charge")]
    [Range(0, int.MaxValue, ErrorMessage = "Additional shipping charge must be greater than or equal to 0.")]
    public decimal AdditionalShippingCharge { get; set; } = 0.00m;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Unit Of Measure is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Unit Of Measure is required.")]
    [Display(Name = "Unit Of Measure")]
    public int UnitOfMeasureId { get; set; }
    public SelectList UnitOfMeasures { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Quantity Per Unit Of Measure is required.")]
    [Display(Name = "Quantity Per Unit Of Measure")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity per unit of measure must be greater than or equal to 1.")]
    public decimal? QuantityPerUnitOfMeasure { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Delivery Window is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Delivery window is required.")]
    [Display(Name = "Delivery Window")]
    public int DeliveryWindowId { get; set; }
    public SelectList DeliveryWindows { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Minimum Order Quantity is required.")]
    [Display(Name = "Minimum Order Quantity")]
    [Range(1, int.MaxValue, ErrorMessage = "Minimum order quantity must be greater than or equal to 1.")]
    public decimal MinOrderQuantity { get; set; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Maximum Order Quantity must be greater than or equal to 1.")]
    [Display(Name = "Maximum Order Quantity")]
    public decimal? MaxOrderQuantity { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Is Featured is required.")]
    [Display(Name = "Is Featured?")]
    public bool? IsFeatured { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Is New is required.")]
    [Display(Name = "Is New?")]
    public bool? IsNew { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Is Best Seller is required.")]
    [Display(Name = "Is Best Seller?")]
    public bool? IsBestSeller { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Is Returnable is required.")]
    [Display(Name = "Is Returnable?")]
    public bool? IsReturnable { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Status is required.")]
    [Display(Name = "Status")]
    public ProductStatus? Status { get; set; }

    [Display(Name = "Redirect URL")]
    [Range(5, 2048, ErrorMessage = "Redirect URL must be between 5 and 2048 characters.")]
    public string? RedirectUrl { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Country of origin is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Country of origin is required.")]
    [Display(Name = "Country of Origin")]
    public int CountryOfOriginId { get; set; }
    public SelectList Countries { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Sort Order is required.")]
    [Display(Name = "Sort Order")]
    public decimal SortOrder { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "H1 is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "H1 must be between 1 and 256 characters.")]
    [Display(Name = "H1")]
    public string? H1 { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Meta Title is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Meta title must be between 1 and 256 characters.")]
    [Display(Name = "Meta Title")]
    public string MetaTitle { get; set; }

    [StringLength(2048, MinimumLength = 1, ErrorMessage = "Meta description must be between 1 and 2048 characters.")]
    [Display(Name = "Meta Description")]
    public string? MetaDescription { get; set; }

    [StringLength(1024, MinimumLength = 1, ErrorMessage = "Meta keywords must be between 1 and 1024 characters.")]
    [Display(Name = "Meta Keywords")]
    public string? MetaKeywords { get; set; }

    public IDictionary<int, int> Attributes { get; set; } = new Dictionary<int, int>();

    public AddProductCommand ToCommand(int createdBy, DateTime createdAt, IPAddress createdByIp)
    {
        return new AddProductCommand
        {
            SKU = SKU,
            GTIN = GTIN,
            MPN = MPN,
            MFC = MFC,
            EAN = EAN,
            UPC = UPC,
            Name = Name,
            Slug = Slug,
            Display = Display,
            Breadcrumb = Breadcrumb,
            AnchorText = AnchorText,
            AnchorTitle = AnchorTitle,
            BrandId = BrandId,
            CategoryIds = CategoryIds,
            ProductGroupId = ProductGroupId,
            Attributes = Attributes,
            ShortDescription = ShortDescription,
            FullDescription = FullDescription,
            AllowReviews = (bool)AllowReviews!,
            Price = (decimal)Price!,
            OldPrice = OldPrice,
            CostPrice = CostPrice,
            Stock = (decimal)Stock!,
            MinStock = MinStock,
            ShowAvailability = (bool)ShowAvailability!,
            FreeShipping = (bool)FreeShipping!,
            AdditionalShippingCharge = (decimal)AdditionalShippingCharge!,
            UnitOfMeasureId = UnitOfMeasureId,
            QuantityPerUnitOfMeasure = (decimal)QuantityPerUnitOfMeasure!,
            DeliveryWindowId = DeliveryWindowId,
            MinOrderQuantity = MinOrderQuantity,
            MaxOrderQuantity = MaxOrderQuantity,
            IsFeatured = (bool)IsFeatured!,
            IsNew = (bool)IsNew!,
            IsBestSeller = (bool)IsBestSeller!,
            IsReturnable = (bool)IsReturnable!,
            Status = (ProductStatus)Status!,
            RedirectUrl = RedirectUrl,
            CountryOfOriginId = CountryOfOriginId,
            SortOrder = SortOrder,
            H1 = H1,
            MetaTitle = MetaTitle,
            MetaDescription = MetaDescription,
            MetaKeywords = MetaKeywords,
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            CreatedByIp = createdByIp
        };
    }
}