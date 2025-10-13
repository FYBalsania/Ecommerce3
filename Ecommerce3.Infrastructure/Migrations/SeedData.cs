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
                { 1, "Motorcycles", "motorcycles", "Motorcycles", "Motorcycles", "Motorcycles", "Motorcycles", "Motorcycles Short Description", "<p><strong>Motorcycles</strong></p>", null, "Vehicles > Motor Vehicles > Motorcycles", "1", true, 1, 1, DateTime.Now, "::1" },
                { 2, "Super Bikes", "super-bikes", "Super Bikes", "Super Bikes", "Super Bikes", "Super Bikes", "Super Bikes Short Description", "<p><strong>Super Bikes</strong></p>", 1, "Vehicles > Motor Vehicles > Motorcycles > Sport Bikes", "1.2", true, 2, 1, DateTime.Now, "::1" },
                { 3, "Electric Bikes", "electric-bikes", "Electric Bikes", "Electric Bikes", "Electric Bikes", "Electric Bikes", "Electric Bikes Short Description", "<p><strong>Electric Bikes</strong></p>", 1, "Vehicles > Motor Vehicles > Motorcycles > Electric Motorcycles", "1.3", true, 3, 1, DateTime.Now, "::1" },
                { 4, "Electronics", "electronics", "Electronics", "Electronics", "Electronics", "Electronics", "Electronics Short Description", "<p><strong>Electronics</strong></p>", null, "Electronics", "4", true, 4, 1, DateTime.Now, "::1" },
                { 5, "Laptops", "laptops", "Laptops", "Laptops", "Laptops", "Laptops", "Laptops Short Description", "<p><strong>Laptops</strong></p>", 4, "Electronics > Computers > Laptops", "4.5", true, 5, 1, DateTime.Now, "::1" },
                { 6, "Smartphones", "smartphones", "Smartphones", "Smartphones", "Smartphones", "Smartphones", "Smartphones Short Description", "<p><strong>Smartphones</strong></p>", 4, "Electronics > Communications > Telephony > Mobile Phones", "4.6", true, 6, 1, DateTime.Now, "::1" },
                { 7, "Accessories", "accessories", "Accessories", "Accessories", "Accessories", "Accessories", "Accessories Short Description", "<p><strong>Accessories</strong></p>", null, "Apparel & Accessories", "7", true, 7, 1, DateTime.Now, "::1" },
                { 8, "Helmets", "helmets", "Helmets", "Helmets", "Helmets", "Helmets", "Helmets Short Description", "<p><strong>Helmets</strong></p>", 7, "Apparel & Accessories > Clothing Accessories > Hats > Helmets", "7.8", true, 8, 1, DateTime.Now, "::1" },
                { 9, "Gaming", "gaming", "Gaming", "Gaming", "Gaming", "Gaming", "Gaming Short Description", "<p><strong>Gaming</strong></p>", null, "Video Game Consoles & Accessories", "9", true, 9, 1, DateTime.Now, "::1" },
                { 10, "Consoles", "consoles", "Consoles", "Consoles", "Consoles", "Consoles", "Consoles Short Description", "<p><strong>Consoles</strong></p>", 9, "Video Game Consoles & Accessories > Game Consoles", "9.10", true, 10, 1, DateTime.Now, "::1" }
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
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData("AppUserRole", "UserId", "1");
        migrationBuilder.DeleteData("AppUser", "Id", "1");
        migrationBuilder.DeleteData("Role", "Id", "1");
    }
}