using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ecommerce3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:ltree", ",,");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    LastName = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    CompanyName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailAddress = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    Password = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordResetToken = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    PasswordResetTokenExpiry = table.Column<DateTime>(type: "timestamp", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    History = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserClaim_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AppUserLogin_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserToken",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AppUserToken_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Display = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Breadcrumb = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    AnchorText = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    AnchorTitle = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ShortDescription = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    FullDescription = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brand_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Brand_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Brand_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Display = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Breadcrumb = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    AnchorText = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    AnchorTitle = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    GoogleCategory = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    MetaTitle = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    MetaDescription = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    MetaKeywords = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    H1 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    Path = table.Column<string>(type: "ltree", nullable: false),
                    ShortDescription = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    FullDescription = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryWindow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Unit = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    MinValue = table.Column<int>(type: "integer", nullable: false),
                    MaxValue = table.Column<int>(type: "integer", nullable: true),
                    NormalizedMinDays = table.Column<decimal>(type: "numeric(18,1)", nullable: false),
                    NormalizedMaxDays = table.Column<decimal>(type: "numeric(18,1)", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryWindow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryWindow_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryWindow_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryWindow_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Scope = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    Code = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    StartAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    EndAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    MinOrderValue = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    Type = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    Percent = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    MaxDiscountAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discount_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Discount_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Discount_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImageType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Entity = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Type = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageType_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageType_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageType_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Display = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Breadcrumb = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    DataType = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Display = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Breadcrumb = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    AnchorText = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    AnchorTitle = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    MetaTitle = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    MetaDescription = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    MetaKeywords = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    H1 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    ShortDescription = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    FullDescription = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductGroup_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductGroup_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductGroup_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: true),
                    SessionId = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    SubTotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cart_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cart_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cart_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    FullName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    CompanyName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    AddressLine1 = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    AddressLine2 = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    City = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    StateOrProvince = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    PostalCode = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    Landmark = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    History = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAddress_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRole",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AppUserRole_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductAttributeId = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    Value = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Display = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Breadcrumb = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    NumberValue = table.Column<decimal>(type: "numeric(18,3)", nullable: true),
                    BooleanValue = table.Column<bool>(type: "boolean", nullable: true),
                    DateOnlyValue = table.Column<DateOnly>(type: "date", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    HexCode = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    ColourFamily = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    ColourFamilyHexCode = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValue_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValue_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValue_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValue_ProductAttribute_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SKU = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    GTIN = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    MPN = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    MFC = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    EAN = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    UPC = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Display = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Breadcrumb = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    AnchorText = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    AnchorTitle = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    MetaTitle = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    MetaDescription = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    MetaKeywords = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    H1 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    BrandId = table.Column<int>(type: "integer", nullable: true),
                    ProductGroupId = table.Column<int>(type: "integer", nullable: true),
                    ShortDescription = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    FullDescription = table.Column<string>(type: "text", nullable: true),
                    AllowReviews = table.Column<bool>(type: "boolean", nullable: false),
                    AverageRating = table.Column<int>(type: "integer", nullable: false),
                    TotalReviews = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    OldPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    CostPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    MinStock = table.Column<int>(type: "integer", nullable: false),
                    ShowAvailability = table.Column<bool>(type: "boolean", nullable: false),
                    FreeShipping = table.Column<bool>(type: "boolean", nullable: false),
                    AdditionalShippingCharge = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    WeightKgs = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DeliveryWindowId = table.Column<int>(type: "integer", nullable: false),
                    MinOrderQuantity = table.Column<int>(type: "integer", nullable: false),
                    MaxOrderQuantity = table.Column<int>(type: "integer", nullable: true),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: false),
                    IsNew = table.Column<bool>(type: "boolean", nullable: false),
                    IsBestSeller = table.Column<bool>(type: "boolean", nullable: false),
                    IsReturnable = table.Column<bool>(type: "boolean", nullable: false),
                    ReturnPolicy = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    RedirectUrl = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    Facets = table.Column<string>(type: "jsonb", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_DeliveryWindow_DeliveryWindowId",
                        column: x => x.DeliveryWindowId,
                        principalTable: "DeliveryWindow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_ProductGroup_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    Dated = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CartId = table.Column<int>(type: "integer", nullable: true),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    BillingCustomerAddressId = table.Column<int>(type: "integer", nullable: false),
                    ShippingCustomerAddressId = table.Column<int>(type: "integer", nullable: false),
                    SubTotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ShippingCharge = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Status = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    PaymentStatus = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    ShippingStatus = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    CreatedByUserId = table.Column<int>(type: "integer", nullable: true),
                    CreatedByCustomerId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedByCustomerId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    BillingAddressReference = table.Column<string>(type: "jsonb", nullable: false),
                    CustomerReference = table.Column<string>(type: "jsonb", nullable: false),
                    ShippingAddressReference = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrder_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrder_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrder_AspNetUsers_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrder_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrder_CustomerAddress_BillingCustomerAddressId",
                        column: x => x.BillingCustomerAddressId,
                        principalTable: "CustomerAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrder_CustomerAddress_ShippingCustomerAddressId",
                        column: x => x.ShippingCustomerAddressId,
                        principalTable: "CustomerAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrder_Customer_CreatedByCustomerId",
                        column: x => x.CreatedByCustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrder_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrder_Customer_UpdatedByCustomerId",
                        column: x => x.UpdatedByCustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroupProductAttribute",
                columns: table => new
                {
                    ProductGroupId = table.Column<int>(type: "integer", nullable: false),
                    ProductAttributeId = table.Column<int>(type: "integer", nullable: false),
                    ProductAttributeSortOrder = table.Column<int>(type: "integer", nullable: false),
                    ProductAttributeValueId = table.Column<int>(type: "integer", nullable: false),
                    ProductAttributeValueSortOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroupProductAttribute", x => new { x.ProductGroupId, x.ProductAttributeId, x.ProductAttributeValueId, x.DeletedAt });
                    table.ForeignKey(
                        name: "FK_ProductGroupProductAttribute_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductGroupProductAttribute_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductGroupProductAttribute_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductGroupProductAttribute_ProductAttributeValue_ProductA~",
                        column: x => x.ProductAttributeValueId,
                        principalTable: "ProductAttributeValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductGroupProductAttribute_ProductAttribute_ProductAttrib~",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductGroupProductAttribute_ProductGroup_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CartId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    SubTotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartLine_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartLine_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartLine_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartLine_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountProduct",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountProduct", x => new { x.DiscountId, x.ProductId, x.DeletedAt });
                    table.ForeignKey(
                        name: "FK_DiscountProduct_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountProduct_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountProduct_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountProduct_Discount_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Discount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KVPListItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Discriminator = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    Type = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    ProductId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KVPListItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KVPListItem_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KVPListItem_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KVPListItem_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KVPListItem_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KVPListItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Discriminator = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    Path = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    MetaTitle = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    MetaDescription = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true),
                    MetaKeywords = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    MetaRobots = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    CanonicalUrl = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true),
                    OgTitle = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    OgDescription = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true),
                    OgImageUrl = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true),
                    OgType = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    TwitterCard = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    ContentHtml = table.Column<string>(type: "text", nullable: true),
                    H1 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Summary = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    SchemaJsonLd = table.Column<string>(type: "text", nullable: true),
                    BreadcrumbsJson = table.Column<string>(type: "text", nullable: true),
                    HreflangMapJson = table.Column<string>(type: "text", nullable: true),
                    SitemapPriority = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    SitemapFrequency = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    RedirectFromJson = table.Column<string>(type: "text", nullable: true),
                    IsIndexed = table.Column<bool>(type: "boolean", nullable: false),
                    HeaderScripts = table.Column<string>(type: "text", nullable: true),
                    FooterScripts = table.Column<string>(type: "text", nullable: true),
                    Language = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    Region = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    SeoScore = table.Column<int>(type: "integer", nullable: true),
                    BrandId = table.Column<int>(type: "integer", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    ProductId = table.Column<int>(type: "integer", nullable: true),
                    ProductGroupId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Page_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Page_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Page_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Page_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Page_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Page_ProductGroup_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Page_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory_ProductId_CategoryId_DeletedAt", x => new { x.ProductId, x.CategoryId, x.DeletedAt });
                    table.ForeignKey(
                        name: "FK_ProductCategory_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategory_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategory_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductProductAttribute",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    ProductAttributeId = table.Column<int>(type: "integer", nullable: false),
                    ProductAttributeSortOrder = table.Column<int>(type: "integer", nullable: false),
                    ProductAttributeValueId = table.Column<int>(type: "integer", nullable: false),
                    ProductAttributeValueSortOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductAttribute", x => new { x.ProductId, x.ProductAttributeId, x.ProductAttributeValueId, x.DeletedAt });
                    table.ForeignKey(
                        name: "FK_ProductProductAttribute_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductProductAttribute_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductProductAttribute_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductProductAttribute_ProductAttributeValue_ProductAttrib~",
                        column: x => x.ProductAttributeValueId,
                        principalTable: "ProductAttributeValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductAttribute_ProductAttribute_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductAttribute_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductQnA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Question = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: true),
                    AnsweredBy = table.Column<int>(type: "integer", nullable: true),
                    AnsweredOn = table.Column<DateTime>(type: "timestamp", nullable: true),
                    AnswererIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    Approver = table.Column<int>(type: "integer", nullable: true),
                    ApprovedOn = table.Column<DateTime>(type: "timestamp", nullable: true),
                    ApproverIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQnA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductQnA_AspNetUsers_AnsweredBy",
                        column: x => x.AnsweredBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductQnA_AspNetUsers_Approver",
                        column: x => x.Approver,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductQnA_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductQnA_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductQnA_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductQnA_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Review = table.Column<string>(type: "varchar(18432)", maxLength: 18432, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    Approver = table.Column<int>(type: "integer", nullable: true),
                    ApprovedOn = table.Column<DateTime>(type: "timestamp", nullable: true),
                    ApproverIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReview_AspNetUsers_Approver",
                        column: x => x.Approver,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductReview_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductReview_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductReview_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductReview_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TextListItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Discriminator = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextListItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextListItem_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TextListItem_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TextListItem_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TextListItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SalesOrderId = table.Column<int>(type: "integer", nullable: false),
                    CartLineId = table.Column<int>(type: "integer", nullable: true),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedByCustomerId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedByUserId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedByCustomerId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    ProductReference = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_AspNetUsers_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_CartLine_CartLineId",
                        column: x => x.CartLineId,
                        principalTable: "CartLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_Customer_CreatedByCustomerId",
                        column: x => x.CreatedByCustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_Customer_UpdatedByCustomerId",
                        column: x => x.UpdatedByCustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OgFileName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    FileName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    FileExtension = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    ImageTypeId = table.Column<int>(type: "integer", nullable: false),
                    Size = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    AltText = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    Title = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    Loading = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    Link = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    LinkTarget = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    BrandId = table.Column<int>(type: "integer", nullable: true),
                    ProductId = table.Column<int>(type: "integer", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    PageId = table.Column<int>(type: "integer", nullable: true),
                    ProductGroupId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UpdatedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedByIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_ImageType_ImageTypeId",
                        column: x => x.ImageTypeId,
                        principalTable: "ImageType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_Page_PageId",
                        column: x => x.PageId,
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_ProductGroup_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserClaim_UserId",
                table: "AppUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLogin_UserId",
                table: "AppUserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRole_RoleId",
                table: "AppUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_CreatedAt",
                table: "Brand",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_CreatedBy",
                table: "Brand",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_DeletedAt",
                table: "Brand",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_DeletedBy",
                table: "Brand",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_IsActive",
                table: "Brand",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_SortOrder",
                table: "Brand",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_UpdatedBy",
                table: "Brand",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "UK_Brand_Name",
                table: "Brand",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Brand_Slug",
                table: "Brand",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CreatedAt",
                table: "Cart",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CreatedBy",
                table: "Cart",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CustomerId",
                table: "Cart",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_DeletedAt",
                table: "Cart",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_DeletedBy",
                table: "Cart",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_SessionId",
                table: "Cart",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_Total",
                table: "Cart",
                column: "Total");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UpdatedBy",
                table: "Cart",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_CartId",
                table: "CartLine",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_CreatedAt",
                table: "CartLine",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_CreatedBy",
                table: "CartLine",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_DeletedAt",
                table: "CartLine",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_DeletedBy",
                table: "CartLine",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_ProductId",
                table: "CartLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_Total",
                table: "CartLine",
                column: "Total");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_UpdatedBy",
                table: "CartLine",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CreatedAt",
                table: "Category",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CreatedBy",
                table: "Category",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Category_DeletedAt",
                table: "Category",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Category_DeletedBy",
                table: "Category",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Category_IsActive",
                table: "Category",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                table: "Category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_SortOrder",
                table: "Category",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Category_UpdatedBy",
                table: "Category",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "UK_Category_Name",
                table: "Category",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Category_Slug",
                table: "Category",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CompanyName",
                table: "Customer",
                column: "CompanyName");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CreatedAt",
                table: "Customer",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DeletedAt",
                table: "Customer",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_FirstName",
                table: "Customer",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_LastName",
                table: "Customer",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_PasswordResetToken",
                table: "Customer",
                column: "PasswordResetToken");

            migrationBuilder.CreateIndex(
                name: "UK_Customer_EmailAddress",
                table: "Customer",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Customer_PhoneNumber",
                table: "Customer",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_City",
                table: "CustomerAddress",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_CreatedAt",
                table: "CustomerAddress",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_CustomerId",
                table: "CustomerAddress",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_DeletedAt",
                table: "CustomerAddress",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_PostalCode",
                table: "CustomerAddress",
                column: "PostalCode");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_StateOrProvince",
                table: "CustomerAddress",
                column: "StateOrProvince");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryWindow_CreatedAt",
                table: "DeliveryWindow",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryWindow_CreatedBy",
                table: "DeliveryWindow",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryWindow_DeletedAt",
                table: "DeliveryWindow",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryWindow_DeletedBy",
                table: "DeliveryWindow",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryWindow_Name_SortOrder_IsActive",
                table: "DeliveryWindow",
                columns: new[] { "Name", "SortOrder", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryWindow_UpdatedBy",
                table: "DeliveryWindow",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "UK_DeliveryWindow_Name",
                table: "DeliveryWindow",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discount_CreatedAt",
                table: "Discount",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_CreatedBy",
                table: "Discount",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_DeletedAt",
                table: "Discount",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_DeletedBy",
                table: "Discount",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_IsActive",
                table: "Discount",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_Scope",
                table: "Discount",
                column: "Scope");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_StartAt_EndAt",
                table: "Discount",
                columns: new[] { "StartAt", "EndAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Discount_UpdatedBy",
                table: "Discount",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "UK_Discount_Code",
                table: "Discount",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountProduct_CreatedBy",
                table: "DiscountProduct",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountProduct_DeletedBy",
                table: "DiscountProduct",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountProduct_ProductId",
                table: "DiscountProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_BrandId_SortOrder",
                table: "Image",
                columns: new[] { "BrandId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_CategoryId_SortOrder",
                table: "Image",
                columns: new[] { "CategoryId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_CreatedAt",
                table: "Image",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Image_CreatedBy",
                table: "Image",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Image_DeletedAt",
                table: "Image",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Image_DeletedBy",
                table: "Image",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Image_FileName",
                table: "Image",
                column: "FileName");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ImageTypeId",
                table: "Image",
                column: "ImageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_PageId_SortOrder",
                table: "Image",
                columns: new[] { "PageId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_ProductGroupId_SortOrder",
                table: "Image",
                columns: new[] { "ProductGroupId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_ProductId_SortOrder",
                table: "Image",
                columns: new[] { "ProductId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_UpdatedBy",
                table: "Image",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ImageType_CreatedAt",
                table: "ImageType",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ImageType_CreatedBy",
                table: "ImageType",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ImageType_DeletedAt",
                table: "ImageType",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ImageType_DeletedBy",
                table: "ImageType",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ImageType_Entity_Type",
                table: "ImageType",
                columns: new[] { "Entity", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_ImageType_IsActive",
                table: "ImageType",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ImageType_UpdatedBy",
                table: "ImageType",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryKVPListItem_CategoryId_Type",
                table: "KVPListItem",
                columns: new[] { "CategoryId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_KVPListItem_CreatedBy",
                table: "KVPListItem",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_KVPListItem_DeletedAt",
                table: "KVPListItem",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_KVPListItem_DeletedBy",
                table: "KVPListItem",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_KVPListItem_Discriminator",
                table: "KVPListItem",
                column: "Discriminator");

            migrationBuilder.CreateIndex(
                name: "IX_KVPListItem_UpdatedBy",
                table: "KVPListItem",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductKVPListItem_ProductId_Type",
                table: "KVPListItem",
                columns: new[] { "ProductId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Page_BrandId",
                table: "Page",
                column: "BrandId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Page_CategoryId",
                table: "Page",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Page_CreatedAt",
                table: "Page",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Page_CreatedBy",
                table: "Page",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Page_DeletedAt",
                table: "Page",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Page_DeletedBy",
                table: "Page",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Page_IsActive",
                table: "Page",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Page_IsIndexed",
                table: "Page",
                column: "IsIndexed");

            migrationBuilder.CreateIndex(
                name: "IX_Page_ProductGroupId",
                table: "Page",
                column: "ProductGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Page_ProductId",
                table: "Page",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Page_UpdatedBy",
                table: "Page",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "UK_Page_BrandId_CategoryId_DeletedAt",
                table: "Page",
                columns: new[] { "BrandId", "CategoryId", "DeletedAt" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Page_Path",
                table: "Page",
                column: "Path",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_ProductGroupPage_ProductGroupId_DeletedAt",
                table: "Page",
                columns: new[] { "ProductGroupId", "DeletedAt" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_ProductPage_ProductId_DeletedAt",
                table: "Page",
                columns: new[] { "ProductId", "DeletedAt" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                table: "Product",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CreatedAt",
                table: "Product",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CreatedBy",
                table: "Product",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeletedAt",
                table: "Product",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeletedBy",
                table: "Product",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeliveryWindowId",
                table: "Product",
                column: "DeliveryWindowId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Facets",
                table: "Product",
                column: "Facets")
                .Annotation("Npgsql:IndexMethod", "gin");

            migrationBuilder.CreateIndex(
                name: "IX_Product_IsBestSeller",
                table: "Product",
                column: "IsBestSeller");

            migrationBuilder.CreateIndex(
                name: "IX_Product_IsFeatured",
                table: "Product",
                column: "IsFeatured");

            migrationBuilder.CreateIndex(
                name: "IX_Product_IsNew",
                table: "Product",
                column: "IsNew");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductGroupId",
                table: "Product",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SortOrder",
                table: "Product",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Status",
                table: "Product",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UpdatedBy",
                table: "Product",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "UK_Product_EAN",
                table: "Product",
                column: "EAN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Product_GTIN",
                table: "Product",
                column: "GTIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Product_MFC",
                table: "Product",
                column: "MFC",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Product_MPN",
                table: "Product",
                column: "MPN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Product_Name",
                table: "Product",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Product_SKU",
                table: "Product",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Product_Slug",
                table: "Product",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Product_UPC",
                table: "Product",
                column: "UPC",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_CreatedAt",
                table: "ProductAttribute",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_CreatedBy",
                table: "ProductAttribute",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_DeletedAt",
                table: "ProductAttribute",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_DeletedBy",
                table: "ProductAttribute",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_SortOrder",
                table: "ProductAttribute",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_UpdatedBy",
                table: "ProductAttribute",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "UK_ProductAttribute_Name",
                table: "ProductAttribute",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_ProductAttribute_Slug",
                table: "ProductAttribute",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_CreatedBy",
                table: "ProductAttributeValue",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_DeletedBy",
                table: "ProductAttributeValue",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_Discriminator",
                table: "ProductAttributeValue",
                column: "Discriminator");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_ProductAttributeId_SortOrder_Value",
                table: "ProductAttributeValue",
                columns: new[] { "ProductAttributeId", "SortOrder", "Value" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_Slug",
                table: "ProductAttributeValue",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_UpdatedBy",
                table: "ProductAttributeValue",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "UK_ProductAttributeValue_ProductAttributeId_Value",
                table: "ProductAttributeValue",
                columns: new[] { "ProductAttributeId", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "ProductCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CreatedAt",
                table: "ProductCategory",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CreatedBy",
                table: "ProductCategory",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_DeletedAt",
                table: "ProductCategory",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_DeletedBy",
                table: "ProductCategory",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_IsPrimary",
                table: "ProductCategory",
                column: "IsPrimary");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_UpdatedBy",
                table: "ProductCategory",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_CreatedAt",
                table: "ProductGroup",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_CreatedBy",
                table: "ProductGroup",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_DeletedAt",
                table: "ProductGroup",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_DeletedBy",
                table: "ProductGroup",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_IsActive",
                table: "ProductGroup",
                column: "IsActive",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_SortOrder",
                table: "ProductGroup",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_UpdatedBy",
                table: "ProductGroup",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "UK_ProductGroup_Name",
                table: "ProductGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_ProductGroup_Slug",
                table: "ProductGroup",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroupProductAttribute_CreatedAt",
                table: "ProductGroupProductAttribute",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroupProductAttribute_CreatedBy",
                table: "ProductGroupProductAttribute",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroupProductAttribute_DeletedAt",
                table: "ProductGroupProductAttribute",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroupProductAttribute_DeletedBy",
                table: "ProductGroupProductAttribute",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroupProductAttribute_ProductAttributeId",
                table: "ProductGroupProductAttribute",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroupProductAttribute_ProductAttributeValueId",
                table: "ProductGroupProductAttribute",
                column: "ProductAttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroupProductAttribute_UpdatedBy",
                table: "ProductGroupProductAttribute",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductAttribute_CreatedBy",
                table: "ProductProductAttribute",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductAttribute_DeletedAt",
                table: "ProductProductAttribute",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductAttribute_DeletedBy",
                table: "ProductProductAttribute",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductAttribute_ProductAttributeId",
                table: "ProductProductAttribute",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductAttribute_ProductAttributeValueId",
                table: "ProductProductAttribute",
                column: "ProductAttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductAttribute_UpdatedBy",
                table: "ProductProductAttribute",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQnA_AnsweredBy",
                table: "ProductQnA",
                column: "AnsweredBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQnA_AnsweredOn",
                table: "ProductQnA",
                column: "AnsweredOn");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQnA_ApprovedOn",
                table: "ProductQnA",
                column: "ApprovedOn");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQnA_Approver",
                table: "ProductQnA",
                column: "Approver");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQnA_CreatedAt",
                table: "ProductQnA",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQnA_CreatedBy",
                table: "ProductQnA",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQnA_DeletedAt",
                table: "ProductQnA",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQnA_DeletedBy",
                table: "ProductQnA",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQnA_ProductId",
                table: "ProductQnA",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQnA_SortOrder",
                table: "ProductQnA",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQnA_UpdatedBy",
                table: "ProductQnA",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_ApprovedOn",
                table: "ProductReview",
                column: "ApprovedOn");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_Approver",
                table: "ProductReview",
                column: "Approver");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_CreatedAt",
                table: "ProductReview",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_CreatedBy",
                table: "ProductReview",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_DeletedAt",
                table: "ProductReview",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_DeletedBy",
                table: "ProductReview",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_ProductId",
                table: "ProductReview",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_SortOrder",
                table: "ProductReview",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_UpdatedBy",
                table: "ProductReview",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_BillingCustomerAddressId",
                table: "SalesOrder",
                column: "BillingCustomerAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_CartId",
                table: "SalesOrder",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_CreatedAt",
                table: "SalesOrder",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_CreatedByCustomerId",
                table: "SalesOrder",
                column: "CreatedByCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_CreatedByUserId",
                table: "SalesOrder",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_CustomerId",
                table: "SalesOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_Dated_D",
                table: "SalesOrder",
                column: "Dated",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_DeletedAt",
                table: "SalesOrder",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_DeletedBy",
                table: "SalesOrder",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_ShippingCustomerAddressId",
                table: "SalesOrder",
                column: "ShippingCustomerAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_UpdatedByCustomerId",
                table: "SalesOrder",
                column: "UpdatedByCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_UpdatedByUserId",
                table: "SalesOrder",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "SalesOrder_PaymentStatus",
                table: "SalesOrder",
                column: "PaymentStatus");

            migrationBuilder.CreateIndex(
                name: "SalesOrder_ShippingStatus",
                table: "SalesOrder",
                column: "ShippingStatus");

            migrationBuilder.CreateIndex(
                name: "SalesOrder_Status",
                table: "SalesOrder",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "SalesOrder_Total",
                table: "SalesOrder",
                column: "Total");

            migrationBuilder.CreateIndex(
                name: "UK_SalesOrder_Number",
                table: "SalesOrder",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_CartLineId",
                table: "SalesOrderLine",
                column: "CartLineId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_CreatedAt",
                table: "SalesOrderLine",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_CreatedByCustomerId",
                table: "SalesOrderLine",
                column: "CreatedByCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_CreatedByUserId",
                table: "SalesOrderLine",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_DeletedAt",
                table: "SalesOrderLine",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_DeletedBy",
                table: "SalesOrderLine",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_ProductId",
                table: "SalesOrderLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_SalesOrderId",
                table: "SalesOrderLine",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_Total",
                table: "SalesOrderLine",
                column: "Total");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_UpdatedByCustomerId",
                table: "SalesOrderLine",
                column: "UpdatedByCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_UpdatedByUserId",
                table: "SalesOrderLine",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductId_Type_SortOrder",
                table: "TextListItem",
                columns: new[] { "ProductId", "Type", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_TextListItem_CreatedAt",
                table: "TextListItem",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_TextListItem_CreatedBy",
                table: "TextListItem",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TextListItem_DeletedAt",
                table: "TextListItem",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_TextListItem_DeletedBy",
                table: "TextListItem",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TextListItem_Discriminator",
                table: "TextListItem",
                column: "Discriminator");

            migrationBuilder.CreateIndex(
                name: "IX_TextListItem_Type",
                table: "TextListItem",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_TextListItem_UpdatedBy",
                table: "TextListItem",
                column: "UpdatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserClaim");

            migrationBuilder.DropTable(
                name: "AppUserLogin");

            migrationBuilder.DropTable(
                name: "AppUserRole");

            migrationBuilder.DropTable(
                name: "AppUserToken");

            migrationBuilder.DropTable(
                name: "DiscountProduct");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "KVPListItem");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "ProductGroupProductAttribute");

            migrationBuilder.DropTable(
                name: "ProductProductAttribute");

            migrationBuilder.DropTable(
                name: "ProductQnA");

            migrationBuilder.DropTable(
                name: "ProductReview");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "SalesOrderLine");

            migrationBuilder.DropTable(
                name: "TextListItem");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "ImageType");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "ProductAttributeValue");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "CartLine");

            migrationBuilder.DropTable(
                name: "SalesOrder");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "ProductAttribute");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "CustomerAddress");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "DeliveryWindow");

            migrationBuilder.DropTable(
                name: "ProductGroup");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
