using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce3.Infrastructure.Migrations;

public partial class SeedData : Migration
{
    private static readonly string[] roleColumns = ["Id", "Name", "NormalizedName", "ConcurrencyStamp"];
    private static readonly string[] userColumns = [
        "Id", "FirstName", "LastName", "FullName", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed",
        "PasswordHash", "SecurityStamp", "ConcurrencyStamp","PhoneNumber", "PhoneNumberConfirmed", 
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
                { 1, "KTM", "ktm", "KTM", "KTM", "KTM", "KTM", "KTM Short Description", "<p><strong>KTM</strong></p>", true, 1, 1, DateTime.Now, "::1" },
                { 2, "Facebook", "facebook", "Facebook", "Facebook", "Facebook", "Facebook", "Facebook Short Description", "<p><strong>Facebook</strong></p>", true, 2, 1, DateTime.Now, "::1" },
                { 3, "Google", "google", "Google", "Google", "Google", "Google", "Google Short Description", "<p><strong>Google</strong></p>", true, 3, 1, DateTime.Now, "::1" },
                { 4, "Apple", "apple", "Apple", "Apple", "Apple", "Apple", "Apple Short Description", "<p><strong>Apple</strong></p>", true, 4, 1, DateTime.Now, "::1" },
                { 5, "Microsoft", "microsoft", "Microsoft", "Microsoft", "Microsoft", "Microsoft", "Microsoft Short Description", "<p><strong>Microsoft</strong></p>", true, 5, 1, DateTime.Now, "::1" },
                { 6, "Tesla", "tesla", "Tesla", "Tesla", "Tesla", "Tesla", "Tesla Short Description", "<p><strong>Tesla</strong></p>", true, 6, 1, DateTime.Now, "::1" },
                { 7, "Sony", "sony", "Sony", "Sony", "Sony", "Sony", "Sony Short Description", "<p><strong>Sony</strong></p>", true, 7, 1, DateTime.Now, "::1" },
                { 8, "NVIDIA", "nvidia", "NVIDIA", "NVIDIA", "NVIDIA", "NVIDIA", "NVIDIA Short Description", "<p><strong>NVIDIA</strong></p>", true, 8, 1, DateTime.Now, "::1" },
                { 9, "Amazon", "amazon", "Amazon", "Amazon", "Amazon", "Amazon", "Amazon Short Description", "<p><strong>Amazon</strong></p>", true, 9, 1, DateTime.Now, "::1" },
                { 10, "Intel", "intel", "Intel", "Intel", "Intel", "Intel", "Intel Short Description", "<p><strong>Intel</strong></p>", true, 10, 1, DateTime.Now, "::1" }
            });

        //Page
        migrationBuilder.InsertData(
            "Page",
            new[] 
            { 
                "Id", "Discriminator", "MetaTitle", "MetaDescription", "MetaKeywords", 
                "H1", "SitemapPriority", "SitemapFrequency", "IsIndexed", 
                "Language", "Region", "SeoScore", "BrandId", "IsActive", 
                "CreatedBy", "CreatedAt", "CreatedByIp" 
            },
            new object[,]
            {
                { 1, "BrandPage", "KTM", "KTM Official Brand Page", "KTM, Bikes, Motorcycles, Racing", "KTM", 0.80, "Yearly", true, "en", "UK", 85, 1, true, 1, DateTime.Now, "::1" },
                { 2, "BrandPage", "Facebook", "Facebook Brand Information", "Facebook, Meta, Social Media, Connect", "Facebook", 0.75, "Monthly", true, "en", "US", 82, 2, true, 1, DateTime.Now, "::1" },
                { 3, "BrandPage", "Google", "Google Search Engine and Services", "Google, Search, Cloud, Gmail, Android", "Google", 0.90, "Weekly", true, "en", "US", 90, 3, true, 1, DateTime.Now, "::1" },
                { 4, "BrandPage", "Apple", "Apple Brand Page", "Apple, iPhone, MacBook, iPad", "Apple", 0.85, "Monthly", true, "en", "US", 88, 4, true, 1, DateTime.Now, "::1" },
                { 5, "BrandPage", "Microsoft", "Microsoft Products and Solutions", "Microsoft, Windows, Office, Azure", "Microsoft", 0.80, "Monthly", true, "en", "US", 87, 5, true, 1, DateTime.Now, "::1" },
                { 6, "BrandPage", "Tesla", "Tesla Electric Vehicles", "Tesla, EV, Elon Musk, Cars, Energy", "Tesla", 0.88, "Monthly", true, "en", "US", 89, 6, true, 1, DateTime.Now, "::1" },
                { 7, "BrandPage", "Sony", "Sony Electronics and Entertainment", "Sony, PlayStation, Cameras, TVs", "Sony", 0.80, "Yearly", true, "en", "JP", 83, 7, true, 1, DateTime.Now, "::1" },
                { 8, "BrandPage", "NVIDIA", "NVIDIA Graphics and AI", "NVIDIA, GPU, RTX, AI, DLSS", "NVIDIA", 0.85, "Monthly", true, "en", "US", 88, 8, true, 1, DateTime.Now, "::1" },
                { 9, "BrandPage", "Amazon", "Amazon Online Marketplace", "Amazon, eCommerce, Prime, Shopping", "Amazon", 0.90, "Weekly", true, "en", "US", 91, 9, true, 1, DateTime.Now, "::1" },
                { 10, "BrandPage", "Intel", "Intel Processors and Technology", "Intel, CPU, Chipsets, Performance", "Intel", 0.78, "Yearly", true, "en", "US", 84, 10, true, 1, DateTime.Now, "::1" }
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
                { 1, "Motorcycles", "motorcycles", "Motorcycles", "Motorcycles", "Motorcycles", "Motorcycles", "Motorcycles Short Description", "<p><strong>Motorcycles</strong></p>", null, "Vehicles > Motor Vehicles > Motorcycles", "motorcycles", true, 1, 1, DateTime.Now, "::1" },
                { 2, "Super Bikes", "super-bikes", "Super Bikes", "Super Bikes", "Super Bikes", "Super Bikes", "Super Bikes Short Description", "<p><strong>Super Bikes</strong></p>", 1, "Vehicles > Motor Vehicles > Motorcycles > Sport Bikes", "motorcycles.super-bikes", true, 2, 1, DateTime.Now, "::1" },
                { 3, "Electric Bikes", "electric-bikes", "Electric Bikes", "Electric Bikes", "Electric Bikes", "Electric Bikes", "Electric Bikes Short Description", "<p><strong>Electric Bikes</strong></p>", 1, "Vehicles > Motor Vehicles > Motorcycles > Electric Motorcycles", "motorcycles.electric-bikes", true, 3, 1, DateTime.Now, "::1" },
                { 4, "Electronics", "electronics", "Electronics", "Electronics", "Electronics", "Electronics", "Electronics Short Description", "<p><strong>Electronics</strong></p>", null, "Electronics", "electronics", true, 4, 1, DateTime.Now, "::1" },
                { 5, "Laptops", "laptops", "Laptops", "Laptops", "Laptops", "Laptops", "Laptops Short Description", "<p><strong>Laptops</strong></p>", 4, "Electronics > Computers > Laptops", "electronics.laptops", true, 5, 1, DateTime.Now, "::1" },
                { 6, "Smartphones", "smartphones", "Smartphones", "Smartphones", "Smartphones", "Smartphones", "Smartphones Short Description", "<p><strong>Smartphones</strong></p>", 4, "Electronics > Communications > Telephony > Mobile Phones", "electronics.smartphones", true, 6, 1, DateTime.Now, "::1" },
                { 7, "Accessories", "accessories", "Accessories", "Accessories", "Accessories", "Accessories", "Accessories Short Description", "<p><strong>Accessories</strong></p>", null, "Apparel & Accessories", "accessories", true, 7, 1, DateTime.Now, "::1" },
                { 8, "Helmets", "helmets", "Helmets", "Helmets", "Helmets", "Helmets", "Helmets Short Description", "<p><strong>Helmets</strong></p>", 7, "Apparel & Accessories > Clothing Accessories > Hats > Helmets", "accessories.helmets", true, 8, 1, DateTime.Now, "::1" },
                { 9, "Gaming", "gaming", "Gaming", "Gaming", "Gaming", "Gaming", "Gaming Short Description", "<p><strong>Gaming</strong></p>", null, "Video Game Consoles & Accessories", "gaming", true, 9, 1, DateTime.Now, "::1" },
                { 10, "Consoles", "consoles", "Consoles", "Consoles", "Consoles", "Consoles", "Consoles Short Description", "<p><strong>Consoles</strong></p>", 9, "Video Game Consoles & Accessories > Game Consoles", "gaming.consoles", true, 10, 1, DateTime.Now, "::1" }
            });
        
        migrationBuilder.InsertData(
            "Page",
            new[] 
            { 
                "Id", "Discriminator", "MetaTitle", "MetaDescription", "MetaKeywords", 
                "H1", "SitemapPriority", "SitemapFrequency", "IsIndexed", 
                "Language", "Region", "SeoScore", "CategoryId", "IsActive", 
                "CreatedBy", "CreatedAt", "CreatedByIp" 
            },
            new object[,]
            {
                { 11, "CategoryPage", "Motorcycles", "All types of motorcycles and related products", "Motorcycles, Bikes, Racing", "Motorcycles", 0.85, "Monthly", true, "en", "UK", 86, 1, true, 1, DateTime.Now, "::1" },
                { 12, "CategoryPage", "Super Bikes", "Premium high-speed motorcycles", "Super Bikes, Racing, Speed", "Super Bikes", 0.88, "Monthly", true, "en", "UK", 88, 2, true, 1, DateTime.Now, "::1" },
                { 13, "CategoryPage", "Electric Bikes", "Eco-friendly electric motorcycles and scooters", "Electric Bikes, EV, Green", "Electric Bikes", 0.90, "Yearly", true, "en", "UK", 90, 3, true, 1, DateTime.Now, "::1" },
                { 14, "CategoryPage", "Electronics", "Electronics and smart devices", "Electronics, Gadgets, Tech", "Electronics", 0.82, "Weekly", true, "en", "US", 83, 4, true, 1, DateTime.Now, "::1" },
                { 15, "CategoryPage", "Laptops", "Find the best laptops for work and gaming", "Laptops, Computers, Technology", "Laptops", 0.87, "Weekly", true, "en", "US", 87, 5, true, 1, DateTime.Now, "::1" },
                { 16, "CategoryPage", "Smartphones", "Latest smartphones and mobile devices", "Smartphones, Mobile, Android, iPhone", "Smartphones", 0.90, "Weekly", true, "en", "US", 91, 6, true, 1, DateTime.Now, "::1" },
                { 17, "CategoryPage", "Accessories", "Apparel and fashion accessories", "Accessories, Apparel, Fashion", "Accessories", 0.80, "Yearly", true, "en", "US", 82, 7, true, 1, DateTime.Now, "::1" },
                { 18, "CategoryPage", "Helmets", "Protective helmets for bikers and sports", "Helmets, Safety, Motorcycles", "Helmets", 0.84, "Monthly", true, "en", "UK", 85, 8, true, 1, DateTime.Now, "::1" },
                { 19, "CategoryPage", "Gaming", "All about gaming gear and consoles", "Gaming, Consoles, Accessories", "Gaming", 0.88, "Weekly", true, "en", "US", 89, 9, true, 1, DateTime.Now, "::1" },
                { 20, "CategoryPage", "Consoles", "Next-gen gaming consoles and devices", "Consoles, PlayStation, Xbox, Nintendo", "Consoles", 0.92, "Weekly", true, "en", "US", 93, 10, true, 1, DateTime.Now, "::1" }
            });
        
        // ProductAttribute
        migrationBuilder.InsertData(
            "ProductAttribute",
            new[]
            { 
                "Id", "Name", "Slug", "Display", "Breadcrumb", "DataType",
                "SortOrder", "CreatedBy", "CreatedAt", "CreatedByIp" 
            },
            new object[,]
            {
                { 1, "Color", "color", "Color", "Color", "Text", 1, 1, DateTime.Now, "::1" },
                { 2, "Storage", "storage", "Storage", "Storage", "Text", 2, 1, DateTime.Now, "::1" },
                { 3, "RAM", "ram", "RAM", "RAM", "Text", 3, 1, DateTime.Now, "::1" },
                { 4, "Screen Size", "screen-size", "Screen Size", "Screen Size", "Text", 4, 1, DateTime.Now, "::1" },
                { 5, "Processor", "processor", "Processor", "Processor", "Text", 5, 1, DateTime.Now, "::1" },
                { 6, "Battery Capacity", "battery-capacity", "Battery Capacity", "Battery Capacity", "Text", 6, 1, DateTime.Now, "::1" },
                { 7, "Material", "material", "Material", "Material", "Text", 7, 1, DateTime.Now, "::1" },
                { 8, "Weight", "weight", "Weight", "Weight", "Text", 8, 1, DateTime.Now, "::1" },
                { 9, "Warranty", "warranty", "Warranty", "Warranty", "Text", 9, 1, DateTime.Now, "::1" },
                { 10, "Connectivity", "connectivity", "Connectivity", "Connectivity", "Text", 10, 1, DateTime.Now, "::1" }
            });
        
        // ProductGroup
        migrationBuilder.InsertData(
            "ProductGroup",
            new[]
            {
                "Id", "Name", "Slug", "Display", "Breadcrumb", "AnchorText", "AnchorTitle",
                "ShortDescription", "FullDescription", "IsActive", "SortOrder",
                "CreatedBy", "CreatedAt", "CreatedByIp"
            },
            new object[,]
            {
                { 1, "iPhone 17", "iphone-17", "iPhone 17", "iPhone 17", "iPhone 17", "iPhone 17", "Premium Apple smartphone series", "<p><strong>Apple iPhone</strong> delivers high performance, elegant design, and cutting-edge cameras.</p>", true, 1, 1, DateTime.Now, "::1" },
                { 2, "Duke 200 2015", "duke-200-2015", "Duke 200 2015", "Duke 200 2015", "Duke 200 2015", "Duke 200 2015", "KTM’s signature performance bike series", "<p><strong>KTM Duke</strong> – lightweight, powerful, and built for speed.</p>", true, 2, 1, DateTime.Now, "::1" },
                { 3, "Galaxy S25", "galaxy-s25", "Galaxy S25", "Galaxy S25", "Galaxy S25", "Galaxy S25", "Samsung Galaxy smartphones known for innovation", "<p><strong>Samsung Galaxy</strong> devices redefine Android excellence.</p>", true, 3, 1, DateTime.Now, "::1" },
                { 4, "PlayStation 5", "playstation-5", "PlayStation 5", "PlayStation 5", "PlayStation 5", "PlayStation 5", "Sony’s legendary gaming console line", "<p><strong>Sony PlayStation</strong> – immersive gaming for all generations.</p>", true, 4, 1, DateTime.Now, "::1" },
                { 5, "ThinkPad", "thinkpad", "ThinkPad", "ThinkPad", "ThinkPad", "ThinkPad", "Lenovo’s durable and business-class laptops", "<p><strong>Lenovo ThinkPad</strong> – trusted productivity companion.</p>", true, 5, 1, DateTime.Now, "::1" },
                { 6, "Surface", "surface", "Surface", "Surface", "Surface", "Surface", "Microsoft Surface tablets and laptops", "<p><strong>Microsoft Surface</strong> combines power, portability, and style.</p>", true, 6, 1, DateTime.Now, "::1" },
                { 7, "Model S", "model-s", "Model S", "Model S", "Model S", "Model S", "Tesla’s premium electric sedan", "<p><strong>Tesla Model S</strong> – the benchmark for electric performance.</p>", true, 7, 1, DateTime.Now, "::1" },
                { 8, "Legion", "legion", "Legion", "Legion", "Legion", "Legion", "Lenovo Legion gaming laptops and desktops", "<p><strong>Lenovo Legion</strong> – for gamers who demand power.</p>", true, 8, 1, DateTime.Now, "::1" },
                { 9, "ROG", "rog", "ROG", "ROG", "ROG", "ROG", "ASUS Republic of Gamers product line", "<p><strong>ASUS ROG</strong> – elite gaming hardware and innovation.</p>", true, 9, 1, DateTime.Now, "::1" },
                { 10, "Echo", "echo", "Echo", "Echo", "Echo", "Echo", "Amazon Echo smart devices with Alexa", "<p><strong>Amazon Echo</strong> – smart living with voice control.</p>", true, 10, 1, DateTime.Now, "::1" }
            });

        // Page
        migrationBuilder.InsertData(
            "Page",
            new[]
            {
                "Id", "Discriminator", "MetaTitle", "MetaDescription", "MetaKeywords",
                "H1", "SitemapPriority", "SitemapFrequency", "IsIndexed",
                "Language", "Region", "SeoScore", "ProductGroupId", "IsActive",
                "CreatedBy", "CreatedAt", "CreatedByIp"
            },
            new object[,]
            {
                { 21, "ProductGroupPage", "iPhone", "Apple iPhone Series Overview", "iPhone, Apple, Smartphones, iOS", "iPhone", 0.90, "Monthly", true, "en", "US", 92, 1, true, 1, DateTime.Now, "::1" },
                { 22, "ProductGroupPage", "Duke", "KTM Duke Motorcycles", "KTM, Duke, Motorcycles, Bikes", "Duke", 0.85, "Monthly", true, "en", "IN", 88, 2, true, 1, DateTime.Now, "::1" },
                { 23, "ProductGroupPage", "Galaxy", "Samsung Galaxy Smartphones", "Samsung, Galaxy, Android, Phones", "Galaxy", 0.88, "Monthly", true, "en", "US", 89, 3, true, 1, DateTime.Now, "::1" },
                { 24, "ProductGroupPage", "PlayStation", "Sony PlayStation Consoles", "PlayStation, PS5, Gaming, Sony", "PlayStation", 0.80, "Monthly", true, "en", "JP", 85, 4, true, 1, DateTime.Now, "::1" },
                { 25, "ProductGroupPage", "ThinkPad", "Lenovo ThinkPad Laptops", "ThinkPad, Lenovo, Laptops, Business", "ThinkPad", 0.78, "Yearly", true, "en", "US", 83, 5, true, 1, DateTime.Now, "::1" },
                { 26, "ProductGroupPage", "Surface", "Microsoft Surface Devices", "Surface, Microsoft, Laptop, Tablet", "Surface", 0.82, "Monthly", true, "en", "US", 86, 6, true, 1, DateTime.Now, "::1" },
                { 27, "ProductGroupPage", "Model S", "Tesla Model S Electric Car", "Tesla, Model S, EV, Electric Car", "Model S", 0.90, "Monthly", true, "en", "US", 91, 7, true, 1, DateTime.Now, "::1" },
                { 28, "ProductGroupPage", "Legion", "Lenovo Legion Gaming Series", "Legion, Lenovo, Gaming, Laptop", "Legion", 0.84, "Monthly", true, "en", "US", 87, 8, true, 1, DateTime.Now, "::1" },
                { 29, "ProductGroupPage", "ROG", "ASUS ROG Gaming Products", "ASUS, ROG, Gaming, Laptop, PC", "ROG", 0.86, "Monthly", true, "en", "US", 88, 9, true, 1, DateTime.Now, "::1" },
                { 30, "ProductGroupPage", "Echo", "Amazon Echo Smart Devices", "Amazon, Echo, Alexa, Smart Home", "Echo", 0.83, "Weekly", true, "en", "US", 85, 10, true, 1, DateTime.Now, "::1" }
            });
        
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
        migrationBuilder.InsertData(
            "ImageType",
            new[]
            {
                "Id", "Entity", "Name", "Description", "IsActive",
                "CreatedBy", "CreatedAt", "CreatedByIp"
            },
            new object[,]
            {
                { 1, "Brand", "Brand Banner", "Main banner image displayed on the brand page.", true, 1, DateTime.Now, "::1" },
                { 2, "Brand", "Brand Logo", "Primary logo image for the brand.", true, 1, DateTime.Now, "::1" },
                { 3, "Category", "Category Banner", "Banner displayed at the top of category pages.", true, 1, DateTime.Now, "::1" },
                { 4, "Category", "Category Thumbnail", "Small thumbnail for category listings.", true, 1, DateTime.Now, "::1" },
                { 5, "ProductGroup", "Group Banner", "Banner used for product group sections.", true, 1, DateTime.Now, "::1" },
                { 6, "Product", "Main Image", "Primary product image shown on product details page.", true, 1, DateTime.Now, "::1" },
                { 7, "Product", "Gallery Image", "Additional product gallery image.", true, 1, DateTime.Now, "::1" },
                { 8, "Page", "Home Page Slide 1", "Main slider image on homepage (first slide).", true, 1, DateTime.Now, "::1" },
                { 9, "Page", "Home Page Slide 2", "Main slider image on homepage (second slide).", true, 1, DateTime.Now, "::1" },
                { 10, "Page", "Promo Banner", "Promotional banner displayed on homepage.", true, 1, DateTime.Now, "::1" }
            });
        
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