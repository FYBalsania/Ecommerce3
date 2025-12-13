using Ecommerce3.Domain.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce3.Infrastructure.Migrations;

public partial class SeedData : Migration
{
    private static readonly string[] roleColumns = ["Id", "Name", "NormalizedName", "ConcurrencyStamp"];

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
        //roles
        migrationBuilder.InsertData("Role", roleColumns, new object[] { "1", "Super Admin", "SUPER ADMIN", "1" });

        //user
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

        migrationBuilder.InsertData(
            table: "AppUserRole",
            columns: new[] { "UserId", "RoleId" },
            values: new object[] { "1", "1" } // Super Admin
        );

        //Home Page.
        migrationBuilder.InsertData("Page",
            [
                "Path", "Discriminator", "MetaTitle", "SitemapPriority", "SitemapFrequency", "IsIndexed", "Language",
                "IsActive", "CreatedBy", "CreatedAt", "CreatedByIp"
            ],
            ["/", "Page", "SpiceMart", 0.80, "Weekly", true, "en", true, 1, DateTime.Now, "::1"]);

        //Unit of Measures.
        migrationBuilder.InsertData("UnitOfMeasure",
            [
                "Id", "Code", "Name", "Type", "BaseId", "ConversionFactor", "IsActive", "CreatedBy", "CreatedAt",
                "CreatedByIp"
            ],
            ["1", "g", "Gram", nameof(UnitOfMeasureType.Weight), null, 1, true, 1, DateTime.Now, "::1"]);
        migrationBuilder.InsertData("UnitOfMeasure",
            [
                "Id", "Code", "Name", "Type", "BaseId", "ConversionFactor", "IsActive", "CreatedBy", "CreatedAt",
                "CreatedByIp"
            ],
            ["2", "kg", "Kilogram", nameof(UnitOfMeasureType.Weight), 1, 1000, true, 1, DateTime.Now, "::1"]);
        migrationBuilder.InsertData("UnitOfMeasure",
            [
                "Id", "Code", "Name", "Type", "BaseId", "ConversionFactor", "IsActive", "CreatedBy", "CreatedAt",
                "CreatedByIp"
            ],
            ["3", "pc", "Piece", nameof(UnitOfMeasureType.Count), null, 1, true, 1, DateTime.Now, "::1"]);
        migrationBuilder.InsertData("UnitOfMeasure",
            [
                "Id", "Code", "Name", "Type", "BaseId", "ConversionFactor", "IsActive", "CreatedBy", "CreatedAt",
                "CreatedByIp"
            ],
            ["4", "pack", "Pack of 3", nameof(UnitOfMeasureType.Count), 3, 3, true, 1, DateTime.Now, "::1"]);

        //Brand
        migrationBuilder.InsertData(
            "Brand",
            new[]
            {
                "Id", "Name", "Slug", "Display", "Breadcrumb", "AnchorText", "AnchorTitle",
                "ShortDescription", "FullDescription", "IsActive", "SortOrder",
                "CreatedBy", "CreatedAt", "CreatedByIp"
            },
            new object[,]
            {
                {
                    1, "Spice Mart", "spice-mart", "Spice Mart", "Spice Mart", "Spice Mart", "Spice Mart", 
                    "Spice Mart", "<p><strong>Spice Mart</strong></p>", true, 1, 
                    1, DateTime.Now, "::1"
                },
            });

        //Brand Page
        migrationBuilder.InsertData(
            "Page",
            new[]
            {
                "Id", "Discriminator", "Path", "MetaTitle", "MetaDescription", "MetaKeywords",
                "H1", "SitemapPriority", "SitemapFrequency", "IsIndexed", "Language", "Region", "SeoScore", "BrandId", "IsActive",
                "CreatedBy", "CreatedAt", "CreatedByIp"
            },
            new object[,]
            {
                {
                    2, "BrandPage", "spice-mart/brand", "Spice Mart", "Spice Mart Brand Page", "Spice Mart, Spice, Spices",
                    "Spice Mart", 0.80, "Yearly", true, "en", "UK", 85, 1, true, 
                    1, DateTime.Now, "::1"
                }
            });

        //Category
         migrationBuilder.InsertData(
            "Category",
            new[]
            {
                "Id", "Name", "Slug", "Display", "Breadcrumb", "AnchorText", "AnchorTitle",
                "ShortDescription", "FullDescription", "ParentId", "GoogleCategory", "Path",
                "IsActive", "SortOrder", "CreatedBy", "CreatedAt", "CreatedByIp"
            },
            new object[,]
            {
                {
                    1, "Spices", "spices", "Spices", "Spices", "Spices", "Spices",
                    "Spices Short Description", "<p><strong>Spices</strong></p>", null,
                    "Food, Beverages & Tobacco > Food Items > Herbs & Spices", "spices",
                    true, 1, 1, DateTime.Now, "::1"
                },
                {
                    2, "Whole Spices", "whole-spices", "Whole Spices", "Whole Spices", "Whole Spices", "Whole Spices",
                    "Whole Spices Short Description", "<p><strong>Whole Spices</strong></p>", 1,
                    "Food, Beverages & Tobacco > Food Items > Herbs & Spices > Whole Spices",
                    "spices.whole-spices", true, 2, 1, DateTime.Now, "::1"
                },
                {
                    3, "Ground Spices", "ground-spices", "Ground Spices", "Ground Spices", "Ground Spices", "Ground Spices",
                    "Ground Spices Short Description", "<p><strong>Ground Spices</strong></p>", 1,
                    "Food, Beverages & Tobacco > Food Items > Herbs & Spices > Ground Spices",
                    "spices.ground-spices", true, 3, 1, DateTime.Now, "::1"
                },
                {
                    4, "Blended Spices", "blended-spices", "Blended Spices", "Blended Spices", "Blended Spices", "Blended Spices",
                    "Blended Spices Short Description", "<p><strong>Blended Spices</strong></p>", 1,
                    "Food, Beverages & Tobacco > Food Items > Herbs & Spices > Spice Mixes",
                    "spices.blended-spices", true, 4, 1, DateTime.Now, "::1"
                },
                {
                    5, "Herbs", "herbs", "Herbs", "Herbs", "Herbs", "Herbs",
                    "Herbs Short Description", "<p><strong>Herbs</strong></p>", 1,
                    "Food, Beverages & Tobacco > Food Items > Herbs & Spices > Herbs",
                    "spices.herbs", true, 5, 1, DateTime.Now, "::1"
                },
                {
                    6, "Organic Spices", "organic-spices", "Organic Spices", "Organic Spices", "Organic Spices", "Organic Spices",
                    "Organic Spices Short Description", "<p><strong>Organic Spices</strong></p>", 1,
                    "Food, Beverages & Tobacco > Food Items > Organic Food > Organic Spices",
                    "spices.organic-spices", true, 6, 1, DateTime.Now, "::1"
                },
                {
                    7, "Masala Powders", "masala-powders", "Masala Powders", "Masala Powders", "Masala Powders", "Masala Powders",
                    "Masala Powders Short Description", "<p><strong>Masala Powders</strong></p>", 1,
                    "Food, Beverages & Tobacco > Food Items > Herbs & Spices > Masalas",
                    "spices.masala-powders", true, 7, 1, DateTime.Now, "::1"
                },
                {
                    8, "Regional Spices", "regional-spices", "Regional Spices", "Regional Spices", "Regional Spices", "Regional Spices",
                    "Regional Spices Short Description", "<p><strong>Regional Spices</strong></p>", 1,
                    "Food, Beverages & Tobacco > Food Items > Herbs & Spices",
                    "spices.regional-spices", true, 8, 1, DateTime.Now, "::1"
                },
                {
                    9, "Exotic Spices", "exotic-spices", "Exotic Spices", "Exotic Spices", "Exotic Spices", "Exotic Spices",
                    "Exotic Spices Short Description", "<p><strong>Exotic Spices</strong></p>", 1,
                    "Food, Beverages & Tobacco > Food Items > Herbs & Spices > Exotic Spices",
                    "spices.exotic-spices", true, 9, 1, DateTime.Now, "::1"
                },
                {
                    10, "Seasonings", "seasonings", "Seasonings", "Seasonings", "Seasonings", "Seasonings",
                    "Seasonings Short Description", "<p><strong>Seasonings</strong></p>", 1,
                    "Food, Beverages & Tobacco > Food Items > Herbs & Spices > Seasonings",
                    "spices.seasonings", true, 10, 1, DateTime.Now, "::1"
                }
            });

         //Category Page
        migrationBuilder.InsertData(
            "Page",
            new[]
            {
                "Id", "Discriminator", "Path", "MetaTitle", "MetaDescription", "MetaKeywords",
                "H1", "SitemapPriority", "SitemapFrequency", "IsIndexed",
                "Language", "Region", "SeoScore", "CategoryId", "IsActive",
                "CreatedBy", "CreatedAt", "CreatedByIp"
            },
            new object[,]
            {
                {
                    3, "CategoryPage", "spices/category", "Spices",
                    "Explore a wide range of premium spices for everyday cooking",
                    "Spices, Indian Spices, Cooking Spices, Masala",
                    "Spices", 0.90, "Monthly", true, "en", "IN", 90, 1, true,
                    1, DateTime.Now, "::1"
                },
                {
                    4, "CategoryPage", "whole-spices/category", "Whole Spices",
                    "Buy whole spices with rich aroma and authentic taste",
                    "Whole Spices, Cardamom, Cloves, Pepper",
                    "Whole Spices", 0.88, "Monthly", true, "en", "IN", 88, 2, true,
                    1, DateTime.Now, "::1"
                },
                {
                    5, "CategoryPage", "ground-spices/category", "Ground Spices",
                    "Freshly ground spices for perfect flavor in every dish",
                    "Ground Spices, Spice Powders, Masala Powder",
                    "Ground Spices", 0.87, "Monthly", true, "en", "IN", 87, 3, true,
                    1, DateTime.Now, "::1"
                },
                {
                    6, "CategoryPage", "blended-spices/category", "Blended Spices",
                    "Expertly blended spices for traditional and modern recipes",
                    "Blended Spices, Spice Mix, Masala Mix",
                    "Blended Spices", 0.86, "Monthly", true, "en", "IN", 86, 4, true,
                    1, DateTime.Now, "::1"
                },
                {
                    7, "CategoryPage", "herbs/category", "Herbs",
                    "Natural dried herbs for cooking and seasoning",
                    "Herbs, Dried Herbs, Cooking Herbs",
                    "Herbs", 0.84, "Yearly", true, "en", "IN", 84, 5, true,
                    1, DateTime.Now, "::1"
                },
                {
                    8, "CategoryPage", "organic-spices/category", "Organic Spices",
                    "Certified organic spices grown without chemicals",
                    "Organic Spices, Natural Spices, Healthy Cooking",
                    "Organic Spices", 0.89, "Monthly", true, "en", "IN", 89, 6, true,
                    1, DateTime.Now, "::1"
                },
                {
                    9, "CategoryPage", "masala-powders/category", "Masala Powders",
                    "Traditional masala powders for authentic Indian taste",
                    "Masala Powder, Indian Masala, Curry Masala",
                    "Masala Powders", 0.91, "Monthly", true, "en", "IN", 91, 7, true,
                    1, DateTime.Now, "::1"
                },
                {
                    10, "CategoryPage", "regional-spices/category", "Regional Spices",
                    "Regional spice specialties from across India",
                    "Regional Spices, South Indian, North Indian Masala",
                    "Regional Spices", 0.83, "Yearly", true, "en", "IN", 83, 8, true,
                    1, DateTime.Now, "::1"
                },
                {
                    11, "CategoryPage", "exotic-spices/category", "Exotic Spices",
                    "Rare and exotic spices sourced from around the world",
                    "Exotic Spices, Premium Spices, Gourmet Spices",
                    "Exotic Spices", 0.85, "Yearly", true, "en", "IN", 85, 9, true,
                    1, DateTime.Now, "::1"
                },
                {
                    12, "CategoryPage", "seasonings/category", "Seasonings",
                    "Seasonings and spice blends to enhance every meal",
                    "Seasonings, Flavoring, Spice Seasoning",
                    "Seasonings", 0.88, "Monthly", true, "en", "IN", 88, 10, true,
                    1, DateTime.Now, "::1"
                }
            });


        // ProductAttribute
        // migrationBuilder.InsertData(
        //     "ProductAttribute",
        //     new[]
        //     { 
        //         "Id", "Name", "Slug", "Display", "Breadcrumb", "DataType",
        //         "SortOrder", "CreatedBy", "CreatedAt", "CreatedByIp" 
        //     },
        //     new object[,]
        //     {
        //         { 1, "Colour", "colour", "Colour", "Colour", "Colour", 1, 1, DateTime.Now, "::1" },
        //         { 2, "Storage", "storage", "Storage", "Storage", "Text", 2, 1, DateTime.Now, "::1" },
        //         { 3, "RAM", "ram", "RAM", "RAM", "DateOnly", 3, 1, DateTime.Now, "::1" },
        //         { 4, "Screen Size", "screen-size", "Screen Size", "Screen Size", "Decimal", 4, 1, DateTime.Now, "::1" },
        //         { 5, "Processor", "processor", "Processor", "Processor", "Text", 5, 1, DateTime.Now, "::1" },
        //         { 6, "Battery Capacity", "battery-capacity", "Battery Capacity", "Battery Capacity", "Text", 6, 1, DateTime.Now, "::1" },
        //         { 7, "Material", "material", "Material", "Material", "Text", 7, 1, DateTime.Now, "::1" },
        //         { 8, "Weight", "weight", "Weight", "Weight", "Text", 8, 1, DateTime.Now, "::1" },
        //         { 9, "Warranty", "warranty", "Warranty", "Warranty", "Text", 9, 1, DateTime.Now, "::1" },
        //         { 10, "Connectivity", "connectivity", "Connectivity", "Connectivity", "Text", 10, 1, DateTime.Now, "::1" },
        //         { 11, "ESim", "esim", "ESim", "ESim", "Boolean", 10, 1, DateTime.Now, "::1" }
        //     });

        // ProductAttribute
        // migrationBuilder.InsertData(
        //     "ProductAttributeValue",
        //     new[]
        //     { 
        //         "Id", "ProductAttributeId", "Discriminator", "Value", "Slug", "Display", "Breadcrumb", "HexCode", "ColourFamily", "ColourFamilyHexCode",
        //         "SortOrder", "CreatedBy", "CreatedAt", "CreatedByIp" 
        //     },
        //     new object[,]
        //     {
        //         { 1, 1, nameof(ProductAttributeColourValue), "Blood Red", "blood-red", "Blood Red", "Blood Red", "#8A0303", "Red", "#FF0000", 1, 1, DateTime.Now, "::1" },
        //         { 2, 1, nameof(ProductAttributeColourValue), "Pale Yellow", "pale-yellow", "Pale Yellow", "Pale Yellow", "#FFFF99", "Yellow", "#FFFF99", 2, 1, DateTime.Now, "::1" },
        //         { 3, 1, nameof(ProductAttributeColourValue), "Emerald Green", "emerald-green", "Emerald Green", "Emerald Green", "#50C878", "Green", "#50C878", 3, 1, DateTime.Now, "::1" },
        //     });
        //
        // migrationBuilder.InsertData(
        //     "ProductAttributeValue",
        //     new[]
        //     { 
        //         "Id", "ProductAttributeId", "Discriminator", "Value", "Slug", "Display", "Breadcrumb",
        //         "SortOrder", "CreatedBy", "CreatedAt", "CreatedByIp" 
        //     },
        //     new object[,]
        //     {
        //         { 4, 2, nameof(ProductAttributeValue), "Hello", "hello", "Hello", "Hello", 1, 1, DateTime.Now, "::1" },
        //         { 5, 2, nameof(ProductAttributeValue), "Hi", "hi", "Hi", "Hi", 1, 1, DateTime.Now, "::1" },
        //     });
        //
        // migrationBuilder.InsertData(
        //     "ProductAttributeValue",
        //     new[]
        //     { 
        //         "Id", "ProductAttributeId", "Discriminator", "Value", "Slug", "Display", "Breadcrumb", "DateOnlyValue",
        //         "SortOrder", "CreatedBy", "CreatedAt", "CreatedByIp" 
        //     },
        //     new object[,]
        //     {
        //         { 6, 3, nameof(ProductAttributeDateOnlyValue), "Hello", "hello", "Hello", "Hello", new DateOnly(2025, 01, 01), 1, 1, DateTime.Now, "::1" },
        //         { 7, 3, nameof(ProductAttributeDateOnlyValue), "Hi", "hi", "Hi", "Hi", new DateOnly(2025, 01, 02), 2, 1, DateTime.Now, "::1" }
        //     });
        //
        //
        // migrationBuilder.InsertData(
        //     "ProductAttributeValue",
        //     new[]
        //     { 
        //         "Id", "ProductAttributeId", "Discriminator", "Value", "Slug", "Display", "Breadcrumb", "DecimalValue",
        //         "SortOrder", "CreatedBy", "CreatedAt", "CreatedByIp" 
        //     },
        //     new object[,]
        //     {
        //         { 8, 4, nameof(ProductAttributeDecimalValue), "100.50", "100-50", "100.50", "100.50", 100.50m, 1, 1, DateTime.Now, "::1" },
        //         { 9, 4, nameof(ProductAttributeDecimalValue), "249.99", "249-99", "249.99", "249.99", 249.99m, 2, 1, DateTime.Now, "::1" }
        //     });
        //
        // migrationBuilder.InsertData(
        //     "ProductAttributeValue",
        //     new[]
        //     { 
        //         "Id", "ProductAttributeId", "Discriminator", "Value", "Slug", "Display", "Breadcrumb", "BooleanValue",
        //         "SortOrder", "CreatedBy", "CreatedAt", "CreatedByIp" 
        //     },
        //     new object[,]
        //     {
        //         { 10, 11, nameof(ProductAttributeBooleanValue), "Yes", "yes", "Yes", "Yes", true, 1, 1, DateTime.Now, "::1" },
        //         { 11, 11, nameof(ProductAttributeBooleanValue), "No", "no", "No", "No", false, 2, 1, DateTime.Now, "::1" }
        //     });

        // ProductGroup
        // migrationBuilder.InsertData(
        //     "ProductGroup",
        //     new[]
        //     {
        //         "Id", "Name", "Slug", "Display", "Breadcrumb", "AnchorText", "AnchorTitle",
        //         "ShortDescription", "FullDescription", "IsActive", "SortOrder",
        //         "CreatedBy", "CreatedAt", "CreatedByIp"
        //     },
        //     new object[,]
        //     {
        //         {
        //             1, "iPhone 17", "iphone-17", "iPhone 17", "iPhone 17", "iPhone 17", "iPhone 17",
        //             "Premium Apple smartphone series",
        //             "<p><strong>Apple iPhone</strong> delivers high performance, elegant design, and cutting-edge cameras.</p>",
        //             true, 1, 1, DateTime.Now, "::1"
        //         },
        //         {
        //             2, "Duke 200 2015", "duke-200-2015", "Duke 200 2015", "Duke 200 2015", "Duke 200 2015",
        //             "Duke 200 2015", "KTM’s signature performance bike series",
        //             "<p><strong>KTM Duke</strong> – lightweight, powerful, and built for speed.</p>", true, 2, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             3, "Galaxy S25", "galaxy-s25", "Galaxy S25", "Galaxy S25", "Galaxy S25", "Galaxy S25",
        //             "Samsung Galaxy smartphones known for innovation",
        //             "<p><strong>Samsung Galaxy</strong> devices redefine Android excellence.</p>", true, 3, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             4, "PlayStation 5", "playstation-5", "PlayStation 5", "PlayStation 5", "PlayStation 5",
        //             "PlayStation 5", "Sony’s legendary gaming console line",
        //             "<p><strong>Sony PlayStation</strong> – immersive gaming for all generations.</p>", true, 4, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             5, "ThinkPad", "thinkpad", "ThinkPad", "ThinkPad", "ThinkPad", "ThinkPad",
        //             "Lenovo’s durable and business-class laptops",
        //             "<p><strong>Lenovo ThinkPad</strong> – trusted productivity companion.</p>", true, 5, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             6, "Surface", "surface", "Surface", "Surface", "Surface", "Surface",
        //             "Microsoft Surface tablets and laptops",
        //             "<p><strong>Microsoft Surface</strong> combines power, portability, and style.</p>", true, 6, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             7, "Model S", "model-s", "Model S", "Model S", "Model S", "Model S",
        //             "Tesla’s premium electric sedan",
        //             "<p><strong>Tesla Model S</strong> – the benchmark for electric performance.</p>", true, 7, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             8, "Legion", "legion", "Legion", "Legion", "Legion", "Legion",
        //             "Lenovo Legion gaming laptops and desktops",
        //             "<p><strong>Lenovo Legion</strong> – for gamers who demand power.</p>", true, 8, 1, DateTime.Now,
        //             "::1"
        //         },
        //         {
        //             9, "ROG", "rog", "ROG", "ROG", "ROG", "ROG", "ASUS Republic of Gamers product line",
        //             "<p><strong>ASUS ROG</strong> – elite gaming hardware and innovation.</p>", true, 9, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             10, "Echo", "echo", "Echo", "Echo", "Echo", "Echo", "Amazon Echo smart devices with Alexa",
        //             "<p><strong>Amazon Echo</strong> – smart living with voice control.</p>", true, 10, 1, DateTime.Now,
        //             "::1"
        //         }
        //     });

        // Page
        // migrationBuilder.InsertData(
        //     "Page",
        //     new[]
        //     {
        //         "Id", "Discriminator", "Path", "MetaTitle", "MetaDescription", "MetaKeywords",
        //         "H1", "SitemapPriority", "SitemapFrequency", "IsIndexed",
        //         "Language", "Region", "SeoScore", "ProductGroupId", "IsActive",
        //         "CreatedBy", "CreatedAt", "CreatedByIp"
        //     },
        //     new object[,]
        //     {
        //         {
        //             21, "ProductGroupPage", "iphone/productgroup", "iPhone", "Apple iPhone Series Overview",
        //             "iPhone, Apple, Smartphones, iOS", "iPhone", 0.90, "Monthly", true, "en", "US", 92, 1, true, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             22, "ProductGroupPage", "duke/productgroup", "Duke", "KTM Duke Motorcycles",
        //             "KTM, Duke, Motorcycles, Bikes", "Duke", 0.85, "Monthly", true, "en", "IN", 88, 2, true, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             23, "ProductGroupPage", "galaxy/productgroup", "Galaxy", "Samsung Galaxy Smartphones",
        //             "Samsung, Galaxy, Android, Phones", "Galaxy", 0.88, "Monthly", true, "en", "US", 89, 3, true, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             24, "ProductGroupPage", "playstation/productgroup", "PlayStation", "Sony PlayStation Consoles",
        //             "PlayStation, PS5, Gaming, Sony", "PlayStation", 0.80, "Monthly", true, "en", "JP", 85, 4, true, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             25, "ProductGroupPage", "thinkpad/productgroup", "ThinkPad", "Lenovo ThinkPad Laptops",
        //             "ThinkPad, Lenovo, Laptops, Business", "ThinkPad", 0.78, "Yearly", true, "en", "US", 83, 5, true, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             26, "ProductGroupPage", "surface/productgroup", "Surface", "Microsoft Surface Devices",
        //             "Surface, Microsoft, Laptop, Tablet", "Surface", 0.82, "Monthly", true, "en", "US", 86, 6, true, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             27, "ProductGroupPage", "models/productgroup", "Model S", "Tesla Model S Electric Car",
        //             "Tesla, Model S, EV, Electric Car", "Model S", 0.90, "Monthly", true, "en", "US", 91, 7, true, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             28, "ProductGroupPage", "legion/productgroup", "Legion", "Lenovo Legion Gaming Series",
        //             "Legion, Lenovo, Gaming, Laptop", "Legion", 0.84, "Monthly", true, "en", "US", 87, 8, true, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             29, "ProductGroupPage", "rog/productgroup", "ROG", "ASUS ROG Gaming Products",
        //             "ASUS, ROG, Gaming, Laptop, PC", "ROG", 0.86, "Monthly", true, "en", "US", 88, 9, true, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             30, "ProductGroupPage", "echo/productgroup", "Echo", "Amazon Echo Smart Devices",
        //             "Amazon, Echo, Alexa, Smart Home", "Echo", 0.83, "Weekly", true, "en", "US", 85, 10, true, 1,
        //             DateTime.Now, "::1"
        //         }
        //     });

        // DeliveryWindow
        migrationBuilder.InsertData(
            "DeliveryWindow",
            new[]
            {
                "Id", "Name", "Unit", "MinValue", "MaxValue",
                "NormalizedMinDays", "NormalizedMaxDays", "IsActive",
                "SortOrder", "CreatedBy", "CreatedAt", "CreatedByIp"
            },
            new object[,]
            {
                { 1, "Same Day Delivery", "Hour", 0, 24, 0m, 1m, true, 1, 1, DateTime.Now, "::1" },
                { 2, "Next Day Delivery", "Day", 1, 1, 1m, 1m, true, 2, 1, DateTime.Now, "::1" },
                { 3, "2-3 Days Delivery", "Day", 2, 3, 2m, 3m, true, 3, 1, DateTime.Now, "::1" },
                { 4, "4-5 Days Delivery", "Day", 4, 5, 4m, 5m, true, 4, 1, DateTime.Now, "::1" },
                { 5, "Standard (1 Week)", "Day", 5, 7, 5m, 7m, true, 5, 1, DateTime.Now, "::1" },
                { 6, "Express (2-3 Hours)", "Hour", 2, 3, 0.1m, 0.2m, true, 6, 1, DateTime.Now, "::1" },
                { 7, "Scheduled (2 Weeks)", "Day", 14, 14, 14m, 14m, true, 7, 1, DateTime.Now, "::1" },
                { 8, "Bulk Order Delivery", "Day", 7, 14, 7m, 14m, true, 8, 1, DateTime.Now, "::1" },
                { 9, "International Standard", "Day", 10, 21, 10m, 21m, true, 9, 1, DateTime.Now, "::1" },
                { 10, "International Express", "Day", 3, 5, 3m, 5m, true, 10, 1, DateTime.Now, "::1" },
                { 11, "Standard (2-3 Weeks)", "Week", 2, 3, 14m, 21m, true, 11, 1, DateTime.Now, "::1" },
                { 12, "Economy (3-4 Weeks)", "Week", 3, 4, 21m, 28m, true, 12, 1, DateTime.Now, "::1" },
                { 13, "Freight / Sea Shipping (4-6 Weeks)", "Week", 4, 6, 28m, 42m, true, 13, 1, DateTime.Now, "::1" },
                { 14, "Extended (6+ Weeks)", "Week", 6, null, 42m, null, true, 14, 1, DateTime.Now, "::1" }
            });

        // ImageType
        // migrationBuilder.InsertData(
        //     "ImageType",
        //     new[]
        //     {
        //         "Id", "Entity", "Name", "Slug", "Description", "IsActive",
        //         "CreatedBy", "CreatedAt", "CreatedByIp"
        //     },
        //     new object[,]
        //     {
        //         {
        //             1, "Brand", "Brand Banner", "brand-banner", "Main banner image displayed on the brand page.", true,
        //             1, DateTime.Now, "::1"
        //         },
        //         {
        //             2, "Brand", "Brand Logo", "brand-logo", "Primary logo image for the brand.", true, 1, DateTime.Now,
        //             "::1"
        //         },
        //         {
        //             3, "Category", "Category Banner", "category-banner",
        //             "Banner displayed at the top of category pages.", true, 1, DateTime.Now, "::1"
        //         },
        //         {
        //             4, "Category", "Category Thumbnail", "category-thumbnail", "Small thumbnail for category listings.",
        //             true, 1, DateTime.Now, "::1"
        //         },
        //         {
        //             5, "ProductGroup", "Group Banner", "group-banner", "Banner used for product group sections.", true,
        //             1, DateTime.Now, "::1"
        //         },
        //         {
        //             6, "Product", "Main Image", "main-image", "Primary product image shown on product details page.",
        //             true, 1, DateTime.Now, "::1"
        //         },
        //         {
        //             7, "Product", "Gallery Image", "gallery-image", "Additional product gallery image.", true, 1,
        //             DateTime.Now, "::1"
        //         },
        //         {
        //             8, "Page", "Home Page Slide 1", "home-page-slider-1",
        //             "Main slider image on homepage (first slide).", true, 1, DateTime.Now, "::1"
        //         },
        //         {
        //             9, "Page", "Home Page Slide 2", "home-page-slider-2",
        //             "Main slider image on homepage (second slide).", true, 1, DateTime.Now, "::1"
        //         },
        //         {
        //             10, "Page", "Promo Banner", "promo-banner", "Promotional banner displayed on homepage.", true, 1,
        //             DateTime.Now, "::1"
        //         }
        //     });

        // Bank
        migrationBuilder.InsertData(
            "Bank",
            new[]
            {
                "Id", "Name", "Slug", "IsActive", "SortOrder", "CreatedBy", "CreatedAt", "CreatedByIp"
            },
            new object[,]
            {
                { 1, "State Bank of India", "state-bank-of-india", true, 1, 1, DateTime.Now, "::1" },
                { 2, "HDFC Bank", "hdfc-bank", true, 2, 1, DateTime.Now, "::1" },
                { 3, "ICICI Bank", "icici-bank", true, 3, 1, DateTime.Now, "::1" },
                { 4, "Axis Bank", "axis-bank", true, 4, 1, DateTime.Now, "::1" },
                { 5, "Punjab National Bank", "punjab-national-bank", true, 5, 1, DateTime.Now, "::1" },
                { 6, "Kotak Mahindra Bank", "kotak-mahindra-bank", true, 6, 1, DateTime.Now, "::1" },
                { 7, "Bank of Baroda", "bank-of-baroda", true, 7, 1, DateTime.Now, "::1" },
                { 8, "Canara Bank", "canara-bank", true, 8, 1, DateTime.Now, "::1" },
                { 9, "Union Bank of India", "union-bank-of-india", true, 9, 1, DateTime.Now, "::1" },
                { 10, "IndusInd Bank", "indusind-bank", true, 10, 1, DateTime.Now, "::1" }
            });

        // Post Code
        migrationBuilder.InsertData(
            "PostCode",
            new[]
            {
                "Id", "Code", "IsActive", "CreatedBy", "CreatedAt", "CreatedByIp"
            },
            new object[,]
            {
                { 1, "400001", true, 1, DateTime.Now, "::1" },
                { 2, "400002", true, 1, DateTime.Now, "::1" },
                { 3, "400003", true, 1, DateTime.Now, "::1" },
                { 4, "400004", true, 1, DateTime.Now, "::1" },
                { 5, "400005", true, 1, DateTime.Now, "::1" },
                { 6, "400006", true, 1, DateTime.Now, "::1" },
                { 7, "400007", true, 1, DateTime.Now, "::1" },
                { 8, "400008", true, 1, DateTime.Now, "::1" },
                { 9, "400009", true, 1, DateTime.Now, "::1" },
                { 10, "400010", true, 1, DateTime.Now, "::1" }
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