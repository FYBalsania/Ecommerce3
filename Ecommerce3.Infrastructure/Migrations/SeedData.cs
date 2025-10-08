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
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData("AppUserRole", "UserId", "1");
        migrationBuilder.DeleteData("AppUser", "Id", "1");
        migrationBuilder.DeleteData("Role", "Id", "1");
    }
}