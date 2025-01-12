using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KBA.SellerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeNewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "BatteryDetails");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "UserLogins",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "UserLogins",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "UserLogins",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "UserLogins",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "BatteryDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatteryDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsVisibleForSeller = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductBatteryDetailsId = table.Column<int>(type: "int", nullable: false),
                    ProductEventTypeId = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    ActivationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BatteryDetailsId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_BatteryDetails_BatteryDetailsId",
                        column: x => x.BatteryDetailsId,
                        principalTable: "BatteryDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_BatteryDetails_ProductBatteryDetailsId",
                        column: x => x.ProductBatteryDetailsId,
                        principalTable: "BatteryDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_EventTypes_ProductEventTypeId",
                        column: x => x.ProductEventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BatteryDetailsId",
                table: "Products",
                column: "BatteryDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductBatteryDetailsId",
                table: "Products",
                column: "ProductBatteryDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductEventTypeId",
                table: "Products",
                column: "ProductEventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerId",
                table: "Products",
                column: "SellerId");
        }
    }
}
