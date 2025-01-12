using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KBA.SellerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTabelPRoduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_BatteryDetails_BatteryDetailsId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_BatteryDetails_BatteryDetailsId1",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_EventTypes_EventTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BatteryDetailsId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BatteryDetailsId1",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "EventTypeId",
                table: "Products",
                newName: "ProductEventTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_EventTypeId",
                table: "Products",
                newName: "IX_Products_ProductEventTypeId");

            migrationBuilder.AlterColumn<int>(
                name: "BatteryDetailsId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductBatteryDetailsId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductBatteryDetailsId",
                table: "Products",
                column: "ProductBatteryDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BatteryDetails_BatteryDetailsId",
                table: "Products",
                column: "BatteryDetailsId",
                principalTable: "BatteryDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BatteryDetails_ProductBatteryDetailsId",
                table: "Products",
                column: "ProductBatteryDetailsId",
                principalTable: "BatteryDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_EventTypes_ProductEventTypeId",
                table: "Products",
                column: "ProductEventTypeId",
                principalTable: "EventTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_BatteryDetails_BatteryDetailsId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_BatteryDetails_ProductBatteryDetailsId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_EventTypes_ProductEventTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductBatteryDetailsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductBatteryDetailsId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductEventTypeId",
                table: "Products",
                newName: "EventTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductEventTypeId",
                table: "Products",
                newName: "IX_Products_EventTypeId");

            migrationBuilder.AlterColumn<int>(
                name: "BatteryDetailsId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BatteryDetailsId1",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BatteryDetailsId1",
                table: "Products",
                column: "BatteryDetailsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BatteryDetails_BatteryDetailsId",
                table: "Products",
                column: "BatteryDetailsId",
                principalTable: "BatteryDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BatteryDetails_BatteryDetailsId1",
                table: "Products",
                column: "BatteryDetailsId1",
                principalTable: "BatteryDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_EventTypes_EventTypeId",
                table: "Products",
                column: "EventTypeId",
                principalTable: "EventTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
