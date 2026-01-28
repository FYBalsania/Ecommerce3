using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ecommerce3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:citext", ",,")
                .Annotation("Npgsql:PostgresExtension:ltree", ",,")
                .Annotation("Npgsql:PostgresExtension:pg_trgm", ",,");

            migrationBuilder.CreateTable(
                name: "AppUser",
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
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "citext", maxLength: 64, nullable: false),
                    LastName = table.Column<string>(type: "citext", maxLength: 64, nullable: true),
                    CompanyName = table.Column<string>(type: "citext", maxLength: 256, nullable: true),
                    EmailAddress = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(type: "citext", maxLength: 64, nullable: true),
                    Password = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordResetToken = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    PasswordResetTokenExpiry = table.Column<DateTime>(type: "timestamp", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
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
                        name: "FK_AppUserClaim_AppUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUser",
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
                        name: "FK_AppUserLogin_AppUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUser",
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
                        name: "FK_AppUserToken_AppUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bank_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bank_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bank_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Display = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Breadcrumb = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    AnchorText = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    AnchorTitle = table.Column<string>(type: "citext", maxLength: 256, nullable: true),
                    ShortDescription = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    FullDescription = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brand_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Brand_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Brand_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Display = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Breadcrumb = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    AnchorText = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    AnchorTitle = table.Column<string>(type: "citext", maxLength: 256, nullable: true),
                    GoogleCategory = table.Column<string>(type: "citext", maxLength: 1024, nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    Path = table.Column<string>(type: "ltree", nullable: false),
                    ShortDescription = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    FullDescription = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
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
                name: "countries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    iso2_code = table.Column<string>(type: "char(2)", nullable: false),
                    iso3_code = table.Column<string>(type: "char(3)", nullable: false),
                    numeric_code = table.Column<string>(type: "char(3)", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.id);
                    table.ForeignKey(
                        name: "FK_countries_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_countries_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_countries_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryWindow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Unit = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    MinValue = table.Column<int>(type: "integer", nullable: false),
                    MaxValue = table.Column<int>(type: "integer", nullable: true),
                    NormalizedMinDays = table.Column<decimal>(type: "numeric(18,1)", nullable: false),
                    NormalizedMaxDays = table.Column<decimal>(type: "numeric(18,1)", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryWindow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryWindow_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryWindow_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryWindow_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Discriminator = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    Code = table.Column<string>(type: "citext", maxLength: 16, nullable: false),
                    Name = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    StartAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    EndAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    MinOrderValue = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    Type = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    Percent = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    MaxDiscountAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discount_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Discount_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Discount_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImageType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Entity = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "citext", maxLength: 128, nullable: false),
                    Slug = table.Column<string>(type: "citext", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageType_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageType_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageType_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostCode_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostCode_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostCode_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Display = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Breadcrumb = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    DataType = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    SortOrder = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Display = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Breadcrumb = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    AnchorText = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    AnchorTitle = table.Column<string>(type: "citext", maxLength: 256, nullable: true),
                    ShortDescription = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    FullDescription = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductGroup_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductGroup_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductGroup_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "unit_of_measures",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    singular_name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    plural_name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    type = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    base_id = table.Column<int>(type: "integer", nullable: true),
                    conversion_factor = table.Column<decimal>(type: "numeric(20,8)", nullable: false),
                    decimal_places = table.Column<byte>(type: "smallint", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit_of_measures", x => x.id);
                    table.ForeignKey(
                        name: "FK_unit_of_measures_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_unit_of_measures_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_unit_of_measures_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_unit_of_measures_unit_of_measures_base_id",
                        column: x => x.base_id,
                        principalTable: "unit_of_measures",
                        principalColumn: "id",
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
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cart_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cart_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
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
                    FullName = table.Column<string>(type: "citext", maxLength: 128, nullable: true),
                    PhoneNumber = table.Column<string>(type: "citext", maxLength: 64, nullable: true),
                    CompanyName = table.Column<string>(type: "citext", maxLength: 256, nullable: true),
                    AddressLine1 = table.Column<string>(type: "citext", maxLength: 512, nullable: false),
                    AddressLine2 = table.Column<string>(type: "citext", maxLength: 512, nullable: true),
                    City = table.Column<string>(type: "citext", maxLength: 64, nullable: false),
                    StateOrProvince = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    PostalCode = table.Column<string>(type: "citext", maxLength: 16, nullable: false),
                    Landmark = table.Column<string>(type: "citext", maxLength: 512, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
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
                        name: "FK_AppUserRole_AppUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUser",
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
                    Value = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Display = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Breadcrumb = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    BooleanValue = table.Column<bool>(type: "boolean", nullable: true),
                    DateOnlyValue = table.Column<DateOnly>(type: "date", nullable: true),
                    DecimalValue = table.Column<decimal>(type: "numeric(18,3)", nullable: true),
                    HexCode = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    ColourFamily = table.Column<string>(type: "citext", maxLength: 64, nullable: true),
                    ColourFamilyHexCode = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    SortOrder = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValue_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValue_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValue_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValue_ProductAttribute_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Name = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Display = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    Breadcrumb = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    AnchorText = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    AnchorTitle = table.Column<string>(type: "citext", maxLength: 256, nullable: true),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    ProductGroupId = table.Column<int>(type: "integer", nullable: true),
                    ShortDescription = table.Column<string>(type: "varchar(512)", maxLength: 1024, nullable: true),
                    FullDescription = table.Column<string>(type: "text", nullable: true),
                    AllowReviews = table.Column<bool>(type: "boolean", nullable: false),
                    AverageRating = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalReviews = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    OldPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    CostPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    Stock = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    MinStock = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    ShowAvailability = table.Column<bool>(type: "boolean", nullable: false),
                    FreeShipping = table.Column<bool>(type: "boolean", nullable: false),
                    AdditionalShippingCharge = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    UnitOfMeasureId = table.Column<int>(type: "integer", nullable: false),
                    QuantityPerUnitOfMeasure = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    DeliveryWindowId = table.Column<int>(type: "integer", nullable: false),
                    MinOrderQuantity = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    MaxOrderQuantity = table.Column<decimal>(type: "numeric(18,3)", nullable: true),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: false),
                    IsNew = table.Column<bool>(type: "boolean", nullable: false),
                    IsBestSeller = table.Column<bool>(type: "boolean", nullable: false),
                    IsReturnable = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    RedirectUrl = table.Column<string>(type: "citext", maxLength: 2048, nullable: true),
                    SortOrder = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    CountryOfOriginId = table.Column<int>(type: "integer", nullable: false),
                    facets = table.Column<List<string>>(type: "text[]", nullable: false, defaultValueSql: "'{}'"),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
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
                    table.ForeignKey(
                        name: "FK_Product_countries_CountryOfOriginId",
                        column: x => x.CountryOfOriginId,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_unit_of_measures_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "unit_of_measures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "citext", maxLength: 16, nullable: false),
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
                    created_by_customer_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by_customer_id = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "integer", nullable: true),
                    BillingAddressReference = table.Column<string>(type: "jsonb", nullable: false),
                    CustomerReference = table.Column<string>(type: "jsonb", nullable: false),
                    ShippingAddressReference = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrder_AppUser_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrder_AppUser_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrder_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
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
                        name: "FK_SalesOrder_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrder_Customer_created_by_customer_id",
                        column: x => x.created_by_customer_id,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrder_Customer_updated_by_customer_id",
                        column: x => x.updated_by_customer_id,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroupProductAttribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductGroupId = table.Column<int>(type: "integer", nullable: false),
                    ProductAttributeId = table.Column<int>(type: "integer", nullable: false),
                    ProductAttributeSortOrder = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ProductAttributeValueId = table.Column<int>(type: "integer", nullable: false),
                    ProductAttributeValueSortOrder = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroupProductAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductGroupProductAttribute_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductGroupProductAttribute_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductGroupProductAttribute_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
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
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartLine_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartLine_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartLine_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiscountId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountProduct_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountProduct_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
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
                    SortOrder = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    ProductId = table.Column<int>(type: "integer", nullable: true),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KVPListItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KVPListItem_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KVPListItem_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KVPListItem_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
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
                    Path = table.Column<string>(type: "citext", maxLength: 256, nullable: true),
                    MetaTitle = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    MetaDescription = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true),
                    MetaKeywords = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    MetaRobots = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    CanonicalUrl = table.Column<string>(type: "citext", maxLength: 2048, nullable: true),
                    OgTitle = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    OgDescription = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true),
                    OgImageUrl = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true),
                    OgType = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    TwitterCard = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    ContentHtml = table.Column<string>(type: "text", nullable: true),
                    H1 = table.Column<string>(type: "citext", maxLength: 256, nullable: true),
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
                    BankId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Page_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Page_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Page_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Page_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategory_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategory_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategory_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductProductAttribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    ProductAttributeId = table.Column<int>(type: "integer", nullable: false),
                    ProductAttributeSortOrder = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ProductAttributeValueId = table.Column<int>(type: "integer", nullable: false),
                    ProductAttributeValueSortOrder = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductProductAttribute_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductProductAttribute_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductProductAttribute_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
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
                    Question = table.Column<string>(type: "citext", maxLength: 2048, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    Answer = table.Column<string>(type: "citext", nullable: true),
                    AnsweredBy = table.Column<int>(type: "integer", nullable: true),
                    AnsweredOn = table.Column<DateTime>(type: "timestamp", nullable: true),
                    AnswererIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    Approver = table.Column<int>(type: "integer", nullable: true),
                    ApprovedOn = table.Column<DateTime>(type: "timestamp", nullable: true),
                    ApproverIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQnA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductQnA_AppUser_AnsweredBy",
                        column: x => x.AnsweredBy,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductQnA_AppUser_Approver",
                        column: x => x.Approver,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductQnA_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductQnA_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductQnA_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
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
                    Review = table.Column<string>(type: "citext", maxLength: 18432, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    Approver = table.Column<int>(type: "integer", nullable: true),
                    ApprovedOn = table.Column<DateTime>(type: "timestamp", nullable: true),
                    ApproverIp = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReview_AppUser_Approver",
                        column: x => x.Approver,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductReview_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductReview_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductReview_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
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
                    Text = table.Column<string>(type: "citext", nullable: false),
                    SortOrder = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: true),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextListItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextListItem_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TextListItem_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TextListItem_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
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
                    created_by_customer_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by_customer_id = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedByUserId = table.Column<int>(type: "integer", nullable: true),
                    ProductReference = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_AppUser_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_AppUser_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_CartLine_CartLineId",
                        column: x => x.CartLineId,
                        principalTable: "CartLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_Customer_created_by_customer_id",
                        column: x => x.created_by_customer_id,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_Customer_updated_by_customer_id",
                        column: x => x.updated_by_customer_id,
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
                    Discriminator = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    OgFileName = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    FileName = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    FileExtension = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    ImageTypeId = table.Column<int>(type: "integer", nullable: false),
                    Size = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    AltText = table.Column<string>(type: "citext", maxLength: 128, nullable: true),
                    Title = table.Column<string>(type: "citext", maxLength: 128, nullable: true),
                    Loading = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    Link = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    LinkTarget = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    BrandId = table.Column<int>(type: "integer", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    ProductId = table.Column<int>(type: "integer", nullable: true),
                    ProductGroupId = table.Column<int>(type: "integer", nullable: true),
                    PageId = table.Column<int>(type: "integer", nullable: true),
                    BankId = table.Column<int>(type: "integer", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by_ip = table.Column<IPAddress>(type: "inet", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by_ip = table.Column<IPAddress>(type: "inet", nullable: true),
                    deleted_by = table.Column<int>(type: "integer", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by_ip = table.Column<IPAddress>(type: "inet", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_AppUser_created_by",
                        column: x => x.created_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_AppUser_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_AppUser_updated_by",
                        column: x => x.updated_by,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
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
                        onDelete: ReferentialAction.Cascade);
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
                name: "EmailIndex",
                table: "AppUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AppUser",
                column: "NormalizedUserName",
                unique: true);

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
                name: "idx_bank_created_at",
                table: "Bank",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_bank_deleted_at",
                table: "Bank",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_Bank_created_by",
                table: "Bank",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_Bank_deleted_by",
                table: "Bank",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_Bank_IsActive",
                table: "Bank",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Bank_SortOrder",
                table: "Bank",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Bank_updated_by",
                table: "Bank",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "UK_Bank_Name",
                table: "Bank",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Bank_Slug",
                table: "Bank",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_brand_created_at",
                table: "Brand",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_brand_deleted_at",
                table: "Brand",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_AnchorText",
                table: "Brand",
                column: "AnchorText")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Brand_AnchorTitle",
                table: "Brand",
                column: "AnchorTitle")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Brand_Breadcrumb",
                table: "Brand",
                column: "Breadcrumb")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Brand_created_by",
                table: "Brand",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_deleted_by",
                table: "Brand",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_Display",
                table: "Brand",
                column: "Display")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Brand_IsActive",
                table: "Brand",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_SortOrder",
                table: "Brand",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_updated_by",
                table: "Brand",
                column: "updated_by");

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
                name: "idx_cart_created_at",
                table: "Cart",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_cart_deleted_at",
                table: "Cart",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_created_by",
                table: "Cart",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CustomerId",
                table: "Cart",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_deleted_by",
                table: "Cart",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_SessionId",
                table: "Cart",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_Total",
                table: "Cart",
                column: "Total");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_updated_by",
                table: "Cart",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "idx_cart_line_created_at",
                table: "CartLine",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_cart_line_deleted_at",
                table: "CartLine",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_CartId",
                table: "CartLine",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_created_by",
                table: "CartLine",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_deleted_by",
                table: "CartLine",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_ProductId",
                table: "CartLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_Total",
                table: "CartLine",
                column: "Total");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_updated_by",
                table: "CartLine",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "idx_category_created_at",
                table: "Category",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_category_deleted_at",
                table: "Category",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_Category_AnchorText",
                table: "Category",
                column: "AnchorText")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Category_AnchorTitle",
                table: "Category",
                column: "AnchorTitle")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Category_Breadcrumb",
                table: "Category",
                column: "Breadcrumb")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Category_created_by",
                table: "Category",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_Category_deleted_by",
                table: "Category",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Display",
                table: "Category",
                column: "Display")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Category_GoogleCategory",
                table: "Category",
                column: "GoogleCategory")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Category_IsActive",
                table: "Category",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                table: "Category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Path",
                table: "Category",
                column: "Path")
                .Annotation("Npgsql:IndexMethod", "gist");

            migrationBuilder.CreateIndex(
                name: "IX_Category_SortOrder",
                table: "Category",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Category_updated_by",
                table: "Category",
                column: "updated_by");

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
                name: "idx_country_created_at",
                table: "countries",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_country_deleted_at",
                table: "countries",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "idx_country_is_active",
                table: "countries",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "idx_country_name",
                table: "countries",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_country_sort_order",
                table: "countries",
                column: "sort_order");

            migrationBuilder.CreateIndex(
                name: "IX_countries_created_by",
                table: "countries",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_countries_deleted_by",
                table: "countries",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_countries_updated_by",
                table: "countries",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "idx_customer_created_at",
                table: "Customer",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_customer_deleted_at",
                table: "Customer",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CompanyName",
                table: "Customer",
                column: "CompanyName")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_FirstName",
                table: "Customer",
                column: "FirstName")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_LastName",
                table: "Customer",
                column: "LastName")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

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
                column: "PhoneNumber")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "idx_customer_address_created_at",
                table: "CustomerAddress",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_customer_address_deleted_at",
                table: "CustomerAddress",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_AddressLine1",
                table: "CustomerAddress",
                column: "AddressLine1")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_AddressLine2",
                table: "CustomerAddress",
                column: "AddressLine2")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_City",
                table: "CustomerAddress",
                column: "City")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_CompanyName",
                table: "CustomerAddress",
                column: "CompanyName")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_CustomerId",
                table: "CustomerAddress",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_FullName",
                table: "CustomerAddress",
                column: "FullName")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_Landmark",
                table: "CustomerAddress",
                column: "Landmark")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_PhoneNumber",
                table: "CustomerAddress",
                column: "PhoneNumber")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_PostalCode",
                table: "CustomerAddress",
                column: "PostalCode")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_StateOrProvince",
                table: "CustomerAddress",
                column: "StateOrProvince")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "idx_delivery_window_created_at",
                table: "DeliveryWindow",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_delivery_window_deleted_at",
                table: "DeliveryWindow",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryWindow_created_by",
                table: "DeliveryWindow",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryWindow_deleted_by",
                table: "DeliveryWindow",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryWindow_Name_SortOrder_IsActive",
                table: "DeliveryWindow",
                columns: new[] { "Name", "SortOrder", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryWindow_updated_by",
                table: "DeliveryWindow",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "UK_DeliveryWindow_Name",
                table: "DeliveryWindow",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_discount_created_at",
                table: "Discount",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_discount_deleted_at",
                table: "Discount",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_created_by",
                table: "Discount",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_deleted_by",
                table: "Discount",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_Discriminator",
                table: "Discount",
                column: "Discriminator");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_EndAt",
                table: "Discount",
                column: "EndAt");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_IsActive",
                table: "Discount",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_StartAt",
                table: "Discount",
                column: "StartAt");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_updated_by",
                table: "Discount",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "UK_Discount_Code",
                table: "Discount",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Discount_Name",
                table: "Discount",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountProduct_created_by",
                table: "DiscountProduct",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountProduct_deleted_by",
                table: "DiscountProduct",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountProduct_DiscountId",
                table: "DiscountProduct",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountProduct_ProductId",
                table: "DiscountProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "idx_image_created_at",
                table: "Image",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_image_deleted_at",
                table: "Image",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_Image_AltText",
                table: "Image",
                column: "AltText")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_BankId",
                table: "Image",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_BrandId",
                table: "Image",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_CategoryId",
                table: "Image",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_created_by",
                table: "Image",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_Image_deleted_by",
                table: "Image",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_Image_FileName",
                table: "Image",
                column: "FileName")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_ImageTypeId",
                table: "Image",
                column: "ImageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_OgFileName",
                table: "Image",
                column: "OgFileName")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_PageId",
                table: "Image",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ProductGroupId",
                table: "Image",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ProductId",
                table: "Image",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_SortOrder",
                table: "Image",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Image_Title",
                table: "Image",
                column: "Title")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_updated_by",
                table: "Image",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "idx_image_type_created_at",
                table: "ImageType",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_image_type_deleted_at",
                table: "ImageType",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_ImageType_created_by",
                table: "ImageType",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_ImageType_deleted_by",
                table: "ImageType",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_ImageType_Entity",
                table: "ImageType",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ImageType_IsActive",
                table: "ImageType",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ImageType_Name",
                table: "ImageType",
                column: "Name")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_ImageType_Slug",
                table: "ImageType",
                column: "Slug")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_ImageType_updated_by",
                table: "ImageType",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "UK_ImageType_Entity_Name",
                table: "ImageType",
                columns: new[] { "Entity", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_ImageType_Entity_Slug",
                table: "ImageType",
                columns: new[] { "Entity", "Slug" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_kvp_list_item_created_at",
                table: "KVPListItem",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_kvp_list_item_deleted_at",
                table: "KVPListItem",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryKVPListItem_CategoryId_Type",
                table: "KVPListItem",
                columns: new[] { "CategoryId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_KVPListItem_created_by",
                table: "KVPListItem",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_KVPListItem_deleted_by",
                table: "KVPListItem",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_KVPListItem_updated_by",
                table: "KVPListItem",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductKVPListItem_ProductId_Type",
                table: "KVPListItem",
                columns: new[] { "ProductId", "Type" });

            migrationBuilder.CreateIndex(
                name: "idx_page_created_at",
                table: "Page",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_page_deleted_at",
                table: "Page",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_Page_BankId",
                table: "Page",
                column: "BankId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Page_BrandId",
                table: "Page",
                column: "BrandId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Page_CanonicalUrl",
                table: "Page",
                column: "CanonicalUrl")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Page_CategoryId",
                table: "Page",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Page_created_by",
                table: "Page",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_Page_deleted_by",
                table: "Page",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_Page_H1",
                table: "Page",
                column: "H1")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Page_IsActive",
                table: "Page",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Page_IsIndexed",
                table: "Page",
                column: "IsIndexed");

            migrationBuilder.CreateIndex(
                name: "IX_Page_MetaTitle",
                table: "Page",
                column: "MetaTitle")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Page_Path",
                table: "Page",
                column: "Path");

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
                name: "IX_Page_updated_by",
                table: "Page",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "UK_ProductGroupPage_ProductGroupId_DeletedAt",
                table: "Page",
                columns: new[] { "ProductGroupId", "deleted_at" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_ProductPage_ProductId_DeletedAt",
                table: "Page",
                columns: new[] { "ProductId", "deleted_at" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_postcode_created_at",
                table: "PostCode",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_postcode_deleted_at",
                table: "PostCode",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_PostCode_created_by",
                table: "PostCode",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_PostCode_deleted_by",
                table: "PostCode",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_PostCode_IsActive",
                table: "PostCode",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_PostCode_updated_by",
                table: "PostCode",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "UK_PostCode_Code",
                table: "PostCode",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_product_created_at",
                table: "Product",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_product_deleted_at",
                table: "Product",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_Product_AnchorText",
                table: "Product",
                column: "AnchorText")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Product_AnchorTitle",
                table: "Product",
                column: "AnchorTitle")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                table: "Product",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Breadcrumb",
                table: "Product",
                column: "Breadcrumb")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CountryOfOriginId",
                table: "Product",
                column: "CountryOfOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_created_by",
                table: "Product",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_Product_deleted_by",
                table: "Product",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeliveryWindowId",
                table: "Product",
                column: "DeliveryWindowId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Display",
                table: "Product",
                column: "Display")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Product_EAN",
                table: "Product",
                column: "EAN");

            migrationBuilder.CreateIndex(
                name: "IX_Product_facets",
                table: "Product",
                column: "facets")
                .Annotation("Npgsql:IndexMethod", "gin");

            migrationBuilder.CreateIndex(
                name: "IX_Product_GTIN",
                table: "Product",
                column: "GTIN");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MFC",
                table: "Product",
                column: "MFC");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MPN",
                table: "Product",
                column: "MPN");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Price",
                table: "Product",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductGroupId",
                table: "Product",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_QuantityPerUnitOfMeasure",
                table: "Product",
                column: "QuantityPerUnitOfMeasure");

            migrationBuilder.CreateIndex(
                name: "IX_Product_RedirectUrl",
                table: "Product",
                column: "RedirectUrl")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Product_SortOrder",
                table: "Product",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Status",
                table: "Product",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Stock",
                table: "Product",
                column: "Stock");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitOfMeasureId",
                table: "Product",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UPC",
                table: "Product",
                column: "UPC");

            migrationBuilder.CreateIndex(
                name: "IX_Product_updated_by",
                table: "Product",
                column: "updated_by");

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
                name: "idx_product_attribute_created_at",
                table: "ProductAttribute",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_product_attribute_deleted_at",
                table: "ProductAttribute",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_Breadcrumb",
                table: "ProductAttribute",
                column: "Breadcrumb")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_created_by",
                table: "ProductAttribute",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_deleted_by",
                table: "ProductAttribute",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_Display",
                table: "ProductAttribute",
                column: "Display")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_SortOrder",
                table: "ProductAttribute",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_updated_by",
                table: "ProductAttribute",
                column: "updated_by");

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
                name: "idx_product_attribute_value_created_at",
                table: "ProductAttributeValue",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_product_attribute_value_deleted_at",
                table: "ProductAttributeValue",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeBooleanValue_ProductAttributeId_SortOrder_BooleanValue",
                table: "ProductAttributeValue",
                columns: new[] { "ProductAttributeId", "SortOrder", "BooleanValue" },
                filter: "(\"deleted_at\") IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeColourValue_ColourFamily",
                table: "ProductAttributeValue",
                column: "ColourFamily")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeDateOnlyValue_ProductAttributeId_SortOrder_DateOnlyValue",
                table: "ProductAttributeValue",
                columns: new[] { "ProductAttributeId", "SortOrder", "DateOnlyValue" },
                filter: "(\"deleted_at\") IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeDecimalValue_ProductAttributeId_SortOrder_DecimalValue",
                table: "ProductAttributeValue",
                columns: new[] { "ProductAttributeId", "SortOrder", "DecimalValue" },
                filter: "(\"deleted_at\") IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_Breadcrumb",
                table: "ProductAttributeValue",
                column: "Breadcrumb")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_created_by",
                table: "ProductAttributeValue",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_deleted_by",
                table: "ProductAttributeValue",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_Display",
                table: "ProductAttributeValue",
                column: "Display")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_ProductAttributeId_SortOrder_Value",
                table: "ProductAttributeValue",
                columns: new[] { "ProductAttributeId", "SortOrder", "Value" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_updated_by",
                table: "ProductAttributeValue",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "UK_ProductAttributeValue_ProductAttributeId_Slug",
                table: "ProductAttributeValue",
                columns: new[] { "ProductAttributeId", "Slug" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_ProductAttributeValue_ProductAttributeId_Value",
                table: "ProductAttributeValue",
                columns: new[] { "ProductAttributeId", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_product_category_created_at",
                table: "ProductCategory",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_product_category_deleted_at",
                table: "ProductCategory",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "ProductCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_created_by",
                table: "ProductCategory",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_deleted_by",
                table: "ProductCategory",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_IsPrimary",
                table: "ProductCategory",
                column: "IsPrimary");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_updated_by",
                table: "ProductCategory",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "UK_ProductCategory_ProductId_CategoryId_DeletedAt",
                table: "ProductCategory",
                columns: new[] { "ProductId", "CategoryId", "deleted_at" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_product_group_created_at",
                table: "ProductGroup",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_product_group_deleted_at",
                table: "ProductGroup",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_AnchorText",
                table: "ProductGroup",
                column: "AnchorText")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_AnchorTitle",
                table: "ProductGroup",
                column: "AnchorTitle")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_Breadcrumb",
                table: "ProductGroup",
                column: "Breadcrumb")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_created_by",
                table: "ProductGroup",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_deleted_by",
                table: "ProductGroup",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_Display",
                table: "ProductGroup",
                column: "Display")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

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
                name: "IX_ProductGroup_updated_by",
                table: "ProductGroup",
                column: "updated_by");

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
                name: "idx_product_group_product_attribute_created_at",
                table: "ProductGroupProductAttribute",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_product_group_product_attribute_deleted_at",
                table: "ProductGroupProductAttribute",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroupProductAttribute_created_by",
                table: "ProductGroupProductAttribute",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroupProductAttribute_deleted_by",
                table: "ProductGroupProductAttribute",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroupProductAttribute_ProductAttributeId",
                table: "ProductGroupProductAttribute",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroupProductAttribute_ProductAttributeValueId",
                table: "ProductGroupProductAttribute",
                column: "ProductAttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroupProductAttribute_updated_by",
                table: "ProductGroupProductAttribute",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "UK_ProductGroupProductAttribute_ProductGroupId_ProductAttributeId_ProductAttributeValueId",
                table: "ProductGroupProductAttribute",
                columns: new[] { "ProductGroupId", "ProductAttributeId", "ProductAttributeValueId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_product_product_attribute_created_at",
                table: "ProductProductAttribute",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_product_product_attribute_deleted_at",
                table: "ProductProductAttribute",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductAttribute_created_by",
                table: "ProductProductAttribute",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductAttribute_deleted_by",
                table: "ProductProductAttribute",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductAttribute_ProductAttributeId",
                table: "ProductProductAttribute",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductAttribute_ProductAttributeValueId",
                table: "ProductProductAttribute",
                column: "ProductAttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductAttribute_updated_by",
                table: "ProductProductAttribute",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "UK_ProductProductAttribute_ProductId_ProductAttributeId_ProductAttributeValueId_DeletedAt",
                table: "ProductProductAttribute",
                columns: new[] { "ProductId", "ProductAttributeId", "ProductAttributeValueId", "deleted_at" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_product_qna_created_at",
                table: "ProductQnA",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_product_qna_deleted_at",
                table: "ProductQnA",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQnA_Answer",
                table: "ProductQnA",
                column: "Answer")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

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
                name: "IX_ProductQnA_created_by",
                table: "ProductQnA",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQnA_deleted_by",
                table: "ProductQnA",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQnA_ProductId",
                table: "ProductQnA",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQnA_SortOrder",
                table: "ProductQnA",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQnA_updated_by",
                table: "ProductQnA",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "UK_ProductQnA_Question",
                table: "ProductQnA",
                column: "Question")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "idx_product_review_created_at",
                table: "ProductReview",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_product_review_deleted_at",
                table: "ProductReview",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_ApprovedOn",
                table: "ProductReview",
                column: "ApprovedOn");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_Approver",
                table: "ProductReview",
                column: "Approver");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_created_by",
                table: "ProductReview",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_deleted_by",
                table: "ProductReview",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_ProductId",
                table: "ProductReview",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_Rating",
                table: "ProductReview",
                column: "Rating");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_Review",
                table: "ProductReview",
                column: "Review")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_SortOrder",
                table: "ProductReview",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_updated_by",
                table: "ProductReview",
                column: "updated_by");

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
                name: "idx_sales_order_created_at",
                table: "SalesOrder",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_sales_order_deleted_at",
                table: "SalesOrder",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_BillingCustomerAddressId",
                table: "SalesOrder",
                column: "BillingCustomerAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_CartId",
                table: "SalesOrder",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_created_by_customer_id",
                table: "SalesOrder",
                column: "created_by_customer_id");

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
                name: "IX_SalesOrder_deleted_by",
                table: "SalesOrder",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_ShippingCustomerAddressId",
                table: "SalesOrder",
                column: "ShippingCustomerAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_updated_by_customer_id",
                table: "SalesOrder",
                column: "updated_by_customer_id");

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
                name: "idx_sales_order_line_created_at",
                table: "SalesOrderLine",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_sales_order_line_deleted_at",
                table: "SalesOrderLine",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_CartLineId",
                table: "SalesOrderLine",
                column: "CartLineId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_created_by_customer_id",
                table: "SalesOrderLine",
                column: "created_by_customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_CreatedByUserId",
                table: "SalesOrderLine",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_deleted_by",
                table: "SalesOrderLine",
                column: "deleted_by");

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
                name: "IX_SalesOrderLine_updated_by_customer_id",
                table: "SalesOrderLine",
                column: "updated_by_customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_UpdatedByUserId",
                table: "SalesOrderLine",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "idx_text_list_item_created_at",
                table: "TextListItem",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_text_list_item_deleted_at",
                table: "TextListItem",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_ProductId_Type_SortOrder",
                table: "TextListItem",
                columns: new[] { "ProductId", "Type", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_TextListItem_created_by",
                table: "TextListItem",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_TextListItem_deleted_by",
                table: "TextListItem",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_TextListItem_Text",
                table: "TextListItem",
                column: "Text")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_TextListItem_Type",
                table: "TextListItem",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_TextListItem_updated_by",
                table: "TextListItem",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "ix_unit_of_measure_deleted_at",
                table: "unit_of_measures",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_unit_of_measures_base_id",
                table: "unit_of_measures",
                column: "base_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_unit_of_measures_created_by",
                table: "unit_of_measures",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_unit_of_measures_deleted_by",
                table: "unit_of_measures",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "ix_unit_of_measures_is_active",
                table: "unit_of_measures",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "IX_unit_of_measures_updated_by",
                table: "unit_of_measures",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "uk_unit_of_measures_code",
                table: "unit_of_measures",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_unit_of_measures_plural_name",
                table: "unit_of_measures",
                column: "plural_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_unit_of_measures_singular_name",
                table: "unit_of_measures",
                column: "singular_name",
                unique: true);
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
                name: "PostCode");

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
                name: "Bank");

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
                name: "countries");

            migrationBuilder.DropTable(
                name: "unit_of_measures");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "AppUser");
        }
    }
}
