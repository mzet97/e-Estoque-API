using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_Estoque_API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(80)", nullable: false),
                    Description = table.Column<string>(type: "varchar(5000)", nullable: false),
                    ShortDescription = table.Column<string>(type: "varchar(500)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyAddress",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Street = table.Column<string>(type: "varchar(80)", nullable: false),
                    Number = table.Column<string>(type: "varchar(80)", nullable: false),
                    Complement = table.Column<string>(type: "varchar(80)", nullable: false),
                    Neighborhood = table.Column<string>(type: "varchar(80)", nullable: false),
                    District = table.Column<string>(type: "varchar(80)", nullable: false),
                    City = table.Column<string>(type: "varchar(80)", nullable: false),
                    County = table.Column<string>(type: "varchar(80)", nullable: false),
                    ZipCode = table.Column<string>(type: "varchar(80)", nullable: false),
                    Latitude = table.Column<string>(type: "varchar(80)", nullable: false),
                    Longitude = table.Column<string>(type: "varchar(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Street = table.Column<string>(type: "varchar(80)", nullable: false),
                    Number = table.Column<string>(type: "varchar(80)", nullable: false),
                    Complement = table.Column<string>(type: "varchar(80)", nullable: false),
                    Neighborhood = table.Column<string>(type: "varchar(80)", nullable: false),
                    District = table.Column<string>(type: "varchar(80)", nullable: false),
                    City = table.Column<string>(type: "varchar(80)", nullable: false),
                    County = table.Column<string>(type: "varchar(80)", nullable: false),
                    ZipCode = table.Column<string>(type: "varchar(80)", nullable: false),
                    Latitude = table.Column<string>(type: "varchar(80)", nullable: false),
                    Longitude = table.Column<string>(type: "varchar(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taxs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(80)", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal", nullable: false),
                    IdCategory = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Taxs_Categories_IdCategory",
                        column: x => x.IdCategory,
                        principalSchema: "public",
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(80)", nullable: false),
                    DocId = table.Column<string>(type: "varchar(80)", nullable: false),
                    Email = table.Column<string>(type: "varchar(250)", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(80)", nullable: false),
                    IdCompanyAddress = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_CompanyAddress_IdCompanyAddress",
                        column: x => x.IdCompanyAddress,
                        principalSchema: "public",
                        principalTable: "CompanyAddress",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(80)", nullable: false),
                    DocId = table.Column<string>(type: "varchar(80)", nullable: false),
                    Email = table.Column<string>(type: "varchar(80)", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(80)", nullable: false),
                    IdCustomerAddress = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerAddress_IdCustomerAddress",
                        column: x => x.IdCustomerAddress,
                        principalSchema: "public",
                        principalTable: "CustomerAddress",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", nullable: false),
                    ShortDescription = table.Column<string>(type: "varchar(250)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal", nullable: false),
                    Height = table.Column<decimal>(type: "decimal", nullable: false),
                    Length = table.Column<decimal>(type: "decimal", nullable: false),
                    Image = table.Column<string>(type: "varchar(5000)", nullable: false),
                    IdCategory = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCompany = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_IdCategory",
                        column: x => x.IdCategory,
                        principalSchema: "public",
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Companies_IdCompany",
                        column: x => x.IdCompany,
                        principalSchema: "public",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal", nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal", nullable: false),
                    SaleType = table.Column<int>(type: "integer", nullable: false),
                    PaymentType = table.Column<int>(type: "integer", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    IdCustomer = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Customers_IdCustomer",
                        column: x => x.IdCustomer,
                        principalSchema: "public",
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "inventories",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    DateOrder = table.Column<string>(type: "varchar(80)", nullable: false),
                    IdProduct = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inventories_Products_IdProduct",
                        column: x => x.IdProduct,
                        principalSchema: "public",
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SaleProducts",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    IdProduct = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSale = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleProducts_Products_IdProduct",
                        column: x => x.IdProduct,
                        principalSchema: "public",
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleProducts_Sales_IdSale",
                        column: x => x.IdSale,
                        principalSchema: "public",
                        principalTable: "Sales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_IdCompanyAddress",
                schema: "public",
                table: "Companies",
                column: "IdCompanyAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_IdCustomerAddress",
                schema: "public",
                table: "Customers",
                column: "IdCustomerAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IdCategory",
                schema: "public",
                table: "Products",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IdCompany",
                schema: "public",
                table: "Products",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProducts_IdProduct",
                schema: "public",
                table: "SaleProducts",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProducts_IdSale",
                schema: "public",
                table: "SaleProducts",
                column: "IdSale");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_IdCustomer",
                schema: "public",
                table: "Sales",
                column: "IdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Taxs_IdCategory",
                schema: "public",
                table: "Taxs",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_IdProduct",
                schema: "public",
                table: "inventories",
                column: "IdProduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleProducts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Taxs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "inventories",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Sales",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "public");

            migrationBuilder.DropTable(
                name: "CustomerAddress",
                schema: "public");

            migrationBuilder.DropTable(
                name: "CompanyAddress",
                schema: "public");
        }
    }
}
