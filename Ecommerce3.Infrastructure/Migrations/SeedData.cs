using System;
using System.Net;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce3.Infrastructure.Migrations;

public partial class SeedData : Migration
{
    private static readonly string[] userColumns =
    [
        "Id", "FirstName", "LastName", "FullName", "UserName", "NormalizedUserName", "Email", "NormalizedEmail",
        "EmailConfirmed",
        "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed",
        "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount"
    ];

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        //Roles
        migrationBuilder.InsertData("Role", ["Id", "Name", "NormalizedName", "ConcurrencyStamp"],
            ["1", "Super Admin", "SUPER ADMIN", "1"]);

        //User
        migrationBuilder.InsertData(
            table: "AppUser",
            columns: userColumns,
            values: new object[]
            {
                "1", // User Id
                "Super", // FirstName
                "Administrator", // LastName
                "Super Administrator", // FullName
                "sa", // UserName
                "SA", // NormalizedUserName
                "sa@ecommerce3.com", // Email
                "SA@ECOMMERCE3.com", // NormalizedEmail
                true, // EmailConfirmed
                "AQAAAAIAAYagAAAAEDXgGCfXrLuE1QJ9xhfbq3LKglhmgL7j4QGaZXSh03A6cKujqiqgPYIlEBpDdS6SHg==", // PasswordHash (e.g., for "Admin@123")
                "RANDOMSECURITYSTAMP123", // SecurityStamp
                Guid.NewGuid().ToString(), // ConcurrencyStamp
                "1234567890", //PhoneNumber
                false, // PhoneNumberConfirmed
                false, // TwoFactorEnabled
                true, // LockoutEnabled
                0, // AccessFailedCount
            });

        //User Roles
        migrationBuilder.InsertData("AppUserRole", ["UserId", "RoleId"], ["1", "1"]);

        //Home Page.
        migrationBuilder.InsertData("Page",
            [
                "Path", "Discriminator", "MetaTitle", "SitemapPriority", "SitemapFrequency", "IsIndexed", "Language",
                "IsActive", "CreatedBy", "CreatedAt", "CreatedByIp"
            ],
            ["/", "Page", "SpiceMart", 0.80, "Weekly", true, "en", true, 1, DateTime.Now, "localhost"]);

        //Unit of Measures.
        migrationBuilder.InsertData(
            "UnitOfMeasure",
            [
                "Id", "Code", "Name", "Type", "BaseId", "ConversionFactor", "IsActive", "CreatedBy", "CreatedAt",
                "CreatedByIp"
            ],
            new object[,]
            {
                { 1, "g", "Gram", nameof(UnitOfMeasureType.Weight), null, 1, true, 1, DateTime.Now, "localhost" },
                { 2, "kg", "Kilogram", nameof(UnitOfMeasureType.Weight), 1, 1000, true, 1, DateTime.Now, "localhost" },
                { 3, "pc", "Piece", nameof(UnitOfMeasureType.Count), null, 1, true, 1, DateTime.Now, "localhost" },
                { 4, "pack", "Pack of 3", nameof(UnitOfMeasureType.Count), 3, 3, true, 1, DateTime.Now, "localhost" }
            });

        //Country.
        migrationBuilder.Sql("""
                             INSERT INTO countries
                             (id, name, iso2_code, iso3_code, numeric_code, is_active, sort_order, created_by, created_at, created_by_ip)
                             VALUES
                             (1, 'United Kingdom', 'GB', 'GBR', 826, true, 1, 1, NOW(), '127.0.0.1'::inet);
                             """);

        // DeliveryWindow
        migrationBuilder.InsertData(
            "DeliveryWindow",
            [
                "Id", "Name", "Unit", "MinValue", "MaxValue", "NormalizedMinDays", "NormalizedMaxDays",
                "IsActive", "SortOrder", "CreatedBy", "CreatedAt", "CreatedByIp"
            ],
            new object[,]
            {
                { 1, "Same Day Delivery", "Hour", 0, 24, 0m, 1m, true, 1, 1, DateTime.Now, "localhost" },
                { 2, "Next Day Delivery", "Day", 1, 1, 1m, 1m, true, 2, 1, DateTime.Now, "localhost" },
                { 3, "2-3 Days Delivery", "Day", 2, 3, 2m, 3m, true, 3, 1, DateTime.Now, "localhost" },
                { 4, "4-5 Days Delivery", "Day", 4, 5, 4m, 5m, true, 4, 1, DateTime.Now, "localhost" },
                { 5, "Standard (1 Week)", "Day", 5, 7, 5m, 7m, true, 5, 1, DateTime.Now, "localhost" },
                { 6, "Express (2-3 Hours)", "Hour", 2, 3, 0.1m, 0.2m, true, 6, 1, DateTime.Now, "localhost" },
                { 7, "Scheduled (2 Weeks)", "Day", 14, 14, 14m, 14m, true, 7, 1, DateTime.Now, "localhost" },
                { 8, "Bulk Order Delivery", "Day", 7, 14, 7m, 14m, true, 8, 1, DateTime.Now, "localhost" },
                { 9, "International Standard", "Day", 10, 21, 10m, 21m, true, 9, 1, DateTime.Now, "localhost" },
                { 10, "International Express", "Day", 3, 5, 3m, 5m, true, 10, 1, DateTime.Now, "localhost" },
                { 11, "Standard (2-3 Weeks)", "Week", 2, 3, 14m, 21m, true, 11, 1, DateTime.Now, "localhost" },
                { 12, "Economy (3-4 Weeks)", "Week", 3, 4, 21m, 28m, true, 12, 1, DateTime.Now, "localhost" },
                {
                    13, "Freight / Sea Shipping (4-6 Weeks)", "Week", 4, 6, 28m, 42m, true, 13, 1, DateTime.Now,
                    "localhost"
                },
                { 14, "Extended (6+ Weeks)", "Week", 6, null, 42m, null, true, 14, 1, DateTime.Now, "localhost" }
            });

        //Brand
        migrationBuilder.InsertData(
            "Brand",
            [
                "Id", "Name", "Slug", "Display", "Breadcrumb", "AnchorText", "AnchorTitle",
                "ShortDescription", "FullDescription", "IsActive", "SortOrder",
                "CreatedBy", "CreatedAt", "CreatedByIp"
            ],
            new object[,]
            {
                {
                    1, "Spice Mart", "spice-mart", "Spice Mart", "Spice Mart", "Spice Mart", "Spice Mart",
                    "Spice Mart", "<p><strong>Spice Mart</strong></p>", true, 1,
                    1, DateTime.Now, "localhost"
                },
            });

        //Brand Page
        migrationBuilder.InsertData(
            "Page",
            [
                "Id", "Discriminator", "Path", "MetaTitle", "MetaDescription", "MetaKeywords",
                "H1", "SitemapPriority", "SitemapFrequency", "IsIndexed", "Language", "Region", "SeoScore", "BrandId",
                "IsActive",
                "CreatedBy", "CreatedAt", "CreatedByIp"
            ],
            new object[,]
            {
                {
                    2, "BrandPage", null, "Spice Mart", "Spice Mart Brand Page", "Spice Mart, Spice, Spices",
                    "Spice Mart", 0.80, "Yearly", true, "en", "UK", 85, 1, true,
                    1, DateTime.Now, "localhost"
                }
            });

        //Category
        migrationBuilder.InsertData(
            "Category",
            [
                "Id", "Name", "Slug", "Display", "Breadcrumb", "AnchorText", "AnchorTitle",
                "ShortDescription", "FullDescription", "ParentId", "GoogleCategory", "Path",
                "IsActive", "SortOrder", "CreatedBy", "CreatedAt", "CreatedByIp"
            ],
            new object[,]
            {
                {
                    1, "Organic", "organic", "Organic", "Organic", "Organic", "Organic Products", null, null, null,
                    null, "organic", true, 1, 1, DateTime.Now, "localhost"
                },
                {
                    2, "Herbs & Spices", "herbs-spices", "Herbs & Spices", "Herbs & Spices", "Herbs & Spices",
                    "Herbs & Spices", null, null, null, null, "herbs-spices", true, 2, 1, DateTime.Now, "localhost"
                },
                {
                    3, "Nuts & Seeds", "nuts-seeds", "Nuts & Seeds", "Nuts & Seeds", "Nuts & Seeds", "Nuts & Seeds",
                    null, null, null, null, "nuts-seeds", true, 3, 1, DateTime.Now, "localhost"
                },
                {
                    4, "Healthy Snacks & Treats", "healthy-snacks-treats", "Healthy Snacks & Treats",
                    "Healthy Snacks & Treats", "Healthy Snacks & Treats", "Healthy Snacks & Treats", null, null, null,
                    null, "healthy-snacks-treats", true, 4, 1, DateTime.Now, "localhost"
                },
                {
                    5, "Personal Care & Aromatherapy", "personal-care-aromatherapy", "Personal Care & Aromatherapy",
                    "Personal Care & Aromatherapy", "Personal Care & Aromatherapy", "Personal Care & Aromatherapy",
                    null, null, null, null, "personal-care-aromatherapy", true, 5, 1, DateTime.Now, "localhost"
                },
                {
                    6, "Super Foods", "super-foods", "Super Foods", "Super Foods", "Super Foods", "Super Foods", null,
                    null, null, null, "super-foods", true, 6, 1, DateTime.Now, "localhost"
                },
                {
                    7, "Whole Foods", "whole-foods", "Whole Foods", "Whole Foods", "Whole Foods", "Whole Foods", null,
                    null, null, null, "whole-foods", true, 7, 1, DateTime.Now, "localhost"
                },
                {
                    8, "Baking", "baking", "Baking", "Baking", "Baking", "Baking", null, null, null, null, "baking",
                    true, 8, 1, DateTime.Now, "localhost"
                },
                {
                    9, "Teas", "teas", "Teas", "Teas", "Teas", "Teas", null, null, null, null, "teas", true, 9, 1,
                    DateTime.Now, "localhost"
                },
                {
                    10, "Pulses, Lentils & Grains", "pulses-lentils-grains", "Pulses, Lentils & Grains",
                    "Pulses, Lentils & Grains", "Pulses, Lentils & Grains", "Pulses, Lentils & Grains", null, null,
                    null, null, "pulses-lentils-grains", true, 10, 1, DateTime.Now, "localhost"
                },
                {
                    11, "Miscellaneous", "miscellaneous", "Miscellaneous", "Miscellaneous", "Miscellaneous",
                    "Miscellaneous", null, null, null, null, "miscellaneous", true, 11, 1, DateTime.Now, "localhost"
                },
                {
                    12, "Offers", "offers", "Offers", "Offers", "Offers", "Offers", null, null, null, null, "offers",
                    true, 12, 1, DateTime.Now, "localhost"
                },
                // Organic subcategories
                {
                    13, "Organic Baking", "organic-baking", "Baking", "Organic > Baking", "Organic Baking",
                    "Organic Baking", null, null, 1, null, "organic.organic-baking", true, 1, 1, DateTime.Now,
                    "localhost"
                },
                {
                    14, "Organic Herbs & Spices", "organic-herbs-spices", "Herbs & Spices", "Organic > Herbs & Spices",
                    "Organic Herbs & Spices", "Organic Herbs & Spices", null, null, 1, null,
                    "organic.organic-herbs-spices", true, 2, 1, DateTime.Now, "localhost"
                },
                {
                    15, "Organic Super Foods", "organic-super-foods", "Super Foods", "Organic > Super Foods",
                    "Organic Super Foods", "Organic Super Foods", null, null, 1, null, "organic.organic-super-foods",
                    true, 3, 1, DateTime.Now, "localhost"
                },
                {
                    16, "Organic Nuts & Seeds", "organic-nuts-seeds", "Nuts & Seeds", "Organic > Nuts & Seeds",
                    "Organic Nuts & Seeds", "Organic Nuts & Seeds", null, null, 1, null, "organic.organic-nuts-seeds",
                    true, 4, 1, DateTime.Now, "localhost"
                },
                {
                    17, "Organic Whole Foods", "organic-whole-foods", "Whole Foods", "Organic > Whole Foods",
                    "Organic Whole Foods", "Organic Whole Foods", null, null, 1, null, "organic.organic-whole-foods",
                    true, 5, 1, DateTime.Now, "localhost"
                },
                {
                    18, "Organic Teas", "organic-teas", "Teas", "Organic > Teas", "Organic Teas", "Organic Teas", null,
                    null, 1, null, "organic.organic-teas", true, 6, 1, DateTime.Now, "localhost"
                }
            });

        //Category Page
        migrationBuilder.InsertData(
            "Page",
            [
                "Id", "Discriminator", "Path", "MetaTitle", "MetaDescription", "MetaKeywords", "MetaRobots",
                "CanonicalUrl",
                "OgTitle", "OgDescription", "OgImageUrl", "OgType", "TwitterCard", "ContentHtml", "H1", "Summary",
                "SchemaJsonLd", "BreadcrumbsJson", "HreflangMapJson", "SitemapPriority", "SitemapFrequency",
                "RedirectFromJson", "IsIndexed",
                "HeaderScripts", "FooterScripts", "Language", "Region", "SeoScore",
                "BrandId", "CategoryId", "ProductId", "ProductGroupId", "BankId",
                "IsActive", "CreatedBy", "CreatedAt", "CreatedByIp"
            ],
            new object[,]
            {
                {
                    3, "CategoryPage", "/", "Organic Products UK", "Shop organic products in the UK",
                    "organic, healthy, natural", "index,follow", "/", "Organic Products", "Buy organic online", null,
                    "website", "summary_large_image", null, "Organic", "Organic products", null, null, null, 0.8m,
                    "weekly", null, true, null, null, "en", "GB", 90, null, 1, null, null, null, true, 1, DateTime.Now,
                    "localhost"
                },
                {
                    4, "CategoryPage", "/", "Organic Baking UK", "Organic baking ingredients", "organic baking",
                    "index,follow", "/", "Organic Baking", "Organic baking supplies", null, "website", "summary", null,
                    "Organic Baking", "Organic baking", null, null, null, 0.7m, "weekly", null, true, null, null, "en",
                    "GB", 88, null, 13, null, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    5, "CategoryPage", "/", "Organic Herbs & Spices UK", "Organic herbs and spices",
                    "organic herbs, spices", "index,follow", "/", "Organic Herbs", "Organic herbs & spices", null,
                    "website", "summary", null, "Organic Herbs & Spices", "Organic spices", null, null, null, 0.7m,
                    "weekly", null, true, null, null, "en", "GB", 88, null, 14, null, null, null, true, 1, DateTime.Now,
                    "localhost"
                },
                {
                    6, "CategoryPage", "/", "Organic Super Foods UK", "Organic superfoods", "organic superfoods",
                    "index,follow", "/", "Organic Superfoods", "Organic super foods", null, "website", "summary", null,
                    "Organic Super Foods", "Organic superfoods", null, null, null, 0.7m, "weekly", null, true, null,
                    null, "en", "GB", 88, null, 15, null, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    7, "CategoryPage", "/", "Organic Nuts & Seeds UK", "Organic nuts and seeds", "organic nuts seeds",
                    "index,follow", "/", "Organic Nuts", "Organic nuts & seeds", null, "website", "summary", null,
                    "Organic Nuts & Seeds", "Organic nuts", null, null, null, 0.7m, "weekly", null, true, null, null,
                    "en", "GB", 88, null, 16, null, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    8, "CategoryPage", "/", "Organic Whole Foods UK", "Organic whole foods", "organic whole foods",
                    "index,follow", "/", "Organic Whole Foods", "Organic whole foods", null, "website", "summary", null,
                    "Organic Whole Foods", "Whole foods", null, null, null, 0.7m, "weekly", null, true, null, null,
                    "en", "GB", 88, null, 17, null, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    9, "CategoryPage", "/", "Organic Teas UK", "Organic teas", "organic teas", "index,follow", "/",
                    "Organic Teas", "Organic teas", null, "website", "summary", null, "Organic Teas", "Teas", null,
                    null, null, 0.7m, "weekly", null, true, null, null, "en", "GB", 88, null, 18, null, null, null,
                    true, 1, DateTime.Now, "localhost"
                },
                {
                    10, "CategoryPage", "/", "Herbs & Spices UK", "Buy herbs & spices", "herbs spices", "index,follow",
                    "/", "Herbs & Spices", "Spices & herbs", null, "website", "summary", null, "Herbs & Spices",
                    "Spices", null, null, null, 0.7m, "weekly", null, true, null, null, "en", "GB", 85, null, 2, null,
                    null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    11, "CategoryPage", "/", "Nuts & Seeds UK", "Buy nuts & seeds", "nuts seeds", "index,follow", "/",
                    "Nuts & Seeds", "Nuts & seeds", null, "website", "summary", null, "Nuts & Seeds", "Nuts", null,
                    null, null, 0.7m, "weekly", null, true, null, null, "en", "GB", 85, null, 3, null, null, null, true,
                    1, DateTime.Now, "localhost"
                },
                {
                    12, "CategoryPage", "/", "Healthy Snacks UK", "Healthy snacks and treats", "healthy snacks",
                    "index,follow", "/", "Healthy Snacks", "Healthy snacks", null, "website", "summary", null,
                    "Healthy Snacks & Treats", "Snacks", null, null, null, 0.6m, "weekly", null, true, null, null, "en",
                    "GB", 80, null, 4, null, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    13, "CategoryPage", "/", "Personal Care UK", "Natural personal care", "personal care",
                    "index,follow", "/", "Personal Care", "Personal care & aromatherapy", null, "website", "summary",
                    null, "Personal Care & Aromatherapy", "Care", null, null, null, 0.6m, "weekly", null, true, null,
                    null, "en", "GB", 80, null, 5, null, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    14, "CategoryPage", "/", "Super Foods UK", "Superfoods", "superfoods", "index,follow", "/",
                    "Super Foods", "Super foods", null, "website", "summary", null, "Super Foods", "Superfoods", null,
                    null, null, 0.6m, "weekly", null, true, null, null, "en", "GB", 80, null, 6, null, null, null, true,
                    1, DateTime.Now, "localhost"
                },
                {
                    15, "CategoryPage", "/", "Whole Foods UK", "Whole foods", "whole foods", "index,follow", "/",
                    "Whole Foods", "Whole foods", null, "website", "summary", null, "Whole Foods", "Foods", null, null,
                    null, 0.6m, "weekly", null, true, null, null, "en", "GB", 80, null, 7, null, null, null, true, 1,
                    DateTime.Now, "localhost"
                },
                {
                    16, "CategoryPage", "/", "Baking UK", "Baking ingredients", "baking", "index,follow", "/", "Baking",
                    "Baking supplies", null, "website", "summary", null, "Baking", "Baking", null, null, null, 0.6m,
                    "weekly", null, true, null, null, "en", "GB", 80, null, 8, null, null, null, true, 1, DateTime.Now,
                    "localhost"
                },
                {
                    17, "CategoryPage", "/", "Teas UK", "Buy teas", "tea", "index,follow", "/", "Teas", "Teas", null,
                    "website", "summary", null, "Teas", "Teas", null, null, null, 0.6m, "weekly", null, true, null,
                    null, "en", "GB", 80, null, 9, null, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    18, "CategoryPage", "/", "Pulses & Grains UK", "Pulses lentils grains", "pulses lentils grains",
                    "index,follow", "/", "Pulses & Grains", "Pulses & grains", null, "website", "summary", null,
                    "Pulses, Lentils & Grains", "Grains", null, null, null, 0.5m, "weekly", null, true, null, null,
                    "en", "GB", 75, null, 10, null, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    19, "CategoryPage", "/", "Miscellaneous UK", "Miscellaneous", "misc", "index,follow", "/",
                    "Miscellaneous", "Miscellaneous", null, "website", "summary", null, "Miscellaneous", "Misc", null,
                    null, null, 0.4m, "weekly", null, true, null, null, "en", "GB", 70, null, 11, null, null, null,
                    true, 1, DateTime.Now, "localhost"
                },
                {
                    20, "CategoryPage", "/", "Offers UK", "Discounts & offers", "offers deals", "index,follow", "/",
                    "Offers", "Deals & offers", null, "website", "summary", null, "Offers", "Deals", null, null, null,
                    0.9m, "daily", null, true, null, null, "en", "GB", 95, null, 12, null, null, null, true, 1,
                    DateTime.Now, "localhost"
                }
            });
        
        //ProductGroup
        migrationBuilder.InsertData(
            "ProductGroup",
            [
                "Id", "Name", "Slug", "Display", "Breadcrumb", "AnchorText", "AnchorTitle",
                "IsActive", "SortOrder", "CreatedBy", "CreatedAt", "CreatedByIp",
            ],
            new object[,]
            {
                {
                    1, "4 Mixed Peppercorns", "4-mixed-peppercorns", "4 Mixed Peppercorns",
                    "4 Mixed Peppercorns", "4 Mixed Peppercorns", "4 Mixed Peppercorns",
                    true, 1m, 1, DateTime.Now, "localhost",
                },
                {
                    2, "5 Mixed Peppercorns", "5-mixed-peppercorns", "5 Mixed Peppercorns",
                    "5 Mixed Peppercorns", "5 Mixed Peppercorns", "5 Mixed Peppercorns",
                    true, 2m, 1, DateTime.Now, "localhost",
                },
                {
                    3, "Annatto Seeds", "annatto-seeds", "Annatto Seeds",
                    "Annatto Seeds", "Annatto Seeds", "Annatto Seeds",
                    true, 3m, 1, DateTime.Now, "localhost",
                },
                {
                    4, "Basil Seeds", "basil-seeds", "Basil Seeds",
                    "Basil Seeds", "Basil Seeds", "Basil Seeds",
                    true, 4m, 1, DateTime.Now, "localhost",
                },
                {
                    5, "Almond Powder", "almond-powder", "Almond Powder",
                    "Almond Powder", "Almond Powder", "Almond Powder",
                    true, 5m, 1, DateTime.Now, "localhost",
                },
                {
                    6, "Edible Gum Powder", "edible-gum-powder", "Edible Gum Powder",
                    "Edible Gum Powder", "Edible Gum Powder", "Edible Gum Powder",
                    true, 6m, 1, DateTime.Now, "localhost",
                }
            }
        );

        //Page
        migrationBuilder.InsertData(
            "Page",
            [
                "Id", "Discriminator", "Path", "MetaTitle", "MetaDescription", "MetaKeywords", "MetaRobots",
                "CanonicalUrl",
                "OgTitle", "OgDescription", "OgImageUrl", "OgType", "TwitterCard", "ContentHtml", "H1", "Summary",
                "SchemaJsonLd", "BreadcrumbsJson", "HreflangMapJson", "SitemapPriority", "SitemapFrequency",
                "RedirectFromJson", "IsIndexed",
                "HeaderScripts", "FooterScripts", "Language", "Region", "SeoScore",
                "BrandId", "CategoryId", "ProductId", "ProductGroupId", "BankId",
                "IsActive", "CreatedBy", "CreatedAt", "CreatedByIp"
            ],
            new object[,]
            {
                {
                    21, "ProductGroupPage", "/p/4-mixed-peppercorns",
                    "4 Mixed Peppercorns", "Buy 4 Mixed Peppercorns in the UK", "4 mixed peppercorns, pepper",
                    "index,follow", "/p/4-mixed-peppercorns",
                    "4 Mixed Peppercorns", "Premium 4 Mixed Peppercorns", null, "product", "summary_large_image",
                    null, "4 Mixed Peppercorns", "4 Mixed Peppercorns",
                    null, null, null, 0.9m, "weekly", null, true,
                    null, null, "en", "GB", 95,
                    null, null, null, 1, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    22, "ProductGroupPage", "/p/5-mixed-peppercorns",
                    "5 Mixed Peppercorns", "Buy 5 Mixed Peppercorns in the UK", "5 mixed peppercorns, pepper",
                    "index,follow", "/p/5-mixed-peppercorns",
                    "5 Mixed Peppercorns", "Premium 5 Mixed Peppercorns", null, "product", "summary_large_image",
                    null, "5 Mixed Peppercorns", "5 Mixed Peppercorns",
                    null, null, null, 0.9m, "weekly", null, true,
                    null, null, "en", "GB", 95,
                    null, null, null, 2, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    23, "ProductGroupPage", "/p/annatto-seeds",
                    "Annatto Seeds", "Buy Annatto Seeds in the UK", "annatto seeds", "index,follow", "/p/annatto-seeds",
                    "Annatto Seeds", "Premium Annatto Seeds", null, "product", "summary_large_image",
                    null, "Annatto Seeds", "Annatto Seeds",
                    null, null, null, 0.9m, "weekly", null, true,
                    null, null, "en", "GB", 95,
                    null, null, null, 3, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    24, "ProductGroupPage", "/p/basil-seeds",
                    "Basil Seeds", "Buy Basil Seeds in the UK", "basil seeds", "index,follow", "/p/basil-seeds",
                    "Basil Seeds", "Premium Basil Seeds", null, "product", "summary_large_image",
                    null, "Basil Seeds", "Basil Seeds",
                    null, null, null, 0.9m, "weekly", null, true,
                    null, null, "en", "GB", 95,
                    null, null, null, 4, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    25, "ProductGroupPage", "/p/almond-powder",
                    "Almond Powder", "Buy Almond Powder in the UK", "almond powder", "index,follow", "/p/almond-powder",
                    "Almond Powder", "Premium Almond Powder", null, "product", "summary_large_image",
                    null, "Almond Powder", "Almond Powder",
                    null, null, null, 0.9m, "weekly", null, true,
                    null, null, "en", "GB", 95,
                    null, null, null, 5, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    26, "ProductGroupPage", "/p/edible-gum-powder",
                    "Edible Gum Powder", "Buy Edible Gum Powder in the UK", "edible gum powder", "index,follow",
                    "/p/edible-gum-powder",
                    "Edible Gum Powder", "Premium Edible Gum Powder", null, "product", "summary_large_image",
                    null, "Edible Gum Powder", "Edible Gum Powder",
                    null, null, null, 0.9m, "weekly", null, true,
                    null, null, "en", "GB", 95,
                    null, null, null, 6, null, true, 1, DateTime.Now, "localhost"
                }
            }
        );

        //Product
        string fourMixedHtml =
            "<p>Peppercorns are great to use as a table condiment, as they bring a delightfully fresh flavour to meals. Try adding to a Caesar salad dressing to lift and cut through the creamy flavour.</p><p><br></p><p>4 Colour includes: black, white, pink, green.</p><p><br></p><p>100% Pure with no fillers, colours or binders. Suitable for vegetarians &amp; vegans.</p><p>Supplied in our Zip lock stand up Aluminium doypack pouches.</p><p><br></p><p><strong>Storage</strong></p><p>Store in a cool dry place away from sunlight.</p><p><br></p><p><strong>Ingredients</strong></p><p>4 Mixed Peppercorns Whole</p><p><br></p><p><strong>Allergen Information</strong></p><p>Packed on premises that handles nuts, seeds, cereals, soya &amp; products containing gluten.</p>";
        string fiveMixedHtml =
            "<p>Peppercorns are great to use as a table condiment, as they bring a delightfully fresh flavour to meals. Try adding to a Caesar salad dressing to lift and cut through the creamy flavour.<br><br>5 Colour includes: black, white, pink, green, pimento.<br><br>100% Pure with no fillers, colours or binders. Suitable for vegetarians &amp; vegans. Supplied in our Zip lock stand up Aluminium doypack pouches.<br><br><strong>Storage</strong><br>Store in a cool dry place away from sunlight.<br><br><strong>Ingredients</strong><br>5 Mixed Peppercorns Whole<br><br><strong>Allergen Information</strong><br>Packed on premises that handles nuts, seeds, cereals, soya &amp; products containing gluten.</p>";
        string annatoHTML =
            "<p>Annatto Seeds are popular in Mexican cuisine, they can add a bit of colour and vibrancy to dishes when needed and can be used in the same way as saffron as a cheaper replacement. Annatto seeds, also known as “achiote”, are a brilliant natural food colouring used for years to colour Red Leicester cheese; their peppery but sweet taste works across many cuisines and they are full of vital vitamins and minerals. Supplied in our Zip lock stand up Aluminium doypack pouches.<br><br><strong>Storage</strong><br>Store in a cool dry place away from sunlight.<br><br><strong>Ingredients</strong><br>Annatto Seeds<br><br><strong>Allergen Information</strong><br>Packed on premises that handles nuts, seeds, cereals, soya &amp; products containing gluten.</p>\n";
        string basilHTML =
            "<p>Popular in Mediterranean cuisine, dried basil is a simple but effective ingredient for pasta, pizza, dips and soups; being closely related to lavender and thyme, when dried its flavour becomes more emphatic, making it a perfect kitchen staple. Our basil is packed on the premises to maintain freshness; add it at the end of cooking for a more distinctive taste. 100% Pure with no fillers, colours or binders. Suitable for vegetarians &amp; vegans. Supplied in our Zip lock stand up Aluminium doypack pouches.<br><br><strong>Storage</strong><br>Store in a cool dry place away from sunlight.<br><br><strong>Ingredients</strong><br>Basil Seeds<br><br><strong>Allergen Information</strong><br>Packed on premises that handles nuts, seeds, cereals, soya &amp; products containing gluten.</p>\n";
        string almondHTML =
            "<p>Almond powder produced from blanched almond kernels. Ground almonds (also known as almond flour or almond meal) are popular for baking sweets, cakes and biscuits; all our ground and flaked almonds are blanched with the brown skin removed before grinding. Supplied in our Zip lock stand up Aluminium doypack pouches.<br><br><strong>Storage</strong><br>Store in a cool dry place away from sunlight.<br><br><strong>Ingredients</strong><br>Almond Ground<br><br><strong>Allergen Information</strong><br>Packed on premises that handles nuts, seeds, cereals, soya &amp; products containing gluten.</p>\n";
        string gumHTML =
            "<p>Gum Powder is a natural gum derived from the hardened sap of acacia trees, primarily native to the Middle East and parts of Western Africa, and is widely used in the food and beverage industry as a stabilizer for long shelf life; it is commonly used in soft drink syrups to improve texture and stability. Beyond beverages, gum Arabic is a key ingredient in confections such as gumdrops, marshmallows and edible glitter, and is also popular in modern cake decorating for its versatile, edible glossy finish. 100% Pure, with no fillers, colours or binders. Suitable for vegetarians &amp; vegans. Supplied in our Zip lock stand up Aluminium doypack pouches.<br><br><strong>Storage</strong><br>Store in a cool dry place away from sunlight.<br><br><strong>Ingredients</strong><br>Edible Gum Powder<br><br><strong>Allergen Information</strong><br>Packed on premises that handles nuts, seeds, cereals, soya &amp; products containing gluten.</p>\n";
         migrationBuilder.InsertData(
             "Product",
             [
                 "Id", "SKU",
                 "Name", "Slug", "Display",
                 "Breadcrumb", "AnchorText", "AnchorTitle",
                 "BrandId", "ProductGroupId",
                 "ShortDescription", "FullDescription",
                 "AllowReviews", "AverageRating", "TotalReviews",
                 "Price", "OldPrice", "CostPrice",
                 "Stock", "MinStock", "ShowAvailability",
                 "FreeShipping", "AdditionalShippingCharge",
                 "UnitOfMeasureId", "QuantityPerUnitOfMeasure",
                 "DeliveryWindowId",
                 "MinOrderQuantity", "MaxOrderQuantity",
                 "IsFeatured", "IsNew", "IsBestSeller", "IsReturnable",
                 "Status", "RedirectUrl", "CountryOfOriginId",
                 "SortOrder", "facets",
                 "CreatedBy", "CreatedAt", "CreatedByIp",
             ],
             new object[,]
             {
                 {
                     1, "4MP50G",
                     "4 Mixed Peppercorns 50g", "4-mixed-peppercorns-50g", "4 Mixed Peppercorns 50g",
                     "4 Mixed Peppercorns 50g", "4 Mixed Peppercorns 50g", "4 Mixed Peppercorns 50g",
                     1, 1,
                     null,
                     fourMixedHtml,
                     true, 2m, 0,
                     2.49m, null, null,
                     10m, null, true,
                     true, 2.75m,
                     1, 50m,
                     3,
                     1m, null,
                     true, true, true, true,
                     "Active", null, 1,
                     1m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost",
                 },
                 {
                     2, "4MP90G",
                     "4 Mixed Peppercorns 90g", "4-mixed-peppercorns-90g", "4 Mixed Peppercorns 90g",
                     "4 Mixed Peppercorns 90g", "4 Mixed Peppercorns 90g", "4 Mixed Peppercorns 90g",
                     1, 1,
                     null,
                     fourMixedHtml,
                     true, 2m, 0,
                     4.49m, null, null,
                     8m, null, true,
                     true, 2.75m,
                     1, 90m,
                     3,
                     1m, null,
                     false, false, true, true,
                     "Active", null, 1,
                     2m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost",
                 },
                 {
                     3, "4MP200G",
                     "4 Mixed Peppercorns 200g", "4-mixed-peppercorns-200g", "4 Mixed Peppercorns 200g",
                     "4 Mixed Peppercorns 200g", "4 Mixed Peppercorns 200g", "4 Mixed Peppercorns 200g",
                     1, 1,
                     null,
                     fourMixedHtml,
                     true, 4m, 0,
                     7.99m, null, null,
                     6m, null, true,
                     true, 2.75m,
                     1, 200m,
                     3,
                     1m, null,
                     false, false, true, true,
                     "Active", null, 1,
                     3m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost",
                 },
                 {
                     4, "4MP450G",
                     "4 Mixed Peppercorns 450g", "4-mixed-peppercorns-450g", "4 Mixed Peppercorns 450g",
                     "4 Mixed Peppercorns 450g", "4 Mixed Peppercorns 450g", "4 Mixed Peppercorns 450g",
                     1, 1,
                     null,
                     fourMixedHtml,
                     true, 4.5m, 0,
                     14.99m, null, null,
                     4m, null, true,
                     true, 2.75m,
                     1, 450m,
                     3,
                     1m, null,
                     false, false, true, true,
                     "Active", null, 1,
                     4m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost",
                 },
                 {
                     5, "4MP950G",
                     "4 Mixed Peppercorns 950g", "4-mixed-peppercorns-950g", "4 Mixed Peppercorns 950g",
                     "4 Mixed Peppercorns 950g", "4 Mixed Peppercorns 950g", "4 Mixed Peppercorns 950g",
                     1, 1,
                     null,
                     fourMixedHtml,
                     true, 4.5m, 0,
                     24.99m, null, null,
                     3m, null, true,
                     true, 2.75m,
                     1, 950m,
                     3,
                     1m, null,
                     false, false, true, true,
                     "Active", null, 1,
                     5m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost",
                 },
                 {
                     6, "4MP19KG",
                     "4 Mixed Peppercorns 1.9Kg", "4-mixed-peppercorns-1-9kg", "4 Mixed Peppercorns 1.9Kg",
                     "4 Mixed Peppercorns 1.9Kg", "4 Mixed Peppercorns 1.9Kg", "4 Mixed Peppercorns 1.9Kg",
                     1, 1,
                     null,
                     fourMixedHtml,
                     true, 3.5m, 0,
                     44.99m, null, null,
                     2m, null, true,
                     true, 2.75m,
                     2, 1.9m,
                     3,
                     1m, null,
                     false, false, true, true,
                     "Active", null, 1,
                     6m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost",
                 },
                 {
                     7, "5MP50G", "5 Mixed Peppercorns 50g", "5-mixed-peppercorns-50g", "5 Mixed Peppercorns 50g",
                     "5 Mixed Peppercorns 50g", "5 Mixed Peppercorns 50g", "5 Mixed Peppercorns 50g",
                     1, 2, null,
                     fiveMixedHtml,
                     true, 4m, 0, 2.75m, null, null, 10m, null, true, true, 2.75m, 1, 50m, 3, 1m, null, true, true, true,
                     true, "Active", null, 1, 1m,
                     new[] { "category:2" }, 1, DateTime.Now, "localhost"
                 },
                 {
                     8, "5MP90G", "5 Mixed Peppercorns 90g", "5-mixed-peppercorns-90g", "5 Mixed Peppercorns 90g",
                     "5 Mixed Peppercorns 90g", "5 Mixed Peppercorns 90g", "5 Mixed Peppercorns 90g",
                     1, 2, null,
                     fiveMixedHtml,
                     true, 4m, 0, 4.99m, null, null, 8m, null, true, true, 2.75m, 1, 90m, 3, 1m, null, false, false,
                     true, true, "Active", null, 1, 2m,
                     new[] { "category:2" }, 1, DateTime.Now, "localhost"
                 },
                 {
                     9, "5MP200G", "5 Mixed Peppercorns 200g", "5-mixed-peppercorns-200g", "5 Mixed Peppercorns 200g",
                     "5 Mixed Peppercorns 200g", "5 Mixed Peppercorns 200g", "5 Mixed Peppercorns 200g",
                     1, 2, null,
                     fiveMixedHtml,
                     true, 2.5m, 0, 8.49m, null, null, 6m, null, true, true, 2.75m, 1, 200m, 3, 1m, null, false, false,
                     true, true, "Active", null, 1, 3m,
                     new[] { "category:2" }, 1, DateTime.Now, "localhost"
                 },
                 {
                     10, "5MP450G", "5 Mixed Peppercorns 450g", "5-mixed-peppercorns-450g", "5 Mixed Peppercorns 450g",
                     "5 Mixed Peppercorns 450g", "5 Mixed Peppercorns 450g", "5 Mixed Peppercorns 450g",
                     1, 2, null,
                     fiveMixedHtml,
                     true, 4.5m, 0, 15.99m, null, null, 4m, null, true, true, 2.75m, 1, 450m, 3, 1m, null, false, false,
                     true, true, "Active", null, 1, 4m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost"
                 },
                 {
                     11, "5MP950G", "5 Mixed Peppercorns 950g", "5-mixed-peppercorns-950g", "5 Mixed Peppercorns 950g",
                     "5 Mixed Peppercorns 950g", "5 Mixed Peppercorns 950g", "5 Mixed Peppercorns 950g",
                     1, 2, null,
                     fiveMixedHtml,
                     true, 2.5m, 0, 26.99m, null, null, 3m, null, true, true, 2.75m, 1, 950m, 3, 1m, null, false, false,
                     true, true, "Active", null,1, 5m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost"
                 },
                 {
                     12, "5MP19KG", "5 Mixed Peppercorns 1.9Kg", "5-mixed-peppercorns-1-9kg",
                     "5 Mixed Peppercorns 1.9Kg",
                     "5 Mixed Peppercorns 1.9Kg", "5 Mixed Peppercorns 1.9Kg", "5 Mixed Peppercorns 1.9Kg",
                     1, 2, null,
                     fiveMixedHtml,
                     true, 1.5m, 0, 46.99m, null, null, 2m, null, true, true, 2.75m, 2, 1.9m, 3, 1m, null, false, false,
                     true, true, "Active", null,1, 6m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost"
                 },
                 {
                     13, "ANN50G", "Annatto Seeds 50g", "annatto-seeds-50g", "Annatto Seeds 50g", "Annatto Seeds 50g",
                     "Annatto Seeds 50g", "Annatto Seeds 50g", 1, 3, null, annatoHTML, true, 2.5m, 0, 3.25m, null, null,
                     10m, null, true, true, 2.75m, 1, 50m, 3, 1m, null, true, true, true, true, "Active", null, 1,1m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost"
                 },
                 {
                     14, "ANN90G", "Annatto Seeds 90g", "annatto-seeds-90g", "Annatto Seeds 90g", "Annatto Seeds 90g",
                     "Annatto Seeds 90g", "Annatto Seeds 90g", 1, 3, null, annatoHTML, true, 4.5m, 0, 5.99m, null, null,
                     8m, null, true, true, 2.75m, 1, 90m, 3, 1m, null, false, false, true, true, "Active", null,1, 2m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost"
                 },
                 {
                     15, "ANN200G", "Annatto Seeds 200g", "annatto-seeds-200g", "Annatto Seeds 200g",
                     "Annatto Seeds 200g", "Annatto Seeds 200g", "Annatto Seeds 200g", 1, 3, null, annatoHTML, true,
                     3.5m,
                     0, 9.49m, null, null, 6m, null, true, true, 2.75m, 1, 200m, 3, 1m, null, false, false, true, true,
                     "Active", null,1, 3m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost"
                 },
                 {
                     16, "ANN450G", "Annatto Seeds 450g", "annatto-seeds-450g", "Annatto Seeds 450g",
                     "Annatto Seeds 450g", "Annatto Seeds 450g", "Annatto Seeds 450g", 1, 3, null, annatoHTML, true, 4m,
                     0, 16.99m, null, null, 4m, null, true, true, 2.75m, 1, 450m, 3, 1m, null, false, false, true, true,
                     "Active", null, 1,4m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost"
                 },
                 {
                     17, "ANN950G", "Annatto Seeds 950g", "annatto-seeds-950g", "Annatto Seeds 950g",
                     "Annatto Seeds 950g", "Annatto Seeds 950g", "Annatto Seeds 950g", 1, 3, null, annatoHTML, true,
                     3.5m,
                     0, 24.99m, null, null, 3m, null, true, true, 2.75m, 1, 950m, 3, 1m, null, false, false, true, true,
                     "Active", null,1, 5m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost"
                 },
                 {
                     18, "ANN19KG", "Annatto Seeds 1.9Kg", "annatto-seeds-1-9kg", "Annatto Seeds 1.9Kg",
                     "Annatto Seeds 1.9Kg", "Annatto Seeds 1.9Kg", "Annatto Seeds 1.9Kg", 1, 3, null, annatoHTML, true,
                     4.5m, 0, 28.99m, null, null, 2m, null, true, true, 2.75m, 2, 1.9m, 3, 1m, null, false, false, true,
                     true, "Active", null,1, 6m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost"
                 },
                 {
                     19, "BAS50G", "Basil Seeds 50g", "basil-seeds-50g", "Basil Seeds 50g", "Basil Seeds 50g",
                     "Basil Seeds 50g", "Basil Seeds 50g", 1, 4, null, basilHTML, true, 5m, 0, 2.49m, null, null, 10m,
                     null, true, true, 2.75m, 1, 50m, 3, 1m, null, true, true, true, true, "Active", null,1, 1m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost"
                 },
                 {
                     20, "BAS90G", "Basil Seeds 90g", "basil-seeds-90g", "Basil Seeds 90g", "Basil Seeds 90g",
                     "Basil Seeds 90g", "Basil Seeds 90g", 1, 4, null, basilHTML, true, 4m, 0, 4.49m, null, null, 8m,
                     null, true, true, 2.75m, 1, 90m, 3, 1m, null, false, false, true, true, "Active", null,1, 2m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost"
                 },
                 {
                     21, "BAS200G", "Basil Seeds 200g", "basil-seeds-200g", "Basil Seeds 200g", "Basil Seeds 200g",
                     "Basil Seeds 200g", "Basil Seeds 200g", 1, 4, null, basilHTML, true, 2m, 0, 7.99m, null, null, 6m,
                     null, true, true, 2.75m, 1, 200m, 3, 1m, null, false, false, true, true, "Active", null,1, 3m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost"
                 },
                 {
                     22, "BAS450G", "Basil Seeds 450g", "basil-seeds-450g", "Basil Seeds 450g", "Basil Seeds 450g",
                     "Basil Seeds 450g", "Basil Seeds 450g", 1, 4, null, basilHTML, true, 1m, 0, 12.99m, null, null, 4m,
                     null, true, true, 2.75m, 1, 450m, 3, 1m, null, false, false, true, true, "Active", null,1, 4m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost"
                 },
                 {
                     23, "BAS950G", "Basil Seeds 950g", "basil-seeds-950g", "Basil Seeds 950g", "Basil Seeds 950g",
                     "Basil Seeds 950g", "Basil Seeds 950g", 1, 4, null, basilHTML, true, 1.5m, 0, 17.99m, null, null,
                     3m,
                     null, true, true, 2.75m, 1, 950m, 3, 1m, null, false, false, true, true, "Active", null, 1,5m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost"
                 },
                 {
                     24, "BAS19KG", "Basil Seeds 1.9Kg", "basil-seeds-1-9kg", "Basil Seeds 1.9Kg", "Basil Seeds 1.9Kg",
                     "Basil Seeds 1.9Kg", "Basil Seeds 1.9Kg", 1, 4, null, basilHTML, true, 2.5m, 0, 20.75m, null, null,
                     2m, null, true, true, 2.75m, 2, 1.9m, 3, 1m, null, false, false, true, true, "Active", null,1, 6m,
                     new[] { "category:2" },
                     1, DateTime.Now, "localhost"
                 },
                 {
                     25, "ALM50G", "Almond Powder 50g", "almond-powder-50g", "Almond Powder 50g", "Almond Powder 50g",
                     "Almond Powder 50g", "Almond Powder 50g", 1, 5, null, almondHTML, true, 3m, 0, 2.99m, null, null,
                     10m, null, true, true, 2.75m, 1, 50m, 3, 1m, null, true, true, true, true, "Active", null,1, 1m,
                     new[] { "category:8" }, 1,
                     DateTime.Now, "localhost"
                 },
                 {
                     26, "ALM90G", "Almond Powder 90g", "almond-powder-90g", "Almond Powder 90g", "Almond Powder 90g",
                     "Almond Powder 90g", "Almond Powder 90g", 1, 5, null, almondHTML, true, 4.5m, 0, 4.99m, null, null,
                     8m, null, true, true, 2.75m, 1, 90m, 3, 1m, null, false, false, true, true, "Active", null,1, 2m,
                     new[] { "category:8" }, 1,
                     DateTime.Now, "localhost"
                 },
                 {
                     27, "ALM200G", "Almond Powder 200g", "almond-powder-200g", "Almond Powder 200g",
                     "Almond Powder 200g", "Almond Powder 200g", "Almond Powder 200g", 1, 5, null, almondHTML, true,
                     2.5m,
                     0, 8.99m, null, null, 6m, null, true, true, 2.75m, 1, 200m, 3, 1m, null, false, false, true, true,
                     "Active", null,1, 3m,
                     new[] { "category:8" }, 1,
                     DateTime.Now, "localhost"
                 },
                 {
                     28, "ALM450G", "Almond Powder 450g", "almond-powder-450g", "Almond Powder 450g",
                     "Almond Powder 450g", "Almond Powder 450g", "Almond Powder 450g", 1, 5, null, almondHTML, true,
                     1.5m,
                     0, 14.99m, null, null, 4m, null, true, true, 2.75m, 1, 450m, 3, 1m, null, false, false, true, true,
                     "Active", null,1, 4m,
                     new[] { "category:8" }, 1,
                     DateTime.Now, "localhost"
                 },
                 {
                     29, "ALM950G", "Almond Powder 950g", "almond-powder-950g", "Almond Powder 950g",
                     "Almond Powder 950g", "Almond Powder 950g", "Almond Powder 950g", 1, 5, null, almondHTML, true, 2m,
                     0, 22.99m, null, null, 3m, null, true, true, 2.75m, 1, 950m, 3, 1m, null, false, false, true, true,
                     "Active", null,1, 5m,
                     new[] { "category:8" }, 1,
                     DateTime.Now, "localhost"
                 },
                 {
                     30, "ALM19KG", "Almond Powder 1.9Kg", "almond-powder-1-9kg", "Almond Powder 1.9Kg",
                     "Almond Powder 1.9Kg", "Almond Powder 1.9Kg", "Almond Powder 1.9Kg", 1, 5, null, almondHTML, true,
                     4m, 0, 28.75m, null, null, 2m, null, true, true, 2.75m, 2, 1.9m, 3, 1m, null, false, false, true,
                     true, "Active", null,1, 6m,
                     new[] { "category:8" }, 1,
                     DateTime.Now, "localhost"
                 },
                 {
                     31, "EGP50G", "Edible Gum Powder 50g", "edible-gum-powder-50g", "Edible Gum Powder 50g",
                     "Edible Gum Powder 50g", "Edible Gum Powder 50g", "Edible Gum Powder 50g", 1, 6, null, gumHTML,
                     true, 4m, 0, 3.45m, null, null, 10m, null, true, true, 2.75m, 1, 50m, 3, 1m, null, true, true, true,
                     true, "Active", null, 1,1m,
                     new[] { "category:8" }, 1,
                     DateTime.Now, "localhost"
                 },
                 {
                     32, "EGP100G", "Edible Gum Powder 100g", "edible-gum-powder-100g", "Edible Gum Powder 100g",
                     "Edible Gum Powder 100g", "Edible Gum Powder 100g", "Edible Gum Powder 100g", 1, 6, null, gumHTML,
                     true, 4m, 0, 6.49m, null, null, 8m, null, true, true, 2.75m, 1, 100m, 3, 1m, null, false, false,
                     true, true, "Active", null,1, 2m,
                     new[] { "category:8" }, 1,
                     DateTime.Now, "localhost"
                 },
                 {
                     33, "EGP200G", "Edible Gum Powder 200g", "edible-gum-powder-200g", "Edible Gum Powder 200g",
                     "Edible Gum Powder 200g", "Edible Gum Powder 200g", "Edible Gum Powder 200g", 1, 6, null, gumHTML,
                     true, 4m, 0, 11.99m, null, null, 6m, null, true, true, 2.75m, 1, 200m, 3, 1m, null, false, false,
                     true, true, "Active", null,1, 3m,
                     new[] { "category:8" }, 1,
                     DateTime.Now, "localhost"
                 },
                 {
                     34, "EGP500G", "Edible Gum Powder 500g", "edible-gum-powder-500g", "Edible Gum Powder 500g",
                     "Edible Gum Powder 500g", "Edible Gum Powder 500g", "Edible Gum Powder 500g", 1, 6, null, gumHTML,
                     true, 5m, 0, 24.99m, null, null, 4m, null, true, true, 2.75m, 1, 500m, 3, 1m, null, false, false,
                     true, true, "Active", null,1, 4m,
                     new[] { "category:8" }, 1,
                     DateTime.Now, "localhost"
                 },
                 {
                     35, "EGP1KG", "Edible Gum Powder 1Kg", "edible-gum-powder-1kg", "Edible Gum Powder 1Kg",
                     "Edible Gum Powder 1Kg", "Edible Gum Powder 1Kg", "Edible Gum Powder 1Kg", 1, 6, null, gumHTML,
                     true, 4.5m, 0, 39.99m, null, null, 3m, null, true, true, 2.75m, 2, 1m, 3, 1m, null, false, false,
                     true, true, "Active", null,1, 5m,
                     new[] { "category:8" }, 1,
                     DateTime.Now, "localhost"
                 },
                 {
                     36, "EGP2KG", "Edible Gum Powder 2Kg", "edible-gum-powder-2kg", "Edible Gum Powder 2Kg",
                     "Edible Gum Powder 2Kg", "Edible Gum Powder 2Kg", "Edible Gum Powder 2Kg", 1, 6, null, gumHTML,
                     true, 3.5m, 0, 57.99m, null, null, 2m, null, true, true, 2.75m, 2, 2m, 3, 1m, null, false, false,
                     true, true, "Active", null,1, 6m,
                     new[] { "category:8" }, 1,
                     DateTime.Now, "localhost"
                 }
             }
        );

        migrationBuilder.InsertData(
            "Page",
            [
                "Id", "Discriminator", "Path", "MetaTitle", "MetaDescription", "MetaKeywords", "MetaRobots",
                "CanonicalUrl",
                "OgTitle", "OgDescription", "OgImageUrl", "OgType", "TwitterCard", "ContentHtml", "H1", "Summary",
                "SchemaJsonLd", "BreadcrumbsJson", "HreflangMapJson", "SitemapPriority", "SitemapFrequency",
                "RedirectFromJson", "IsIndexed",
                "HeaderScripts", "FooterScripts", "Language", "Region", "SeoScore",
                "BrandId", "CategoryId", "ProductId", "ProductGroupId", "BankId",
                "IsActive", "CreatedBy", "CreatedAt", "CreatedByIp"
            ],
            new object[,]
            {
                {
                    27, "ProductPage", "/p/4-mixed-peppercorns-50g", "4 Mixed Peppercorns 50g",
                    "Buy 4 Mixed Peppercorns 50g in the UK", "4 mixed peppercorns 50g", "index,follow",
                    "/p/4-mixed-peppercorns-50g", "4 Mixed Peppercorns 50g", "Premium 4 Mixed Peppercorns", null,
                    "product", "summary_large_image", null, "4 Mixed Peppercorns 50g", "50g pack", null, null, null,
                    0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null, 1, null, null, true, 1,
                    DateTime.Now, "localhost"
                },
                {
                    28, "ProductPage", "/p/4-mixed-peppercorns-90g", "4 Mixed Peppercorns 90g",
                    "Buy 4 Mixed Peppercorns 90g in the UK", "4 mixed peppercorns 90g", "index,follow",
                    "/p/4-mixed-peppercorns-90g", "4 Mixed Peppercorns 90g", "Premium 4 Mixed Peppercorns", null,
                    "product", "summary_large_image", null, "4 Mixed Peppercorns 90g", "90g pack", null, null, null,
                    0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null, 2, null, null, true, 1,
                    DateTime.Now, "localhost"
                },
                {
                    29, "ProductPage", "/p/4-mixed-peppercorns-200g", "4 Mixed Peppercorns 200g",
                    "Buy 4 Mixed Peppercorns 200g in the UK", "4 mixed peppercorns 200g", "index,follow",
                    "/p/4-mixed-peppercorns-200g", "4 Mixed Peppercorns 200g", "Premium 4 Mixed Peppercorns", null,
                    "product", "summary_large_image", null, "4 Mixed Peppercorns 200g", "200g pack", null, null, null,
                    0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null, 3, null, null, true, 1,
                    DateTime.Now, "localhost"
                },
                {
                    30, "ProductPage", "/p/4-mixed-peppercorns-450g", "4 Mixed Peppercorns 450g",
                    "Buy 4 Mixed Peppercorns 450g in the UK", "4 mixed peppercorns 450g", "index,follow",
                    "/p/4-mixed-peppercorns-450g", "4 Mixed Peppercorns 450g", "Premium 4 Mixed Peppercorns", null,
                    "product", "summary_large_image", null, "4 Mixed Peppercorns 450g", "450g pack", null, null, null,
                    0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null, 4, null, null, true, 1,
                    DateTime.Now, "localhost"
                },
                {
                    31, "ProductPage", "/p/4-mixed-peppercorns-950g", "4 Mixed Peppercorns 950g",
                    "Buy 4 Mixed Peppercorns 950g in the UK", "4 mixed peppercorns 950g", "index,follow",
                    "/p/4-mixed-peppercorns-950g", "4 Mixed Peppercorns 950g", "Premium 4 Mixed Peppercorns", null,
                    "product", "summary_large_image", null, "4 Mixed Peppercorns 950g", "950g pack", null, null, null,
                    0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null, 5, null, null, true, 1,
                    DateTime.Now, "localhost"
                },
                {
                    32, "ProductPage", "/p/4-mixed-peppercorns-1-9kg", "4 Mixed Peppercorns 1.9Kg",
                    "Buy 4 Mixed Peppercorns 1.9Kg in the UK", "4 mixed peppercorns 1.9kg", "index,follow",
                    "/p/4-mixed-peppercorns-1-9kg", "4 Mixed Peppercorns 1.9Kg", "Premium 4 Mixed Peppercorns", null,
                    "product", "summary_large_image", null, "4 Mixed Peppercorns 1.9Kg", "1.9Kg pack", null, null, null,
                    0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null, 6, null, null, true, 1,
                    DateTime.Now, "localhost"
                },
                {
                    33, "ProductPage", "/p/5-mixed-peppercorns-50g", "5 Mixed Peppercorns 50g",
                    "Buy 5 Mixed Peppercorns 50g in the UK", "5 mixed peppercorns 50g", "index,follow",
                    "/p/5-mixed-peppercorns-50g", "5 Mixed Peppercorns 50g", "Premium 5 Mixed Peppercorns", null,
                    "product", "summary_large_image", null, "5 Mixed Peppercorns 50g", "50g pack", null, null, null,
                    0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null, 7, null, null, true, 1,
                    DateTime.Now, "localhost"
                },
                {
                    34, "ProductPage", "/p/5-mixed-peppercorns-90g", "5 Mixed Peppercorns 90g",
                    "Buy 5 Mixed Peppercorns 90g in the UK", "5 mixed peppercorns 90g", "index,follow",
                    "/p/5-mixed-peppercorns-90g", "5 Mixed Peppercorns 90g", "Premium 5 Mixed Peppercorns", null,
                    "product", "summary_large_image", null, "5 Mixed Peppercorns 90g", "90g pack", null, null, null,
                    0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null, 8, null, null, true, 1,
                    DateTime.Now, "localhost"
                },
                {
                    35, "ProductPage", "/p/5-mixed-peppercorns-200g", "5 Mixed Peppercorns 200g",
                    "Buy 5 Mixed Peppercorns 200g in the UK", "5 mixed peppercorns 200g", "index,follow",
                    "/p/5-mixed-peppercorns-200g", "5 Mixed Peppercorns 200g", "Premium 5 Mixed Peppercorns", null,
                    "product", "summary_large_image", null, "5 Mixed Peppercorns 200g", "200g pack", null, null, null,
                    0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null, 9, null, null, true, 1,
                    DateTime.Now, "localhost"
                },
                {
                    36, "ProductPage", "/p/5-mixed-peppercorns-450g", "5 Mixed Peppercorns 450g",
                    "Buy 5 Mixed Peppercorns 450g in the UK", "5 mixed peppercorns 450g", "index,follow",
                    "/p/5-mixed-peppercorns-450g", "5 Mixed Peppercorns 450g", "Premium 5 Mixed Peppercorns", null,
                    "product", "summary_large_image", null, "5 Mixed Peppercorns 450g", "450g pack", null, null, null,
                    0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null, 10, null, null, true, 1,
                    DateTime.Now, "localhost"
                },
                {
                    37, "ProductPage", "/p/5-mixed-peppercorns-950g", "5 Mixed Peppercorns 950g",
                    "Buy 5 Mixed Peppercorns 950g in the UK", "5 mixed peppercorns 950g", "index,follow",
                    "/p/5-mixed-peppercorns-950g", "5 Mixed Peppercorns 950g", "Premium 5 Mixed Peppercorns", null,
                    "product", "summary_large_image", null, "5 Mixed Peppercorns 950g", "950g pack", null, null, null,
                    0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null, 11, null, null, true, 1,
                    DateTime.Now, "localhost"
                },
                {
                    38, "ProductPage", "/p/5-mixed-peppercorns-1-9kg", "5 Mixed Peppercorns 1.9Kg",
                    "Buy 5 Mixed Peppercorns 1.9Kg in the UK", "5 mixed peppercorns 1.9kg", "index,follow",
                    "/p/5-mixed-peppercorns-1-9kg", "5 Mixed Peppercorns 1.9Kg", "Premium 5 Mixed Peppercorns", null,
                    "product", "summary_large_image", null, "5 Mixed Peppercorns 1.9Kg", "1.9Kg pack", null, null, null,
                    0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null, 12, null, null, true, 1,
                    DateTime.Now, "localhost"
                },
                {
                    39, "ProductPage", "/p/annatto-seeds-50g", "Annatto Seeds 50g", "Buy Annatto Seeds 50g in the UK",
                    "annatto seeds 50g", "index,follow", "/p/annatto-seeds-50g", "Annatto Seeds 50g",
                    "Premium Annatto Seeds", null, "product", "summary_large_image", null, "Annatto Seeds 50g",
                    "50g pack", null, null, null, 0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null,
                    13, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    40, "ProductPage", "/p/annatto-seeds-90g", "Annatto Seeds 90g", "Buy Annatto Seeds 90g in the UK",
                    "annatto seeds 90g", "index,follow", "/p/annatto-seeds-90g", "Annatto Seeds 90g",
                    "Premium Annatto Seeds", null, "product", "summary_large_image", null, "Annatto Seeds 90g",
                    "90g pack", null, null, null, 0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null,
                    14, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    41, "ProductPage", "/p/annatto-seeds-200g", "Annatto Seeds 200g",
                    "Buy Annatto Seeds 200g in the UK", "annatto seeds 200g", "index,follow", "/p/annatto-seeds-200g",
                    "Annatto Seeds 200g", "Premium Annatto Seeds", null, "product", "summary_large_image", null,
                    "Annatto Seeds 200g", "200g pack", null, null, null, 0.9m, "weekly", null, true, null, null, "en",
                    "GB", 95, null, null, 15, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    42, "ProductPage", "/p/annatto-seeds-450g", "Annatto Seeds 450g",
                    "Buy Annatto Seeds 450g in the UK", "annatto seeds 450g", "index,follow", "/p/annatto-seeds-450g",
                    "Annatto Seeds 450g", "Premium Annatto Seeds", null, "product", "summary_large_image", null,
                    "Annatto Seeds 450g", "450g pack", null, null, null, 0.9m, "weekly", null, true, null, null, "en",
                    "GB", 95, null, null, 16, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    43, "ProductPage", "/p/annatto-seeds-950g", "Annatto Seeds 950g",
                    "Buy Annatto Seeds 950g in the UK", "annatto seeds 950g", "index,follow", "/p/annatto-seeds-950g",
                    "Annatto Seeds 950g", "Premium Annatto Seeds", null, "product", "summary_large_image", null,
                    "Annatto Seeds 950g", "950g pack", null, null, null, 0.9m, "weekly", null, true, null, null, "en",
                    "GB", 95, null, null, 17, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    44, "ProductPage", "/p/annatto-seeds-1-9kg", "Annatto Seeds 1.9Kg",
                    "Buy Annatto Seeds 1.9Kg in the UK", "annatto seeds 1.9kg", "index,follow",
                    "/p/annatto-seeds-1-9kg", "Annatto Seeds 1.9Kg", "Premium Annatto Seeds", null, "product",
                    "summary_large_image", null, "Annatto Seeds 1.9Kg", "1.9Kg pack", null, null, null, 0.9m, "weekly",
                    null, true, null, null, "en", "GB", 95, null, null, 18, null, null, true, 1, DateTime.Now,
                    "localhost"
                },
                {
                    45, "ProductPage", "/p/basil-seeds-50g", "Basil Seeds 50g", "Buy Basil Seeds 50g in the UK",
                    "basil seeds 50g", "index,follow", "/p/basil-seeds-50g", "Basil Seeds 50g", "Premium Basil Seeds",
                    null, "product", "summary_large_image", null, "Basil Seeds 50g", "50g pack", null, null, null, 0.9m,
                    "weekly", null, true, null, null, "en", "GB", 95, null, null, 19, null, null, true, 1, DateTime.Now,
                    "localhost"
                },
                {
                    46, "ProductPage", "/p/basil-seeds-90g", "Basil Seeds 90g", "Buy Basil Seeds 90g in the UK",
                    "basil seeds 90g", "index,follow", "/p/basil-seeds-90g", "Basil Seeds 90g", "Premium Basil Seeds",
                    null, "product", "summary_large_image", null, "Basil Seeds 90g", "90g pack", null, null, null, 0.9m,
                    "weekly", null, true, null, null, "en", "GB", 95, null, null, 20, null, null, true, 1, DateTime.Now,
                    "localhost"
                },
                {
                    47, "ProductPage", "/p/basil-seeds-200g", "Basil Seeds 200g", "Buy Basil Seeds 200g in the UK",
                    "basil seeds 200g", "index,follow", "/p/basil-seeds-200g", "Basil Seeds 200g",
                    "Premium Basil Seeds", null, "product", "summary_large_image", null, "Basil Seeds 200g",
                    "200g pack", null, null, null, 0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null,
                    21, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    48, "ProductPage", "/p/basil-seeds-450g", "Basil Seeds 450g", "Buy Basil Seeds 450g in the UK",
                    "basil seeds 450g", "index,follow", "/p/basil-seeds-450g", "Basil Seeds 450g",
                    "Premium Basil Seeds", null, "product", "summary_large_image", null, "Basil Seeds 450g",
                    "450g pack", null, null, null, 0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null,
                    22, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    49, "ProductPage", "/p/basil-seeds-950g", "Basil Seeds 950g", "Buy Basil Seeds 950g in the UK",
                    "basil seeds 950g", "index,follow", "/p/basil-seeds-950g", "Basil Seeds 950g",
                    "Premium Basil Seeds", null, "product", "summary_large_image", null, "Basil Seeds 950g",
                    "950g pack", null, null, null, 0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null,
                    23, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    50, "ProductPage", "/p/basil-seeds-1-9kg", "Basil Seeds 1.9Kg", "Buy Basil Seeds 1.9Kg in the UK",
                    "basil seeds 1.9kg", "index,follow", "/p/basil-seeds-1-9kg", "Basil Seeds 1.9Kg",
                    "Premium Basil Seeds", null, "product", "summary_large_image", null, "Basil Seeds 1.9Kg",
                    "1.9Kg pack", null, null, null, 0.9m, "weekly", null, true, null, null, "en", "GB", 95, null, null,
                    24, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    51, "ProductPage", "/p/almond-powder-50g", "Almond Powder 50g", "Buy Almond Powder 50g in the UK",
                    "almond powder 50g", "index,follow", "/p/almond-powder-50g", "Almond Powder 50g",
                    "Premium Almond Powder", null, "product", "summary_large_image", null, "Almond Powder 50g",
                    "50g pack", null, null, null, 0.9m, "weekly", null, true, null, null, "en", "GB", 90, null, null,
                    25, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    52, "ProductPage", "/p/almond-powder-90g", "Almond Powder 90g", "Buy Almond Powder 90g in the UK",
                    "almond powder 90g", "index,follow", "/p/almond-powder-90g", "Almond Powder 90g",
                    "Premium Almond Powder", null, "product", "summary_large_image", null, "Almond Powder 90g",
                    "90g pack", null, null, null, 0.9m, "weekly", null, true, null, null, "en", "GB", 90, null, null,
                    26, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    53, "ProductPage", "/p/almond-powder-200g", "Almond Powder 200g",
                    "Buy Almond Powder 200g in the UK", "almond powder 200g", "index,follow", "/p/almond-powder-200g",
                    "Almond Powder 200g", "Premium Almond Powder", null, "product", "summary_large_image", null,
                    "Almond Powder 200g", "200g pack", null, null, null, 0.9m, "weekly", null, true, null, null, "en",
                    "GB", 90, null, null, 27, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    54, "ProductPage", "/p/almond-powder-450g", "Almond Powder 450g",
                    "Buy Almond Powder 450g in the UK", "almond powder 450g", "index,follow", "/p/almond-powder-450g",
                    "Almond Powder 450g", "Premium Almond Powder", null, "product", "summary_large_image", null,
                    "Almond Powder 450g", "450g pack", null, null, null, 0.9m, "weekly", null, true, null, null, "en",
                    "GB", 90, null, null, 28, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    55, "ProductPage", "/p/almond-powder-950g", "Almond Powder 950g",
                    "Buy Almond Powder 950g in the UK", "almond powder 950g", "index,follow", "/p/almond-powder-950g",
                    "Almond Powder 950g", "Premium Almond Powder", null, "product", "summary_large_image", null,
                    "Almond Powder 950g", "950g pack", null, null, null, 0.9m, "weekly", null, true, null, null, "en",
                    "GB", 90, null, null, 29, null, null, true, 1, DateTime.Now, "localhost"
                },
                {
                    56, "ProductPage", "/p/almond-powder-1-9kg", "Almond Powder 1.9Kg",
                    "Buy Almond Powder 1.9Kg in the UK", "almond powder 1.9kg", "index,follow",
                    "/p/almond-powder-1-9kg", "Almond Powder 1.9Kg", "Premium Almond Powder", null, "product",
                    "summary_large_image", null, "Almond Powder 1.9Kg", "1.9Kg pack", null, null, null, 0.9m, "weekly",
                    null, true, null, null, "en", "GB", 90, null, null, 30, null, null, true, 1, DateTime.Now,
                    "localhost"
                },
                {
                    57, "ProductPage", "/p/edible-gum-powder-50g", "Edible Gum Powder 50g",
                    "Buy Edible Gum Powder 50g in the UK", "edible gum powder 50g", "index,follow",
                    "/p/edible-gum-powder-50g", "Edible Gum Powder 50g", "Premium Edible Gum Powder", null, "product",
                    "summary_large_image", null, "Edible Gum Powder 50g", "50g pack", null, null, null, 0.9m, "weekly",
                    null, true, null, null, "en", "GB", 92, null, null, 31, null, null, true, 1, DateTime.Now,
                    "localhost"
                },
                {
                    58, "ProductPage", "/p/edible-gum-powder-100g", "Edible Gum Powder 100g",
                    "Buy Edible Gum Powder 100g in the UK", "edible gum powder 100g", "index,follow",
                    "/p/edible-gum-powder-100g", "Edible Gum Powder 100g", "Premium Edible Gum Powder", null, "product",
                    "summary_large_image", null, "Edible Gum Powder 100g", "100g pack", null, null, null, 0.9m,
                    "weekly", null, true, null, null, "en", "GB", 92, null, null, 32, null, null, true, 1, DateTime.Now,
                    "localhost"
                },
                {
                    59, "ProductPage", "/p/edible-gum-powder-200g", "Edible Gum Powder 200g",
                    "Buy Edible Gum Powder 200g in the UK", "edible gum powder 200g", "index,follow",
                    "/p/edible-gum-powder-200g", "Edible Gum Powder 200g", "Premium Edible Gum Powder", null, "product",
                    "summary_large_image", null, "Edible Gum Powder 200g", "200g pack", null, null, null, 0.9m,
                    "weekly", null, true, null, null, "en", "GB", 92, null, null, 33, null, null, true, 1, DateTime.Now,
                    "localhost"
                },
                {
                    60, "ProductPage", "/p/edible-gum-powder-500g", "Edible Gum Powder 500g",
                    "Buy Edible Gum Powder 500g in the UK", "edible gum powder 500g", "index,follow",
                    "/p/edible-gum-powder-500g", "Edible Gum Powder 500g", "Premium Edible Gum Powder", null, "product",
                    "summary_large_image", null, "Edible Gum Powder 500g", "500g pack", null, null, null, 0.9m,
                    "weekly", null, true, null, null, "en", "GB", 92, null, null, 34, null, null, true, 1, DateTime.Now,
                    "localhost"
                },
                {
                    61, "ProductPage", "/p/edible-gum-powder-1kg", "Edible Gum Powder 1Kg",
                    "Buy Edible Gum Powder 1Kg in the UK", "edible gum powder 1kg", "index,follow",
                    "/p/edible-gum-powder-1kg", "Edible Gum Powder 1Kg", "Premium Edible Gum Powder", null, "product",
                    "summary_large_image", null, "Edible Gum Powder 1Kg", "1Kg pack", null, null, null, 0.9m, "weekly",
                    null, true, null, null, "en", "GB", 92, null, null, 35, null, null, true, 1, DateTime.Now,
                    "localhost"
                },
                {
                    62, "ProductPage", "/p/edible-gum-powder-2kg", "Edible Gum Powder 2Kg",
                    "Buy Edible Gum Powder 2Kg in the UK", "edible gum powder 2kg", "index,follow",
                    "/p/edible-gum-powder-2kg", "Edible Gum Powder 2Kg", "Premium Edible Gum Powder", null, "product",
                    "summary_large_image", null, "Edible Gum Powder 2Kg", "2Kg pack", null, null, null, 0.9m, "weekly",
                    null, true, null, null, "en", "GB", 92, null, null, 36, null, null, true, 1, DateTime.Now,
                    "localhost"
                }
            }
        );

        // Product Category
         migrationBuilder.InsertData(
             "ProductCategory",
             ["Id", "ProductId", "CategoryId", "IsPrimary", "SortOrder", "CreatedBy", "CreatedAt", "CreatedByIp"],
             new object[,]
             {
                 { 1, 1, 2, true, 1, 1, DateTime.Now, "localhost" },
                 { 2, 2, 2, true, 2, 1, DateTime.Now, "localhost" },
                 { 3, 3, 2, true, 3, 1, DateTime.Now, "localhost" },
                 { 4, 4, 2, true, 4, 1, DateTime.Now, "localhost" },
                 { 5, 5, 2, true, 5, 1, DateTime.Now, "localhost" },
                 { 6, 6, 2, true, 6, 1, DateTime.Now, "localhost" },
                 { 7, 7, 2, true, 7, 1, DateTime.Now, "localhost" },
                 { 8, 8, 2, true, 8, 1, DateTime.Now, "localhost" },
                 { 9, 9, 2, true, 9, 1, DateTime.Now, "localhost" },
                 { 10, 10, 2, true, 10, 1, DateTime.Now, "localhost" },
                 { 11, 11, 2, true, 11, 1, DateTime.Now, "localhost" },
                 { 12, 12, 2, true, 12, 1, DateTime.Now, "localhost" },
                 { 13, 13, 3, true, 13, 1, DateTime.Now, "localhost" },
                 { 14, 14, 3, true, 14, 1, DateTime.Now, "localhost" },
                 { 15, 15, 3, true, 15, 1, DateTime.Now, "localhost" },
                 { 16, 16, 3, true, 16, 1, DateTime.Now, "localhost" },
                 { 17, 17, 3, true, 17, 1, DateTime.Now, "localhost" },
                 { 18, 18, 3, true, 18, 1, DateTime.Now, "localhost" },
                 { 19, 19, 3, true, 19, 1, DateTime.Now, "localhost" },
                 { 20, 20, 3, true, 20, 1, DateTime.Now, "localhost" },
                 { 21, 21, 3, true, 21, 1, DateTime.Now, "localhost" },
                 { 22, 22, 3, true, 22, 1, DateTime.Now, "localhost" },
                 { 23, 23, 3, true, 23, 1, DateTime.Now, "localhost" },
                 { 24, 24, 3, true, 24, 1, DateTime.Now, "localhost" },
                 { 25, 25, 4, true, 25, 1, DateTime.Now, "localhost" },
                 { 26, 26, 4, true, 26, 1, DateTime.Now, "localhost" },
                 { 27, 27, 4, true, 27, 1, DateTime.Now, "localhost" },
                 { 28, 28, 4, true, 28, 1, DateTime.Now, "localhost" },
                 { 29, 29, 4, true, 29, 1, DateTime.Now, "localhost" },
                 { 30, 30, 4, true, 30, 1, DateTime.Now, "localhost" },
                 { 31, 31, 4, true, 31, 1, DateTime.Now, "localhost" },
                 { 32, 32, 4, true, 32, 1, DateTime.Now, "localhost" },
                 { 33, 33, 4, true, 33, 1, DateTime.Now, "localhost" },
                 { 34, 34, 4, true, 34, 1, DateTime.Now, "localhost" },
                 { 35, 35, 4, true, 35, 1, DateTime.Now, "localhost" },
                 { 36, 36, 4, true, 36, 1, DateTime.Now, "localhost" },
             });

        // ImageType
        migrationBuilder.InsertData(
            "ImageType",
            ["Id", "Entity", "Name", "Slug", "Description", "IsActive", "CreatedBy", "CreatedAt", "CreatedByIp"],
            new object[,]
            {
                {
                    1, nameof(Product), "List Item", "list-item", "Product list item", true, 1, DateTime.Now,
                    "localhost"
                },
                {
                    2, nameof(Category), "List Item", "list-item", "Category list item", true, 1, DateTime.Now,
                    "localhost"
                },
                { 3, nameof(Page), "Banner", "banner", "Page banner", true, 1, DateTime.Now, "localhost" },
            });

        // Bank
        migrationBuilder.InsertData(
            "Bank",
            ["Id", "Name", "Slug", "IsActive", "SortOrder", "CreatedBy", "CreatedAt", "CreatedByIp"],
            new object[,]
            {
                { 1, "State Bank of India", "state-bank-of-india", true, 1, 1, DateTime.Now, "localhost" },
                { 2, "HDFC Bank", "hdfc-bank", true, 2, 1, DateTime.Now, "localhost" },
                { 3, "ICICI Bank", "icici-bank", true, 3, 1, DateTime.Now, "localhost" },
                { 4, "Axis Bank", "axis-bank", true, 4, 1, DateTime.Now, "localhost" },
                { 5, "Punjab National Bank", "punjab-national-bank", true, 5, 1, DateTime.Now, "localhost" },
                { 6, "Kotak Mahindra Bank", "kotak-mahindra-bank", true, 6, 1, DateTime.Now, "localhost" },
                { 7, "Bank of Baroda", "bank-of-baroda", true, 7, 1, DateTime.Now, "localhost" },
                { 8, "Canara Bank", "canara-bank", true, 8, 1, DateTime.Now, "localhost" },
                { 9, "Union Bank of India", "union-bank-of-india", true, 9, 1, DateTime.Now, "localhost" },
                { 10, "IndusInd Bank", "indusind-bank", true, 10, 1, DateTime.Now, "localhost" }
            });

        // Post Code
        migrationBuilder.InsertData(
            "PostCode",
            ["Id", "Code", "IsActive", "CreatedBy", "CreatedAt", "CreatedByIp"],
            new object[,]
            {
                { 1, "400001", true, 1, DateTime.Now, "localhost" },
                { 2, "400002", true, 1, DateTime.Now, "localhost" },
                { 3, "400003", true, 1, DateTime.Now, "localhost" },
                { 4, "400004", true, 1, DateTime.Now, "localhost" },
                { 5, "400005", true, 1, DateTime.Now, "localhost" },
                { 6, "400006", true, 1, DateTime.Now, "localhost" },
                { 7, "400007", true, 1, DateTime.Now, "localhost" },
                { 8, "400008", true, 1, DateTime.Now, "localhost" },
                { 9, "400009", true, 1, DateTime.Now, "localhost" },
                { 10, "400010", true, 1, DateTime.Now, "localhost" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DELETE from PostCode");
        migrationBuilder.Sql("DELETE from Bank");
        migrationBuilder.Sql("DELETE from ImageType");
        migrationBuilder.Sql("DELETE from DeliveryWindow");
        migrationBuilder.Sql("DELETE from Page");
        migrationBuilder.Sql("DELETE from ProductGroup");
        migrationBuilder.Sql("DELETE from ProductAttribute");
        migrationBuilder.Sql("DELETE from Category");
        migrationBuilder.Sql("DELETE from Brand");
        migrationBuilder.Sql("DELETE from AppUserRole");
        migrationBuilder.Sql("DELETE from AppUser");
        migrationBuilder.Sql("DELETE from Role");
    }
}